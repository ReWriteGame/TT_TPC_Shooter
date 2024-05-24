using UnityEngine;

public interface IGun 
{
    public Transform LeftHandPosition { get;}
    public Transform RightHandPosition { get;}
    public Transform FirePoint { get;}

    public void SetUsedState();
    public void SetNotUsedState();

    public void Shoot();

}
