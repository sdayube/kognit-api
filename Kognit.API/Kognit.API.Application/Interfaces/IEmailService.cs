using Kognit.API.Application.DTOs.Email;
using System.Threading.Tasks;

namespace Kognit.API.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}