using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour 
{
    private List<RoomTypes> thingsInRoomList;
    private Color baseColor;
    private bool playerOccupied = false;
    public List<Enemy> enemies;
    public Room[] adjRooms = {null};
    private Vector2 pos;
    public Vector2 Pos { get { return pos; } }

    public Color Base{ get { return baseColor; } }
    public bool Occupied {
        get { return playerOccupied; }
        set { playerOccupied = value; }
    }

    // public Room(List<RoomTypes> thingsInRoomList)
    // {
    // }

    public void Initialize(List<RoomTypes> thingsInRoomList, Vector2 pos, Color col)
    {
        baseColor = col;
        this.thingsInRoomList = thingsInRoomList;
        this.pos = pos;
    }

    public void PrintOutRoomType()
    {
        foreach(RoomTypes type in thingsInRoomList)
        {
            Debug.Log(type.ToString());
        }
    }
}