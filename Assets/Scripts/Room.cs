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

    public List<RoomTypes> RoomsTypes { get { return thingsInRoomList; } }

    // public Room(List<RoomTypes> thingsInRoomList)
    // {
    // }

    public void Initialize(List<RoomTypes> thingsInRoomList, Vector2 pos, Color col)
    {
        baseColor = col;
        this.thingsInRoomList = thingsInRoomList;
        this.enemies = new List<Enemy>();
        this.pos = pos;
    }

    public void AddEnemy()
    {
        Enemy enemy = new Enemy();
        enemies.Add(enemy);
    }

    public void PrintOutRoomType()
    {
        string types = "Room Types: ";
        foreach(RoomTypes type in thingsInRoomList)
        {
            types += type.ToString() + ", ";
        }
        types += ",\b Enemy count: " + enemies.Count; 
        Debug.Log(types);
    }
}