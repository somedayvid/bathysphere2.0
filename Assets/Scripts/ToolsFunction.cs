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
        timeLeftTick = cur_timeToMineOnce;
    }

    float base_timeToMineOnce = 2.5f;
    float cur_timeToMineOnce = 2.5f;
    float timeLeftTick;

    // Update is called once per frame
    void Update()
    {
        if (activeToolType == ToolType.Drill && isHolding) {
            Debug.Log("I AM DOING A THING");
            timeLeftTick -= Time.deltaTime;
            if(timeLeftTick <= 0)
            {
                //do thing
                timeLeftTick = cur_timeToMineOnce;
            }
        }
    }

    public void ToggleTool()
    {
        
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
