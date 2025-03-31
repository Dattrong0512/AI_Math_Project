
namespace AIMathProject.Application.Abstracts
{
    public interface IEmailTemplateReader
    {
        Task<string> GetTemplate(string templateName);
    }
}