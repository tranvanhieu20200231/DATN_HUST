using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInteractiveRecovery : MonoBehaviour
{
    [SerializeField] private Stats stats;

    [SerializeField] private TextMeshProUGUI healthCountText;
    [SerializeField] private TextMeshProUGUI healthAmountText;

    [SerializeField] private GameObject recoveryUI;

    [SerializeField] private TextMeshProUGUI updateQuantityText;
    [SerializeField] private TextMeshProUGUI updateAmountText;

    [SerializeField] private TextMeshProUGUI updateCoinText;

    private PlayerInput playerInput;
    private PlayerInput recoveryInput;

    private GameObject recoveryEquip;

    public static int maxHealCount = 1;
    private int currentHealCount;
    public static float amountRecovered = 50f;

    private int baseUpgradeCost = 50;
    public static int currentUpgradeCost;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        recoveryInput = recoveryUI.GetComponent<PlayerInput>();
        currentUpgradeCost = baseUpgradeCost;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentHealCount = maxHealCount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Recovery"))
        {
            recoveryEquip = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Recovery"))
        {
            recoveryEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && recoveryEquip != null)
        {
            playerInput.enabled = false;
            recoveryInput.enabled = true;
            recoveryUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }

        UpdateText();

        if (currentHealCount > 0 && PlayerInputHandler.isHealth)
        {
            stats.Health.Increase(amountRecovered);

            currentHealCount--;
            PlayerInputHandler.isHealth = false;
        }
    }

    private void UpdateText()
    {
        healthCountText.text = $"{currentHealCount}";
        healthAmountText.text = $"{amountRecovered}";

        updateQuantityText.text = maxHealCount.ToString() + " --> " + (maxHealCount + 1).ToString();
        updateAmountText.text = ((int)amountRecovered).ToString() + " --> " + ((int)(amountRecovered + 20f)).ToString();

        updateCoinText.text = "Need " + currentUpgradeCost.ToString() + " coins to upgrade";

        if (currentHealCount > maxHealCount)
        {
            currentHealCount = maxHealCount;
        }
    }

    public void UpdateQuantity()
    {
        if (CoinUI.currentCoin >= currentUpgradeCost)
        {
            CoinUI.currentCoin -= currentUpgradeCost;
            maxHealCount++;
            currentHealCount = maxHealCount;

            currentUpgradeCost += baseUpgradeCost;

            recoveryUI.SetActive(false);
            recoveryInput.enabled = false;
            playerInput.enabled = true;
        }
    }

    public void UpdateAmount()
    {
        if (CoinUI.currentCoin >= currentUpgradeCost)
        {
            CoinUI.currentCoin -= currentUpgradeCost;
            amountRecovered = amountRecovered + 20f;

            currentUpgradeCost += baseUpgradeCost;

            recoveryUI.SetActive(false);
            recoveryInput.enabled = false;
            playerInput.enabled = true;
        }
    }
}
