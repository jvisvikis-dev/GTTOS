using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;
    [SerializeField] private TextMeshProUGUI ResultText;
    [SerializeField] private TextMeshProUGUI AdviceText;
    [SerializeField] private GameObject EndPanel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        EndPanel.gameObject.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenEndGamePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EndPanel.SetActive(true);
    }

    public void SetResultText(string text)
    {
        ResultText.text = text;
    }

    public void SetAdviceText(string text)
    {
        AdviceText.text = text;
    }
}
