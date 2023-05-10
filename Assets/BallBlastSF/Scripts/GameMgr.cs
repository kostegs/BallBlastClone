using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private CharacteristicsImprover _characteristicsImprover;
    [SerializeField] private UIPause _uiPause;
    [SerializeField] private SceneManagement _sceneManagement;
    [SerializeField] private Cart _cart;

    public int LevelNumber { get; private set; }
    public bool DefeatState { get; private set; }

    private bool _pauseState;

    private void Awake()
    {
        LevelNumber = DataStorage.LevelNumber;
        _stonesManager.OnAllStonesBroken += OnAllStonesBrokenHandler;
        _cart.OnStoneCollision += OnDefeat;
    }  

    public void AddLevelNumber() => LevelNumber++;

    public void Restart() => _sceneManagement.RestartLevel();

    private void OnDestroy() => DataStorage.FillDataFromGameMgr(this);

    public void OnAllStonesBrokenHandler()
    {
        AddLevelNumber();
        ShowImproverForm();
    }

    public void OnDefeat()
    {
        DefeatState = true;
        ShowImproverForm();
    }

    public void ShowImproverForm()
    {
        _characteristicsImprover.OnFinishImproving += OnFinishImprovingHandler;
        _characteristicsImprover.ShowImproverForm();
        FreezeGamePlay();
    }
    
    public void FreezeGamePlay() => Time.timeScale = 0f;

    public void UnFreezeGamePlay() => Time.timeScale = 1f;

    public void OnFinishImprovingHandler()
    {
        UnFreezeGamePlay();
        Restart();
    }

    public void SetPause()
    {
        _pauseState = !_pauseState;

        _uiPause.SetPauseState(_pauseState);

        if (_pauseState)
            FreezeGamePlay();
        else
            UnFreezeGamePlay();
    }        
}
