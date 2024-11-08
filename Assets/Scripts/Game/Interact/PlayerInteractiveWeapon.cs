using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractiveWeapon : MonoBehaviour
{
    [SerializeField] private Image HealthUI;
    [SerializeField] private Image PrimaryWeaponUI;
    [SerializeField] private Image SecondaryWeaponUI;

    [SerializeField] private GameObject weaponSwapUI;

    private WeaponGeneratorPrimary weaponGeneratorPrimary;
    private WeaponGeneratorSecondary weaponGeneratorSecondary;

    private PlayerInput playerInput;
    private PlayerInput weaponSwapPlayerInput;

    private string weaponName;
    private GameObject weaponEquip;
    private Sprite weaponSprite;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        weaponGeneratorPrimary = GetComponentInChildren<WeaponGeneratorPrimary>();
        weaponGeneratorSecondary = GetComponentInChildren<WeaponGeneratorSecondary>();

        weaponSwapPlayerInput = weaponSwapUI.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            weaponName = other.gameObject.name;
            weaponEquip = other.gameObject;
            weaponSprite = weaponEquip.GetComponent<SpriteRenderer>()?.sprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == weaponEquip)
        {
            weaponName = null;
            weaponEquip = null;
            weaponSprite = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && !string.IsNullOrEmpty(weaponName))
        {
            playerInput.enabled = false;
            weaponSwapPlayerInput.enabled = true;
            weaponSwapUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }
    }

    public void AddWeaponPrimary()
    {
        weaponGeneratorPrimary.GenerateWeaponPrimaryByName(weaponName, weaponEquip.transform);

        PrimaryWeaponUI.sprite = weaponSprite;
        Destroy(weaponEquip);

        weaponSwapUI.SetActive(false);
        weaponSwapUI.GetComponent<PlayerInput>().enabled = false;
        playerInput.enabled = true;
    }

    public void AddWeaponSecondary()
    {
        weaponGeneratorSecondary.GenerateWeaponSecondaryByName(weaponName, weaponEquip.transform);

        SecondaryWeaponUI.sprite = weaponSprite;
        Destroy(weaponEquip);

        weaponSwapUI.SetActive(false);
        weaponSwapUI.GetComponent<PlayerInput>().enabled = false;
        playerInput.enabled = true;
    }
}

