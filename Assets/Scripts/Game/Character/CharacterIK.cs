using UnityEngine;

public class CharacterIK : MonoBehaviour, IInitialize
{
    [SerializeField] private Character character;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private Transform targetLook;

    private Transform leftHand;
    private Transform rightHand;
    private float rightHandWeight;
    private float leftHandWeight = 1;
    private Transform shoulder;
    private Transform aimPivot;
    private Quaternion leftHandRotation;

    private Rifle Rifle => character.CurrentWeapon;

    private void Update() => UpdateCustom();

    private void OnAnimatorIK(int layerIndex)
    {
        aimPivot.position = shoulder.position;

        if (characterInput.isAim)
        {
            aimPivot.LookAt(targetLook);
            animator.SetLookAtWeight(1, .4f, 1);
        }
        else
        {
            animator.SetLookAtWeight(.3f, .3f, .3f);
        }

        animator.SetLookAtPosition(targetLook.position);

        Transform lHandPos = leftHand;
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, lHandPos.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
    }

    public void Init()
    {
        shoulder = animator.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "AimPivot";
        aimPivot.transform.parent = transform;

        leftHand = new GameObject().transform;
        leftHand.name = "LeftHand";
        leftHand.transform.parent = aimPivot;

        rightHand = new GameObject().transform;
        rightHand.name = "RightHand";
        rightHand.transform.parent = aimPivot;

        targetLook = new GameObject().transform;
        targetLook.name = "AimTargetPoint";

        rightHand.localPosition = Rifle.RightHandPos;
        Quaternion rotRightHand = Quaternion.Euler(Rifle.RightHandRot.x, Rifle.RightHandRot.y, Rifle.RightHandRot.z);
        rightHand.localRotation = rotRightHand;
    }

    public void SetAimTargetPosition(Vector3 position)
    {
        targetLook.position = position;
    }

    private void UpdateCustom()
    {
        if (character.IsDied)
        {
            rightHandWeight = 0;
            leftHandWeight = 0;
            return;
        }

        leftHandRotation = Rifle.LeftHandPosition.rotation;
        leftHand.position = Rifle.LeftHandPosition.position;

        rightHandWeight += characterInput.isAim ? Time.deltaTime * 4 : -Time.deltaTime * 4;
        rightHandWeight = Mathf.Clamp(rightHandWeight, 0f, 1f);
    }
}