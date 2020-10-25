using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "SG.Oq4AKBEMTAmjZBwhS8yRhA.vNJ_PiXvke3mkmVowTgiNwedC4mh5iIAF1rCjbGP";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase postedFileBase)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("ywuu064@student.monash.edu");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (postedFileBase != null && postedFileBase.ContentLength > 0)
            {
                string theFileName = Path.GetFileName(postedFileBase.FileName);
                byte[] fileBytes = new byte[postedFileBase.ContentLength];
                using (BinaryReader theReader = new BinaryReader(postedFileBase.InputStream))
                {
                    fileBytes = theReader.ReadBytes(postedFileBase.ContentLength);
                }
                string dataAsString = Convert.ToBase64String(fileBytes);
                msg.AddAttachment(theFileName, dataAsString);
            }

            var response = client.SendEmailAsync(msg);
        }

    }
}
