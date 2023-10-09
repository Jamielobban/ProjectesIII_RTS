using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LP.FDG.Buildings
{
    public class BasicBuilding : MonoBehaviour
    {
        public BuildingStatTypes.Base baseStats;

        // In your Tower/Building script
        public Transform attackPoint1;
        public Transform attackPoint2;

        private void Start()
        {
            attackPoint1 = gameObject.transform.GetChild(0);
            attackPoint2 = gameObject.transform.GetChild(1);
            // Register attack points with the AttackPointManager
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint1);
            AttackPointManager.instance.availableAttackPoints.Add(attackPoint2);
        }
    }

}
