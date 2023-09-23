using KillZombies.UI;
using UnityEngine;

namespace KillZombies.Managers
{
    public class UIMgr : MonoBehaviour
    {
        public UI_Main uiMain;

        public void Init()
        {
            UIBinder.BindAll();
        }
    }
}