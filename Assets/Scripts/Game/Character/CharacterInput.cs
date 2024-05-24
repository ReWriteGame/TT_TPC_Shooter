using System;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public bool isSprint;
    public bool isJump;
    public bool isAim;
    public bool isFire;
    public bool isNextWeapon;
    public bool isPreviousWeapon;
    public bool isReload;
    
    public Vector2 newDirection => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    public Action OnSoot;
    public Action OnReload;
    public Action OnNextWeapon;
    public Action OnPreviousWeapon;
    private void Update()
    {
        isSprint = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        isReload = Input.GetKeyDown(KeyCode.R);
        isAim = Input.GetMouseButton(1);
        isFire = Input.GetMouseButton(0);
        isNextWeapon = Input.GetKeyDown(KeyCode.E);
        isPreviousWeapon = Input.GetKeyDown(KeyCode.Q);
        
        if(Input.GetMouseButtonDown(0))OnSoot?.Invoke();
        if(Input.GetMouseButtonDown(0))OnSoot?.Invoke();
        if(isReload)OnReload?.Invoke();
        if(isNextWeapon)OnNextWeapon?.Invoke();
        if(isPreviousWeapon)OnPreviousWeapon?.Invoke();
    }
}