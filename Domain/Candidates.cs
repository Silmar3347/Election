using System;
using System.Linq;

namespace Domain
{
    public class Candidates
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public int Votes { get; set; }

        public Candidates(string name, string cpf)
        {
            Id = Guid.NewGuid();
            Name = name;
            Cpf = cpf;
            Votes = 0;

        }
    }
}