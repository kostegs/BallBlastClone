using System;
using UnityEngine;

public class UIImprovingForm : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiObjectsForHiding;
    [SerializeField] private GameObject _improveCharacteristicsForm;
    
    public event Action OnCloseImprovingForm;

    public void ShowForm()
    {
        foreach (GameObject uiObject in _uiObjectsForHiding) 
        {
            uiObject.SetActive(false);
        }

        _improveCharacteristicsForm.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseForm()
    {
        OnCloseImprovingForm?.Invoke();
        Time.timeScale = 1f;
    }
    
}
