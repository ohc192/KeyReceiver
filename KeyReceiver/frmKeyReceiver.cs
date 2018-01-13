using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
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
                        return;
                    /* Icon size can be done using 10 ms keydown (will not register otherwise) */
                    case VirtualKeyCode.F3:
                    case VirtualKeyCode.F4:
                        simulator.Keyboard.KeyDown(key);
                        Thread.Sleep(10);
                        simulator.Keyboard.KeyUp(key);
                        return;
                    /* Boolean settings can be done using 50 ms (will not register otherwise) */
                    default:
                        simulator.Keyboard.KeyDown(key);
                        Thread.Sleep(50);
                        simulator.Keyboard.KeyUp(key);
                        return;
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
            btnClientConnect.Enabled = false;
            clientPort = Int32.Parse(txtPort.Text);
            btnClientDisconnect.Enabled = true;
        }

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
            btnClientConnect.Enabled = true;
        }

        // Maintain a dictionary of actiuve keys and their keyvalues.
        private Dictionary<string, int> keyDict = new Dictionary<string, int>();

        /**
         * On keyboard event.
         */
        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
        {
            if (adding)
            {
                string key = e.KeyCode.ToString();
                if (keyDict.ContainsKey(key))
                {
                    keyDict.Remove(key);
                }
                else
                {
                    keyDict.Add(key, e.KeyValue);
                }
                fillList();
            }
            else if (keyDict.Any(k => k.Value == e.KeyValue))
            { 
                try
                {
                    client = new TcpClient(txtClientAddress.Text, clientPort);
                    using (NetworkStream stream = client.GetStream())
                    {
                        Byte[] data = BitConverter.GetBytes(e.KeyValue); // Encoding.UTF8.GetBytes(e.KeyValue);
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
            foreach (string key in keyDict.Keys)
            {
                lstButtons.Items.Add(key);
            }
        }

        // Boolean indicating we are adding buttons to the list.
        private bool adding = false;
        /**
         * Add button event.
         */
        private void btnAddButtons_Click(object sender, EventArgs e)
        {
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
    }
}
