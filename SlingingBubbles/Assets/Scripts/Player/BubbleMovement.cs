using UnityEngine;
using UnityEngine.InputSystem;

public class BubbleMovement : MonoBehaviour
{
    private bool flingOK = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void testMouse(InputAction.CallbackContext context)
    {
        Debug.Log("sup lol: " + context.phase);

        if(context.started)
        {
            Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (gameObject.GetComponent<Collider2D>().OverlapPoint(point))
            {
                Debug.Log("mouse touching");
            }
        }

        else if (context.performed)
        {

        }

        else if (context.canceled)
        {

        }
    }
}
