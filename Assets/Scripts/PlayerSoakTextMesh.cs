using UnityEngine;

public class PlayerSoakTextMesh : MonoBehaviour
{
	[SerializeField] private PlayerSoakController soakController;
	[SerializeField] private TextMesh textMesh;
    
	private void Update()
	{
		textMesh.text = $"{soakController.Soak:P0}";
	}
}