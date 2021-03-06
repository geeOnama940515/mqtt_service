using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace mqttservice
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        #region Variables
        public static ManagedMqttClient mqttClient;
        public static MqttFactory MqttFactory;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (var dbContext = new hydroPhonicsDBEntities())
                {
                    if (dbContext.Database.Exists())
                    {
                        lstNotifier.Items.Add("Status : Connected at " + DateTime.Now);
                        //  await ConnectAsync();
                        //await StartMQTT();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"DB Connection");
            }

        }
        public async Task StartMQTT()
        {
            await ConnectMQTT();
            await Publish("Test", "Test123");
            mqttClient.UseConnectedHandler(async e => {           
            await SubscribeCommand("cmnd/#");
            await SubscribeHydrophonics("hydroponics/#");
            });
            mqttClient.UseApplicationMessageReceivedHandler(MessageReceived);
        }
        private async Task ConnectMQTT()
        {
            string clientId = Guid.NewGuid().ToString();
            string mqttURI = "124.107.183.2";
            string mqttUser = "roger";
            string mqttPassword = "password";
            int mqttPort = 1883;
            bool mqttSecure = false;

            var messageBuilder = new MqttClientOptionsBuilder()
              .WithClientId(clientId)
              .WithCredentials(mqttUser, mqttPassword)
              .WithTcpServer(mqttURI, mqttPort)
              .WithCleanSession();

            var options = mqttSecure
              ? messageBuilder
                .WithTls()
                .Build()
              : messageBuilder
                .Build();

            var managedOptions = new ManagedMqttClientOptionsBuilder()
              .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
              .WithClientOptions(options)
              .Build();

            mqttClient = (ManagedMqttClient)new MqttFactory().CreateManagedMqttClient();
            await mqttClient.StartAsync(managedOptions);
        }
        private async Task Publish(String Topic, String Payload)
        {
            await mqttClient.PublishAsync(Topic, Payload).ConfigureAwait(false);
        }
        private async Task SubscribeHydrophonics(String Topic)
        {
            await mqttClient.SubscribeAsync(Topic);
        }
        private async Task SubscribeCommand(String Topic)
        {
            await mqttClient.SubscribeAsync(Topic);
        }
        private async Task Disconnect()
        {
            await mqttClient.StopAsync();
        }
        private void MessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                lstNotifier.Items.Add("Topic : " + e.ApplicationMessage.Topic + " Payload : " + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                int i = e.ApplicationMessage.Topic.IndexOf("/") + 1;
                string _Topic = e.ApplicationMessage.Topic.Substring(i);
                string _Payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                using (var dbContext = new hydroPhonicsDBEntities())
                {
                    dbContext.sensors.Add(new sensor { SensorType = _Topic, SensorData = _Payload, DateCreated = DateTime.Now.ToString() });
                    dbContext.SaveChanges();
                }
            }));
        }

        private async void startServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstNotifier.Items.Add("Connected to MQTT Server!");
            await StartMQTT();
        }

        private async void stopServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstNotifier.Items.Add("Stopped Subscription!");
            await Disconnect();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void restoreWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Form1.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Maximized;
                this.Show();
                this.Activate();
            }

        }
    }
}
