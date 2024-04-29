namespace Apps.EasyTranslate.Webhooks.Handlers;

public class TaskUpdatedHandler : WebhookHandlerBase
{
    protected override string SubscriptionEvent => "task.updated";
}
