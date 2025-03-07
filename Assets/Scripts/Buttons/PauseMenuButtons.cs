using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public enum ButtonType { Resume, SaveGame, LoadGame, Options, MainMenu, Quit }
    public ButtonType buttonFunction;
    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        switch (buttonFunction)
        {
            case ButtonType.Resume:
                if (PauseMenu.Instance != null) PauseMenu.Instance.ResumeGame();
                break;

            case ButtonType.SaveGame:
                if (PauseMenu.Instance != null) PauseMenu.Instance.SaveGame();
                break;

            case ButtonType.LoadGame:
                if (PauseMenu.Instance != null) PauseMenu.Instance.LoadGame();
                break;

            case ButtonType.Options:
                if (PauseMenu.Instance != null) PauseMenu.Instance.Options();
                break;

            case ButtonType.MainMenu:
                if (PauseMenu.Instance != null) PauseMenu.Instance.MainMenu();
                break;

            case ButtonType.Quit:
                Application.Quit();
                break;

            default:
                Debug.LogWarning("Button function not assigned!");
                break;
        }
    }
}
