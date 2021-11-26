using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{

    Inputs inputs;

    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPosition;
    private Vector3 cameraFollow = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float CameraCollisionOffSet = 0.2f;
    public float minCollisionOffset = 0.2f;
    public float CameraCollisionRadious =2;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float CameraPivotSpeed = 2;

    public float lookAngle;
    public float pivotAngle;
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
        (transform.position, targetTransform.position, ref cameraFollow, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void CamRotate()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputs.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputs.cameraInputY * CameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;


        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }


    public void HandleCamera()
    {
        FollowTarget();
        CamRotate();
        CameraCollisions();
    }
    public void CameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if(Physics.SphereCast
        (cameraPivot.transform.position, CameraCollisionRadious, direction, out hit, Mathf.Abs(targetPosition),collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - CameraCollisionOffSet);

        }

        if (Mathf.Abs(targetPosition) < minCollisionOffset)
        {
            targetPosition = targetPosition - minCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
    public void Awake()
    {
        inputs = FindObjectOfType<Inputs>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }
}
