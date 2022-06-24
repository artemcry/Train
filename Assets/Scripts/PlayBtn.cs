using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayBtn : MonoBehaviour
{
    public GameObject mainMenu;
    public void Awake()
    {
        UserData.Load();
    }
    public void onClick()
    {
        if (UserData.UserName == null || UserData.UserName == "")
        {
            mainMenu.SetActive(true);
            GameObject.Find("MainMenu").SetActive(false);
        }
        else
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
