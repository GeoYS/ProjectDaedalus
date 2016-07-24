using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Request
    {
        public enum RequestType
        {
            MOVE_ARMY, DEPLOY_TROOP, SET_DEPLOYMENT_POINT
        }

        public string Player { get; }
        public RequestType Type { get; }
        public Dictionary<string, string> Data { get; }

        public Request(string player, RequestType type, Dictionary<string,string> data)
        {
            Player = player;
            Type = type;
            Data = data;
        }
    }
}
