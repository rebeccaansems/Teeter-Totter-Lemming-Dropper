using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public CanvasGroup EndGamePanel, HighScorePanel, PausedPanel;
    public Text TotalScoreText, TotalDonutText, TotalTimeText, HighscoreText;
    public PlayerStats PlayerInformation;

    private void Start()
    {
        Application.runInBackground = true;

        //make invisible and buttons unusable
        EndGamePanel.interactable = false;
        HighScorePanel.interactable = false;
        PausedPanel.interactable = false;

        EndGamePanel.blocksRaycasts = false;
        HighScorePanel.blocksRaycasts = false;
        PausedPanel.blocksRaycasts = false;

        EndGamePanel.alpha = 0;
        HighScorePanel.alpha = 0;
        PausedPanel.alpha = 0;

        //set time scale to normal
        Time.timeScale = 1;
    }

    public void MakeEndGameVisible()
    {
        //if game hasn't ended yet
        if (Time.timeScale != 0)
        {
            //stop time
            Time.timeScale = 0;

            //make visible and buttons usable
            EndGamePanel.interactable = true;
            EndGamePanel.blocksRaycasts = true;
            EndGamePanel.alpha = 1;

            //update highscores
            UpdateHighScorePanel();

            //set information on end screen
            int[] playerInfo = PlayerInformation.GetPlayerInformation();

            TotalScoreText.text = string.Format("Total Score: {0}", playerInfo[0]);
            TotalDonutText.text = string.Format("Total Donuts Eaten: {0}\nLeft Lemming: {1}\nRight Lemming: {2}",
                playerInfo[1], playerInfo[2], playerInfo[3]);
            TotalTimeText.text = string.Format("Total Time Bonuses: {0}", playerInfo[4]);
        }
    }

    public void ResetLevel()
    {
        //Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenHighScorePanel()
    {
        //opens highscore panel
        HighScorePanel.interactable = true;
        HighScorePanel.blocksRaycasts = true;
        HighScorePanel.alpha = 1;

        //closes end game panel
        EndGamePanel.interactable = false;
        EndGamePanel.blocksRaycasts = false;
        EndGamePanel.alpha = 0;

        SetupHighScorePanel();
    }

    public void CloseHighScorePanel()
    {
        //closes highscore panel
        HighScorePanel.interactable = false;
        HighScorePanel.blocksRaycasts = false;
        HighScorePanel.alpha = 0;

        //opens end game panel
        EndGamePanel.interactable = true;
        EndGamePanel.blocksRaycasts = true;
        EndGamePanel.alpha = 1;
    }

    private void SetupHighScorePanel()
    {
        //put updated highscore information into text
        HighscoreText.text = string.Format("{1}: {0}\n{3}: {2}\n{5}: {4}\n{7}: {6}\n{9}: {8}\n{11}: {10}",
            PlayerPrefs.GetInt("Score0", 0).ToString("0000"), PlayerPrefs.GetString("Date0", DateTime.Now.ToString("dd-MM-yy")),
            PlayerPrefs.GetInt("Score1", 0).ToString("0000"), PlayerPrefs.GetString("Date1", DateTime.Now.ToString("dd-MM-yy")),
            PlayerPrefs.GetInt("Score2", 0).ToString("0000"), PlayerPrefs.GetString("Date2", DateTime.Now.ToString("dd-MM-yy")),
            PlayerPrefs.GetInt("Score3", 0).ToString("0000"), PlayerPrefs.GetString("Date3", DateTime.Now.ToString("dd-MM-yy")),
            PlayerPrefs.GetInt("Score4", 0).ToString("0000"), PlayerPrefs.GetString("Date4", DateTime.Now.ToString("dd-MM-yy")),
            PlayerPrefs.GetInt("Score5", 0).ToString("0000"), PlayerPrefs.GetString("Date5", DateTime.Now.ToString("dd-MM-yy")));
    }

    private void UpdateHighScorePanel()
    {
        //get current player info
        int[] playerInfo = PlayerInformation.GetPlayerInformation();

        //shove into a list all of the saved data and current data, defaults are 0 and today
        List<Tuple<int, string>> highscoreTable = new List<Tuple<int, string>>();
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score0", 0), PlayerPrefs.GetString("Date0", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score1", 0), PlayerPrefs.GetString("Date1", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score2", 0), PlayerPrefs.GetString("Date2", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score3", 0), PlayerPrefs.GetString("Date3", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score4", 0), PlayerPrefs.GetString("Date4", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score5", 0), PlayerPrefs.GetString("Date5", DateTime.Now.ToString("dd-MM-yy"))));
        highscoreTable.Add(new Tuple<int, string>(playerInfo[0], DateTime.Now.ToString("dd-MM-yy")));

        //sort highscore table by score, big -> small
        highscoreTable = highscoreTable.OrderBy(w => w.First).Reverse().ToList();

        //save top six scores
        PlayerPrefs.SetInt("Score0", highscoreTable[0].First);
        PlayerPrefs.SetInt("Score1", highscoreTable[1].First);
        PlayerPrefs.SetInt("Score2", highscoreTable[2].First);
        PlayerPrefs.SetInt("Score3", highscoreTable[3].First);
        PlayerPrefs.SetInt("Score4", highscoreTable[4].First);
        PlayerPrefs.SetInt("Score5", highscoreTable[5].First);

        //save dates associated with top six scores
        PlayerPrefs.SetString("Date0", highscoreTable[0].Second);
        PlayerPrefs.SetString("Date1", highscoreTable[1].Second);
        PlayerPrefs.SetString("Date2", highscoreTable[2].Second);
        PlayerPrefs.SetString("Date3", highscoreTable[3].Second);
        PlayerPrefs.SetString("Date4", highscoreTable[4].Second);
        PlayerPrefs.SetString("Date5", highscoreTable[5].Second);

        //save highscore table
        PlayerPrefs.Save();
    }

    public void OpenPauseMenu()
    {
        //opens pause panel
        PausedPanel.interactable = true;
        PausedPanel.blocksRaycasts = true;
        PausedPanel.alpha = 1;

        //stop time
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        //closes pause panel
        PausedPanel.interactable = false;
        PausedPanel.blocksRaycasts = false;
        PausedPanel.alpha = 0;

        //start time
        Time.timeScale = 1;
    }
}

public class Tuple<T1, T2>
{
    public T1 First { get; private set; }
    public T2 Second { get; private set; }
    internal Tuple(T1 first, T2 second)
    {
        First = first;
        Second = second;
    }
}