using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 movementSpeed = new Vector2(50, 50);

    private Rigidbody2D rb;
    public Camera cam;

    Vector2 mousePos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

 

    // Update is called once per frame
    void Update()
    {
       rb.velocity = Vector3.zero;
       mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movementSpeed.x * inputX, movementSpeed.y * inputY,0);

       

        movement *= Time.deltaTime;

        transform.Translate(movement,Space.World);

         Vector2 lookDir = mousePos - rb.position;
         float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
          rb.rotation = angle;  
    }
}
