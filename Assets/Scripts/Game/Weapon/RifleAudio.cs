using UnityEngine;

public class RifleAudio : MonoBehaviour
{
    [SerializeField] private Rifle rifle;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip emptyShotClip;
    [Range(0, 1)] [SerializeField] private float shootAudioVolume = 0.5f;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        rifle.OnShot += ShootAudio;
        rifle.OnShotEmpty += ShootEmptyAudio;
    }

    private void Unsubscribe()
    {
        rifle.OnShot -= ShootAudio;
        rifle.OnShotEmpty -= ShootEmptyAudio;
    }

    private void ShootAudio() => AudioSource.PlayClipAtPoint(shootClip, transform.position, shootAudioVolume);

    private void ShootEmptyAudio() => AudioSource.PlayClipAtPoint(emptyShotClip, transform.position, shootAudioVolume);
}