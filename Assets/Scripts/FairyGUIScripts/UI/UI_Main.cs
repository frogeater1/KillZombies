/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace KillZombies.UI
{
    public partial class UI_Main : GComponent
    {
        public GButton m_test1;
        public GButton m_test2;
        public const string URL = "ui://qhc1lzfqehq74";

        public static UI_Main CreateInstance()
        {
            return (UI_Main)UIPackage.CreateObject("UI", "Main");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_test1 = (GButton)GetChildAt(0);
            m_test2 = (GButton)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}