using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predictor : MonoBehaviour
{
    public GameObject MainObject;
    public Sprite[] Sprites;

    //Set the information about donut after spawning
    public void Setup(int donutSprite)
    {
        //set the sprite
        this.GetComponent<SpriteRenderer>().sprite = Sprites[donutSprite];
    }

    private void Update()
    {
        if (Mathf.Abs(MainObject.transform.position.x - this.transform.position.x) < 1)
        {
            Destroy(this.gameObject);
        }
    }
}
