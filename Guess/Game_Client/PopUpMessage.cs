using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Client
{
    public partial class PopUpMessage : Form
    {
        public PopUpMessage()
        {
            InitializeComponent();
        }
        public PopUpMessage(string PopUpString)
        {
            InitializeComponent();
            labelTextToDisplay.Text = PopUpString;
        }

        /// <summary>
        /// Hides buttons on the pop up message
        /// </summary>
        public void DisableButtons()
        {
            btnAccept.Hide();
            btnDecline.Hide();
        }

        /// <summary>
        /// Sets the Dialog Result to OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Sets the Dialog Result to Cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecline_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
