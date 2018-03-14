using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VoxelBusters.NativePlugins;

public class EndGameUI : MonoBehaviour
{
    public CanvasGroup EndGamePanel, HighScorePanel, PausedPanel, CreditsPanel, OtherAppsPanel, MainScreenPanel;
    public Text TotalScoreText, TotalDonutText, TotalTimeText, HighscoreText;
    public PlayerStats PlayerInformation;

    private int prevPanel = -1;

    private void Start()
    {
        if (PlayerStats.k_GamesPlayedThisSession == 0)
        {
            Time.timeScale = 0;
            PlayerInformation.TouchEnabled = false;

            MainScreenPanel.interactable = true;
            MainScreenPanel.blocksRaycasts = true;
            MainScreenPanel.alpha = 1;
        }
        else
        {
            Time.timeScale = 1;

            MainScreenPanel.interactable = false;
            MainScreenPanel.blocksRaycasts = false;
            MainScreenPanel.alpha = 0;
        }

        Application.runInBackground = true;

        //make invisible and buttons unusable
        EndGamePanel.interactable = false;
        HighScorePanel.interactable = false;
        PausedPanel.interactable = false;
        CreditsPanel.interactable = false;
        OtherAppsPanel.interactable = false;

        EndGamePanel.blocksRaycasts = false;
        HighScorePanel.blocksRaycasts = false;
        PausedPanel.blocksRaycasts = false;
        CreditsPanel.blocksRaycasts = false;
        OtherAppsPanel.blocksRaycasts = false;

        EndGamePanel.alpha = 0;
        HighScorePanel.alpha = 0;
        PausedPanel.alpha = 0;
        CreditsPanel.alpha = 0;
        OtherAppsPanel.alpha = 0;
    }

