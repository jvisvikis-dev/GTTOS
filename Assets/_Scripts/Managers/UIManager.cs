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
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject controlTexts;

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

    public void OpenControls(string action)
    {
        controlTexts.SetActive(true);
        action1Text.text = action;
    }

    public void CloseControls()
    {
        controlTexts.SetActive(false);
    }
}
