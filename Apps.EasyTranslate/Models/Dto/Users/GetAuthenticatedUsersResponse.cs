using Apps.EasyTranslate.Models.Dto.Generic;

namespace Apps.EasyTranslate.Models.Dto.Users;

public class GetAuthenticatedUsersResponse
{
    public Data<UserAttributes> Data { get; set; }
    
    public Meta Meta { get; set; }
}
