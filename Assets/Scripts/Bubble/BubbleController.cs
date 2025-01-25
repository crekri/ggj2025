using System;
using UnityEngine;

namespace Bubble
{
	public class BubbleController : MonoBehaviour
	{
		[SerializeField] private float soapAmount;
		[SerializeField] private float speed;
		[SerializeField] private bool isTrap;

		private Vector2 velocity;
		private float lifetime;

		public void Setup(Vector2 direction01)
		{
			this.velocity = direction01 * speed;
		}

		public void FixedUpdate()
		{
			this.transform.position += (Vector3) velocity * Time.fixedDeltaTime;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log("BubbleController.OnTriggerEnter2D");
			var playerController = other.GetComponentInParent<IPlayerController>();
			if (playerController != null)
			{
				var hitInfo = new BubbleHitInfo(this, velocity, isTrap, soapAmount);
				playerController.OnBubbleHit(hitInfo);
				Destroy(gameObject);
			}
		}
	}
}