using UnityEngine;

public class idleState : State
{
    dieState die;
    winState win;
    //get player gameobject

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
