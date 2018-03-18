using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public PlayerStats Player;

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialFinised", 0) == 1)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Player.TouchEnabled && Input.mousePosition.y < 1750)
        {
            PlayerPrefs.SetInt("TutorialFinised", 1);
            Destroy(this.gameObject);
        }
    }
}
