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
    public partial class frmEmployeeType : Form
    {

        //DataSset Container
        DataSet dsEmployeeType = new DataSet();

        // class object of database funcions 
        ManageDB managerDB = new ManageDB();
        public frmEmployeeType()
        {
            InitializeComponent();
        }

        private void FrmEmployeeType_Load(object sender, EventArgs e)
        {
            Grid_init();

            dsEmployeeType.Clear();
            dsEmployeeType = managerDB.GetEmployeeType();
            FillGrid(dsEmployeeType);

            CleanForm();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int EmployeeTypeID;
            string EmployeeType;
            if (ConfirmConcestency() == true)
            {
                EmployeeType = tbEmployeeType.Text.Trim();

                if (tbEmployeeTypeID.TextLength == 0)
                {
                    try
                    {
                        int lid = managerDB.AddEmployeeType(EmployeeType);
                        if (lid > 0)
                        {

                            MessageBox.Show("New EmployeeType is Added at " + lid.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Can't Add, as EmployeeType with Same CODE or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbEmployeeType.Focus();
                    }
                }
                else
                {
                    try
                    {
                        EmployeeTypeID = int.Parse(tbEmployeeTypeID.Text.Trim());
                        managerDB.UpdateEmployeeType(EmployeeTypeID, EmployeeType);
                        CleanForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Modify, as EmployeeType with Same ID or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbEmployeeType.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Correct Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Grid_init()
        {
            dgEmployeeType.ColumnCount = 2;
            dgEmployeeType.Columns[0].HeaderText = "EmployeeType ID";
            dgEmployeeType.Columns[1].HeaderText = "EmployeeType";


            for (int i = 0; i < dgEmployeeType.Columns.Count; i++)
            {
                dgEmployeeType.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgEmployeeType.AutoGenerateColumns = false;

            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgEmployeeType.Columns.Add(Editlink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgEmployeeType.Columns.Add(Deletelink);

            dgEmployeeType.Columns[0].Visible = false;

        }

        private void FillGrid(DataSet ds)
        {
            dgEmployeeType.Rows.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataGridViewRow row = dgEmployeeType.Rows[dgEmployeeType.Rows.Add()];
                row.Cells[0].Value = ds.Tables[0].Rows[i]["EmployeeTypeID"].ToString().Trim();
                row.Cells[1].Value = ds.Tables[0].Rows[i]["EmployeeType"].ToString().Trim();

            }
            dgEmployeeType.Refresh();
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

            dsEmployeeType.Clear();
            dsEmployeeType = managerDB.GetEmployeeType();

            FillGrid(dsEmployeeType);

            tbEmployeeType.Focus();
        }

        private bool ConfirmConcestency()
        {
            if (String.IsNullOrEmpty(tbEmployeeType.Text))
            {
                MessageBox.Show("EmployeeType can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dsEmployeeType.Clear();
            dsEmployeeType = managerDB.SearchEmployeeType(tbEmployeeType.Text.Trim());

            if (dsEmployeeType.Tables["EmployeeType"].Rows.Count == 0)
            {
                MessageBox.Show("No Record found with the given EmployeeType", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbEmployeeType.Focus();
                return;
            }
            else
            {
                FillGrid(dsEmployeeType);
            }
        }

        private void DgEmployeeType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(string.Format("Edit -- CellClick row:{0}, column:{1}", e.RowIndex, e.ColumnIndex));
            if (e.ColumnIndex == 2) //Edit link 
            {
                DataGridViewRow row = dgEmployeeType.Rows[e.RowIndex];

                tbEmployeeTypeID.Text = row.Cells[0].Value.ToString();
                tbEmployeeType.Text = row.Cells[1].Value.ToString();

            }
            else if (e.ColumnIndex == 3) //delete link 
            {
                DataGridViewRow row = dgEmployeeType.Rows[e.RowIndex];
                if (row.Cells[3].Value != null)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        managerDB.DeleteEmployeeType(int.Parse(row.Cells[0].Value.ToString()));
                    }
                }
                CleanForm();
            }
        }
    }
}
