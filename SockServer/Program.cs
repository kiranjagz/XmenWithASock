using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSockets.Core.Common.Socket;
using XSockets.Core.XSocket.Helpers;
using XSockets.Plugin.Framework;
using XSockets.Plugin.Framework.Attributes;

namespace SockServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start server
            Task.Run(() =>
            {
                using (var server = XSockets.Plugin.Framework.Composable.GetExport<XSockets.Core.Common.Socket.IXSocketServerContainer>())
                {
                    server.Start();
                }
            });

            // Start client
            Task.Run(async () =>
            {
                await Task.Delay(5000);
                var c = new XSockets.XSocketClient("ws://localhost:4502", "http://localhost");
                await c.Open();
    
                var controller = c.Controller("chat");
                controller.OnOpen += Controller_OnOpen;
                controller.On<Message>("message", m => Console.WriteLine($"{m.Text}"));

                for (var i = 0; i < 2; i++)
                {
                    await controller.Invoke("message", new Message { Text = $"Hello Universe.{i}", Time = DateTime.Now });
                }               
            });

            Console.ReadLine();
        }

        private static void Controller_OnOpen(object sender, XSockets.Common.Event.Arguments.OnClientConnectArgs e)
        {
            Console.WriteLine($"ConnectionId. {e.ClientInfo.ConnectionId}");
            Console.WriteLine($"PersistentId. { e.ClientInfo.PersistentId}");
            Console.WriteLine("chat opened");
        }
    }
}
