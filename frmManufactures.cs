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
    public partial class frmManufactures : Form
    {
        //DataSset Container
        DataSet dsManufacturers = new DataSet();

        // class object of database funcions 
        ManageDB managerDB = new ManageDB();

        public frmManufactures()
        {
            InitializeComponent();
        }

        private void FrmManufactures_Load(object sender, EventArgs e)
        {
            Grid_init();

            dsManufacturers.Clear();
            dsManufacturers = managerDB.GetManufacturersDetails();

            FillGrid(dsManufacturers);

            CleanForm();
        }



        private void Grid_init()
        {
            dgManufactureres.ColumnCount = 9;

            dgManufactureres.Columns[0].HeaderText = "MID";
            dgManufactureres.Columns[1].HeaderText = "Code";
            dgManufactureres.Columns[2].HeaderText = "Name";

            dgManufactureres.Columns[3].HeaderText = "City";
            dgManufactureres.Columns[4].HeaderText = "Location";
            dgManufactureres.Columns[5].HeaderText = "Contact Person";
            dgManufactureres.Columns[6].HeaderText = "Cell No.";
            dgManufactureres.Columns[7].HeaderText = "Phone No.";
            dgManufactureres.Columns[8].HeaderText = "Description";

            for (int i = 0; i < dgManufactureres.Columns.Count; i++)
            {
                dgManufactureres.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgManufactureres.AutoGenerateColumns = false;
            
            DataGridViewLinkColumn Editlink = new DataGridViewLinkColumn();
            Editlink.UseColumnTextForLinkValue = true;
            Editlink.HeaderText = "Edit";
            Editlink.DataPropertyName = "lnkColumn";
            Editlink.LinkBehavior = LinkBehavior.SystemDefault;
            Editlink.Text = "Edit";
            dgManufactureres.Columns.Add(Editlink);

            DataGridViewLinkColumn Deletelink = new DataGridViewLinkColumn();
            Deletelink.UseColumnTextForLinkValue = true;
            Deletelink.HeaderText = "Delete";
            Deletelink.DataPropertyName = "lnkColumn";
            Deletelink.LinkBehavior = LinkBehavior.SystemDefault;
            Deletelink.Text = "Delete";
            dgManufactureres.Columns.Add(Deletelink);

            dgManufactureres.Columns[0].Visible = false;
            dgManufactureres.Columns[4].Visible = false;
            dgManufactureres.Columns[8].Visible = false;
        }

        private void FillGrid(DataSet ds)
        {
            dgManufactureres.Rows.Clear();           

            for (int i = 0; i < ds.Tables["ManufactureresDetails"].Rows.Count; i++)
            {
                DataGridViewRow row = dgManufactureres.Rows[dgManufactureres.Rows.Add()];

                row.Cells[0].Value = ds.Tables["ManufactureresDetails"].Rows[i]["MID"].ToString().Trim();
                row.Cells[1].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerCode"].ToString().Trim();
                row.Cells[2].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerName"].ToString().Trim();

                row.Cells[3].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerCity"].ToString().Trim();
                row.Cells[4].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerLocation"].ToString().Trim();

                row.Cells[5].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerContactPerson"].ToString().Trim();
                row.Cells[6].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerCell"].ToString().Trim();
                row.Cells[7].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerPhone"].ToString().Trim();

                row.Cells[8].Value = ds.Tables["ManufactureresDetails"].Rows[i]["ManufacturerDescription"].ToString().Trim();                
            }
            dgManufactureres.Refresh();
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

            dsManufacturers.Clear();
            dsManufacturers = managerDB.GetManufacturersDetails();

            FillGrid(dsManufacturers);

            txtCode.Focus();
        }

        private bool ConfirmConcestency()
        {
            if (txtCode.TextLength != 2)
            {
                MessageBox.Show("Code should be of Two Characters");
                return false;
            }
            else if (txtName.TextLength == 0)
            {
                MessageBox.Show("Name should not be empty");
                return false;
            }           
            else
                return true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int mid;
            string code, name, city, location, contactPerson, cell, phone, description;

            if (ConfirmConcestency() == true)
            {
                code = txtCode.Text.Trim();
                name = txtName.Text.Trim();
                city = txtCity.Text.Trim();
                location= txtLocation.Text.Trim();
                contactPerson = txtContactPerson.Text.Trim();
                cell = txtCell.Text.Trim();
                phone = txtPhone.Text.Trim();
                description = txtDescription.Text.Trim();               

                if (txtManufacturerID.TextLength == 0)
                {                     
                    try
                    {
                        int lid = managerDB.AddManufacturerDetails(code, name, city, location, contactPerson, cell, phone, description);
                        if (lid > 0)
                        {
                            MessageBox.Show("New Manufacturer is Added at " + lid.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Not Added, Contact with your Administrator --- " + lid.ToString());
                        }
                        CleanForm();
                    }
                    catch
                    {
                        MessageBox.Show("Can't Add, as Manaufacturer with Same CODE or NAME is already exits, please choose different");
                        txtCode.Focus();
                    }
                }
                else
                {
                    try
                    {
                        mid = Int32.Parse(txtManufacturerID.Text.Trim());
                        managerDB.UpdateManufacturerDetails(mid, code, name, city, location, contactPerson, cell, phone, description);
                        CleanForm();
                    }
                    catch
                    {
                        MessageBox.Show("Can't Modify, as Manaufacturer with Same CODE or NAME is already exits, please choose different");
                        txtCode.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Enter Correct Values");
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is frmManufactures)
                {
                    f.Close();
                    break;
                }
            }
        }

        private void DgManufactureres_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(string.Format("Edit -- CellClick row:{0}, column:{1}", e.RowIndex, e.ColumnIndex));

            if (e.ColumnIndex == 9) //Edit link 
            {
                DataGridViewRow row = dgManufactureres.Rows[e.RowIndex];

                txtManufacturerID.Text = row.Cells[0].Value.ToString();
                txtCode.Text = row.Cells[1].Value.ToString();
                txtName.Text = row.Cells[2].Value.ToString();

                txtCity.Text = row.Cells[3].Value.ToString();   
                txtLocation.Text = row.Cells[4].Value.ToString().Trim();

                txtContactPerson.Text = row.Cells[5].Value.ToString().Trim();
                txtCell.Text = row.Cells[6].Value.ToString().Trim();
                txtPhone.Text = row.Cells[7].Value.ToString().Trim();

                txtDescription.Text = row.Cells[8].Value.ToString().Trim();
            }
            else if (e.ColumnIndex == 10) //delete link 
            {
                DataGridViewRow row = dgManufactureres.Rows[e.RowIndex];
                if (row.Cells[10].Value != null)
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete ?", "ALERT", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        managerDB.DeleteManufacturersDetails(Int32.Parse(row.Cells[0].Value.ToString()));
                    }
                }
                CleanForm();
            }            
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dsManufacturers.Clear();
            dsManufacturers = managerDB.SearchManufactureresDetails(txtName.Text.Trim());

            //MessageBox.Show(txtName.Text.Trim() + "have " + dsManufacturers.Tables["ManufactureresDetails"].Rows.Count +"records");

            if (dsManufacturers.Tables["ManufactureresDetails"].Rows.Count == 0)
            {
                MessageBox.Show("No Record found with the given Name of Manufacturer");
                txtName.Focus();
                return;
            }
            else
            {
                FillGrid(dsManufacturers);
            }
        }        
    }
}
