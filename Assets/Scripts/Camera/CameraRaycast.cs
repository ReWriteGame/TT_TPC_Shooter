using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private float maxRayDistance = 100;
    [SerializeField] private float minRayDistance = 100;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Camera camera;

    private Vector3 hitPoint;
    private RaycastHit hit;
    private bool isHit;

    private Vector3 StartPoint => camera.transform.position + Direction * minRayDistance;
    private Vector3 EndPoint => camera.transform.position + Direction * maxRayDistance;
    private Vector3 Direction => camera.transform.forward;
    public Vector3 HitPoint => hitPoint;
    public bool IsHit => isHit;
    public RaycastHit Hit => hit;
    

    private void Update() => UpdateCustom();
    
    private void UpdateCustom()
    {
        isHit = Physics.Raycast(StartPoint, Direction, out hit, maxRayDistance, layer);
        hitPoint = IsHit ? hit.point : EndPoint;
    }

    private void OnDrawGizmos()
    {
        Vector3 startPoint = StartPoint;
        Vector3 endPoint = EndPoint;

        Gizmos.color = Color.white;
        Gizmos.DrawLine(camera.transform.position, endPoint);

        if (!IsHit) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPoint, HitPoint);
    }
}