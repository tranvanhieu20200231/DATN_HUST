using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<WeaponDataPair> weaponDataPairs;

    public static string currentPrimaryWeaponName;
    public static string currentSecondaryWeaponName;

    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
    private List<Type> componentDependencies = new List<Type>();

    private Animator anim;

    public void GenerateWeaponPrimaryByName(string newWeaponName, Transform dropPos)
    {
        DropItem(currentPrimaryWeaponName, dropPos);
        GenerateWeaponByName(newWeaponName, true, false);
    }

    public void GenerateWeaponSecondaryByName(string newWeaponName, Transform dropPos)
    {
        DropItem(currentSecondaryWeaponName, dropPos);
        GenerateWeaponByName(newWeaponName, false, false);
    }

    protected void GenerateWeaponByName(string newWeaponName, bool isPrimary, bool isWeaponStart)
    {
        anim = GetComponentInChildren<Animator>();

        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == newWeaponName)?.data;

        if (data != null)
        {
            GenerateWeapon(data);

            if (!isWeaponStart)
            {
                if (isPrimary)
                {
                    currentPrimaryWeaponName = newWeaponName;
                }
                else
                {
                    currentSecondaryWeaponName = newWeaponName;
                }
            }
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

    private void DropItem(string currentWeaponName, Transform dropParent)
    {
        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == currentWeaponName)?.data;

        if (data != null && data.ItemDrop != null)
        {
            GameObject itemDrop = data.ItemDrop;

            GameObject itemInstance = Instantiate(itemDrop, dropParent.position, Quaternion.identity, dropParent.parent);
            itemInstance.name = itemDrop.name;
        }
        else
        {
            Debug.LogWarning($"No data or drop item weapon name: {currentWeaponName}");
        }
    }
}

[Serializable]
public class WeaponDataPair
{
    public string weaponName;
    public WeaponDataSO data;
}
