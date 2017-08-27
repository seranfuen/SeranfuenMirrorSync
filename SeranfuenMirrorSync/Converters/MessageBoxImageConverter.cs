using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static SeranfuenMirrorSync.ViewModels.ShowMessageRequestedEventArgs;

namespace SeranfuenMirrorSync.Converters
{
    public static class MessageBoxImageConverter
    {
        public static MessageBoxImage FromViewModelMessageType(MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Error:
                    return MessageBoxImage.Error;
                case MessageType.Warning:
                    return MessageBoxImage.Warning;
                default:
                    return MessageBoxImage.Information;
            }
        }
    }
}
