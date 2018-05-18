using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sama.Core.Domain.Donations;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Services;
using Sama.Core.Domain.Ngos;
using Sama.Infrastructure.Mongo;

namespace Sama.Services.Data
{
    public class DataInitializer : MongoDbSeeder
    {
        private readonly IPasswordHasher _passwordHasher;

        public DataInitializer(IMongoDatabase database, 
            IPasswordHasher passwordHasher) : base(database)
        {
            _passwordHasher = passwordHasher;
        }

        protected override async Task CustomSeedAsync()
        {
            var cursor = await Database.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();
            if (collections.Any())
            {
                return;
            }
            await CreateUsersAsync();
        }

        private async Task CreateUsersAsync()
        {
            await CreateAdminAsync();
            for (var i=1; i<11; i++)
            {
                await CreateUserAsync(i);
                await CreateNgoUsersAsync(i);
            }
            await CreateNgoAsync(1, 50.049683, 19.854544);
            await CreateNgoAsync(2, 50.079683, 19.984544);
            await CreateNgoAsync(3, 50.019683, 19.544544);
            await CreateNgoAsync(4, 50.029683, 19.734544);
            await CreateNgoAsync(5, 50.059683, 19.944544);
        }

        private async Task CreateAdminAsync()
        {
            var user = new User(Guid.NewGuid(), $"admin@sama.network", "admin");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
        }

        private async Task CreateUserAsync(int number)
        {
            var user = new User(Guid.NewGuid(), $"user{number}@sama.network", "user");
            _passwordHasher.SetPasswordHash(user, "secret");
            var payment = new Payment(Guid.NewGuid(), user.Id, 1000, "secure-hash");
            user.SetFunds(payment.Value);
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
            await Database.GetCollection<Payment>("Payments").InsertOneAsync(payment);
        }

        private async Task CreateNgoUsersAsync(int number)
        {
            var user = new User(Guid.NewGuid(), $"ngo{number}@sama.network", "user");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
        }

        private async Task CreateNgoAsync(int number, double latitude, double longitude)
        {
            var ngo = new Ngo(Guid.NewGuid(), $"NGO {number}",
                "address", latitude, longitude, approved: true);
            var children = CreateChildren().ToList();
            foreach (var child in children)
            {
                ngo.AddChild(child);
            }
            ngo.Donate(2000);
            ngo.DonateChildren();
            foreach (var child in children)
            {
                var childInfo = ngo.Children.Single(x => x.Id == child.Id);
                child.Donate(childInfo.Funds);
            }
            //var donation = new Donation(Guid.NewGuid())

            await Database.GetCollection<Ngo>("Ngos").InsertOneAsync(ngo);
            await Database.GetCollection<Child>("Children").InsertManyAsync(children);
        }

        private IEnumerable<Child> CreateChildren()
        {
            for (var i=0; i<10; i++)
            {
                yield return new Child(Guid.NewGuid(), "John Doe", DateTime.UtcNow.AddYears(-10));
            }
        }
    }
}