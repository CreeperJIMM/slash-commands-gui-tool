namespace slash_commands_gui_tool
{
    internal class FormHelper
    {
        public static bool IsReachBottom(Form form, EventArgs e, int limit = 0)
        {
            // 獲取視窗的當前位置和尺寸
            int windowBottom = form.Bounds.Bottom + limit;
            // 獲取螢幕的工作區域
            Screen currentScreen = Screen.FromControl(form);
            int screenBottom = currentScreen.WorkingArea.Bottom;
            // 檢查是否碰觸螢幕底部
            if (windowBottom >= screenBottom) 
                return true;
            else
                return false;
        }
        public static bool UpHasSpace(Form form, EventArgs e, bool up, int limit = 0)
        {
            int windowHeight = form.Bounds.Height;
            int windowTop = form.Bounds.Top;
            Screen currentScreen = Screen.FromControl(form);
            int screenTop = currentScreen.WorkingArea.Top;
            // 檢查是否有足夠空間往上移動並加長高度
            if (windowTop > screenTop && up) {
                int availableSpace = windowTop - screenTop;
                int extendHeight = Math.Min(availableSpace, limit);
                form.SetBounds(form.Bounds.X, form.Bounds.Y - extendHeight, form.Bounds.Width, windowHeight + extendHeight);
                return true;
            }
            else {
                return false;
            }
        }
    }
}
