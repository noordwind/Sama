using Sama.Core.Types;

namespace Sama.Services.Ngos.Queries
{
    public class BrowseNgos : PagedQuery
    {
        public string Type { get; set; }
    }
}