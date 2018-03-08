using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text ScoreText;

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

        ScoreText.text = "Score: " + playerScore.ToString("0000");
    }
}
