using System;
using System.Collections.Generic;
using Bubble;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
	public float Ammo01 { get; private set; }

	public float BigBubbleCost => bigCost;

	[SerializeField] private float smallCost = .2f;
	[SerializeField] private float bigCost = .6f;
	[SerializeField] private float rechargeRate = .3f;

	[SerializeField] private Transform bubbleSpawnPoint;
	[SerializeField] private BubbleController smallBubblePrefab;
	[SerializeField] private BubbleController bigBubblePrefab;

	private IPlayerController playerController;

	private bool isFirstPressed;

	private void Awake()
	{
		playerController = GetComponent<IPlayerController>();
	}

	public float BubbleChargeTime = .5f;

	private bool wasPressed;
	private bool isPressed;
	private float bigBubbleChargeTimer;
	private bool isCharging;

	public void SetInput(bool isPressed)
	{
		this.isPressed = isPressed;
	}

	private void Update()
	{
		bool hasMinAmmo = Ammo01 >= smallCost;
		bool hasBigAmmo = Ammo01 >= bigCost;

		if (hasMinAmmo && isPressed)
		{
			bigBubbleChargeTimer += Time.deltaTime;
			isCharging = true;

			if (hasBigAmmo && bigBubbleChargeTimer >= BubbleChargeTime)
				BlowBigBubble();

			wasPressed = isPressed;
		}
		else
		{
			bigBubbleChargeTimer = 0;
			if (isCharging)
				BlowBubble();
		}

		Ammo01 += rechargeRate * Time.deltaTime;
	}

	private void BlowBubble()
	{
		isCharging = false;
		bigBubbleChargeTimer = 0;
		Ammo01 -= smallCost;

		var bubble = Instantiate(smallBubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
		bubble.Setup(playerController.GetOrientation());
	}

	private void BlowBigBubble()
	{
		isCharging = false;
		bigBubbleChargeTimer = 0;
		Ammo01 -= bigCost;

		var bubble = Instantiate(bigBubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
		bubble.Setup(playerController.GetOrientation());
	}
}