using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using StackExchange.NetGain;
using StackExchange.NetGain.WebSockets;

namespace WebChat
{
    public class NetGainsServer
    {
        public NetGainsServer()
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Loopback, 6002);
            using (var server = new TcpServer())
            {
                server.ProtocolFactory = WebSocketsSelectorProcessor.Default;
                server.ConnectionTimeoutSeconds = 60;
                server.Received += msg =>
                {
                    var conn = (WebSocketConnection)msg.Connection;
                    string reply = (string)msg.Value + " / " + conn.Host;
                    Console.WriteLine("[server] {0}", msg.Value);
                    msg.Connection.Send(msg.Context, reply);
                };
                server.Start("abc", endpoint);
                Console.WriteLine("Server running");
            }
        }
    }

    public class MvcApplication : System.Web.HttpApplication
    {
        static NetGainsServer NetGains = new NetGainsServer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}