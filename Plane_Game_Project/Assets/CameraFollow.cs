
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody targetRb;

    [Header("Camera Position")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -13f);
    [SerializeField] private float positionSmoothTime = 0.3f;

    [Header("Camera Rotation")]
    [SerializeField] private float rotationSmoothSpeed = 4f;
    [SerializeField] private float lookAheadDistance = 8f;
    [SerializeField] private float lookHeight = 1.5f;

    [Header("Dynamic Distance")]
    [SerializeField] private float minDistance = 12f;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float speedForMaxDistance = 100f;
    [SerializeField] private float distanceSmoothSpeed = 2f;

    [Header("Camera Inertia")]
    [SerializeField] private float rotationInertia = 0.15f;
    [SerializeField] private float bankCameraEffect = 0.15f;

   

    private Vector3 positionVelocity;
    private float currentDistance;

    private void Start()
    {
        currentDistance = maxDistance;
    }

    private void LateUpdate()
    {
        if (target == null || targetRb == null)
            return;

        UpdateCameraPosition();
        UpdateCameraRotation();
    }

    private void UpdateCameraPosition()
    {
        float speed = targetRb.linearVelocity.magnitude;

        // Increase camera distance at higher speeds.
        float speedFactor = Mathf.Clamp01(
            speed / speedForMaxDistance
        );

        float desiredDistance = Mathf.Lerp(
            minDistance,
            maxDistance,
            speedFactor
        );

        currentDistance = Mathf.Lerp(
            currentDistance,
            desiredDistance,
            distanceSmoothSpeed * Time.deltaTime
        );

        // Position relative to aircraft orientation.
        Vector3 desiredPosition =
            target.position +
            target.right * offset.x +
            target.up * offset.y -
            target.forward * currentDistance;

        // Small inertia effect from aircraft movement.
        Vector3 velocityOffset =
            targetRb.linearVelocity * rotationInertia;

        desiredPosition -= velocityOffset * 0.05f;
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref positionVelocity,
            positionSmoothTime
        );
    }

    private void UpdateCameraRotation()
    {
        Vector3 velocityDirection =
            targetRb.linearVelocity.sqrMagnitude > 4f
            ? targetRb.linearVelocity.normalized
            : target.forward;

        // Look ahead in direction of movement.
        Vector3 lookTarget =
            target.position +
            velocityDirection * lookAheadDistance +
            target.up * lookHeight;

        Vector3 direction =
            lookTarget - transform.position;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion desiredRotation =
            Quaternion.LookRotation(direction.normalized, target.up);

        // Banking effect.
        float bank = -target.InverseTransformDirection(
            targetRb.angularVelocity
        ).z;

        desiredRotation *= Quaternion.Euler(
            0f,
            0f,
            bank * bankCameraEffect * Mathf.Rad2Deg
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSmoothSpeed * Time.deltaTime
        );
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;

        if (target != null)
            targetRb = target.GetComponent<Rigidbody>();
    }
}
