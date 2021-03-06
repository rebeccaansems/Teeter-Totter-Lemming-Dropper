﻿using System.Collections;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{

    public GameObject DonutPrefab, CollectablePrefab, BirdPrefab;
    public float LowestY, HighestY;

    private GameObject predictor, spawn;
    private float startX;

    private void Start()
    {
        StartCoroutine(Spawn());
        startX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + 3;
    }

    //Spawn donuts and collectables
    IEnumerator Spawn()
    {
        if (PlayerStats.s_GamesPlayedThisSession == 0)
        {
            yield return new WaitForSeconds(3);
        }
        else
        {
            yield return new WaitForSeconds(1);
        }

        while (true)
        {
            int spawnNum = Random.Range(0, 35);
            if (spawnNum < 27)
            {
                float[] usedY = new float[] { -1f, -1f, -1f };
                //spawn up to 3 donuts at once
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

                    //spawn new donut
                    spawn = Instantiate(DonutPrefab);

                    //set parent to this object
                    spawn.transform.parent = this.transform;

                    //determine if starting location is left or right
                    if (Random.Range(0, 2) == 0) //started left
                    {
                        startX = startX * -1;
                    }

                    //start at a y between LowestY and HighestY on a 0.25
                    float startY = Random.Range(LowestY, HighestY) * 4;
                    startY = Mathf.Round(startY);
                    startY = startY/ 4;

                    //don't allow donuts to share same y coords
                    while (usedY[0] == startY || usedY[1] == startY)
                    {
                        startY = Random.Range(LowestY, HighestY) * 4;
                        startY = Mathf.Round(startY);
                        startY = startY/ 4;
                    }
                    usedY[i] = startY;
                    spawn.transform.position = new Vector3(startX, startY, 0);

                    if (Mathf.Abs(spawn.transform.Find("Donut Predictor Right").transform.position.x) >
                        Mathf.Abs(spawn.transform.Find("Donut Predictor Left").transform.position.x))
                    {
                        //get left predictor and destroy right
                        predictor = spawn.transform.Find("Donut Predictor Left").gameObject;
                        Destroy(spawn.transform.Find("Donut Predictor Right").gameObject);
                    }
                    else
                    {
                        //get left predictor and destroy right
                        predictor = spawn.transform.Find("Donut Predictor Right").gameObject;
                        Destroy(spawn.transform.Find("Donut Predictor Left").gameObject);
                    }

                    //set end location direction
                    spawn.GetComponentInChildren<DonutMovement>().DirectionMultiplier = (int)Mathf.Clamp(startX, -1, 1) * -1;

                    //get donut type
                    int[] donutInfo = GetDonutInfo(Random.Range(0, 50150));
                    //sets donut type in donut and predictor
                    spawn.GetComponentInChildren<DonutMovement>().Setup(donutInfo[0], donutInfo[1]);
                    predictor.GetComponent<Predictor>().Setup(donutInfo[0]);
                }
            }
            else if (spawnNum < 29) //is a collectable
            {
                //spawn new collectable
                spawn = Instantiate(CollectablePrefab);

                //set parent to this object
                spawn.transform.parent = this.transform;

                GameObject predictor;

                //determine if starting location is left or right
                if (Random.Range(0, 2) == 0) //started left
                {
                    startX = startX * -1;
                }

                //start at a y between LowestY and HighestY on a 0.25
                float startY = Random.Range(LowestY, HighestY) * 4;
                startY = Mathf.Round(startY);
                startY = startY/ 4;
                spawn.transform.position = new Vector3(startX, startY, 0);

                if (Mathf.Abs(spawn.transform.Find("Collectable Predictor Right").transform.position.x) >
                    Mathf.Abs(spawn.transform.Find("Collectable Predictor Left").transform.position.x))
                {
                    //get left predictor and destroy right
                    predictor = spawn.transform.Find("Collectable Predictor Left").gameObject;
                    Destroy(spawn.transform.Find("Collectable Predictor Right").gameObject);
                }
                else
                {
                    //get left predictor and destroy right
                    predictor = spawn.transform.Find("Collectable Predictor Right").gameObject;
                    Destroy(spawn.transform.Find("Collectable Predictor Left").gameObject);
                }

                //set end location direction
                spawn.GetComponentInChildren<CollectableMovement>().DirectionMultiplier = (int)Mathf.Clamp(startX, -1, 1) * -1;

                //sets collectable type in object and predictor
                int collectType = Random.Range(0, 4);
                spawn.GetComponentInChildren<CollectableMovement>().Setup(collectType);
                predictor.GetComponent<Predictor>().Setup(collectType);
            }
            else //spawn bird
            {
                //spawn new bird
                spawn = Instantiate(BirdPrefab);

                //set parent to this object
                spawn.transform.parent = this.transform;

                GameObject predictor;

                //determine if starting location is left or right
                if (Random.Range(0, 2) == 0) //started left
                {
                    startX = startX * -1;
                }

                //start at a y between LowestY and HighestY on a 0.25
                float startY = Random.Range(LowestY, HighestY) * 4;
                startY = Mathf.Round(startY);
                startY = startY/ 4;
                spawn.transform.position = new Vector3(startX, startY, 0);

                if (Mathf.Abs(spawn.transform.Find("Bird Predictor Right").transform.position.x) >
                    Mathf.Abs(spawn.transform.Find("Bird Predictor Left").transform.position.x))
                {
                    //get left predictor and destroy right
                    predictor = spawn.transform.Find("Bird Predictor Left").gameObject;
                    Destroy(spawn.transform.Find("Bird Predictor Right").gameObject);
                }
                else
                {
                    //get left predictor and destroy right
                    predictor = spawn.transform.Find("Bird Predictor Right").gameObject;
                    Destroy(spawn.transform.Find("Bird Predictor Left").gameObject);
                }

                //set end location direction
                spawn.GetComponentInChildren<BirdMovement>().DirectionMultiplier = (int)Mathf.Clamp(startX, -1, 1) * -1;

                //sets bird color in object and predictor
                int color = Random.Range(0, 5);
                spawn.GetComponentInChildren<BirdMovement>().Setup(color);
                predictor.GetComponent<Predictor>().Setup(color);
            }
            //every 0.1 - 5 seconds spawn a new donut
            yield return new WaitForSeconds(Random.Range(0.1f, 4f));
        }
    }

    //get donut type (sprite and score) accoring to num
    int[] GetDonutInfo(int num)
    {
        if (num > 50100)
        {
            return new int[] { 11, 1000 };
        }
        else if (num > 50000)
        {
            return new int[] { 10, 750 };
        }
        else if (num > 49500)
        {
            return new int[] { 9, 500 };
        }
        else if (num > 48500)
        {
            return new int[] { 8, 400 };
        }
        else if (num > 47000)
        {
            return new int[] { 7, 300 };
        }
        else if (num > 44500)
        {
            return new int[] { 6, 250 };
        }
        else if (num > 41500)
        {
            return new int[] { 5, 100 };
        }
        else if (num > 37500)
        {
            return new int[] { 4, 50 };
        }
        else if (num > 32500)
        {
            return new int[] { 3, 10 };
        }
        else if (num > 25000)
        {
            return new int[] { 2, 5 };
        }
        else if (num > 15000)
        {
            return new int[] { 1, 2 };
        }
        else
        {
            return new int[] { 0, 1 };
        }
    }
}
