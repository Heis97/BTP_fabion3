using Active_Directory_Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTP
{
    public partial class SelectDriver_frm : Form, ILanguageChangable
    {
        public SelectDriver_frm()
        {
            InitializeComponent();
        }

        private void SelectDriver_frm_Load(object sender, EventArgs e)
        {
            //SimulatorRadioBtn.Checked = true;
           // ControllerRadioBtn.Checked = false;
           // IPMaskedText.Enabled = false;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
           /* if (SimulatorRadioBtn.Checked == true)
            {
                ConnectionData.ControllerIP = "localhost";
            }
            else if (ControllerRadioBtn.Checked == true)
            {
                ConnectionData.ControllerIP = IPMaskedText.ToString();
            }*/

            

            Main_frm Mform = this.Owner as Main_frm;  //создаём ссылку на главную форму
            if (Mform != null)                        //проверяем, если преобразование удалось, переменная form1 получит ссылку, если нет, то будет null
            {
                this.Hide();
                Mform.SetCommunication();              //если ссылка получена, можно приступать к работе с ней

            }

        }
        public string getPort()
        {
            return tb_comport.Text;
        }
        private void ControllerRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            SimulatorRadioBtn.Checked = false;
            IPMaskedText.Enabled = true;
        }

        private void SimulatorRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            ControllerRadioBtn.Checked = false;
            IPMaskedText.Enabled = false;
        }

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(SelectDriver_frm));
            CultureInfo newCultureInfo = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(newLocalization));

            foreach (Control C in Controls)
            {
                resources.ApplyResources(C, C.Name, newCultureInfo);
                if (C is GroupBox)
                {
                    foreach (Control G in C.Controls)
                    {
                        resources.ApplyResources(G, G.Name, newCultureInfo);
                    }
                }
            }
        }
    }
}
