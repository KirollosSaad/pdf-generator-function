using DinkToPdf.Contracts;
using PDFReceiptGenerator.Function.Models;
using PDFReceiptGenerator.Function.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace PDFReceiptGenerator.Function.Service
{
    public class ReceiptGenerator : IReceiptGenerator
    {
        private readonly IPDFGenerator _pdfGenerator;
        private readonly IConverter _pdfDocumentToByteArrayConverter;
        private readonly IViewRender _viewRender;

        public ReceiptGenerator(IPDFGenerator pdfGenerator,
            IConverter pdfDocumentToByteArrayConverter,
            IViewRender viewRender)
        {
            _pdfGenerator = pdfGenerator ?? throw new ArgumentNullException(nameof(pdfGenerator));
            _pdfDocumentToByteArrayConverter = pdfDocumentToByteArrayConverter ?? throw new ArgumentNullException(nameof(pdfDocumentToByteArrayConverter));
            _viewRender = viewRender ?? throw new ArgumentNullException(nameof(viewRender));
        }

        public async Task<byte[]> GenerateReceipt(ReceiptDTO receipt)
        {
            var receiptString = await _viewRender.Render<ReceiptDTO>("index.cshtml", receipt);  //"sdfsdfsd";// view.Render("Template/index", receipt);
            var receiptPDFDocument = _pdfGenerator.Generate(receiptString, null);
            return _pdfDocumentToByteArrayConverter.Convert(receiptPDFDocument);
        }
    }
}
