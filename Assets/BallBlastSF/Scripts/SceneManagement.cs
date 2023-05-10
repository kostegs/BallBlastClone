using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void NewGame() => SceneManager.LoadScene(1);

    public void RestartLevel() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}

