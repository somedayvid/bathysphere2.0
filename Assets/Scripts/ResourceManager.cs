using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public enum ResourceType
{
    Oxygen,
    Nitrogen,
    Hydrogen,
    Carbon,
}

public enum Essentials
{
    Air,
    Nutrients,
    Water,
    Energy,
}

public class ResourceManager : MonoBehaviour
{
    public Transform hudTankParentTrans;
    public List<Resources> resourceList;

    private List<Transform> hudTankList;

    private void Start()
    {
        resourceList = new List<Resources>() { };

        hudTankList = new List<Transform>();

        foreach(Transform child in hudTankParentTrans)
        {
            hudTankList.Add(child);
        }

        string[] ResourceTypeList = System.Enum.GetNames (typeof(ResourceType));


        GameObject obj1 = new GameObject("Air");
        Resources res1 = obj1.AddComponent<Resources>();
        res1.Initialize("Air", hudTankList[0].gameObject, LevelManager.Get().BreathingToll);
        resourceList.Add(res1);
        obj1.transform.SetParent(transform, false);


        for (int i = 1; i < ResourceTypeList.Length; i++)
        {
            GameObject obj = new GameObject(ResourceTypeList[i]);
            Resources res = obj.AddComponent<Resources>();
            res.Initialize(ResourceTypeList[i], hudTankList[i].gameObject);
            resourceList.Add(res);
            obj.transform.SetParent(transform, false);
        }

        // foreach (Resources re in resourceList)
        // {
        //     print(re.name);
        // }
    }

}

public class Resources : MonoBehaviour
{
    public float maxVol;
    public float currentVol;
    new public string name;
    public float toll;
    public float timeBetweenConsumption;
    //a marker that indicates the type of emergency that triggers when a certain amount of thing cannot be subtracted from

    private GameObject tankHud;

    private TextMeshProUGUI volDisplayText;
    private RectTransform maxVolRectTransform;
    private RectTransform curVolRect;

    private Func<float> tollFunction;
    private Func<float> timeFunction;
    /*TODO
    smooth out motion of taking volume
    sound effect too    
    link toll to individual amounts bc some liquids have different calculations for toll than others
    so for breathing, it happens over a number of breaths in a minute, the breathing rate should scale with movespeed and intensity
    but also lower breath toll for when it does, so shallower, quicker breaths basically, kind of like an inverse equation thing
    */

    /// <summary>
    /// Initialize Eric's cat resource manager
    /// </summary>
    /// <param name="name"></param>
    /// <param name="tankHud"></param>
    /// <param name="tollFunction"></param>
    public void Initialize(string name, GameObject tankHud, Func<float> tollFunction = null, Func<float> timeFunction = null)
    {
        this.name = name;
        this.tankHud = tankHud;
        this.tollFunction = tollFunction;
        this.timeFunction = timeFunction;
        maxVol = 100f;
        currentVol = 50f;
        timeBetweenConsumption = 4f;
        toll = 2f;

        volDisplayText = this.tankHud.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Transform maxVolTrans = this.tankHud.transform.GetChild(2).transform.GetChild(0);
        maxVolRectTransform = maxVolTrans.GetComponent<RectTransform>();

        volDisplayText.text = currentVol.ToString();
        curVolRect = maxVolTrans.transform.GetChild(0).GetComponent<RectTransform>();
        tankHud.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = this.name;

        UpdateHUDTank();

        StartCoroutine("Consume");
    }

    public float GetVolRatio()
    {
        return currentVol/maxVol;
    }


    private void UpdateHUDTank()
    {
        volDisplayText.text = currentVol.ToString();
        curVolRect.sizeDelta = new Vector2(maxVolRectTransform.rect.width * GetVolRatio(), curVolRect.sizeDelta.y);
    }

    public IEnumerator Consume()
    {
        float timeTemp = timeBetweenConsumption;
        if(timeFunction != null)
        {
            timeTemp = timeFunction();
        }

        yield return new WaitForSeconds(timeBetweenConsumption);

        float tollTemp = toll;
        if(tollFunction!= null){
            tollTemp = tollFunction();
        }

        if (CanSubtractResources(tollTemp))
        {
            StartCoroutine("Consume");
            UpdateHUDTank();
        }
        else print("yo there's an issue");
        print(currentVol);
    }

    public bool CanSubtractResources(float amt)
    {
        if(amt <= currentVol)
        {
            currentVol -= amt;
            return true;
        }
        else return false;
    }
}