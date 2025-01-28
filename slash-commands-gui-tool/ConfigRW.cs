using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigRW
{
    internal class ConfigHelper
    {
        static readonly string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static readonly string appFolder = Path.Combine(localPath, "SlashCommandsGUITool");
        static readonly string configpath = Path.Combine(appFolder, "config.ini");
        public ConfigHelper()
        {
            if (!File.Exists(configpath)) {
                CreateEmptyConfigFile();
            }
        }

        // 創建一個空的配置檔案（如果檔案不存在）
        private void CreateEmptyConfigFile()
        {
            try {
                var defaultConfig = new List<string>
                {
                "[General]",
                "Language=null",
                "AutoSync=true",
                "",
                "[UserSettings]",
                "Theme=System"
            };
                File.WriteAllLines(configpath, defaultConfig);
            }
            catch (Exception ex) {
                Console.WriteLine($"創建配置檔案時發生錯誤：{ex.Message}");
            }
        }
        // 讀取指定區塊的值
        public string? GetValue(string section, string key)
        {
            string[] lines = File.ReadAllLines(configpath);
            bool inSection = false;

            foreach (var line in lines) {
                if (line.Trim().StartsWith($"[{section}]")) {
                    inSection = true;
                    continue;
                }
                if (inSection && line.Contains("=")) {
                    string[] parts = line.Split(new char[] { '=' }, 2);
                    if (parts[0].Trim() == key) {
                        return parts[1].Trim();
                    }
                }
                if (inSection && line.Trim().StartsWith("[")) {
                    break;
                }
            }
            return null;
        }
        // 設定指定區塊中的 key 值
        public void SetValue(string section, string key, string value)
        {
            var lines = new List<string>(File.ReadAllLines(configpath));
            bool sectionFound = false;
            bool keyUpdated = false;
            for (int i = 0; i < lines.Count; i++) {
                if (lines[i].Trim().StartsWith($"[{section}]")) {
                    sectionFound = true;
                    continue;
                }
                if (sectionFound && lines[i].Contains('=') && lines[i].Split('=')[0].Trim() == key) {
                    lines[i] = $"{key}={value}";
                    keyUpdated = true;
                    break;
                }
                if (sectionFound && lines[i].Trim().StartsWith('[')) {
                    break;
                }
            }
            if (!sectionFound) {
                lines.Add($"[{section}]");
            }
            if (!keyUpdated) {
                lines.Add($"{key}={value}");
            }
            try {
                File.WriteAllLines(configpath, lines);
            }
            catch (Exception ex) {
                Console.WriteLine($"寫入配置檔案時發生錯誤：{ex.Message}");
            }
        }
        // 移除指定區塊中的設定項
        public void RemoveKey(string section, string key)
        {
            var lines = new List<string>(File.ReadAllLines(configpath));
            bool sectionFound = false;
            bool keyFound = false;
            for (int i = 0; i < lines.Count; i++) {
                if (lines[i].Trim().StartsWith($"[{section}]")) {
                    sectionFound = true;
                    continue;
                }
                if (sectionFound && lines[i].Contains('=')) {
                    string[] parts = lines[i].Split(new char[] { '=' }, 2);
                    if (parts[0].Trim() == key) {
                        lines.RemoveAt(i);
                        keyFound = true;
                        break;
                    }
                }
                if (sectionFound && lines[i].Trim().StartsWith('[')) {
                    break;
                }
            }
            if (keyFound) {
                try {
                    File.WriteAllLines(configpath, lines);
                }
                catch (Exception ex) {
                    Console.WriteLine($"刪除配置項時發生錯誤：{ex.Message}");
                }
            }
            else {
                Console.WriteLine("未找到指定的 key。");
            }
        }
    }
}
