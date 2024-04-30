namespace Apps.EasyTranslate.Models.Requests;

public class CreateTranslationKeyRequest : LibraryRequest
{
    public string Name { get; set; }
    
    public string Text { get; set; }
}