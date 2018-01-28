using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeyReceiver
{
    class DCItemState
    {
        private Clicker clicker = new Clicker("DC2K17");
        private ItemState currentState = new ItemState();
        
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);

        internal void setState(ItemState newState)
        {
            // XOR the codes
            int codeDiff = currentState.toCode() ^ newState.toCode();
            if (codeDiff != 0)
            {
                ItemState diffState = new ItemState(codeDiff);

                Task.Run(async () =>
                {
                    semaphore.Wait();
                    await clicker.SetStateDiff(diffState);
                    semaphore.Release();
                });

                currentState = newState;
            }
        }
    }
}
