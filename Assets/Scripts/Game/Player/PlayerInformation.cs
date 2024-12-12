using TMPro;
using UnityEngine;

public class PlayerInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackBase;
    [SerializeField] private TextMeshProUGUI healthBase;

    private void Update()
    {
        attackBase.text = "Attack Base : " + ((int)(PlayerData.attack)).ToString();
        healthBase.text = "Health Base : " + ((int)(PlayerData.health)).ToString();
    }
}
