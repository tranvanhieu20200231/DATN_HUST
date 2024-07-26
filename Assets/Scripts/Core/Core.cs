using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    private List<CoreComponent> CoreComponent = new List<CoreComponent>();

    private void Awake()
    {

    }

    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponent)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponent.Contains(component))
        {
            CoreComponent.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponent.OfType<T>().FirstOrDefault();

        if (comp)
        {
            return comp;
        }

        comp = GetComponentInChildren<T>();

        if (comp)
        {
            return comp;
        }

        Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");

        return null;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }
}
