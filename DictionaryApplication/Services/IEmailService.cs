using DictionaryApplication.DTOs;

namespace DictionaryApplication.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto request);
    }
}
