using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Canvas HUDCanvas;
    [SerializeField] private Image FuelBarCanvas;
    [SerializeField] private Canvas PauseCanvas;
    [SerializeField] private Canvas WinGameCanvas;

    [Header("Listener Events")]
    [SerializeField] FloatEventChannel FuelBarUpdateEvent;
    [SerializeField] private EventChannel PauseGameEvent;
    [SerializeField] private EventChannel UnpauseGameEvent;
    [SerializeField] private EventChannel WinGameEvent;

    private void OnEnable()
    {
        FuelBarUpdateEvent.OnEventTriggered += UpdateFuelBar;
        PauseGameEvent.OnEventTriggered += ShowPauseMenu;
        UnpauseGameEvent.OnEventTriggered += HidePauseMenu;
        WinGameEvent.OnEventTriggered += ShowWinGame;
    }

    private void OnDisable()
    {
        FuelBarUpdateEvent.OnEventTriggered -= UpdateFuelBar;
        PauseGameEvent.OnEventTriggered -= ShowPauseMenu;
        UnpauseGameEvent.OnEventTriggered -= HidePauseMenu;
        WinGameEvent.OnEventTriggered -= ShowWinGame;
    }

    private void ShowPauseMenu()
    {
        HUDCanvas.gameObject.SetActive(false);
        PauseCanvas.gameObject.SetActive(true);
    }

    private void HidePauseMenu()
    {
        PauseCanvas.gameObject.SetActive(false);
        HUDCanvas.gameObject.SetActive(true);
    }

    private void UpdateFuelBar(float n)
    {
        FuelBarCanvas.fillAmount = Mathf.Clamp(n, 0, 1);
    }
        public void ShowWinGame()
    {
        HUDCanvas.gameObject.SetActive(false);
        WinGameCanvas.gameObject.SetActive(true);
    }
}
