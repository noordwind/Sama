using System;
using System.Threading.Tasks;
using Sama.Infrastructure.Authentication;
using Sama.Core.Types;
using Microsoft.AspNetCore.Identity;
using Sama.Core.Domain;
using Sama.Services.Dispatchers;
using System.Linq;
using AutoMapper;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Factories;
using Sama.Core.Domain.Identity.Repositories;
using Sama.Services.Identity.Dtos;
using Sama.Services.Shared.Dtos;

namespace Sama.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserFactory _userFactory;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IEventDispatcher _eventDispatcher;

        public IdentityService(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IUserFactory userFactory,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler,
            IEventDispatcher eventDispatcher)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _userFactory = userFactory;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<UserDto> GetAsync(Guid id)
            => _mapper.Map<UserDto>(await _userRepository.GetAsync(id));

        public async Task AddFunds(Guid id, decimal funds)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new ServiceException("user_not_found", 
                    $"User: '{id}' was not found.");
            }
            user.AddFunds(new Payment(Guid.NewGuid(), id, funds, "hash"));
            await _userRepository.UpdateAsync(user);
        }

        public async Task SignUpAsync(Guid id, string email, string username, string password, string role)
        {
            var user = await _userFactory.CreateAsync(id, email, username, password, role);
            await _userRepository.CreateAsync(user);
            await _eventDispatcher.DispatchAsync(user.Events.ToArray());
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !ValidatePassword(user,password, _passwordHasher))
            {
                throw new ServiceException("invalid_credentials",
                    "Invalid credentials.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return jwt;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException("user_not_found", 
                    $"User: '{userId}' was not found.");
            }
            if (!ValidatePassword(user, currentPassword, _passwordHasher))
            {
                throw new ServiceException("invalid_current_password", 
                    "Invalid current password.");
            }
            SetPassword(user, newPassword);
            await _userRepository.UpdateAsync(user);            
        }

        private void SetPassword(User user, string password)
        {
            var passwordHash = _passwordHasher.HashPassword(user, password);
            user.SetPasswordHash(passwordHash);
        }

        private static bool ValidatePassword(User user, string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Failed;
    }
}