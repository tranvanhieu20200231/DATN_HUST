using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMenuManager : MonoBehaviour
{
    private GameManager GM;

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

    private int currentIndex = 0;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerInput.enabled = false;
        }
    }

    private void Start()
    {
        GM = FindAnyObjectByType<GameManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerInput.enabled = true;
        }

        Initialization();
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

            EventTrigger.Entry highlightEntry = new EventTrigger.Entry();
            highlightEntry.eventID = EventTriggerType.PointerEnter;
            highlightEntry.callback.AddListener((eventData) => OnButtonHighlight(pair));
            trigger.triggers.Add(highlightEntry);

            EventTrigger.Entry exitEntry = new EventTrigger.Entry();
            exitEntry.eventID = EventTriggerType.PointerExit;
            exitEntry.callback.AddListener((eventData) => OnPointerExit(pair));
            trigger.triggers.Add(exitEntry);
        }

        OnButtonHighlight(buttonTextPairs[currentIndex]);
    }

    void OnButtonPress(ButtonTextPair selectedPair)
    {
        foreach (var pair in buttonTextPairs)
        {
            if (pair != selectedPair)
            {
                pair.text.color = normalColor;
            }
        }
    }

    void OnButtonHighlight(ButtonTextPair highlightedPair)
    {
        CheckInput();

        foreach (var pair in buttonTextPairs)
        {
            pair.text.color = pair == highlightedPair ? highlightColor : normalColor;
        }

        currentIndex = buttonTextPairs.IndexOf(highlightedPair);
    }

    void OnPointerExit(ButtonTextPair exitedPair)
    {
    }

    public void OnMoveUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentIndex = (currentIndex - 1 + buttonTextPairs.Count) % buttonTextPairs.Count;
            OnButtonHighlight(buttonTextPairs[currentIndex]);
        }
    }

    public void OnMoveDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentIndex = (currentIndex + 1) % buttonTextPairs.Count;
            OnButtonHighlight(buttonTextPairs[currentIndex]);
        }
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            buttonTextPairs[currentIndex].button.onClick.Invoke();

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
        if (GM != null)
        {
            if (playerInput.currentControlScheme == "Gamepad")
            {
                GM.ActivateInputGamepad();
            }

            if (playerInput.currentControlScheme == "Keyboard")
            {
                GM.ActivateInputKeyboard();
            }
        }
    }

    private void OnEnable()
    {
        OnButtonHighlight(buttonTextPairs[0]);
    }

    public void StopPause()
    {
        Time.timeScale = 1.0f;
    }
}
