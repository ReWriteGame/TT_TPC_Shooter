using System;
using Modules.Score;
using UnityEngine;

[SelectionBase]
public class BetaBot : MonoBehaviour, IDamageble, IInitialize
{
    [SerializeField] private BetaBotVisual betaBotVisual;
    [SerializeField] private BetaBotState state;
    [SerializeField] private BetaBotAnimationEvents animationEvents;

    public Action OnDied;
    public Action OnDamaged;
    public Action OnAttackAction;

    private Character target;

    public ScoreCounter Health => state.health;
    public bool IsDied => state.isDied;

    private void Start() => Init();
    private void OnDestroy() => Unsubscribe();

    public void Init()
    {
        Subscribe();
        betaBotVisual.Init();
        target = FindObjectOfType<Character>();
    }

    private void Subscribe()
    {
        state.health.OnReachMinValue += Died;
        animationEvents.OnAttackEvent += AttackWrapper;
    }

    private void Unsubscribe()
    {
        state.health.OnReachMinValue -= Died;
        animationEvents.OnAttackEvent -= AttackWrapper;
    }


    public void Damaged(float value)
    {
        state.health.DecreaseValue(value);
        if (!state.health.CheckValueIsMin) OnDamaged?.Invoke();
    }

    private void Died()
    {
        state.isDied = true;
        OnDied?.Invoke();
    }

    private void FixedUpdate()
    {
        if(state.isDied)return;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < state.damageDistance  && !target.IsDied) AttackAction();
    }

    public void AttackWrapper(AnimationEvent animationEvent) => Attack();

    public void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance < state.damageDistance) target.Damaged(state.damage);
    }

    public void AttackAction() =>         OnAttackAction?.Invoke();
}