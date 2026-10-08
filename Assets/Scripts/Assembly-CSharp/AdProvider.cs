using System;

[Serializable]
public class AdProvider
{
	public string displayName;

	public string id;

	public string consentProvided;

	public bool IsConsentProvided
	{
		get
		{
			return Convert.ToBoolean(consentProvided);
		}
		set
		{
			consentProvided = Convert.ToString(value).ToLower();
		}
	}
}
