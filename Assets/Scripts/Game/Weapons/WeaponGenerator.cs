using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<WeaponDataPair> weaponDataPairs;

    private string currentPrimaryWeaponName = "Null";
    private string currentSecondaryWeaponName = "Null";

    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
    private List<Type> componentDependencies = new List<Type>();

    private Animator anim;

    public static bool isPrimaryWeapon;
    public static bool isSecondaryWeapon;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        if (weapon.gameObject.name == "PrimaryWeapon")
        {
            GenerateWeaponByName(currentPrimaryWeaponName);
        }
        if (weapon.gameObject.name == "SecondaryWeapon")
        {
            GenerateWeaponByName(currentSecondaryWeaponName);
        }
    }

    private void Update()
    {
        isPrimaryWeapon = currentPrimaryWeaponName != "Null";
        isSecondaryWeapon = currentSecondaryWeaponName != "Null";
    }

    public void GenerateWeaponByName(string newWeaponName)
    {
        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == newWeaponName)?.data;

        if (data != null)
        {
            GenerateWeapon(data);
            currentPrimaryWeaponName = newWeaponName;
        }
        else
        {
            Debug.LogWarning($"No data weapon name: {newWeaponName}");
        }
    }

    private void GenerateWeapon(WeaponDataSO data)
    {
        weapon.SetData(data);

        componentAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDependencies.Clear();

        componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
        componentDependencies = data.GetAllDependencies();

        foreach (var dependency in componentDependencies)
        {
            if (componentsAddedToWeapon.Any(component => component.GetType() == dependency))
                continue;

            var weaponComponent = componentAlreadyOnWeapon
                .FirstOrDefault(component => component.GetType() == dependency);

            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }

            weaponComponent.Init();
            componentsAddedToWeapon.Add(weaponComponent);
        }

        var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);

        foreach (var weaponComponent in componentsToRemove)
        {
            Destroy(weaponComponent);
        }

        anim.runtimeAnimatorController = data.AnimatorController;
    }

    public void DropItem(Transform dropPos)
    {
        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == currentPrimaryWeaponName)?.data;

        if (data != null && data.ItemDrop != null)
        {
            GameObject itemDrop = data.ItemDrop;

            GameObject itemInstance = Instantiate(itemDrop, dropPos.position, Quaternion.identity);
            itemInstance.name = itemDrop.name;
        }
        else
        {
            Debug.LogWarning($"No data or drop item weapon name: {currentPrimaryWeaponName}");
        }
    }
}

[Serializable]
public class WeaponDataPair
{
    public string weaponName;
    public WeaponDataSO data;
}
