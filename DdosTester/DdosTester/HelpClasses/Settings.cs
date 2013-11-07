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
    }
}
