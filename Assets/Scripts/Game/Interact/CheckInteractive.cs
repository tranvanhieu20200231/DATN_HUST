using UnityEngine;

public class CheckInteractive : MonoBehaviour
{
    public string objShowName;

    [SerializeField] private GameObject weaponInformation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.ShowObject(objShowName);

            if (gameObject.tag == "Weapon" && weaponInformation != null)
            {
                weaponInformation.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCheckInteractive playerCheckInteractive = other.GetComponentInChildren<PlayerCheckInteractive>();
            playerCheckInteractive.HideObject(objShowName);

            if (gameObject.tag == "Weapon" && weaponInformation != null)
            {
                weaponInformation.SetActive(false);
            }
        }
    }
}
