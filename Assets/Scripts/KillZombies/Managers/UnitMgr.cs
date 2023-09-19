using System.Collections.Generic;
using KillZombies.Unit;
using UnityEngine;

namespace KillZombies.Managers
{
    public class UnitMgr : MonoBehaviour
    {
        public Character characterPrefab;
        public Zombie[] zombiePrefabs;
        public LinkedList<Zombie> zombies = new();

        public readonly Vector3 CharacterSpawnPos = new Vector3(500, 0, 180);

        public Character me;

        public void LogicalUpdate()
        {
            me.LogicalUpdate();
            foreach (var zombie in zombies)
            {
                zombie.LogicalUpdate();
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
            var zombie = Instantiate(zombiePrefabs[Random.Range(0, zombiePrefabs.Length)], pos, Quaternion.identity);
            zombie.Init();
            zombies.AddLast(zombie);
        }

        public void KillZombies()
        {
            foreach (var zombie in zombies)
            {
                zombie.fsmCtrl.SwitchState(FSMState.Death);
            }
        }
    }
}