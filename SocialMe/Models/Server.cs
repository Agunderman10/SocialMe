namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    class Server
    {
        #region Private Members
        private string _port;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private TcpListener listener;
        private Socket socket;
        private NetworkStream netStream;
        #endregion
        #region Constructors
        public Server(string port, MainWindowViewModel mainWindowViewModel)
        {
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
            listener = new TcpListener(IPAddress.Any, int.Parse(_port));
            listener.Start();

            socket = listener.AcceptSocket();

            if(socket.Connected)
            {
                _mainWindowViewModel.IsConnected();

                netStream = new NetworkStream(socket);

                //set background listener on background thread
                BackgroundStreamListener backgroundStreamListener = new BackgroundStreamListener();
                Thread thread = new Thread(() => backgroundStreamListener.ServerRunMessageListener(netStream, socket, _mainWindowViewModel));
                thread.IsBackground = true;
                thread.Start();


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
        #endregion
    }
}