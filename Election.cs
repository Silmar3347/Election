using System;
using System.Collections.Generic;
using System.Linq;

namespace Election
{
    class Election
    {

        public List <(Guid id, string name, string cpf, int votes)> Candidates {get ; set;}
        public bool CreateCandidates(List<(string name,string cpf)> candidates, string password)
        {

            if(password == "Pa$$w0rd")
            {
                Candidates = candidates.Select(item => {
                    return (Guid.NewGuid(), item.name,item.cpf, 0);
                }).ToList();

                return true;
            }
                return false;
        }

        public Guid GetCandidateIdByCpf(string cpf)
        {
            return Candidates.First(x => x.cpf == cpf).id;
        }
        
        
        public void Vote(Guid id)
        {
            Candidates = Candidates.Select(item => {
                return item.id == id ? (item.id, item.name,item.cpf, item.votes + 1) : item;
            }).ToList();
        }

        public List<(Guid id, string name,string cpf, int votes)> GetWinners()
        {
            var winners = new List<(Guid id, string name,string cpf, int votes)>{Candidates[0]};

            for (int i = 1; i < Candidates.Count; i++)
            {
                if (Candidates[i].votes > winners[0].votes)
                {
                    winners.Clear();
                    winners.Add(Candidates[i]);
                }
                else if (Candidates[i].votes == winners[0].votes)
                {
                    winners.Add(Candidates[i]);
                }
            }
            return winners;
        }

        public List<(Guid id, string name, string cpf, int votes)> ReturnSameCandidates(string name)
        {
            return Candidates.Where(item => item.name == name).ToList();
        }
    }
}
