using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private CharacteristicsImprover _characteristicsImprover;

    public int LevelNumber { get; private set; }

    private void Awake()
    {
        LevelNumber = DataStorage.LevelNumber;
        _stonesManager.OnAllStonesBroken += OnAllStonesBrokenHandler;       
    }  

    public void AddLevelNumber() => LevelNumber++;

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void OnDestroy() => DataStorage.FillDataFromGameMgr(this);

    public void OnAllStonesBrokenHandler()
    {
        AddLevelNumber();
        _characteristicsImprover.OnFinishImproving += OnFinishImprovingHandler;
        _characteristicsImprover.ShowImproverForm();
    }
    
    public void OnFinishImprovingHandler()
    {
        Restart();
    }
}
