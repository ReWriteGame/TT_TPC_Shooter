using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllerLogic : MonoBehaviour, IInitialize
{
    [SerializeField] private Camera camera;
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 2;

    [SerializeField] private float directionDampTime = 0.25f;


    [SerializeField] private float speedOffset = 0.1f;
    [SerializeField] private float speedChangeRate = 10.0f;

    [SerializeField] private bool analogMovement;


    private CharacterController characterController;
    private float targetSpeed;
    private Vector2 inputDirection;
    private float currentSpeed;
    private float direction;
    public float rotateAngle;


    public float CurrentSpeed => currentSpeed;
    public float LocomotionThreshold => 0.2f;
    public Vector3 InputDirectionV3 => new Vector3(inputDirection.x, 0, inputDirection.y);
    public Vector3 CurrentDirection => new Vector3(transform.forward.x, 0, transform.forward.z);
    public float Direction => direction;
    public float RotateAngle => rotateAngle;
    public float DirectionDampTime => directionDampTime;
    public bool IsJump => characterInput.isJump;
    private bool IsSprint => characterInput.isSprint;


    public Action OnSetNewInputMoveDirection;

    private void Update() => UpdateCustom();

    public void Init() => characterController = GetComponent<CharacterController>();

    private void UpdateCustom()
    {
        GetInputDirection();
        StickToWorldSpace(CurrentDirection, camera.transform);
        MoveSpeedCalculation();
        if (characterInput.isAim) Aiming();
    }


    private void MoveSpeedCalculation()
    {
        float maxSpeed = IsSprint ? sprintSpeed : moveSpeed;
        if (inputDirection == Vector2.zero) maxSpeed = 0.0f;

        float inputMagnitude = analogMovement ? inputDirection.magnitude : 1f;
        targetSpeed = maxSpeed * inputMagnitude;


        float currentHorSpeed = currentSpeed; // need make y = 0

        // accelerate or decelerate to target speed
        bool useSpeedCorrect = currentHorSpeed < targetSpeed - speedOffset ||
                               currentHorSpeed > targetSpeed + speedOffset;
        float speedCorrect = Mathf.Lerp(currentHorSpeed, targetSpeed,
            Time.deltaTime * speedChangeRate);

        currentSpeed = useSpeedCorrect ? speedCorrect : targetSpeed;
        currentSpeed = (float)Math.Round(currentSpeed, 3);

        if (characterInput.isAim) currentSpeed = 0;
    }


    private void GetInputDirection()
    {
        //Vector2 newDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 newDirection = characterInput.isAim ? Vector2.zero : characterInput.newDirection;
        SetNewDirection(NormalizedToOne(newDirection));
    }


    private void SetNewDirection(Vector2 newDirection)
    {
        if (inputDirection.magnitude < 0.01f) inputDirection = Vector2.zero;
        if (CompareVector2(inputDirection, newDirection, 0.01f)) return;
        inputDirection = newDirection;
        OnSetNewInputMoveDirection?.Invoke();
    }

    private bool CompareVector2(Vector2 a, Vector2 b, float epsilon = 0.001f) =>
        Mathf.Abs(a.x - b.x) < epsilon && Mathf.Abs(a.y - b.y) < epsilon;


    private Vector2 NormalizedToOne(Vector2 vector) =>
        vector.magnitude > 1 ? vector.normalized : vector;


    private void StickToWorldSpace(Vector3 currentDirection, Transform camera)
    {
        Vector3 cameraDirection = camera.forward;
        cameraDirection.y = 0.0f; // kill Y
        Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, cameraDirection.normalized);

        Vector3 moveDirection = referentialShift * InputDirectionV3;
        Vector3 axisSign = Vector3.Cross(moveDirection, currentDirection);

        float horizontalCameraDirection = camera ? camera.transform.eulerAngles.y : 0;
        Vector3 direction2 = RotateVectorRelativeToOther(InputDirectionV3, horizontalCameraDirection);

        float angleRootToMove = Vector3.Angle(currentDirection, direction2) * (axisSign.y >= 0 ? -1f : 1f);
        angleRootToMove = (float)Math.Round(angleRootToMove, 2);

        rotateAngle = angleRootToMove;
        angleRootToMove /= 180f;
        direction = angleRootToMove * moveSpeed;
    }

    private void Aiming() => RotateObjectToDirection(camera.transform.forward);


    void RotateObjectToDirection(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = targetRotation;
    }
    
    private Vector3 RotateVectorRelativeToOther(Vector3 directionVector, float rotationVector)
    {
        Quaternion rotationY = Quaternion.Euler(0f, rotationVector, 0f);
        return rotationY * directionVector;
    }
}