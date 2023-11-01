using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour,IService
{
    [SerializeField] private GameObject pausePanel;
    private PlayerInput input;

    public void Init()
    {
        input = new PlayerInput();
        input.Enable();
        input.pc.Pause.performed += PauseGame;
    }

    public void PauseGame(InputAction.CallbackContext callbackContext)
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ChangeScene(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
    }

    private void OnApplicationQuit()
    {
        SceneServices.ServiceLocator.Get<SaveAndLoadGame>().Save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
