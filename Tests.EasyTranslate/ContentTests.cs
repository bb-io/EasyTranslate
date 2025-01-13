using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.EasyTranslate.Actions;
using Apps.EasyTranslate.Connections;
using Apps.EasyTranslate.Models.Requests;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Tests.EasyTranslate
{
    [TestClass]
    public class ContentTests :TestBase
    {

        [TestMethod]
        public async Task CreateConten_IsNotNull()
        {
            var parameters = new CreateContentRequest 
            { 
                Prompt = "Explain the scince"
            };

            var action = new ContentGenerationActions(InvocationContext);
            var result = await action.CreateContentAsync(parameters);
            Assert.IsNotNull(result, "Response is null.");
            Assert.IsNotNull(result.Response, "Response data is null.");
        }
    }
}
