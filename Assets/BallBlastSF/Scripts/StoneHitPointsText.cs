using UnityEngine;
using TMPro;

[RequireComponent(typeof(Stone))]
public class StoneHitPointsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hitPointText;

    private Stone _stone;

    private void Awake()
    {
        _stone = GetComponent<Stone>();
        _stone.HitPointsChanged.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        _stone.HitPointsChanged.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hitPoints = _stone.HitPoints;        
        
        if (hitPoints >= 1000)         
            _hitPointText.text = hitPoints / 1000 + "K";
        else
            _hitPointText.text = hitPoints.ToString();
    }
}
