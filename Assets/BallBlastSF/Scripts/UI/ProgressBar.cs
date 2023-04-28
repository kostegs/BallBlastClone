using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _progressBarFilling;
    [SerializeField] private StonesManager _stonesManager;    

    private int _sizeOfAllStones;
    
    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;        
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        if (_sizeOfAllStones == 0)
            _sizeOfAllStones = _stonesManager.StonesSizesForProgressBar;

        float scoreInPercent = (float)eventArgs.StoneSize / _sizeOfAllStones;
        _progressBarFilling.fillAmount += scoreInPercent;
    }
}
