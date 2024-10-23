using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonMenuManager : MonoBehaviour
{
    private PlayerInput playerInput;

    [System.Serializable]
    public class ButtonTextPair
    {
        public Button button;
        public TextMeshProUGUI text;
    }

    public List<ButtonTextPair> buttonTextPairs;

    [SerializeField] private Button backButton;

    private Color normalColor = new Color(1, 1, 1, 100f / 255f);
    private Color highlightColor = new Color(1, 1, 1, 255f / 255f);
    private Color pressedColor = new Color(1, 0, 0, 255f / 255f);

    private int currentIndex = 0; // Dùng để theo dõi button hiện tại

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        Initialization();
    }

    private void Update()
    {
        CheckInput();
    }

    private void Initialization()
    {
        foreach (var pair in buttonTextPairs)
        {
            Button btn = pair.button;
            TextMeshProUGUI txt = pair.text;

            txt.color = normalColor;

            btn.onClick.AddListener(() => OnButtonPress(pair));
            btn.transition = Selectable.Transition.None;

            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();

            // Highlight khi con trỏ chuột di chuyển vào
            EventTrigger.Entry highlightEntry = new EventTrigger.Entry();
            highlightEntry.eventID = EventTriggerType.PointerEnter;
            highlightEntry.callback.AddListener((eventData) => OnButtonHighlight(pair));
            trigger.triggers.Add(highlightEntry);

            // Đừng thay đổi trạng thái khi con trỏ chuột rời khỏi nút
            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((eventData) => OnPointerExit(pair)); // Không thay đổi trạng thái về normal
            trigger.triggers.Add(exitEntry);
        }

        // Khởi tạo button đầu tiên ở trạng thái highlight
        OnButtonHighlight(buttonTextPairs[currentIndex]);
    }

    // Khi nút được nhấn
    void OnButtonPress(ButtonTextPair selectedPair)
    {
        TextMeshProUGUI txt = selectedPair.text;

        txt.color = pressedColor;
        txt.transform.DOScale(1.2f, 0.15f).OnComplete(() =>
        {
            txt.transform.DOScale(1f, 0.15f);
            txt.color = highlightColor;
        });

        // Cập nhật trạng thái cho các nút khác về normal
        foreach (var pair in buttonTextPairs)
        {
            if (pair != selectedPair)
            {
                pair.text.color = normalColor;
            }
        }
    }

    // Khi nút được highlight
    void OnButtonHighlight(ButtonTextPair highlightedPair)
    {
        foreach (var pair in buttonTextPairs)
        {
            pair.text.color = pair == highlightedPair ? highlightColor : normalColor;
        }

        // Cập nhật button hiện tại
        currentIndex = buttonTextPairs.IndexOf(highlightedPair);
    }

    // Không thay đổi trạng thái khi con trỏ rời khỏi nút
    void OnPointerExit(ButtonTextPair exitedPair)
    {
        // Do nothing, giữ trạng thái hiện tại
    }

    // Hàm di chuyển lên button phía trước
    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Di chuyển lên, nếu đang ở button đầu tiên thì quay vòng xuống cuối
            currentIndex = (currentIndex - 1 + buttonTextPairs.Count) % buttonTextPairs.Count;
            OnButtonHighlight(buttonTextPairs[currentIndex]);
        }
    }

    // Hàm di chuyển xuống button phía sau
    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Di chuyển xuống, nếu đang ở button cuối cùng thì quay vòng lên đầu
            currentIndex = (currentIndex + 1) % buttonTextPairs.Count;
            OnButtonHighlight(buttonTextPairs[currentIndex]);
        }
    }

    // Submit sự kiện với gamepad
    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Gọi sự kiện onClick của button đang được highlight
            buttonTextPairs[currentIndex].button.onClick.Invoke();

            // Xử lý hiển thị hiệu ứng nhấn nút
            OnButtonPress(buttonTextPairs[currentIndex]);
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (context.performed && backButton != null)
        {
            backButton.onClick.Invoke();
        }
    }

    private void CheckInput()
    {
        if (GameManager.staticInputIsKeyboard != null && GameManager.staticInputIsGamepad != null)
        {
            if (playerInput.currentControlScheme == "Gamepad")
            {
                GameManager.staticInputIsKeyboard.SetActive(false);
                GameManager.staticInputIsGamepad.SetActive(true);
            }
            else
            {
                GameManager.staticInputIsKeyboard.SetActive(true);
                GameManager.staticInputIsGamepad.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        OnButtonHighlight(buttonTextPairs[0]);
    }
}
