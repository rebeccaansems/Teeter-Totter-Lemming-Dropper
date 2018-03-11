using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public EndGameUI EndGame;
    public Text ScoreText, TimerText;
    public Image[] Collectables;
    public Sprite[] GatheredCollectables;

    public float TimerLength;
    public bool TouchEnabled = true;

    private int playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten, totalTimeBonus;
    private bool fullCollectAwarded = false;

    //Increase score and num donuts eaten
    public void AddDonutEaten(int score, bool isLeftLemming)
    {
        //increase player score by given amount
        playerScore += score;
        //increase donuts eaten by 1
        donutsEaten++;

        //if donut eaten by left, increase left, if right, increase right
        if (isLeftLemming)
        {
            leftLemmingDonutsEaten++;
        }
        else
        {
            rightLemmingDonutsEaten++;
        }
        //display updated score
        ScoreText.text = "Score: " + playerScore.ToString("00000");
    }

    //Increase time and collectables eaten
    public void AddCollectableEaten(int score, int collect)
    {
        //increase player score by given amount
        playerScore += score;
        //goldify the correct icon in the top panel
        Collectables[collect].sprite = GatheredCollectables[collect];

        //increase total time remaining  by 10 seconds
        AddTime(10);

        //if all four collectabls have been gathered
        if (HaveAllCollectablesBeenGathered())
        {
            //increase total time remaining by 30 seconds
            AddTime(30);
            //double total score
            playerScore = playerScore * 2;
            //show updated score
            ScoreText.text = "Score: " + playerScore.ToString("00000");
        }
    }

    //Check if all collectables have been gathered
    private bool HaveAllCollectablesBeenGathered()
    {
        //if bonus has not been collected already
        if (fullCollectAwarded == false)
        {
            //loop through all collectables
            for (int i = 0; i < GatheredCollectables.Length; i++)
            {
                //collectable has not been gathered
                if (Collectables[i].sprite != GatheredCollectables[i])
                {
                    return false;
                }
            }
            //all collectables have been collected, set bonus collected to true
            fullCollectAwarded = true;
            return true;
        }
        return false;
    }

    //increase time remaining in game and total time bonuses added
    public void AddTime(int timeAdd)
    {
        totalTimeBonus += timeAdd;
        TimerLength += timeAdd;
    }

    private void Update()
    {
        //remove time for remaining time
        TimerLength -= Time.deltaTime;
        //update text with how much is remaining
        TimerText.text = "Seconds Left: " + TimerLength.ToString("000");

        //if less than 0 seconds in game
        if (TimerLength < 0)
        {
            TimerUp();
        }
    }

    //What to do once the timer is up
    private void TimerUp()
    {
        EndGame.MakeEndGameVisible();
    }

    //get array of players information
    public int[] GetPlayerInformation()
    {
        return new int[] { playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten, totalTimeBonus };
    }
}
