using System.Threading.Tasks;

namespace Sama.Infrastructure.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}