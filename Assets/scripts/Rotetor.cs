using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotet : MonoBehaviour
{
    public float speed = 50;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
