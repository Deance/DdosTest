using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace DdosTester
{
    public enum ClientStatus
    {
        Offline = 0,
        Online = 1
    }

    class Client : IComparable 
    {
        private IPAddress _IP;
        private ClientStatus _Status;

        public Client(IPAddress IP)
        {
            _IP = IP;
            _Status = ClientStatus.Offline;
        }

        public Client(TcpClient client)
        {
            
        }

        public Client(string IP)
        {
            _IP = IPAddress.Parse(IP);
            _Status = ClientStatus.Offline;
        }

        public ClientStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        
        public IPAddress IP
        {
            get
            {
                return _IP;
            }
            set
            {
                _IP = value;
            }
        }
        
        // Method deskribed how to compare Client objects. 
        // (In particular - how to sort array of Client objects and find come object on array)
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Client other = obj as Client;
            if (other != null)
                return IP.ToString().CompareTo(other.IP.ToString());
        else
           throw new ArgumentException("Object is not a Temperature");
        }

        public static bool operator == (Client client1, Client client2)
        {
            return (client1.IP.ToString() == client2.IP.ToString());
        }
        public static bool operator !=(Client client1, Client client2)
        {
            return (client1.IP.ToString() != client2.IP.ToString());
        }

    }


}
