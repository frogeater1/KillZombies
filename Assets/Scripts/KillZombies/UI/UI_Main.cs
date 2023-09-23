namespace KillZombies.UI
{
    public partial class UI_Main
    {
        partial void Init()
        {
            Game.Instance.uiMgr.uiMain = this;
        }
    }
}