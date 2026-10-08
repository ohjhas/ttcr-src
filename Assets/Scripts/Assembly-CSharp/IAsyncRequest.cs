using System;
using UnityEngine;

public interface IAsyncRequest
{
	AsyncRequestComponent asyncRequest { get; }

	bool hasAsyncRequest { get; }

	void AddAsyncRequest(ResourceRequest newRequest, Action newCallback);

	void ReplaceAsyncRequest(ResourceRequest newRequest, Action newCallback);

	void RemoveAsyncRequest();
}
