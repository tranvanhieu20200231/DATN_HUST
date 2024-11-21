using UnityEngine;

public class CheckInteractive : MonoBehaviour
{
    public string objShowName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.ShowObject(objShowName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.HideObject(objShowName);
        }
    }
}
