using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEffect : MonoBehaviour
{
    // Start is called before the first frame update

    Vector2 floatY;
    float originalY;

    public float floatStrength;

    void Start ()
    {
        this.transform.position += new Vector3(0, 0.3f, 0);
        this.originalY = this.transform.position.y;
    }

    void Update () {
        floatY = transform.position;
        floatY.y = originalY + (Mathf.Sin(Time.time * 3) * -floatStrength);
        transform.position = floatY;
    }       
}
