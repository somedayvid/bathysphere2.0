using UnityEngine;
using TMPro;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance;

    public TextMeshProUGUI currencyDisplay;
    public TextMeshProUGUI energyDisplay;
    public TextMeshProUGUI energyUsageDisplay;
    public TextMeshProUGUI physicalExertionDisplay;//soon to be hr tracker

    public static HUDManager Get()
    {
        return instance;
    }

    private void Awake() {
        instance = this;
        Button_NextScreen();
    }

    public void UpdateCurrency(int newValue)
    {
        currencyDisplay.text = newValue.ToString();
    }

    public List<GameObject> screensList;
    private int curScreenIndex = -1;

    public void Button_NextScreen()
    {
        if (curScreenIndex >= 0) { screensList[curScreenIndex]?.SetActive(false); }
        curScreenIndex++;
        if (curScreenIndex >= screensList.Count) { curScreenIndex = 0; } 
        screensList[curScreenIndex].SetActive(true);
    }

    public TextMeshProUGUI OnOffDisplay;
    private bool collectionsActive = false;

    public void Button_Toggle_Collections()
    {
        if (collectionsActive) { collectionsActive = false; }
        else {  collectionsActive = true; }

        OnOffDisplay.text = collectionsActive.ToString().ToUpper();
    }
}
