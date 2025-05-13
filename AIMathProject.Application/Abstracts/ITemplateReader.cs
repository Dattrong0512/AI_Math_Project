
namespace AIMathProject.Application.Abstracts
{
    public interface ITemplateReader
    {
        Task<string> GetTemplate(string templateName);
    }
}