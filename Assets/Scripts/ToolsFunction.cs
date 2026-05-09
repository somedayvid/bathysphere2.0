using UnityEngine;

public enum ToolType
{
    Drill,
    Hammer,
    Saw,
}

public class ToolsFunction : MonoBehaviour
{
    private static ToolsFunction instance;
    public static ToolsFunction Get()
    {
        return instance;
    }

    private AdjustableValuesBank bank;

    private void Awake() {
        instance = this;    
    }

    private ToolType activeToolType = ToolType.Drill;

    void Start()
    {
        bank = AdjustableValuesBank.Get();
        cur_timeToMineOnce = base_timeToMineOnce;
        timeLeftTick = cur_timeToMineOnce;
    }

    float base_timeToMineOnce = 2.5f;
    float cur_timeToMineOnce;
    float timeLeftTick;

    // Update is called once per frame
    void Update()
    {
        switch (activeToolType)
        {
            case ToolType.Drill:
                if (isHolding)
                {
                    timeLeftTick -= Time.deltaTime;
                    if(timeLeftTick <= 0)
                    {
                        
                        timeLeftTick = cur_timeToMineOnce;
                    }
                }
                break;
            case ToolType.Saw:
                break;
            case ToolType.Hammer:
                break;
        }
    }

    public void NextTool()
    {
        activeToolType += 1;
        if(activeToolType >= ToolType.Saw)
        {
            activeToolType = ToolType.Drill;
        }
    }
    
    //drill function

    public bool isHolding = false;
}