    public void MakeEndGameVisible()
    {
        //if game hasn't ended yet
        if (PlayerInformation.TimerLength < 0 && Time.timeScale != 0)
        {
            //Increase number of games played
            PlayerStats.k_GamesPlayed++;
            PlayerStats.k_GamesPlayedThisSession++;
            PlayerPrefs.SetInt("GamesPlayed", PlayerStats.k_GamesPlayed);

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
        if (PlayerStats.k_GamesPlayedThisSession % 3 == 0 && Advertisement.IsReady())
        {
            Advertisement.Show();
        }

        //Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenHighScorePanel(int panel)
    {
        //opens highscore panel
        HighScorePanel.interactable = true;
        HighScorePanel.blocksRaycasts = true;
        HighScorePanel.alpha = 1;

        //closes end game panel
        EndGamePanel.interactable = false;
        EndGamePanel.blocksRaycasts = false;
        EndGamePanel.alpha = 0;

        //closes paused panel
        PausedPanel.interactable = false;
        PausedPanel.blocksRaycasts = false;
        PausedPanel.alpha = 0;

        prevPanel = panel;

        SetupHighScorePanel();
    }

    public void CloseHighScorePanel()
    {
        //closes highscore panel
        HighScorePanel.interactable = false;
        HighScorePanel.blocksRaycasts = false;
        HighScorePanel.alpha = 0;

        if (prevPanel == 0)
        {
            //opens end game panel
            EndGamePanel.interactable = true;
            EndGamePanel.blocksRaycasts = true;
            EndGamePanel.alpha = 1;
        }
        else if (prevPanel == 1)
        {
            //opens end game panel
            PausedPanel.interactable = true;
            PausedPanel.blocksRaycasts = true;
            PausedPanel.alpha = 1;
        }

        prevPanel = -1;
    }

    private void SetupHighScorePanel()
    {
        //put updated highscore information into text
        HighscoreText.text = string.Format("{1}: {0}\n{3}: {2}\n{5}: {4}\n{7}: {6}\n{9}: {8}\n{11}: {10}\n{13}: {12}\n{15}: {14}",
            PlayerPrefs.GetInt("Score0", 0).ToString("0000"), PlayerPrefs.GetString("Date0", "N/A"),
            PlayerPrefs.GetInt("Score1", 0).ToString("0000"), PlayerPrefs.GetString("Date1", "N/A"),
            PlayerPrefs.GetInt("Score2", 0).ToString("0000"), PlayerPrefs.GetString("Date2", "N/A"),
            PlayerPrefs.GetInt("Score3", 0).ToString("0000"), PlayerPrefs.GetString("Date3", "N/A"),
            PlayerPrefs.GetInt("Score4", 0).ToString("0000"), PlayerPrefs.GetString("Date4", "N/A"),
            PlayerPrefs.GetInt("Score6", 0).ToString("0000"), PlayerPrefs.GetString("Date6", "N/A"),
            PlayerPrefs.GetInt("Score7", 0).ToString("0000"), PlayerPrefs.GetString("Date7", "N/A"),
            PlayerPrefs.GetInt("Score8", 0).ToString("0000"), PlayerPrefs.GetString("Date8", "N/A"));
    }

    private void UpdateHighScorePanel()
    {
        //get current player info
        int[] playerInfo = PlayerInformation.GetPlayerInformation();

        //shove into a list all of the saved data and current data, defaults are 0 and today
        List<Tuple<int, string>> highscoreTable = new List<Tuple<int, string>>();
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score0", 0), PlayerPrefs.GetString("Date0", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score1", 0), PlayerPrefs.GetString("Date1", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score2", 0), PlayerPrefs.GetString("Date2", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score3", 0), PlayerPrefs.GetString("Date3", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score4", 0), PlayerPrefs.GetString("Date4", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score5", 0), PlayerPrefs.GetString("Date5", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score6", 0), PlayerPrefs.GetString("Date6", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score7", 0), PlayerPrefs.GetString("Date7", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(PlayerPrefs.GetInt("Score8", 0), PlayerPrefs.GetString("Date8", "N/A")));
        highscoreTable.Add(new Tuple<int, string>(playerInfo[0], "Game " + PlayerStats.k_GamesPlayed + ": " + DateTime.Now.ToString("dd-MM-yy")));

        //sort highscore table by score, big -> small
        highscoreTable = highscoreTable.OrderBy(w => w.First).Reverse().ToList();

        //save top six scores
        PlayerPrefs.SetInt("Score0", highscoreTable[0].First);
        PlayerPrefs.SetInt("Score1", highscoreTable[1].First);
        PlayerPrefs.SetInt("Score2", highscoreTable[2].First);
        PlayerPrefs.SetInt("Score3", highscoreTable[3].First);
        PlayerPrefs.SetInt("Score4", highscoreTable[4].First);
        PlayerPrefs.SetInt("Score5", highscoreTable[5].First);
        PlayerPrefs.SetInt("Score6", highscoreTable[6].First);
        PlayerPrefs.SetInt("Score7", highscoreTable[7].First);
        PlayerPrefs.SetInt("Score8", highscoreTable[7].First);

        //save dates associated with top six scores
        PlayerPrefs.SetString("Date0", highscoreTable[0].Second);
        PlayerPrefs.SetString("Date1", highscoreTable[1].Second);
        PlayerPrefs.SetString("Date2", highscoreTable[2].Second);
        PlayerPrefs.SetString("Date3", highscoreTable[3].Second);
        PlayerPrefs.SetString("Date4", highscoreTable[4].Second);
        PlayerPrefs.SetString("Date5", highscoreTable[5].Second);
        PlayerPrefs.SetString("Date6", highscoreTable[6].Second);
        PlayerPrefs.SetString("Date7", highscoreTable[7].Second);
        PlayerPrefs.SetString("Date8", highscoreTable[7].Second);

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

        //disable player input
        PlayerInformation.TouchEnabled = false;
    }

    public void ClosePauseMenu()
    {
        //closes pause panel
        PausedPanel.interactable = false;
        PausedPanel.blocksRaycasts = false;
        PausedPanel.alpha = 0;

        //start time
        Time.timeScale = 1;

        StartCoroutine(DelayAllowingPlayerInput());
    }

    IEnumerator DelayAllowingPlayerInput()
    {
        yield return new WaitForEndOfFrame();

        //enable player input
        PlayerInformation.TouchEnabled = true;
    }

    public void OpenCreditsMenu()
    {
        //opens credits panel
        CreditsPanel.interactable = true;
        CreditsPanel.blocksRaycasts = true;
        CreditsPanel.alpha = 1;

        //closes pause panel
        PausedPanel.interactable = true;
        PausedPanel.blocksRaycasts = true;
        PausedPanel.alpha = 1;

        //opens pause panel
        PausedPanel.interactable = true;
        PausedPanel.blocksRaycasts = true;
        PausedPanel.alpha = 1;
    }

    public void CloseCreditsMenu()
    {
        //closes credits panel
        CreditsPanel.interactable = false;
        CreditsPanel.blocksRaycasts = false;
        CreditsPanel.alpha = 0;

        //opens pause panel
        PausedPanel.interactable = true;
        PausedPanel.blocksRaycasts = true;
        PausedPanel.alpha = 1;
    }

    public void OpenOtherAppsPanel()
    {
        //closes pause panel
        PausedPanel.interactable = false;
        PausedPanel.blocksRaycasts = false;
        PausedPanel.alpha = 0;

        //opens other apps panel
        OtherAppsPanel.interactable = true;
        OtherAppsPanel.blocksRaycasts = true;
        OtherAppsPanel.alpha = 1;
    }

    public void CloseOtherAppsPanel()
    {
        //closes other apps panel
        OtherAppsPanel.interactable = false;
        OtherAppsPanel.blocksRaycasts = false;
        OtherAppsPanel.alpha = 0;

        //opens pause panel
        PausedPanel.interactable = true;
        PausedPanel.blocksRaycasts = true;
        PausedPanel.alpha = 1;
    }

    public void OpenOtherAppURL(OtherApp app)
    {
#if UNITY_ANDROID || UNITY_EDITOR
        if (app.GooglePlayURL == string.Empty)
        {
            Application.OpenURL(app.AppleURL);
        }
        else
        {
            Application.OpenURL(app.GooglePlayURL);
        }
#else
        if (app.AppleURL == string.Empty)
        {
            Application.OpenURL(app.GooglePlayURL);
        }
        else
        {
            Application.OpenURL(app.AppleURL);
        }
#endif
    }

    public void ShareGame()
    {
        SocialShareSheet _shareSheet = new SocialShareSheet();
        _shareSheet.Text = "I just got a" + PlayerPrefs.GetInt("Score0", 0) + " on Teeter Totter Lemming Dropper, can you beat that?";

#if UNITY_IOS
        _shareSheet.URL = "https://itunes.apple.com/us/app/teeter-totter-lemming-dropper/id1358140021?ls=1&mt=8";
#elif UNITY_ANDROID
        _shareSheet.URL = "https://play.google.com/store/apps/details?id=com.RebeccaAnsems.teetertotter&hl=en";
#endif

        // Show composer
        NPBinding.UI.SetPopoverPointAtLastTouchPosition(); // To show popover at last touch point on iOS. On Android, its ignored.
        NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
    }

    private void FinishedSharing(eShareResult _result)
    {
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        MainScreenPanel.GetComponent<Animator>().SetBool("fadeOut", true);
        StartCoroutine(DelayAllowingPlayerInput());
    }

    public void ButtonPressed()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            this.GetComponent<PlayAudio>().Play();
            Time.timeScale = 0;
        }
        else
        {
            this.GetComponent<PlayAudio>().Play();
        }
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