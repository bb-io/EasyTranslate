using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.Folders;

public class GetAllFoldersDto
{
    public Data<Dictionary<string, string>>[] Data { get; set; }
    
    public Meta Meta { get; set; }
}