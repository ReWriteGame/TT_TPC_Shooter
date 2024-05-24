using UnityEngine;

public class BetaBotAnimation : MonoBehaviour, IInitialize
{
    [SerializeField] private BetaBot beta;
    [SerializeField] private Animator animator;
    [SerializeField] private RagdollManager ragdoll;
    [SerializeField] private Canvas canvasLabel;


    // animation IDs
    private int animIDDamaged;
    private int animIDAttack;


    private void Start() => Init();
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    public void Init() => AssignAnimationIDs();


    private void AssignAnimationIDs()
    {
        animIDDamaged = Animator.StringToHash("Damaged");
        animIDAttack = Animator.StringToHash("Attack");
    }

    private void Subscribe()
    {
        beta.OnDied += DiedAnimation;
        beta.OnDamaged += GetDamage;
        beta.OnAttackAction += AnimationAttack;
    }

    private void Unsubscribe()
    {
        beta.OnDied -= DiedAnimation;
        beta.OnDamaged -= GetDamage;
        beta.OnAttackAction -= AnimationAttack;
    }

    private void DiedAnimation()
    {
        ragdoll.EnableColliders();
        animator.enabled = false;
        canvasLabel.enabled = false;
    }

    private void GetDamage()
    {
        animator.SetTrigger(animIDDamaged);
    }

    private void AnimationAttack()
    {
        animator.SetTrigger(animIDAttack);
    }
}