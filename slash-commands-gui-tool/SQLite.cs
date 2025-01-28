using slash_commands_gui_tool;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using DPAPI;

namespace SQLite
{
    public partial class SQLiteHelper
    {   
        static readonly string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static readonly string appFolder = Path.Combine(localPath, "SlashCommandsGUITool");
        private static  string dbFilePath = "database.db";
        static DPAPIHelper dpapi = new DPAPIHelper();
        SQLiteConnection conn;
        public SQLiteHelper()
        {
            Directory.CreateDirectory(appFolder);
            dbFilePath = Path.Combine(appFolder, "database.db");
            conn = new SQLiteConnection($"Data Source={dbFilePath};Version=3;");
        }
        // 你可以修改iv的變量，修改完請記得刪除data.db讓程序重新創建新的資料庫
        private static string key = "-1";  // 16 字節密鑰(系統自行生成)
        private static readonly string VerityTXT = "MadeBYCreeperJIMM"; //驗證用字串
        private static readonly string iv = "1145141145141145";   // 16 字節初始化向量
        // AES 加密函數
        public string Encrypt(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (StreamWriter sw = new StreamWriter(cs))
                sw.Write(plainText);
            return Convert.ToBase64String(ms.ToArray());
        }
        // AES 解密函數
        public string Decrypt(string cipherText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Convert.FromBase64String(key);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] Text = Convert.FromBase64String(cipherText);
            string T = "";
            try {
                using MemoryStream ms = new MemoryStream(Text);
                using CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using StreamReader sr = new StreamReader(cs);
                T += sr.ReadLine();
            }
            catch {
                return "-1";
            }
            return T;
        }

        public bool CreateDatabase()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
                return true;
            }
            return false;
        }
        public bool DeleteLocalFolder()
        {
            try {
                if (Directory.Exists(appFolder)) Directory.Delete(appFolder, true);
                return true;
            }
            catch {
                return false;
            }
        }
        public void DeleteDatabase()
        {
            if (File.Exists(dbFilePath)) File.Delete(dbFilePath);
        }
        // 創建 Users 資料表
        public void CreateTable()
        {
            conn.Open();
            string createTableSQL = @"
                CREATE TABLE IF NOT EXISTS Clients (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ApplicationID TEXT NOT NULL,
                    ClientToken TEXT NOT NULL
                );";
            SQLiteCommand cmd = new SQLiteCommand(createTableSQL, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }
        public void CreateData()
        {
            key = dpapi.CreateKey();
            string? KEY = dpapi.Encrypt(key);
            if(KEY == null) return;
            int a = KEY.Length;
            this.InsertClient(this.Encrypt(VerityTXT), "0", KEY, false);
        }
        public bool VerifyData()
        {
            Form1.Client ?client = GetBasic();
            if (client == null) return false;
            else
            {
                string j = Encoding.UTF8.GetString(Convert.FromBase64String(client.ClientToken));
                int a = client.ClientToken.Length;
                string? k = dpapi.Decrypt(client.ClientToken);
                if(k == null) return false;
                key = k;
                if(this.Decrypt(client.Name) == VerityTXT) return true;
                else return false;
            }
        }
        // 插入資料
        public void InsertClient(string Name, string applicationID, string clientToken, bool IsEncrypt)
        {
            conn.Open();
            string insertSQL = "INSERT INTO Clients (Name, ApplicationID, ClientToken) VALUES (@Name, @ApplicationID, @ClientToken)";
            SQLiteCommand cmd = new SQLiteCommand(insertSQL, conn);
            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@ApplicationID", applicationID);
            if (IsEncrypt) clientToken = Encrypt(clientToken);
            cmd.Parameters.AddWithValue("@ClientToken", clientToken);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            return;
        }
        //刪除資料
        public void DeleteClient(int id)
        {
            conn.Open();
            string sql = "DELETE FROM Clients WHERE Id = @Id;";
            using var command = new SQLiteCommand(sql, conn);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            command.Dispose();
            conn.Close();
            return;
        }
        // 查詢資料
        public Form1.Client[] GetClients()
        {
            List<Form1.Client> clients = new List<Form1.Client>();
            conn.Open();
            string selectSQL = "SELECT ID, Name, ApplicationID, ClientToken FROM Clients";
            SQLiteCommand cmd = new SQLiteCommand(selectSQL, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string Name = reader.GetString(1);
                string applicationID = reader.GetString(2);
                string clientToken = Decrypt(reader.GetString(3));
                clients.Add(new Form1.Client(id, Name, applicationID, clientToken));
            }
            reader.Close();
            conn.Close();
            cmd.Dispose();
            clients.RemoveAt(0);
            return clients.ToArray();
        }
        public Form1.Client? GetBasic()
        {
            conn.Open();
            string selectSQL = "SELECT * FROM Clients WHERE ID = 1";
            SQLiteCommand cmd = new SQLiteCommand(selectSQL, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            Form1.Client? client = null;
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string Name = reader.GetString(1);
                    string applicationID = reader.GetString(2);
                    string clientToken = reader.GetString(3);
                    client = new Form1.Client(id, Name, applicationID, clientToken);
                }
            }
            reader.Close();
            conn.Close();
            cmd.Dispose();
            if (client != null) return client;
            else return null;
        }
    }
}
