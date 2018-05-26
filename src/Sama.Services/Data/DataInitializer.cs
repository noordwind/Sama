using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Sama.Core.Domain.Identity;
using Sama.Core.Domain.Identity.Services;
using Sama.Core.Domain.Ngos;
using Sama.Core.Domain.Shared;
using Sama.Infrastructure.Mongo;

namespace Sama.Services.Data
{
    public class DataInitializer : MongoDbSeeder
    {
        private readonly Random _random = new Random();
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
            var usernames = _namesGenerator.Generate(11);
            for (var i = 0; i < 10; i++)
            {
                users.Add(await CreateUserAsync(i, $"{usernames[i].Item1}-{usernames[i].Item2}"));
                ngoUsers.Add(await CreateNgoUserAsync(i, $"{usernames[i].Item2}-{usernames[i].Item1}"));
            }

            //Goa 
            await CreateNgoAsync(ngoUsers[0].Id, "Goa Outreach",
                "The Brown House H.No. 4/88, Acoi, Mapusa, Goa 403507, Indie", 15.605579, 73.830168, users);
            await CreateNgoAsync(ngoUsers[1].Id, "Mitsuko Trust",
                "Rebelo Bldg 1st Floor, OPP.Cafe Bhonsale, Panaji, Goa 403001, Indie", 15.487704, 73.824241, users);
            await CreateNgoAsync(ngoUsers[2].Id, "Live Happy - Happy Home",
                "Assagao, Goa 403507, Indie", 15.601474, 73.785346, users);
            await CreateNgoAsync(ngoUsers[3].Id, "Bethesda Life Centre",
                "1503/3, Rego Bhag Bambolim Complex Alto Santa Cruz " +
                "Tiswadi, Goa 403202, Indie", 15.477480, 73.841425, users);
            await CreateNgoAsync(ngoUsers[4].Id, "Care for India",
                "Tiswadi, Santa Cruz, Goa 403005, Indie", 15.482289, 73.842879, users);

            //Krakow
            await CreateNgoAsync(ngoUsers[5].Id, "Little kids", "Address", 50.049683, 19.854544, users);
            await CreateNgoAsync(ngoUsers[6].Id, "Small orphanage", "Address", 50.079683, 19.984544, users);
            await CreateNgoAsync(ngoUsers[7].Id, "Home for children", "Address", 50.019683, 19.544544, users);
            await CreateNgoAsync(ngoUsers[8].Id, "Kids place", "Address", 50.029683, 19.734544, users);
            await CreateNgoAsync(ngoUsers[9].Id, "Children's hope", "Address", 50.059683, 19.944544, users);
        }

        private async Task CreateAdminAsync()
        {
            var user = new User(Guid.NewGuid(), $"admin@sama.network", "admin", "admin");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
        }

        private async Task<User> CreateUserAsync(int number, string username)
        {
            var user = new User(Guid.NewGuid(), $"user{number + 1}@sama.network", username, "user");
            _passwordHasher.SetPasswordHash(user, "secret");
            var payment = new Payment(Guid.NewGuid(), user.Id, 30000, "secure-hash");
            user.AddFunds(payment);
            await Database.GetCollection<User>("Users").InsertOneAsync(user);
            await Database.GetCollection<Payment>("Payments").InsertOneAsync(payment);

            return user;
        }

        private async Task<User> CreateNgoUserAsync(int number, string username)
        {
            var user = new User(Guid.NewGuid(), $"ngo{number + 1}@sama.network", username, "ngo");
            _passwordHasher.SetPasswordHash(user, "secret");
            await Database.GetCollection<User>("Users").InsertOneAsync(user);

            return user;
        }

        private async Task CreateNgoAsync(Guid ownerId, string name, string address,
            double latitude, double longitude, IEnumerable<User> users)
        {
            var location = new Location(address, latitude, longitude);
            var ngo = new Ngo(Guid.NewGuid(), ownerId, name, location,
                $"{name} description", fundsPerChild: 1500, approved: true);
            var children = CreateChildren(1500).ToList();
            foreach (var child in children)
            {
                ngo.AddChild(child);
            }

            foreach (var user in users)
            {
                var donation = user.DonateNgo(Guid.NewGuid(), ngo, 600, "hash");
                await Database.GetCollection<User>("Users").ReplaceOneAsync(x => x.Id == user.Id, user);
                ngo.DonateChildren(donation);
            }
            foreach (var child in children)
            {
                var childInfo = ngo.Children.Single(x => x.Id == child.Id);
                child.Donate(childInfo.GatheredFunds);
            }

            await Database.GetCollection<Ngo>("Ngos").InsertOneAsync(ngo);
            await Database.GetCollection<Child>("Children").InsertManyAsync(children);
        }

        private IEnumerable<Child> CreateChildren(decimal neededFunds)
        {
            var count = 10;
            var names = _namesGenerator.Generate(count);
            for (var i = 0; i < count; i++)
            {
                var gender = _random.Next(0, 2) == 1 ? "male" : "female";
                yield return new Child(Guid.NewGuid(), $"{names[i].Item1} {names[i].Item2}",
                    gender, DateTime.UtcNow.AddYears(-10), "notes", neededFunds);
            }
        }
    }
}