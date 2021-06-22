using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRotateScript : MonoBehaviour
{
    System.Random rand = new System.Random();
    GameObject fish;
    public int rotation;
    void Start()
    {
        rotation = 270;
        fish = transform.GetChild(0).gameObject;
        RotateFishes();
    }
    public void RotateFishes()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90 * rand.Next(4));
        rotation = 90 * rand.Next(4);
        fish.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
