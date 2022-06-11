using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public  Rigidbody2D rigidBody;
    public  SpringJoint2D springJoint;
    public  GameObject greenmushPrefab; 
    public GameObject redmushPrefab;
    public  SpriteRenderer spriteRenderer;
    public  Sprite usedQuestionBox; // the sprite that indicates empty box instead of a question mark

    public Vector3 initialPosition;

    private bool hit =  false;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnCollisionEnter2D(Collision2D col)
    {
        GameObject consumablePrefab = Random.Range(0, 2) ==  0  ?  greenmushPrefab  :  redmushPrefab;
        if (col.gameObject.CompareTag("Player") &&  !hit){
            hit  =  true;
            // ensure that we move this object sufficiently 
            rigidBody.AddForce(new  Vector2(0, rigidBody.mass*20), ForceMode2D.Impulse);
            // spawn mushroom
            Instantiate(consumablePrefab, new  Vector3(this.transform.position.x, this.transform.position.y  +  1.0f, this.transform.position.z), Quaternion.identity);
            // begin check to disable object's spring and rigidbody
            StartCoroutine(DisableHittable());
        }
    }

    bool  ObjectMovedAndStopped(){
        return  Mathf.Abs(rigidBody.velocity.magnitude)<0.01;
    }

    IEnumerator  DisableHittable(){
        if (!ObjectMovedAndStopped()){
            yield  return  new  WaitUntil(() =>  ObjectMovedAndStopped());
        }

        //continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite  =  usedQuestionBox; // change sprite to be "used-box" sprite
        rigidBody.bodyType  =  RigidbodyType2D.Static; // make the box unaffected by Physics

        //reset box position
        this.transform.localPosition  =  initialPosition;
        springJoint.enabled  =  false; // disable spring
    }
}
