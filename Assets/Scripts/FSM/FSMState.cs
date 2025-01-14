

using System.ComponentModel.Design;
using Unity.VisualScripting;

public class FSMState<T>
{

    public virtual void OnInit(FSM<T> fsm)
    {}

    public virtual void OnEnter(FSM<T> fsm)
    {}

    public virtual void OnUpdate(FSM<T> fsm)
    {}

    public virtual void OnLeave(FSM<T> fsm)
    {}

    protected virtual void ChangeState<TState>(FSM<T> fsm) where TState : FSMState<T>
    {
        fsm.ChangeState<TState>();
    }

}