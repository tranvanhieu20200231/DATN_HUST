using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string sceneStart;
    private string mainMenu;

    [SerializeField] private GameObject inputIsKeyboard;
    [SerializeField] private GameObject inputIsGamepad;

    public static GameObject staticInputIsKeyboard;
    public static GameObject staticInputIsGamepad;

    private void Start()
    {
        staticInputIsKeyboard = inputIsKeyboard;
        staticInputIsGamepad = inputIsGamepad;
    }

    public void StartGameWithDelay(string nameSceneStart)
    {
        sceneStart = nameSceneStart;

        Invoke("StartGame", 0.3f);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneStart);
    }

    public void MainMenuWithDelay(string nameMainMenu)
    {
        mainMenu = nameMainMenu;

        Invoke("MainMenu", 0.3f);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGameWithDelay()
    {
        Invoke("QuitGame", 0.3f);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
