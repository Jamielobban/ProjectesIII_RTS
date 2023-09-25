using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LP.FDG.InputManager;

namespace LP.FDG.Player
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager instance;

        public Transform playerUnits;

        public Transform enemyUnits;
        // Start is called before the first frame update
        void Start()
        {
            instance = this;
            Units.UnitHandler.instance.SetBasicUnitStats(playerUnits);
           //S Units.UnitHandler.instance.SetBasicUnitStats(enemyUnits);
        }

        // Update is called once per frame
        void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}


