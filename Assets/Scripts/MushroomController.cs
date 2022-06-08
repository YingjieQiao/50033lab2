using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public int speed;
    private Rigidbody2D mushBody;
    private Vector2 velocity;
    private Vector2 position;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        mushBody = GetComponent<Rigidbody2D>();
        mushBody.AddForce(Vector2.up  *  20, ForceMode2D.Impulse);
        position = mushBody.position;
        direction = new Vector2(-1,0);
    }

    // Update is called once per frame
    void Update()
    {
        position = mushBody.position;
        Vector2 nextPosition = position + speed * direction.normalized * Time.fixedDeltaTime;
        mushBody.MovePosition(nextPosition);
        // if (Mathf.Abs(velocity.x) > 0.05){
        //     // move mushroom
        //     MoveMushroom();
        // }else{
        //     // change direction
        //     ChangeVelocity();
        //     MoveMushroom();
        // }
    }

    // void ChangeVelocity(){
    //     velocity = new Vector2(-velocity.x, velocity.y);
    // }

    // void MoveMushroom(){
    //     mushBody.MovePosition(mushBody.position + velocity * Time.fixedDeltaTime);
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
    }

    void  OnBecameInvisible(){
        Destroy(gameObject);	
    }
}
