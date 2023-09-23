using System.Collections.Generic;
using FairyGUI;
using KillZombies.Unit;
using UnityEngine;
using Utilities;

namespace KillZombies.Managers
{
    public class UnitMgr : MonoBehaviour
    {
        public Character characterPrefab;
        public Zombie[] zombiePrefabs;

        public Transform container;
        public Pool<Zombie> zombiePool;

        public readonly Vector3 CharacterSpawnPos = new Vector3(500, 0, 180);

        public Character me;

        public void Init()
        {
            zombiePool = new Pool<Zombie> { container = container };
        }


        public void LogicalUpdate()
        {
            me.LogicalUpdate();

            List<Zombie> removeList = new List<Zombie>();

            foreach (var zombie in zombiePool.Instances())
            {
                if (zombie.destroyFlag)
                {
                    removeList.Add(zombie);
                }
                else
                {
                    zombie.LogicalUpdate();
                }
            }

            foreach (var zombie in removeList)
            {
                zombiePool.Release(zombie);
            }
        }

        public void CharacterSpawn()
        {
            me = Instantiate(characterPrefab, CharacterSpawnPos, Quaternion.identity);
            me.Init();
        }

        public void SpawnZombie()
        {
            var randomVector2 = Random.insideUnitCircle.normalized * 20;
            var pos = new Vector3(me.transform.position.x + randomVector2.x, 0, me.transform.position.z + randomVector2.y);
            var zombie = zombiePool.Get(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos, Quaternion.identity);
            zombie.Init();
        }

        public void KillZombies()
        {
            foreach (var zombie in zombiePool.Instances())
            {
                zombie.curHp = 0;
                zombie.fsmCtrl.SwitchState(FSMState.Death);
            }
        }
    }
}