using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed=15;
    private float minX, maxX;
    public float padding=0.49f ;
    public GameObject projectile;
    public float projectileSpeed;
    public float firingRate=0.2f;
    public float maxHealth = 250;
    public float health = 250;
    public AudioClip fireSound;
    public GameObject healthBar;

    // Use this for initialization
    void Start () {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost= Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftMost.x + padding;
        maxX = rightMost.x - padding;
    }

    // Update is called once per frame
    void Update() {
        handleInput();
    }
    void fire(){
       // Vector3 startingPosition = transform.position ;//+ new Vector3(0, 1f, 0)
            GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0); // rigidbody2D.velocity = new Vector3(0 , projectileSpeed, )
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            InvokeRepeating("fire",0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow)){
            transform.Translate(Vector3.right * Time.deltaTime * speed);  
        }
        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision){
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
       //     Debug.Log("Player hit!");
            health -= missile.getDamage();
            missile.hit();
            setHealthBar();
            if (health <= 0)
            {
                die();
            }
            //    Debug.Log("hit!");
        }
        // Destroy(collision.gameObject);
        //  Destroy(gameObject);
    }
    void die()
    {
        LevelManager levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();        
        Destroy(gameObject);
        levelManager.LoadLevel("Win Screen");
    }
         void setHealthBar(){
        float currentHealth = this.health / this.maxHealth;
     //   Debug.Log("health = " + health + "maxHealth = " + maxHealth + " currentHealth = " + currentHealth);
        healthBar.transform.localScale = new Vector3(currentHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

}
