using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyReceiver
{
    class RecordKey
    {
        public Keys clientKey { get; }
        public Keys serverKey { get; set; }

        public RecordKey(Keys clientKey, Keys serverKey)
        {
            this.clientKey = clientKey;
            this.serverKey = serverKey;
        }
    }
}
