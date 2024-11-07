using UnityEngine;

public class CheckInteractive : MonoBehaviour
{
    public string objShowName;

    private bool isPlayerNearby;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.ShowObject(objShowName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.HideObject(objShowName);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && PlayerInputHandler.isChoice && objShowName == "ChoiceTutorial" && gameObject.tag != "Weapon")
        {
            Interact();

            PlayerInputHandler.isChoice = false;
        }
    }

    public virtual void Interact()
    {
        Debug.Log("Tương tác với NPC!");
    }
}
