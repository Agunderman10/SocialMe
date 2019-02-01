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
                    NetworkStream netStream = client.GetStream();

                    if(netStream.CanRead)
                    {
                        byte[] bytes = new byte[client.ReceiveBufferSize];
                        netStream.Read(bytes, 0, (int)client.ReceiveBufferSize);

                        string receivedMessage = Encoding.UTF8.GetString(bytes);
                        _mainWindowViewModel.DisplayMessage(receivedMessage);
                    }
                    else
                    {
                        _mainWindowViewModel.DisplayErrorMessage();
                        client.Close();
                        netStream.Close();
                        return;
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Cant connect");
            }

        }
    }
}