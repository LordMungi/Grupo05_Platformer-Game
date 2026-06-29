using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Image FuelBarCanvas;

    [Header("Listener Events")]
    [SerializeField] FloatEventChannel FuelBarUpdateEvent;

    private void OnEnable()
    {
        FuelBarUpdateEvent.OnEventTriggered += UpdateFuelBar;
    }

    private void OnDisable()
    {
        FuelBarUpdateEvent.OnEventTriggered -= UpdateFuelBar;
    }

    private void UpdateFuelBar(float n)
    {
        FuelBarCanvas.fillAmount = Mathf.Clamp(n, 0, 1);
    }
}
