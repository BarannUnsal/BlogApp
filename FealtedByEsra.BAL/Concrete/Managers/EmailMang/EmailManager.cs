using FealtedByEsra.BAL.Abstract.Services.MailService;
using FealtedByEsra.BAL.Helpers.UrlHelpers;
using FealtedByEsra.Entity.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;

namespace FealtedByEsra.BAL.Concrete.Managers.EmailMang
{
    public class EmailManager : IEmailService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmailManager(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public bool FirstEmail(string newsletter, string title, int id)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            MailMessage mail = new();
            mail.From = new MailAddress("esra@esrarga.com");
            mail.To.Add(newsletter);
            mail.Subject = title;

            mail.Body = $"<!DOCTYPE html>\r\n<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:v=\"urn:schemas-microsoft-com:vml\" lang=\"tr\">\r\n\r\n<head>\r\n    <link rel=\"stylesheet\" type=\"text/css\" hs-webfonts=\"true\" href=\"https://fonts.googleapis.com/css?family=Lato|Lato:i,b,bi\">\r\n    <title>Email Subscribe</title>\r\n    <meta property=\"og:title\" content=\"Email Subscribe\">\r\n\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n\r\n    <style type=\"text/css\">\r\n\r\n        a {{\r\n            text-decoration: underline;\r\n            color: inherit;\r\n            font-weight: bold;\r\n            color: #253342;\r\n        }}\r\n\r\n        h1 {{\r\n            font-size: 56px;\r\n        }}\r\n\r\n        h2 {{\r\n            font-size: 28px;\r\n            font-weight: 900;\r\n        }}\r\n\r\n        p {{\r\n            font-weight: 100;\r\n        }}\r\n\r\n        td {{\r\n            vertical-align: top;\r\n        }}\r\n\r\n        #email {{\r\n            margin: auto;\r\n            width: 600px;\r\n            background-color: white;\r\n        }}\r\n\r\n        button {{\r\n            font: inherit;\r\n            background-color: #FF7A59;\r\n            border: none;\r\n            padding: 10px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 2px;\r\n            font-weight: 900;\r\n            color: white;\r\n            border-radius: 5px;\r\n            box-shadow: 3px 3px #d94c53;\r\n        }}\r\n\r\n        #abtn {{\r\n            font: inherit;\r\n            background-color: #FF7A59;\r\n            border: none;\r\n            padding: 10px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 2px;\r\n            font-weight: 900;\r\n            color: white;\r\n            border-radius: 5px;\r\n            box-shadow: 3px 3px #d94c53;\r\n            text-decoration: none;\r\n            color: #FFFFFF;\r\n        }}\r\n\r\n        .subtle-link {{\r\n            font-size: 9px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 1px;\r\n            color: #CBD6E2;\r\n        }}\r\n    </style>\r\n\r\n</head> <div id=\"email\">\r\n        <table role=\"presentation\" width=\"100%\">\r\n            <tr>\r\n\r\n                <td bgcolor=\"#00A4BD\" align=\"center\" style=\"color: white;\">\r\n\r\n                    <img alt=\"Flower\" src=\"https://hs-8886753.f.hubspotemail.net/hs/hsstatic/TemplateAssets/static-1.60/img/hs_default_template_images/email_dnd_template_images/ThankYou-Flower.png\" width=\"400px\" align=\"middle\">\r\n\r\n                    <h1> Hoş Geldin! </h1>\r\n\r\n                </td>\r\n        </table>\r\n\r\n        <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"10px\" style=\"padding: 30px 30px 30px 60px;\">\r\n            <tr>\r\n                <td>\r\n                    <h2> Sizi neler bekliyor</h2>\r\n                    <p>\r\n                        Bültene abone olduğunuza göre okumayı seviyorsunuz. Yazdığım yazıları ve yeni içeriklerimi okumak için \r\n                    </p>\r\n                    <a href=\"www.esrarga.com\" id=\"#abtn\">\r\n                        Keşfet\r\n                    </a>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n\r\n        <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"10px\" width=\"100%\" style=\"padding: 30px 30px 30px 60px;\">\r\n            <tr>\r\n                <td>\r\n                    <img alt=\"Blog\" src=\"https://www.hubspot.com/hubfs/assets/hubspot.com/style-guide/brand-guidelines/guidelines_sample-illustration-3.svg\" width=\"200px\" align=\"middle\">\r\n\r\n                    <h2> Vivamus ac elit eget </h2>\r\n                    <p>\r\n                        Vivamus ac elit eget dolor placerat tristique et vulputate nibh. Sed in elementum nisl, quis mollis enim. Etiam gravida dui vel est euismod, at aliquam ipsum euismod.\r\n\r\n                    </p>\r\n\r\n                </td>\r\n\r\n                <td>\r\n\r\n                    <img alt=\"Shopping\" src=\"https://www.hubspot.com/hubfs/assets/hubspot.com/style-guide/brand-guidelines/guidelines_sample-illustration-5.svg\" width=\"200px\" align=\"middle\">\r\n                    <h2> Suspendisse tincidunt iaculis </h2>\r\n                    <p>\r\n                        Suspendisse tincidunt iaculis fringilla. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras laoreet elit purus, quis pulvinar ipsum pulvinar et.\r\n\r\n                    </p>\r\n                </td>\r\n            </tr>\r\n\r\n            <tr>\r\n                <td> <a #abtn> Button 2 </a> </td>\r\n                <td> <a #abtn> Button 3 </a> </td>\r\n\r\n        </table>\r\n\r\n        <table role=\"presentation\" bgcolor=\"#EAF0F6\" width=\"100%\" style=\"margin-top: 50px;\">\r\n            <tr>\r\n                <td align=\"center\" style=\"padding: 30px 30px;\">\r\n\r\n                    <h2> Nullam porta arcu </h2>\r\n                    <p>\r\n                        Nam vel lobortis lorem. Nunc facilisis mauris at elit pulvinar, malesuada condimentum erat vestibulum. Pellentesque eros tellus, finibus eget erat at, tempus rutrum justo.\r\n\r\n                    </p>\r\n                    <a href=\"www.esrarga.com/about\"> Soru sor</a>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n\r\n        <table role=\"presentation\" bgcolor=\"#F5F8FA\" width=\"100%\">\r\n            <tr>\r\n                <td align=\"left\" style=\"padding: 30px 30px;\">\r\n                    <a class=\"subtle-link\" href=\"https://esrarga.com/Newsletter/RemoveNewsletter/{id}\"> Abonelikten çık </a>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div></body>\r\n</html>";

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("esra@esrarga.com", "?94a14ruL");
            smtp.Port = 587;
            smtp.Host = "mail.esrarga.com";

            smtp.Send(mail);
            return true;
        }

