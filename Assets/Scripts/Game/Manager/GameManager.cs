using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string sceneStart;
    private string mainMenu;

    [SerializeField] private List<GameObject> inputIsKeyboard;
    [SerializeField] private List<GameObject> inputIsGamepad;

    private void Start()
    {
        SetActiveObjects(inputIsKeyboard, false);
        SetActiveObjects(inputIsGamepad, false);
    }

    private void SetActiveObjects(List<GameObject> objects, bool isActive)
    {
        foreach (var obj in objects)
        {
            if (obj != null)
                obj.SetActive(isActive);
        }
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

    public void ActivateInputKeyboard()
    {
        SetActiveObjects(inputIsKeyboard, true);
        SetActiveObjects(inputIsGamepad, false);
    }

    public void ActivateInputGamepad()
    {
        SetActiveObjects(inputIsKeyboard, false);
        SetActiveObjects(inputIsGamepad, true);
    }
}
