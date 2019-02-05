namespace SocialMe
{
    using System.Net.Sockets;
    using System.Text;

    class BackgroundStreamListener
    {
        //we run this listener on a background thread, listening for messages
        public void RunMessageListener(TcpClient _client, MainWindowViewModel _mainWindowViewModel)
        {
            NetworkStream netStream = _client.GetStream();

                while(netStream.CanRead)
                {
                    byte[] bytes = new byte[_client.ReceiveBufferSize];
                    netStream.Read(bytes, 0, (int)_client.ReceiveBufferSize);

                    string receivedMessage = Encoding.UTF8.GetString(bytes);
                    _mainWindowViewModel.DisplayMessage(receivedMessage);
                }
               
                _mainWindowViewModel.DisplayErrorMessage();
                _client.Close();
                netStream.Close();
                return;            
        }
    }
}