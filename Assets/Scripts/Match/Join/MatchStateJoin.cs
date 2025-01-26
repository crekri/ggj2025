using UnityEngine;

namespace Match
{
	public class MatchStateJoin : AMatchStateBehaviour
	{
		[SerializeField] private GameObject joinUI;
		
		public override void OnEnter()
		{
			joinUI.gameObject.SetActive(true);
		}

		public override void OnExit()
		{
			joinUI.gameObject.SetActive(false);
		}
	}
}