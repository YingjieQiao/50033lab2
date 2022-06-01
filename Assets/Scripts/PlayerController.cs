using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    public float speed;
    public float upSpeed;
    public float maxSpeed = 10;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private Rigidbody2D marioBody;
    private Animator marioAnimator;
    private AudioSource marioAudio;

    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
          faceRightState = false;
          marioSprite.flipX = true;

          if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
            	marioAnimator.SetTrigger("onSkid");
          }
        }

        if (Input.GetKeyDown("d") && !faceRightState){
          faceRightState = true;
          marioSprite.flipX = false;

          if (Mathf.Abs(marioBody.velocity.x) >  1.0) {
            	marioAnimator.SetTrigger("onSkid");
          }
        }

        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              countScoreState = false;
              score++;
          }
        }
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
          Vector2 movement = new Vector2(moveHorizontal, 0);
          if (Mathf.Abs(marioBody.velocity.x) < maxSpeed)
                  marioBody.AddForce(movement * speed);
        } else {
          marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            marioBody.velocity.Set(0, marioBody.velocity.y);
        }
        // jump
        if (Input.GetKeyDown("space") && onGroundState)
        {
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          Debug.Log("onGroundState {0}" + onGroundState);
          countScoreState = true; //check if Gomba is underneath
        }

        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles"))
        {
          onGroundState = true; // back on ground
          // marioAnimator.SetBool("onGround", onGroundState);
          countScoreState = false; // reset score state
          scoreText.text = "Score: " + score.ToString();
        };
    }

    void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.CompareTag("Enemy"))
      {
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      }
    }

    void  PlayJumpSound(){
      marioAudio.PlayOneShot(marioAudio.clip);
    }

}
