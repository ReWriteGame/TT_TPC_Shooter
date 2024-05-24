using System;
using UnityEngine;

public class BetaBotAnimationEvents : MonoBehaviour
{
    public Action<AnimationEvent> OnAttackEvent;

    private void OnAttack(AnimationEvent animationEvent)
    {
        OnAttackEvent?.Invoke(animationEvent);
    }
}