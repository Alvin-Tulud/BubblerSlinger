using UnityEngine;
using UnityEngine.SceneManagement;

public class winState : State
{
    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        //when win state is triggered play the exit anim and switch to next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        return this;
    }
}
