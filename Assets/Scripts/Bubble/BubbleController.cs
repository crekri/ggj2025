using System;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubble
{
	public class BubbleController : MonoBehaviour
	{
		[SerializeField] private float soapAmount;
		[FormerlySerializedAs("speed")] [SerializeField] private float startingSpeed;
		[SerializeField] private bool isTrap;

		private Vector2 directionUnit;
		private float speed;
		private float lifetime;

		[SerializeField] private AnimationCurve forwardSpeedCurve;
		[SerializeField] private AnimationCurve ySpeedCurve;

		private float timer = 0;

		public void ApplyDirection(Vector2 directionUnit, float? overrideSpeed = null)
		{
			this.directionUnit = directionUnit;
			this.speed = overrideSpeed ?? startingSpeed;
			timer = 0;
		}

		private Vector2 applyVelocity;

		public void FixedUpdate()
		{
			applyVelocity = directionUnit * speed * forwardSpeedCurve.Evaluate(timer);
			applyVelocity.y = ySpeedCurve.Evaluate(timer);

			this.transform.position += (Vector3) applyVelocity * Time.fixedDeltaTime;
		}

		private void Update()
		{
			timer += Time.deltaTime;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log("BubbleController.OnTriggerEnter2D");
			var playerController = other.GetComponentInParent<IPlayerController>();
			if (playerController != null)
			{
				var hitInfo = new BubbleHitInfo(this, applyVelocity, isTrap, soapAmount);
				playerController.OnBubbleHit(hitInfo);
				Destroy(gameObject);
			}
		}

		public void Parry(Vector2 direction, float strengthPercentage)
		{
			ApplyDirection(direction.SnapTo4Directions(), speed * strengthPercentage);
			//var speed = Mathf.Max(startingSpeed, applyVelocity.magnitude);
			//this.applyVelocity = direction.normalized * (speed * strengthPercentage);
		}
	}
}