using System;
using Extensions;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bubble
{
	public class BubbleController : MonoBehaviour
	{
		[SerializeField] private float wallPopDegree = 45; //Only pop if movement direction and wall normal is less than this degree
		[SerializeField] private float soapAmount;
		[FormerlySerializedAs("speed")] [SerializeField] private float startingSpeed;
		[SerializeField] private bool isBigBubble;
		[SerializeField] private float onMergeGrowAmount = .1f;

		private Vector2 directionUnit;
		private float speed;
		private float lifetime;

		[SerializeField] private AnimationCurve forwardSpeedCurve;
		[SerializeField] private AnimationCurve ySpeedCurve;

		private float timer = 0;

		private float targetSize = 1;

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

		private void OnTriggerEnter2D(Collider2D otherCollider)
		{
			Debug.Log("BubbleController.OnTriggerEnter2D");
			var playerController = otherCollider.GetComponentInParent<IPlayerController>();
			if (playerController != null)
			{
				var hitInfo = new BubbleHitInfo(this, applyVelocity, soapAmount);
				playerController.OnBubbleHit(hitInfo);
				Destroy(gameObject);
				return;
			}

			var otherBubble = otherCollider.GetComponentInParent<BubbleController>();
			if (otherBubble != null)
			{
				if (otherBubble.isBigBubble && !this.isBigBubble)
				{
					this.Pop();
					otherBubble.Grow(this.onMergeGrowAmount);
				}
				else if (!otherBubble.isBigBubble && this.isBigBubble)
				{
					otherBubble.Pop();
					this.Grow(otherBubble.onMergeGrowAmount);
				}
				else
				{
					//Both are same bubble rank
					Pop();
					otherBubble.Pop();
				}

				return;
			}

			var isWall = otherCollider.CompareTag(Tags.Wall);
			if (isWall)
			{
				var cloestPoint = otherCollider.ClosestPoint(this.transform.position);
				var normal = (cloestPoint - (Vector2) this.transform.position).normalized;
				if (Vector2.Angle(directionUnit, normal) < wallPopDegree)
				{
					Pop();
				}
			}
		}

		public void Parry(Vector2 direction, float strengthPercentage)
		{
			ApplyDirection(direction.SnapTo4Directions(), speed * strengthPercentage);
		}

		public void Pop()
		{
			Destroy(gameObject);
		}

		public void Grow(float delta) => targetSize += delta;
	}
}