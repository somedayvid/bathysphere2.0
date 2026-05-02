using System.Collections.Generic;
using UnityEngine;
public enum EType
{
    Air,
    Nutrients,
    Water,
    Energy,
}

public class Essentials : MonoBehaviour
{
    public Transform hudTankParentTrans;
    public List<ResourceTank> resourceList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
