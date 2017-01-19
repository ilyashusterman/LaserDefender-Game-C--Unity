using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float health = 150;
    public GameObject projectile;
    public float projectileSpeed =10f;
    public float firingRate = 0.5f;
    public int scoreValue = 150;
    private Score score;
    public AudioClip fireSound;
    public AudioClip deathSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile) {
            float damage = missile.getDamage();
            health -= damage;
            if (damage <= 130)
            {
                handleTextDamage(damage);
            }else
            {
                handleTextCriticleDamage(damage);
            }
            missile.hit();
            if (health <= 0) {
                fire();
                die();
            }
        //    Debug.Log("hit!");
        }
       // Destroy(collision.gameObject);
      //  Destroy(gameObject);
    }
    void die() {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                Destroy(gameObject);
                score.setScore(scoreValue);
    }

    void handleTextDamage(float damage)
    {
        int damageText = ((int)damage);
        FloatingtextController.createFloatingtext(damageText.ToString(), transform);
    }
    void handleTextCriticleDamage(float damage)
    {
        int damageText = ((int)damage);
        FloatingtextController.createCriticalText(damageText.ToString(), transform);
    }

    void fire(){
        GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Start()
    {
       score =  GameObject.Find("Score").GetComponent<Score>();
        FloatingtextController.initialize();
    }

     void Update()
    {
        float probability = Time.deltaTime * firingRate;
        if(UnityEngine.Random.value < probability){
            fire();
        }
    }

}
