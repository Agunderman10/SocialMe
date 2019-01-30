using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialMe
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private Members
        private string _clientIP;
        private string _serverIP;
        private string _clientMessage = "Type Something...";
        private readonly ObservableCollection<string> _messageHistory = new ObservableCollection<string>();

        #endregion
        #region Public Properties
        //the user's IP
        public string ClientIP
        {
            get { return this._clientIP; }
            set
            {
                if (this._clientIP != value)
                {
                    this._clientIP = value;
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
        //adds clients messages to the message container
        private void AddToMessageHistory()
        {
            //if client message is empty don't add empty space to message history
            if(string.IsNullOrWhiteSpace(ClientMessage))
            {
                return;
            }

            _messageHistory.Add(ClientMessage);
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
