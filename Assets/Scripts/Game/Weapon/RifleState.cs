using System;

[Serializable]
public class RifleState
{
    public int ammoMaxCount;
    public int ammoCurrentCount;
    public float damage;
    public float timeBetweenShots;
    public float reloadTime;
    
    public RifleState(GunSettingsSO dataGun)
    {
        ammoMaxCount = dataGun.AmmoCount;
        damage = dataGun.Damage;
        timeBetweenShots = dataGun.TimeBetweenShots;
        reloadTime = dataGun.ReloadTime;
        ammoCurrentCount = dataGun.AmmoCount;
    }
}