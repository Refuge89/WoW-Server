using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using Framework.Contants.Game;
using Framework.Helpers;
using Framework.Network;
using Framework.Sessions;
using World_Server.Game.Entitys;
using World_Server.Game.World.Components;
using World_Server.Managers;
using World_Server.Sessions;

namespace World_Server
{
    public partial class Main : Form
    {
        private static readonly Assembly MAssembly = Assembly.GetEntryAssembly();

        public static WorldServer World { get; private set; }
        public static DatabaseManager Database;

        public static GameObjectComponent GameObjectComponent { get; private set; }

        public void Log(string message, Color color)
        {
            if (InvokeRequired)
            {
                Invoke(
                    new MethodInvoker(
                        delegate() {
                            ConsoleInput.AppendLine(message, color);
                        }));
            }
            else
            {
                ConsoleInput.AppendLine(message, color);
            }
        }

        public void addClient(string id, string msg)
        {
            BeginInvoke(new Action(() =>
            {
                ListViewItem newList = new ListViewItem(id);
                newList.SubItems.Add(msg);
                _Main.listView1.Items.Add(newList);
            }));
        }

        public void editClient(string id, string msg, string character = null)
        {
            BeginInvoke(new Action(() =>
            {
                var item = listView1.FindItemWithText(id);
                item.SubItems[1].Text = msg;
                if (character != null)
                {
                    item.SubItems[2].Text = character;
                }
            }));
        }

        public static Main _Main;

        public Main()
        {
            InitializeComponent();
            _Main = this;

            // Add columns
            listView1.Columns.Add("Id", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Name", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Char", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Location", -2, HorizontalAlignment.Left);

            var time = Time.getMSTime();

            Version ver = MAssembly.GetName().Version;

            ConsoleInput.AppendLine($"World of Warcraft (Realm Server/World Server)", Color.Green);
            ConsoleInput.AppendLine($"Supported WoW Client 1.2.1", Color.Green);
            ConsoleInput.AppendLine($"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}", Color.Green);
            ConsoleInput.AppendLine($"Running on .NET Framework Version {Environment.Version.Major}.{Environment.Version.Minor}.{Environment.Version.Build}", Color.Green);

            var worldPoint = new IPEndPoint(IPAddress.Any, 1001);
            World = new WorldServer();

            if (World.Start(worldPoint))
            {
                Database = new DatabaseManager();

                // Handlers
                HandlerManager.Boot();
                PlayerManager.Boot();

                // World Spawn
                GameObjectComponent = new GameObjectComponent();

                ConsoleInput.AppendLine($"Server is now listening at {worldPoint.Address}:{worldPoint.Port}", Color.BlueViolet);
                ConsoleInput.AppendLine($"Successfully started in {Time.getMSTimeDiff(time, Time.getMSTime()) / 1000}s", Color.BlueViolet);
            }

            GC.Collect();
            ConsoleInput.AppendLine($"Total Memory: {Convert.ToSingle(GC.GetTotalMemory(false) / 1024 / 1024)}MB", Color.BlueViolet);
        }

        public class WorldServer : Server
        {
            public static List<WorldSession> Sessions = new List<WorldSession>();
            public int ConnectionId = 0;

            public override Session GenerateSession(int connectionId, System.Net.Sockets.Socket connectionSocket)
            {
                connectionId++;
                WorldSession session = new WorldSession(connectionId, connectionSocket);
                Sessions.Add(session);
                _Main.addClient(connectionId.ToString(), "");
                return session;
            }

            public static void TransmitToAll(ServerPacket packet)
            {
                Sessions.FindAll(s => s.Character != null).ForEach(s => s.SendPacket(packet));
            }

            public static WorldSession GetSessionByPlayerName(string playerName)
            {
                return Sessions.Find(user => user.Character.Name.ToLower() == playerName.ToLower());
            }

            public static WorldSession GetSessionByUserName(string userName)
            {
                return Sessions.Find(user => user.ConnectionId == int.Parse(userName));
            }
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = listView1.SelectedItems[0].Text;

            UnitEntity entity = WorldServer.GetSessionByUserName(text).Entity.Target ?? WorldServer.GetSessionByUserName(text).Entity;

            try
            {
                foreach (string line in textBox1.Lines)
                {
                    string[] splitMessage = line.Split(' ');

                    if (splitMessage[3] == null)
                    {
                        entity.SetUpdateField((int) (UnitFields) Enum.Parse(typeof(UnitFields), splitMessage[0]),
                            int.Parse(splitMessage[1]));
                    }
                    else
                    {
                        entity.SetUpdateField((int)(UnitFields)Enum.Parse(typeof(UnitFields), splitMessage[0]) + int.Parse(splitMessage[1]) * 12, int.Parse(splitMessage[1]));
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
