using System.Net.Sockets;
using System.Text;

namespace SocialMe
{
    class BackgroundStreamListener
    {
        public void RunListener(TcpClient _client, MainWindowViewModel _mainWindowViewModel)
        {
            NetworkStream netStream = _client.GetStream();

            if (netStream.CanRead)
            {
                byte[] bytes = new byte[_client.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)_client.ReceiveBufferSize);

                string receivedMessage = Encoding.UTF8.GetString(bytes);
                _mainWindowViewModel.DisplayMessage(receivedMessage);
            }
            else
            {
                _mainWindowViewModel.DisplayErrorMessage();
                _client.Close();
                netStream.Close();
                return;
            }
        }
    }
}
