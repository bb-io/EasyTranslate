namespace Apps.EasyTranslate.Models.Requests;

public class CreateTranslationKeysRequest : LibraryRequest
{
    public IEnumerable<string> Names { get; set; }
    
    public IEnumerable<string> Texts { get; set; }
}