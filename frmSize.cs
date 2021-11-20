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
    public partial class frmSize : Form
    {
        //DataSset Container
        DataSet dsSizes = new DataSet();

        // class object of database funcions 
        ManageDB managerDB = new ManageDB();
        public frmSize()
        {
            InitializeComponent();
        }

        private void frmSize_Load(object sender, EventArgs e)
        {
            Grid_init();

            dsSizes.Clear();
            dsSizes = managerDB.GetSizes();
            FillGrid(dsSizes);

             CleanForm();
        }
        private void Grid_init()
        {
            dgSize.ColumnCount = 2;
            dgSize.Columns[0].HeaderText = "Size ID";
            dgSize.Columns[1].HeaderText = "Size";
            

            for (int i = 0; i < dgSize.Columns.Count; i++)
            {
                dgSize.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgSize.AutoGenerateColumns = false;

            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgSize.Columns.Add(Editlink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgSize.Columns.Add(Deletelink);

            dgSize.Columns[0].Visible = false;

        }
        private void FillGrid(DataSet ds)
        {
            dgSize.Rows.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataGridViewRow row = dgSize.Rows[dgSize.Rows.Add()];
                row.Cells[0].Value = ds.Tables[0].Rows[i]["SizeID"].ToString().Trim();
                row.Cells[1].Value = ds.Tables[0].Rows[i]["Size"].ToString().Trim();
                
            }
            dgSize.Refresh();
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

            dsSizes.Clear();
            dsSizes = managerDB.GetSizes();

            FillGrid(dsSizes);

            tbSize.Focus();
        }
        private bool ConfirmConcestency()
        {
            if (String.IsNullOrEmpty(tbSize.Text))
            {
                MessageBox.Show("Size can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            int sizeID;
            string size;
            if (ConfirmConcestency() == true)
            {
                size = tbSize.Text.Trim();
                
                if (tbSizeID.TextLength == 0)
                {
                    try
                    {
                        int lid = managerDB.AddSize(size);
                        if (lid > 0)
                        {

                            MessageBox.Show("New Size is Added at " + lid.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Not Added, Contact with your Administrator --- " + lid.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        CleanForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Add, as Size with Same CODE or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbSize.Focus();
                    }
                }
                else
                {
                    try
                    {
                        sizeID = int.Parse(tbSizeID.Text.Trim());
                        managerDB.UpdateSize(sizeID, size);
                        CleanForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Modify, as Size with Same ID or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbSize.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Correct Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dsSizes.Clear();
            dsSizes = managerDB.SearchSize(tbSize.Text.Trim());

            if (dsSizes.Tables["Sizes"].Rows.Count == 0)
            {
                MessageBox.Show("No Record found with the given Size", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbSize.Focus();
                return;
            }
            else
            {
                FillGrid(dsSizes);
            }
        }

        private void dgSize_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(string.Format("Edit -- CellClick row:{0}, column:{1}", e.RowIndex, e.ColumnIndex));
            if (e.ColumnIndex == 2) //Edit link 
            {
                DataGridViewRow row = dgSize.Rows[e.RowIndex];

                tbSizeID.Text = row.Cells[0].Value.ToString();
                tbSize.Text = row.Cells[1].Value.ToString();
               
            }
            else if (e.ColumnIndex == 3) //delete link 
            {
                DataGridViewRow row = dgSize.Rows[e.RowIndex];
                if (row.Cells[3].Value != null)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        managerDB.DeleteSize(int.Parse(row.Cells[0].Value.ToString()));
                    }
                }
                CleanForm();
            }
        }
    }
}
