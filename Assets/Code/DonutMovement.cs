using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutMovement : MonoBehaviour
{

    public float Speed;
    public float EndXRight, EndXLeft;
    public Sprite[] DonutSprites;

    private int score;
    private Vector3 endLocation;
    
    public void Setup(int donutSprite, int score)
    {
        endLocation = new Vector3(EndXLeft, this.transform.position.y, 0);
        this.GetComponent<SpriteRenderer>().sprite = DonutSprites[donutSprite];
        this.score = score;
    }

    void Update()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endLocation, step);

        if (this.transform.position.x - endLocation.x < 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
