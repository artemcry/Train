using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Int size { get; private set; }
    public int cellSize { get; private set; }
    public int startRailsCount { get; private set; }

    private float gridOffset = 0.5f;
    private GameObject[,] _grid;
    public Level level { get; private set; }
    public float trainSpeed;

    public GameObject[] resourcePrefabs;
    public GameObject[] treePrefabs;
    public GameObject[] stonePrefabs;
    [SerializeField]
    private GameObject rails;
    [SerializeField]
    private GameObject turnedRails;
    [SerializeField]
    private GameObject train;
    [SerializeField]
    private GameObject trainTerminal;
    [SerializeField]
    private GameObject trainStock;
    [SerializeField]
    private GameObject gridBorder;
    [SerializeField]
    public GameObject gameOverScreen;

    private void Awake()
    {
        UserData.Load();
    }
    void Start()
    {        
        startRailsCount = 9;
        cellSize = 1;
        loadLevel(UserData.UserLevel.ToString());
        _grid = new GameObject[size.x, size.y];


        GameObject player = GameObject.Find("Player");
        System.Random r = new System.Random();
        int x = r.Next(3, startRailsCount);
        int y = r.Next(3, startRailsCount);

        player.transform.position = new Vector3(x, -0.5f, y);
        _grid[x, y] = player;

        generateTrain();
        generateBorder();
        level.generateLevel();
        _grid[x, y] = null;
    }
    public GameObject this[int x, int y]
    {
        get { return _grid[x, y]; }
        set { _grid[x, y] = value; }
    }
    void loadLevel(string name)
    {
        level = Level.getLevelByNum(name, this);
        size = level.gridSize;
        trainSpeed = level.trainSpeed;
    }

    void generateTrain()
    {
        // first station
        for (int i = 0; i < startRailsCount; i++)
            pushRails(i, level.firstStationY, Rails.Type.Horizontal);
           

        // make Train
        float trainOffset = 2.45f;
        Instantiate(trainStock, new Vector3(gridOffset, 0, level.firstStationY), Quaternion.identity);
        Instantiate(trainTerminal, new Vector3(trainOffset + gridOffset, 0, level.firstStationY), Quaternion.identity);
        Instantiate(train, new Vector3(2 * trainOffset + gridOffset, 0, level.firstStationY), Quaternion.identity);

        // end station       
        for (int i = 0; i < 5; i++) 
            pushRails(size.x - 1 - i, level.endStationY, Rails.Type.Horizontal);
    }
    void generateBorder()
    {
        GameObject t = Instantiate(gridBorder);
        float h = 2.0f;
        gridBorder.transform.position = new Vector3(size.x / 2.0f - gridOffset, h / 2.0f - gridOffset, size.y);
        gridBorder.transform.localScale = new Vector3(size.x, h, 1);

        GameObject gb = Instantiate(gridBorder);
        gb.transform.position = new Vector3(size.x / 2.0f - gridOffset, h / 2.0f - gridOffset, -1);
        gb.transform.localScale = new Vector3(size.x, h, 1);

        GameObject l = Instantiate(gridBorder);
        l.transform.position = new Vector3(-1, h / 2.0f - gridOffset, size.y / 2.0f - gridOffset);
        l.transform.localScale = new Vector3(1, h, size.y + gridOffset * 4);

        GameObject r = Instantiate(gridBorder);
        r.transform.position = new Vector3(size.x, h / 2.0f - gridOffset, size.y / 2.0f - gridOffset);
        r.transform.localScale = new Vector3(1, h, size.y + gridOffset * 4);
    }
    void pushRails(int x, int y, Rails.Type t)
    {
        Rails rr = Instantiate(t == Rails.Type.Horizontal || t == Rails.Type.Vertical ? rails : turnedRails,
            new Vector3(x, 0, y), Quaternion.identity).GetComponent<Rails>();
        rr.setType(t);
        _grid[x, y] = rr.gameObject;
    }
    public bool setRails(int x, int y)
    {
        if (isRailsSet(x, y))
            return false;
        Rails.Type type = Rails.Type.Horizontal;

        if (isRailsSet(x, y + 1))
        {
            type = Rails.Type.Vertical;
            if (isRailsSet(x - 1, y + 1))
            {
                Destroy(_grid[x, y + 1]);
                pushRails(x, y + 1, Rails.Type.BottomLeft);
            }
            else if (isRailsSet(x + 1, y + 1))
            {
                Destroy(_grid[x, y + 1]);
                pushRails(x, y + 1, Rails.Type.BottomRight);
            }
        }
        else if (isRailsSet(x + 1, y))
        {
            type = Rails.Type.Horizontal;
            if (isRailsSet(x + 1, y + 1))
            {
                Destroy(_grid[x + 1, y]);
                pushRails(x + 1, y, Rails.Type.TopLeft);
            }
            else if (isRailsSet(x + 1, y - 1))
            {
                Destroy(_grid[x + 1, y]);
                pushRails(x + 1, y, Rails.Type.BottomLeft);
            }
        }
        else if (isRailsSet(x, y - 1))
        {
            type = Rails.Type.Vertical;
            if (isRailsSet(x + 1, y - 1))
            {
                Destroy(_grid[x, y - 1]);
                pushRails(x, y - 1, Rails.Type.TopRight);
            }
            else if (isRailsSet(x - 1, y - 1))
            {
                Destroy(_grid[x, y - 1]);
                pushRails(x, y - 1, Rails.Type.TopLeft);
            }
        }
        else if (isRailsSet(x - 1, y))
        {
            type = Rails.Type.Horizontal;
            if (isRailsSet(x - 1, y + 1))
            {
                Destroy(_grid[x - 1, y]);
                pushRails(x - 1, y, Rails.Type.TopRight);
            }
            else if (isRailsSet(x - 1, y - 1))
            {
                Destroy(_grid[x - 1, y]);
                pushRails(x - 1, y, Rails.Type.BottomRight);
            }
        }


        pushRails(x, y, type);
        return true;
    }
    public bool isRailsSet(int x, int y)
    {
        if (_grid[x, y] != null)
            return _grid[x, y].tag == "Rails";
        return false;
    }

}
