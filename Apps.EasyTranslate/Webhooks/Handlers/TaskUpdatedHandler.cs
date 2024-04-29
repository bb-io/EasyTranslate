namespace Apps.EasyTranslate.Webhooks.Handlers;

public class TaskUpdatedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents => new List<string>{ "task.updated" };
}
