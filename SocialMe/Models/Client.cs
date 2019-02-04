namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Windows;

    class Client
    {
        private string _ipAddress;
        private string _port;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private TcpClient client;
        private IPEndPoint IpEndPoint;

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

                    BackgroundStreamListener backgroundStreamListener = new BackgroundStreamListener();
                    backgroundStreamListener.RunMessageListener(client, _mainWindowViewModel);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}