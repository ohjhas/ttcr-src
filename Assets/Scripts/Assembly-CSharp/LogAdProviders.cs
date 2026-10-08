using System;
using System.Collections.Generic;

[Serializable]
public struct LogAdProviders
{
	public List<string> AuthorizedAdProv;

	public List<string> ForbiddenAdProv;

	public LogAdProviders(List<string> authProv, List<string> unAtuthProv)
	{
		AuthorizedAdProv = authProv;
		ForbiddenAdProv = unAtuthProv;
	}
}
