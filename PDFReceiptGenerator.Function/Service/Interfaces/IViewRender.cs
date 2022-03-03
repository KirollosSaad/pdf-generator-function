using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace PDFReceiptGenerator.Function.Service.Interfaces
{
    public interface IViewRender
    {
        Task<string> Render<TModel>(string name, TModel model);
    }
}
