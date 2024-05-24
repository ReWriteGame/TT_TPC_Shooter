using UnityEngine;

public class CharacterAnimation : MonoBehaviour, IInitialize
{
    [SerializeField] private CharacterControllerLogic characterControllerLogic;
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private Animator animator;
    [SerializeField] private Character character;

    private AnimatorStateInfo stateInfo;
    private AnimatorTransitionInfo transInfo;

    private int locomotionID;
    private int locomotionPivotLID;
    private int locomotionPivotRID;
    private int locomotionPivotLTransID;
    private int locomotionPivotRTransID;
    private int animIDDied;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        character.OnDied += DiedAnimation;
        character.OnDied += DisableIK;
    }

    private void Unsubscribe()
    {
        character.OnDied -= DiedAnimation;
        character.OnDied -= DisableIK;
    }

    private void Update() => UpdateCustom();

    public void Init()
    {
        EnableIK();

        locomotionID = Animator.StringToHash("Base Layer.Locomotion");
        locomotionPivotLID = Animator.StringToHash("Base Layer.LocomotionPivotL");
        locomotionPivotRID = Animator.StringToHash("Base Layer.LocomotionPivotR");
        locomotionPivotLTransID = Animator.StringToHash("Locomotion -> LocomotionPivotL");
        locomotionPivotRTransID = Animator.StringToHash("Locomotion -> LocomotionPivotR");
        animIDDied = Animator.StringToHash("Died");
    }

    private void EnableIK()
    {
        animator.SetLayerWeight(1, 1);
    }

    private void DisableIK()
    {
        animator.SetLayerWeight(1, 0);
    }

    private void UpdateCustom()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        transInfo = animator.GetAnimatorTransitionInfo(0);


        animator.SetFloat("Speed", characterControllerLogic.CurrentSpeed);


        animator.SetFloat("Direction", characterControllerLogic.Direction, characterControllerLogic.DirectionDampTime,
            Time.deltaTime);
        animator.SetFloat("Angle", characterControllerLogic.RotateAngle);

        if (characterControllerLogic.CurrentSpeed < characterControllerLogic.LocomotionThreshold &&
            characterControllerLogic.InputDirectionV3.magnitude < 0.05f) // Dead zone
        {
            animator.SetFloat("Direction", 0f);
            animator.SetFloat("Angle", 0f);
        }

        animator.SetBool("Jump", characterControllerLogic.IsJump);
        animator.SetBool("Aiming", characterInput.isAim);

        animator.SetFloat("Angle", characterControllerLogic.RotateAngle);
    }


    private bool IsInJump() => IsInIdleJump() || IsInLocomotionJump();
    private bool IsInIdleJump() => animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.IdleJump");
    private bool IsInLocomotionJump() => animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.LocomotionJump");


    private void DiedAnimation()
    {
        animator.SetTrigger(animIDDied);
    }
}