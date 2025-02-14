using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class idleState : State
{
    private dieState die;
    private winState win;
    private BubbleMovement PlayerScript;
    //get player gameobject

    void Start()
    {
        die = GetComponent<dieState>();
        win = GetComponent<winState>();

        PlayerScript = GameObject.FindWithTag("Player").GetComponent<BubbleMovement>();
    }

    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        if (PlayerScript.getIsDead() || PlayerScript.getFlingCount() == 0)
        {
            return die;
        }

        else if (PlayerScript.getIsWon())
        {
            return win;
        }

        return this;
    }
}
