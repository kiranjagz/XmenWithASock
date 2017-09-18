using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSockets.Core.XSocket.Helpers;
using XSockets.Owin.Host;
using XSockets.Plugin.Framework.Attributes;

namespace SockServer
{
    [XSocketMetadata(PluginAlias = "chat")]
    public class Chat : XSockets.Core.XSocket.XSocketController
    {
        public override Task OnOpened()
        {
            return base.OnOpened();
        }

        public async Task Message(Message m)
        {
            await this.InvokeToAll(m, "message");
        }
    }

    public class Message
    {
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
