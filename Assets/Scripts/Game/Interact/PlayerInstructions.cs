using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInstructions : MonoBehaviour
{
    [SerializeField] private GameObject íntructionUI;

    private PlayerInput playerInput;
    private PlayerInput íntructionInput;

    private GameObject íntructionEquip;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        íntructionInput = íntructionUI.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Instructions"))
        {
            íntructionEquip = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Instructions"))
        {
            íntructionEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && íntructionEquip != null)
        {
            playerInput.enabled = false;
            íntructionInput.enabled = true;
            íntructionUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }
    }
}
