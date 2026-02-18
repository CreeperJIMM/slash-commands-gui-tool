using DiscordAPI;
using Newtonsoft.Json;
using slash_commands_gui_tool;
using System.IO;
using System.Security.Policy;

namespace LocalFileRW
{
    internal partial class LocalFileHelper
    {
        static readonly string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string fileFolder = Path.Combine(localPath, "SlashCommandsGUITool", "SavedFiles");
        public static string FilePath = "";
        public static string FileName = "";
        public static DateTime SavedWriteTime = DateTime.MinValue;
        public LocalFileHelper()
        {
            Directory.CreateDirectory(fileFolder);
        }
        public bool OpenFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = Resource.OpenJsonFile;
            openFile.Filter = $"{Resource.JsonFile} (*.json) | *.json";
            openFile.Multiselect = false;
            openFile.CheckFileExists = true;
            openFile.InitialDirectory = fileFolder;
            DialogResult dialogResult = openFile.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                FilePath = openFile.FileName;
                FileName = openFile.SafeFileName;
                return true;
            }
            else {
                return false;
            }
        }
        public bool SaveFile()
        {
            string[] jsonFiles = Directory.GetFiles(fileFolder, "*.json");
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = Resource.SaveJsonFile;
            saveFile.Filter = $"{Resource.JsonFile} (*.json) | *.json";
            saveFile.InitialDirectory = fileFolder;
            saveFile.FileName = $"example{jsonFiles.Length+1}.json";
            DialogResult dialogResult = saveFile.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                FilePath = saveFile.FileName;
                return true;
            }
            else {
                return false;
            }
        }
        public SlashCommand[]? LoadFile()
        {
            if(!File.Exists(FilePath)) return null;
            try {
                string jsonContent = File.ReadAllText(FilePath);
                SlashCommand[]? data = JsonConvert.DeserializeObject<SlashCommand[]>(jsonContent);
                SavedWriteTime = File.GetLastWriteTime(FilePath);
                if (data == null) return null;
                return data;
            }
            catch {
                return null;
            }
        }
        public bool WriteFile(SlashCommand[] slash)
        {
            try {
                slash = FilterSlash(slash);
                JsonSerializerSettings settings = new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                string jsonData = JsonConvert.SerializeObject(slash, settings);
                File.WriteAllText(FilePath, jsonData);
                SavedWriteTime = File.GetLastWriteTime(FilePath);
                return true;
            }
            catch {
                return false;
            }
        }
        public SlashCommand[] FilterSlash(SlashCommand[] slashs)
        {
            foreach(SlashCommand slash in slashs) {
                if (slash.type != 1) slash.description = null;
                slash.id = null;
                slash.application_id = null;
                slash.version = null;
            }
            return slashs;
        }
        public String GetFilePath()
        {
            return FilePath;
        }
        public DateTime GetFileWriteTime()
        {
            return SavedWriteTime;
        }
    }
}
