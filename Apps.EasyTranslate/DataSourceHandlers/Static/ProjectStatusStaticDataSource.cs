using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.EasyTranslate.DataSourceHandlers.Static;

public class ProjectStatusStaticDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "COMPLETED", "Completed" },
            { "CREATED", "Created" },
            { "APPROVAL_NEEDED", "Approval needed" },
            { "PRICE_ACCEPTED", "Price accepted" },
            { "PRICE_DECLINED", "Price declined" },
        };
    }
}