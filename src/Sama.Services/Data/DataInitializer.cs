using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Services;
using Sama.Core.Domain.Ngos;
using Sama.Infrastructure.Mongo;

namespace Sama.Services.Data
{
    public class DataInitializer : MongoDbSeeder
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly INamesGenerator _namesGenerator;

        public DataInitializer(IMongoDatabase database, 
            IPasswordHasher passwordHasher, INamesGenerator namesGenerator) : base(database)
        {
            _passwordHasher = passwordHasher;
            _namesGenerator = namesGenerator;
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
            var users = new List<User>();
            var ngoUsers = new List<User>();
            for (var i=1; i<11; i++)
            {
                users.Add(await CreateUserAsync(i));
                ngoUsers.Add(await CreateNgoUserAsync(i));
            }

            //Krakow
            await CreateNgoAsync(ngoUsers[0].Id, "Little kids", 50.049683, 19.854544, users);
            await CreateNgoAsync(ngoUsers[1].Id, "Small orphanage", 50.079683, 19.984544, users);
            await CreateNgoAsync(ngoUsers[2].Id, "Home for children" , 50.019683, 19.544544, users);
            await CreateNgoAsync(ngoUsers[3].Id, "Kids place", 50.029683, 19.734544, users);
            await CreateNgoAsync(ngoUsers[4].Id, "Children' hope", 50.059683, 19.944544, users);

           //Goa 
            await CreateNgoAsync(ngoUsers[5].Id, "Little kids", 15.533414, 73.764954, users);
            await CreateNgoAsync(ngoUsers[6].Id, "Small orphanage", 15.553414, 73.774954, users);
            await CreateNgoAsync(ngoUsers[7].Id, "Home for children", 15.543414, 73.714954, users);
            await CreateNgoAsync(ngoUsers[8].Id, "Kids place", 15.533414, 73.794954, users);
            await CreateNgoAsync(ngoUsers[9].Id, "Children' hope", 15.583414, 73.734954, users);
        }

        private async Task CreateAdminAsync()
        {
            var user = new User(Guid.NewGuid(), $"admin@sama.network", "admin");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
        }

        private async Task<User> CreateUserAsync(int number)
        {
            var user = new User(Guid.NewGuid(), $"user{number}@sama.network", "user");
            _passwordHasher.SetPasswordHash(user, "secret");
            var payment = new Payment(Guid.NewGuid(), user.Id, 5000, "secure-hash");
            user.AddFunds(payment);
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
            await Database.GetCollection<Payment>("Payments").InsertOneAsync(payment);

            return user;
        }

        private async Task<User> CreateNgoUserAsync(int number)
        {
            var user = new User(Guid.NewGuid(), $"ngo{number}@sama.network", "user");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);

            return user;
        }

        private async Task CreateNgoAsync(Guid ownerId, string name, double latitude, double longitude,
            IEnumerable<User> users)
        {
            var ngo = new Ngo(Guid.NewGuid(), ownerId, name, "address", 
                latitude, longitude, approved: true);
            var children = CreateChildren().ToList();
            foreach (var child in children)
            {
                ngo.AddChild(child);
            }
            foreach (var user in users)
            {
                var donation = user.Donate(Guid.NewGuid(), ngo.Id, ngo.Name, 200, "hash");
                await Database.GetCollection<User>("Users").ReplaceOneAsync(x => x.Id == user.Id, user);
                ngo.Donate(donation);
            }
            ngo.DonateChildren();
            foreach (var child in children)
            {
                var childInfo = ngo.Children.Single(x => x.Id == child.Id);
                child.Donate(childInfo.Funds);
            }
            await Database.GetCollection<Ngo>("Ngos").InsertOneAsync(ngo);
            await Database.GetCollection<Child>("Children").InsertManyAsync(children);
        }

        private IEnumerable<Child> CreateChildren()
        {
            var count = 10;
            var names = _namesGenerator.Generate(10);
            for (var i=0; i<count; i++)
            {
                yield return new Child(Guid.NewGuid(), $"{names[i].Item1} {names[i].Item2}", DateTime.UtcNow.AddYears(-10));
            }
        }
    }
}