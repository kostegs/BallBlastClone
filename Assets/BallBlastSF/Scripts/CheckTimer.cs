using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTimer : MonoBehaviour
{
    private float _timer;
    private float _timerCommon;
    int counter = 0;

    void Start()
    {
            
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _timerCommon += Time.deltaTime;
        

        if (_timerCommon < 1)
        {
            if (_timer >= 0.142f)
            {
                Debug.Log("shoot");
                _timer = 0;
            }
            //Debug.Log(_timerCommon);
            counter++;
        }

        Debug.Log($"Counter: {counter}");
    }
}
