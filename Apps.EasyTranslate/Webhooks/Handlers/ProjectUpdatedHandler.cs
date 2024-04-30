namespace Apps.EasyTranslate.Webhooks.Handlers;

public class ProjectUpdatedHandler : WebhookHandlerBase
{
    protected override List<string> SubscriptionEvents =>
    [
        "project.status.approval_needed",
        "project.status.price_accepted",
        "project.status.price_declined",
        "project.status.cancelled_by_customer"
    ];
}
