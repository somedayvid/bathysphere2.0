using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour 
{
    private List<RoomTypes> thingsInRoomList;
    private Color baseColor;
    private Color playerColor;
    public Room[] adjRooms = {null};

    // public Room(List<RoomTypes> thingsInRoomList)
    // {
    // }

    public void SetTypes(List<RoomTypes> thingsInRoomList)
    {
        this.thingsInRoomList = thingsInRoomList;
    }

    public void PrintOutRoomType()
    {
        foreach(RoomTypes type in thingsInRoomList)
        {
            Debug.Log(type.ToString());
        }
    }
}