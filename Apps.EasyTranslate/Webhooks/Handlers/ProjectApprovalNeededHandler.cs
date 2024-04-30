using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class ProjectApprovalNeededHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents =>
    [
        "project.status.approval_needed",
    ];

    public ProjectApprovalNeededHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}