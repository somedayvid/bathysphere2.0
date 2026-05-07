using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

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
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Soon to be relocated to adjustablevaluesbank
    /// </summary>
    /// <returns>Amount of air required for breathing</returns>
    public float BreathingToll()
    {
        //update with formula to calculate for inbetween movement speeds as well for 8-70lpm
        return currentAtm * minGasNeed;
    }

    private int[][] rooms;
    private Room[][] roomsList;
    // private int numRooms = 20;

    private Vector2 playerPos = Vector2.zero;

    public GameObject tempRoomSpaceObj;
    public Transform minimapCornerMarker;

    private void CreateLevel()
    {   
        rooms = new int[][]
        {
            new int[] {0,1,1,1,1},
            new int[] {0,2,1,1,1},
            new int[] {1,1,1,1,0},
            new int[] {2,0,0,1,3},
            new int[] {2,0,0,1,3}
        };

        roomsList = new Room[rooms.Length][];

        for (int i = 0; i < rooms.Length; i++)
        {
            roomsList[i] = new Room[rooms[i].Length];
        }

        for (int i = 0; i < rooms.Length; i++)
        {
            for(int j = 0; j < rooms[i].Length; j++)
            {
                Room temp = null;
                Vector3 pos = new Vector3(minimapCornerMarker.position.x + 1.5f * j, minimapCornerMarker.position.y - 1.5f * i);
                switch (rooms[i][j])
                {
                    case 0:
                        temp = NewRoom(new List<RoomTypes> {RoomTypes.Empty}, pos, Color.blue);
                    break;
                    case 1:
                        temp = NewRoom(new List<RoomTypes> {RoomTypes.Ore}, pos, Color.grey);
                    break;
                    case 2:
                        temp = NewRoom(new List<RoomTypes> {RoomTypes.Spawner}, pos, Color.yellow);
                    break;
                    case 3:
                        temp = NewRoom(new List<RoomTypes> {RoomTypes.Empty}, pos, Color.red);
                    break;
                }

                roomsList[i][j] = temp;
            }
        }
    }

    private Room NewRoom(List<RoomTypes> roomTypes, Vector3 pos, Color col)
    {
        Room newTemp = Instantiate(tempRoomSpaceObj).GetComponent<Room>();
        newTemp.SetTypes(roomTypes);
        newTemp.transform.position = pos;
        newTemp.transform.SetParent(transform);
        newTemp.gameObject.GetComponent<SpriteRenderer>().color = col;
        return newTemp;
    }

    public void PlayerMove(Vector2 direction)
    {
        Vector2 tempDir = playerPos + direction;
        if(tempDir.x >= 0 &&
            tempDir.y >= 0 &&
            tempDir.x <= 4 &&
            tempDir.y <= 4)
        {
            playerPos = tempDir;
            Debug.Log("Player is now at " + playerPos.ToString() + " room is of type(s): ");
            roomsList[(int)tempDir.x][(int)tempDir.y].PrintOutRoomType();
            roomsList[(int)tempDir.x][(int)tempDir.y].gameObject.GetComponent<SpriteRenderer>().color = Color.cornsilk;
        }
        else
        {
            Debug.Log("Player could not move there");
        }
    }
}

public enum RoomTypes
{
    Empty,
    Spawner,
    Ore,
    Encounter,
    Hole,
    Company,//shop room
}

public enum EncounterType
{
    
}

