using System;
using System.Runtime.Loader;

namespace PDFReceiptGenerator.Function.AssemblyLoader
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedLibraryName)
        {
            return LoadUnmanagedDllFromPath(unmanagedLibraryName);
        }
    }
}
