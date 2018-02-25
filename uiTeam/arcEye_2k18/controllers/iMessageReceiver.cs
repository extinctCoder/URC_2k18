using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDMessaging;
using XDMessaging.Messages;

namespace arcEye_2k18.controllers
{
    public interface iMessageReceiver
    {
        //TypedDataGram <DynamicObject> _typedData
        //{
        //    get;
        //    set;
        //}

        IXDBroadcaster _xdBroadcaster
        {
            get;
            set;
        }

        XDMessagingClient _xdMessagingClient
        {
            get;
            set;
        }

        IXDListener _xdListener
        {
            get;
            set;
        }

        BackgroundWorker _backgroundWorker
        {
            get;
            set;
        }

        void initIMessageReceiver(String _contentName);

        void XdListenerOnMessageReceived(object sender, XDMessageEventArgs xdMessageEventArgs);

        void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs);

        void BackgroundWorkerOnRunWorkerCompleted(object sender,
            RunWorkerCompletedEventArgs runWorkerCompletedEventArgs);

    }
}

