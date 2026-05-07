using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour 
{
    private List<RoomTypes> thingsInRoomList;
    private Color baseColor;
    private bool playerOccupied = false;
    public Room[] adjRooms = {null};

    public Color Base{ get { return baseColor; } }
    public bool Occupied {
        get { return playerOccupied; }
        set { playerOccupied = value; }
    }

    // public Room(List<RoomTypes> thingsInRoomList)
    // {
    // }

    public void SetTypes(List<RoomTypes> thingsInRoomList)
    {
        this.thingsInRoomList = thingsInRoomList;
    }

    public void AssignBaseColor(Color col)
    {
        baseColor = col;
    }

    public void PrintOutRoomType()
    {
        foreach(RoomTypes type in thingsInRoomList)
        {
            Debug.Log(type.ToString());
        }
    }
}