using UnityEngine;

public class ShootRaycastSystem : MonoBehaviour
{
    [SerializeField] private CameraRaycast cameraRaycast;
    [SerializeField] private Character character;

    public void TryApplyDamage()
    {
        if (cameraRaycast.IsHit && cameraRaycast.Hit.collider.TryGetComponent(out IDamageble damageble))
            damageble.Damaged(character.CurrentWeapon.Damage);
    }
}