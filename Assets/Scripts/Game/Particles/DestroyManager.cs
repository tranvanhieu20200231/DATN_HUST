using UnityEngine;

public class DestroyManager : MonoBehaviour
{
    public float destroyTime = 5.0f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
