using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class UserData
{
    public static string saveDir = Application.persistentDataPath + "/save";
    public static Level.GameResult currentGameResult;

    public static string UserName
    {
        get { return obj._userName; }
        set { obj._userName = value;
            if (!obj._rating.ContainsKey(value))
                obj._rating[value] = 1;
        }
    }

    public static int UserLevel
    {
        get { return obj._rating[obj._userName]; }
        set { obj._rating[obj._userName] = value; }
    }
    public static Dictionary<string, int> Rating
    {
        get { return obj._rating; }
    }   
    private static UserData obj = null;
    private string _userName = null;
    private Dictionary<string, int> _rating = null;

    private UserData()
    {
    }

    public static void Load()
    {
        if (obj != null)
            return;
        
        try
        {
            using (Stream stream = File.Open(saveDir, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                obj = (UserData)binaryFormatter.Deserialize(stream);
            }
        }
        catch (Exception)
        {
            obj = new UserData();
            obj._rating = new Dictionary<string, int>() {
                {"Bob", 1},
                {"Alex", 2},
                {"Need", 1},
                {"Flex", 2},
                {"Air", 2},
                {"Artx", 2}
            };
            obj._userName = null;
        }
    }

    public static void Save()
    {
        using (Stream stream = File.Open(saveDir, FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, obj);
        }
    }

}
