using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class ProjectPriceDeclinedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents =>
    [
        "project.status.price_declined",
    ];

    public ProjectPriceDeclinedHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}