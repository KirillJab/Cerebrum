using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSpawner : MonoBehaviour
{
    public GameObject brain;
    System.Random rand = new System.Random();
    public double timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            spawnBrain();
            timer = rand.NextDouble() + rand.Next(1, 3);
        }
        timer -= Time.deltaTime;
    }

    private void spawnBrain()
    {
        GameObject obj = Instantiate(brain, transform.position, Quaternion.identity);
    }
}
