using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float speed = 2;
    private float minX, maxX;
    public float padding = 3.444f;
    public GameObject enemyPrefab;
    public float width= 10f, height=8f;
    private bool movingRight = true;
    public float spawnDelaySeconds = 1f;
    // Use this for initialization
    void Start () {
        spawnEnemies();
        handlePostion();
    }
    void SpawnUntilFull()
    {
        Transform freePos = NextFreePosition();
        GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
        enemy.transform.parent = freePos;
        if (FreePositionExists())
        {
            Invoke("SpawnUntilFull", spawnDelaySeconds);
        }
    }
    Transform NextFreePosition()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount <= 0)
            {
                return position;
            }
        }
        return null;
    }
    bool FreePositionExists()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount <= 0)
            {
                return true;
            }
        }
        return false;
    }
    void spawnEnemies()
    {
        foreach(Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void handlePostion() {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundry = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBoundry = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        minX = leftBoundry.x - padding;
        maxX = rightBoundry.x + padding;
    }

    public void OnDrawGizmos(){
        Gizmos.DrawWireCube(transform.position,new Vector3(width, height));
    }
    // Update is called once per frame
    void Update () {
        moveFormation();
        if (allMembersDead())
        {
            Debug.Log("all dead!");
            SpawnUntilFull();
        }
	}

     bool allMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount > 0) {
                return false;
            }
        }
            return true;
    }

    void moveFormation()
    {
        Vector3 direction =Vector3.zero;
        if (movingRight)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;
        }
        transform.Translate(direction * Time.deltaTime * speed);
        float rightEdgeformation = transform.position.x + (width * 0.5f);
        float leftEdgeformation = transform.position.x - (width * 0.5f);
        if (leftEdgeformation < minX)
        {
            movingRight = true;
        } else if (rightEdgeformation > maxX)
        {
            movingRight = false;
        }
        
        //float position = transform.position.x;
        //if (position == maxX)
        //{
        //    direction = Vector3.left;
        //}
        //else if (position == minX)
        //{
        //    direction = Vector3.right;
        //}
        //transform.Translate(direction * Time.deltaTime * speed);
        //float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        //transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
