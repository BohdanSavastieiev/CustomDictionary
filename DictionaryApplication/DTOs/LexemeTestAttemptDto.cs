namespace DictionaryApplication.DTOs
{
    public class LexemeTestAttemptDto
    {
        public LexemeDto Lexeme { get; set; } = null!;
        public string LexemeTestRepresentation { get; set; } = null!;
        public string CorrectAnswerRepresentation { get; set; } = null!;
        public string? TestAnswer { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
