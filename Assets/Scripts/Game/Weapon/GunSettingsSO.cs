using UnityEngine;

[CreateAssetMenu(fileName = "GunSettings", menuName = "ScriptableObjects/GunSettings", order = 2)]
public class GunSettingsSO : ScriptableObject
{
    [SerializeField] private int ammoCount = 10;
    [SerializeField] private float damage = 10;
    [SerializeField] private float timeBetweenShots = 1;
    [SerializeField] private float reloadTime = 10;

    public int AmmoCount => ammoCount;
    public float Damage => damage;
    public float ReloadTime => reloadTime;
    public float TimeBetweenShots => timeBetweenShots;
}