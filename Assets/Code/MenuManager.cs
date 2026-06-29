using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas creditsCanvas;

    [SerializeField] private GameObject exitButton;

    private void Awake()
    {
#if UNITY_WEBGL
        exitButton.SetActive(false);
#endif
    }
    public void LoadGame()
    {
        ServiceProvider.Instance.GetService<CustomSceneManager>().LoadLevel();
    }

    public void ShowCredits()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        creditsCanvas.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        creditsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
