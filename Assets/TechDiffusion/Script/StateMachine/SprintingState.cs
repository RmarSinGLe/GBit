using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintingState : IState
{ 
    private StateMachine stateMachine;
    private static readonly int IsSprinting = Animator.StringToHash("IsSprinting");

    public SprintingState(StateMachine sm)
    {
        stateMachine = sm;
    }

    public void Enter()
    {
        stateMachine.Animator.SetBool(IsSprinting, true);
        Debug.Log("进入追逐状态");
    }

    public void Exit()
    {
        stateMachine.Animator.SetBool(IsSprinting, false);
        Debug.Log("退出追逐状态");
    }

    public void Update()
    {
        // todo
    }
}
