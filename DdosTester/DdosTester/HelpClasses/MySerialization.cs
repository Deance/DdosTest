using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DdosTester.HelpClasses
{
    abstract class MySerialization // Special class to serialize/deserialize objects to send it by network. 
    {                     // Made for 'Settings' and 'Int32' objects.
        
        // Returns a byte[] for a network stream to send it to client;
        public static byte[] SettingsToBytes(Settings obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        // Returns a Settings object from byte[] from network stream;
        public static Settings ByteToSettings(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (Settings)formatter.Deserialize(stream);
        }

        public static byte[] Int32ToBytes(Int32 obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }

        public static Int32 BytesToInt32(byte[] serializedAsBytes)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Write(serializedAsBytes, 0, serializedAsBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (Int32)formatter.Deserialize(stream);
        }
    }
}
