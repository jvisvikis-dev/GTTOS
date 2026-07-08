using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager instance;
    public static MainMenuManager Instance => instance;

    private bool loadMainMenu = true;
    public bool LoadMainMenu => loadMainMenu;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetMenuOnLoad(bool status)
    {
        loadMainMenu = status;
    }
}
