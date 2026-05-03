using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using NUnit.Framework;

public class TankDisplayManager : MonoBehaviour
{
    private static TankDisplayManager instance;
    public static TankDisplayManager Get()
    {
        return instance;
    }

    public Transform hudTankParentTrans;

    private List<Transform> hudTankList;

    private Dictionary<string, TankDisplay> stringTankDDict;

    private void Awake() {
        instance = this;
    }
    
    public void Initialize()
    {
        hudTankList = new List<Transform>();
        stringTankDDict = new Dictionary<string, TankDisplay>();
        
        /*an object that contains the 

        */
        foreach (Transform child in hudTankParentTrans)
        {
            hudTankList.Add(child);
        }

        int iterator = 0;
        AdjustableValuesBank bankObj = AdjustableValuesBank.Get();

        foreach(string key in bankObj.StringTankPair.Keys)
        {
            Transform temp = hudTankList[iterator];
            stringTankDDict.Add(key, new TankDisplay(temp.GetChild(0).GetComponent<TextMeshProUGUI>()));
            iterator++;
        }
    }

    public TankDisplay GetTankDisplay(string name)
    {
        return stringTankDDict[name];
    }
}

public class TankDisplay
{
    private TextMeshProUGUI textObj;
    public TankDisplay(TextMeshProUGUI textObj)
    {
        this.textObj = textObj;
    }

    public void UpdateTankDisplay(string name, float newVol, float maxVol)
    {
        textObj.text = name + ": " + newVol.ToString() + "/" + maxVol.ToString();
    }
}
