using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State currentState;
    private State nextState;
    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        nextState = currentState.RunCurrentState();

        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }

    private void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
