using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPointManager : MonoBehaviour
{
    public static AttackPointManager instance; // Singleton instance

    // Create a list to keep track of available and taken attack points
    public List<Transform> availableAttackPoints = new List<Transform>();
    public List<Transform> takenAttackPoints = new List<Transform>();

    // Counter for the number of enemies in the game
    public int enemyCount = 0;
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
    //public Transform ReserveAttackPoint()
    //{
    //    //if (availableAttackPoints.Count > 0)
    //    //{
    //    //    Transform attackPoint = availableAttackPoints[0];
    //    //    availableAttackPoints.RemoveAt(0);
    //    //    takenAttackPoints.Add(attackPoint);
    //    //    return attackPoint;
    //    //}
    //    //else
    //    //{
    //    //    return null; // No available attack points
    //    //}
    //}

    public Transform ReserveClosestAttackPoint(Vector3 enemyPosition)
    {
        if (availableAttackPoints.Count > 0)
        {
            Transform closestAttackPoint = null;
            float closestDistance = float.MaxValue;

            foreach (Transform attackPoint in availableAttackPoints)
            {
                float distance = Vector3.Distance(enemyPosition, attackPoint.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestAttackPoint = attackPoint;
                }
            }

            if (closestAttackPoint != null)
            {
                availableAttackPoints.Remove(closestAttackPoint);
                takenAttackPoints.Add(closestAttackPoint);
            }

            return closestAttackPoint;
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

    // Function to increment the enemy count
    public void IncrementEnemyCount()
    {
        enemyCount++;
    }

    // Function to decrement the enemy count
    public void DecrementEnemyCount()
    {
        enemyCount--;
        enemyCount = Mathf.Max(0, enemyCount);
    }

    // Function to check if there are no enemies left
    public bool AreAllEnemiesDefeated()
    {
        return enemyCount == 0;
    }
}