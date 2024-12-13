using UnityEngine;

public class TeleportActive : MonoBehaviour
{
    [SerializeField] private GameObject teleportObj;

    private void OnDisable()
    {
        if (teleportObj != null)
        {
            teleportObj.SetActive(true);
        }
    }
}
