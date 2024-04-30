namespace Apps.EasyTranslate.Models.Dto;

public class ErrorDto
{
    public string Message { get; set; }
    public Data Data { get; set; }
    public Errors Errors { get; set; }
    public Localization Localization { get; set; }
}

public class Data
{
    public string Message { get; set; }
}

public class Errors
{
    public string[] TargetLanguages { get; set; }
}

public class Localization
{
    public string Text { get; set; }
    public string Key { get; set; }
    public Variables Variables { get; set; }
}

public class Variables
{
    public string Attribute { get; set; }
}