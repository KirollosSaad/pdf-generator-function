using PDFReceiptGenerator.Function.Service.Interfaces;
using RazorLight;
using System.IO;
using System.Threading.Tasks;

namespace PDFReceiptGenerator.Function.Service
{
    public class ViewRender : IViewRender
    {
        private readonly IRazorLightEngine _engine;
        public ViewRender()
        {
            var absolutePath = Path.GetFullPath("./Template");
            _engine = new RazorLightEngineBuilder()
                       .UseFileSystemProject(absolutePath)
                       .UseMemoryCachingProvider()
                       .Build();
        }

        public async Task<string> Render<TModel>(string name, TModel model)
        {
            return await _engine.CompileRenderAsync("./index.cshtml", model);
        }
    }
}
