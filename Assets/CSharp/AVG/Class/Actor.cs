using System;
using System.Xml.Linq;
using Poi;
using UnityEngine;

namespace AVG
{
    internal class Actor:iActor
    {
        private int iD;
        private Vector3 pos;
        private Mood type;
        private float x;
        private static readonly Vector2 size = new Vector2(256,516);
        public Actor(XElement item)
        {
            iD = item.Attribute("ID").Value.ToInt();
            type = (Mood)Enum.Parse(typeof(Mood), item.Attribute("Mood").Value);
            x = item.Attribute("x").Value.ToFloat();
            pos = new Vector3(x * 1280 - 640,0,0);
        }

        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Mood Mood
        {
            get
            {
                return type;
            }
        }

        public string Name
        {
            get
            {
                return Writing.Get(iD);
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Vector3 Position
        {
            get
            {
                return pos;
            }
        }

        public Vector2 Size
        {
            get
            {
                return size;
            }
        }
    }
}