using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private Stone _stonePrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [Header("Stones colors")]
    [SerializeField] private Color[] _stoneColors;

    private Color _alphaChannel = new Color(0.0f, 0.0f, 0.0f, 1.0f);

    public Stone SpawnStone(int stoneSize, int stoneMaxHitPoints, Vector3 stonePosition)
    {
        Stone stone = Instantiate(_stonePrefab, stonePosition, Quaternion.identity);
        stone.SetSize((Stone.StoneSize)stoneSize);
        stone.HitPoints = stoneMaxHitPoints;

        int randomColorIndex = Random.Range(0, _stoneColors.Length);
        Color randomColor = _stoneColors[randomColorIndex];
        stone.SetColor(randomColor + _alphaChannel);
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
