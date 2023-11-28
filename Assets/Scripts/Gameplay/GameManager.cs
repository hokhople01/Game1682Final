using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MovementController
{
    public GameObject[] players;
    public GameOverScreen gameOverScreen;
    public void ChechWinState() 
    {
        int aliveCount = 0;

        foreach(GameObject player in players)
        {
            if(player.activeSelf)
            {
                aliveCount++;
            }
        }

        if(aliveCount <=1)
        {
            Invoke(nameof(NewRound), 3f);
        }

    }
    private void NewRound()
    {
         
            SceneManager.LoadScene("GameOver");

          
       
    }

}
