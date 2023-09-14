using System.Collections;
using System.Collections.Generic;
using KillZombies.Managers;
using KillZombies.Unit;
using UnityEngine;
using Utilities;

namespace KillZombies
{
    public class Game : MonoSingletonBase<Game>
    {
        public InputMgr inputMgr;
        public CameraMgr cameraMgr;
        public Character character;
    }
}