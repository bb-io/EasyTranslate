using RestSharp;

namespace Apps.EasyTranslate.Api;

public class EasyTranslateRequest : RestRequest
{
    public EasyTranslateRequest(EasyTranslateRequestParameters requestParameters, string token) : base(requestParameters.Url, requestParameters.Method)
    {
        this.AddHeader("Authorization", $"Bearer {token}");
    }
}