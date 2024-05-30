using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts:")]
    public GrappleRope grappleRope;

    [Header("Layer Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private LayerMask hitLayers;

    [Header("Transform References:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 80)][SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = true;
    [SerializeField] private float maxDistance = 4;

    [Header("Launching")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Transform_Launch;
    [Range(0, 5)][SerializeField] private float launchSpeed = 5;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 3;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch,
    }

    [Header("Component References:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D ballRigidbody;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 DistanceVector;
    private Vector2 mouseFirePointDistanceVector;
    float defultGravity;

    private Cursor cursor;
    private InputAction grappleAction;

    private void Awake()
    {
        cursor = FindObjectOfType<Cursor>();
        grappleAction = FindObjectOfType<PlayerInput>().actions["ShootGrapple"];

        grappleAction.started += _ => StartGrapple();
        grappleAction.canceled += _ => StopGrapple();
    }

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        defultGravity = ballRigidbody.gravityScale;
    }

    private void Update()
    {
        mouseFirePointDistanceVector = (Vector2)cursor.transform.position - (Vector2)gunPivot.position;
        RotateGun(cursor.transform.position, !grappleRope.enabled);

        if (grappleAction.IsPressed())
        {
            ContinueGrapple();
        }
    }

    private void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        if (rotateOverTime && allowRotationOverTime)
        {
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void SetGrapplePoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, mouseFirePointDistanceVector.normalized,1000f, hitLayers);
        if (hit && (!hasMaxDistance || Vector2.Distance(hit.point, firePoint.position) <= maxDistance))
        {
            grapplePoint = hit.point;
            DistanceVector = grapplePoint - (Vector2)gunPivot.position;
            grappleRope.enabled = true;
            Grapple();
        }
    }

    internal void Grapple()
    {
        print("Grappeeeell");
        if (!launchToPoint)
        {
            Debug.Log("DOING STUFF");
            m_springJoint2D.autoConfigureDistance = autoConfigureDistance;

            if (!autoConfigureDistance)
            {
                m_springJoint2D.distance = targetDistance;
                m_springJoint2D.frequency = targetFrequency;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        else
        {
            if (launchType == LaunchType.Transform_Launch)
            {
                ballRigidbody.gravityScale = 0;
                ballRigidbody.velocity = Vector2.zero;
            }
            else if (launchType == LaunchType.Physics_Launch)
            {
                m_springJoint2D.connectedAnchor = grapplePoint;
                m_springJoint2D.distance = 0;
                m_springJoint2D.frequency = launchSpeed;
                m_springJoint2D.enabled = true;
            }
        }
    }

    private void StartGrapple()
    {
        Debug.Log("StartGrapple");
        SetGrapplePoint();
    }

    private void ContinueGrapple()
    {
        Debug.Log("Continnue Grapple");
        if (grappleRope.enabled)
        {
            RotateGun(grapplePoint, false);
        }
        else
        {
            RotateGun(cursor.transform.position, false);
        }

        if (launchToPoint && grappleRope.isGrappling)
        {
            if (launchType == LaunchType.Transform_Launch)
            {
                gunHolder.position = Vector3.Lerp(gunHolder.position, grapplePoint, Time.deltaTime * launchSpeed);
            }
        }
    }

    private void StopGrapple()
    {
        Debug.Log("Stop Grapple");
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        ballRigidbody.gravityScale = defultGravity;
    }

    private void OnDrawGizmos()
    {
        if (hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }
}
