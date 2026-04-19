using UnityEngine;

public class CurrencyManager
{
    public int currency;
    public void AddCurrency()
    {
        currency += 1;
    }
}

public class CurrencyUI : MonoBehaviour
{
    public CurrencyManager currencyManager = new CurrencyManager();

    public void OnButtonClick()
    {
        currencyManager.AddCurrency();
    }
}
