using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointManager : MonoBehaviour
{
    public static AttackPointManager instance; // Singleton instance

    // Create a list to keep track of available and taken attack points
    public List<Transform> availableAttackPoints = new List<Transform>();
    public List<Transform> takenAttackPoints = new List<Transform>();

    private void Awake()
    {
        // Ensure only one instance of the AttackPointManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add a method to reserve an attack point
    public Transform ReserveAttackPoint()
    {
        if (availableAttackPoints.Count > 0)
        {
            Transform attackPoint = availableAttackPoints[0];
            availableAttackPoints.RemoveAt(0);
            takenAttackPoints.Add(attackPoint);
            return attackPoint;
        }
        else
        {
            return null; // No available attack points
        }
    }

    // Add a method to release an attack point
    public void ReleaseAttackPoint(Transform attackPoint)
    {
        if (takenAttackPoints.Contains(attackPoint))
        {
            takenAttackPoints.Remove(attackPoint);
            availableAttackPoints.Add(attackPoint);
        }
    }
}