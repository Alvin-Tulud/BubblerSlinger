using UnityEngine;

public class startState : State
{
    idleState idle;

    void Start()
    {
        idle = GetComponent<idleState>();
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
}
