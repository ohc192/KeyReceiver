using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KeyReceiver
{
    public class ItemState
    {
        public static int padding = 10;

        public bool EquipmentLvl2 = true;
        public bool EquipmentLvl3 = true;
        public bool Meds = true;
        public bool Sniper = true;
        public bool AR = true;
        public bool SMG = true;
        public bool Scopes = true;

        public ItemState()
        {

        }

        public static bool isValidCode(int code)
        {
            return code == -1 || code >= (1 << padding);
        }

        public ItemState(int code)
        {
            if (code == -1)
            {
                EquipmentLvl2 = EquipmentLvl3 = Meds = Sniper = AR = SMG = Scopes = false;
            }

            EquipmentLvl2 = (code & (1 << (padding + 0))) > 0;
            EquipmentLvl3 = (code & (1 << (padding + 1))) > 0;
            Meds = (code & (1 << (padding + 2))) > 0;
            Sniper = (code & (1 << (padding + 3))) > 0;
            AR = (code & (1 << (padding + 4))) > 0;
            SMG = (code & (1 << (padding + 5))) > 0;
            Scopes = (code & (1 << (padding + 6))) > 0;
        }

        public override string ToString()
        {
            if (isAll())
            {
                return "All";
            }

            if (isNone())
            {
                return "None";
            }

            string result = "";
            if (EquipmentLvl2)
                result += "Eq2, ";
            if (EquipmentLvl3)
                result += "Eq3, ";
            if (Meds)
                result += "Meds, ";
            if (Sniper)
                result += "SR, ";
            if (AR)
                result += "AR, ";
            if (SMG)
                result += "SMG, ";
            if (Scopes)
                result += "Scope, ";

            result = result.Trim().Trim(',');
            return result;
        }

        public bool isAll()
        {
            return EquipmentLvl2 && EquipmentLvl3 && Meds && Sniper && AR && SMG && Scopes;
        }

        public bool isAny()
        {
            return EquipmentLvl2 || EquipmentLvl3 || Meds || Sniper || AR || SMG || Scopes;
        }

        public bool isNone()
        {
            return !isAny();
        }

        public int toCode()
        {
            if (isNone())
            {
                return -1;
            }

            int result = 0;
            if (EquipmentLvl2)
                result |= (1 << (padding + 0));
            if (EquipmentLvl3)
                result |= (1 << (padding + 1));
            if (Meds)
                result |= (1 << (padding + 2));
            if (Sniper)
                result |= (1 << (padding + 3));
            if (AR)
                result |= (1 << (padding + 4));
            if (SMG)
                result |= (1 << (padding + 5));
            if (Scopes)
                result |= (1 << (padding + 6));

            return result;
        }
    }
}
