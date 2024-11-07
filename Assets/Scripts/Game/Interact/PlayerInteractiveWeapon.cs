using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractiveWeapon : MonoBehaviour
{
    [SerializeField] private Image HealthUI;
    [SerializeField] private Image PrimaryWeaponUI;
    [SerializeField] private Image SecondaryWeaponUI;

    private List<WeaponGenerator> weaponGenerators = new List<WeaponGenerator>();

    private string weaponName;
    private GameObject weaponEquip;
    private Sprite weaponSprite;

    private void Start()
    {
        weaponGenerators.AddRange(GetComponentsInChildren<WeaponGenerator>());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            weaponName = other.gameObject.name;
            weaponEquip = other.gameObject;
            weaponSprite = weaponEquip.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            weaponName = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && !string.IsNullOrEmpty(weaponName))
        {
            foreach (var generator in weaponGenerators)
            {
                if (generator.name.Contains("Primary"))
                {
                    generator.DropItem(weaponEquip.transform);
                    generator.GenerateWeaponByName(weaponName);

                    PrimaryWeaponUI.sprite = weaponSprite;
                    Destroy(weaponEquip);
                    break;
                }
                else if (generator.name.Contains("Secondary"))
                {
                    generator.DropItem(weaponEquip.transform);
                    generator.GenerateWeaponByName(weaponName);

                    SecondaryWeaponUI.sprite = weaponSprite;
                    Destroy(weaponEquip);
                    break;
                }
            }

            PlayerInputHandler.isChoice = false;
        }
    }
}

