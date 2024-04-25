using Apps.EasyTranslate.Models.Dto.Folders;

namespace Apps.EasyTranslate.Models.Responses.Folders;

public class GetAllFoldersResponse
{
    public List<FolderResponse> Folders { get; set; }
    
    public GetAllFoldersResponse(GetAllFoldersDto dto)
    {
        Folders = dto.Data.Select(x => new FolderResponse(x)).ToList();
    }
}