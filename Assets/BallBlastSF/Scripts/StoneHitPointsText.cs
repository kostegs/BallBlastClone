using UnityEngine;
using TMPro;

[RequireComponent(typeof(Destructable))]
public class StoneHitPointsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hitPointText;

    private Destructable _destructable;

    private void Awake()
    {
        _destructable = GetComponent<Destructable>();
        _destructable.HitPointsChanged.AddListener(OnChangeHitPoints);
    }

    private void OnDestroy()
    {
        _destructable.HitPointsChanged.RemoveListener(OnChangeHitPoints);
    }

    private void OnChangeHitPoints()
    {
        int hitPoints = _destructable.GetHitPoints();
        Debug.Log($"hitPoints {hitPoints}");

        if (hitPoints >= 1000)
            _hitPointText.text = hitPoints / 1000 + "K";
        else
            _hitPointText.text = hitPoints.ToString();
    }
}
