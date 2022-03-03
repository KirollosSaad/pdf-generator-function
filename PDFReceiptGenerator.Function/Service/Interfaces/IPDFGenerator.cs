using DinkToPdf;

namespace PDFReceiptGenerator.Function.Service.Interfaces
{
    public interface IPDFGenerator
    {
        public HtmlToPdfDocument Generate(string content, string userStyleSheet);
    }
}
