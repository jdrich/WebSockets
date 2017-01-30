using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSockets.WebSockets;
using WebSocketSharp.Server;

namespace WebSockets
{
    public class WebSocketConfig
    {
        protected static WebSocketServer wssv;

        public static void Start()
        {
            wssv = new WebSocketServer(System.Net.IPAddress.Any, 63003);
            wssv.AddWebSocketService<CanvasServer>("/Canvas/Server");
            wssv.Start();
        }

        public static void Stop()
        {
            wssv.Stop();
        }
    }
}