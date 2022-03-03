using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.DependencyInjection;
using PDFReceiptGenerator.Function.AssemblyLoader;
using PDFReceiptGenerator.Function.Models;
using PDFReceiptGenerator.Function.Service;
using PDFReceiptGenerator.Function.Service.Interfaces;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PDFReceiptGenerator.Function
{
    class Program
    {
        static void Main(string[] args)
        {
            var receiptData = new ReceiptDTO
            {
                AcceptedDate = "21-2-2022",
                Currency = "DKK",
                InvoiceNumber = "4076845405",
                LenderAddress = "Chr M Østergaards Vej",
                LenderCity = "Alex",
                LenderCompanyId = "sdfsdf-sfdsfgjhfgjh-fgh450",
                LenderCompanyName = "Wedio",
                LenderIsCompany = false,
                LenderName = "Valeriu Shustaovich",
                LenderZip = "25004",
                OrderNumber = "898966164946",
                RentalVatWithoutFee = "10",
                RenterAddress = "14 MSC st",
                RenterCity = "Cairo",
                RenterCompanyId = "hjkhj-544-sdf54-sdf",
                RenterCompanyName = "Wedio-2",
                renterIsCompany = false,
                RenterName = "kiro",
                RenterZip = "8546",
                TotalDaysPriceAfterMultipleDayDiscount = "254",
                TotalRentalPrice = "574"
            };

            var receiptGenerator = GetService<IReceiptGenerator>();
            var receiptBytes = receiptGenerator.GenerateReceipt(receiptData).GetAwaiter().GetResult();
            File.WriteAllBytes($"./{Guid.NewGuid().ToString()}.pdf", receiptBytes);
        }

        private static T GetService<T>()
        {
            var serviceProvider = GetServiceProvider();
            return serviceProvider.GetRequiredService<T>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IViewRender, ViewRender>();
            services.AddSingleton<IPDFGenerator, PDFGenerator>();
            services.AddSingleton<IReceiptGenerator, ReceiptGenerator>();
            var libraryFileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "libwkhtmltox.so" : "libwkhtmltox.dll";
            var wkHtmlToPdfBinaries = Path.Combine(Directory.GetCurrentDirectory(), libraryFileName);
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(wkHtmlToPdfBinaries);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        private static IServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }
    }
}
