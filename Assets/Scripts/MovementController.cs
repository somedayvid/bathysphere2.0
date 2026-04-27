using UnityEngine;

public class MovementController : MonoBehaviour
{
    //screen space coordinates plane
    public void GoNorth()
    {
        LevelManager.Get().PlayerMove(new Vector2(-1, 0));
    }

    public void East()
    {
        LevelManager.Get().PlayerMove(new Vector2(0, 1));
    }

    public void South()
    {
        LevelManager.Get().PlayerMove(new Vector2(1, 0));
    }

    public void West()
    {
        LevelManager.Get().PlayerMove(new Vector2(0, -1));
    }
}
