using System;
using System.Collections.Generic;
using Bubble;
using UnityEngine;

public class PlayerAbilityController : MonoBehaviour
{
	[SerializeField] private Transform bubbleSpawnPoint;
	[SerializeField] private BubbleController smallBubblePrefab;
	[SerializeField] private BubbleController bigBubblePrefab;

	private IPlayerController playerController;

	private bool isFirstPressed;

	[SerializeField] private float smallBubbleSpeed = 10f;
	[SerializeField] private float bigBubbleSpeed = 5f;

	private void Awake()
	{
		playerController = GetComponent<IPlayerController>();
	}

	public float BubbleChargeTime = .5f;

	private bool wasPressed;
	private bool isPressed;
	private float inputTimer;
	private bool isCharging;

	public void SetInput(bool isPressed)
	{
		this.isPressed = isPressed;
	}

	private void Update()
	{
		if (isPressed)
		{
			inputTimer += Time.deltaTime;
			isCharging = true;

			if (inputTimer >= BubbleChargeTime)
			{
				BlowBigBubble();
			}
		}
		else
		{
			if (wasPressed)
			{
				BlowBubble();
			}
		}

		wasPressed = isPressed;
	}

	private void BlowBubble()
	{
		isCharging = false;
		inputTimer = 0;

		var bubble = Instantiate(smallBubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
		bubble.Spawn(playerController.GetOrientation() * smallBubbleSpeed);
	}

	private void BlowBigBubble()
	{
		isCharging = false;
		inputTimer = 0;

		var bubble = Instantiate(bigBubblePrefab, bubbleSpawnPoint.position, Quaternion.identity);
		bubble.Spawn(playerController.GetOrientation() * bigBubbleSpeed);
	}
}