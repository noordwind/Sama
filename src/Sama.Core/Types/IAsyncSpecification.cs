using System.Threading.Tasks;

namespace Sama.Core.Types
{
    public interface IAsyncSpecification<in T>
    {
        Task<bool> IsSatisfiedByAsync(T value);
    }
}