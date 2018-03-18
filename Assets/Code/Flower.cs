using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

	void Awake()
    {
        this.GetComponent<Animator>().SetInteger("setColor", Random.Range(0, 6));
    }
}
