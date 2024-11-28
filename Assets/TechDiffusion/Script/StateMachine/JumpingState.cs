using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : IState
{
    private StateMachine stateMachine;

    public JumpingState(StateMachine sm)
    {
        stateMachine = sm;
    }
    public void Enter()
    {
        stateMachine.Animator.SetBool("IsJumping", true);
        Debug.Log("进入跳跃状态");
    }

    public void Exit()
    {
        stateMachine.Animator.SetBool("IsJumping", false);
        Debug.Log("退出跳跃状态");
    }

    public void Update()
    {
        // todo
    }
}
