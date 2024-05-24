using System;
using Class.Cooldown;
using UnityEngine;

[SelectionBase]
public class Rifle : MonoBehaviour, IGun
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private ColidersContainer colidersContainer;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform leftHandPosition;
    [SerializeField] private Transform rightHandPosition;

    [SerializeField] private GunSettingsSO defaultSettings;
    [SerializeField] private RifleState state;

    [SerializeField] private Cooldown shootCooldown;
    [SerializeField] private Cooldown reloadCooldown;

    public Action OnShot;
    public Action OnShotEmpty;
    public Action OnReload;

    [field: SerializeField] public Vector3 RightHandPos { get; private set; }
    [field: SerializeField] public Vector3 RightHandRot { get; private set; }

    public Transform LeftHandPosition => leftHandPosition;
    public Transform RightHandPosition => rightHandPosition;
    public Transform FirePoint => firePoint;
    public float Damage => state.damage;


    private void Awake() => Init();

    public void Init()
    {
        state = new RifleState(defaultSettings);
        shootCooldown = new Cooldown(state.timeBetweenShots);
        reloadCooldown = new Cooldown(state.reloadTime);
        reloadCooldown.OnEndCooldown += ReloadNoTime;
    }

    public void SetUsedState()
    {
        rb.isKinematic = true;
        colidersContainer.SetAllTriggers();
    }

    public void SetNotUsedState()
    {
        rb.isKinematic = false;
        colidersContainer.SetAllColliders();
    }

    public void Shoot()
    {
        if (!shootCooldown.Reloaded) return;
        if (state.ammoCurrentCount > 0)
        {
            shootCooldown.Activate();
            state.ammoCurrentCount--;
            OnShot?.Invoke();
        }
        else
        {
            OnShotEmpty?.Invoke();
        }
    }

    public void Reload()
    {
        if (!reloadCooldown.Reloaded) return;
        reloadCooldown.Activate();
    }

    private void ReloadNoTime()
    {
        state.ammoCurrentCount = state.ammoMaxCount;
        OnReload?.Invoke();
    }
}