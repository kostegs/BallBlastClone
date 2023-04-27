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
        _stone.OnHitPointsChanged += OnChangeHitPoints;        
    }

    private void OnChangeHitPoints(object stone, EventArgs e)
    {
        int hitPoints = _stone.HitPoints;        
        
        if (hitPoints >= 1000)         
            _hitPointText.text = hitPoints / 1000 + "K";
        else
            _hitPointText.text = hitPoints.ToString();
    }
}
