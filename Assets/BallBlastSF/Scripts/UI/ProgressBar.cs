using TMPro;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private StonesManager _stonesManager;

    private int _sizeOfAllStones;
    private int _amount;

    private void Start()
    {
        _stonesManager.OnStoneDestroyed += OnStoneDestroyedHandler;
    }

    public void OnStoneDestroyedHandler(StoneDestroyedEventArgs eventArgs)
    {
        if (_sizeOfAllStones == 0)
            _sizeOfAllStones = _stonesManager.StonesSizesForProgressBar;

        _amount += eventArgs.StoneSize;

        _text.text = $"{_amount.ToString()} from {_sizeOfAllStones}";
    }
}
