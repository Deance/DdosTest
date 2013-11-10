using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DdosTester
{
    [Serializable]
    public enum AttackType
    {
        TCP_SYN_Flood = 1,
        ICMP_Flood = 2,
        TCP_Flood = 3,
        Database_Attack = 4
    };
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
    }
}
