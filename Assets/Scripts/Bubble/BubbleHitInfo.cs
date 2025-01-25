using System;
using UnityEngine;

namespace Bubble
{
	public struct BubbleHitInfo
	{
		public BubbleController BubbleController { get; }
		public Vector2 BubbleVelocity { get; }
		public float SoapAmount { get; }
		
		public BubbleHitInfo(BubbleController bubbleController, Vector2 bubbleVelocity,  float soapAmount)
		{
			BubbleController = bubbleController;
			BubbleVelocity = bubbleVelocity;
			SoapAmount = soapAmount;
		}
	}
}