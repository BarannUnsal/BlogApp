using FealtedByEsra.Entity.Entities;

namespace FealtedByEsra.BAL.Abstract.Services.MailService
{
    public interface IEmailService
    {
        bool SendEmail(string name, string title, string email, string description);
        bool FirstEmail(string newsletter, string title, int id);
        bool NewPostEmail(List<Newsletter> newsletters, string title, string img, string desc, int id, int postId);
    }
}
