namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Windows;

    class Client
    {
        private string _ipAddress;
        private string _port;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private TcpClient client;
        private IPEndPoint IpEndPoint;
        private NetworkStream netStream;
        
        public Client(string ipAddress, string port, MainWindowViewModel mainWindowViewModel)
        {
            this._ipAddress = ipAddress;
            this._port = port;
            this._mainWindowViewModel = mainWindowViewModel;
        }

        public void ConnectClient()
        {
            client = new TcpClient();
            IpEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), int.Parse(_port));
            
            try
            {
                //connect our tcp client to the endpoint with the specified ip and port
                client.Connect(IpEndPoint);

                if(client.Connected)
                {
                    _mainWindowViewModel.IsConnected();
                    netStream = client.GetStream();

                    //set background listener on background thread
                    BackgroundStreamListener backgroundStreamListener = new BackgroundStreamListener();
                    Thread thread = new Thread(() => backgroundStreamListener.ClientRunMessageListener(netStream, client, _mainWindowViewModel));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public void SendMessage(string message)
        {
            //if netstream can write to the network stream
            if (netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                netStream.Write(sendBytes, 0, sendBytes.Length);
            }
            else
            {
                netStream.Close();
                return;
            }
        }
    }
}