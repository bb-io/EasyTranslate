namespace Apps.EasyTranslate.Webhooks.Handlers;

public class StringKeyUpdatedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents => ["strings.key.updated"];
}