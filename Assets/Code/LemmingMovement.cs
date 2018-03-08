using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMovement : MonoBehaviour
{
    public Vector3 LowerLocation, UpperLocation;
    public LemmingMovement OtherLemming;
    public float Speed;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        //if player clicks or presses screen and lemming is currently at top
        if (Input.GetMouseButtonDown(0) && Vector3.Distance(transform.position, UpperLocation) < 0.05f)
        {
            StartCoroutine(MoveTo(LowerLocation, true));
        }
    }

    public IEnumerator MoveTo(Vector3 target, bool isMainLemming)
    {
        anim.SetBool("isJumping", true);
        yield return new WaitForSeconds(0.1f);

        while (Vector3.Distance(transform.position, target) > 0.05f)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            yield return new WaitForFixedUpdate();
        }

        anim.SetBool("isJumping", false);

        if (isMainLemming)
        {
            StartCoroutine(OtherLemming.MoveTo(OtherLemming.UpperLocation, false));
        }
    }
}
