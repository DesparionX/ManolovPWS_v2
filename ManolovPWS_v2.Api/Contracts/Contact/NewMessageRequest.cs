namespace ManolovPWS_v2.Api.Contracts.Inbox
{
    public sealed record NewMessageRequest(
        string Title,
        string Context,
        string SenderName,
        string SenderEmail
        );
}
