using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckInteractive : MonoBehaviour
{
    [System.Serializable]
    public class InteractiveObject
    {
        public string objectName;
        public GameObject targetObject;
    }

    public List<InteractiveObject> interactiveObjects = new List<InteractiveObject>();

    [ContextMenu("Assign Object Names")]
    private void AssignObjectNames()
    {
        foreach (var item in interactiveObjects)
        {
            if (item.targetObject != null)
            {
                item.objectName = item.targetObject.name;
                item.targetObject.SetActive(false);
            }
        }
    }

    public void ShowObject(string objectName)
    {
        var obj = interactiveObjects.Find(item => item.objectName == objectName);
        if (obj != null && obj.targetObject != null)
        {
            obj.targetObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object with name " + objectName + " not found or targetObject is null.");
        }
    }

    public void HideObject(string objectName)
    {
        var obj = interactiveObjects.Find(item => item.objectName == objectName);
        if (obj != null && obj.targetObject != null)
        {
            obj.targetObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Object with name " + objectName + " not found or targetObject is null.");
        }
    }
}
