using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using slash_commands_gui_tool;

namespace DiscordAPI
{
    public partial class DiscordAPIhelper
    {
        private System.Windows.Forms.Timer timer;
        private static readonly HttpClient client = new HttpClient();
        private DateTime lastRequestTime;
        ToolStripProgressBar? bar;

        public DiscordAPIhelper()
        {
            lastRequestTime = DateTime.MinValue;
            timer = new System.Windows.Forms.Timer();
        }
        public async Task<bool> DetectNetwork()
        {
            try {
                client.DefaultRequestHeaders.Clear();
                var response = await client.GetAsync("https://discord.com/");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public async Task<DiscordUser?> GetClientAsync(Form1.Client Client)
        {
            try {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {Client.ClientToken}");
                // 發送GET請求到API
                var response = await client.GetAsync("https://discord.com/api/v10/users/@me");
                // 確保請求成功
                response.EnsureSuccessStatusCode();
                // 讀取回應的內容並返回
                string responseData = await response.Content.ReadAsStringAsync();
                DiscordUser? data = JsonConvert.DeserializeObject<DiscordUser>(responseData);
                return data;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public async Task<SimpleSlash[]?> GetCommandsListAsync(Form1.Client Client)
        {
            try {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {Client.ClientToken}");
                // 發送GET請求到API
                var response = await client.GetAsync($"https://discord.com/api/v10/applications/{Client.ApplicationID}/commands");
                // 確保請求成功
                response.EnsureSuccessStatusCode();
                // 讀取回應的內容並返回
                string responseData = await response.Content.ReadAsStringAsync();
                SimpleSlash[]? data = JsonConvert.DeserializeObject<SimpleSlash[]>(responseData);
                if( data == null ) return null;
                return data;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public async Task<SlashCommand?> GetCommandAsync(Form1.Client Client, string commandId, ToolStripProgressBar bar)
        {
            try {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bot {Client.ClientToken}");
                if (bar != null) {
                    this.bar = bar;
                    Timer();
                }
                // 發送GET請求到API
                var response = await client.GetAsync($"https://discord.com/api/v10/applications/{Client.ApplicationID}/commands/{commandId}");
                // 確保請求成功
                response.EnsureSuccessStatusCode();
                if(bar != null) {
                    timer.Stop();
                    bar.Value = 100;
                }
                // 讀取回應的內容並返回
                string responseData = await response.Content.ReadAsStringAsync();
                SlashCommand? data = JsonConvert.DeserializeObject<SlashCommand>(responseData);
                if (data == null) return null;
                return data;
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
        public async Task<bool> PostCommandAsync(Form1.Client Client, SlashCommand slash, ToolStripProgressBar bar)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("accept-language", "zh-TW,zh-CN;q=0.9,zh;q=0.8,en-US;q=0.7");
            client.DefaultRequestHeaders.Add("Authorization", $"Bot {Client.ClientToken}");
            if (bar != null) {
                this.bar = bar;
                Timer();
            }
            string jsonData = JsonConvert.SerializeObject(slash);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://discord.com/api/v10/applications/{Client.ApplicationID}/commands", content);
            if (bar != null) {
                timer.Stop();
                bar.Value = 100;
            }
            if (response.IsSuccessStatusCode) {
                string responseData = await response.Content.ReadAsStringAsync();
                return true;
            }
            else {
                return false;
            }
        }
        public async Task<bool> DeleteCommandAsync(Form1.Client Client, string slashId, ToolStripProgressBar bar)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bot {Client.ClientToken}");
            if (bar != null) {
                this.bar = bar;
                Timer();
            }
            var response = await client.DeleteAsync($"https://discord.com/api/v10/applications/{Client.ApplicationID}/commands/{slashId}");
            if (bar != null) {
                timer.Stop();
                bar.Value = 100;
            }
            if (response.IsSuccessStatusCode) {
                string responseData = await response.Content.ReadAsStringAsync();
                return true;
            }
            else {
                return false;
            }
        }
        public double IsRequestAllowed(int cooldown)
        {
            DateTime currentTime = DateTime.Now;
            if ((currentTime - lastRequestTime) > TimeSpan.FromMilliseconds(cooldown)) {
                lastRequestTime = currentTime;
                return -1;
            }
            else {
                return (TimeSpan.FromMilliseconds(cooldown) - (currentTime - lastRequestTime)).TotalSeconds;
            }
        }
        private void Timer()
        {
            timer.Interval = 50;
            timer.Tick += Timer_Tick;
            bar.Value = 0;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(bar.Value < 100) {
                bar.Value = bar.Value + 5;
            }
        }

    }
    public class DiscordUser
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Avatar { get; set; }
        public string? Discriminator { get; set; }
        public int PublicFlags { get; set; }
        public int Flags { get; set; }
        public bool Bot { get; set; }
        public string? Banner { get; set; }
        public string? AccentColor { get; set; }
        public bool MfaEnabled { get; set; }
        public string? Locale { get; set; }
        public int PremiumType { get; set; }
        public string? Email { get; set; }
        public bool Verified { get; set; }
    }
    public class SimpleSlash
    {
        public string? id { get; set; }
        public int type { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }
    public class SlashCommand
    {
        public string? id { get; set; }
        public string? application_id { get; set; }
        public string? version { get; set; }
        public int type { get; set; }
        public string? name { get; set; }
        public Dictionary<string, string>? name_localizations { get; set; }
        public string? description { get; set; }
        public Dictionary<string, string>? description_localizations { get; set; }
        public string? default_member_permissions { get; set; }
        public List<CommandOption>? options { get; set; }
        public bool? nsfw { get; set; }
        public static T? Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            T? json = JsonConvert.DeserializeObject<T>(serialized);
            if (json != null) return json;
            else return default;
        }
    }
    public class CommandOption
    {
        public int type { get; set; }
        public string name { get; set; }
        public Dictionary<string, string>? name_localizations { get; set; }
        public string description { get; set; }
        public Dictionary<string, string>? description_localizations { get; set; }
        public bool? required { get; set; }
        public bool? autocomplete { get; set; }
        public List<CommandOptionChoice>? choices { get; set; }
        public List<CommandOption>? options { get; set; }

        public CommandOption(int type, string name, string description)
        {
            this.type = type;
            this.name = name;
            this.description = description;
            this.required = false;
        }
    }
    public class CommandOptionChoice
    {
        public string? name { get; set; }
        public Dictionary<string, string>? name_localizations { get; set; }
        public object? value { get; set; }
    }

    public class CommandType
    {
        public string name { get; set; }
        public int value { get; set; }
        public bool sub { get; set; }
        public bool choice { get; set; }
        public CommandType(string name, int value, bool sub, bool choice)
        {
            this.name = name;
            this.value = value;
            this.sub = sub;
            this.choice = choice;
        }
    }

    public class Language
    {
        public string? locale { get; set; }
        public string? name { get; set; }
        public string? display { get; set; }
        public Language(string locale, string name, string display)
        {
            this.locale = locale;
            this.name = name;
            this.display = display;
        }
    }
}