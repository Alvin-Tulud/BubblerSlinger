using UnityEngine;

public class startState : State
{
    idleState idle;

    public int flingCount;

    private BubbleMovement playerScript;

    void Start()
    {
        idle = GetComponent<idleState>();

        playerScript = GameObject.Find("PlayerBubble").GetComponent<BubbleMovement>();
    }

    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        return idle;
        //return this;
    }

    public void SetCount(int count)
    {
        playerScript.setFling(count);
    }
}
