using DictionaryApplication.Data;
using DictionaryApplication.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DictionaryApplication.DTOs
{
    public class LexemeDetailsDto
    {
        public Lexeme Lexeme { get; set; }
        public double TestResults { get; set; }
        public string TestResultsRepresentation { get; set; }

        public LexemeDetailsDto(Lexeme lexeme)
        {
            Lexeme = lexeme;

            TestResults = Lexeme.TotalTestAttempts > 0
                ? (double)Lexeme.CorrectTestAttempts / Lexeme.TotalTestAttempts
                : 0;

            TestResultsRepresentation = Lexeme.TotalTestAttempts > 0
                ? $"{string.Format("{0,6:##0.00; }", TestResults * 100)} %\n({Lexeme.CorrectTestAttempts} out of {Lexeme.TotalTestAttempts})"
                : "0 attempts";
        }
    }
}
