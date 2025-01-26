using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dieState : State
{
    private Animator transition;

    private void Start()
    {
        transition = GameObject.FindWithTag("Transition").GetComponent<Animator>();
    }

    public override State ChangeState()
    {
        return this;
    }

    public override State RunCurrentState()
    {
        //when win state is triggered play the exit anim and switch to next level
        StartCoroutine(playTransition());
        return this;
    }

    IEnumerator playTransition()
    {
        transition.SetBool("isStart", false);
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
