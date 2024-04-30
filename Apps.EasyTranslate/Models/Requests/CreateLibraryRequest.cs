using Apps.EasyTranslate.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.EasyTranslate.Models.Requests;

public class CreateLibraryRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }

    [Display("Source language"), DataSource(typeof(SourceLanguageDataHandler))]
    public string SourceLanguage { get; set; }
    
    [Display("Target languages"), DataSource(typeof(TargetLanguageDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }
    
    public string? Type { get; set; }

    [Display("Privacy policy text")]
    public string? PrivacyPolicyText { get; set; }
    
    [Display("Summary title")]
    public string? SummaryTitle { get; set; }
    
    [Display("Summary text")]
    public string? SummaryText { get; set; }
    
    [Display("Summary source language")]
    public string? SummarySourceLanguage { get; set; }
    
    [Display("Summary target language")]
    public string? SummaryTargetLanguage { get; set; }
    
    [Display("Project file name")]
    public string? ProjectFileName { get; set; }
    
    [Display("Translator")]
    public string? Translator { get; set; }
    
    [Display("Word count")]
    public string? WordCount { get; set; }
    
    [Display("Price label")]
    public string? PriceLabel { get; set; }
    
    [Display("Price total")]
    public string? PriceTotal { get; set; }
    
    [Display("Menu language")]
    public string? MenuLanguage { get; set; }
    
    [Display("Menu currency")]
    public string? MenuCurrency { get; set; }
    
    [Display("Menu button")]
    public string? MenuButton { get; set; }
}