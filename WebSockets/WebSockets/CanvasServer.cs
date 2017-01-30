
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace WebSockets.WebSockets
{
    public class CanvasServer : WebSocketBehavior
    {
        protected static List<string> messages = new List<string>();

        protected static Dictionary<string, int> playerLastIndex = new Dictionary<string, int>();

        protected static Regex messageRegex = new Regex(@"\#[0-9a-f]{6}\|\d+\|\d+\|(0|1)");
        
        protected void Clear(string player)
        {
            messages = new List<string>();
            playerLastIndex = new Dictionary<string, int>();

            Sessions.Broadcast(player + '>' + "clear");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Receive(e.Data);
        }
        
        protected void Receive(string data) 
        {
            string[] chunks = data.Split('>');
            string player = chunks[0];
            string drawMessage = chunks[1];

            if(drawMessage == "clear")
            {
                Clear(player);

                return;
            }

            if(drawMessage == "load")
            {
                Load(player); 

                return; 
            }
                    
            if(messageRegex.IsMatch(drawMessage.ToLower()))
            {
                messages.Add(data);

                string color = drawMessage.Split('|').First();
                                 
                Sessions.Broadcast(data);

                Parallel.ForEach(playerLastIndex.Values, v => { v++; });
            }
        }

        protected void Load(String player)
        {
            if (!playerLastIndex.Keys.Contains(player))
            {
                playerLastIndex[player] = 0;

                while (playerLastIndex[player] < messages.Count())
                {
                    Send(messages[playerLastIndex[player]]);

                    playerLastIndex[player]++;
                }
            }
        }
    }

}