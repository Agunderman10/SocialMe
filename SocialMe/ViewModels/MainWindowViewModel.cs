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
    }
}
