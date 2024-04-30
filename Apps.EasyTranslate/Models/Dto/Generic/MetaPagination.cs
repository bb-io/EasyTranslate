namespace Apps.EasyTranslate.Models.Dto.Generic;

public class MetaPagination
{
    public int CurrentPage { get; set; }
    
    public int? From { get; set; }
    
    public int LastPage { get; set; }
    
    public Link[] Links { get; set; }
    
    public string Path { get; set; }
    
    public int PerPage { get; set; }
    
    public int? To { get; set; }
    
    public int Total { get; set; }
    
    public string Copyright { get; set; }
    
    public string Environment { get; set; }
}