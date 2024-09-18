namespace Apps.EasyTranslate.Models.Dto;

public class DataDto<T1, T2>
{
    public T1 Data { get; set; }
    
    public List<Generic.Data<T2>>? Included { get; set; } = new();
}