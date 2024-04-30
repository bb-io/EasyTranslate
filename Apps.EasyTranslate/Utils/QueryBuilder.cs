using System.Text;
using Apps.EasyTranslate.Models.Requests;

namespace Apps.EasyTranslate.Utils;

public static class QueryBuilder
{
    public static string BuildProjectsEndpoint(string baseEndpoint, FetchAllProjectsRequest request)
    {
        var endpointBuilder = new StringBuilder(baseEndpoint);
        
        if (request.Statuses != null && request.Statuses.Any())
        {
            endpointBuilder.Append("?");
            foreach (var status in request.Statuses)
            {
                endpointBuilder.Append($"filters[status][]={status}&");
            }
            
            endpointBuilder.Remove(endpointBuilder.Length - 1, 1);
        }

        return endpointBuilder.ToString();
    }
}