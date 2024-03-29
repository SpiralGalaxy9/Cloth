﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
//using System.Data.SqlClient;

using MySql.Data;
using MySql.Data.MySqlClient;


namespace Cloth
{
    class ManageDB
    {
        //local Connection String for Sanaullah
        static string conString = @"server=localhost; database=cloth; user id=root";

        //local Connection String for Babar
        //conString = @"Data Source = CVALLEY\SQLEXPRESS; Initial Catalog =ML_IMS ; Integrated Security = True";

        //local Connection String for Kashif
        //conString = @"Data Source = CVALLEY\SQLEXPRESS; Initial Catalog =ML_IMS ; Integrated Security = True";

        //Online/Networking Connection String for SQL ---- need to set according to MySQL format
        //static string conString = @"Server=192.168.18.2,1433;Network Library=DBMSSOCN;Initial Catalog=SAMILMS;User ID=operator;Password=operator786@";
        
        //MySql Connection 
        MySqlConnection con = new MySqlConnection(conString);

        //MySql command 
        MySqlCommand sqlComObj;

        // MySQL Reader 
        // MySqlDataReader sqlDataRdrObj;

        // MySQL Data Adapter
        MySqlDataAdapter sqlDataAdptObj = new MySqlDataAdapter();


        // *****************************  Manufacturer Related Queries   *********************************
        //  Get, Search, Add, Update, Delete
        
        public DataSet GetManufacturersDetails()
        {
            DataSet datasetManufacturers = new DataSet();

            string query = "SELECT * FROM Manufacturers order by ManufacturerName";

            sqlComObj = new MySqlCommand(query, con); // sql command           
            sqlDataAdptObj.SelectCommand = sqlComObj;
            datasetManufacturers.Clear();
            sqlDataAdptObj.Fill(datasetManufacturers, "ManufactureresDetails"); // fill dataset

            return datasetManufacturers;
        }

        public DataSet SearchManufactureresDetails(string SearchValue)
        {
            DataSet datasetManufacturers = new DataSet();

            string query = "SELECT * FROM Manufacturers ";
            query += " where ManufacturerName Like @SearchValue Order By  ManufacturerName";


            sqlComObj = new MySqlCommand(query, con); // sql command      
            sqlComObj.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
            sqlDataAdptObj.SelectCommand = sqlComObj;
            datasetManufacturers.Clear();
            sqlDataAdptObj.Fill(datasetManufacturers, "ManufactureresDetails"); // fill dataset

            return datasetManufacturers;
        }
        
