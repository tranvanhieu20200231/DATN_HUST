using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> inputIsKeyboard;
    [SerializeField] private List<GameObject> inputIsGamepad;

    public static bool isNewGame = false;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

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

    public void NewGame()
    {
        isNewGame = true;
        SaveLoadGame.DeleteData();
        SaveLoadGame.LoadGame();
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        SaveLoadGame.LoadGame();
        SceneManager.LoadScene(PlayerInteractiveTeleport.currentLevelIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
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
