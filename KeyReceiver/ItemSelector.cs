using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyReceiver
{
    public partial class ItemSelector : Form
    {
        public ItemState ItemState { get; set; }
        public ItemSelector()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            ItemState state = new ItemState();
            state.EquipmentLvl2 = chkEqLvl2.Checked;
            state.EquipmentLvl3 = chkEqLvl3.Checked;
            state.Meds = chkMeds.Checked;
            state.Sniper = chkSniper.Checked;
            state.AR = chkAR.Checked;
            state.SMG = chkSMG.Checked;
            state.Scopes = chkScopes.Checked;

            this.ItemState = state; 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
