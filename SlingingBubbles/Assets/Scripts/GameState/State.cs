using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract State RunCurrentState();

    public abstract State ChangeState();
}
