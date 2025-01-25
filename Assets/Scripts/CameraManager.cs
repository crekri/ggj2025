using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private List<Transform> targets; // The Players to follow
    private int playerCount = 0;

    void Start()
    {
        targets = new List<Transform>();

    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (playerCount != players.Length)
        {
            targets.Clear();
            foreach (GameObject player in players)
            {
                targets.Add(player.transform);
            }
            playerCount = players.Length;
        }

        if (targets.Count > 0)
        {
            Vector3 midpoint = CalculateMidpoint();
            transform.position = midpoint;
        }
    }

    private Vector3 CalculateMidpoint()
    {
        if (targets.Count == 0) 
        {
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;
        foreach (Transform target in targets)
        {
            sum += target.position;
        }
        return sum / targets.Count;
    }
}
