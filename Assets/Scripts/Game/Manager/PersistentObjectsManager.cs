using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PersistentObjectsManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> persistentObjects;

    private static PersistentObjectsManager instance;

    private bool objectsDisabledInMenu = false;

    private void Awake()
    {
        ResetGameObject();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            DisablePersistentObjects();
        }
        else
        {
            if (objectsDisabledInMenu)
            {
                EnablePersistentObjects();
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
    }

    private void DisablePersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        if (player != null)
            player.SetActive(false);

        objectsDisabledInMenu = true;
    }

    private void EnablePersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (player != null)
        {
            player.SetActive(true);

            player.GetComponent<PlayerInput>().enabled = true;
        }

        objectsDisabledInMenu = false;
    }

    private void ResetGameObject()
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
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
