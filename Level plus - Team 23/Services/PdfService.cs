using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Xml.Linq;
using IronPdf;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Level_plus___Team_23.Models;
using System;

namespace Level_plus___Team_23.Services
{
    public class PdfService
    {

        public MimePart createPdfCertificateAttachment(Course course)
        {

            var renderer = new HtmlToPdf();
            var html = createHtmlContent(course);
            var pdfBytes = renderer.RenderHtmlAsPdf(html).BinaryData;

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(new MemoryStream(pdfBytes)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = "Certificate.pdf"
            };

            return attachment;
        }

        private string createHtmlContent(Course course)
        {

            var pngBinaryData = File.ReadAllBytes("wwwroot/img/logo.png");
            var ImgDataURI = @"data:image/png;base64," + Convert.ToBase64String(pngBinaryData);
            var ImgHtml = String.Format("<img src='{0}' width='500' height='300'>", ImgDataURI);

            string style = @"
                    <style>
                        /* Add your certificate styling here */
                        body {
                            font-family: Arial, sans-serif;
                            text-align: center;
                        }
                        h1 {
                            color: #333;
                            margin-bottom: 20px;
                        }
                        p {
                            font-size: 18px;
                            margin-bottom: 10px;
                        }
                    </style>";

            return $@"
            <html>
                <head>
                    {style}
                </head>
                <body>
                    <h1>Certificate of Completion</h1>
                    <p>This is to certify that</p>
                    <p><strong>{course.Student.Name}</strong></p>
                    <p>has successfully completed the online programming course titled</p>
                    <p><strong>{course.Title}</strong></p>
                    <p>offered by</p>
                    {ImgHtml}
                    <p>Congratulations!</p>
                    <p>By completing this course, {course.Student.Name} has gained a solid understanding in the programming fundamentals of <strong>{course.Title}</strong></p>
                    <p>Issued on the {DateTime.Now.Day} day of {DateTime.Now.Month}, {DateTime.Now.Year}</p>
                </body>
            </html>";
        }
    }
}
