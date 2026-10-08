using Entitas;

[State]
public sealed class O7SdkSendLogsComponent : IComponent
{
	public string recipientEmail;

	public string subject;

	public string body;

	public string[] attachmentPaths;

	public int requestCode;
}
