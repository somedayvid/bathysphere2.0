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

    //for use in getting all transforms for tanks
    public Transform hudTankParentTrans;

    public static HUDManager Get()
    {
        return instance;
    }

    private void Awake() {
        instance = this;
        InitializeMainScreen();
        Button_NextScreen();
    }

    public void UpdateCurrency(int newValue)
    {
        currencyDisplay.text = newValue.ToString();
    }

    [Header("Main Screen")]
    private List<GameObject> screensList;
    public TextMeshProUGUI screenName;
    private int curScreenIndex = -1;

    public Transform mainPanelTrans;
    private void InitializeMainScreen()
    {
        screensList = new List<GameObject>();
        foreach(Transform child in mainPanelTrans)
        {
            screensList.Add(child.gameObject);
        }
    }

    /// <summary>
    /// Advances to next screen in main screen's list
    /// </summary>
    public void Button_NextScreen()
    {
        if (curScreenIndex >= 0) { screensList[curScreenIndex]?.SetActive(false); }
        curScreenIndex++;
        if (curScreenIndex >= screensList.Count) { curScreenIndex = 0; } 
        screensList[curScreenIndex].SetActive(true);
        screenName.text = screensList[curScreenIndex].name.Substring(6);
    }

    //for main screen in middle
    public TextMeshProUGUI OnOffDisplay_Collections;
    // /private bool collectionsActive = false;

    public void Button_Toggle_Collections()
    {
        OnOffDisplay_Collections.text = CollectionsFunction.Get().ToggleCollections().ToString().ToUpper();
    }

    public TextMeshProUGUI OnOffDisplay_MainTool;
    public TextMeshProUGUI currentTool;

    public void Button_Toggle_Tool()
    {
        //OnOffDisplay_MainTool = ToolsFunction.Get().ToggleTool().ToString().ToUpper();
    }

    public void Button_NextTool()
    {
        ToolsFunction.Get().NextTool();
    }

    public void OnPointerDown() => ToolsFunction.Get().isHolding = true;

    public void OnPointerUp() => ToolsFunction.Get().isHolding = false;
}
