using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFSMState
{
    public void Enter();
    public void Exit();
    public void Update();
}
