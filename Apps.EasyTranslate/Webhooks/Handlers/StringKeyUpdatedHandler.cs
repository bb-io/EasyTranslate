namespace Apps.EasyTranslate.Webhooks.Handlers;

public class StringKeyUpdatedHandler : WebhookHandlerBase
{
    protected override string SubscriptionEvent => "strings.key.updated";
}
