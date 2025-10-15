using System.Net.Http.Headers;
using System.Text;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Content;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Content;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;

namespace Apps.EasyTranslate.Actions;

[ActionList("Content generation")]
public class ContentGenerationActions(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Action("Create content", Description = "Create content based on provided prompt and settings")]
    public async Task<CreateContentResponse> CreateContentAsync([ActionParameter]CreateContentRequest request)
    {
        var token = await Client.GetToken(Creds); 
        var baseUrl = Client.BuildUrl(Creds);

        var endpoint = $"{baseUrl}/laas/api/v1/teams/[teamname]/caas/generate";

        var body = new
        {
            data = new
            {
                type = "content_generation",
                attributes = new
                {
                    prompt = request.Prompt,
                    language = request.Language ?? "en",
                    max_tokens = request.MaxTokens ?? 340,
                    temperature = request.Temperature ?? "0.5",
                    presence_penalty = request.PresencePenalty ?? "0",
                    frequency_penalty = request.FrequencyPenalty ?? "0",
                    main_subject = request.MainSubject,
                    content_brief = request.ContentBrief,
                    keywords = request.Keywords,
                    tone_of_voice = request.ToneOfVoice
                }
            }
        };

        var json = JsonConvert.SerializeObject(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));

        var httpResponse = await httpClient.PostAsync(endpoint, content);

        if (httpResponse.IsSuccessStatusCode)
        {
            // Open the SSE stream
            var stream = await httpResponse.Content.ReadAsStreamAsync();

            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (!string.IsNullOrEmpty(line) && line.StartsWith("data:"))
                {
                    var jsonData = line.Substring("data:".Length);
                    var responseDto = JsonConvert.DeserializeObject<ContentGenerationDto>(jsonData);
                    if (responseDto != null)
                    {
                        return new CreateContentResponse { Response = responseDto.Data.Attributes.GeneratedContent };
                    }
                }
            }
        }
        else
        {
            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            throw new PluginApplicationException($"Request failed with status code {httpResponse.StatusCode} and message: {responseContent}");
        }
        
        return new CreateContentResponse { Response = "No content generated" };
    }
}