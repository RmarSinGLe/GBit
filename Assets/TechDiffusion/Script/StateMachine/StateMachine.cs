using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<CharacterState, IState> states;
    private CharacterState currentState;
    private Animator animator;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        states = new Dictionary<CharacterState, IState>();
        currentState = CharacterState.Idle;

        // 初始化状态
        states.Add(CharacterState.Idle, new IdleState(this));
        states.Add(CharacterState.Running, new RunningState(this));
        states.Add(CharacterState.Jumping, new JumpingState(this));
        states.Add(CharacterState.Sprint,new SprintingState(this));
    }

    private void Start()
    {
        states[currentState].Enter();
    }

    private void Update()
    {
        states[currentState].Update();
    }

    public void ChangeState(CharacterState newState)
    {
        if (currentState != newState)
        {
            states[currentState].Exit();
            currentState = newState;
            states[newState].Enter();
        }
    }

    public Animator Animator
    {
        get => animator;
    }

    public CharacterState CurrentState
    {
        get => currentState;
    }
}

// 使用方法
// stateMachine.ChangeState(CharacterState.Idle);