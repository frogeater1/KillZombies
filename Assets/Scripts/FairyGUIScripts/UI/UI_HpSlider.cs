/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace KillZombies.UI
{
    public partial class UI_HpSlider : GComponent
    {
        public GImage m_value;
        public const string URL = "ui://qhc1lzfqehq73";

        public static UI_HpSlider CreateInstance()
        {
            return (UI_HpSlider)UIPackage.CreateObject("UI", "HpSlider");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_value = (GImage)GetChildAt(1);
            Init();
        }
        partial void Init();
    }
}