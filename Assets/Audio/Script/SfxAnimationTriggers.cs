using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxAnimationTriggers : MonoBehaviour
{

    private void EnemyDash() => AudioManager.instance.PlaySFX(AudioGlobals.EnemyDash);
    private void EnemyYawp() => AudioManager.instance.PlaySFX(AudioGlobals.EnemyYawp);
    private void Move() => AudioManager.instance.PlaySFX(AudioGlobals.Move);
    private void JumpFromGround() => AudioManager.instance.PlaySFX(AudioGlobals.JumpFromGround);
    private void Jump2() => AudioManager.instance.PlaySFX(AudioGlobals.Jump2);
    private void ToGround() => AudioManager.instance.PlaySFX(AudioGlobals.ToGround);





}
