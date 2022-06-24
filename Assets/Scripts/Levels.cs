using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : Level
{
    public Level_1(GameGrid g) : base(g)
    {
        gridSize = new Vector2Int(20, 15);
        firstStationY = gridSize.y - 3;
        endStationY = 3;
        resourcesOverlap = 0.35f;
        trainSpeed = 1.15f;
        levelName = "Level One";
    }
}
public class Level_2 : Level
{
    public Level_2(GameGrid g) : base(g)
    {
        gridSize = new Vector2Int(23, 15);
        firstStationY = gridSize.y-3;
        endStationY = 3;
        resourcesOverlap = 0.40f;
        trainSpeed = 1.3f;
        levelName = "Level Two";
    }    
}
public class Level_3 : Level
{
    public Level_3(GameGrid g) : base(g)
    {
        gridSize = new Vector2Int(26, 15);
        firstStationY = gridSize.y - 3;
        endStationY = 3;
        resourcesOverlap = 0.42f;
        trainSpeed = 1.4f;
        levelName = "Level Two";
    }

}