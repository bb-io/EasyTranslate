using RestSharp;

namespace Apps.EasyTranslate.Api;

public class EasyTranslateRequestParameters
{
    public string Url { get; set; }
    public Method Method { get; init; }
}