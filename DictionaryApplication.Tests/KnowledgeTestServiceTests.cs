using Autofac.Extras.Moq;
using DictionaryApplication;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models;
using DictionaryApplication.Repositories;
using DictionaryApplication.Services;
using Moq;
using Xunit.Sdk;

namespace DictionaryApplication.Tests
{
    public class KnowledgeTestServiceTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("Hello", "Hello")]
        [InlineData("Private", "Privat")]
        [InlineData("Love", "Loved")]
        [InlineData("Hello", "HELLO")]
        [InlineData("Hello", "hello")]
        public void IsCorrectAnswer_ShouldReturnTrue(string userAnswer, params string[] correctTranslations)
        {
            // Arrange
            var lexemeTestAttemptRepositoryMock = new Mock<ILexemeTestAttemptRepository>();
            var knowledgeTestService = new KnowledgeTestService(lexemeTestAttemptRepositoryMock.Object);
            bool expected = true;

            // Act
            bool actual = knowledgeTestService.IsCorrectAnswer(userAnswer, correctTranslations);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "Al")]
        [InlineData("Hello", "Hel")]
        [InlineData("Private", "Public")]
        [InlineData("Aim", "")]
        [InlineData("Aim", "Gold")]
        public void IsCorrectAnswer_ShouldReturnFalse(string userAnswer, params string[] correctTranslations)
        {
            // Arrange
            var lexemeTestAttemptRepositoryMock = new Mock<ILexemeTestAttemptRepository>();
            var knowledgeTestService = new KnowledgeTestService(lexemeTestAttemptRepositoryMock.Object);
            bool expected = false;

            // Act
            bool actual = knowledgeTestService.IsCorrectAnswer(userAnswer, correctTranslations);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CheckAnswers_WorksProperlyWhenAnswersAreCorrect()
        {
            // Arrange
            var lexemeTestAttemptRepositoryMock = new Mock<ILexemeTestAttemptRepository>();
            var knowledgeTestService = new KnowledgeTestService(lexemeTestAttemptRepositoryMock.Object);

            // Act
            knowledgeTestService.CheckAnswers(correctTestAttempts, knowledgeTestParameters);
            var expected = true;

            // Assert
            foreach (var lexeme in correctTestAttempts)
            {
                var actual = lexeme.IsCorrectAnswer;
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void CheckAnswers_WorksProperlyWhenAnswersAreWrong()
        {
            // Arrange
            var lexemeTestAttemptRepositoryMock = new Mock<ILexemeTestAttemptRepository>();
            var knowledgeTestService = new KnowledgeTestService(lexemeTestAttemptRepositoryMock.Object);

            // Act
            knowledgeTestService.CheckAnswers(wrongTestAttempts, knowledgeTestParameters);
            var expected = false;

            // Assert
            foreach (var lexeme in wrongTestAttempts)
            {
                var actual = lexeme.IsCorrectAnswer;
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public async Task SetResults_ShouldUpdateLexemes()
        {
            var lexemeTestAttemptRepositoryMock = new Mock<ILexemeTestAttemptRepository>();
            lexemeTestAttemptRepositoryMock.Setup(repo => repo.UpdateTestResultAsync(It.IsAny<LexemeTestAttemptDto>()))
                                           .Returns(Task.CompletedTask);

            var knowledgeTestService = new KnowledgeTestService(lexemeTestAttemptRepositoryMock.Object);

            int expectedTotalTestAttempts = 3;
            int expectedCorrectTestAttemptsIfCorrect = 2;
            int expectedCorrectTestAttemptsIfWrong = 1;

            // Act
            await knowledgeTestService.SetResults(lexemeTestAttempts);

            // Assert
            foreach (var lexeme in lexemeTestAttempts)
            {
                Assert.Equal(expectedTotalTestAttempts, lexeme.Lexeme.TotalTestAttempts); // Ensure TotalTestAttempts is updated correctly
                Assert.Equal(lexeme.IsCorrectAnswer 
                    ? expectedCorrectTestAttemptsIfCorrect 
                    : expectedCorrectTestAttemptsIfWrong,
                    lexeme.Lexeme.CorrectTestAttempts); // Ensure CorrectTestAttempts is updated correctly
                lexemeTestAttemptRepositoryMock.Verify(repo => repo.UpdateTestResultAsync(lexeme), Times.Once); // Ensure UpdateTestResultAsync is called for each LexemeTestAttempt
            }
        }

        // Data
        #region

        private List<LexemeTestAttemptDto> lexemeTestAttempts = new List<LexemeTestAttemptDto>
        {
            new LexemeTestAttemptDto
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Apple", TotalTestAttempts = 2, CorrectTestAttempts = 1 },
                IsCorrectAnswer = true
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Dog",
                Lexeme = new Lexeme{ Word = "Dog", TotalTestAttempts = 2, CorrectTestAttempts = 1  },
                IsCorrectAnswer = true
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Love",
                Lexeme = new Lexeme{ Word = "Break", TotalTestAttempts = 2, CorrectTestAttempts = 1  },
                IsCorrectAnswer = false
            },
        };

        private List<LexemeTestAttemptDto> correctTestAttempts = new List<LexemeTestAttemptDto>
        {
            new LexemeTestAttemptDto
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Apple" },
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Dog",
                Lexeme = new Lexeme{ Word = "Dog" },
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Break",
                Lexeme = new Lexeme{ Word = "Break" },
            },        
        };

        private List<LexemeTestAttemptDto> wrongTestAttempts = new List<LexemeTestAttemptDto>
        {
            new LexemeTestAttemptDto
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Dog" },
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Break",
                Lexeme = new Lexeme{ Word = "Apple" },
            },
            new LexemeTestAttemptDto
            {
                TestAnswer = "Love",
                Lexeme = new Lexeme{ Word = "Easy" },
            },
        };

        private KnowledgeTestParameters knowledgeTestParameters = new KnowledgeTestParameters
        {
            IsUserTranslatesStudiedLanguage = false,
            NumberOfWords = 3,
            SelectedDictionaryIds = new List<int> { 1, 2 },
            TestType = TestType.AllWords
        };

        #endregion
    }
}