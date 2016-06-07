using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using uhttpsharp;
using uhttpsharp.Handlers;
using uhttpsharp.Headers;
using uhttpsharp.Listeners;
using uhttpsharp.RequestProviders;

using Newtonsoft.Json.Linq;

namespace CitizenMP.Server.HTTP
{
    class HttpServer
    {
        private Dictionary<string, Func<IHttpHeaders, IHttpContext, Task<JObject>>> m_handlers;

        private Resources.ResourceManager m_resourceManager;

        private Configuration m_configuration;

        public HttpServer(Configuration config, Resources.ResourceManager resManager)
        {
            m_configuration = config;
            m_resourceManager = resManager;

            m_handlers = new Dictionary<string, Func<IHttpHeaders, IHttpContext, Task<JObject>>>();

            m_handlers["initconnect"] = InitConnectMethod.Get(config, resManager.GameServer);
            m_handlers["getconfiguration"] = GetConfigurationMethod.Get(config, resManager);
        }
    }
}