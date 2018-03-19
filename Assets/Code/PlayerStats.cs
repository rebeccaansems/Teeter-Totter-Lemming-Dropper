using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int s_GamesPlayed = 0, s_GamesPlayedThisSession = 0;
    public static float s_SFXVolume, s_MusicVolume;

    public EndGameUI[] EndGame;
    public Image[] BonusImage;
    public Text[] InfoText, BonusText;
    public Image[] Collectables;
    public Sprite[] GatheredCollectables;
    public Animator[] BonusTextAnimator;
    public Sprite EmptySprite;
    public Slider[] SfxSlider, MusicSlider;

    public float TimerLength;
    public bool TouchEnabled = true;

    private Coroutine bonus;

    private int playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten, totalTimeBonus, totalCollectables;
    private bool fullCollectAwarded = false;

    private void Start()
    {
        s_GamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);

        s_SFXVolume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        s_MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        SfxSlider[(int)DeviceSelector.DEVICE].value = s_SFXVolume;
        MusicSlider[(int)DeviceSelector.DEVICE].value = s_MusicVolume;

        if (s_GamesPlayedThisSession == 0)
        {
            TimerLength += 3;
        }
    }

    //Increase score and num donuts eaten
    public void AddDonutEaten(Sprite donutImage, int score, bool isLeftLemming)
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
        InfoText[(int)DeviceSelector.DEVICE].text = string.Format("Score: {0}\nSeconds Left: {1}", playerScore.ToString("00000"), TimerLength.ToString("000"));

        //make bonus appear
        BonusImage[(int)DeviceSelector.DEVICE].sprite = donutImage;
        BonusText[(int)DeviceSelector.DEVICE].text = "+" + score + " pts";
        BonusTextAnimator[(int)DeviceSelector.DEVICE].SetBool("fadeIn", true);

        if (bonus != null)
        {
            StopCoroutine(bonus);
        }
        bonus = StartCoroutine(ShowBonusText());
    }

    IEnumerator ShowBonusText()
    {
        yield return new WaitForSeconds(3);
        BonusTextAnimator[(int)DeviceSelector.DEVICE].SetBool("fadeIn", false);
    }

    //Increase time and collectables eaten
    public void AddCollectableEaten(Sprite collectImage, int score, int collect)
    {
        //increase player score by given amount
        playerScore += score;
        totalCollectables++;
        //goldify the correct icon in the top panel
        int num = collect + (DeviceSelector.DEVICE == DeviceSelector.DeviceType.Phone ? 0 : 4);
        Collectables[collect + (DeviceSelector.DEVICE == DeviceSelector.DeviceType.Phone ? 0 : 4)].sprite =
            GatheredCollectables[collect + (DeviceSelector.DEVICE == DeviceSelector.DeviceType.Phone ? 0 : 4)];

        //show updated score
        InfoText[(int)DeviceSelector.DEVICE].text = string.Format("Score: {0}\nSeconds Left: {1}", playerScore.ToString("00000"), TimerLength.ToString("000"));

        //increase total time remaining  by 10 seconds
        AddTime(10);

        //make bonus appear
        BonusImage[(int)DeviceSelector.DEVICE].sprite = collectImage;
        BonusText[(int)DeviceSelector.DEVICE].text = "+" + score + " pts +10 secs";
        BonusTextAnimator[(int)DeviceSelector.DEVICE].SetBool("fadeIn", true);

        if (bonus != null)
        {
            StopCoroutine(bonus);
        }
        bonus = StartCoroutine(ShowBonusText());

        //if all four collectabls have been gathered
        if (HaveAllCollectablesBeenGathered())
        {
            //increase total time remaining by 30 seconds
            AddTime(30);
            //double total score
            playerScore = playerScore * 2;
            //show updated score
            InfoText[(int)DeviceSelector.DEVICE].text = string.Format("Score: {0}\nSeconds Left: {1}", playerScore.ToString("00000"), TimerLength.ToString("000"));

            //make bonus appear
            BonusImage[(int)DeviceSelector.DEVICE].sprite = EmptySprite;
            BonusText[(int)DeviceSelector.DEVICE].text = "BONUS! Score x2";
            BonusTextAnimator[(int)DeviceSelector.DEVICE].SetBool("fadeIn", true);

            if (bonus != null)
            {
                StopCoroutine(bonus);
            }
            bonus = StartCoroutine(ShowBonusText());
        }
    }

    //Check if all collectables have been gathered
    private bool HaveAllCollectablesBeenGathered()
    {
        //if bonus has not been collected already
        if (fullCollectAwarded == false)
        {
            //loop through all collectables
            for (int i = 0; i < GatheredCollectables.Length / 2; i++)
            {
                //collectable has not been gathered
                if (Collectables[i + (DeviceSelector.DEVICE == DeviceSelector.DeviceType.Phone ? 0 : 4)].sprite
                    != GatheredCollectables[i + (DeviceSelector.DEVICE == DeviceSelector.DeviceType.Phone ? 0 : 4)])
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
        InfoText[(int)DeviceSelector.DEVICE].text = string.Format("Score: {0}\nSeconds Left: {1}", playerScore.ToString("00000"), TimerLength.ToString("000"));

        //if less than 0 seconds in game
        if (TimerLength < 0)
        {
            TimerUp();
        }
    }

    //What to do once the timer is up
    private void TimerUp()
    {
        EndGame[(int)DeviceSelector.DEVICE].MakeEndGameVisible();
    }

    //get array of players information
    public int[] GetPlayerInformation()
    {
        return new int[] { playerScore, donutsEaten, leftLemmingDonutsEaten, rightLemmingDonutsEaten, totalTimeBonus, totalCollectables };
    }

    public void SetSFXVolume()
    {
        s_SFXVolume = SfxSlider[(int)DeviceSelector.DEVICE].value;
        PlayerPrefs.SetFloat("SfxVolume", s_SFXVolume);
    }

    public void SetMusicVolume()
    {
        s_MusicVolume = MusicSlider[(int)DeviceSelector.DEVICE].value;
        PlayerPrefs.SetFloat("MusicVolume", s_MusicVolume);
    }
}
