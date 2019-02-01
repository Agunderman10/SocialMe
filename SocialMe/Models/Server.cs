namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class Server
    {
        #region Private Members
        private string _ipAddress;
        private string _message;
        private string _port;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private TcpListener listener;
        private Socket socket;
        private NetworkStream netStream;
        #endregion
        #region Constructors
        public Server(string ipAddress, string message, string port, MainWindowViewModel mainWindowViewModel)
        {
            this._ipAddress = ipAddress;
            this._message = message;
            this._port = port;
            this._mainWindowViewModel = mainWindowViewModel;
        }
        #endregion
        #region Public Properties

        #endregion
        #region Public Methods
        //start the server and listen for data
        public void StartServer()
        {
            //init and start new tcp listener, listening for any ip on the user specified port
            listener = new TcpListener(IPAddress.Any, int.Parse(_port)); //change ip later maybe
            listener.Start();

            socket = listener.AcceptSocket();
            netStream = new NetworkStream(socket);
            
            //if netstream can write to the network stream
            if(netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(_message);
                netStream.Write(sendBytes, 0, sendBytes.Length);
            }
            else
            {
                netStream.Close();
            }
        }
        #endregion
    }
}