using System;
using UnityEngine;

public class StoneManager : MonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] private StoneSpawner _spawner;
    [SerializeField] private float _spawnUpForce;

    [Header("GamePlay Settings")]
    [SerializeField] private GamePlaySettings _gamePlaySettings;

    [Header("Balance")]
    [SerializeField] private float _spawnRate; 
    [SerializeField][Range(0.0f, 1.0f)] private float _minHitPointsPercentage;
    [SerializeField] private float _maxHitPointsRate;
    [SerializeField] private int _stonesAmount;

    private int _stonesMaxHitPoints;
    private int _stonesMinHitPoints;
    private float _timer;
    private int _amountSpawned;
    private int _currentStonesAmount;
    private int[] _stoneSizes;

    private void Start()
    {
        int damagePerSecond = (int)((_gamePlaySettings.Damage * _gamePlaySettings.ProjectileAmount) * (1 / _gamePlaySettings.FireRate));

        _stonesMaxHitPoints = (int)(damagePerSecond * _maxHitPointsRate);
        _stonesMinHitPoints = (int)(_stonesMaxHitPoints * _minHitPointsPercentage);

        _timer = _spawnRate;

        CreateStonesCharacteristics();
    }

    private void CreateStonesCharacteristics()
    {
        _stoneSizes = new int[_stonesAmount];

        for (int i = 0; i < _stonesAmount; i++)
        {
            int stoneSize = UnityEngine.Random.Range(1, 4);
            _stoneSizes[i] = stoneSize;
        }
    }

    private void Update()
    {
        if (_amountSpawned != _stonesAmount)
        {
            _timer += Time.deltaTime;

            if (_timer >= _spawnRate)
            {
                SpawnStone();
                _timer = 0;
            }
        }
    }

    private void SpawnChildStones(Stone.StoneSize parentSize, int parentMaxHitpoints, Vector3 parentPosition)
    {
        int sizeNewStones = ((int)parentSize) - 1;
        int maxHP_NewStones = Mathf.Clamp(parentMaxHitpoints / 2, 1, parentMaxHitpoints);

        for (int i = 0; i < 2; i++)
        {
            float direction = (i % 2 * 2) - 1;

            Stone stone = _spawner.SpawnStone(sizeNewStones, maxHP_NewStones, parentPosition, _spawnUpForce, direction);
            SubscribeToStoneEvents(stone);
        }
    }

    private void SpawnStone()
    {
        int currentStoneSize = _stoneSizes[_amountSpawned];

        int maxHitPoints = UnityEngine.Random.Range(_stonesMinHitPoints, _stonesMaxHitPoints + 1);

        int decreasePercent = 60 - (currentStoneSize * 20);

        maxHitPoints = maxHitPoints - (maxHitPoints * decreasePercent) / 100;

        Stone stone = _spawner.SpawnStone(currentStoneSize, maxHitPoints);
        SubscribeToStoneEvents(stone);

        _amountSpawned++;
    }

    private void SubscribeToStoneEvents(Stone stone)
    {
        stone.OnStoneCollision += StoneCollisionHandler;
        stone.OnStoneHitPointsEnd += StoneHitPointsEndHandler;
    }

    public void StoneCollisionHandler(object stone, StoneCollisionEventArgs eventArgs)
    {
        if (eventArgs._collisionTransform.GetComponent<Projectile>() != null)
        {
            int damage = _gamePlaySettings.Damage; 

            if (stone is Stone)
                ((Stone)stone).ApplyDamage(damage);
        }
    }

    public void StoneHitPointsEndHandler(object _stone, EventArgs eventArgs)
    {
        Stone stone = _stone as Stone;

        if (stone.Size != Stone.StoneSize.Small)
            SpawnChildStones(stone.Size, stone.MaxHitPoints, stone.transform.position);

        Destroy(stone.gameObject);
    }
}
