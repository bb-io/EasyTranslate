using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Libraries;
using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Libraries;

public class LibraryResponse
{
    [Display("Library ID")]
    public string Id { get; set; }
    
    [Display("Library name")]
    public string Name { get; set; }
    
    [Display("Library description")]
    public string Description { get; set; }

    [Display("File name")]
    public string FileName { get; set; }
    
    [Display("Status")]
    public string Status { get; set; }
    
    [Display("Team ID")]
    public string TeamId { get; set; }
    
    [Display("Source language")]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages")]
    public string[] Languages { get; set; }

    [Display("User ID")]
    public string UserId { get; set; }

    [Display("Overview")]
    public List<OverviewResponse> Overview { get; set; }

    [Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [Display("Updated at")]
    public DateTime UpdatedAt { get; set; }

    public LibraryResponse(Data<LibraryAttributes> data)
    {
        Id = data.Id;
        Name = data.Attributes.Name;
        Description = data.Attributes.Description;
        FileName = data.Attributes.FileName;
        Status = data.Attributes.Status;
        TeamId = data.Attributes.TeamId;
        SourceLanguage = data.Attributes.SourceLanguage;
        Languages = data.Attributes.Languages;
        UserId = data.Attributes.User.Id;
        Overview = data.Attributes.Overview.Select(x => new OverviewResponse
        {
            LanguageCode = x.LanguageCode,
            TotalKeys = x.TotalKeys,
            TotalStrings = x.TotalStrings,
            StringsTranslated = x.StringsTranslated
        }).ToList();
        CreatedAt = DateTime.Parse(data.Attributes.CreatedAt);
        UpdatedAt = DateTime.Parse(data.Attributes.UpdatedAt);
    }
}