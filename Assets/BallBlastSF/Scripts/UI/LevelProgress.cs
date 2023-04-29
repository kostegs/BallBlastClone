using UnityEngine;
using TMPro;

public class LevelProgress : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _nextLevelText;
    [SerializeField] private GameMgr _gameMgr;
    
    void Start()
    {
        _currentLevelText.text = _gameMgr.LevelNumber.ToString();
        _nextLevelText.text = (_gameMgr.LevelNumber + 1).ToString();
    }    
}
