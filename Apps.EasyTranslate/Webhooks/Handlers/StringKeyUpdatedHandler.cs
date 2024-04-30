using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.EasyTranslate.Webhooks.Handlers;

public class StringKeyUpdatedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents => ["strings.key.updated"];

    public StringKeyUpdatedHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}