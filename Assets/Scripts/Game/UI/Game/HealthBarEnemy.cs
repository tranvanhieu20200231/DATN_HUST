using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    [SerializeField] private Stats stats;

    [SerializeField] private Slider slider;

    public Color low;
    public Color high;
    public Vector3 offset;

    private void Update()
    {
        if (slider != null)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        slider.gameObject.SetActive(stats.Health.CurrentValue < stats.Health.MaxValue);
        slider.value = stats.Health.CurrentValue;
        slider.maxValue = stats.Health.MaxValue;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }
}
