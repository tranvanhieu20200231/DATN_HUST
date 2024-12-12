using UnityEngine;

public class LevelUI : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }
}
