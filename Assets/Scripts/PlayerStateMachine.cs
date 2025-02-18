﻿using System;
using Bubble;
using Match;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerStateMachine : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;

	public PlayerSoakController SoakController => playerSoakController;
	public PlayerFreeState PlayerFreeState => playerFreeState;
	public PlayerInvisibleState PlayerInvisibleState => playerInvisibleState;
	public PlayerTrapState PlayerTrapState => playerTrapState;

	[SerializeField] private PlayerFreeState playerFreeState;
	[SerializeField] private PlayerInvisibleState playerInvisibleState;
	[SerializeField] private PlayerTrapState playerTrapState;
	[SerializeField] private PlayerSoakController playerSoakController;
	[SerializeField] private PlayerParams playerParams;
	public PlayerState CurrentState { get; private set; }
	public PlayerParams PlayerParams { get; private set; }

	public void Awake()
	{
		foreach (var state in GetComponents<PlayerState>())
		{
			state.Init(this);
		}

		TransitTo(GetComponent<PlayerFreeState>());
	}

	public void TransitTo(PlayerState playerState)
	{
		if (CurrentState != null)
		{
			CurrentState.OnExit();
		}

		CurrentState = playerState;
		CurrentState.OnEnter();
	}

	public void FixedUpdate()
	{
		if (CurrentState != null)
		{
			CurrentState.MyFixedUpdate();
		}
	}

	[SerializeField] private AnimationCurve playerTrapRatio;

	public void OnBubbleHit(BubbleHitInfo info)
	{
		playerSoakController.AddSoak(info.SoapAmount);
		var currentAmount = playerSoakController.Soak;

		if (currentAmount >= 1f)
		{
			bool isTrap = Random.Range(0, 1f) <= playerTrapRatio.Evaluate(currentAmount);
			if (isTrap)
			{
				TransitTo(playerTrapState);
			}
		}
	}

	[SerializeField] private ParticleSystem pplHit;
	[SerializeField] private AudioClip pplHitSound;
	[SerializeField] private ParticleSystem pplDeath;
	
	public void OnGuardHit(Vector2 lastInputUnit, float guardHitPower)
	{
		if (CurrentState == PlayerFreeState)
		{
			var fx = Instantiate(pplHit, transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint(pplHitSound,transform.position);
			PlayerFreeState.ApplyKnockback(lastInputUnit, guardHitPower);
		}
		else if (CurrentState == playerTrapState)
		{
			Debug.Log($"Destroy{playerParams.gameObject.name}");
			Destroy(playerParams.gameObject);
		}
	}

	public void SetPosition(Vector3 targetPosition)
	{
		rb.transform.position = targetPosition;
	}
}