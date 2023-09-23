using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
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
        public UIMgr uiMgr;


        public Transform container;
        public Pool<Bullet> bulletPool;
        public DamageNumber damageNumberPrefab;

        //tmp
        public float tick;
        public bool gamestart;

        protected override void Awake()
        {
            base.Awake();
            inputMgr = GetComponent<InputMgr>();
            cameraMgr = GetComponent<CameraMgr>();
            unitMgr = GetComponent<UnitMgr>();
            uiMgr = GetComponent<UIMgr>();

            cameraMgr.Init();
            uiMgr.Init();
            unitMgr.Init();

            bulletPool = new Pool<Bullet> { container = container };

            Physics.simulationMode = SimulationMode.Script;
        }

        private void GameStart()
        {
            unitMgr.CharacterSpawn();
        }

        public void Update()
        {
            if (tick <= 0)
            {
                if (!gamestart)
                {
                    GameStart();
                    gamestart = true;
                }

                Physics.Simulate(Common.TickTime);
                tick += Common.TickTime;

                LogicalUpdate();
                unitMgr.LogicalUpdate();
            }

            tick -= Time.deltaTime;
        }

        public void LogicalUpdate()
        {
            List<Bullet> removeList = new();

            foreach (var bullet in bulletPool.Instances())
            {
                if (bullet.destroyFlag)
                {
                    removeList.Add(bullet);
                }
                else
                {
                    bullet.LogicalUpdate();
                }
            }

            foreach (var bullet in removeList)
            {
                bulletPool.Release(bullet);
            }
        }
    }
}