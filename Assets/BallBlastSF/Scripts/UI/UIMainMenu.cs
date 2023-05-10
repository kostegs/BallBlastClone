using UnityEngine;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _activeText;
    [SerializeField] private TextMeshProUGUI _notActiveText;

    private void Start()
    {
        bool loadGameActive = SaveLoadSystem.SaveFileExists();

        _activeText.gameObject.SetActive(loadGameActive);
        _notActiveText.gameObject.SetActive(loadGameActive == false);        
    }
}
