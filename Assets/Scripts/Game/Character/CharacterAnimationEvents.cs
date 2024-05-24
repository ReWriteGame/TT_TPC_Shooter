using System;
using UnityEngine;


//the script must be on an object with an animator to work AnimationEvent
//these events invoke in FBX animation files
[RequireComponent(typeof(Animator))]
public class CharacterAnimationEvents : MonoBehaviour
{
    public Action<AnimationEvent> OnFootstepEvent;

    private void OnFootstep(AnimationEvent animationEvent) =>
        OnFootstepEvent?.Invoke(animationEvent);
}