using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace KeyReceiver
{
    public partial class frmKeyReceiver : Form
    {
        private IKeyboardMouseEvents m_GlobalHook;

        /**
         *  Constructor of this form.
         */
        public frmKeyReceiver()
        {
            InitializeComponent();
            btnClientDisconnect.Enabled = false;
            btnServerStop.Enabled = false;

            Subscribe();
        }
        
        /**
         * Subscribe to keyboard hook.
         */
        public void Subscribe()
        {
            m_GlobalHook = Hook.GlobalEvents();
            m_GlobalHook.KeyDown += GlobalHookKeyDown;
        }

        /**
         * Unsubscribe to keyboard hook.
         */
        public void Unsubscribe()
        {
            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
            m_GlobalHook.Dispose();
        }

        /**
         * Form closed event.
         */
        private void frmKeyReceiver_FormClosed(object sender, FormClosedEventArgs e)
        {
            Unsubscribe();
        }

        #region SERVER

        private bool serverRunning = false;
        private TcpListener listener;
        private TcpClient receiveSocket;
        private InputSimulator simulator = new InputSimulator();

        /**
         * Server start button.
         */
        private void btnServerStart_Click(object sender, EventArgs e)
        {
            btnServerStart.Enabled = false;
            int port = Int32.Parse(txtPort.Text);
            IPAddress local = IPAddress.Parse("0.0.0.0");
            listener = TcpListener.Create(port);
            listener.Start();

            initListen();
            serverRunning = true;
            btnServerStop.Enabled = true;
            readServerIP();
        }

        private void readServerIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress hostaddress = host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            if (hostaddress != null)
            {
                txtServerIP.Text = hostaddress.ToString();
            } else
            {
                txtServerIP.Text = "Error..";
            }
        }

        /**
         * Initiate async listen handler.
         */
        private void initListen()
        {
            listener.BeginAcceptTcpClient(HandleAsyncConnection, listener);
        }

        /**
         * Handle incoming connection on listener socket.
         */
        private async void HandleAsyncConnection(IAsyncResult res)
        {
            if (serverRunning)
            {
                // Listen for upcoming connection.
                initListen();
            } else
            {
                // Or cancel if sever is stopped.
                return;
            }

            // Socket for connected client
            receiveSocket = listener.EndAcceptTcpClient(res);
            StringBuilder sb = new StringBuilder();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            using (NetworkStream networkStream = receiveSocket.GetStream())
            {
                var buffer = new byte[10];
                var byteCount = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                // Read key from network stream
                int keyValue = BitConverter.ToInt32(buffer, 0);
                // Convert to VirtualKeyCode
                VirtualKeyCode key = (VirtualKeyCode) keyValue;

                // Simulate a keypress on the client
                switch (key) {
                    /* Zoom in/out can be done using keypress (smallest step) */
                    case VirtualKeyCode.F1:
                    case VirtualKeyCode.F2:
                        simulator.Keyboard.KeyPress(key);
                        break;
                    /* Icon size can be done using 10 ms keydown (will not register otherwise) */
                    case VirtualKeyCode.F3:
                    case VirtualKeyCode.F4:
                        simulator.Keyboard.KeyDown(key);
                        Thread.Sleep(10);
                        simulator.Keyboard.KeyUp(key);
                        break;
                    /* Boolean settings can be done using 50 ms (will not register otherwise) */
                    default:
                        simulator.Keyboard.KeyDown(key);
                        Thread.Sleep(50);
                        simulator.Keyboard.KeyUp(key);
                        break;
                }
            }

            receiveSocket.Close();
        }

        /**
         * Server stop button.
         */
        private void btnServerStop_Click(object sender, EventArgs e)
        {
            btnServerStop.Enabled = false;
            serverRunning = false;
            if (receiveSocket != null)
            {
                receiveSocket.Close();
            }
            listener.Stop();
            btnServerStart.Enabled = true;
            txtServerIP.Text = "Waiting...";
        }

        #endregion

        #region CLIENT

        private TcpClient client;
        private int clientPort;

        /**
         * Client connect button.
         */
        private void btnClientConnect_Click(object sender, EventArgs e)
        {
            clientConnected = true;
            btnClientConnect.Enabled = false;
            clientPort = Int32.Parse(txtPort.Text);
            btnClientDisconnect.Enabled = true;
        }

        bool clientConnected = false;
        /**
         * Client disconnect button.
         */
        private void btnClientDisconnect_Click(object sender, EventArgs e)
        {
            clientStop();
        }

        /**
         * Stop the client connection.
         */
        private void clientStop()
        {
            btnClientDisconnect.Enabled = false;
            if (client != null)
            {
                client.Close();
            }
            clientConnected = false;
            btnClientConnect.Enabled = true;
        }

        // Maintain a dictionary of actiuve keys and their keyvalues.
        private List<RecordKey> keyDict = new List<RecordKey>();

        /**
         * On keyboard event.
         */
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (isEditing)
            {
                RecordKey editedKey = keyDict.First(k => k.clientKey.ToString() == editingClientKey);
                editedKey.serverKey = key;
                isEditing = false;
                fillList();
            }
            else if (adding)
            {
                if (keyDict.Any(k => k.clientKey == key))
                {
                    keyDict.RemoveAll(k => k.clientKey == key);
                }
                else
                {
                    keyDict.Add(new RecordKey(key, key));
                }
                fillList();
            }
            else if (keyDict.Any(k => k.clientKey == key) && clientConnected)
            {
                // Assume we can have only one
                RecordKey recordKey = keyDict.First(k => k.clientKey == key);
                try
                {
                    client = new TcpClient(txtClientAddress.Text, clientPort);
                    using (NetworkStream stream = client.GetStream())
                    {
                        Byte[] data = BitConverter.GetBytes((int)recordKey.serverKey); // Encoding.UTF8.GetBytes(e.KeyValue);
                        stream.Write(data, 0, data.Length);
                    }
                    client.Close();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException: {0}", ex);
                    clientStop();
                }
            }
        }

        /**
         * Fill the gui list of buttons using the key dictionary.
         */
        private void fillList()
        {
            lstButtons.Items.Clear();
            foreach (RecordKey key in keyDict)
            {
                lstButtons.Items.Add(new ListViewItem(new[] { key.clientKey.ToString(), key.serverKey.ToString() }));
            }
        }

        // Boolean indicating we are adding buttons to the list.
        private bool adding = false;
        /**
         * Add button event.
         */
        private void btnAddButtons_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                return;
            }

            if (!adding)
            {
                btnAddButtons.Text = "Stop";
            }
            else
            {
                btnAddButtons.Text = "Add";
            }
            adding = !adding;
        }
        #endregion

        /**
         * Client/server radio buttons.
         */
        private void rdbtnClient_CheckedChanged(object sender, EventArgs e)
        {
            grpClient.Enabled = rdbtnClient.Checked;
        }

        private void rdbtnServer_CheckedChanged(object sender, EventArgs e)
        {
            grpServer.Enabled = rdbtnServer.Checked;
        }

        bool isEditing = false;
        string editingClientKey;
        private void lstButtons_MouseDown(object sender, MouseEventArgs e)
        {
            Point mousePos = lstButtons.PointToClient(Control.MousePosition);
            ListViewHitTestInfo hitTest = lstButtons.HitTest(mousePos);
            if (hitTest.Item != null && !adding && !isEditing)
            {
                isEditing = true;
                editingClientKey = hitTest.Item.Text;

                hitTest.Item.SubItems[1].Text = "...";
            }
        }
    }
}
