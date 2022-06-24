using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }
       
    public Direction direction = Direction.Right;
    private GameGrid grid;
    private float speedFactor = 0.0017f;
    private Rails.Type? currentRails;
    Vector2 move;


    private void Awake()
    {
        grid = GameObject.Find("Grid").GetComponent<GameGrid>();
        move = new Vector2(speedFactor, 0);
        direction = Direction.Right;
        currentRails = Rails.Type.Horizontal;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x >= grid.size.x - 1.75f && (int)transform.position.z == grid.level.endStationY)
            grid.level.GameOver(Level.GameResult.Win);

        Rails.Type? type;

        if (direction == Direction.Right || direction == Direction.Top)                   
            type = grid?[(int)(transform.position.x), (int)(transform.position.z)]?.GetComponent<Rails>()?.type;                    
        else
            type = grid?[(int)Mathf.Ceil(transform.position.x - 0.05f), (int)Mathf.Ceil(transform.position.z - 0.05f)]?.GetComponent<Rails>()?.type;
        if (type == null)
            grid.level.GameOver(Level.GameResult.Lost);

   
        if (type != currentRails)
        {
            currentRails = type;
            if ((direction == Direction.Left || direction == Direction.Right) && transform.position.x - (float)(int)transform.position.x >= 0.05)
                grid.level.GameOver(Level.GameResult.Lost);
            else if (transform.position.z - (float)(int)transform.position.z >= 0.05)
                grid.level.GameOver(Level.GameResult.Lost);

            transform.position = new Vector3(Mathf.Round(transform.position.x), 0, Mathf.Round(transform.position.z)); // position alignment
            switch (type)
            {
                case Rails.Type.TopLeft:
                    direction = direction == Direction.Right ? Direction.Top : Direction.Left;
                    break;
                case Rails.Type.TopRight:
                    direction = direction == Direction.Left ? Direction.Top : Direction.Right;
                    break;
                case Rails.Type.BottomLeft:
                    direction = direction == Direction.Right ? Direction.Bottom : Direction.Left;
                    break;
                case Rails.Type.BottomRight:
                    direction = direction == Direction.Left ? Direction.Bottom : Direction.Right;
                    break;
                default:
                    break;
            }
            move.x = move.y = 0;
            switch (direction)
            {
                case Direction.Top:
                    move.y = speedFactor;
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    break;
                case Direction.Bottom:
                    move.y = -speedFactor;
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;
                case Direction.Left:
                    move.x = -speedFactor;
                    transform.rotation = Quaternion.Euler(0, -180, 0);
                    break;
                case Direction.Right:
                    move.x = speedFactor;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
                default:
                    break;
            }           
        }
        transform.position += new Vector3(move.x*grid.trainSpeed, transform.position.y, move.y* grid.trainSpeed);
    }    
}
