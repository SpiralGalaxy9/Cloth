using System;

using System.Windows.Forms;

namespace Cloth
{
    public partial class frmMain : Form
    {
        
        public frmMain()
        {
            InitializeComponent();

        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            
        }

        private void TsBtnLogin_Click(object sender, EventArgs e)
        {
            

            frmLogin fl = new frmLogin();
            fl.MdiParent = frmMain.ActiveForm;
            fl.Show();            
        }

        
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
