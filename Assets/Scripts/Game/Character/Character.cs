using System;
using Modules.Score;
using UnityEngine;

[SelectionBase]
public class Character : MonoBehaviour, IInitialize, IDamageble
{
    [SerializeField] private CharacterControllerLogic characterController;
    [SerializeField] private CharacterAnimation characterAnimation;
    [SerializeField] private CharacterIK characterIK;
    [SerializeField] private CameraRaycast cameraRaycast;
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private WeaponSystem weaponSystem;
    [SerializeField] private SlotsSystem slots;
    [SerializeField] private ScoreCounter health;

    private bool isAim;
    
    public Action OnDied;
    
    public Rifle CurrentWeapon => weaponSystem.CurrentWeapon;
    public bool IsDied => health.CheckValueIsMin;


    private void Start() => Init();
    private void Update() => UpdateCustom();
    private void OnDestroy() => Unsubscribe();

    public void Init()
    {
        health = new ScoreCounter(new ScoreCounterData(100, 0, 100));
        characterController.Init();
        characterAnimation.Init();
        characterIK.Init();

        foreach (var obj in slots.Objects)
            if (obj.TryGetComponent(out Rifle rifle))
                rifle.SetUsedState();

        Subscribe();
    }

    private void UpdateCustom()
    {
        characterIK.SetAimTargetPosition(cameraRaycast.HitPoint);
    }

    private void Subscribe()
    {
        characterInput.OnSoot += ShootAction;
        characterInput.OnNextWeapon += SelectNextWeapon;
        characterInput.OnPreviousWeapon += SelectPreviousWeapon;
        characterInput.OnReload += weaponSystem.Reload;
        health.OnReachMinValue += Died;
    }

    private void Unsubscribe()
    {
        characterInput.OnSoot -= ShootAction;
        characterInput.OnNextWeapon -= SelectNextWeapon;
        characterInput.OnPreviousWeapon -= SelectPreviousWeapon;
        characterInput.OnReload -= weaponSystem.Reload;
        health.OnReachMinValue -= Died;
    }

    public void ShootAction()
    {
        if (characterInput.isAim)
            weaponSystem.Shoot();
    }

    public void TakeNewItem(GameObject obj)
    {
        if (obj.TryGetComponent(out Rifle gun))
        {
            gun.SetUsedState();
            slots.AddObject(obj);
            weaponSystem.SetCurrentWeapon(gun);
        }
    }

    public void ThrowItem(GameObject obj)
    {
        if (obj.TryGetComponent(out Rifle gun))
        {
            gun.SetNotUsedState();
            slots.RemoveObject(obj);
        }
    }

    public void SelectNextWeapon()
    {
        if (slots.Objects.Count <= 0) return;
        int index = slots.Objects.IndexOf(weaponSystem.CurrentWeapon.gameObject);
        int newIndex = index + 1 < slots.Objects.Count ? index + 1 : 0;
        if (slots.Objects[newIndex].TryGetComponent(out Rifle rifle)) SelectWeapon(rifle);
    }

    public void SelectPreviousWeapon()
    {
        if (slots.Objects.Count <= 0) return;
        int index = slots.Objects.IndexOf(weaponSystem.CurrentWeapon.gameObject);
        int newIndex = index - 1 >= 0 ? index - 1 : slots.Objects.Count - 1;
        if (slots.Objects[newIndex].TryGetComponent(out Rifle rifle)) SelectWeapon(rifle);
    }

    private void SelectWeapon(Rifle rifle)
    {
        if (!rifle) return;
        weaponSystem.SetCurrentWeapon(rifle);
        slots.Objects.ForEach(x => x.SetActive(false));
        rifle.gameObject.SetActive(true);
    }

    public void Damaged(float value) => health.DecreaseValue(value);

    public void Died()
    {
        OnDied?.Invoke();
    }
}