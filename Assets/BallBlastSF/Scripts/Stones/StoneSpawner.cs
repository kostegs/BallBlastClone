using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    
    public Stone SpawnStone(int stoneSize, int stoneMaxHitPoints, Vector3 stonePosition)
    {
        Stone stone = Instantiate(_stonePrefab, stonePosition, Quaternion.identity);
        stone.SetSize((Stone.StoneSize)stoneSize);
        stone.HitPoints = stoneMaxHitPoints;
                        
        return stone;
    }

    public Stone SpawnStone(int stoneSize, int stoneMaxHitPoints)
    {
        Vector3 randomPosition = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        Stone stone = SpawnStone(stoneSize, stoneMaxHitPoints, randomPosition);

        return stone;
    }

    public Stone SpawnStone(int stoneSize, int stoneMaxHitPoints, Vector3 stonePosition, float verticalVelocity, float horizontalDirection)
    {
        Stone stone = SpawnStone(stoneSize, stoneMaxHitPoints, stonePosition);
        stone.AddVerticalVelocity(verticalVelocity);
        stone.SetHorizontalDirection(horizontalDirection);

        return stone;
    }


}
