using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteractiveTeleport : MonoBehaviour
{
    [SerializeField] private GameObject teleportUI;
    [SerializeField] private GameObject winGameUI;

    private PlayerInput playerInput;
    private PlayerInput teleportInput;
    private PlayerInput winGameInput;

    private GameObject teleportEquip;

    public static int currentLevelIndex = 1;
    public static int nextLevelIndex = 2;

    private HashSet<GameObject> triggeredTeleports = new HashSet<GameObject>();

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        teleportInput = teleportUI.GetComponent<PlayerInput>();
        winGameInput = winGameUI.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Teleport"))
        {
            teleportEquip = other.gameObject;

            if (!triggeredTeleports.Contains(other.gameObject))
            {
                triggeredTeleports.Add(other.gameObject);

                if (currentLevelIndex > 1)
                {
                    nextLevelIndex = currentLevelIndex + 1;
                }
            }
        }

        if (other.CompareTag("WinGame"))
        {
            playerInput.enabled = false;
            winGameInput.enabled = true;
            winGameUI.SetActive(true);
        }

        print(nextLevelIndex);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Teleport"))
        {
            teleportEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && teleportEquip != null)
        {
            playerInput.enabled = false;
            teleportInput.enabled = true;
            teleportUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }
    }

    public void ReturnVillage()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene(1);
            currentLevelIndex = 1;
            SaveLoadGame.SaveData();
        }

        teleportUI.SetActive(false);
        teleportInput.enabled = false;
        playerInput.enabled = true;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelIndex);
        currentLevelIndex = nextLevelIndex;

        SaveLoadGame.SaveData();

        teleportUI.SetActive(false);
        teleportInput.enabled = false;
        playerInput.enabled = true;
    }
}
