using System;
using System.Collections.Generic;
using UnityEngine;

public class StonesManager : MonoBehaviour
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

    [Header("Other")]
    [SerializeField] private GameMgr _gameMgr;

    private int _stonesMaxHitPoints;
    private int _stonesMinHitPoints;
    private float _timer;
    private int _amountSpawned;    
    private int[] _stoneSizes;
    private StoneDestroyedEventArgs _stoneDestroyedEventArgs;
    private int _stonesSizesForProgressbar;
    private List<Stone> _stonesOnScene;
    private float _freezingTimer;
    private bool _freezeState;

    public event Action<StoneDestroyedEventArgs> OnStoneDestroyed;
    public event Action OnAllStonesBroken;

    public int StonesSizesForProgressBar => _stonesSizesForProgressbar;

    private void Start()
    {
        int damagePerSecond = (int)(((_gamePlaySettings.Damage * _gamePlaySettings.ProjectileAmount) * (1 / _gamePlaySettings.FireRate)) / 4);

        _stonesMaxHitPoints = damagePerSecond * _gameMgr.LevelNumber;
        _stonesMinHitPoints = (int)(_stonesMaxHitPoints * _minHitPointsPercentage);

        _stoneDestroyedEventArgs = new StoneDestroyedEventArgs();

        _timer = _spawnRate;
        _stonesOnScene = new List<Stone>();

        CreateStonesCharacteristics();
        CalculateStonesSizesForProgressBar();
    }

    private void CreateStonesCharacteristics()
    {
        _stoneSizes = new int[_stonesAmount];

        for (int i = 0; i < _stonesAmount; i++)
        {
            int stoneSize = UnityEngine.Random.Range(2, 5);
            _stoneSizes[i] = stoneSize;
        }
    }

    private void CalculateStonesSizesForProgressBar()
    {
        foreach (int stoneSize in _stoneSizes)
        {
            _stonesSizesForProgressbar += stoneSize + CalculateStoneSizeRecursive(stoneSize - 1);            
        }
    }

    private int CalculateStoneSizeRecursive(int parentStoneSize)
    {
        if (parentStoneSize <= 0)
            return 0;

        int stoneSize = 0;

        for (int i = 1; i <= 2; i++) // because of we divide parent stone for two child stones
            stoneSize += parentStoneSize + CalculateStoneSizeRecursive(parentStoneSize - 1);

        return stoneSize;
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
            stone.SetFreezeState(_freezeState);
            _stonesOnScene.Add(stone);
        }
    }

    private void SpawnStone()
    {
        int currentStoneSize = _stoneSizes[_amountSpawned];

        int maxHitPoints = UnityEngine.Random.Range(_stonesMinHitPoints, _stonesMaxHitPoints + 1);

        int decreasePercent = 80 - (currentStoneSize * 20);
        
        maxHitPoints = maxHitPoints - (maxHitPoints * decreasePercent) / 100;

        Stone stone = _spawner.SpawnStone(currentStoneSize, maxHitPoints);
        SubscribeToStoneEvents(stone);

        _amountSpawned++;
        stone.SetFreezeState(_freezeState);
        _stonesOnScene.Add(stone);
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

        if (stone.DestroyingMode)
            return;

        if (stone.Size != Stone.StoneSize.Small)
            SpawnChildStones(stone.Size, stone.MaxHitPoints, stone.transform.position);

        _stoneDestroyedEventArgs.StonePosition.x = stone.transform.position.x;
        _stoneDestroyedEventArgs.StonePosition.y = stone.transform.position.y;
        _stoneDestroyedEventArgs.StoneSize = (int)stone.Size;

        // We use it because 2-3 projectiles create collisions with very small gap on time. 
        // A stone is on the way of destroying, but it's still alive. It makes mistakes with calculates of points for the progressbar.
        stone.SetDestroyingMode(); 
        Destroy(stone.gameObject);
        _stonesOnScene.Remove(stone);

        OnStoneDestroyed?.Invoke(_stoneDestroyedEventArgs);

        if (_amountSpawned == _stonesAmount && _stonesOnScene.Count == 0)         
            OnAllStonesBroken?.Invoke();
    }

    public void FreezeStones()
    {
        _freezeState = true;
        SetFreezeStateForStones();
    }

    public void UnFreezeStones()
    {
        _freezeState = false;
        SetFreezeStateForStones();
    }

    private void SetFreezeStateForStones()
    {
        foreach (Stone stone in _stonesOnScene)
            stone.SetFreezeState(_freezeState);
    }
}
