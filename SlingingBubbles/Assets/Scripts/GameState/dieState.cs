using UnityEngine;
using UnityEngine.SceneManagement;

public class dieState : State
{
    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        //when die state is triggered play the exit anim and reload level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return this;
    }
}
