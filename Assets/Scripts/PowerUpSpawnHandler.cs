using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnHandler : MonoBehaviour
{
    public List<Transform> spawnPts;
    public float spawnRate;
    private float _currentSpawnRate;
    
    
    public IEnumerator SpawnPowerUpRoutine()
    {
        _isSpawning = true;
        //do something 

        _currentSpawnRate = spawnRate;
        var randPoints = Random.Range(0, Mathf.Max(spawnPts.Count, 0));
        for (int i = 0; i < randPoints; i++)
        {
            SpawnPowerUp();
            yield return new WaitForSeconds(.1f);
        }


        _isSpawning = false;
    }

    private void SpawnPowerUp()
    {
        //Game Feel spawn in curve
    }

    private bool _isSpawning;
    public void Update()
    {
        if (_currentSpawnRate > spawnRate)
        {
            _currentSpawnRate -= Time.deltaTime;
        }
        else
        {
            if(_isSpawning) return;
            StartCoroutine(SpawnPowerUpRoutine());
            
            
        }
    }
}