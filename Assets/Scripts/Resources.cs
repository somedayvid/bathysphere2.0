using System.Collections.Generic;
using UnityEngine;
public enum RType
{
    Oxygen,
    Nitrogen,
    Hydrogen,
    Carbon,
}

public class Resources : MonoBehaviour
{
    public Transform hudTankParentTrans;
    public List<ResourceTank> resourceList;

    private List<Transform> hudTankList;

    private void Start()
    {
        resourceList = new List<ResourceTank>() { };

        hudTankList = new List<Transform>();

        foreach (Transform child in hudTankParentTrans)
        {
            hudTankList.Add(child);
        }

        string[] ResourceTypeList = System.Enum.GetNames(typeof(RType));

        GameObject obj1 = new GameObject("Air");
        ResourceTank res1 = obj1.AddComponent<ResourceTank>();
        res1.Initialize("Air", hudTankList[0].gameObject, LevelManager.Get().BreathingToll);
        resourceList.Add(res1);
        obj1.transform.SetParent(transform, false);

        for (int i = 1; i < ResourceTypeList.Length; i++)
        {
            GameObject obj = new GameObject(ResourceTypeList[i]);
            ResourceTank res = obj.AddComponent<ResourceTank>();
            res.Initialize(ResourceTypeList[i], hudTankList[i].gameObject);
            resourceList.Add(res);
            obj.transform.SetParent(transform, false);
        }

        GameObject obj2 = new GameObject("Energy");
        ResourceTank res2 = obj2.AddComponent<ResourceTank>();
        res2.Initialize("Energy", null, LevelManager.Get().BreathingToll, null, HUDManager.Get().energyDisplay, true);
        resourceList.Add(res2);
        obj2.transform.SetParent(transform, false);
        // foreach (Resources re in resourceList)
        // {
        //     print(re.name);
        // }
    }
}
