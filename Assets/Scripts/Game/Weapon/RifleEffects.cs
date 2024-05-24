using UnityEngine;

public class RifleEffects : MonoBehaviour
{
    [SerializeField] private Rifle rifle;
    [SerializeField] private ParticleSystem shootFlashPS;
    [SerializeField] private ParticleSystem shootBulletPS;
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        rifle.OnShot += ShootFlash;
        rifle.OnShot += ShootBullet;
    }

    private void Unsubscribe()
    {
        rifle.OnShot += ShootFlash;
        rifle.OnShot += ShootBullet;
    }

    private void ShootFlash() => shootFlashPS.Play();
    private void ShootBullet() => shootBulletPS.Play();
}