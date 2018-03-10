using UnityEngine;

public class Predictor : MonoBehaviour
{
    public GameObject MainObject;
    public Sprite[] Sprites;

    //set the sprite for predictor
    public void SetSprite(int donutSprite)
    {
        this.GetComponent<SpriteRenderer>().sprite = Sprites[donutSprite];
    }

    private void Update()
    {
        //if real object is within 1 point
        if (Mathf.Abs(MainObject.transform.position.x - this.transform.position.x) < 1)
        {
            //destroy this object
            Destroy(this.gameObject);
        }
    }
}
