using System;
using System.Collections.Generic;

namespace Sama.Services.Data
{
    public interface INamesGenerator
    {
        IReadOnlyList<Tuple<string,string>> Generate(int quantity);
    }
}