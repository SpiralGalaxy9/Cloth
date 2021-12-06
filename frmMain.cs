using System;

using System.Windows.Forms;

namespace Cloth
{
    public partial class frmMain : Form
    {
        // application form
        Form frm = null;
    
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
            if (!CheckForDuplicate(frm))
            {
                frm = new frmLogin();
                frm.MdiParent = this;
                frm.Show();
               
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

        private void tsBtnManufacturers_Click(object sender, EventArgs e)
        {
            // runs only one instance of form
            if (!CheckForDuplicate(frm))
            {
                frm  = new frmManufactures();
                frm.MdiParent = frmMain.ActiveForm;
                frm.Show();
            }
        }

        private void tsBtnColors_Click(object sender, EventArgs e)
        {
            // runs only one instance of form
            if (!CheckForDuplicate(frm))
            {
                frm = new frmColor();
                frm.MdiParent = this;
                frm.Show();

            }

        }

        private void tsBtnSizes_Click(object sender, EventArgs e)
        {
            // runs only one instance of form
            if (!CheckForDuplicate(frm))
            {
                frm = new frmSize();
                frm.MdiParent = this;
                frm.Show();

            }
        }

        private void tsBtnAgeGroup_Click(object sender, EventArgs e)
        {
            if (!CheckForDuplicate(frm))
            {
                frm = new frmAgeGroup();
                frm.MdiParent = this;
                frm.Show();

            }
        }
    }
}
