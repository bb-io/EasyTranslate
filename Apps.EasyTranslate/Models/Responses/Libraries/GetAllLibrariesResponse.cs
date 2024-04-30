using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Libraries;

namespace Apps.EasyTranslate.Models.Responses.Libraries;

public class GetAllLibrariesResponse
{
    public List<LibraryResponse> Libraries { get; set; }

    public GetAllLibrariesResponse(GetAllLibrariesDto dto)
    {
        Libraries = dto.Data.Select(x => new LibraryResponse(x)).ToList();
    }
    
    public GetAllLibrariesResponse(List<Data<LibraryAttributes>> libraries)
    {
        Libraries = libraries.Select(x => new LibraryResponse(x)).ToList();
    }
}