using Xunit;
using Domain;

namespace Tests
{
    public class CandidateTest
    {
        [Fact]
        public void Should_contains_same_parameters_provided()
        {
            var name = "João da Silva";
            var CPF = "895.658.478-89";
            
            var candidate = new Candidates(name, CPF);

            Assert.Equal(name, candidate.Name);
            Assert.Equal(CPF, candidate.Cpf);
        }

        [Fact]
        public void Should_contains_votes_equals_zero()
        {
            var name = "João da Silva";
            var CPF = "895.658.478-89";

            var candidate = new Candidates(name, CPF);

            Assert.Equal(0, candidate.Votes);
        }

        [Fact]
        public void Should_contain_votes_equals_2_when_voted_twice()
        {
            var name = "João da Silva";
            var CPF = "895.658.478-89";
            var candidate = new Candidates(name, CPF);

            candidate.Vote();
            candidate.Vote();

            Assert.Equal(2, candidate.Votes);
        }

        [Fact]
        public void Should_not_generate_same_id_for_both_candidates()
        {
            var Jose = new Candidates("José", "895.456.214-78");
            var Ana = new Candidates("Ana", "456.456.214-78");
            
            Assert.NotEqual(Jose.Id, Ana.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("000.000.000-00")]
        [InlineData("000.000.000-01")]
        [InlineData("100.000.000-00")]
        [InlineData("555.444.333-22")]
        [InlineData("123.456.789-10")]
        [InlineData("000,368,560-00")]
        [InlineData("109.876.543-21")]
        [InlineData("000.368.560-00")]
        [InlineData("02.368-560.00")]
        [InlineData("999.999.999-99")]
        [InlineData("542.365.568-06")]
        [InlineData("640.3685606")]
        [InlineData("640.368.560-6")]
        [InlineData("640.368.560-6a")]
        [InlineData("640.368.560-061")]
        public void Should_return_false_when_CPF_is_not_valid(string CPF)
        {
            // Dado / Setup
            var Jose = new Candidates("José", CPF);

            // When / Ação
            var isValid = Jose.Validate();
            
            // Deve / Asserções
            Assert.False(isValid);
        }

        [Theory]
        [InlineData("64036856006")]
        [InlineData("640.368.560-06")]
        public void Should_return_true_when_CPF_is_valid(string CPF)
        {
            // Dado / Setup
            var Jose = new Candidates("José", CPF);

            // When / Ação
            var isValid = Jose.Validate();
            
            // Deve / Asserções
            Assert.True(isValid);
        }



        [Theory]
        [InlineData("joao marcelo-")]
        [InlineData("Carlos.")]
        public void Should_return_false_when_name_is_incorrect(string Name)
        {
            var Jose = new Candidates(Name, "640.368.560-06");

            var isValid = Jose.Validate();

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("Joao Carlos")]
        [InlineData("Seu Zé")]
        public void Should_return_true_when_NAME_is_valid(string Name)
        {
            // Dado / Setup
            var Jose = new Candidates(Name, "640.368.560-06");

            // When / Ação
            var isValid = Jose.Validate();
            
            // Deve / Asserções
            Assert.True(isValid);
        }

        
    }
}