using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameMgr _gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))        
            _gameManager.SetPause();        
    }
}
