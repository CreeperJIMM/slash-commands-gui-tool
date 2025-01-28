using ConfigRW;
using DarkModeForms;

namespace ThemeSW
{
    internal class ThemeHelper
    {
        static ConfigHelper config = new ConfigHelper();
        DarkModeCS? darkMode;
        string? theme;
        public ThemeHelper()
        {
            
        }
        public void SetTheme(Form form)
        {
            //Theme
            theme = config.GetValue("UserSettings", "Theme");
            if(darkMode != null) {
                if(theme == "Dark") darkMode.ApplyTheme(true);
                else if (theme == "White") darkMode.ApplyTheme(false);
                else  darkMode.ApplyTheme(DarkModeCS.DisplayMode.SystemDefault);
            }
            else {
                DarkModeCS.DisplayMode mode = DarkModeCS.DisplayMode.ClearMode;
                if (theme == "Dark") mode = DarkModeCS.DisplayMode.DarkMode;
                if (theme == "White") mode = DarkModeCS.DisplayMode.ClearMode;
                else mode = DarkModeCS.DisplayMode.SystemDefault;
                darkMode = new DarkModeCS(form) {
                    ColorMode = mode
                };
            }
        }
    }
}
