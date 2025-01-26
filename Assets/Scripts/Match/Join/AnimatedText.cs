using TMPro;
using UnityEngine;

namespace Match
{
	public class AnimatedText : MonoBehaviour
	{
		public TextMeshProUGUI Text => text;
		[SerializeField] private TextMeshProUGUI text;
		[SerializeField] private float animateSpeed = 10f;
		private float showTimer = 0;
		private float targetScale = 1f;

		public void ChangeText(string newMessage, float startScale = .8f, float targetScale = 1f, float showDuration = 1f)
		{
			showTimer = showDuration;
			if (text.text == newMessage && showTimer > 0)
				return;

			text.text = newMessage;
			gameObject.transform.localScale = Vector3.one * startScale;
			this.targetScale = targetScale;
		}

		private void Update()
		{
			showTimer -= Time.deltaTime;
			transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * targetScale, animateSpeed * Time.deltaTime);
			text.alpha = showTimer * 2f;
		}
	}
}