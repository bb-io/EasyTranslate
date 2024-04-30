using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class ProjectCancelledHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents =>
    [
        "project.status.cancelled_by_customer",
    ];

    public ProjectCancelledHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}