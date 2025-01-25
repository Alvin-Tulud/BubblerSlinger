using UnityEngine;

public class idleState : State
{
    dieState die;
    winState win;

    void Start()
    {
        die = GetComponent<dieState>();
        win = GetComponent<winState>();
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
