using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
    void Update();
}

public enum CharacterState
{
    Idle,
    Running,
    Jumping,
    Sprint
}