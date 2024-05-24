using UnityEngine;

public class BetaBotEffects : MonoBehaviour
{
    [SerializeField] private BetaBotAnimationEvents betaBotAnimationEvents;
    [SerializeField] private ParticleSystem attackPS;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        betaBotAnimationEvents.OnAttackEvent += AttackWrapper;
    }

    private void Unsubscribe()
    {
        betaBotAnimationEvents.OnAttackEvent -= AttackWrapper;
    }

    private void AttackWrapper(AnimationEvent animationEvent) => AttackEffect();
    private void AttackEffect() => attackPS.Play();
}