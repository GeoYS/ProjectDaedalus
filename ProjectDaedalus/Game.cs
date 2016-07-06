using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDaedalus
{
    public class Game
    {
        public const string LOBBY = "game/lobby";
        public const string INPUT_LOBBY_REQUEST = "game/input/lobby/request";
        public const string INPUT_LOBBY_HEARTBEAT = "game/input/lobby/heartbeat";
        public const string INPUT_LOBBY_STARTGAME= "game/input/lobby/startgame";

        public Lobby lobby { get; set; }

        public World world { get; set; }

        public void Start()
        {
            IFirebaseConfig config = new FirebaseConfig();

            config.BasePath = "https://project-828640652417633159.firebaseio.com/";
            config.AuthSecret = "geaMUqiYfEWcCTz2SiEUldY5XCPzzVjQ3n5FDkIL";

            IFirebaseClient client = new FirebaseClient(config);

            Initialise(client);

            // main delta loop

            Stopwatch timer = new Stopwatch();
            timer.Start();
            while(true)
            {
                if(timer.ElapsedMilliseconds >= 1000) // 1 second delta
                {
                    Console.WriteLine("Loop performing...");
                    Loop(client, timer.ElapsedMilliseconds);
                    timer.Restart();
                }
            }
        }

        private void Initialise(IFirebaseClient client)
        {
            FirebaseResponse responseClearGame =
                client.Delete("game");

            lobby = new Lobby();

            FirebaseResponse responseInitLobby =
                client.Set<Lobby>("game/lobby", lobby);
        }

        private void Loop(IFirebaseClient client, long delta)
        {
            // handle new users
            FirebaseResponse responseLobbyRequests = client.Get(INPUT_LOBBY_REQUEST);
            var lobbyRequests = JsonConvert.DeserializeObject<Dictionary<string, LobbyRequest>>(responseLobbyRequests.Body);
            if (lobbyRequests != null)
            {
                foreach (var key in lobbyRequests.Keys)
                {
                    Console.WriteLine(lobbyRequests[key].name + " is joining");

                    Player newPlayer = new Player
                    {
                        name = lobbyRequests[key].name,
                        isActive = false,
                        heartBeat = false
                    };

                    if (!lobby.players.ContainsKey(newPlayer.name))
                    {
                        lobby.players.Add(newPlayer.name, newPlayer);
                        lobby.numPlayers ++;
                    }

                    FirebaseResponse responseDeleteR =
                        client.Delete(INPUT_LOBBY_REQUEST + "/" + key);
                }
            }

            // update user connections
            foreach(var player in lobby.players.Values)
            {
                if(player.isActive)
                { 
                    player.doHeartBeat();
                }
            }

            // handle lobby heartbeats
            FirebaseResponse responseHeartbeat = client.Get(INPUT_LOBBY_HEARTBEAT);
            var lobbyHeartbeats = JsonConvert.DeserializeObject<Dictionary<string, LobbyHeartbeat>>(responseHeartbeat.Body);
            if (lobbyHeartbeats != null)
            {
                foreach (var key in lobbyHeartbeats.Keys)
                {

                    if (lobby.players.Keys.Contains(key))
                    {
                        Player player = lobby.players[key];

                        if(player.isActive)
                        {
                            string secret = lobbyHeartbeats[key].secret;
                            player.doHeartBeat(DateTime.Now.MilliTicks(), secret);
                        }
                        else
                        {
                            Console.WriteLine(key + " has joined");
                            string newSecret = lobbyHeartbeats[key].secret;
                            player.setSecret(newSecret);
                            player.doHeartBeat(DateTime.Now.MilliTicks(), newSecret);
                        }
                    }

                    FirebaseResponse responseDeleteR =
                        client.Delete(INPUT_LOBBY_HEARTBEAT + "/" + key);
                }
            }

            client.Update<Lobby>(LOBBY, lobby);

            // start game
            FirebaseResponse startGameResponse = client.Get(INPUT_LOBBY_STARTGAME);
            var gameConfig = JsonConvert.DeserializeObject<Dictionary<string, string>>(startGameResponse.Body);
            if (gameConfig != null && this.world == null)
            {
                this.world = World.GenerateWorld(lobby.players.Keys.ToList(), 30);
                Repo.QueueUpdate(() => client.Set<World.Map>("game/world/map", world.map));
                client.Delete(INPUT_LOBBY_STARTGAME);
            }

            Repo.PerformAllUpdates();
        }

        public class Lobby
        {
            public Lobby()
            {
                players = new Dictionary<string, Player>();
                gameStarted = false;
                numPlayers = 0;
            }

            public bool gameStarted { get; set; }
            public int numPlayers { get; set; }
            public Dictionary<string, Player> players { get; set; }
        }

        public class LobbyRequest
        {
            public LobbyRequest() { }
            public string name { get; set; }
        }

        public class LobbyHeartbeat
        {
            public LobbyHeartbeat() { }
            public string name { get; set; }
            public string secret { get; set; }
        }

        public class Player
        {
            public string name { get; set; }
            public bool isActive { get; set; }
            public bool heartBeat { get; set; }
            public string secretCheck { get; set; }

            private long lastHeartBeat { get; set; }
            private string secret { get; set; }

            public Player()
            {
                lastHeartBeat = 0;
                isActive = false;
                heartBeat = true;
                secret = null;
                secretCheck = "";
            }

            public string getSecret()
            {
                return secret;
            }

            public void setSecret(string newSecret)
            {
                if(secret == null)
                {
                    secret = newSecret;
                    secretCheck = newSecret.Substring(newSecret.Length - 6);
                }
            }

            public void resetSecret()
            {
                secret = null;
                secretCheck = "";
            }
            
            public void doHeartBeat(long hearbeatMilli = 0, string secret = "")
            {
                if(hearbeatMilli == 0)
                {
                    long nowMilli = DateTime.Now.MilliTicks();
                    if (nowMilli - this.lastHeartBeat > 20000)
                    {
                        isActive = false;
                        resetSecret();
                    }
                    if(nowMilli - this.lastHeartBeat > 1000)
                    {
                        heartBeat = true;
                    }
                    else
                    {
                        heartBeat = false;
                    }
                }
                else if(this.secret != null && secret.Equals(this.secret))
                {
                    this.lastHeartBeat = hearbeatMilli;
                    isActive = true;
                    heartBeat = false;
                }
            }
        }
    }

    public static class ExtensionMethods
    {
        public static long MilliTicks(this DateTime dateTime)
        {
            return dateTime.Ticks / 10000;
        }
    }
}
