using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;
using System.Reflection;

namespace SeranfuenMirrorSync.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region ' Events '

        public event EventHandler<ShowMessageRequestedEventArgs> ShowMessageRequested;
        public event EventHandler<UserConfirmationRequestedEventArgs> UserConfirmationRequested;

        #endregion

        #region ' INotifyPropertyChanged '

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ' IDataErrorInfo '

        private Dictionary<string, string> _errorInfo = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                if (_errorInfo.ContainsKey(columnName))
                {
                    return _errorInfo[columnName];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (value == null)
                {
                    _errorInfo.Remove(columnName);
                } else
                {
                    _errorInfo[columnName] = value;
                }
            }
        }

        public string Error
        {
            get;
            set;
        }

        #endregion

        #region ' Properties '

        public virtual string DisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        #endregion

        #region ' Methods '

        public override string ToString()
        {
            return DisplayName;
        }

        protected virtual void ValidateNotNullField(string propertyName, object value)
        {
            if (value == null)
            {
                this[propertyName] = string.Format("{0} must have a value", GetPropertyDisplayName(propertyName).Capitalize());
            }
        }

        protected virtual void ValidateNotNullOrEmpty(string propertyName, string value)
        {
            if (value == null)
            {
                this[propertyName] = string.Format("{0} cannot be empty", GetPropertyDisplayName(propertyName).Capitalize());
            }
        }

        protected virtual string GetPropertyDisplayName(string propertyName)
        {
            MemberInfo property = GetType().GetProperty(propertyName);
            var dd = property.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            if (dd != null)
            {
                return dd.DisplayName;
            } else
            {
                return null;
            }
        }

        protected virtual void ShowUserMessage(string message, ShowMessageRequestedEventArgs.MessageType type = ShowMessageRequestedEventArgs.MessageType.Info)
        {
            ShowMessageRequested?.Invoke(this, new ShowMessageRequestedEventArgs(message, type));
        }

        protected virtual bool RequestUserConfirmation(string message)
        {
            var args = new UserConfirmationRequestedEventArgs(message, false);
            UserConfirmationRequested?.Invoke(this, args);
            return !args.Cancel;
        }

        #endregion
    }

    #region ' EventArgs '

    public class ShowMessageRequestedEventArgs : EventArgs
    {
        public enum MessageType
        {
            Info,
            Warning,
            Error
        }

        public ShowMessageRequestedEventArgs(string message, MessageType type = MessageType.Info)
        {
            Message = message;
            Type = type;
        }

        public string Message
        {
            get;
            private set;
        }

        public MessageType Type
        {
            get;
            private set;
        }
    }

    public class UserConfirmationRequestedEventArgs : CancelEventArgs
    {
        public UserConfirmationRequestedEventArgs(string message, bool cancel ) : base(cancel)
        {
            Message = message;
        }

        public string Message
        {
            get;
            private set;
        }
    }

    #endregion
}