using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;


namespace Cloth
{
    class ManageDB
    {
        static string conString;

        //local string for Sanaullah
        //conString = @"Data Source = CVALLEY\SQLEXPRESS; Initial Catalog =ML_IMS ; Integrated Security = True";

        //local string for Babar
        //conString = @"Data Source = CVALLEY\SQLEXPRESS; Initial Catalog =ML_IMS ; Integrated Security = True";

        //local string for Kashif
        //conString = @"Data Source = CVALLEY\SQLEXPRESS; Initial Catalog =ML_IMS ; Integrated Security = True";


        //Online String
        //static string conString = @"Server=192.168.18.2,1433;Network Library=DBMSSOCN;Initial Catalog=SAMILMS;User ID=operator;Password=operator786@";


        SqlConnection con = new SqlConnection(conString);
        // sql command 
        SqlCommand sqlComObj = new SqlCommand();
        // sql reader and adapter
        //SqlDataReader sqlDataRdrObj;
        SqlDataAdapter sqlDataAdptObj = new SqlDataAdapter();


        /*
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
