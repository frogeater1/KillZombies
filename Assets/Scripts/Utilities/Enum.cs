namespace KillZombies
{
    public enum WeaponType
    {
        Gun = 0,
        ZombieAttack = 1,
    }

    public enum FSMState
    {
        Idle, //指非攻击，死亡，拾取中等特殊状态，包含移动和跳跃
        Attack,
        Death,
    }
}