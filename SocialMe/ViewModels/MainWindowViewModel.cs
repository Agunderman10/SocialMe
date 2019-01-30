using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMe
{
    class MainWindowViewModel
    {
        #region Private Members
        private string _clientIP;
        private string _serverIP;
        private string _clientMessage = "Type Something...";
        private readonly ObservableCollection<string> _messageHistory = new ObservableCollection<string>();
        #endregion
        #region Public Properties
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

        public string ClientMessage
        {
            get { return this._clientMessage; }
            set
            {
                if (this._clientMessage != value)
                {
                    this._clientMessage = value;
                }
            }
        }

        public IEnumerable<string> MessageHistory
        {
            get { return _messageHistory; }
        }
        #endregion
    }
}
