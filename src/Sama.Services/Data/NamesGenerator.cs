using System;
using System.Collections.Generic;
using System.Linq;

namespace Sama.Services.Data
{
    public class NamesGenerator : INamesGenerator
    {
        private static readonly List<string> FirstNames = new List<string>
        {
            "Sergio",
            "Daniel",
            "Carolina",
            "David",
            "Reina",
            "Saul",
            "Bernard",
            "Danny",
            "Dimas",
            "Yuri",
            "Ivan",
            "Laura",
            "Tom",
            "Martin",
            "Peter",
            "Samuel",
            "Jamie" +
            "Adam",
            "Paul",
            "Rose"
        };

        private static readonly List<string> LastNames = new List<string>
        {
            "Tapia",
            "Gutierrez",
            "Rueda",
            "Galviz",
            "Yuli",
            "Rivera",
            "Mamami",
            "Saucedo",
            "Dominguez",
            "Escobar",
            "Martin",
            "Crespo",
            "Johnson",
            "Williams",
            "Jones",
            "Brown",
            "David",
            "Miller",
            "Wilson",
            "Anderson",
            "Thomas",
            "Jackson",
            "White",
            "Robinson",
            "Jackson",
            "Anders",
            "Fox"
        };

        public IReadOnlyList<Tuple<string,string>> Generate(int quantity)
        {
            var permutations = new List<Tuple<int, int>>();
            var random = new Random();
            while (permutations.Count < quantity)
            {
                var firstName = random.Next(0, FirstNames.Count);
                var lastName = random.Next(0, LastNames.Count);

                var tuple = new Tuple<int, int>(firstName, lastName);

                if (!permutations.Contains(tuple))
                {
                    permutations.Add(tuple);
                }
            }

            return permutations
                    .Select(tuple => new Tuple<string, string>(FirstNames[tuple.Item1], LastNames[tuple.Item2]))
                    .ToList();
        }        
    }
}