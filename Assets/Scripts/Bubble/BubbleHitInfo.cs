using UnityEngine;

namespace Bubble
{
	public struct BubbleHitInfo
	{
		public BubbleController BubbleController { get; }
		public Vector2 BubbleVelocity { get; }
		public bool IsTrap { get; }
		public float SoapAmount { get; }
		
		public BubbleHitInfo(BubbleController bubbleController, Vector2 bubbleVelocity, bool isTrap, float soapAmount)
		{
			BubbleController = bubbleController;
			BubbleVelocity = bubbleVelocity;
			IsTrap = isTrap;
			SoapAmount = soapAmount;
		}
	}
}