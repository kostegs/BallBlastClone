using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private Stone _stone;
    private int _scores = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStoneDestroyedHandler(int scores)
    {
        _scores += scores;

        Text text = GetComponent<Text>();
        text.text = $"Кол-во очков от разрушенных камней: {_scores}";
    }

}
