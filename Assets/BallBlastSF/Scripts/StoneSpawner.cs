using UnityEngine;
using UnityEngine.Events;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    
    /*private void Spawn()
    {
        Stone stone = Instantiate(_stonePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        stone.SetSize((Stone.Size)Random.Range(1, 4));
        stone.MaxHitPoints = Random.Range(_stoneMinHitPoints, _stoneMaxHitPoints + 1);
        Debug.Log($"stone.MaxHitPoints {stone.MaxHitPoints}");

        // Set Max HitPoints in dependence of stone size
        int decreasePercent = 60 - ((int)(stone.StoneSize) * 20);
        Debug.Log($"decreasePercent {decreasePercent}");

        stone.MaxHitPoints = stone.MaxHitPoints - (stone.MaxHitPoints * decreasePercent) / 100;
        Debug.Log($"stone.MaxHitPoints {stone.MaxHitPoints}");

        _amountSpawned++;
    }*/

    public Stone SpawnStone(int stoneSize, int stoneMaxHitPoints)
    {
        Stone stone = Instantiate(_stonePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
        stone.SetSize((Stone.Size)stoneSize);
        stone.MaxHitPoints = stoneMaxHitPoints;
                        
        return stone;
    }


}
