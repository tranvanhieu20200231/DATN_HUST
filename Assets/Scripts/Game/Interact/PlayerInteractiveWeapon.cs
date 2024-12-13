using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInteractiveWeapon : MonoBehaviour
{
    [SerializeField] private Image HealthUI;
    [SerializeField] private Image PrimaryWeaponUI;
    [SerializeField] private Image SecondaryWeaponUI;

    [SerializeField] private GameObject weaponSwapUI;

    [SerializeField] private List<GameObject> weaponObjects;

    private WeaponGeneratorPrimary weaponGeneratorPrimary;
    private WeaponGeneratorSecondary weaponGeneratorSecondary;

    private string lastPrimaryWeaponName;
    private string lastSecondaryWeaponName;

    private PlayerInput playerInput;
    private PlayerInput weaponSwapPlayerInput;

    private string weaponName;
    private GameObject weaponEquip;
    private Sprite weaponSprite;

    public static bool isResetUIWeapon = false;

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

        if (GameManager.isNewGame)
        {
            PlayerInteractiveTeleport.nextLevelIndex = 1;
            PlayerInteractiveRecovery.currentUpgradeCost = 50;

            ShopManager.indexWeapon_1 = 0;
            ShopManager.indexWeapon_2 = 1;
            ShopManager.indexWeapon_3 = 2;

            GameManager.isNewGame = false;
        }

        LoadWeaponUI();
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

    private void LoadWeaponUI()
    {
        if (lastPrimaryWeaponName != WeaponGenerator.currentPrimaryWeaponName)
        {
            GameObject primaryWeapon = weaponObjects.Find(obj => obj.name == WeaponGenerator.currentPrimaryWeaponName);
            if (primaryWeapon != null)
            {
                PrimaryWeaponUI.sprite = primaryWeapon.GetComponent<SpriteRenderer>()?.sprite;
            }
            lastPrimaryWeaponName = WeaponGenerator.currentPrimaryWeaponName;
        }

        if (lastSecondaryWeaponName != WeaponGenerator.currentSecondaryWeaponName)
        {
            GameObject secondaryWeapon = weaponObjects.Find(obj => obj.name == WeaponGenerator.currentSecondaryWeaponName);
            if (secondaryWeapon != null)
            {
                SecondaryWeaponUI.sprite = secondaryWeapon.GetComponent<SpriteRenderer>()?.sprite;
            }
            lastSecondaryWeaponName = WeaponGenerator.currentSecondaryWeaponName;
        }
    }

}
