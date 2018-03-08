﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutSpawner : MonoBehaviour
{

    public GameObject DonutPrefab;
    public float StartXRight, StartXLeft;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 5f));

            GameObject donut = Instantiate(DonutPrefab);
            donut.transform.parent = this.transform;

            float startY = Random.Range(-2.5f, 2) * 10;
            startY = Mathf.Round(startY);
            startY = startY / 10;
            donut.transform.position = new Vector3(StartXRight, startY, 0);

            int[] donutInfo = GetDonutInfo(Random.Range(0, 50150));
            donut.GetComponent<DonutMovement>().Setup(donutInfo[0], donutInfo[1]);
        }
    }

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
