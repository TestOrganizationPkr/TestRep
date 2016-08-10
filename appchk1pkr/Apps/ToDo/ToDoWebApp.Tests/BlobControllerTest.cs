using BlobStorage;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ToDoWebApp.Controllers;

namespace ToDoWebApp.Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BlobControllerTest
    {
        private readonly string _defaultFileName = "profilepic";
        private readonly string _defaultContainer = "mycontainer";
        private readonly string _testContainer = "testcontainer";

        [TestMethod]
        public void TestFileUpload()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {

                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (Stream stream = File.OpenRead(filePath))
                {
                    MyTestPostedFileBase obj = new MyTestPostedFileBase(stream, "image/png", "ProfileIcon.png");
                    var result = controller.FileUpload(obj, _defaultContainer);

                    //Assert
                    Assert.IsFalse(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestGetFileName()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                var result = controller.GetFileName();

                //Assert
                Assert.IsFalse(result.Data.ToString() == "error", "Exception occur so error.");
            }
        }
        
        [TestMethod]
        public void TestGetFileNameException()
        {
            // Arrange
            using (BlobController controller = new BlobController(new ToDoMockBlobService()))
            {
                // Act
                var result = controller.GetFileName();

                //Assert
                Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
            }
        }

        [TestMethod]
        public void TestDeleteFile()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                var result = controller.DeleteFile();

                //Assert
                Assert.IsFalse(result.Data.ToString() == "error", "Exception occur so error.");
            }
        }
        
        [TestMethod]
        public void TestDeleteWithContainerNameFile()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {

                // Act
                var result = controller.DeleteFile(_testContainer);

                //Assert
                Assert.IsFalse(result.Data.ToString() == "error", "Exception occur so error.");
            }
        }

        [TestMethod]
        public void TestDeleteWithContainerNameDefaultFile()
        {
            #region AddFile

            // Arrange
            using (BlobController controller1 = new BlobController(new BlobBL(new Blob())))
            {

                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (Stream stream = File.OpenRead(filePath))
                {
                    MyTestPostedFileBase file = new MyTestPostedFileBase(stream, "image/png", "ProfileIcon.png");
                    controller1.FileUpload(file);
                }
            }
            #endregion


            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                var result = controller.DeleteFile(_defaultContainer);

                //Assert
                Assert.IsFalse(result.Data.ToString() == "error", "Exception occur so error.");
            }
        }

        [TestMethod]
        public void TestFileUploadException()
        {
            // Arrange
            using (BlobController controller = new BlobController(new ToDoMockBlobService()))
            {
                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (Stream stream = File.OpenRead(filePath))
                {
                    MyTestPostedFileBase obj = new MyTestPostedFileBase(stream, "image/png", "ProfileIcon.png");
                    var result = controller.FileUpload(obj);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestDeleteException()
        {
            // Arrange
            using (BlobController controller = new BlobController(new ToDoMockBlobService()))
            {
                // Act
                var result = controller.DeleteFile();

                //Assert
                Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
            }    
        }

        [TestMethod]
        public void TestFileUploadWrongData()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (Stream stream = File.OpenRead(filePath))
                {
                    MyTestPostedFileBase obj = new MyTestPostedFileBase(stream, "image/png", "avatar.docx");
                    var result = controller.FileUpload(obj);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }

        [TestMethod]
        public void TestFileUploadWrongDataScenario()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (Stream stream = File.OpenRead(filePath))
                {
                    MyTestPostedFileBase obj = new MyTestPostedFileBase(stream, "docx", "ProfileIcon.png");
                    var result = controller.FileUpload(obj);

                    //Assert
                    Assert.IsTrue(result.Data.ToString() == "error", "Exception occur so error.");
                }
            }
        }
    }
}
