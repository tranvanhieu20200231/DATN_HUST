using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstructions : MonoBehaviour
{
    [SerializeField] private GameObject �ntructionUI;

    private PlayerInput playerInput;
    private PlayerInput �ntructionInput;

    private GameObject �ntructionEquip;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        �ntructionInput = �ntructionUI.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Instructions"))
        {
            �ntructionEquip = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Instructions"))
        {
            �ntructionEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && �ntructionEquip != null)
        {
            playerInput.enabled = false;
            �ntructionInput.enabled = true;
            �ntructionUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }
    }
}
