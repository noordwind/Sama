using System.Threading.Tasks;

namespace Sama.Services
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}