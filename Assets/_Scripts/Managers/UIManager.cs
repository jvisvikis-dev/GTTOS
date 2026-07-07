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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        endPanel.gameObject.SetActive(false);
        interactableText.text = "";
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
}
