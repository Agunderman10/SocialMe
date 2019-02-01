namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    class Client
    {
        private string _ipAddress;
        private string _port;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public Client(string ipAddress, string port, MainWindowViewModel mainWindowViewModel)
        {
            this._ipAddress = ipAddress;
            this._port = port;
            this._mainWindowViewModel = mainWindowViewModel;
        }


        public void ConnectClient()
        {
            TcpClient client = new TcpClient();
            IPEndPoint IpEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), int.Parse(_port));

            try
            {
                //connect our tcp client to the endpoint with the specified ip and port
                client.Connect(IpEndPoint);

                if(client.Connected)
                {
                    _mainWindowViewModel.IsConnected();
                }
            }
            catch(Exception)
            {
                return;
            }

        }
    }
}