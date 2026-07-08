using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI adviceText;
    [SerializeField] private TextMeshProUGUI interactableText;
    [SerializeField] private TextMeshProUGUI action1Text;
    [SerializeField] private TextMeshProUGUI exitText;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject controlTexts;
    [SerializeField] private GameObject action1Control;
    [SerializeField] private GameObject mainMenuPanel;

    public Action play;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        endPanel.gameObject.SetActive(false);
        interactableText.text = "";
        if (MainMenuManager.Instance.LoadMainMenu)
            OpenMainMenu();
        else
            CloseMainMenu();
        
    }

    public void Retry()
    {
        MainMenuManager.Instance.SetMenuOnLoad(false);
        ReloadScene();
    }

    public void BackToMain()
    {
        MainMenuManager.Instance.SetMenuOnLoad(true);
        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenEndGamePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endPanel.SetActive(true);
    }

    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void SetAdviceText(string text)
    {
        adviceText.text = text;
    }

    public void SetInteractableText(string text)
    {
        interactableText.text = text;
    }

    public void ClearInteractableText()
    {
        interactableText.text = "";
    }

    public void OpenControls(string action = null, bool onlyExit = false)
    {
        controlTexts.SetActive(true);
        if (onlyExit)
        {
            action1Text.gameObject.SetActive(false);
            action1Control.SetActive(false);
            exitText.text = "Drop";
        }
        else
        {
            action1Text.gameObject.SetActive(true);
            action1Control.SetActive(true);
            action1Text.text = action;
            exitText.text = "Exit";
        }
    }

    public void CloseControls()
    {
        controlTexts.SetActive(false);
    }

    private void OpenMainMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mainMenuPanel.SetActive(true);
    }

    private void CloseMainMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainMenuPanel.SetActive(false);
    }

    public void PlayGame()
    {
        CloseMainMenu();
        play?.Invoke();
    }
}
