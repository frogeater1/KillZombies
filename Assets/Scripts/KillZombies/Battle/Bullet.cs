using System.Collections;
using System.Collections.Generic;
using KillZombies;
using KillZombies.Unit;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TrailRenderer tailRenderer;

    public Weapon weapon;
    public Vector3 targetPos;

    public bool hited;
    public bool destroyFlag;

    public void Init(Weapon w)
    {
        weapon = w;
        if (tailRenderer)
            tailRenderer.Clear();
        hited = false;
        destroyFlag = false;
        targetPos = weapon.transform.position + weapon.range * weapon.owner.transform.forward;
    }


    public void LogicalUpdate()
    {
        if (transform.position == targetPos)
        {
            DelayDestroy();
            hited = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, weapon.speed * Common.TickTime);
        }
    }

    public virtual void Hit(Unit target)
    {
        weapon.Work(target);
        if (weapon.hitOne)
        {
            DelayDestroy();
        }
    }


    public void DelayDestroy()
    {
        destroyFlag = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Zombie")) return;

        // var thisDir = other.transform.position - transform.position;

        // if (skill.cfg.Angle is < 360 and > 0 && Vector3.Angle(thisDir, direction) > skill.cfg.Angle / 2) return;

        var unit = other.GetComponent<Unit>();
        if (unit == weapon.owner) return;
        if ((hited == false || !weapon.hitOne))
        {
            hited = true;
            Hit(unit);
        }
    }
}