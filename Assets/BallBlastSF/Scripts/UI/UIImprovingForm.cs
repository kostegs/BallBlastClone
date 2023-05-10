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
            uiObject.SetActive(false);        

        _improveCharacteristicsForm.SetActive(true);        
    }

    public void CloseForm() => OnCloseImprovingForm?.Invoke();    
}
