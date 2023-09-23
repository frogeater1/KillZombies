using UnityEngine;
using UnityEngine.Device;
using Screen = UnityEngine.Screen;

namespace KillZombies.UI
{
    public partial class UI_HpSlider
    {
        public Unit.Unit unit;

        public void Init(Unit.Unit u)
        {
            unit = u;
            Refresh();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Follow();
        }

        private void Follow()
        {
            float offsetY = 100;
            UnityEngine.Vector3 pos = Game.Instance.cameraMgr.mainCamera.WorldToScreenPoint(unit.transform.position);
            pos.y = Screen.height - pos.y; // 正常的屏幕坐标转化为fgui屏幕坐标
            SetXY(pos.x, pos.y - offsetY);
        }


        public void Refresh()
        {
            m_value.fillAmount = unit.curHp / unit.maxHp;
        }
    }
}