using UnityEngine;

namespace Extensions
{
	public static class Vector3Extensions
	{
		public static Vector2 SnapTo4Directions(this Vector2 vector)
		{
			if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
				return new Vector2(Mathf.Sign(vector.x), 0);
			else
				return new Vector2(0, Mathf.Sign(vector.y));
		}
	}
}