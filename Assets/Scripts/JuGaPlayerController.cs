using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuGaPlayerController : MonoBehaviour {

	private AudioSource source;
	private Rigidbody2D rb2d;
	private bool facingRight = true;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;

	public AudioClip jumpClip;
	public AudioClip foodClip;
	public AudioClip bonusClip;
	public AudioClip badClip;
	public AudioClip deadlyClip;
	public AudioClip finishClip;
	public AudioClip winClip;
	public AudioClip loseClip;
	//ground check
	private bool isOnGround;
	private bool EndSound;

	public int count; 
	public float speed;
	public float jumpForce;
	public Transform groundcheck;
	public float checkRadius;
	public LayerMask allGround;
	public Text countText;
	public Text timerText;
	public Text endText;
	
	private float timer;
    private int wholetime;

	//private float jumpTimeCounter;
	//public float jumpTime;
	//private bool isJumping;

	//audio

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		count = 0;
		SetCountText ();
		EndSound = false;
		endText.text = "";
	}

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}
		
	// Update is called once per frame
	private void Update () {
		if (Input.GetKey("escape"))
			Application.Quit();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2 (moveHorizontal, 0);

		//rb2d.AddForce(movement * speed);
		rb2d.velocity = new Vector2 (moveHorizontal * speed, rb2d.velocity.y);
		isOnGround = Physics2D.OverlapCircle (groundcheck.position, checkRadius, allGround);
		Debug.Log (isOnGround);

		if (facingRight == false && moveHorizontal > 0) {
			Flip ();
		} else if (facingRight == true && moveHorizontal < 0) {
			Flip ();
		}
		
		timer = timer + Time.deltaTime;
		{

			if (timer >= 10) {					
				source.PlayOneShot (finishClip);
				if (count < 10) {			
					endText.text = "YOU LOSE!";
					//gameObject.SetActive (false);
					StartCoroutine (ByeAfterDelay (2));
					//source.PlayOneShot (loseClip);

				} else if (count >= 10) {
					//... then set the text property of our winText object to "You win!"
					//  winText.text = "You win!";				
					endText.text = "YOU WIN!";
					StartCoroutine (ByeAfterDelay (2));	
					//source.PlayOneShot (winClip);				
				}

			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (timer <= 10) {
			if (other.gameObject.CompareTag ("GoodFood")) {
				count = count + 1;
				other.gameObject.SetActive (false);
				//GameLoader.AddScore(count);
				SetCountText ();
				source.PlayOneShot (foodClip);

			} else if (other.gameObject.CompareTag ("BonusFood")) {
				count = count + 5;
				other.gameObject.SetActive (false);
				//GameLoader.AddScore(count);
				SetCountText ();
				source.PlayOneShot (bonusClip);
			} else if (other.gameObject.CompareTag ("BadFood")) {
				count = count - 1;
				other.gameObject.SetActive (false);
				//GameLoader.AddScore(count);
				SetCountText ();
				source.PlayOneShot (badClip);
			} else if (other.gameObject.CompareTag ("DeadlyFood")) {
				count = count - 3;
				other.gameObject.SetActive (false);
				//GameLoader.AddScore(count);
				SetCountText ();
				source.PlayOneShot (deadlyClip);
			}
		}
	}

void Flip()
	{
		facingRight = !facingRight;
		Vector2 Scalar = transform.localScale;
		Scalar.x = Scalar.x * -1;
		transform.localScale = Scalar;
	}

	void SetCountText ()
	{
		countText.text = "Score x " + count.ToString ();
		
	  //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
      //  countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...

	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Ground" && isOnGround)
		{
			if (Input.GetKey (KeyCode.UpArrow)) 
		    {
				//rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
				rb2d.velocity = Vector2.up * jumpForce;
				float vol = Random.Range(volLowRange, volHighRange);
				source.PlayOneShot(jumpClip);
			} 
		}
	}
		
	IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        //GameLoader.gameOn = false;
    }
}
