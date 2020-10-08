using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Election
    {

        private List<Candidates> candidates {get ; set;}

        public IReadOnlyCollection<Candidates> Candidates => candidates;

        public bool CreateCandidates(List<Candidates> candidate, string password)
        {

            if(password == "Pa$$w0rd")
            {
                candidates = candidate;

                return true;
            }
                return false;
        }

        public Guid GetCandidateIdByCpf(string cpf)
        {
            return candidates.First(x => x.Cpf == cpf).Id;
        }
        
        
        public void Vote(Guid id)
        {
            candidates.First(candidate => candidate.Id == id).Votes++; 
        }

        public List<Candidates> GetWinners()
        {
            var winners = new List<Candidates>{candidates[0]};

            for (int i = 1; i < Candidates.Count; i++)
            {
                if (candidates[i].Votes > winners[0].Votes)
                {
                    winners.Clear();
                    winners.Add(Candidates.ElementAt(i));
                }
                else if (candidates[i].Votes == winners[0].Votes)
                {
                    winners.Add(Candidates.ElementAt(i));
                }
            }
                return winners;
        }

        public List<Candidates> GetCandidatesByName(string name)
        {
            return candidates.Where(item => item.Name == name).ToList();
        }
    }
}
