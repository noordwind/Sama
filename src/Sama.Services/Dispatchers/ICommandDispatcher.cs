using System.Threading.Tasks;

namespace Sama.Services.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}