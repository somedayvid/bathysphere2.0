using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    private int currentAtm = 1; 
    private int finalAtm = 10;
    private int levelsPerAtm;

    private float minGasNeed = 8.0f;
    private float maxGasNeed = 70.0f;

    //private float currentGasNeed = f;

    //1 atm is 34 feet / 10 meters
    //1 atm equates to lets say 8 lpm of breathing gas 2 atm to 16 and 4 to 32 and so on
    //values are for 1 atm
    //8 lpm at rest, 20 at moderate activity, 70 at vigorous

    public int CurrentAtm{
        get { return currentAtm; }
    }

    public static LevelManager Get()
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

    public float BreathingToll()
    {
        //update with formula to calculate for inbetween movement speeds as well for 8-70lpm
        return currentAtm * minGasNeed;
    }
}
