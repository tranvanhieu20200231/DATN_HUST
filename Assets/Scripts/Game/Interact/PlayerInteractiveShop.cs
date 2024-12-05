using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractiveShop : MonoBehaviour
{
    [SerializeField] private GameObject shopUI;

    private PlayerInput playerInput;
    private PlayerInput shopInput;

    private GameObject shopEquip;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        shopInput = shopUI.GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            shopEquip = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            shopEquip = null;
        }
    }

    private void Update()
    {
        if (PlayerInputHandler.isChoice && shopEquip != null)
        {
            playerInput.enabled = false;
            shopInput.enabled = true;
            shopUI.SetActive(true);

            PlayerInputHandler.isChoice = false;
        }
    }

    public void ResetShop()
    {
        if (CoinUI.currentCoin >= 50)
        {
            CoinUI.currentCoin -= 50;
            shopEquip.GetComponent<ShopManager>().ResetShopItems();

            shopUI.SetActive(false);
            shopInput.enabled = false;
            playerInput.enabled = true;
        }
    }
}
