using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform vehicle; // Reference to the vehicle's transform
   public Vector3 offset; // Offset between the camera and the vehicle
   public float smoothSpeed = 0.125f; // Smooth speed for camera movement
   void LateUpdate()
   {
       Vector3 desiredPosition = vehicle.position + offset;
       Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
       transform.position = smoothedPosition;
       transform.LookAt(vehicle);
   }
}