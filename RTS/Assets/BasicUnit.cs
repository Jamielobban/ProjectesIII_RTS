using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LP.FDG.Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Create New Unit/Basic")]
    public class BasicUnit : ScriptableObject
    {
        public enum unitType
        {
            Worker,
            Warrior,
            Healer
        };
        public bool isPlayerUnit;

        public unitType type;
        public new string name;

        public GameObject unitPrefab;

        public int cost;
        public int attack;
        public int health;
        public int armor;
    }
}

