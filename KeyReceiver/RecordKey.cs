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
        public ItemState resultState { get; set; }

        public bool isKey;

        public RecordKey(Keys clientKey, Keys serverKey)
        {
            this.isKey = true;
            this.clientKey = clientKey;
            this.serverKey = serverKey;
        }

        public RecordKey(Keys clientKey, ItemState resultState)
        {
            this.isKey = false;
            this.clientKey = clientKey;
            this.resultState = resultState;
        }

        public int getCode()
        {
            if (isKey)
            {
                return (int) serverKey;
            }

            return resultState.toCode();
        }
    }
}
