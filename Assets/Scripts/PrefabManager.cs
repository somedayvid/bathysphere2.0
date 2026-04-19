using UnityEngine;
using TMPro;
public class PrefabManager : MonoBehaviour
{
    public TextMeshProUGUI currencyDisplay;
    private static PrefabManager instance;

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
