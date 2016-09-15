using BlobStorage;
using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ToDoMobileApp.Controllers;

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
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath)))
                    {
                        fileContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("image/png");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "ProfileIcon.png"
                        };
                        form.Add(fileContent);
                        
                        controller.Request = new HttpRequestMessage { Method = HttpMethod.Get, Content = form };
                        var response = controller.FileUpload(_defaultContainer).Result;

                        //Assert
                        Assert.IsTrue(response.IsSuccessStatusCode);
                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    }
                }
            }
        }

        [TestMethod]
        public void TestGetFileName()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get
                };

                // Act
                var response = controller.GetFileName();

                //Assert
                //Assert
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public void TestGetFileNameException()
        {
            // Arrange
            using (BlobController controller = new BlobController(new ToDoMockBlobService()))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get
                };

                // Act
                var response = controller.GetFileName();

                //Assert
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
            }
        }

        [TestMethod]
        public void TestDeleteFile()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete
                };

                // Act
                var response = controller.DeleteFile();

                //Assert
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public void TestDeleteWithContainerNameFile()
        {
            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete
                };

                // Act
                var response = controller.DeleteFile(_testContainer);

                //Assert
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestMethod]
        public void TestDeleteWithContainerNameDefaultFile()
        {
            #region AddFile

            // Arrange
            using (BlobController controllerBlob = new BlobController(new BlobBL(new Blob())))
            {
                // Act
                string filePath = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "ProfileIcon.png");
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath)))
                    {
                        fileContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("image/png");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "ProfileIcon.png"
                        };
                        form.Add(fileContent);

                        controllerBlob.Request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            Content = form
                        };

                        var response1 = controllerBlob.FileUpload().Result;
                    }
                }
            }


            #endregion


            // Arrange
            using (BlobController controller = new BlobController(new BlobBL(new Blob())))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete
                };

                // Act
                var response = controller.DeleteFile(_defaultContainer);

                //Assert
                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
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
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath)))
                    {
                        fileContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("image/png");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "ProfileIcon.png"
                        };
                        form.Add(fileContent);

                        controller.Request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            Content = form
                        };

                        var response = controller.FileUpload().Result;

                        //Assert
                        Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
                    }
                }
            }
        }

        [TestMethod]
        public void TestDeleteException()
        {
            // Arrange
            using (BlobController controller = new BlobController(new ToDoMockBlobService()))
            {
                controller.Request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete
                };

                // Act
                var response = controller.DeleteFile();

                //Assert
                Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
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
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath)))
                    {
                        fileContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("image/png");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "avatar.docx"
                        };
                        form.Add(fileContent);

                        controller.Request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            Content = form
                        };

                        var response = controller.FileUpload().Result;

                        //Assert
                        Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
                    }
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
                using (MultipartFormDataContent form = new MultipartFormDataContent())
                {
                    using (var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath)))
                    {
                        fileContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = "ProfileIcon.png"
                        };
                        form.Add(fileContent);

                        controller.Request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            Content = form
                        };

                        var response = controller.FileUpload().Result;

                        //Assert
                        Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
                    }
                }
            }
        }
    }
}
