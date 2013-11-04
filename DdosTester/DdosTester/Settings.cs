using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DdosTester
{
    class Settings
    {
        private IPAddress[] _IPs;
        private AttackType _Type;

        public Settings()
        {
        }

        public IPAddress[] IPs
        {
            get
            {
                return _IPs;
            }
            set
            {
                _IPs = value;
            }
        }

        public AttackType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        // Returns a byte[] for a network stream to send it to client;
        public static byte[] SerializeObject(Settings obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        } 

        // Returns a Settings object from byte[] from network stream;
        public static Settings DeserializeObject(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (Settings)formatter.Deserialize(stream);
        }
    }
}
