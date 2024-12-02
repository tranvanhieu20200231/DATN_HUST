using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Stats stats;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthFill;

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
        GameManager.isNewGame = true;
        SaveLoadGame.DeleteDataDie();
        SaveLoadGame.LoadGame();

        StartCoroutine(HandleNewGame());
    }

    private IEnumerator HandleNewGame()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(1);
        player.SetActive(true);
    }
}
