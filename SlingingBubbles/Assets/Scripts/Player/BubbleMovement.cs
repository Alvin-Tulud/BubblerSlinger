using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool flingOK;
    private Vector2 flingVector;
    private bool updatingFling;

    private bool isDead;
    private bool isWon;
    private bool decelerating;

    private GameObject aimer;
    private SpriteRenderer aimSprite;
    private GameObject aimer2;
    private SpriteRenderer aimSprite2;

    [SerializeField] private int maxFlingCount;
    [SerializeField] private int currentFlingCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        flingOK = false;
        flingVector = new Vector2();

        isDead = false;
        isWon = false;
        decelerating = false;

        aimer = gameObject.transform.Find("aimer").gameObject;
        aimSprite = aimer.GetComponent<SpriteRenderer>();

        //This one is for an indicator halfway between the two of them
        aimer2 = gameObject.transform.Find("midAimer").gameObject;
        aimSprite2 = aimer2.GetComponent<SpriteRenderer>();

        rb.linearDamping = 1.3f;
    }

    public int getFlingCount()
    {
        return currentFlingCount;
    }

    public void setFling(int count)
    {
        //I assume the bubble counts down to 0 from max?
        maxFlingCount = count;
        currentFlingCount = count;
    }

    // Update is called once per frame
    void Update()
    {
        if(updatingFling)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bubblePos = gameObject.transform.position;
            flingVector = point - bubblePos;
            float m = flingVector.magnitude;

            if(m < 1) //no fling if not strong enough
            {
                flingVector = new Vector2();
                aimSprite.enabled = false;
                aimSprite2.enabled = false;
            }
            else //restricts fling to max strength
            {
                m = Mathf.Clamp(m, 1f, 7f);
                flingVector = flingVector.normalized * m;
                aimSprite.enabled = true;
                aimSprite2.enabled = true;
                Vector3 v = gameObject.transform.position - (Vector3)flingVector;
                aimer.transform.position = v;
                v = gameObject.transform.position - (Vector3)(flingVector.magnitude/2 * flingVector.normalized);
                aimer2.transform.position = v;
            }
        }
    }

    private void FixedUpdate()
    {
        //If the bubble hits a normal wall it will slow down till it stops
        if(decelerating)
        {
            rb.linearVelocity = rb.linearVelocity * 0.9f;

            if(rb.linearVelocity.magnitude < 0.05f)
            {
                Debug.Log("slowed far enough, now stopping");
                decelerating = false;
            }
        }

        if(currentFlingCount == 0)
        {
            if(rb.linearVelocity.magnitude == 0)
            {
                Debug.Log("flings = 0, change this logic w/ your stuff");
                currentFlingCount = 1;
                //^Just here for testing
            }

        }
    }

    public void testMouse(InputAction.CallbackContext context)
    {
        //Debug.Log("sup lol: " + context.phase);

        if(context.started)
        {
            rb.linearVelocity = Vector2.zero;

            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.GetComponent<Collider2D>().OverlapPoint(point))
            {
                Debug.Log("mouse touching");
                flingOK = true;
                //set velocity to 0?
            }
        }

        else if (context.performed)
        {
            //if flingOK: start tracking the vector to fling the bubble with
            if(flingOK)
            {
                updatingFling = true;
                aimSprite.enabled = true;
            }
        }

        else if (context.canceled)
        {
            aimSprite.enabled = false;
            aimSprite2.enabled = false;
            updatingFling = false;
            //if flingOK: fling w/ the vector
            //if the fling isn't strong enough, don't fling - prevents accidental flings
            //can edit 1 if you want to make the dead zone larger/smaller
            float m = flingVector.magnitude;
            if (flingOK && (m > 1))
            {

                Debug.Log("flinging w/ magnitude: " + m);
                //Restricts max fling force to 7 - change if u want
                //*technically reduntant but don't have time to change rn
                m = Mathf.Clamp(m, 1f, 7f);
                Debug.Log("magnitude clamped to " + m);


                rb.AddForce(flingVector * -1.6f, ForceMode2D.Impulse);
                flingVector = new Vector2();



                //Andrew:
                //I added this to decrease the bubble's fling count per successful fling, you can edit this if needed
                currentFlingCount -= 1;
            }
            flingOK = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //layer 6 is spike, 7 is wall, 11 is bouncewall
        switch (collision.gameObject.layer)
        {
            case 6:
                rb.linearVelocity = Vector2.zero;
                isDead = true;
                break;
            case 7: //wall
                //rb.linearVelocity = Vector2.zero;
                decelerating = true;
                break;
            case 9:
                isWon = true;
                decelerating = true;
                break;
            case 11:
                //do the bouncing stuff here idk

                //gonna just multiply its velocity when it hits a bounce pad, if this doesn't feel good I'll just make it give u a set velocity
                rb.linearVelocity = rb.linearVelocity * 1.6f;
                break;
            default:
                break;
        }
    }

    public bool getIsDead() { return isDead; }

    public bool getIsWon() { return isWon; }
}
