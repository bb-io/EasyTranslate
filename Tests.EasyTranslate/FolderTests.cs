using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Models.Requests;

namespace Tests.EasyTranslate
{
    [TestClass]
    public class FolderTests : TestBase
    {
        [TestMethod]
        public async Task GetAllFolders_IsNotNull()
        {
            var action = new FolderActions(InvocationContext);
            var result = await action.GetAllFolders();
            Assert.IsNotNull(result, "Response is null.");

            foreach (var folder in result.Folders)
            {
                Console.WriteLine($"{folder.Id} {folder.Name}");
            }
        }
    
        [TestMethod]
        public async Task GetFolder_IsNotNull()
        {
            var input = new FolderRequest {FolderId= "2cef81cc-aaa8-46ff-b4b8-800e8d16816f" };
            var action = new FolderActions(InvocationContext);
            var result = await action.GetFolder(input);
            Assert.IsNotNull(result, "Response is null.");
            Console.WriteLine($"{result.Id} {result.Name}");
        }

        [TestMethod]
        public async Task CreateFolder_IsNotNull()
        {
            var input = new CreateFolderRequest { Name = "TestA" };
            var action = new FolderActions(InvocationContext);
            var response = await action.CreateFolder(input);
            Assert.IsNotNull(response, "Response is null.");
            Console.WriteLine($"{response.Id} {response.Name}");
        }

        [TestMethod]
        public async Task UpdateFolder_IsNotNull()
        {
            var input = new FolderRequest { FolderId = "83a086b4-e88c-4016-bad5-9d45503030cf" };
            var input2 = new UpdateFolderRequest { Name = "TestA1" };
            var action = new FolderActions(InvocationContext);
            var result = await action.UpdateFolder(input, input2);
            Assert.IsNotNull(result, "Response is null.");
            Console.WriteLine($"{result.Id} {result.Name}");
        }

    } 

}
