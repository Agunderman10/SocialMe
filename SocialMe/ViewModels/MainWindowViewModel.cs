namespace SocialMe
{

    //importing system components
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
        private string _userDefinedPortForNewServer;
        private string _connectingServerIP;
        private string _connectingServerPort;
        private string _userMessage;
        private bool _isServer;
        private readonly ObservableCollection<string> _messageHistory = new ObservableCollection<string>();
        private Server server;
        private Client client;
        #endregion
        #region Public Properties
        //the user's IP
        public string UserIP
        {
            get { return GetLocalIPAddress(); }
        }

        //the port the user defines
        public string UserDefinedPortForNewServer
        {
            get { return this._userDefinedPortForNewServer; }
            set
            {
                if(this._userDefinedPortForNewServer != value)
                {
                    this._userDefinedPortForNewServer = value;
                }
            }
        }

        //the other messenger's IP
        public string ConnectingServerIP
        {
            get { return this._connectingServerIP; }
            set
            {
                if (this._connectingServerIP != value)
                {
                    this._connectingServerIP = value;
                }
            }
        }

        //the server's port
        public string ConnectingServerPort
        {
            get { return this._connectingServerPort; }
            set
            {
                if(this._connectingServerPort != value)
                {
                    this._connectingServerPort = value;
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

        public bool IsServer
        {
            get { return this._isServer; }
            set
            {
                if(this._isServer != value)
                {
                    this._isServer = value;
                }
            }
        }

        public ICommand SendMessageCommand
        {
            get { return new ButtonCommands(SendMessage); }
        }

        public ICommand StartServerCommand
        {
            get { return new ButtonCommands(StartServer); }
        }

        public ICommand ConnectToServerCommand
        {
            get { return new ButtonCommands(ConnectToServer); }
        }
        #endregion
        #region Public Methods
        public void IsConnected()
        {
            _messageHistory.Add("Connected");
        }

        public void DisplayMessage(string message)
        {
            //puts delegate on UI dispatcher to avoid thread affinity runtime errors 
            //when adding new element to Observable Collection from non-Dispatcher thread
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                string shownMessage = "Your friend> " + message;
                _messageHistory.Add(shownMessage);
            });
        }

        public void DisplayErrorMessage()
        {
            _messageHistory.Add("Error receiving message");
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

        //starts the specified server
        private void StartServer()
        {
            server = new Server(UserDefinedPortForNewServer, this);
            server.StartServer();

            //set isserver to true so we can check this when sending messages later
            this.IsServer = true;
        }

        //connects the client to the specified server
        private void ConnectToServer()
        {
            client = new Client(ConnectingServerIP, ConnectingServerPort, this);
            client.ConnectClient();

            //set isserver to false so we can check this when sending messages later
            this.IsServer = false;
        }

        //adds clients messages to the message container
        private void SendMessage()
        {
            //if client message is empty don't add empty space to message history
            if (string.IsNullOrWhiteSpace(UserMessage))
            {
                return;
            }

            //if the user sending the message is the server send message from server, if user sending message
            //is client then send message from client
            if (IsServer)
            {
                server.SendMessage(UserMessage);
            }
            else if (!IsServer)
            {
                client.SendMessage(UserMessage);
            }
            else
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
