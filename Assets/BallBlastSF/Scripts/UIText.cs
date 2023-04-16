using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private int _scores = 0;

    public void OnStoneDestroyedHandler(int scores)
    {
        _scores += scores;

        Text text = GetComponent<Text>();
        text.text = $"���-�� ����� �� ����������� ������: {_scores}";
    }

}
