using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObjectsManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> persistentObjects;

    private static PersistentObjectsManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                DontDestroyOnLoad(obj);
        }

        if (player != null)
            DontDestroyOnLoad(player);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (player != null)
            {
                Destroy(player);
            }

            foreach (GameObject obj in persistentObjects)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }

        Transform teleportPoint = GameObject.Find("TeleportPoint")?.transform;

        if (teleportPoint != null && player != null)
        {
            player.transform.position = teleportPoint.position + new Vector3(0, 2, 0);
        }
        else if (teleportPoint == null)
        {
            Debug.LogWarning("TeleportPoint không được tìm thấy trong scene.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
