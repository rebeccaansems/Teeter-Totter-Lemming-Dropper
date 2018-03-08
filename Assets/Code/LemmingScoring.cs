using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingScoring : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Score")
        {
            Debug.Log("!");
        }
    }
}
