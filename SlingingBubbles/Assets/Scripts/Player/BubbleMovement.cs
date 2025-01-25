using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleMovement : MonoBehaviour
{
    private bool flingOK;
    private Vector2 flingVector;
    private bool updatingFling;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flingOK = false;
        flingVector = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if(updatingFling)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 bubblePos = gameObject.transform.position;
            flingVector = point - bubblePos;
        }
    }

    public void testMouse(InputAction.CallbackContext context)
    {
        //Debug.Log("sup lol: " + context.phase);

        if(context.started)
        {
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
            }
        }

        else if (context.canceled)
        {
            updatingFling = false;
            //if flingOK: fling w/ the vector
            //if the fling isn't strong enough, don't fling - prevents accidental flings
            //can edit 1 if you want to make the dead zone larger/smaller
            float m = flingVector.magnitude;
            if (flingOK && (m > 1))
            {

                Debug.Log("flinging w/ magnitude: " + m);
                //Restricts max fling force to 7 - change if u want
                m = Mathf.Clamp(m, 1f, 7f);
                Debug.Log("magnitude clamped to " + m);


                gameObject.GetComponent<Rigidbody2D>().AddForce(flingVector.normalized * m * -1, ForceMode2D.Impulse);
                flingVector = new Vector2();
                flingOK = false;

            }
            //
        }
    }
}
