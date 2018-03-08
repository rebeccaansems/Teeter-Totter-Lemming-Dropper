using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text ScoreText, TimerText;
    public float TimerLength;

    private int playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten;

    public void AddDonutEaten(int score, bool isLeftLemming)
    {
        playerScore += score;
        donutsEaten++;

        if (isLeftLemming)
        {
            leftLemmingDonutsEaten++;
        }
        else
        {
            rightLemmingDonutsEaten++;
        }

        ScoreText.text = "Score: " + playerScore.ToString("00000");
    }

    public void AddTime(int timeAdd)
    {
        TimerLength += timeAdd;
    }

    private void Update()
    {
        TimerLength -= Time.deltaTime;

        TimerText.text = "Seconds Left: " + TimerLength.ToString("000");
        if (TimerLength < 0)
        {
            TimerUp();
        }
    }

    private void TimerUp()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
