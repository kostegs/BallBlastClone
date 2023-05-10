using UnityEngine;

public class UIPause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseWindow;
    
    public void SetPauseState(bool pauseState)
    {
        _pauseWindow.SetActive(pauseState);
    }
}
