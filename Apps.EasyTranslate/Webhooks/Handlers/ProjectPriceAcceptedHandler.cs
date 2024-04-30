using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class ProjectPriceAcceptedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents =>
    [
        "project.status.price_accepted",
    ];

    public ProjectPriceAcceptedHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}
