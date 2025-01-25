using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cinemachine.CinemachineTargetGroup))]
public class PlayerRetriever : MonoBehaviour
{
    private Cinemachine.CinemachineTargetGroup targetGroup;
    private int playerCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetGroup = GetComponent<Cinemachine.CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (playerCount != players.Length)
        {
            targetGroup.m_Targets = new Cinemachine.CinemachineTargetGroup.Target[players.Length];
            for (int i = 0; i < players.Length; i++)
            {
                targetGroup.m_Targets[i].target = players[i].transform;
                targetGroup.m_Targets[i].weight = 1;
                targetGroup.m_Targets[i].radius = 5;
            }
            playerCount = players.Length;
        }
    }
}
