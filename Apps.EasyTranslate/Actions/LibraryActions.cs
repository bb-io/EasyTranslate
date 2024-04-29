using RestSharp;
using Apps.EasyTranslate.Invocables;
using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Libraries;
using Apps.EasyTranslate.Models.Requests;
using Apps.EasyTranslate.Models.Responses.Libraries;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;

namespace Apps.EasyTranslate.Actions;

[ActionList]
public class LibraryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : AppInvocable(invocationContext)
{
    private readonly IFileManagementClient fileManagementClient = fileManagementClient;

    [Action("Get all libraries", Description = "Get all libraries for a team")]
    public async Task<GetAllLibrariesResponse> GetAllLibraries([ActionParameter] TeamRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries";

        var allLibraries = new List<Data<LibraryAttributes>>();
        int currentPage = 1;
        MetaPagination meta;

        do
        {
            var dto = await Client.ExecuteWithJson<GetAllLibrariesDto>(endpoint, Method.Get, null, Creds);

            if (dto?.Data != null)
            {
                allLibraries.AddRange(dto.Data);
                meta = dto.Meta;
                currentPage++;
            }
            else
            {
                break;
            }
        } while (currentPage <= (meta?.LastPage ?? 1));

        return new GetAllLibrariesResponse(allLibraries);
    }

    [Action("Get a library", Description = "Get a library for a team")]
    public async Task<LibraryResponse> GetLibrary([ActionParameter] LibraryRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}";
        var dto = await Client.ExecuteWithJson<GetLibraryDto>(endpoint, Method.Get, null, Creds);
        return new LibraryResponse(dto.Data);
    }

    [Action("Create a library", Description = "Create a library for a team")]
    public async Task<LibraryResponse> CreateLibrary([ActionParameter] CreateLibraryRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries";

        var body = new
        {
            data = new
            {
                type = "library",
                attributes = new
                {
                    name = request.Name,
                    description = request.Description ?? string.Empty,
                    source_language = request.SourceLanguage,
                    type = request.Type,
                    target_languages = request.TargetLanguages,
                    translations = new
                    {
                        menu = new
                        {
                            language = request.MenuLanguage,
                            currency = request.MenuCurrency,
                            button = request.MenuButton,
                        },
                        terms = new
                        {
                            privacy_policy = new
                            {
                                text = request.PrivacyPolicyText,
                                title = request.SummaryTitle,
                            }
                        },
                        summary = new
                        {
                            title = request.SummaryTitle,
                            text = request.SummaryText,
                            language = new
                            {
                                source = request.SummarySourceLanguage,
                                target = request.SummaryTargetLanguage
                            },
                            project_section = new
                            {
                                name = request.ProjectFileName,
                                translator = request.Translator,
                                word_count = request.WordCount,
                                price_label = request.PriceLabel,
                                price_total = request.PriceTotal
                            }
                        }
                    }
                }
            }
        };

        var dto = await Client.ExecuteWithJson<GetLibraryDto>(endpoint, Method.Post, body, Creds);
        return new LibraryResponse(dto.Data);
    }

    [Action("Delete a library", Description = "Delete a library for a team")]
    public async Task DeleteLibrary([ActionParameter] LibraryRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}";
        await Client.ExecuteWithJson(endpoint, Method.Delete, null, Creds);
    }

    [Action("Start library automation", Description = "Start library automation for a team")]
    public async Task<LibraryAutomationResponse> StartLibraryAutomation([ActionParameter] LibraryAutomationRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}/start-automation";

        var attributes = new Dictionary<string, object>
        {
            { "target_languages", request.TargetLanguages }
        };

        if (request.KeyNames != null && request.KeyNames.Any())
        {
            attributes.Add("key_names", request.KeyNames);
        }

        var body = new
        {
            data = new
            {
                type = "library_automation",
                attributes
            }
        };

        var dto = await Client.ExecuteWithJson<LibraryAutomationDto>(endpoint, Method.Post, body, Creds);
        return new LibraryAutomationResponse(dto);
    }

    [Action("Add target languages to a library", Description = "Add target languages to a library for a team")]
    public async Task<LibraryResponse> AddTargetLanguages([ActionParameter] TargetLanguagesRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}/languages";

        var body = new
        {
            data = new
            {
                type = "library-language",
                attributes = new
                {
                    target_languages = request.TargetLanguages
                }
            }
        };

        var dto = await Client.ExecuteWithJson<GetLibraryDto>(endpoint, Method.Put, body, Creds);
        return new LibraryResponse(dto.Data);
    }

    [Action("Remove target languages from a library", Description = "Remove target languages from a library for a team")]
    public async Task<LibraryResponse> RemoveTargetLanguages([ActionParameter] RemoveTargetLanguagesRequest request)
    {
        var targetLanguage = request.TargetLanguage;
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}/languages/{targetLanguage}";
        var dto = await Client.ExecuteWithJson<GetLibraryDto>(endpoint, Method.Delete, null, Creds);
        return new LibraryResponse(dto.Data);
    }

    [Action("Download library", Description = "Download a library for a team")]
    public async Task<LibraryDownloadResponse> DownloadLibrary([ActionParameter] DownloadLibraryRequest request)
    {
        string endpoint = $"/strings-library/api/v1/teams/{request.TeamName}/libraries/{request.LibraryId}/download";

        var body = new
        {
            data = new
            {
                type = "library-download",
                attributes = new
                {
                    languages = request.Languages,
                    options = new
                    {
                        exclude_empty_translations = request.ExcludeEmptyTranslations ?? false,
                        unpack_strings = request.UnpackStrings ?? false
                    }
                }
            }
        };

        var response = await Client.ExecuteWithJson(endpoint, Method.Post, body, Creds);

        var bytes = response.RawBytes ?? throw new Exception("Failed to download library, returned an empty response");
        var memoryStream = new MemoryStream(bytes);

        var library = await GetLibrary(request);
        var fileReference = await fileManagementClient.UploadAsync(memoryStream, ContentType.GZip, $"{library.Name}.zip");
        return new LibraryDownloadResponse()
        {
            File = fileReference
        };
    }
}