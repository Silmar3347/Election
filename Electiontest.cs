using System;
using System.Collections.Generic;
using Xunit;

namespace Election
{   
    public class Electionest
    {
        [Fact]
        public void should_not_create_candidates_when_password_is_incorrect()
        {
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};

            var created = Election.CreateCandidates(candidates, "Incorrect");

            Assert.Null(Election.Candidates);
            Assert.False(created);
        }

        [Fact]
        public void should_create_candidates_when_password_is_correct()
        {
            //Given
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};

            //When
            var created = Election.CreateCandidates(candidates, "Pa$$w0rd");

            //Then

            Assert.True(created);

            Assert.True(Election.Candidates.Count == 2);
            Assert.True(CandidateJose.Name != CandidateAna.Name);
        }

        [Fact]
        public void should_return_same_candidates()
        {
        //Given
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};
            Election.CreateCandidates(candidates, "Pa$$w0rd");
            
            //When
            var candidateJose = Election.GetCandidateIdByCpf(CandidateJose.Cpf);
            var candidateAna = Election.GetCandidateIdByCpf(CandidateAna.Cpf);

            Assert.NotEqual(candidateAna, candidateJose);
        }

        [Fact]
        public void should_vote_twice_in_candidate_Jose()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

             var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");

            var joseId = Election.GetCandidateIdByCpf(CandidateAna.Cpf);
            var anaId = Election.GetCandidateIdByCpf(CandidateJose.Cpf);

            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(joseId);
            Election.Vote(joseId);

            // Deve / Asserções
            var candidateJose = Election.Candidates.Find(x => x.Id == joseId);
            var candidateAna = Election.Candidates.Find(x => x.Id == anaId);
            Assert.True(candidateAna.Votes == 0);
            Assert.True(candidateJose.Votes == 2);


        }

        [Fact]
        public void should_return_Ana_as_winner_when_only_Ana_receives_votes()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var anaId = Election.GetCandidateIdByCpf(CandidateAna.Cpf);
            
            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(anaId);
            Election.Vote(anaId);
            var winners = Election.GetWinners();

            // Deve / Asserções
            Assert.True(winners.Count == 1);
            Assert.Equal(anaId, winners[0].Id);
            Assert.Equal(2, winners[0].Votes);
        }

        [Fact]
        public void should_return_both_candidates_when_occurs_draw()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {CandidateAna, CandidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var joseId = Election.GetCandidateIdByCpf(CandidateJose.Cpf);
            var anaId = Election.GetCandidateIdByCpf(CandidateAna.Cpf);
            
            // Quando / Ação
            Election.Vote(anaId);
            Election.Vote(joseId);
            var winners = Election.GetWinners();

            // Deve / Asserções
            var candidateJose = winners.Find(x => x.Cpf == CandidateJose.Cpf);
            var candidateAna = winners.Find(x => x.Cpf == CandidateAna.Cpf);
            Assert.Equal(1, candidateJose.Votes);
            Assert.Equal(1, candidateAna.Votes);
        }

        [Fact]
        public void Should_Return_Repeated_Names()
        {
            //Given
            var Election = new Election();
            var CandidateAna = new Candidates("Ana","15458525658");
            var CandidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>() {CandidateAna, CandidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");

            //When
            var input = "Jose";
            var result = Election.GetCandidatesByName(input);

            //Then

            Assert.Equal("Jose", result[0].Name);
            Assert.True(result.Count == 1);
        }
    }
}
  

