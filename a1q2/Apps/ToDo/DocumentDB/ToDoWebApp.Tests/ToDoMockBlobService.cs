using BusinessLogic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ToDoWebApp.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class ToDoMockBlobService : IBlobBL
    {
        public void DeleteBlob(string fileName, string containerName = "")
        {
            throw new NotImplementedException();
        }

        public string GetBlob(string fileName, string containerName = "")
        {
            throw new NotImplementedException();
        }

        public void SaveBlob(string fileName, Stream stream, string containerName = "", int access = 1)
        {
            throw new NotImplementedException();
        }
    }
}
