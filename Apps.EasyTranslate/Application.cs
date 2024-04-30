using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.EasyTranslate;

public class Application : IApplication, ICategoryProvider
{
    public string Name
    {
        get => "EasyTranslate";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ApplicationCategory> Categories
    {
        get => new[]
        {
            ApplicationCategory.TranslationBusinessManagement, 
            ApplicationCategory.MachineTranslationAndMtqe,
            ApplicationCategory.ArtificialIntelligence
        };
        set { }
    }
}