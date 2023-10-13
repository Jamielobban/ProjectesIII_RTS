using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;
using LP.FDG.Player;

namespace LP.FDG.Units
{
    public class UnitHandler : MonoBehaviour
    {
        public static UnitHandler instance;

        [SerializeField]
        private BasicUnit worker, warrior, healer;


        public LayerMask pUnitLayer, eUnitLayer;
        
        private void Awake()
        {

            instance = this;
            eUnitLayer = LayerMask.NameToLayer("EnemyUnits");
            pUnitLayer = LayerMask.NameToLayer("Units"); 
        }
        private void Start()
        {
        }
        public UnitStatTypes.Base GetBasicUnitStats(string type)
        {
            BasicUnit unit;
            switch (type)
            {
                case "worker":
                    unit = worker;
                    break;
                case "healer":
                    unit = healer;
                    break;
                case "warrior":
                    unit = warrior;
                    break;
                default:
                    Debug.Log($"Unit Type: {type} could not be found");
                    return null;
            }
            return unit.baseStats;
        }

        public void SetBasicUnitStats(Transform type)
        {

            Transform pUnits = PlayerManager.instance.playerUnits;
            Transform eUnits = PlayerManager.instance.enemyUnits;
            foreach (Transform child in type)
            {
                foreach (Transform unit in child)
                {
                    string unitName = child.name.Substring(0,child.name.Length - 1).ToLower();
                    var stats = GetBasicUnitStats(unitName);
                    
                    //Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();
                    if (type == pUnits)
                    {
                        Player.PlayerUnit pU = unit.GetComponent<Player.PlayerUnit>();

                        //set unnits
                        pU.baseStats = GetBasicUnitStats(unitName);

                    }
                    else if(type == eUnits)
                    {
                        Enemy.EnemyUnit eU = unit.GetComponent<Enemy.EnemyUnit>();

                        //set unnits
                        eU.baseStats = GetBasicUnitStats(unitName);
                    }




                    //if upgrades add them
                    //add upgrades to unit stats

                }
            }
        }
    }
}

