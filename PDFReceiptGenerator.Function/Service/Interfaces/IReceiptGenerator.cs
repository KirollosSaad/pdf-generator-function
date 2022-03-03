using PDFReceiptGenerator.Function.Models;
using System.Threading.Tasks;

namespace PDFReceiptGenerator.Function.Service.Interfaces
{
    public interface IReceiptGenerator
    {
        Task<byte[]> GenerateReceipt(ReceiptDTO receipt);
    }
}
