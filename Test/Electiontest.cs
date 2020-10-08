using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Domain;

namespace Test
{   
    public class Electionest
    {
        [Fact]
        public void should_not_create_candidates_when_password_is_incorrect()
        {
            var election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {candidateAna, candidateJose};

            var created = election.CreateCandidates(candidates, "Incorrect");

            Assert.Null(election.Candidates);
            Assert.False(created);
        }

        [Fact]
        public void should_create_candidates_when_password_is_correct()
        {
            //Given
            var Election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {candidateAna, candidateJose};

            //When
            var created = Election.CreateCandidates(candidates, "Pa$$w0rd");

            //Then

            Assert.True(created);

            Assert.Equal(2, Election.Candidates.Count);
            Assert.NotEqual(candidateJose.Name, candidateAna.Name);
        }

        [Fact]
        public void should_return_same_candidates()
        {
        //Given
            var Election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {candidateAna, candidateJose};
            Election.CreateCandidates(candidates, "Pa$$w0rd");
            
            //When
            var candidateJosee = Election.GetCandidateIdByCpf(candidateJose.Cpf);
            var candidateAnaa = Election.GetCandidateIdByCpf(candidateAna.Cpf);

            Assert.NotEqual(candidateAna, candidateJose);
        }

        [Fact]
        public void should_vote_twice_in_candidate_Jose()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

             var candidates = new List<Candidates>()  {candidateAna, candidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");

            var joseId = Election.GetCandidateIdByCpf(candidateAna.Cpf);
            var anaId = Election.GetCandidateIdByCpf(candidateJose.Cpf);

            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(joseId);
            Election.Vote(joseId);

            // Deve / Asserções
            var candidateJosee = Election.Candidates.First(x => x.Id == joseId);
            var candidateAnaa = Election.Candidates.First(x => x.Id == anaId);
            Assert.True(candidateAnaa.Votes == 0);
            Assert.True(candidateJosee.Votes == 2);


        }

        [Fact]
        public void should_return_Ana_as_winner_when_only_Ana_receives_votes()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {candidateAna, candidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var anaId = Election.GetCandidateIdByCpf(candidateAna.Cpf);
            
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
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>()  {candidateAna, candidateJose};

            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var joseId = Election.GetCandidateIdByCpf(candidateJose.Cpf);
            var anaId = Election.GetCandidateIdByCpf(candidateAna.Cpf);
            
            // Quando / Ação
            Election.Vote(anaId);
            Election.Vote(joseId);
            var winners = Election.GetWinners();

            // Deve / Asserções
            var candidateJosee = winners.Find(x => x.Cpf == candidateJose.Cpf);
            var candidateAnaa = winners.Find(x => x.Cpf == candidateAna.Cpf);
            Assert.Equal(1, candidateJose.Votes);
            Assert.Equal(1, candidateAna.Votes);
        }

        [Fact]
        public void Should_Return_Repeated_Names()
        {
            //Given
            var Election = new Election();
            var candidateAna = new Candidates("Ana","15458525658");
            var candidateJose = new Candidates("Jose","14584584847");

            var candidates = new List<Candidates>() {candidateAna, candidateJose};

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
  

