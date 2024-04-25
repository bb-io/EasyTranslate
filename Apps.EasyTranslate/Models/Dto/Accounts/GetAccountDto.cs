using Apps.EasyTranslate.Models.Dto.Generic;
using Apps.EasyTranslate.Models.Dto.Users;

namespace Apps.EasyTranslate.Models.Dto.Accounts;

public class GetAccountDto
{
    public Data<AccountAttributes> Data { get; set; }
    
    public Meta Meta { get; set; }
}