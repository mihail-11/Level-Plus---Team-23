using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Xml.Linq;
using IronPdf;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Level_plus___Team_23.Services
{
    public class PdfService
    {

        public MimePart createPdfCertificateAttachment()
        {

            // Generate the PDF document
            var renderer = new HtmlToPdf();
            var html = "<html><body><h1>Congratulations!</h1></body></html>";
            var pdfBytes = renderer.RenderHtmlAsPdf(html).BinaryData;

            // Create the PDF attachment
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(pdfBytes)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "Certificate.pdf"
            };

            return attachment;
        }
    }
}
