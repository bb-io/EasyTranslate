namespace Apps.EasyTranslate.Models.Dto.Generic;

public class Data<T>
{
    public string Type { get; set; }
    
    public string Id { get; set; }
    
    public T Attributes { get; set; }
}