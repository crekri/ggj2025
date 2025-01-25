using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubble
{
	public class BubbleController : MonoBehaviour
	{
		[SerializeField] private float soapAmount;
		[FormerlySerializedAs("speed")] [SerializeField] private float startingSpeed;
		[SerializeField] private bool isTrap;

		private Vector2 velocity;
		private float lifetime;

		public void Setup(Vector2 direction01)
		{
			this.velocity = direction01 * startingSpeed;
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

		public void Parry(Vector2 direction, float strengthPercentage)
		{
			var speed = Mathf.Max(startingSpeed, velocity.magnitude);
			this.velocity = direction.normalized * (speed * strengthPercentage);
		}
	}
}