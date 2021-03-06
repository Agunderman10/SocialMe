﻿namespace SocialMe
{
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Windows;

    class BackgroundStreamListener
    {
        //we run this listener on a background thread, listening for messages for the client
        public void ClientRunMessageListener(NetworkStream netStream, TcpClient client, MainWindowViewModel mainWindowViewModel)
        {
            while(netStream.CanRead)
            {
                try
                {
                    byte[] bytes = new byte[client.ReceiveBufferSize];
                    netStream.Read(bytes, 0, (int)client.ReceiveBufferSize);

                    string receivedMessage = Encoding.UTF8.GetString(bytes);
                    mainWindowViewModel.DisplayMessage(receivedMessage);
                }
                //this exception is thrown if the server disconnects from the client, we show this to the client to notify 
                //them of the disconnection
                catch(IOException e)
                {
                    MessageBox.Show(e.Message);
                    return;
                }
            }
                
            mainWindowViewModel.DisplayErrorMessage();
            client.Close();
            netStream.Close();
            return;            
        }

        //we run this listener on a background thread, listening for messages for the server
        public void ServerRunMessageListener(NetworkStream netStream, Socket socket, MainWindowViewModel mainWindowViewModel)
        {

            while (netStream.CanRead)
            {
                byte[] bytes = new byte[socket.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)socket.ReceiveBufferSize);

                string receivedMessage = Encoding.UTF8.GetString(bytes);
                mainWindowViewModel.DisplayMessage(receivedMessage);
            }

            mainWindowViewModel.DisplayErrorMessage();
            socket.Close();
            netStream.Close();
            return;
        }
    }
}