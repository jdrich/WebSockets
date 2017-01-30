using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSockets
{
    public struct Vector2
    {
        int x;
        int y;
    }

    public class Canvas
    {
        protected static Canvas canvas;

        public static Canvas Get()
        {
            if(canvas == null)
            {
                canvas = new Canvas();
            }

            return canvas;
        }

        protected List<string> Players;

        protected Dictionary<string, List<List<Vector2>>> PlayerSegments;

        protected Canvas()
        {
            Players = new List<string>();
            PlayerSegments = new Dictionary<string, List<List<Vector2>>>();
        }

        public void AddPlayer(string color)
        {
            if(!Players.Contains(color))
            {
                Players.Add(color);

                PlayerSegments[color] = new List<List<Vector2>>();
            }
        }

        public void AddPlayerSegment(string player, Vector2 position, bool newSegment = false)
        {   
            if(PlayerSegments[player].Count() == 0 || newSegment)
            {
                PlayerSegments[player].Add(new List<Vector2>());
            }

            List<Vector2> current = PlayerSegments[player].Last();

            current.Add(position);
        }
    }
}