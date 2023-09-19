using System.Collections;
using System.Collections.Generic;
using KillZombies.Managers;
using KillZombies.Unit;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace KillZombies
{
    public class Game : MonoSingletonBase<Game>
    {
        public InputMgr inputMgr;
        public CameraMgr cameraMgr;
        public UnitMgr unitMgr;
        public PoolMgr poolMgr;

        //test
        public Unit.Unit testUnit;

        //tmp
        public float tick;


        protected override void Awake()
        {
            base.Awake();
            inputMgr = GetComponent<InputMgr>();
            cameraMgr = GetComponent<CameraMgr>();
            unitMgr = GetComponent<UnitMgr>();
            poolMgr = GetComponent<PoolMgr>();


            Physics.simulationMode = SimulationMode.Script;

            GameStart();
        }

        private void GameStart()
        {
            unitMgr.CharacterSpawn();
        }

        public void Update()
        {
            if (tick <= 0)
            {
                Physics.Simulate(Common.TickTime);
                tick += Common.TickTime;

                unitMgr.LogicalUpdate();
                poolMgr.LogicalUpdate();
            }

            tick -= Time.deltaTime;
        }
    }
}