using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text ScoreText, TimerText;
    public Image[] Collectables;
    public Sprite[] GatheredCollectables;
    public float TimerLength;

    private int playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten;
    private bool fullCollectAwarded = false;

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

    public void AddCollectableEaten(int score, int collect)
    {
        playerScore += score;
        Collectables[collect].sprite = GatheredCollectables[collect];

        AddTime(10);

        if (AwardFullCollectablesBonus())
        {
            AddTime(30);
            playerScore = playerScore * 2;
            ScoreText.text = "Score: " + playerScore.ToString("00000");
        }
    }

    private bool AwardFullCollectablesBonus()
    {
        if (fullCollectAwarded == false)
        {
            for (int i = 0; i < GatheredCollectables.Length; i++)
            {
                if (Collectables[i].sprite != GatheredCollectables[i])
                {
                    return false;
                }
                fullCollectAwarded = true;
                return true;
            }
        }
        return false;
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
