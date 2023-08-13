using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class movement : MonoBehaviour
{
    public float slowRate;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rb.drag += Time.deltaTime * slowRate;
            Debug.Log("worked");
            
        } else if (Input.GetMouseButtonUp(0))
        {
            rb.drag = 0;
        }
    }
}