        public bool NewPostEmail(List<Newsletter> newsletters, string title, string img, string desc, int id, int postId)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            MailMessage mail = new();
            mail.From = new MailAddress("esra@esrarga.com");
            foreach (var newsletter in newsletters)
            {
                mail.To.Add(newsletter.Email);
            }
            mail.Subject = title;
            mail.Body = $"<!DOCTYPE html>\r\n<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:v=\"urn:schemas-microsoft-com:vml\" lang=\"tr\">\r\n\r\n<head>\r\n    <link rel=\"stylesheet\" type=\"text/css\" hs-webfonts=\"true\" href=\"https://fonts.googleapis.com/css?family=Lato|Lato:i,b,bi\">\r\n    <title>{title}</title>\r\n    <meta property=\"og:title\" content=\"{title}\">\r\n\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n\r\n \r\n    <style type=\"text/css\">\r\n\r\n        a {{\r\n            text-decoration: underline;\r\n            color: inherit;\r\n            font-weight: bold;\r\n            color: #253342;\r\n        }}\r\n\r\n        h1 {{\r\n            font-size: 56px;\r\n        }}\r\n\r\n        h2 {{\r\n            font-size: 18px;\r\n            font-weight: 900;\r\n        }}\r\n\r\n        p {{\r\n            font-weight: 100;\r\n        }}\r\n\r\n        td {{\r\n            vertical-align: top;\r\n        }}\r\n\r\n        #email {{\r\n            margin: auto;\r\n            width: 600px;\r\n            background-color: white;\r\n        }}\r\n\r\n        button {{\r\n            font: inherit;\r\n            background-color: #FF7A59;\r\n            border: none;\r\n            padding: 10px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 2px;\r\n            font-weight: 900;\r\n            color: white;\r\n            border-radius: 5px;\r\n            box-shadow: 3px 3px #d94c53;\r\n        }}\r\n\r\n        #abtn {{\r\n            font: inherit;\r\n            background-color: #FF7A59;\r\n            border: none;\r\n            padding: 10px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 2px;\r\n            font-weight: 900;\r\n            color: white;\r\n            border-radius: 5px;\r\n            box-shadow: 3px 3px #d94c53;\r\n            text-decoration: none;\r\n            color: #FFFFFF;\r\n        }}\r\n\r\n        .subtle-link {{\r\n            font-size: 9px;\r\n            text-transform: uppercase;\r\n            letter-spacing: 1px;\r\n            color: #CBD6E2;\r\n        }}\r\n    </style>\r\n   \r\n\r\n</head> " +
                $"<body bgcolor=\"#F5F8FA\" style=\"width: 100%; margin: auto 0; padding:0; font-family:Lato, sans-serif; font-size:18px; color:#33475B; word-break:break-word\">\r\n\r\n    <div id=\"email\">\r\n        <table role=\"presentation\" width=\"100%\">\r\n            <tr>\r\n\r\n                <td bgcolor=\"#00A4BD\" align=\"center\" style=\"color: white;\">\r\n\r\n                    <img alt=\"{title}\" src=\"https://esrarga.com/PostThumbnail/{img}\" width=\"400px\" align=\"middle\">\r\n\r\n                    <h1> {title} </h1>\r\n\r\n                </td>\r\n        </table>\r\n\r\n        <table role=\"presentation\" border=\"0\" cellpadding=\"0\" cellspacing=\"10px\" style=\"padding: 30px 30px 30px 60px;\">\r\n            <tr>\r\n                <td>\r\n                    <h2> Sizi neler bekliyor</h2>\r\n                    <p>\r\n                        {desc} \r\n                    </p>\r\n                    <a href=\"www.esrarga.com/post/details/{postId}/{FriendlyUrllHelper.GenerateTitle(title)}\" id=\"#abtn\">\r\n                        Devamını Oku\r\n                    </a>\r\n                </td>\r\n            </tr>\r\n        </table>\r\n        <table role=\"presentation\" bgcolor=\"#F5F8FA\" width=\"100%\">\r\n            <tr>\r\n                <td align=\"center\" bgcolor=\"#e9ecef\" style=\"padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;\">\r\n                            <p style=\"margin: 0;\">Bu e-postaları almayı durdurmak için istediğiniz zaman abonelikten <a href=\"https://esrarga.com/Newsletter/RemoveNewsletter/{id}\" target=\"_blank\">çıkabilirsiniz.</a></p>\r\n                        </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n</body>\r\n</html>";

            mail.IsBodyHtml = true;            
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("esra@esrarga.com", "?94a14ruL");
            smtp.Port = 587;
            smtp.Host = "mail.esrarga.com";

            smtp.Send(mail);
            return true;
        }

        public bool SendEmail(string name, string title, string email, string description)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            MailMessage mail = new();
            mail.From = new MailAddress("esra@esrarga.com");
            mail.To.Add("esra.arqa@gmail.com");
            mail.Subject = "Esrarga.com Yeni Bir Mesaj " + title;
            mail.Body = "<!DOCTYPE html>" + "<html>" + "<body stlye=\"background-color: #ff7f26; text-align:center\">" + "<h3>" + name + "</h3><br><div><p>" + title + "</p><br><div>" + "<p>" + description + "</p>" + "</div>" + "<br>" + "<div>" + "<p><strong>" + email + "</strong></p>" + "</div>" + "</body>" + "</html>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("esra@esrarga.com", "?94a14ruL");
            smtp.Port = 587;
            smtp.Host = "mail.esrarga.com";

            smtp.Send(mail);
            return true;
        }
    }
}
