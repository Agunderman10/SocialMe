namespace SocialMe
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Input;

    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private string _userDefinedPort;
        private string _serverIP;
        private string _serverPort;
        private string _userMessage = "Type Something...";
        private readonly ObservableCollection<string> _messageHistory = new ObservableCollection<string>();

        #endregion
        #region Public Properties
        //the user's IP
        public string UserIP
        {
            get { return GetLocalIPAddress(); }
        }

        //the port the user defines
        public string UserDefinedPort
        {
            get { return this._userDefinedPort; }
            set
            {
                if(this._userDefinedPort != value)
                {
                    this._userDefinedPort = value;
                }
            }
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

        public string ServerPort
        {
            get { return this._serverPort; }
            set
            {
                if(this._serverPort != value)
                {
                    this._serverPort = value;
                }
            }
        }

        //message that the user sends
        public string UserMessage
        {
            get { return this._userMessage; }
            set
            {
                if (this._userMessage != value)
                {
                    this._userMessage = value;
                    OnPropertyChanged(nameof(UserMessage));
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
            if(string.IsNullOrWhiteSpace(UserMessage))
            {
                return;
            }

            //adds Me> prefix to messages
            string message = "Me> " + UserMessage;
            _messageHistory.Add(message);
            UserMessage = string.Empty;
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
