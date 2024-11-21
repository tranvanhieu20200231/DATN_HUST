using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingAnim : MonoBehaviour
{
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(scene.buildIndex != 0);
    }

    public void FinishTriggerAnim()
    {
        gameObject.SetActive(false);
    }
}
