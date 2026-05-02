using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ResourceTank : MonoBehaviour
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

    private bool doPrint = false;
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
    public void Initialize(string name, GameObject tankHud = null, Func<float> tollFunction = null, Func<float> timeFunction = null, TextMeshProUGUI displayText = null, bool doPrint = false)
    {
        this.name = name;
        this.tankHud = tankHud;
        this.tollFunction = tollFunction;
        this.timeFunction = timeFunction;
        this.doPrint = doPrint;
        maxVol = 100f;
        currentVol = 50f;
        timeBetweenConsumption = 4f;
        toll = 2f;

        if (this.tankHud != null)
        {
            volDisplayText = this.tankHud.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Transform maxVolTrans = this.tankHud.transform.GetChild(2).transform.GetChild(0);
            maxVolRectTransform = maxVolTrans.GetComponent<RectTransform>();

            volDisplayText.text = currentVol.ToString();
            curVolRect = maxVolTrans.transform.GetChild(0).GetComponent<RectTransform>();
            tankHud.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = this.name;        }
        else
        {
            volDisplayText = displayText;
        }

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
        if(tankHud != null)
        {
            curVolRect.sizeDelta = new Vector2(maxVolRectTransform.rect.width * GetVolRatio(), curVolRect.sizeDelta.y);
        }
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

        if (doPrint)
        {
            Debug.Log(currentVol);
        }
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