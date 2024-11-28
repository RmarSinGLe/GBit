using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RunningState : IState
{
    private StateMachine stateMachine;
    private static readonly int IsRuning = Animator.StringToHash("IsRuning");

    public RunningState(StateMachine sm)
    {
        stateMachine = sm;
    }

    public void Enter()
    {
        stateMachine.Animator.SetBool(IsRuning, true);
        Debug.Log("进入行走状态");
    }

    public void Exit()
    {
        stateMachine.Animator.SetBool(IsRuning, false);
        Debug.Log("退出行走状态");
    }

    public void Update()
    {
        // todo
    }
}
