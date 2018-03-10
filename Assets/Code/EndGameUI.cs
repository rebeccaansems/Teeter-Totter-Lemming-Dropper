using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public CanvasGroup EndGamePanel;
    public Text TotalScoreText, TotalDonutText, TotalTimeText;
    public PlayerStats PlayerInformation;

    private void Start()
    {
        //make invisible and buttons unusable
        EndGamePanel.interactable = false;
        EndGamePanel.alpha = 0;

        //set time scale to normal
        Time.timeScale = 1;
    }

    public void MakeEndGameVisible()
    {
        //stop time
        Time.timeScale = 0;

        //make visible and buttons usable
        EndGamePanel.interactable = true;
        EndGamePanel.alpha = 1;

        //set information on end screen
        int[] playerInfo = PlayerInformation.GetPlayerInformation();

        TotalScoreText.text = string.Format("Total Score: {0}", playerInfo[0]);
        TotalDonutText.text = string.Format("Total Donuts Eaten: {0}\nLeft Lemming: {1}\nRight Lemming: {2}", 
            playerInfo[1], playerInfo[2], playerInfo[3]);
        TotalTimeText.text = string.Format("Total Time Bonuses: {0}", playerInfo[4]);
    }

    public void ResetLevel()
    {
        //Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
