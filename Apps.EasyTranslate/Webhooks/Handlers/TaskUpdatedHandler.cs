using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class TaskUpdatedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents => ["task.updated"];

    public TaskUpdatedHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}