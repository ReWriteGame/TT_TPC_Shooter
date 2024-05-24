using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollManager : MonoBehaviour, IInitialize
{
    [SerializeField] private List<Rigidbody> bones = new List<Rigidbody>();

    private void Awake() => Init();

    public void Init()
    {
        GetAllBones();
        DisableColliders();
    }

    private void GetAllBones()
    {
        bones = GetComponentsInChildren<Rigidbody>().ToList();
    }

    public void DisableColliders()
    {
        foreach (var bone in bones)
        {
            bone.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            bone.isKinematic = true;
        }
    }

    public void EnableColliders()
    {
        foreach (var bone in bones)
        {
            bone.isKinematic = false;
            bone.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    public void AddForce(Vector3 direction)
    {
        foreach (var bone in bones)
        {
            bone.AddForce(direction, ForceMode.Impulse);
        }
    }

    public void AddExplosion(Vector3 expPos, float multiplier)
    {
        foreach (var bone in bones)
        {
            bone.AddForce((bone.transform.position - expPos).normalized * multiplier + Vector3.up * 5f, ForceMode.Impulse);
        }
    }
}
