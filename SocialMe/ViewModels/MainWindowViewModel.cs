using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialMe
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private string _serverIP;
        private string _clientMessage = "Type Something...";
        private readonly ObservableCollection<string> _messageHistory = new ObservableCollection<string>();

        #endregion
        #region Public Properties
        //the user's IP
        public string ClientIP
        {
            get { return GetLocalIPAddress(); }
        }

        //the other messender's IP
        public string ServerIP
        {
            get { return this._serverIP; }
            set
            {
                if (this._serverIP != value)
                {
                    this._serverIP = value;
                }
            }
        }

        //message that the user sends
        public string ClientMessage
        {
            get { return this._clientMessage; }
            set
            {
                if (this._clientMessage != value)
                {
                    this._clientMessage = value;
                    OnPropertyChanged(nameof(ClientMessage));
                }
            }
        }
        
        //container for messages
        public IEnumerable<string> MessageHistory
        {
            get { return _messageHistory; }
        }

        public ICommand AddToMessageHistoryCommand
        {
            get { return new ButtonCommands(AddToMessageHistory); }
        }
        #endregion
        #region Private Methods
        //get local ip address
        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(var ipAddress in host.AddressList)
            {
                if(ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString();
                }
            }
            throw new Exception("Unknown IP Address!");
        }

        //adds clients messages to the message container
        private void AddToMessageHistory()
        {
            //if client message is empty don't add empty space to message history
            if(string.IsNullOrWhiteSpace(ClientMessage))
            {
                return;
            }

            //adds Me> prefix to messages
            string message = "Me> " + ClientMessage;
            _messageHistory.Add(message);
            ClientMessage = "";
        }
        #endregion
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
