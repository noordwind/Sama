using System.Threading.Tasks;

namespace Sama.Infrastructure.Mongo
{
    public interface IMongoDbInitializer
    {
        Task InitializeAsync();
    }
}