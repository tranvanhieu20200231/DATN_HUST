using Cainos.PixelArtPlatformer_VillageProps;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractiveChest : MonoBehaviour
{
    [SerializeField] private GameObject powerUpdateUI;

    [SerializeField] private TextMeshProUGUI redText;
    [SerializeField] private TextMeshProUGUI greenText;
    [SerializeField] private TextMeshProUGUI yellowText;

    private Stats Stats;

    public static int redPowerCurrent = 0;
    public static int greenPowerCurrent = 0;
    public static int yellowPowerCurrent = 0;

    private PlayerInput playerInput;
    private PlayerInput powerUpdateInput;

    private GameObject chestEquip;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        powerUpdateInput = powerUpdateUI.GetComponent<PlayerInput>();

        Stats = GetComponentInChildren<Stats>();

        UpdateTextPower();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            chestEquip = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            chestEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && chestEquip != null)
        {
            playerInput.enabled = false;
            powerUpdateInput.enabled = true;
            powerUpdateUI.SetActive(true);

            chestEquip?.GetComponent<Chest>().Open();
            chestEquip.GetComponent<Collider2D>().enabled = false;

            PlayerInputHandler.isChoice = false;
        }

        UpdateTextPower();
    }

    private void UpdateTextPower()
    {
        redText.text = $"{redPowerCurrent}";
        greenText.text = $"{greenPowerCurrent}";
        yellowText.text = $"{yellowPowerCurrent}";
    }

    public void AddRedPower()
    {
        redPowerCurrent++;
        UpdatePower(8, 20);
    }

    public void AddGreenPower()
    {
        greenPowerCurrent++;
        UpdatePower(5, 50);
    }

    public void AddYellowPower()
    {
        yellowPowerCurrent++;
        UpdatePower(2, 80);
    }

    private void UpdatePower(float attack, float health)
    {
        PlayerData.attack += attack;
        PlayerData.health += health;

        Stats.Health.MaxValue = PlayerData.health;
        Stats.Health.Increase(health);

        powerUpdateUI.SetActive(false);
        powerUpdateInput.enabled = false;
        playerInput.enabled = true;
    }
}
