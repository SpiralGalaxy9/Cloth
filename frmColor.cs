using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloth
{
    public partial class frmColor : Form
    {
        //DataSset Container
        DataSet dsColors = new DataSet();

        // class object of database funcions 
        ManageDB managerDB = new ManageDB();
        public frmColor()
        {
            InitializeComponent();
        }

        private void frmColor_Load(object sender, EventArgs e)
        {
            Grid_init();

            dsColors.Clear();
            dsColors = managerDB.GetColors();

            FillGrid(dsColors);

            CleanForm();
        }
        private void Grid_init()
        {
           dgColors.ColumnCount = 3;
            dgColors.Columns[0].HeaderText = "Color ID";
            dgColors.Columns[1].HeaderText = "Color Code";
            dgColors.Columns[2].HeaderText = "Color Name";
            

            for (int i = 0; i < dgColors.Columns.Count; i++)
            {
                dgColors.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgColors.AutoGenerateColumns = false;

            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgColors.Columns.Add(Editlink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgColors.Columns.Add(Deletelink);
            dgColors.Columns[0].Visible = false;

        }
        private void FillGrid(DataSet ds)
        {
            dgColors.Rows.Clear();

            for (int i = 0; i < ds.Tables["Colors"].Rows.Count; i++)
            {
                DataGridViewRow row = dgColors.Rows[dgColors.Rows.Add()];
                row.Cells[0].Value = ds.Tables["Colors"].Rows[i]["ColorID"].ToString().Trim();
                row.Cells[1].Value = ds.Tables["Colors"].Rows[i]["ColorCode"].ToString().Trim();
                row.Cells[2].Value = ds.Tables["Colors"].Rows[i]["ColorName"].ToString().Trim();
                
            }
            dgColors.Refresh();
        }
        private void CleanForm()
        {
            foreach (var c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }

            dsColors.Clear();
            dsColors = managerDB.GetColors();

            FillGrid(dsColors);

            tbColorName.Focus();
        }

        private bool ConfirmConcestency()
        {
            if(String.IsNullOrEmpty(tbColorName.Text) || String.IsNullOrEmpty(tbColorCode.Text))
            {
                MessageBox.Show("Color Name can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int colorID;
            string colorName, colorCode;
            if(ConfirmConcestency()==true)
            {
                colorCode = tbColorCode.Text.Trim();
                colorName = tbColorName.Text.Trim();
                if(tbColorID.TextLength == 0)
                {
                    try
                    {
                        int lid = managerDB.AddColor(colorCode, colorName);
                        if (lid > 0)
                        {

                            MessageBox.Show("New Color is Added at " + lid.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Not Added, Contact with your Administrator --- " + lid.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        CleanForm();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Add, as color with Same CODE or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbColorName.Focus();
                    }
                }
                else
                {
                    try
                    {
                        colorID = int.Parse(tbColorID.Text.Trim());
                        managerDB.UpdateColor(colorID,colorCode, colorName);
                        CleanForm();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Modify, as color with Same ID or NAME is already exits, please choose different","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        tbColorName.Focus();
                    }
                }
            }else
            {
                MessageBox.Show("Enter Correct Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dsColors.Clear();
            dsColors = managerDB.SearchColor(tbColorName.Text.Trim());

            if (dsColors.Tables["Colors"].Rows.Count == 0)
            {
                MessageBox.Show("No Record found with the given Name of Manufacturer", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbColorName.Focus();
                return;
            }
            else
            {
                FillGrid(dsColors);
            }
        }

        private void dgColors_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(string.Format("Edit -- CellClick row:{0}, column:{1}", e.RowIndex, e.ColumnIndex));
            if (e.ColumnIndex == 3) //Edit link 
            {
                DataGridViewRow row = dgColors.Rows[e.RowIndex];

                tbColorID.Text = row.Cells[0].Value.ToString();
                tbColorCode.Text = row.Cells[1].Value.ToString();
                tbColorName.Text = row.Cells[2].Value.ToString();

            }
            else if (e.ColumnIndex == 4) //delete link 
            {
                DataGridViewRow row = dgColors.Rows[e.RowIndex];
                if (row.Cells[4].Value != null)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        managerDB.DeleteColor(int.Parse(tbColorID.Text));
                    }
                }
                CleanForm();
            }
        }
    }
}
