using UnityEngine;

public class dieState : State
{
    startState start;

    void Start()
    {
        start = GetComponent<startState>();
    }

    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        return this;
    }
}
