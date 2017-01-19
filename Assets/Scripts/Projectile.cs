using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

  //  private float damage = 100f;
	// Use this for initialization
	public void hit () {
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	public float getDamage () {
        float number =  Random.Range(50.0f, 150.0f);
        //Debug.Log(number);
        return number;
	}
}
