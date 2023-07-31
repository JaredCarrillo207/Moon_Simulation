using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    public Transform terrain;
    public Transform rover1;
    public Transform rover2;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float minDistance = 1f; // Minimum distance between rovers

    private Vector3 targetPosition1;
    private Vector3 targetPosition2;

    private void Start()
    {
        // Get the boundaries of the terrain
        float halfTerrainSizeX = terrain.localScale.x * 0.5f;
        float halfTerrainSizeZ = terrain.localScale.z * 0.5f;

        // Set initial y-coordinates of the rovers to match the terrain height
        rover1.position = new Vector3(rover1.position.x, terrain.position.y, rover1.position.z);
        rover2.position = new Vector3(rover2.position.x, terrain.position.y, rover2.position.z);

        // Initialize random target positions for both rovers
        targetPosition1 = GetRandomPosition(halfTerrainSizeX, halfTerrainSizeZ);
        targetPosition2 = GetRandomPosition(halfTerrainSizeX, halfTerrainSizeZ);
    }

    private void Update()
    {
        MoveRover(rover1, ref targetPosition1);
        MoveRover(rover2, ref targetPosition2);
    }

    private void MoveRover(Transform rover, ref Vector3 targetPosition)
    {
        Vector3 roverPosition = rover.position;

        // Remove the y-coordinate from the target position
        targetPosition.y = roverPosition.y;

        // Check distance to other rover
        Transform otherRover = rover == rover1 ? rover2 : rover1;
        float distanceToOtherRover = Vector3.Distance(roverPosition, otherRover.position);

        // If too close to the other rover, update the target position away from it
        if (distanceToOtherRover < minDistance)
        {
            Vector3 directionToOtherRover = (roverPosition - otherRover.position).normalized;
            targetPosition = roverPosition + directionToOtherRover * minDistance;
        }

        // Move the rover towards the target position smoothly using Translate
        rover.Translate((targetPosition - roverPosition).normalized * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the rover smoothly to face the movement direction
        Vector3 direction = (targetPosition - roverPosition).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rover.rotation = Quaternion.Slerp(rover.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Check if the rover has reached its target position
        if (Vector3.Distance(roverPosition, targetPosition) < minDistance)
        {
            targetPosition = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition(float halfTerrainSizeX = 1.5f, float halfTerrainSizeZ = 1.5f)
    {
        float randomX = Random.Range(-halfTerrainSizeX, halfTerrainSizeX);
        float randomZ = Random.Range(-halfTerrainSizeZ, halfTerrainSizeZ);
        return new Vector3(randomX, terrain.position.y, randomZ);
    }
}
