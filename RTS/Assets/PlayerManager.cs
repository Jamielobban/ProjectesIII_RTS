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

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        private void Start()
        {
            //instance = this;
            Units.UnitHandler.instance.SetBasicUnitStats(playerUnits);
            Units.UnitHandler.instance.SetBasicUnitStats(enemyUnits);
        }

        // Update is called once per frame
        private void Update()
        {
            InputHandler.instance.HandleUnitMovement();
        }
    }
}


