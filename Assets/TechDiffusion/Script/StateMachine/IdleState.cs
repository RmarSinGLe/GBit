using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private StateMachine stateMachine;
    private static readonly int IsIdle = Animator.StringToHash("IsIdle");

    public IdleState(StateMachine sm)
    {
        stateMachine = sm;
    }

    public void Enter()
    {
        stateMachine.Animator.SetBool(IsIdle, true);
        Debug.Log("进入空闲状态");
    }

    public void Exit()
    {
        stateMachine.Animator.SetBool(IsIdle, false);
        Debug.Log("退出空闲状态");
    }

    public void Update()
    {
        
    }
}
