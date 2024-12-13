using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Stats stats;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthFill;

    [SerializeField] private GameObject youDiePopup;

    private void OnEnable()
    {
        stats.Health.OnCurrentValueZero += HandleHealthZero;
    }

    private void OnDisable()
    {
        stats.Health.OnCurrentValueZero -= HandleHealthZero;
    }

    private void Update()
    {
        UpdateHealthText();
        UpdateHealthFill();
    }

    private void UpdateHealthText()
    {
        healthText.text = $"{(int)stats.Health.CurrentValue}/{(int)stats.Health.MaxValue}";
    }

    private void UpdateHealthFill()
    {
        float fillAmount = stats.Health.CurrentValue / stats.Health.MaxValue;
        healthFill.fillAmount = fillAmount;
    }

    private void HandleHealthZero()
    {
        youDiePopup.SetActive(true);
        GameManager.isNewGame = true;
        SaveLoadGame.DeleteDataDie();
    }
}
