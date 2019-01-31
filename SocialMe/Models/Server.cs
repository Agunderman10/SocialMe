namespace SocialMe
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class Server
    {
        private IPAddress _ipAddress;
        private string _message;
        private string _port;

        public Server(IPAddress ipAddress, string message, string port)
        {
            this._ipAddress = ipAddress;
            this._message = message;
            this._port = port;
        }

        //start the server and listen for data
        public void StartServer()
        {
            //init and start new tcp listener, listening for any ip on the user specified port
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(_port)); //change ip later maybe
            listener.Start();

            Socket socket = listener.AcceptSocket();
            NetworkStream netStream = new NetworkStream(socket);

            //if netstream can write to the network stream
            if(netStream.CanWrite)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(_message);
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