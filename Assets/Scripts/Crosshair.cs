using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, Input.mousePosition, delay * Time.deltaTime);
    }
}
