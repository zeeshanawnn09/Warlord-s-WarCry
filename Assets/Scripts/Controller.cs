using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{

    public void PlayButton()
    {
        LevelManager.totalEnemies = 0;
        LevelManager.TotalWaves = 3;
        LevelManager.WavesEmitted = 0;
        LevelManager.MoneyGained = 200;
        LevelManager.MaxLives = 10;

        LevelManager.LvlOver = false;
        LevelManager.nxtWave = false;

        SceneManager.LoadScene("Level 01");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
