using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level
{
    public enum GameResult
    {
        Win,
        Lost
    }
    public float resourcesOverlap { get; protected set; }
    public Vector2Int gridSize { get; protected set; }
    public float trainSpeed { get; protected set; }
    public string levelName { get; protected set; }
    public int firstStationY { get; protected set; }
    public int endStationY { get; protected set; }

    protected GameGrid grid;
    protected Level(GameGrid g)
    {
        grid = g;
    }
    public static Level getLevelByNum(string number, GameGrid grid)
    {
        object[] s = { grid };
        System.Type t = System.Type.GetType($"Level_{number}");
        if (t == null)
            return null;
        return (Level)System.Activator.CreateInstance(t, s);
    }
    public static string getLeveNumber(Level l)
    {
        return Regex.Match(l.GetType().Name, @"\d+").Value;
    }
    public virtual void generateLevel()
    {
        if (resourcesOverlap > 1.0f)
            throw new System.Exception("Resources Overlap > 100%");
        int resourcesCount = (int)(gridSize.x * gridSize.y * resourcesOverlap), done = 0;
        System.Random r = new System.Random();
        while (done < resourcesCount)
        {
            int x = r.Next(0, gridSize.x), y = r.Next(0, gridSize.y);
            if (grid[x, y] != null)
                continue;
            done++;
            bool setTree = r.Next() %2 == 0;
            grid[x, y] =
            Object.Instantiate(setTree ? grid.treePrefabs[r.Next(0, grid.treePrefabs.Length)] : grid.stonePrefabs[r.Next(0, grid.stonePrefabs.Length)], 
            new Vector3(grid.transform.position.x + x * grid.cellSize, 0,
                        grid.transform.position.z + y * grid.cellSize), Quaternion.Euler(0, r.Next(-90, 90), 0));        
        }
    }
    public virtual void GameOver(GameResult res)
    {
        Time.timeScale = 0;
        GameObject.Find("GameControllers").SetActive(false);
        grid.gameOverScreen.SetActive(true);
        Text t = GameObject.Find("LevelResult").GetComponent<Text>();
        UserData.currentGameResult = res;
        if (res == GameResult.Win)
        {
            if (getLevelByNum((UserData.UserLevel + 1).ToString(), grid) == null)
            {
                t.text = "You passed the last level!";
                UserData.currentGameResult = GameResult.Lost;
                t.color = new Color(218.0f / 255.0f, 165.0f / 255.0f, 32.0f / 255.0f);
                return;
            }
            UserData.UserLevel++;
            t.text = "You Win!";
            t.color = Color.green;
        }
        else
        {
            t.text = "You Lost";
            t.color = Color.red;
        }

    }
}