        public int AddManufacturerDetails(string code, string name, string city, string location, string contactPerson, string cell, string phone, string description)
        {
            con.Open();
            // sql command to add ManufacturerDetails
            string query = "insert into Manufacturers(ManufacturerCode, ManufacturerName, ManufacturerCity, ManufacturerLocation, ManufacturerContactPerson, ManufacturerCell, ManufacturerPhone, ManufacturerDescription)";
            query += " values(@ManufacturerCode, @ManufacturerName, @ManufacturerCity, @ManufacturerLocation ,@ManufacturerContactPerson, @ManufacturerCell, @ManufacturerPhone, @ManufacturerDescription)";
            using (sqlComObj = new MySqlCommand(query, con))
            {
                sqlComObj.Parameters.AddWithValue("@ManufacturerCode", code);
                sqlComObj.Parameters.AddWithValue("@ManufacturerName", name);
                sqlComObj.Parameters.AddWithValue("@ManufacturerCity", city);
                sqlComObj.Parameters.AddWithValue("@ManufacturerLocation", location);
                sqlComObj.Parameters.AddWithValue("@ManufacturerContactPerson", contactPerson);
                sqlComObj.Parameters.AddWithValue("@ManufacturerCell", cell);
                sqlComObj.Parameters.AddWithValue("@ManufacturerPhone", phone);
                sqlComObj.Parameters.AddWithValue("@ManufacturerDescription", description);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
            //getting the last inserted record, MID
            int lid = Int32.Parse(sqlComObj.LastInsertedId.ToString());
            return lid;
        }

        public void UpdateManufacturerDetails(int mid, string code, string name, string city, string location, string contactPerson, string cell, string phone, string description)
        {

            con.Open();
            string query = "update Manufacturers set ManufacturerCode=@ManufacturerCode, ManufacturerName=@ManufacturerName, ManufacturerCity=@ManufacturerCity, ManufacturerLocation=@ManufacturerLocation,";
            query += " ManufacturerContactPerson =@ManufacturerContactPerson, ManufacturerCell=@ManufacturerCell, ManufacturerPhone=@ManufacturerPhone, ManufacturerDescription=@ManufacturerDescription where MID=@MID";

            using (sqlComObj = new MySqlCommand(query , con))
            {
                // adding parameters
                sqlComObj.Parameters.AddWithValue("@MID", mid);
                sqlComObj.Parameters.AddWithValue("@ManufacturerCode", code);
                sqlComObj.Parameters.AddWithValue("@ManufacturerName", name);
                sqlComObj.Parameters.AddWithValue("@ManufacturerCity", city);
                sqlComObj.Parameters.AddWithValue("@ManufacturerLocation", location);
                sqlComObj.Parameters.AddWithValue("@ManufacturerContactPerson", contactPerson);
                sqlComObj.Parameters.AddWithValue("@ManufacturerCell", cell);
                sqlComObj.Parameters.AddWithValue("@ManufacturerPhone", phone);
                sqlComObj.Parameters.AddWithValue("@ManufacturerDescription", description);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        public void DeleteManufacturersDetails(int mid)
        {
            con.Open();
            string query = "delete from Manufacturers where MID=@MID";


            using (sqlComObj = new MySqlCommand(query, con))
            {
                //adding parameters
                sqlComObj.Parameters.AddWithValue("@MID", mid);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        // *****************************  Colors Form  Related Queries   *********************************
        //  Get, Search, Add, Update, Delete

        public DataSet GetColors()
        {
            DataSet dsColors = new DataSet();

            string query = "SELECT * FROM Color order by ColorName";

            sqlComObj = new MySqlCommand(query, con); // sql command           
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsColors.Clear();
            sqlDataAdptObj.Fill(dsColors, "Colors"); // fill dataset

            return dsColors;
        }
        public DataSet SearchColor(string SearchValue)
        {
            DataSet dsColors = new DataSet();

            string query = "SELECT * FROM Color ";
            query += " where ColorName Like @SearchValue Order By  ColorName";


            sqlComObj = new MySqlCommand(query, con); // sql command      
            sqlComObj.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsColors.Clear();
            sqlDataAdptObj.Fill(dsColors, "Colors"); // fill dataset

            return dsColors;
        }
        public int AddColor(string colorCode, string colorName)
        {
            con.Open();
            // sql command to add color
            string query = "insert into Color(ColorCode, ColorName)";
            query += " values(@ColorCode, @ColorName)";
            using (sqlComObj = new MySqlCommand(query, con))
            {
                sqlComObj.Parameters.AddWithValue("@ColorCode", colorCode);
                sqlComObj.Parameters.AddWithValue("@ColorName",colorName);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
            //getting the last inserted record, ColorID
            int lid = Int32.Parse(sqlComObj.LastInsertedId.ToString());
            return lid;
        }

        public void UpdateColor(int colorId,string colorCode, string colorName)
        {

            con.Open();
            string query = "update Color set ColorCode=@ColorCode, ColorName=@ColorName ";
            query += "where ColorID=@ColorID";

            using (sqlComObj = new MySqlCommand(query, con))
            {
                // adding parameters
                sqlComObj.Parameters.AddWithValue("@ColorID", colorId);
                sqlComObj.Parameters.AddWithValue("@ColorCode", colorCode);
                sqlComObj.Parameters.AddWithValue("@ColorName", colorName);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        public void DeleteColor(int colorID)
        {
            con.Open();
            string query = "delete from Color where ColorID=@ColorID";


            using (sqlComObj = new MySqlCommand(query, con))
            {
                //adding parameters
                sqlComObj.Parameters.AddWithValue("@ColorID", colorID);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }




        // *****************************  Sizes Form  Related Queries   *********************************
        //  Get, Search, Add, Update, Delete

        public DataSet GetSizes()
        {
            DataSet dsSizes = new DataSet();

            string query = "SELECT * FROM Size";

            sqlComObj = new MySqlCommand(query, con); // sql command           
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsSizes.Clear();
            sqlDataAdptObj.Fill(dsSizes, "Sizes"); // fill dataset
            return dsSizes;
        }
        public DataSet SearchSize(string SearchValue)
        {
            DataSet dsSizes = new DataSet();

            string query = "SELECT * FROM Size ";
            query += " where Size Like @SearchValue";


            sqlComObj = new MySqlCommand(query, con); // sql command      
            sqlComObj.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsSizes.Clear();
            sqlDataAdptObj.Fill(dsSizes, "Sizes"); // fill dataset

            return dsSizes;
        }
        public int AddSize(string Size)
        {
            con.Open();
            // sql command to add size
            string query = "insert into Size(Size)";
            query += " values(@Size)";
            using (sqlComObj = new MySqlCommand(query, con))
            {
               
                sqlComObj.Parameters.AddWithValue("@Size", Size);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
            //getting the last inserted record, ColorID
            int lid = Int32.Parse(sqlComObj.LastInsertedId.ToString());
            return lid;
        }

        public void UpdateSize(int SizeId, string Size)
        {

            con.Open();
            string query = "update Size set Size=@Size ";
            query += "where SizeID=@SizeID";

            using (sqlComObj = new MySqlCommand(query, con))
            {
                // adding parameters
                sqlComObj.Parameters.AddWithValue("@Size", Size);
                sqlComObj.Parameters.AddWithValue("@SizeID", SizeId);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        public void DeleteSize(int SizeId)
        {
            con.Open();
            string query = "delete from Size where SizeID=@SizeID";


            using (sqlComObj = new MySqlCommand(query, con))
            {
                //adding parameters
                sqlComObj.Parameters.AddWithValue("@SizeID", SizeId);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }




        // *****************************  Age Group Form  Related Queries   *********************************
        //  Get, Search, Add, Update, Delete

        public DataSet GetAgeGroup()
        {
            DataSet dsAgeGroup = new DataSet();

            string query = "SELECT * FROM AgeGroup";

            sqlComObj = new MySqlCommand(query, con); // sql command           
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsAgeGroup.Clear();
            sqlDataAdptObj.Fill(dsAgeGroup, "AgeGroup"); // fill dataset
            return dsAgeGroup;
        }
        public DataSet SearchAgeGroup(string SearchValue)
        {
            DataSet dsAgeGroup = new DataSet();

            string query = "SELECT * FROM AgeGroup ";
            query += " where AgeGroup Like @SearchValue";


            sqlComObj = new MySqlCommand(query, con); // sql command      
            sqlComObj.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsAgeGroup.Clear();
            sqlDataAdptObj.Fill(dsAgeGroup, "AgeGroup"); // fill dataset

            return dsAgeGroup;
        }
        public int AddAgeGroup(string AgeGroup)
        {
            con.Open();
            // sql command to add size
            string query = "insert into AgeGroup(AgeGroup)";
            query += " values(@AgeGroup)";
            using (sqlComObj = new MySqlCommand(query, con))
            {

                sqlComObj.Parameters.AddWithValue("@AgeGroup", AgeGroup);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
            //getting the last inserted record, ColorID
            int lid = Int32.Parse(sqlComObj.LastInsertedId.ToString());
            return lid;
        }

        public void UpdateAgeGroup(int AgeGroupID, string AgeGroup)
        {

            con.Open();
            string query = "update AgeGroup set AgeGroup=@AgeGroup ";
            query += "where AgeGroupID=@AgeGroupID";

            using (sqlComObj = new MySqlCommand(query, con))
            {
                // adding parameters
                sqlComObj.Parameters.AddWithValue("@AgeGroup", AgeGroup);
                sqlComObj.Parameters.AddWithValue("@AgeGroupID", AgeGroupID);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        public void DeleteAgeGroup(int AgeGroupID)
        {
            con.Open();
            string query = "delete from AgeGroup where AgeGroupID=@AgeGroupID";


            using (sqlComObj = new MySqlCommand(query, con))
            {
                //adding parameters
                sqlComObj.Parameters.AddWithValue("@AgeGroupID", AgeGroupID);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }





        // *****************************  Employee Type Form  Related Queries   *********************************
        //  Get, Search, Add, Update, Delete

        public DataSet GetEmployeeType()
        {
            DataSet dsEmployeeType = new DataSet();

            string query = "SELECT * FROM EmployeeType";

            sqlComObj = new MySqlCommand(query, con); // sql command           
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsEmployeeType.Clear();
            sqlDataAdptObj.Fill(dsEmployeeType, "AgeGroup"); // fill dataset
            return dsEmployeeType;
        }
        public DataSet SearchEmployeeType(string SearchValue)
        {
            DataSet dsAgeGroup = new DataSet();

            string query = "SELECT * FROM EmployeeType ";
            query += " where EmployeeType Like @SearchValue";


            sqlComObj = new MySqlCommand(query, con); // sql command      
            sqlComObj.Parameters.AddWithValue("@SearchValue", "%" + SearchValue + "%");
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsAgeGroup.Clear();
            sqlDataAdptObj.Fill(dsAgeGroup, "EmployeeType"); // fill dataset

            return dsAgeGroup;
        }
        public int AddEmployeeType(string AgeGroup)
        {
            con.Open();
            // sql command to add size
            string query = "insert into EmployeeType(EmployeeType)";
            query += " values(@EmployeeType)";
            using (sqlComObj = new MySqlCommand(query, con))
            {

                sqlComObj.Parameters.AddWithValue("@EmployeeType", AgeGroup);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
            //getting the last inserted record, ColorID
            int lid = Int32.Parse(sqlComObj.LastInsertedId.ToString());
            return lid;
        }

        public void UpdateEmployeeType(int AgeGroupID, string AgeGroup)
        {

            con.Open();
            string query = "update EmployeeType set EmployeeType=@EmployeeType ";
            query += "where EmployeeTypeID=@EmployeeTypeID";

            using (sqlComObj = new MySqlCommand(query, con))
            {
                // adding parameters
                sqlComObj.Parameters.AddWithValue("@EmployeeType", AgeGroup);
                sqlComObj.Parameters.AddWithValue("@EmployeeTypeID", AgeGroupID);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }

        public void DeleteEmployeeType(int EmployeeTypeID)
        {
            con.Open();
            string query = "delete from EmployeeType where EmployeeTypeID=@EmployeeTypeID";


            using (sqlComObj = new MySqlCommand(query, con))
            {
                //adding parameters
                sqlComObj.Parameters.AddWithValue("@EmployeeTypeID", EmployeeTypeID);
                sqlComObj.ExecuteNonQuery();
            }
            con.Close();
        }





        // *****************************  SOME CODES FOR EASY REFERENCES *********************************

        /*

        public void Registration(BloodBank donor)
        {
            string query = "Insert Into bloodbank(id, name) Values('"+ donor.id +"','"+ donor.name +"')";

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;

            int result = cmd.ExecuteNonQuery();
            if (result == '1')
            {
                Console.WriteLine("Registration Successful...");
            }
        }
        public void SearchByname(string name)
        {
            string query = "select * from bloodbank Where name = '"+ name +"'";
            try
            {
                MySqlConnection conn = new MySqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();

                MySqlDataReader rdr = null;

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;

                rdr = cmd.ExecuteReader();

                while(rdr.Read())
                {
                    Console.WriteLine("ID: ", rdr["id"].ToString());
                    Console.WriteLine("Name: ", rdr["name"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        *******************  Codes from Sami Lab Project    ****************************

    
        public DataSet getDailySummary(DateTime fDate, DateTime tDate)
        {
            //DataSset Container
            DataSet dsDailySummary = new DataSet();

            tDate = tDate.AddDays(1);

            string query = " SELECT s.SID, UserDetails.Name, s.SampleDate, s.ReportDate, s.BillAmount, s.DiscountInRs, s.SpecialDiscount, s.PayableAmount, ";
            query += " s.Paid, s.Balance, s.PaymentMethod, s.OID, s.ServiceType, s.ServiceCharges, s.ServiceLocation, s.ServiceBy ";
            query += " FROM SampleInvoice As s ";
            query += " Join Patient On s.PID = Patient.PID ";
            query += " Join UserDetails On Patient.UID = UserDetails.UID ";            
            query += " WHERE SampleDate BETWEEN '" + fDate.ToString() + "' AND '" + tDate.ToString() + "'";
                       
            sqlComObj = new SqlCommand(query, con); // sql command    
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsDailySummary.Clear();
            sqlDataAdptObj.Fill(dsDailySummary, "SearchResults"); // fill dataset

            return dsDailySummary;
        }

        public DataSet getPatientDataForInvoicePrint(int sid)
        {
            //DataSset Container
            DataSet dsSearch = new DataSet();

            string query = "SELECT UserDetails.UID, UserDetails.Name, UserDetails.FHName, UserDetails.CNIC, UserDetails.ContactNo, ";
            query += " UserDetails.Age, UserDetails.Gender, UserDetails.DOB, ";
            query += " s.SampleDate, s.ReportDate, s.ReportHr, s.BillAmount, s.DiscountInRs, s.SpecialDiscount, s.PayableAmount, ";
            query += " s.Paid, s.Balance, s.PaymentMethod, s.OID, s.ServiceType, s.ServiceCharges, s.ServiceLocation, s.ServiceBy, ";
            query += " SampleInvoiceDetails.SIDID, SampleInvoiceDetails.PackageID,  SampleInvoiceDetails.TID, SampleInvoiceDetails.TPValue, ";
            query += " Package.PackageID, Package.PackageName, Package.Package_Fee, ";
            query += " Test.TName, Test.Fee, Test.DiscountInPrec, ";
            query += " TestParameters.TPID, TestParameters.TPName, ";
            query += " SampleInvoiceDetails.Description, TestParameters.Unit, TestParameters.NormalValue ";
            query += " FROM SampleInvoice As s ";
            query += " Join Patient On s.PID = Patient.PID ";
            query += " Join UserDetails On Patient.UID = UserDetails.UID ";
            query += " Join SampleInvoiceDetails On s.SID = SampleInvoiceDetails.SID ";
            query += " Join Test On SampleInvoiceDetails.TID = Test.TID ";
            query += " Join TestParameters On SampleInvoiceDetails.TPID = TestParameters.TPID ";
            query += " left Join Package On SampleInvoiceDetails.PackageID = Package.PackageID ";
            query += "where s.SID = @SID";
                       
            sqlComObj = new SqlCommand(query, con); // sql command    
            sqlComObj.Parameters.AddWithValue("@SID", sid);
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsSearch.Clear();
            sqlDataAdptObj.Fill(dsSearch, "SearchResults"); // fill dataset

            return dsSearch;
        }


        public DataSet SearchUserDetails(string fieldName, string searchValue)
        {
            //DataSset Container
            DataSet dsSearch = new DataSet();

            string query = "SELECT Patient.PID, u.UID, u.Name, u.FHName, u.DOB, u.Age, u.Gender, u.CNIC, u.ContactNo, u.Email, ";
            query += " u.Address1, u.Address2, u.Address_Area, u.City, u.Description ";
            query += " From UserDetails As u ";
            query += " Join Patient On Patient.UID = u.UID ";
            query += " where  u." + fieldName + " Like '%' + @SearchValue + '%'";

            sqlComObj = new SqlCommand(query, con); // sql command    
            sqlComObj.Parameters.AddWithValue("@SearchValue", searchValue);
            sqlDataAdptObj.SelectCommand = sqlComObj;
            dsSearch.Clear();
            sqlDataAdptObj.Fill(dsSearch, "SearchResults"); // fill dataset

            return dsSearch;
        }

         
        public void UpdateSampleInvoiceBalanceInfo(int sid, int payableAmount, int specialDiscount, string specialDiscountReason, int paid, int balance, int oid, string status)
        {
            con.Open();
            // sql command to add Medical Department
            string query = "update SampleInvoice set PayableAmount=@PayableAmount, SpecialDiscount=@SpecialDiscount, ";
            query += " SpecialDiscountReason =@SpecialDiscountReason, Paid=@Paid, Balance=@Balance, BalanceClearedBy=@BalanceClearedBy, status=@status ";
            query += " where SID=@SID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // adding parameters
                cmd.Parameters.AddWithValue("@SID", sid);
                cmd.Parameters.AddWithValue("@PayableAmount", payableAmount);
                cmd.Parameters.AddWithValue("@SpecialDiscount", specialDiscount);
                cmd.Parameters.AddWithValue("@SpecialDiscountReason", specialDiscountReason);
                cmd.Parameters.AddWithValue("@Paid", paid);
                cmd.Parameters.AddWithValue("@Balance", balance);
                cmd.Parameters.AddWithValue("@BalanceClearedBy", oid);
                cmd.Parameters.AddWithValue("@status", status);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        
        public void AddSampleInvoiceDetails(int sid, int packageId, int tid, int tpid, string tpValue, string description)
        {
            con.Open();
            // sql command to add UserDetails
            string query = "insert into SampleInvoiceDetails(SID, PackageID, TID, TPID, TPValue, Description)values(@SID, @PackageID, @TID, @TPID, @TPValue, @Description)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                // adding parameters

                cmd.Parameters.AddWithValue("@SID", sid);
                cmd.Parameters.AddWithValue("@PackageID", packageId);
                cmd.Parameters.AddWithValue("@TID", tid);
                cmd.Parameters.AddWithValue("@TPID", tpid);
                cmd.Parameters.AddWithValue("@TPValue", tpValue);
                cmd.Parameters.AddWithValue("@Description", description);

                cmd.ExecuteNonQuery();
            }
            con.Close();

        }

        public void UpdateSampleInvoice_Parameters(int sidid, string tpValue, string description)
        {
            con.Open();
            // sql command to add Medical Department
            using (SqlCommand cmd = new SqlCommand("update SampleInvoiceDetails set TPValue=@TPValue, Description=@Description where SIDID=@SIDID", con))
            {
                // adding parameters
                cmd.Parameters.AddWithValue("@SIDID", sidid);
                cmd.Parameters.AddWithValue("@TPValue", tpValue);
                cmd.Parameters.AddWithValue("@Description", description);

                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

         * */
    }
}
