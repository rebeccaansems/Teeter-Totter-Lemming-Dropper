﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{

    public GameObject DonutPrefab, CollectablePrefab;
    public float StartX, LowestY, HighestY;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            //every 0.1 - 5 seconds spawn a new donut
            yield return new WaitForSeconds(Random.Range(0.1f, 5f));
            GameObject spawn;

            //1 in 30 chance of being a collectable instead of a donut
            if (Random.Range(0, 31) != 0)
            {
                //spawn new donut
                spawn = Instantiate(DonutPrefab);

                //set parent to this object
                spawn.transform.parent = this.transform;

                GameObject predictor;

                //determine if starting location is left or right
                float startX = StartX;
                if (Random.Range(0, 2) == 0) //started left
                {
                    startX = startX * -1;
                    predictor = spawn.transform.Find("Donut Predictor Left").gameObject;
                    Destroy(spawn.transform.Find("Donut Predictor Right").gameObject);

                }
                else //started right
                {
                    spawn.GetComponentInChildren<DonutMovement>().DirectionMultiplier = -1;
                    predictor = spawn.transform.Find("Donut Predictor Right").gameObject;
                    Destroy(spawn.transform.Find("Donut Predictor Left").gameObject);
                }

                //start at a y between LowestY and HighestY on a 0.2
                float startY = Random.Range(LowestY, HighestY) * 5;
                startY = Mathf.Round(startY);
                startY = startY / 5;
                spawn.transform.position = new Vector3(startX, startY, 0);

                //get donut type
                int[] donutInfo = GetDonutInfo(Random.Range(0, 50150));
                //sets donut type
                spawn.GetComponentInChildren<DonutMovement>().Setup(donutInfo[0], donutInfo[1]);
                predictor.GetComponent<DonutPredictor>().Setup(donutInfo[0]);
            }
            else //is a collectable
            {
                //spawn new collectable
                spawn = Instantiate(CollectablePrefab);

                //set parent to this object
                spawn.transform.parent = this.transform;

                //determine if starting location is left or right
                float startX = StartX;
                if (Random.Range(0, 2) == 0) //started left
                {
                    startX = startX * -1;
                }
                else //started right
                {
                    spawn.GetComponent<CollectableMovement>().DirectionMultiplier = -1;
                }

                //start at a y between LowestY and HighestY on a 0.2
                float startY = Random.Range(LowestY, HighestY) * 5;
                startY = Mathf.Round(startY);
                startY = startY / 5;
                spawn.transform.position = new Vector3(startX, startY, 0);

                //sets collectable type
                spawn.GetComponent<CollectableMovement>().Setup(Random.Range(0,4));
            }
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
