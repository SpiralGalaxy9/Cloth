using System;

using System.Windows.Forms;

namespace Cloth
{
    public partial class frmMain : Form
    {
        // application forms
        private frmLogin fl = null;
        private frmManufactures fm = null;

        public frmMain()
        {
            InitializeComponent();

        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void TsBtnLogin_Click(object sender, EventArgs e)
        {
            // runs only one instance of form
            if (!CheckForDuplicate(fl))
            {
                fl = new frmLogin();
                fl.MdiParent = this;
                fl.Show();
               
            }
        }

        private bool CheckForDuplicate(Form newForm)
        {
            bool bValue = false;
            foreach (Form fm in this.MdiChildren)
            {
                if (fm.GetType() == newForm.GetType())
                {
                    fm.Activate();
                    bValue = true;
                }
            }
            return bValue;
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnFormManufacturer_Click(object sender, EventArgs e)
        {
            // runs only one instance of form
            if (!CheckForDuplicate(fm))
            {
                fm = new frmManufactures();
                fm.MdiParent = frmMain.ActiveForm;
                fm.Show();
            }
        }
    }
}
