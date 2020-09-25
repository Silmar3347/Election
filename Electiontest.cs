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
            (string name,string cpf) Jose = ("Jose", "15458525658");
            (string name, string cpf) Ana = ("Ana","14584584847");
            var candidates = new List<(string name,string cpf)>{Jose, Ana};

            var created = Election.CreateCandidates(candidates, "Incorrect");

            Assert.Null(Election.Candidates);
            Assert.False(created);
        }

        [Fact]
        public void should_create_candidates_when_password_is_correct()
        {
        //Given
        var election = new Election();
        (string name,string cpf) Jose = ("Jose", "15458525658");
        (string name, string cpf) Ana = ("Ana","14584584847");
        var candidates = new List <(string name,string cpf)> {Jose, Ana};

        //When
        var created = election.CreateCandidates(candidates, "Pa$$w0rd");

        //Then

        Assert.True(created);

        Assert.True(election.Candidates.Count == 2);
        Assert.True(Jose.name != Ana.name);
        }

        [Fact]
        public void should_return_same_candidates()
        {
        //Given
        var Election = new Election();
        (string name,string cpf) Jose = ("Jose", "15458525658");
        (string name, string cpf) Ana = ("Ana","14584584847");
        var candidates = new List <(string ana,string jose)> {Jose, Ana};
        Election.CreateCandidates(candidates, "Pa$$w0rd");
        
        //When
        var candidateJose = Election.GetCandidateIdByCpf(Jose.cpf);
        var candidateAna = Election.GetCandidateIdByCpf(Ana.cpf);

        Assert.NotEqual(candidateAna, candidateJose);
        }

        [Fact]
        public void should_vote_twice_in_candidate_Jose()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            (string name,string cpf) Jose = ("Jose", "15458525658");
            (string name, string cpf) ana = ("Ana","14584584847");
            var candidates = new List<(string name,string cpf)>{Jose, ana};
            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var joseId = Election.GetCandidateIdByCpf(Jose.cpf);
            var anaId = Election.GetCandidateIdByCpf(ana.cpf);

            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(joseId);
            Election.Vote(joseId);

            // Deve / Asserções
            var candidateJose = Election.Candidates.Find(x => x.id == joseId);
            var candidateAna = Election.Candidates.Find(x => x.id == anaId);
            Assert.True(candidateAna.votes == 0);
            Assert.True(candidateJose.votes == 2);


        }

        [Fact]
        public void should_return_Ana_as_winner_when_only_Ana_receives_votes()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            (string name,string cpf) Jose = ("Jose", "15458525658");
            (string name, string cpf) Ana = ("Ana","14584584847");
            var candidates = new List<(string jose, string ana)>{Jose, Ana};
            var cpfCandidates = new List<string>{Jose.cpf, Ana.cpf};
            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var anaId = Election.GetCandidateIdByCpf(Ana.cpf);
            
            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(anaId);
            Election.Vote(anaId);
            var winners = Election.GetWinners();

            // Deve / Asserções
            Assert.True(winners.Count == 1);
            Assert.Equal(anaId, winners[0].id);
            Assert.Equal(2, winners[0].votes);
        }

        [Fact]
        public void should_return_both_candidates_when_occurs_draw()
        {
            // Dado / Setup
            // OBJETO Election
            var Election = new Election();
            (string name,string cpf) Jose = ("Jose", "15458525658");
            (string name, string cpf) Ana = ("Ana","14584584847");
            var candidates = new List<(string name, string cpf)>{Jose, Ana};
            Election.CreateCandidates(candidates, "Pa$$w0rd");
            var joseId = Election.GetCandidateIdByCpf(Jose.cpf);
            var anaId = Election.GetCandidateIdByCpf(Ana.cpf);
            
            // Quando / Ação
            // Estamos acessando o MÉTODO ShowMenu do OBJETO Election
            Election.Vote(anaId);
            Election.Vote(joseId);
            var winners = Election.GetWinners();

            // Deve / Asserções
            var candidateJose = winners.Find(x => x.cpf == Jose.cpf);
            var candidateAna = winners.Find(x => x.cpf == Ana.cpf);
            Assert.Equal(1, candidateJose.votes);
            Assert.Equal(1, candidateAna.votes);
        }

        [Fact]
        public void TestName()
        {
        //Given
        var election = new Election();
        (string name,string cpf) Jose = ("Jose", "15458525658");
        (string name, string cpf) Ana = ("Ana","14584584847");
        var candidates = new List<(string name, string cpf)>{Jose, Ana};

        //When
        
        //Then

        
        }
    }
}
  

