using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currency;
    public void AddCurrency()
    {
        currency += 1;
        PrefabManager.Get().UpdateCurrency(currency);
    }
}