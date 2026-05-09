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
    private Color playerCol = Color.cornsilk;

    private List<Room> spawnerRoomsList;

    private void CreateLevel()
    {   
        spawnerRoomsList = new List<Vector2>();

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
                Vector2 pos = new Vector2(j,i);
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
                        spawnerRoomsList.Add(temp);
                    break;
                    case 3:
                        temp = NewRoom(new List<RoomTypes> {RoomTypes.Hole}, pos, Color.red);
                    break;
                }

                roomsList[i][j] = temp;
            }
        }
    }

    public void NoiseCheck()
    {
        foreach(Room roo in spawnerRoomsList)
        {
            Vector2 tempComparison = roo.Pos - playerPos;
            float tempDistance = Mathf.Abs(tempComparison.x) + Mathf.Abs(tempComparison.y);
            //temporarily placed as 10% when imemdiately next to room
            if(UnityEngine.Random.Range(1, 101) < 10.0f/tempDistance)
            {

            }
        }
    }

    private Room NewRoom(List<RoomTypes> roomTypes, Vector3 pos, Color col)
    {
        Room newTemp = Instantiate(tempRoomSpaceObj).GetComponent<Room>();
        newTemp.transform.position = new Vector3(minimapCornerMarker.position.x + 1.5f * pos.x, minimapCornerMarker.position.y - 1.5f * pos.y);
        
        newTemp.Initialize(roomTypes, pos, col);
        newTemp.transform.SetParent(transform);
        newTemp.gameObject.GetComponent<SpriteRenderer>().color = newTemp.Base;
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
            Room temp1 = roomsList[(int)playerPos.x][(int)playerPos.y];
            temp1.gameObject.GetComponent<SpriteRenderer>().color = temp1.Base;

            playerPos = tempDir;
            Debug.Log("Player is now at " + playerPos.ToString() + " room is of type(s): ");
            
            Room temp = roomsList[(int)tempDir.x][(int)tempDir.y];
            temp.PrintOutRoomType();
            temp.gameObject.GetComponent<SpriteRenderer>().color = playerCol;
;
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

