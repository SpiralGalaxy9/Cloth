using System;

using System.Windows.Forms;

namespace Cloth
{
    public partial class frmMain : Form
    {
        // login form object
        Form loginForm;
        public frmMain()
        {
            InitializeComponent();

        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void TsBtnLogin_Click(object sender, EventArgs e)
        {
            // if already open then activate 
            if (loginForm != null)
            {
                loginForm.Activate();
            }
            else
            {
                // otherwise create new instance
                loginForm = new Form();
                loginForm.MdiParent = this;
                loginForm.MaximizeBox = false;
                loginForm.FormBorderStyle = FormBorderStyle.Fixed3D;
                loginForm.FormClosed += LoginForm_FormClosed;
                loginForm.Show();

            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm = null;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
