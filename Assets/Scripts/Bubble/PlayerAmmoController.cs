using UnityEngine;

public class PlayerAmmoController : MonoBehaviour
{
	[SerializeField] private float maxAmmo = 5;
	public float MaxAmmo => maxAmmo;
	public float Ammo01 => Mathf.Clamp01(Ammo / maxAmmo);

	public float Ammo { get; private set; }

	public bool HasEnoughAmmo(float amount) => Ammo >= amount;

	public bool TryConsumeAmmo(float amount)
	{
		if (HasEnoughAmmo(amount))
		{
			Ammo -= amount;
			return true;
		}

		return false;
	}

	private void Awake()
	{
		Ammo = maxAmmo;
	}

	public void GrantAmmo(float amount)
	{
		Ammo = Mathf.Min(Ammo + amount, maxAmmo);
	}
}