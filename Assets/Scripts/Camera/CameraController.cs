using System;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour, IInitialize
{
    [SerializeField] private CinemachineFreeLook cinCamera;
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private float speedChangeFOV = 1f;
    [SerializeField] private float sprintFOV = 75.0f;
    [SerializeField] private float aimFOV = 10f;

    private float startFOV;
    private Camera mCamera;

    private void Awake() => Init();
    private void Update() => UpdateCustom();

    public void Init()
    {
        mCamera = GetComponent<Camera>();
        startFOV = mCamera.fieldOfView;
    }

    private void UpdateCustom()
    {
        float targetFOV = GetTargetFOV();
        float newFOV = Mathf.Lerp(   cinCamera.m_Lens.FieldOfView, targetFOV, speedChangeFOV * Time.deltaTime);
        cinCamera.m_Lens.FieldOfView = (float)Math.Round(newFOV, 2);
    }

    private float GetTargetFOV()
    {
        if (characterInput.isAim) return aimFOV;
        if (characterInput.isSprint) return sprintFOV;
        return startFOV;
    }
}