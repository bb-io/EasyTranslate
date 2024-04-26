using Blackbird.Applications.Sdk.Common;

namespace Apps.EasyTranslate.Models.Responses.Libraries;

public class OverviewResponse
{
    [Display("Language code")]
    public string LanguageCode { get; set; }
    
    [Display("Total keys")]
    public int TotalKeys { get; set; }
        
    [Display("Total strings")]
    public int TotalStrings { get; set; }
    
    [Display("Strings translated")]
    public int StringsTranslated { get; set; }
}