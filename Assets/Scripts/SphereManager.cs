using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Rendering;

public enum ArmType
{
    Hammer,
    Drill,
    Saw,
}

public class SphereManager : MonoBehaviour
{
    public bool collectingActive = false;
    public bool toolActive = false;

    private bool mouseDown = false;

    private ArmType activeArmType = ArmType.Drill;

    private static SphereManager instance;

    public static SphereManager Get()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActiveArm()
    {

    }

    public void SwapNextTool()
    {
        activeArmType += 1;
        if(activeArmType >= ArmType.Saw)
        {

        }
    }
}
