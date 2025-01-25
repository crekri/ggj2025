using System;
using UnityEngine;

namespace Bubble
{
	public class BubbleController : MonoBehaviour
	{
		private Vector2 velocity;

		public void Spawn(Vector2 velocity)
		{
			this.velocity = velocity;
		}

		public void FixedUpdate()
		{
			this.transform.position += (Vector3) velocity * Time.fixedDeltaTime;
		}
	}
}