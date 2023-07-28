using Autofac.Extras.Moq;
using DictionaryApplication;
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
            knowledgeTestService.CheckAnswers(ref correctTestAttempts, knowledgeTestParameters);
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
            knowledgeTestService.CheckAnswers(ref wrongTestAttempts, knowledgeTestParameters);
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
            lexemeTestAttemptRepositoryMock.Setup(repo => repo.UpdateTestResultAsync(It.IsAny<LexemeTestAttempt>()))
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

        private List<LexemeTestAttempt> lexemeTestAttempts = new List<LexemeTestAttempt>
        {
            new LexemeTestAttempt
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Apple", TotalTestAttempts = 2, CorrectTestAttempts = 1 },
                Translations = new List<Lexeme> { new Lexeme { Word = "Яблоко" } },
                IsCorrectAnswer = true
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Dog",
                Lexeme = new Lexeme{ Word = "Dog", TotalTestAttempts = 2, CorrectTestAttempts = 1  },
                Translations = new List<Lexeme> { new Lexeme { Word = "Собака" } },
                IsCorrectAnswer = true
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Love",
                Lexeme = new Lexeme{ Word = "Break", TotalTestAttempts = 2, CorrectTestAttempts = 1  },
                Translations = new List<Lexeme> { new Lexeme { Word = "Раскол" } },
                IsCorrectAnswer = false
            },
        };

        private List<LexemeTestAttempt> correctTestAttempts = new List<LexemeTestAttempt>
        {
            new LexemeTestAttempt
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Apple" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Яблоко" } }
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Dog",
                Lexeme = new Lexeme{ Word = "Dog" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Собака" } }
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Break",
                Lexeme = new Lexeme{ Word = "Break" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Раскол" }, new Lexeme { Word = "Перерыв"} }
            },        
        };

        private List<LexemeTestAttempt> wrongTestAttempts = new List<LexemeTestAttempt>
        {
            new LexemeTestAttempt
            {
                TestAnswer = "Apple",
                Lexeme = new Lexeme{ Word = "Dog" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Собака" } }
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Break",
                Lexeme = new Lexeme{ Word = "Apple" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Яблоко" } }
            },
            new LexemeTestAttempt
            {
                TestAnswer = "Love",
                Lexeme = new Lexeme{ Word = "Easy" },
                Translations = new List<Lexeme> { new Lexeme { Word = "Легко" } }
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