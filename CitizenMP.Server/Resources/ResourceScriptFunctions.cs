﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Neo.IronLua;

namespace CitizenMP.Server.Resources
{
    class ResourceScriptFunctions
    {
        [LuaMember("GetInvokingResource")]
        static string GetInvokingResource_f()
        {
            return (ScriptEnvironment.LastEnvironment ?? ScriptEnvironment.CurrentEnvironment).Resource.Name;
        }

        [LuaMember("StopResource")]
        static bool StopResource_f(string resourceName)
        {
            var resourceManager = ScriptEnvironment.CurrentEnvironment.Resource.Manager;

            var resource = resourceManager.GetResource(resourceName);

            if (resource == null)
            {
                return false;
            }

            if (resource.State != Resources.ResourceState.Running)
            {
                return false;
            }

            try
            {
                if (!resource.Stop())
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Game.RconPrint.Print("Error stopping resource {0}: {1}.\n", resourceName, e.Message);

                return false;
            }
        }

        [LuaMember("StartResource")]
        static bool StartResource_f(string resourceName)
        {
            var resourceManager = ScriptEnvironment.CurrentEnvironment.Resource.Manager;

            var resource = resourceManager.GetResource(resourceName);

            if (resource == null)
            {
                return false;
            }

            if (resource.State != Resources.ResourceState.Stopped && resource.State != ResourceState.Starting)
            {
                return false;
            }

            try
            {
                resource.Start().Wait();

                return true;
            }
            catch (Exception e)
            {
                Game.RconPrint.Print("Error starting resource {0}: {1}.\n", resourceName, e.Message);

                return false;
            }
        }

        [LuaMember("SetGameType")]
        static void SetGameType_f(string gameType)
        {
            ScriptEnvironment.CurrentEnvironment.Resource.Manager.GameServer.GameType = gameType;
        }

        [LuaMember("SetMapName")]
        static void SetMapName_f(string mapName)
        {
            ScriptEnvironment.CurrentEnvironment.Resource.Manager.GameServer.MapName = mapName;
        }
    }
}
