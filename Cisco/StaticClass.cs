using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CiscoService {
    public  class Caller {
        
            private static Caller _instance;
            private static readonly object padlock = new object();
            public string CallerPassword;
            public string CallerNumber;
            public string CallerId;
            private Caller()
            {
            }


        public static Caller Instance()
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Caller();
                    }
                }
                return _instance;

            }   
    }
}
