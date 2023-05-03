using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    [SerializeField] private StonesManager _stonesManager;
    [SerializeField] private CoinsManager _coinsManager;
    [SerializeField] private UIImprovingForm _improvingCharacteristicsForm;

    public int LevelNumber { get; private set; }

    private void Awake()
    {
        LevelNumber = DataStorage.LevelNumber;
        _stonesManager.OnAllStonesBroken += OnAllStonesBrokenHandler;
        _improvingCharacteristicsForm.OnCloseImprovingForm += OnClosingImproveCharacteristicsForm;
    }  

    public void AddLevelNumber() => LevelNumber++;

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void OnDestroy() => DataStorage.FillDataFromGameMgr(this);

    public void OnAllStonesBrokenHandler()
    {
        AddLevelNumber();
        _improvingCharacteristicsForm.ShowForm();        
    }

    public void OnClosingImproveCharacteristicsForm()
    {
        Restart();
    }
}
