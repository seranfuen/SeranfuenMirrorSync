using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;
using System.Reflection;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region ' Commands '

        private class RequestCloseCommand : ICommand
        {
            #region ' Events '

            public event EventHandler CanExecuteChanged;

            #endregion

            #region ' Fields '

            private ViewModel _parent;

            #endregion

            #region ' Ctor '

            public RequestCloseCommand(ViewModel parent)
            {
                _parent = parent;
                _parent.PropertyChanged += _parent_PropertyChanged;
            }

            private void _parent_PropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CanClose")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }

            #endregion

            #region ' Members '

            public bool CanExecute(object parameter)
            {
                return _parent.CanClose;
            }

            public void Execute(object parameter)
            {
                _parent.OnRequestedClose();
            }

            #endregion
        }

        #endregion

        #region ' Fields '

        private bool _canClose = true;
        private bool _hasChanged = false;
        private ICommand _closeCommand;

        #endregion

        #region ' Events '

        public event EventHandler<ShowMessageRequestedEventArgs> ShowMessageRequested;
        public event EventHandler<UserConfirmationRequestedEventArgs> UserConfirmationRequested;
        public event EventHandler<RequestedCloseEventArgs> RequestedClose;

        #endregion

        #region ' INotifyPropertyChanged '

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName, bool updateHasChanged = true)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (updateHasChanged)
            {
                _hasChanged = true;
            }
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

        #region ' Ctor '

        public ViewModel()
        {
            _closeCommand = new RequestCloseCommand(this);
        }

        #endregion

        #region ' Properties '

        public virtual bool HasChanged
        {
            get
            {
                return _hasChanged;
            }
            set
            {

                _hasChanged = value;
                OnPropertyChanged("HasChanged");
            }
        }

        public virtual string DisplayName
        {
            get
            {
                return string.Empty;
            }
        }

        public bool CanClose
        {
            get
            {
                return _canClose;
            }
            set
            {
                if (_canClose != value)
                {
                    _canClose = value;
                    OnPropertyChanged("CanClose");
                }
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand;
            }
        }

        #endregion

        #region ' Methods '

        public override string ToString()
        {
            return DisplayName;
        }

        protected virtual bool OnRequestedClose()
        {
            var args = new RequestedCloseEventArgs(false);
            RequestedClose?.Invoke(this, args);
            return !args.Cancel;
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

        protected virtual void ShowUserMessage(string message, string title, ShowMessageRequestedEventArgs.MessageType type = ShowMessageRequestedEventArgs.MessageType.Info)
        {
            ShowMessageRequested?.Invoke(this, new ShowMessageRequestedEventArgs(message,title, type));
        }

        protected virtual bool RequestUserConfirmation(string message, string title)
        {
            var args = new UserConfirmationRequestedEventArgs(message,title, false);
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

        public ShowMessageRequestedEventArgs(string message, string title, MessageType type = MessageType.Info)
        {
            Message = message;
            Type = type;
            Title = title;
        }

        public string Message
        {
            get;
            private set;
        }

        public string Title
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
        public UserConfirmationRequestedEventArgs(string message, string title, bool cancel ) : base(cancel)
        {
            Message = message;
        }

        public string Message
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }
    }

    public class RequestedCloseEventArgs : CancelEventArgs
    {
        public RequestedCloseEventArgs(bool cancel) : base(cancel)
        {
            
        }
    }

    #endregion
}