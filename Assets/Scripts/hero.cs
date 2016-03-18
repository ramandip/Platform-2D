using UnityEngine;
using System.Collections;



public class VelocityRange
{
    // PUBLIC INSTANCE VARIABLES ++++
    public float minimum;
    public float maximum;

    // CONSTRUCTOR ++++++++++++++++++++++++++++++++++++
    public VelocityRange(float minimum, float maximum)
    {
        this.minimum = minimum;
        this.maximum = maximum;
    }
}

public class hero : MonoBehaviour {
    //PUBLIC VARIABLES (Set from Unity editor)
    public VelocityRange velocityRange;
    private int score = 100;
    public float force = 10f;
    public float jumpForce = 500f;
    public Camera camera;
    public Transform groundCheck;
    public progressMap pm;
    public coin coin;

    //PRIVATE VARIABLES
    private Rigidbody2D _rigidBody2D;
    private Transform _transform;
    private bool isFacingRight = true;
    private Animator _animator;

    // Use this for initialization
    void Start()
    {

        //Initialize Provate variables
        _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();

        // Initialize Public Instance Variables
        this.velocityRange = new VelocityRange(300f, 30000f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //check if hero is grounded
        bool isGrounded = Physics2D.Linecast(
            _transform.position,
            groundCheck.position,
            1 << LayerMask.NameToLayer("Ground")
            );

        Debug.DrawLine(this._transform.position, this.groundCheck.position);

        float xforce = 0f;
        float yforce = 0f;

        //make sure that the camera is following the hero
        Vector3 currentPosition =
                new Vector3(this._transform.position.x, this._transform.position.y, -10f);
        this.camera.transform.position = currentPosition;

        //we can walk or jump only if we are on the ground
        if (isGrounded)
        {
            //value -1 to 1
            float move = Input.GetAxis("Horizontal");
            float jump = Input.GetAxis("Vertical");

            // get absolute value of velocity for our gameObject
            float absVelX = Mathf.Abs(this._rigidBody2D.velocity.x);
            float absVelY = Mathf.Abs(this._rigidBody2D.velocity.y);


            if (move != 0)
            {
                if (move > 0)
                {
                    isFacingRight = true;
                    if (absVelX < velocityRange.maximum)
                    {
                        xforce = force;
                    }
                }
                else if (move < 0)
                {
                    isFacingRight = false;
                    if (absVelX < velocityRange.maximum)
                    {
                        xforce = -force;
                    }
                }
                _animator.SetInteger("state", 1);
            }
            else
            {
                _animator.SetInteger("state", 0);
            }
            //flip This can be a separate function
            if (isFacingRight)
            {
                _transform.localScale = new Vector2(0.04f,0.04f);
            }
            else
            {
                _transform.localScale = new Vector2(-0.04f, 0.04f);
            }

            if (jump > 0)
            {
                if (absVelY < velocityRange.maximum)
                {
                    yforce = jumpForce;
                }
                _animator.SetInteger("state", 2);
            }

            _rigidBody2D.AddForce(new Vector2(xforce, yforce));
        }
    }

    //increase score
    void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.gameObject.CompareTag("coin"))
        {
            
            this.pm.totalScores += score;

        }

        if (other.gameObject.CompareTag("Finish"))
        {

            this.pm.finish();

        }

        if (other.gameObject.CompareTag("Death"))//or check player transform property on y-axis if it is less than suppose -6.3f(in my game) under fixed update, player loose on life
        {
            
            this._spawn();
        }
    }

    //reset the position
    private void _spawn()
    {
        if (this.pm.livesValue == 0)
        {
            pm.endGame();
        }
        else
        {
            this._transform.position = new Vector3(-1.35f, 5.5f, 0);
            pm.livesValue -= 1;
            

        }
        
    }

    
    
}