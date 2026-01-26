using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private Vector2 Direction = new Vector2(1f,0f);
    [SerializeField] private float Speed = 1f;
    [SerializeField] private float Jump = 1f;
    
    private Rigidbody2D BirdTransform;

    private void Start()
    {
        BirdTransform = GetComponent<Rigidbody2D>();
        BirdTransform.velocity = Direction * Speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Vector2 temp = BirdTransform.velocity;                          // remove acceleration in y
            temp.y = 0f;
            BirdTransform.velocity = temp;
            BirdTransform.AddForce(Vector2.up * Jump, ForceMode2D.Impulse); // add force for jump
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) {  
            Direction = -Direction;                         // after collision with wall changing direction
            Vector2 temp = BirdTransform.velocity;          // make velocity in X zero to get it ready to add velocity in opposite direction
            temp.x = 0f;                                    // use temp because i can't change it directly from the variable
            BirdTransform.velocity = temp;
            BirdTransform.velocity += Direction * Speed;    // add velocity to change direction into opposite
        }
    }
}