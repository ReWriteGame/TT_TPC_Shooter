using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private ShootRaycastSystem shootRaycastSystem;
    [SerializeField] private CameraRaycast cameraRaycast;
    [SerializeField] private Rifle weapon;

    public Rifle CurrentWeapon => weapon;

    public void SetCurrentWeapon(Rifle weapon) => this.weapon = weapon;

    public void Shoot()
    {
        if (!weapon) return;
        weapon.OnShot += shootRaycastSystem.TryApplyDamage;
        weapon.Shoot();
        weapon.OnShot -= shootRaycastSystem.TryApplyDamage;
    }

    public void Reload() => weapon.Reload();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(weapon.FirePoint.position, cameraRaycast.HitPoint);
    }
}