using DictionaryApplication.Models;

namespace DictionaryApplication.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto request);
    }
}
