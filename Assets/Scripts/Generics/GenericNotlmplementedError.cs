using UnityEngine;

public static class GenericNotlmplementedError<T>
{
    public static T TryGet(T value, string name)
    {
        if (value != null)
        {
            return value;
        }

        Debug.LogError(typeof(T) + " not implemented on " + name);
        return default;
    }
}
