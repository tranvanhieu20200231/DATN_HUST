using UnityEngine;

public class CheckInteractive : MonoBehaviour
{
    public GameObject buttonChoiceImage;
    private bool isPlayerNearby = false;

    private void Start()
    {
        if (buttonChoiceImage != null)
            buttonChoiceImage.SetActive(isPlayerNearby);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (buttonChoiceImage != null)
                buttonChoiceImage.SetActive(isPlayerNearby);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (buttonChoiceImage != null)
                buttonChoiceImage.SetActive(isPlayerNearby);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && PlayerInputHandler.isChoice)
        {
            InteractWithNPC();

            PlayerInputHandler.isChoice = false;
        }
    }

    public virtual void InteractWithNPC()
    {
        Debug.Log("Tương tác với NPC!");
    }
}
