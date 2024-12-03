using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> shopItems;

    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    [SerializeField] private Transform position3;

    public static int indexWeapon_1 = 0;
    public static int indexWeapon_2 = 1;
    public static int indexWeapon_3 = 2;

    private void Start()
    {
        InitializeShop();
    }

    private void InitializeShop()
    {
        if (shopItems.Count < 3)
            return;

        InstantiateSpecificItem(indexWeapon_1, position1);
        InstantiateSpecificItem(indexWeapon_2, position2);
        InstantiateSpecificItem(indexWeapon_3, position3);
    }

    public void ResetShopItems()
    {
        List<int> randomIndexes = GetUniqueRandomIndexes(3);

        if (randomIndexes.Count != 3)
            return;

        indexWeapon_1 = randomIndexes[0];
        indexWeapon_2 = randomIndexes[1];
        indexWeapon_3 = randomIndexes[2];

        InstantiateSpecificItem(indexWeapon_1, position1);
        InstantiateSpecificItem(indexWeapon_2, position2);
        InstantiateSpecificItem(indexWeapon_3, position3);
    }

    private List<int> GetUniqueRandomIndexes(int count)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < shopItems.Count; i++)
        {
            indexes.Add(i);
        }

        System.Random random = new System.Random();

        for (int i = 0; i < indexes.Count; i++)
        {
            int randomIndex = random.Next(i, indexes.Count);

            int temp = indexes[i];
            indexes[i] = indexes[randomIndex];
            indexes[randomIndex] = temp;
        }

        return indexes.GetRange(0, count);
    }

    private void InstantiateSpecificItem(int index, Transform position)
    {
        if (index < 0 || index >= shopItems.Count)
            return;

        GameObject item = shopItems[index];

        if (position.childCount > 0)
        {
            Destroy(position.GetChild(0).gameObject);
        }

        GameObject gameObjectInstance = Instantiate(item, position.position, position.rotation, position);
        gameObjectInstance.name = item.name;
    }
}
