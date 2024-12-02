using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public static int currentCoin = 0;

    [SerializeField] private TextMeshProUGUI coinText;

    private void Update()
    {
        coinText.text = currentCoin.ToString();
    }
}
