using Apps.EasyTranslate.Models.Dto.Libraries;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Libraries;

public class LibraryAutomationResponse
{
    [Display("Library automation ID")]
    public string Id { get; set; }

    public string Type { get; set; }
    
    [Display("Is enabled")]
    public bool IsEnabled { get; set; }
    
    public int Threshold { get; set; }
    
    public List<CurrentResponse> Current { get; set; }

    public LibraryAutomationResponse(LibraryAutomationDto dto)
    {
        Id = dto.Data.Id;
        Type = dto.Data.Attributes.Type;
        IsEnabled = dto.Data.Attributes.IsEnabled;
        Threshold = dto.Data.Attributes.Threshold;
        Current = new List<CurrentResponse>();
        
        foreach (var (key, value) in dto.Data.Attributes.Current)
        {
            foreach (var currentLanguageDto in value)
            {
                Current.Add(new CurrentResponse
                {
                    LanguageCode = key,
                    StartedAt = currentLanguageDto.StartedAt,
                    AcceptedAt = currentLanguageDto.AcceptedAt,
                    FinishedAt = currentLanguageDto.FinishedAt,
                    LaasProjectId = currentLanguageDto.LaasProjectId,
                    LaasTaskId = currentLanguageDto.LaasTaskId,
                    Keys = currentLanguageDto.Keys
                });
            }
        }
    }
}

public class CurrentResponse
{
    [Display("Language code")]
    public string LanguageCode { get; set; }
    
    [Display("Started at")]
    public DateTime StartedAt { get; set; }
    
    [Display("Accepted at")]
    public DateTime? AcceptedAt { get; set; }
    
    [Display("Finished at")]
    public DateTime? FinishedAt { get; set; }
    
    [Display("Laas project ID")]
    public string LaasProjectId { get; set; }
    
    [Display("Laas task ID")]
    public string LaasTaskId { get; set; }
    
    [Display("Keys")]
    public List<string> Keys { get; set; }
}