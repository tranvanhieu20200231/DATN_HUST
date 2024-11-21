using UnityEngine;

public class TeleportPointCheck : MonoBehaviour
{
    [SerializeField] private GameObject StartPoint;
    [SerializeField] private GameObject TeleportPoint;

    public static int telepointStart;

    private void Awake()
    {
        telepointStart = PlayerPrefsUtility.LoadInt("TelepointStart", 0);

        if (telepointStart == 0)
        {
            StartPoint.SetActive(true);
            TeleportPoint.SetActive(false);
        }
        else
        {
            StartPoint.SetActive(false);
            TeleportPoint.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartPoint.SetActive(false);
            TeleportPoint.SetActive(true);

            PlayerPrefsUtility.SaveInt("TelepointStart", 1);
        }
    }
}
