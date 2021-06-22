using System.Collections;
using System.Collections.Generic;
using static System.Math;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class FlyingBrain : MonoBehaviour
{
    System.Random rand = new System.Random(System.Environment.TickCount);
    int rotationspeed;
    int force;
    int rotation;


    void Start()
    {
        force = rand.Next(250, 401);
        rotationspeed = rand.Next(200, 601);
        transform.rotation = Quaternion.Euler(0, 0, rand.Next(-180, 0));
        GetComponent<Rigidbody2D>().AddForce(transform.right * 250, ForceMode2D.Force);
    }

    void Update()
    {
        if (Abs(transform.position.x) > 6 || Abs(transform.position.y) > 8)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1), rotationspeed * Time.deltaTime);
        }
    }
}
