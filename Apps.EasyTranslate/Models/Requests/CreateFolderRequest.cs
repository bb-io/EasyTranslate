namespace Apps.EasyTranslate.Models.Requests;

public class CreateFolderRequest : TeamRequest
{
    public string Name { get; set; }

    public string? Description { get; set; }
}