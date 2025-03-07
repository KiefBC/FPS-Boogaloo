using System.Collections;
using UnityEngine;

public class DronePathFollower : MonoBehaviour
{
    [SerializeField] private DroneWayPointPath path;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private float pauseAtWaypoint = 0.5f;

    private Transform sourceWP;
    private Transform targetWP;
    private int currentWPIndex = 0;

    private float totalTimeToWP;
    private float elapsedTimeToWp;
    private float speed = 3.0f;

    private bool isRotating = false;
    private bool isMoving = true;

    private void TargetNextWayPoint()
    {
        elapsedTimeToWp = 0f;

        sourceWP = path.GetWayPoint(currentWPIndex);
        currentWPIndex++;

        if (currentWPIndex >= path.GetWayPointCount())
        {
            currentWPIndex = 0;
        }

        targetWP = path.GetWayPoint(currentWPIndex);

        float distanceToWP = Vector3.Distance(sourceWP.position, targetWP.position);

        totalTimeToWP = distanceToWP / speed;

        // When we reach a waypoint, stop and start the rotation sequence
        isMoving = false;
        StartCoroutine(WaypointSequence());
    }

    private void MoveToNextWayPoint()
    {
        if (!isMoving)
            return;

        elapsedTimeToWp += Time.deltaTime;

        float elapsedTimePercentage = elapsedTimeToWp / totalTimeToWP;

        Vector3 newPos = Vector3.Lerp(sourceWP.position, targetWP.position, elapsedTimePercentage);
        transform.position = newPos;

        if (elapsedTimePercentage >= 1.0f)
        {
            TargetNextWayPoint();
        }
    }

    IEnumerator WaypointSequence()
    {
        // Wait a moment at the waypoint before rotating
        yield return new WaitForSeconds(pauseAtWaypoint);
        
        // Start the rotation sequence
        yield return StartCoroutine(RotateDrone());
        
        // Resume movement after rotation is complete
        isMoving = true;
    }

    IEnumerator RotateDrone()
    {
        isRotating = true;

        // Store original Rotation
        Quaternion originalRot = transform.rotation;

        // Target rotation
        Quaternion targetRot = originalRot * Quaternion.Euler(0, 90, 0);

        // Rotate to target
        float elapsedTime = 0.0f;
        float rotationTime = 90f / rotationSpeed;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(originalRot, targetRot, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure we are at the target rotation
        transform.rotation = targetRot;
        
        // Pause at the rotated position
        yield return new WaitForSeconds(pauseAtWaypoint);
        
        // Rotate back to original position
        elapsedTime = 0.0f;
        
        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(targetRot, originalRot, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        // Ensure we are back at the original rotation
        transform.rotation = originalRot;
        
        isRotating = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TargetNextWayPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToNextWayPoint();
    }
}