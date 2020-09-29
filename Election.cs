using System;
using System.Collections.Generic;
using System.Linq;

namespace Election
{
    class Election
    {

        public List <Candidates> Candidates {get ; set;}
        public bool CreateCandidates(List<Candidates> candidates, string password)
        {

            if(password == "Pa$$w0rd")
            {
                Candidates = candidates;

                return true;
            }
                return false;
        }

        public Guid GetCandidateIdByCpf(string cpf)
        {
            return Candidates.First(x => x.Cpf == cpf).Id;
        }
        
        
        public void Vote(Guid id)
        {
            Candidates.First(candidates => candidates.Id == id).Votes++; 
        }

        public List<Candidates> GetWinners()
        {
            var winners = new List<Candidates>{Candidates[0]};

            for (int i = 1; i < Candidates.Count; i++)
            {
                if (Candidates[i].Votes > winners[0].Votes)
                {
                    winners.Clear();
                    winners.Add(Candidates[i]);
                }
                else if (Candidates[i].Votes == winners[0].Votes)
                {
                    winners.Add(Candidates[i]);
                }
            }
                return winners;
        }

        public List<Candidates> GetCandidatesByName(string name)
        {
            return Candidates.Where(item => item.Name == name).ToList();
        }
    }
}
