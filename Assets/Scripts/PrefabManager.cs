using UnityEngine;
using TMPro;
public class PrefabManager : MonoBehaviour
{
    private static PrefabManager instance;

    public TextMeshProUGUI currencyDisplay;
    public TextMeshProUGUI energyDisplay;
    public TextMeshProUGUI energyUsageDisplay;
    public TextMeshProUGUI physicalExertionDisplay;//soon to be hr tracker

    public static PrefabManager Get()
    {
        return instance;
    }

    private void Awake() {
        instance = this;
    }

    public void UpdateCurrency(int newValue)
    {
        currencyDisplay.text = newValue.ToString();
    }
}
