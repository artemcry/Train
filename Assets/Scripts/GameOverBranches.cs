using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBranches : MonoBehaviour
{
    public void Continue() {
        SceneManager.LoadScene(UserData.currentGameResult == Level.GameResult.Win ? "SampleScene" : "StartScreen", LoadSceneMode.Single);
        Time.timeScale = 1;
        UserData.Save();
   }
    public void Home()
    {
        SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
        Time.timeScale = 1;
        UserData.Save();
    }

}
