using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private List<WeaponDataPair> weaponDataPairs;

    protected string currentPrimaryWeaponName;
    protected string currentSecondaryWeaponName;

    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
    private List<Type> componentDependencies = new List<Type>();

    private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        currentPrimaryWeaponName = weaponDataPairs[0].weaponName;
        currentSecondaryWeaponName = weaponDataPairs[0].weaponName;
    }

    public void GenerateWeaponPrimaryByName(string newWeaponName, Transform dropPos)
    {
        DropItem(currentPrimaryWeaponName, dropPos);
        GenerateWeaponByName(newWeaponName, true);
    }

    public void GenerateWeaponSecondaryByName(string newWeaponName, Transform dropPos)
    {
        DropItem(currentSecondaryWeaponName, dropPos);
        GenerateWeaponByName(newWeaponName, false);
    }

    private void GenerateWeaponByName(string newWeaponName, bool isPrimary)
    {
        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == newWeaponName)?.data;

        if (data != null)
        {
            GenerateWeapon(data);

            if (isPrimary)
            {
                currentPrimaryWeaponName = newWeaponName;
            }
            else
            {
                currentSecondaryWeaponName = newWeaponName;
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

    private void DropItem(string currentWeaponName, Transform dropPos)
    {
        WeaponDataSO data = weaponDataPairs
            .FirstOrDefault(pair => pair.weaponName == currentWeaponName)?.data;

        if (data != null && data.ItemDrop != null)
        {
            GameObject itemDrop = data.ItemDrop;

            GameObject itemInstance = Instantiate(itemDrop, dropPos.position, Quaternion.identity);
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
