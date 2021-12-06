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
    public partial class frmAgeGroup : Form
    {

        //DataSset Container
        DataSet dsAgeGroup = new DataSet();

        // class object of database funcions 
        ManageDB managerDB = new ManageDB();
        public frmAgeGroup()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int AgeGroupID;
            string AgeGroup;
            if (ConfirmConcestency() == true)
            {
                AgeGroup = tbAgeGroup.Text.Trim();

                if (tbAgeGroupID.TextLength == 0)
                {
                    try
                    {
                        int lid = managerDB.AddAgeGroup(AgeGroup);
                        if (lid > 0)
                        {

                            MessageBox.Show("New AgeGroup is Added at " + lid.ToString(), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Can't Add, as AgeGroup with Same CODE or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbAgeGroup.Focus();
                    }
                }
                else
                {
                    try
                    {
                        AgeGroupID = int.Parse(tbAgeGroupID.Text.Trim());
                        managerDB.UpdateAgeGroup(AgeGroupID, AgeGroup);
                        CleanForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Can't Modify, as AgeGroup with Same ID or NAME is already exits, please choose different", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbAgeGroup.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Correct Values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmAgeGroup_Load(object sender, EventArgs e)
        {
            Grid_init();

            dsAgeGroup.Clear();
            dsAgeGroup = managerDB.GetAgeGroup();
            FillGrid(dsAgeGroup);

            CleanForm();
        }
        private void Grid_init()
        {
            dgAgeGroup.ColumnCount = 2;
            dgAgeGroup.Columns[0].HeaderText = "AgeGroup ID";
            dgAgeGroup.Columns[1].HeaderText = "AgeGroup";


            for (int i = 0; i < dgAgeGroup.Columns.Count; i++)
            {
                dgAgeGroup.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgAgeGroup.AutoGenerateColumns = false;

            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgAgeGroup.Columns.Add(Editlink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgAgeGroup.Columns.Add(Deletelink);

            dgAgeGroup.Columns[0].Visible = false;

        }

        private void FillGrid(DataSet ds)
        {
            dgAgeGroup.Rows.Clear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataGridViewRow row = dgAgeGroup.Rows[dgAgeGroup.Rows.Add()];
                row.Cells[0].Value = ds.Tables[0].Rows[i]["AgeGroupID"].ToString().Trim();
                row.Cells[1].Value = ds.Tables[0].Rows[i]["AgeGroup"].ToString().Trim();

            }
            dgAgeGroup.Refresh();
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

            dsAgeGroup.Clear();
            dsAgeGroup = managerDB.GetAgeGroup();

            FillGrid(dsAgeGroup);

            tbAgeGroup.Focus();
        }


        private bool ConfirmConcestency()
        {
            if (String.IsNullOrEmpty(tbAgeGroup.Text))
            {
                MessageBox.Show("AgeGroup can not be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dsAgeGroup.Clear();
            dsAgeGroup = managerDB.SearchAgeGroup(tbAgeGroup.Text.Trim());

            if (dsAgeGroup.Tables["AgeGroup"].Rows.Count == 0)
            {
                MessageBox.Show("No Record found with the given AgeGroup", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbAgeGroup.Focus();
                return;
            }
            else
            {
                FillGrid(dsAgeGroup);
            }
        }

        private void DgAgeGroup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(string.Format("Edit -- CellClick row:{0}, column:{1}", e.RowIndex, e.ColumnIndex));
            if (e.ColumnIndex == 2) //Edit link 
            {
                DataGridViewRow row = dgAgeGroup.Rows[e.RowIndex];

                tbAgeGroupID.Text = row.Cells[0].Value.ToString();
                tbAgeGroup.Text = row.Cells[1].Value.ToString();

            }
            else if (e.ColumnIndex == 3) //delete link 
            {
                DataGridViewRow row = dgAgeGroup.Rows[e.RowIndex];
                if (row.Cells[3].Value != null)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        managerDB.DeleteAgeGroup(int.Parse(row.Cells[0].Value.ToString()));
                    }
                }
                CleanForm();
            }
        }
    }
}
