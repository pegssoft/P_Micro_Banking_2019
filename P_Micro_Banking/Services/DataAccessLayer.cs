using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using P_Micro_Banking.Models.Menu;
using P_Micro_Banking.Models;

using System;
using System.Data.SqlClient;
using P_Micro_Banking.Models.Customer;
using P_Micro_Banking.Models.Deposite;
using P_Micro_Banking.Models.Loan;
using P_Micro_Banking.Models.TrnsPosting;
using P_Micro_Banking.Models.Report;
using System.Net.Http;

namespace P_Micro_Banking.Services
{
    public class DataAccessLayer : IDataAccessLayer
    {
        public IConfiguration Configuration { get; }
        public DataAccessLayer(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public Task<List<Menu>> Menu(int code)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("MENU", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", code);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();


            Menu emenu = new Menu();
            List<Menu> lmenu = new List<Menu>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                emenu = new Menu();
                emenu.ID = Convert.ToInt32(row["ID"].ToString());
                emenu.MenuNames = row["Menu_Names"].ToString();
                emenu.Paths = row["Path"].ToString();
                emenu.IconClass = row["IconClass"].ToString();
                emenu.Url = row["Url"].ToString();
                emenu.OnClick = row["OnClick"].ToString();
                emenu.ListClass = row["ListClass"].ToString();
                emenu.aClass = row["aClass"].ToString();

                lmenu.Add(emenu);
            }

            return Task.FromResult(lmenu);

            //return the datatable as list
           
        }

        public Task<List<MenuChild>> MenuChild(int code, string roleid)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("MENU", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@code", code);
            cmd.Parameters.AddWithValue("@RoleID", roleid);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            MenuChild ecmenu = new MenuChild();
            List<MenuChild> lcmenu = new List<MenuChild>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ecmenu = new MenuChild();
                ecmenu.Menu_ID = Convert.ToInt32(row["Menu_ID"].ToString());
                ecmenu.MenuNames = row["Menu_Names"].ToString();
                ecmenu.Paths = row["Path"].ToString();
                ecmenu.IconClass = row["IconClass"].ToString();
                ecmenu.Url = row["Url"].ToString();
                ecmenu.OnClick = row["OnClick"].ToString();
                ecmenu.ListClass = row["ListClass"].ToString();
                ecmenu.aClass = row["aClass"].ToString();

                lcmenu.Add(ecmenu);
            }

            return Task.FromResult(lcmenu);

        }

        public string GetRoleIdAsync(string Userid)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GETUSERROLE", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Userid", Userid); 

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            string roleid = ds.Tables[0].Rows[0].Field<string>("ROLEID");

            //return the datatable as list
            
           return roleid;
            

        }
        public IEnumerable<UtilityClass> GetBranches()
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetBranches", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            // create the adapter
            SqlDataAdapter aadapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            aadapter.SelectCommand = cmd;
            //fill the data set with data from database
            aadapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetRoles()
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetRole", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public string InsertUserRole(UserRole userRole)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("passUserRoles", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@UserId", userRole.UserId);
            command.Parameters.AddWithValue("@RoleId", userRole.RoleId);

            command.Parameters.Add("@ReturnValue", SqlDbType.VarChar, 10);
            command.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@ReturnValue"].Value;
            con.Close();


            return msg;

        }

        public IEnumerable<UtilityClass> GetState()
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetState", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefId", 234); 

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetCity(string state)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCity", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RefId", state);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetRelationshipOfficer(string BranchCode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetRelationshipOfficer", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchCode", BranchCode);


            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetDepositePackage(string PackageClass)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDepositePackage", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PackageClass", PackageClass);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetLoanPackage()
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetLoanPackage", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@PackageClass", PackageClass);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public UtilityUserClass GetDepositePackageItem(string PackageID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDepositePackageItem", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PackageID", PackageID);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];
            
            //return the datatable as list
            UtilityUserClass utucls = new UtilityUserClass()
            {
                item1 = row["item1"].ToString(),
                item2 = row["item2"].ToString(),
                item3 = row["item3"].ToString(),
                item4 = row["item4"].ToString(),
                item5 = row["item5"].ToString(),
                item6 = row["item6"].ToString(),
                item7 = row["item7"].ToString(),
                item8 = row["Item8"].ToString(),

            };

            return utucls;
        }

        public string InsertCustomer(CustomerOnboarding onboarding, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_Customer", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@FirstName", onboarding.FirstName);
            command.Parameters.AddWithValue("@MiddleName", onboarding.MiddleName);
            command.Parameters.AddWithValue("@LastName", onboarding.LastName);
            command.Parameters.AddWithValue("@DOB", onboarding.DOB);
            command.Parameters.AddWithValue("@Gender", onboarding.Gender);
            command.Parameters.AddWithValue("@MaritalStatus", onboarding.MaritalStatus);
            command.Parameters.AddWithValue("@Title", onboarding.Title);
            command.Parameters.AddWithValue("@CusStatus", 1);
            command.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            command.Parameters.AddWithValue("@CreatedBy", user);
            command.Parameters.AddWithValue("@RelationshipOfficerID", onboarding.RelationshipOfficer);
            command.Parameters.AddWithValue("@BranchCode", branchcode);
            command.Parameters.AddWithValue("@Nok_Name", onboarding.NFirstName + " " + onboarding.NLastName);
            command.Parameters.AddWithValue("@Nok_Relationship", onboarding.Relationship);
            command.Parameters.AddWithValue("@Nok_Mobile", onboarding.NPhoneNumber);
            command.Parameters.AddWithValue("@Nok_Address", onboarding.NAddressLine);
            command.Parameters.AddWithValue("@Nok_email", onboarding.NEmail);
            command.Parameters.AddWithValue("@Addrss_Cntry", onboarding.Country);
            command.Parameters.AddWithValue("@Addrss_State", onboarding.State);
            command.Parameters.AddWithValue("@Addrss_City", onboarding.City);
            command.Parameters.AddWithValue("@Addrss_Line_1", onboarding.AddressLine1);
            command.Parameters.AddWithValue("@Addrss_Line_2", onboarding.AddressLine2);
            command.Parameters.AddWithValue("@IdentityType", onboarding.IdentificationType);
            command.Parameters.AddWithValue("@IdentityNum", onboarding.IdentificationNumber);
            command.Parameters.AddWithValue("@BankingStatus", 1);
            command.Parameters.AddWithValue("@Contact_Mobile", onboarding.PhoneNumber);
            command.Parameters.AddWithValue("@Contact_Email", onboarding.Email);
            command.Parameters.AddWithValue("@AccountHolderName", onboarding.FirstName + " " + onboarding.LastName);
            command.Parameters.AddWithValue("@AccountName", onboarding.AccountName);
            command.Parameters.AddWithValue("@AccountCode", onboarding.AccountCode);
            command.Parameters.AddWithValue("@AccountClass", onboarding.PackageClass);
            command.Parameters.AddWithValue("@AccountType", onboarding.PackageType);
            command.Parameters.AddWithValue("@InterestRate", onboarding.InterestRate);
            command.Parameters.AddWithValue("@DChargeRate", onboarding.DepositCharge);
            command.Parameters.AddWithValue("@WChargeRate", onboarding.WithdrawerCharge);
            command.Parameters.AddWithValue("@OpeningChge", onboarding.OpeningCharge);
            command.Parameters.AddWithValue("@Duration", onboarding.Duration);
            command.Parameters.AddWithValue("@DurationDIsc", "Days");
            command.Parameters.AddWithValue("@GLAccount", onboarding.GlAccNo);
            command.Parameters.AddWithValue("@AccountStatus", "InActive");

            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;

        }

        public IEnumerable<CustomerV> GetCustomer(string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomer", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BranchCode", branchcode);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new CustomerV
            {
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                Gender = row["Gender"].ToString(),
                DOB = row["DOB"].ToString(),
                CreatedDate = row["DateCreated"].ToString(),
                CustomerId = row["CustomerID"].ToString(),
                Title = row["Title"].ToString(),


            });
        }

        public CustomerOnboarding GetCustomerSingle(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomerSingle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            CustomerOnboarding utucls = new CustomerOnboarding()
            {
                FirstName = row["FirstName"].ToString(),
                MiddleName = row["MiddleName"].ToString(),
                LastName = row["LastName"].ToString(),
                Gender = Convert.ToInt32(row["Gender"].ToString()),
                DOB = Convert.ToDateTime(row["DOB"].ToString()),
                MaritalStatus = Convert.ToInt32(row["MaritalStatus"].ToString()),
                Title = Convert.ToInt32(row["Title"].ToString()),
                Email = row["Contact_Email"].ToString(),
                PhoneNumber = row["Contact_Mobile"].ToString(),
                Country = Convert.ToInt32(row["Addrss_Cntry"].ToString()),
                State = Convert.ToInt32(row["Addrss_State"].ToString()),
                City = Convert.ToInt32(row["Addrss_City"].ToString()),
                AddressLine1 = row["Addrss_Line_1"].ToString(),
                AddressLine2 = row["Addrss_Line_2"].ToString(),
                NFirstName = row["Nok_Name"].ToString(),
               // NLastName = row["item7"].ToString(),
                NEmail = row["Nok_email"].ToString(),
                NPhoneNumber = row["Nok_Mobile"].ToString(),
                Relationship = Convert.ToInt32(row["Nok_Relationship"].ToString()),
                NAddressLine = row["Nok_Address"].ToString(),
                IdentificationType = Convert.ToInt32(row["IdentityType"].ToString()),
                IdentificationNumber = row["IdentityNum"].ToString(),
                RelationshipOfficer = row["RelationshipOfficerID"].ToString(),

            };

            return utucls;
        }

        public DepositeAcc GetCustomerNameAccount(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomerName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if(ds.Tables[0].Rows.Count <= 0)
            {
                DepositeAcc utuclss = new DepositeAcc()
                {
                    FullNames = "",
                    accDetails = GetAccountDetails(CustomerID),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            DepositeAcc utucls = new DepositeAcc()
            {
                FullNames = row["FullName"].ToString(),
                accDetails = GetAccountDetails(CustomerID),
                

            };

            return utucls;
        }

        public UtilityClass GetLoanInterestRate(string PackageCode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetInterestRate", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PackageID", PackageCode);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                UtilityClass utuclss = new UtilityClass()
                {
                    code = "",
                    item = "",
                    item1 = "",


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            UtilityClass utucls = new UtilityClass()
            {
                code = row["Code"].ToString(),
                item = row["Item"].ToString(),
                item1 = row["item1"].ToString(),


            };

            return utucls;
        }


        public List<AccDetails> GetAccountDetails(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetAccDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            //cmd.Parameters.AddWithValue("@RoleID", roleid);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            AccDetails accdetails = new AccDetails();
            List<AccDetails> listaccdetails = new List<AccDetails>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new AccDetails();
                accdetails.AccountCode = row["AccountCode"].ToString();
                accdetails.AccountNumber = row["AccountNumber"].ToString();
                accdetails.AccountName = row["AccountHolderName"].ToString();
                accdetails.PackageClass = row["AccountClass"].ToString();
                accdetails.PackageType = row["AccountTYpe"].ToString();
                accdetails.Status = row["AccountStatus"].ToString();

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }

        public NewAccount GetCustomerSingleName(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomerName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                NewAccount utuclss = new NewAccount()
                {
                    AccountName = "",
                    //accDetails = GetAccountDetails(CustomerID),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            NewAccount utucls = new NewAccount()
            {
                AccountName = row["FullName"].ToString(),
                CustomerID = CustomerID
                //accDetails = GetAccountDetails(CustomerID),


            };

            return utucls;
        }

        public string InsertNewAccount(NewAccount account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("PassDepositAcc", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@CustomerID", account.CustomerN);
            command.Parameters.AddWithValue("@AccountName", account.AccountName);
            command.Parameters.AddWithValue("@AccountHolderName", account.CustomerName);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@AccountClass", account.PackageClass);
            command.Parameters.AddWithValue("@AccountType", account.PackageType);
            command.Parameters.AddWithValue("@InterestRate", account.InterestRate);
            command.Parameters.AddWithValue("@DChargeRate", account.DepositCharge);
            command.Parameters.AddWithValue("@WChargeRate", account.WithdrawerCharge);
            command.Parameters.AddWithValue("@Duration", account.Duration);
            command.Parameters.AddWithValue("@DurationDIsc", "Days");
            command.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            command.Parameters.AddWithValue("@GLAccount", account.GlAccNo);
            command.Parameters.AddWithValue("@CreatedBy", user);
            command.Parameters.AddWithValue("@RelationshipOfficerID", account.RelationshipOfficer);
            command.Parameters.AddWithValue("@BranchCode", branchcode);
            command.Parameters.AddWithValue("@AccountStatus", "InActive");
            command.Parameters.AddWithValue("@OpeningChge", account.OpeningCharge);
            command.Parameters.AddWithValue("@FixedStartDate", null);
            command.Parameters.AddWithValue("@NoDays", null);
            command.Parameters.AddWithValue("@NoMonths", null);
            command.Parameters.AddWithValue("@FixMatureDate", null);
            command.Parameters.AddWithValue("@Amount", null);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 10);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@DAccNo", SqlDbType.VarChar, 10);
            command.Parameters["@DAccNo"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;
        }

        public string InsertFixedDeposite(NewAccounts account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_FixedDeposit", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@CustomerID", account.CustomerN);
            command.Parameters.AddWithValue("@AccountName", account.AccountName);
            command.Parameters.AddWithValue("@AccountHolderName", account.CustomerName);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@AccountClass", account.PackageClass);
            command.Parameters.AddWithValue("@AccountType", account.PackageType);
            command.Parameters.AddWithValue("@InterestRate", account.InterestRate);
            command.Parameters.AddWithValue("@DChargeRate", account.DepositCharge);
            command.Parameters.AddWithValue("@WChargeRate", account.WithdrawerCharge);
            command.Parameters.AddWithValue("@Duration", account.Duration);
            command.Parameters.AddWithValue("@DurationDIsc", "Days");
            command.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            command.Parameters.AddWithValue("@GLAccount", account.GlAccNo);
            command.Parameters.AddWithValue("@CreatedBy", user);
            command.Parameters.AddWithValue("@RelationshipOfficerID", account.RelationshipOfficer);
            command.Parameters.AddWithValue("@BranchCode", branchcode);
            command.Parameters.AddWithValue("@AccountStatus", "Active");
            command.Parameters.AddWithValue("@OpeningChge", account.OpeningCharge);
            command.Parameters.AddWithValue("@FixedStartDate", account.StartDate);
            command.Parameters.AddWithValue("@NoDays", null);
            command.Parameters.AddWithValue("@NoMonths", account.NoMonth);
            command.Parameters.AddWithValue("@FixMatureDate", Convert.ToDateTime(account.MatureDate));
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Deposite");
            command.Parameters.AddWithValue("@TransNaration", "Fixed Deposit");
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            command.Parameters.AddWithValue("@Cr_Amount", account.Amount);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 10);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;
        }

        public AccountTransaction GetCustomerAccNBTD(string account)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetAccNBH", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", account);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                AccountTransaction utuclss = new AccountTransaction()
                {
                    FullNames = "",
                    charge = "0",
                    CurrentBalance = "0.00",
                    History = GetTransHistory(account),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            AccountTransaction utucls = new AccountTransaction()
            {
                FullNames = row["AccountHolderName"].ToString(),
                charge = row["WChargeRate"].ToString(),
                CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]).ToString("#,##00.00"),
                History = GetTransHistory(account),


            };

            return utucls;
        }

        public List<AccountHistory> GetTransHistory(string accountNum)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetAccNBH", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", accountNum);
            cmd.Parameters.AddWithValue("@code", 2);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            AccountHistory accdetails = new AccountHistory();
            List<AccountHistory> listaccdetails = new List<AccountHistory>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new AccountHistory();
                accdetails.TransReference = row["TransReference"].ToString();
                accdetails.TransDiscription = row["TransDiscription"].ToString();
                accdetails.TransCode = row["TransCode"].ToString();
                accdetails.TransNaration = row["TransNaration"].ToString();
                accdetails.TransDate = row["TransDate"].ToString();
                accdetails.Cr_Dr = row["Cr_Dr"].ToString();
                accdetails.Amount = Convert.ToDecimal(row["Amount"]).ToString("#,##00.00");
                accdetails.Balance = Convert.ToDecimal(row["Balance"]).ToString("#,##00.00");

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }


        public TransactionSearch GetTransactionSearch(string Account)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetAccNBH", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", Account);
            cmd.Parameters.AddWithValue("@code", 3);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                TransactionSearch utuclss = new TransactionSearch()
                {
                    AccountNumber1 = "",
                    AccountHolderName = "",
                    AccountName = "",
                    AccountCode = "",
                    AccountClass ="",
                    InterestRate = "",
                    DChargeRate = "",
                    WChargeRate = "",
                    Duration = "",
                    PhoneNumber = "",
                    GLAccount = "",
                    RelationshipOfficerID = "",
                    BranchCode = "",
                    OpeningChrge= ""

                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            TransactionSearch utucls = new TransactionSearch()
            {
                AccountNumber1 = Account,
                AccountHolderName = row["AccountHolderName"].ToString(),
                AccountName = row["AccountName"].ToString(),
                AccountCode = row["AccountCode"].ToString(),
                AccountClass = row["AccountClass"].ToString(),
                InterestRate = row["InterestRate"].ToString(),
                DChargeRate = row["DChargeRate"].ToString(),
                WChargeRate = row["WChargeRate"].ToString(),
                OpeningChrge = row["OpeningChge"].ToString(),
                Duration = row["Duration"].ToString(),
                PhoneNumber = row["Contact_Mobile"].ToString(),
                GLAccount = row["GLAccount"].ToString(),
                RelationshipOfficerID = row["RelationshipOfficerID"].ToString(),
                BranchCode = row["BranchCode"].ToString()


            };

            return utucls;
        }

        public TransactionSearch GetTransactionSearchForFixed(string Account)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetAccNBH", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", Account);
            cmd.Parameters.AddWithValue("@code", 3);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                TransactionSearch utuclss = new TransactionSearch()
                {
                    AccountNumber1 = "",
                    AccountHolderName = "",
                    AccountName = "",
                    AccountCode = "",
                    AccountClass = "",
                    InterestRate = "",
                    DChargeRate = "",
                    WChargeRate = "",
                    Duration = "",
                    PhoneNumber = "",
                    GLAccount = "",
                    RelationshipOfficerID = "",
                    BranchCode = "",
                    OpeningChrge = "",
                    PrewithCharge = "",
                    AccumulatedNoMonth = "",
                    StartDate = "",
                    AcctualNoMonth = "",
                    Maturitydate = "",
                    Interest = "",
                    Total = "",
                    Amount = ""

                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            string a = row["Amount"].ToString();

            decimal amount = string.IsNullOrEmpty(a) ? 0 : Convert.ToDecimal(row["Amount"]);
            decimal InterestRate = string.IsNullOrEmpty(a) ? 0 : Convert.ToDecimal(row["InterestRate"]);
            DateTime Startdate = string.IsNullOrEmpty(a) ? DateTime.Now.Date : Convert.ToDateTime(row["FixedStartDate"]);
            int nomonth = string.IsNullOrEmpty(a) ? 0 : Convert.ToInt32(row["NoMonths"]);
            decimal WChargeRate = string.IsNullOrEmpty(a) ? 0 : Convert.ToDecimal(row["WChargeRate"]);
            DateTime enddate = string.IsNullOrEmpty(a) ? DateTime.Now.Date : Convert.ToDateTime(row["FixMatureDate"]);
            decimal interest = 0; decimal total = 0; decimal WCR = 0;

            int month = (DateTime.Now.Year - Startdate.Year) * 12 + DateTime.Now.Month - Startdate.Month;
            if (nomonth <= month)
            {
                decimal rate = InterestRate * nomonth;
                interest = ((rate / 100) * amount);

                WCR = 0;
                // Calculating total payment
                total = ((amount + interest) - WCR);
            }
            else
            {
                decimal prate = InterestRate * month;
                interest = ((prate / 100) * amount);

                WCR = ((WChargeRate / 100) * amount);
                // Calculating total payment
                total = ((amount + interest) - WCR);
            }

            //return the datatable as list
            TransactionSearch utucls = new TransactionSearch()
            {
                AccountNumber1 = Account,
                AccountHolderName = row["AccountHolderName"].ToString(),
                AccountName = row["AccountName"].ToString(),
                AccountCode = row["AccountCode"].ToString(),
                AccountClass = row["AccountClass"].ToString(),
                InterestRate = row["InterestRate"].ToString(),
                DChargeRate = row["DChargeRate"].ToString(),
                WChargeRate = row["WChargeRate"].ToString(),
                OpeningChrge = row["OpeningChge"].ToString(),
                Duration = row["Duration"].ToString(),
                PhoneNumber = row["Contact_Mobile"].ToString(),
                GLAccount = row["GLAccount"].ToString(),
                RelationshipOfficerID = row["RelationshipOfficerID"].ToString(),
                BranchCode = row["BranchCode"].ToString(),
                PrewithCharge = WCR.ToString("#,##00.00"),
                AccumulatedNoMonth = month > nomonth ? nomonth.ToString() : month.ToString(),
                StartDate = Startdate.ToShortDateString(),
                AcctualNoMonth = nomonth.ToString(),
                Maturitydate = enddate.ToShortDateString(),
                Interest = interest.ToString("#,##00.00"),
                Total = total.ToString("#,##00.00"),
                Amount= amount.ToString("#,##00.00")

            };

            return utucls;
        }

        public Tuple<string, string> InsertOpenning(TransactionSearch account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_OpeningDeposite", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber1);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@DChargeRate", account.OpeningChrge);
            command.Parameters.AddWithValue("@GLAccount", account.GLAccount);
            command.Parameters.AddWithValue("@AccountStatus", "Active");
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Deposit");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            command.Parameters.AddWithValue("@Cr_Amount", account.Amount);
            command.Parameters.AddWithValue("@Dr_Amount", 0);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@RetBal", SqlDbType.VarChar, 50);
            command.Parameters["@RetBal"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            string msg1 = (string)command.Parameters["@RetBal"].Value;
            con.Close();


            return Tuple.Create(msg, msg1);
        }

        public Tuple<string, string> InsertCashDeposit(TransactionSearch account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_CashDeposite", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber1);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@DChargeRate", account.DChargeRate);
            command.Parameters.AddWithValue("@GLAccount", account.GLAccount);
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Deposite");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            command.Parameters.AddWithValue("@Cr_Amount", account.Amount);
            command.Parameters.AddWithValue("@Dr_Amount", 0);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@RetBal", SqlDbType.VarChar, 50);
            command.Parameters["@RetBal"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            string msg1 = (string)command.Parameters["@RetBal"].Value;
            con.Close();


            return Tuple.Create(msg, msg1);
        }

        public string InsertPostFixedDepositeInterest(TransactionSearch account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_FixedDepositeInterest", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber1);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@CreateBy", user);

            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Deposite");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            decimal inter = Convert.ToDecimal(account.Interest.Replace(",",""));
            command.Parameters.AddWithValue("@I_Amount", inter);
            decimal Prewith = Convert.ToDecimal(account.PrewithCharge.Replace(",", ""));
            command.Parameters.AddWithValue("@PWC_Amount", Prewith);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;
        }

        public Tuple<string, string> InsertCashWithdrawer(TransactionSearch account, string user, string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_CashWithdrawer", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber1);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@WChargeRate", account.WChargeRate);
            command.Parameters.AddWithValue("@GLAccount", account.GLAccount);
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Withdrawer");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Dr");
            command.Parameters.AddWithValue("@Cr_Amount", 0);
            command.Parameters.AddWithValue("@Dr_Amount", account.Amount);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@RetBal", SqlDbType.VarChar, 50);
            command.Parameters["@RetBal"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            string msg1 = (string)command.Parameters["@RetBal"].Value;
            con.Close();


            return Tuple.Create(msg, msg1);
        }

        public DisbursmentDetails GetCustomerNameForLoan(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetCustomerName", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                DisbursmentDetails utuclss = new DisbursmentDetails()
                {
                    AccountName = "",
                    //accDetails = GetAccountDetails(CustomerID),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            DisbursmentDetails utucls = new DisbursmentDetails()
            {
                AccountName = row["FullName"].ToString(),
                PhoneNumber = row["Contact_Mobile"].ToString(),
                CustomerID = CustomerID
                //accDetails = GetAccountDetails(CustomerID),


            };

            return utucls;
        }

        public Tuple<string, string> InsertDisbursment(DisbursmentDetails disbursment, string user, string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            var maxid = UserNumber(7);
            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_Disbursment", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", string.Concat(disbursment.LoanCode, maxid));
            command.Parameters.AddWithValue("@LoanCode", disbursment.LoanCode);
            command.Parameters.AddWithValue("@InterestType", 1);
            command.Parameters.AddWithValue("@InterestRate", disbursment.InterestRate);
            command.Parameters.AddWithValue("@Tenor", disbursment.Tenor);
            command.Parameters.AddWithValue("@PrincipalAmount", disbursment.LoanAmount);
            command.Parameters.AddWithValue("@PrincipalInterestAmount", disbursment.PrincipalInterestAmount);
            command.Parameters.AddWithValue("@TotalInterest", disbursment.TotalInterest);
            command.Parameters.AddWithValue("@InterestPerDuration", disbursment.InterestPerDuration);
            command.Parameters.AddWithValue("@PrincipalInterestAmountPerDuration", disbursment.PrincipalInterestAmountPerDuration);
            command.Parameters.AddWithValue("@DateCreated", DateTime.Now);
            command.Parameters.AddWithValue("@CreatedBy", user);
            command.Parameters.AddWithValue("@RelationshipOfficerID", disbursment.RelationshipOfficerID);
            command.Parameters.AddWithValue("@BranchCode", branchcode);
            command.Parameters.AddWithValue("@DisbursmentStatus", "Active");
            command.Parameters.AddWithValue("@CustomerID", disbursment.CustomerN);
            command.Parameters.AddWithValue("@AccountHolderName", disbursment.CustomerName);
            command.Parameters.AddWithValue("@AccountCode", disbursment.LoanCode);
            command.Parameters.AddWithValue("@GLAccount", disbursment.GlAccNo);
            command.Parameters.AddWithValue("@IntrstGLAccount", disbursment.IntrstGlAccNo);
            command.Parameters.AddWithValue("@AccountStatus", "Active");
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "System Creation");
            command.Parameters.AddWithValue("@TransNaration", "Loan Disbursment");
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Dr");
            command.Parameters.AddWithValue("@Cr_Amount", 0);
            command.Parameters.AddWithValue("@Dr_Amount", disbursment.LoanAmount);


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return Tuple.Create(msg, string.Concat(disbursment.LoanCode, maxid));
        }

        private static readonly Random rdm = new Random();
        //Random ten digit unique number
        public string UserNumber(int digits)
        {
            if (digits <= 1) return "";

            var _min = (int)Math.Pow(10, digits - 1);
            var _max = (int)Math.Pow(10, digits) - 1;
            return rdm.Next(_min, _max).ToString();
        }

        public LoanAccount GetDisbursmentSchName(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDisDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@code", 2);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                LoanAccount utuclss = new LoanAccount()
                {
                    FullNames = "",
                    //accDetails = GetAccountDetails(CustomerID),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            LoanAccount utucls = new LoanAccount()
            {
                FullNames = row["AccountHolderName"].ToString(),
                CustomerID = CustomerID,
                accDetails = GetDisbAccHistory(CustomerID),


            };

            return utucls;
        }

        public List<LoanAccDetails> GetDisbAccHistory(string CustomerID)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDisDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CustomerID", CustomerID);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            LoanAccDetails accdetails = new LoanAccDetails();
            List<LoanAccDetails> listaccdetails = new List<LoanAccDetails>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new LoanAccDetails();
                accdetails.AccountNumber = row["AccountNumber"].ToString();
                accdetails.LoanAccountName = row["AccountName"].ToString();
                accdetails.LoanCode = row["LoanCode"].ToString();
                accdetails.LoanAmount = row["PrincipalAmount"].ToString();
                accdetails.Status = row["DisbursmentStatus"].ToString();
                

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }
        public AccountTransactions GetLoan(string account)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetLoanAccDet", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", account);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                AccountTransactions utuclss = new AccountTransactions()
                {
                    FullNames = "",
                    CurrentBalance = "0.00",
                    History = GetTransHistorys(account),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            AccountTransactions utucls = new AccountTransactions()
            {
                FullNames = row["AccountHolderName"].ToString(),
                CurrentBalance = Convert.ToDecimal(row["CurrentBalance"]).ToString("#,##00.00"),
                History = GetTransHistorys(account),


            };

            return utucls;
        }

        public List<AccountHistorys> GetTransHistorys(string accountNum)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetLoanAccDet", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", accountNum);
            cmd.Parameters.AddWithValue("@code", 2);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            AccountHistorys accdetails = new AccountHistorys();
            List<AccountHistorys> listaccdetails = new List<AccountHistorys>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new AccountHistorys();
                accdetails.TransReference = row["TransReference"].ToString();
                accdetails.TransDiscription = row["TransDiscription"].ToString();
                accdetails.TransCode = row["TransCode"].ToString();
                accdetails.TransNaration = row["TransNaration"].ToString();
                accdetails.TransDate = row["TransDate"].ToString();
                accdetails.Cr_Dr = row["Cr_Dr"].ToString();
                accdetails.Amount = Convert.ToDecimal(row["Amount"]).ToString("#,##00.00");
                accdetails.Balance = Convert.ToDecimal(row["Balance"]).ToString("#,##00.00");

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }

        public RepyTransScherch GetLoanForRepayment(string account)
        {

            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetTransForRepy", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AccountNumber", account);
            cmd.Parameters.AddWithValue("@code", 1);
            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            if (ds.Tables[0].Rows.Count <= 0)
            {
                RepyTransScherch utuclss = new RepyTransScherch()
                {
                    AccountHolderName = "",
                    //accDetails = GetAccountDetails(CustomerID),


                };
                return utuclss;
            }

            var row = dataTable.NewRow();
            row = dataTable.Rows[0];

            //return the datatable as list
            RepyTransScherch utucls = new RepyTransScherch()
            {
                AccountHolderName = row["AccountHolderName"].ToString(),
                AccountNumber1 = account,
                GLAccount = row["GLAccount"].ToString(),
                InterestRate = row["InterestRate"].ToString(),
                AccountCode = row["AccountCode"].ToString(),
                PhoneNumber = row["Contact_Mobile"].ToString(),

                //accDetails = GetAccountDetails(CustomerID),


            };

            return utucls;
        }

        public Tuple<string, string> InsertCashRepayment(RepyTransScherch account, string user, string branchcode)
        {
            //get the connection string
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_Repayment", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber1);
            command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
            command.Parameters.AddWithValue("@GLAccount", account.GLAccount);
            command.Parameters.AddWithValue("@PAmount", account.PrincipalAmount);
            command.Parameters.AddWithValue("@PIAmount", account.PrincipalInterestAmount);
            command.Parameters.AddWithValue("@IAmount", account.TotalInterest);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "Loan");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            command.Parameters.Add("@RetBal", SqlDbType.VarChar, 50);
            command.Parameters["@RetBal"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            string msg1 = (string)command.Parameters["@RetBal"].Value;
            con.Close();


            return Tuple.Create(msg, msg1);
        }

        public IEnumerable<GlOperations> GetDrawerBalance(string user, string status)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDrawerDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreateBy", user);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new GlOperations
            {
                DrawerCode = row["AccountNum"].ToString(),
                AccountName = row["AccountName"].ToString(),
                CurrentDate = DateTime.Now.ToShortDateString(),
                Balance = Convert.ToDecimal(row["CurrentBalance"]).ToString("N"),
                Status = row["AccStatus"].ToString()

            });
        }

        public IEnumerable<UtilityClass> GetTeller(string user, string status)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDrawerDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreateBy", user);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@code", 2);


            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public IEnumerable<UtilityClass> GetToAccount(string user, string status)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetDrawerDetails", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CreateBy", user);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@code", 3);


            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new UtilityClass
            {
                code = row["code"].ToString(),
                item = row["item"].ToString(),

            });
        }

        public string InsertGLCredit(GLTransPosting account, string user, string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_GLCredit", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@Teller", account.Teller);
            command.Parameters.AddWithValue("@ToAccount", account.ToAccount);
            command.Parameters.AddWithValue("@AccountCode", "801");
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "GL Transactions");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Cr");
            


            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;
        }

        public string InsertGLDebit(GLTransPosting account, string user, string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);
            con.Open();
            SqlCommand command = new SqlCommand("Trans_Save_GLDebit", con);
            command.CommandType = CommandType.StoredProcedure;
            //accept the parameters
            command.Parameters.AddWithValue("@Teller", account.Teller);
            command.Parameters.AddWithValue("@ToAccount", account.ToAccount);
            command.Parameters.AddWithValue("@AccountCode", "801");
            command.Parameters.AddWithValue("@Amount", account.Amount);
            command.Parameters.AddWithValue("@CreateBy", user);
            command.Parameters.AddWithValue("@TransReference", Guid.NewGuid());
            command.Parameters.AddWithValue("@TransDiscription", "GL Transactions");
            command.Parameters.AddWithValue("@TransNaration", account.Naration);
            command.Parameters.AddWithValue("@TransDate", DateTime.Now);
            command.Parameters.AddWithValue("@Cr_Dr", "Dr");



            command.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
            command.Parameters["@RetCode"].Direction = ParameterDirection.Output;
            //execute query
            command.ExecuteNonQuery();
            //get the output parameters
            string msg = (string)command.Parameters["@RetCode"].Value;
            con.Close();


            return msg;
        }

        public ReportUtility GetTrailbalance(DateTime startdate, DateTime mStartdate, DateTime mEnddate)
        {
            ReportUtility utility = new ReportUtility()
            {
                Incomes = incomeT(startdate, mStartdate, mEnddate ),
                Liabilitys = LiabilityT(startdate, mStartdate, mEnddate)
            };

            return utility;
        }

        public List<Income> incomeT(DateTime startdate, DateTime mStartdate, DateTime mEnddate)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetTrailBalanceIncomeExp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Openingstartdate", startdate);
            cmd.Parameters.AddWithValue("@Monthstartdate", mStartdate);
            cmd.Parameters.AddWithValue("@Monthenddate", mEnddate);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            Income accdetails = new Income();
            List<Income> listaccdetails = new List<Income>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new Income();
                accdetails.AccountNumber = row["AccountNum"].ToString();
                accdetails.AccountName = row["GlName"].ToString();
                accdetails.Opening = row["Opening"].ToString();
                accdetails.Credit = row["CrAmount"].ToString();
                accdetails.Debit = row["DrAmount"].ToString();
                accdetails.Closing = row["Closing"].ToString();

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }

        public List<Liability> LiabilityT(DateTime startdate, DateTime mStartdate, DateTime mEnddate)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetTrailBalanceIncomeExp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Openingstartdate", startdate);
            cmd.Parameters.AddWithValue("@Monthstartdate", mStartdate);
            cmd.Parameters.AddWithValue("@Monthenddate", mEnddate);
            cmd.Parameters.AddWithValue("@code", 2);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            Liability accdetails = new Liability();
            List<Liability> listaccdetails = new List<Liability>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                accdetails = new Liability();
                accdetails.AccountNumber = row["AccountNum"].ToString();
                accdetails.AccountName = row["GlName"].ToString();
                accdetails.Opening = row["Opening"].ToString();
                accdetails.Credit = row["CrAmount"].ToString();
                accdetails.Debit = row["DrAmount"].ToString();
                accdetails.Closing = row["Closing"].ToString();

                listaccdetails.Add(accdetails);
            }

            return listaccdetails;
        }

        public IEnumerable<NewClient> GetNewClient(DateTime mStartdate, DateTime mEnddate)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetNewClientDisTransac", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", mStartdate);
            cmd.Parameters.AddWithValue("@endDate", mEnddate);
            cmd.Parameters.AddWithValue("@code", 1);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            //return the datatable as list
            return ds.Tables[0].AsEnumerable().Select(row => new NewClient
            {
                count = row["CID"].ToString(),
                CustomerID = row["CustomerID"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                CreatedBy = row["CreatedBy"].ToString(),
                RelationshipOfficerID = row["RelationshipOfficerID"].ToString(),

            });
        }

        public DisburmentTotal GetDisBursment(DateTime mStartdate, DateTime mEnddate)
        {
            decimal PAs = 0;
            decimal PIAs = 0;
            decimal TIs = 0;

            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;

            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetNewClientDisTransac", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", mStartdate);
            cmd.Parameters.AddWithValue("@endDate", mEnddate);
            cmd.Parameters.AddWithValue("@code", 2);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            Disburment dis = new Disburment();
            List<Disburment> disburments = new List<Disburment>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                dis = new Disburment();
                dis.AccountNumber = row["AccountNumber"].ToString();
                dis.AccountHolderName = row["AccountHolderName"].ToString();
                dis.LaonName = row["AccountName"].ToString();
                dis.Tenor = row["Tenor"].ToString();
                dis.Date = row["DateCreated"].ToString();
                dis.PrincipalAmount = Convert.ToDecimal(row["PrincipalAmount"]).ToString("#,##00.00");
                dis.PrincipalInterestAmount = Convert.ToDecimal(row["PrincipalInterestAmount"]).ToString("#,##00.00");
                dis.TotalInterest = Convert.ToDecimal(row["TotalInterest"]).ToString("#,##00.00");
                dis.RelationshipOfficerID = row["RelationshipOfficerID"].ToString();

                decimal PA = Convert.ToDecimal(row["PrincipalAmount"]);
                decimal PIA = Convert.ToDecimal(row["PrincipalInterestAmount"]);
                decimal TI = Convert.ToDecimal(row["TotalInterest"]);

                PAs += PA;
                PIAs += PIA;
                TIs += TI;

                disburments.Add(dis);
            }

            DisburmentTotal total = new DisburmentTotal()
            {
                PrincipalAmount = PAs.ToString("#,##00.00"),
                PrincipalInterestAmount = PIAs.ToString("#,##00.00"),
                TotalInterest = TIs.ToString("#,##00.00"),
                disburments = disburments
            };


            return total;
        }

        public Transction GetTransaction(DateTime mStartdate, DateTime mEnddate)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            decimal sumAm = 0;
            decimal sumAmd = 0;
            decimal sumAmw = 0;
            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetNewClientDisTransac", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", mStartdate);
            cmd.Parameters.AddWithValue("@endDate", mEnddate);
            cmd.Parameters.AddWithValue("@code", 3);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];
            DataTable dataTable1 = ds.Tables[1];
            DataTable dataTable2 = ds.Tables[2];



            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            Repayment repayment = new Repayment();
            List<Repayment> repayments = new List<Repayment>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                repayment = new Repayment();
                repayment.AccountNumber = row["AccountNumber"].ToString();
                repayment.ReletionshipId = row["RelationshipOfficerID"].ToString();
                repayment.Amount = row["Cr_Amount"].ToString();

                decimal a = Convert.ToDecimal(row["Cr_Amount"]);
                sumAm += a;

                repayments.Add(repayment);

            }

            Deposite deposite = new Deposite();
            List<Deposite> deposits = new List<Deposite>();
            foreach (DataRow rows in ds.Tables[1].Rows)
            {
                deposite = new Deposite();
                deposite.AccountNumber = rows["AccountNumber"].ToString();
                deposite.ReletionshipId = rows["RelationshipOfficerID"].ToString();
                deposite.Amount = rows["Cr_Amount"].ToString();

                decimal b = Convert.ToDecimal(rows["Cr_Amount"]);
                sumAmd += b;

                deposits.Add(deposite);

            }

            Widthdrawer widthdrawer = new Widthdrawer();
            List<Widthdrawer> widthdrawers = new List<Widthdrawer>();
            foreach (DataRow roww in ds.Tables[2].Rows)
            {
                widthdrawer = new Widthdrawer();
                widthdrawer.AccountNumber = roww["AccountNumber"].ToString();
                widthdrawer.ReletionshipId = roww["RelationshipOfficerID"].ToString();
                widthdrawer.Amount = roww["Dr_Amount"].ToString();

                decimal c = Convert.ToDecimal(roww["Dr_Amount"]);
                sumAmw += c;

                widthdrawers.Add(widthdrawer);

            }

            Transction transction = new Transction()
            {
                RepaymentTotal = Convert.ToDecimal(sumAm).ToString("#,##00.00"),
                DepositeTotal = Convert.ToDecimal(sumAmd).ToString("#,##00.00"),
                WidthdrawerTotal = Convert.ToDecimal(sumAmw).ToString("#,##00.00"),
                repayments = repayments,
                deposite = deposits,
                widthdrawer = widthdrawers
            };

            return transction;
        }

        public LoanBalanceTotal GetLoanBalance()
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            decimal sum1 = 0;
            decimal sum2 = 0;
            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetNewClientDisTransac", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@startDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@endDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@code", 4);

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];


            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();


            LoanBalance loanBalance = new LoanBalance();
            List<LoanBalance> balances = new List<LoanBalance>();
            foreach ( DataRow row in ds.Tables[0].Rows)
            {
                loanBalance = new LoanBalance();
                loanBalance.AccountNumber = row["AccountNumber"].ToString();
                loanBalance.AccountHolderName = row["AccountHolderName"].ToString();
                loanBalance.AccountName = row["AccountName"].ToString();
                loanBalance.Paidby = row["CreateBy"].ToString();
                loanBalance.ReletionshipId = row["RelationshipOfficerID"].ToString();
                loanBalance.Count = row["Tenor"].ToString();
                loanBalance.Totalpaid = Convert.ToDecimal(row["total"]).ToString("#,##00.00");
                loanBalance.Balance = Convert.ToDecimal(row["balance"]).ToString("#,##00.00");

                decimal t = Convert.ToDecimal(row["total"]);
                decimal b = Convert.ToDecimal(row["balance"]);

                sum1 += t;
                sum2 += b;

                balances.Add(loanBalance);
            }

            LoanBalanceTotal loan = new LoanBalanceTotal()
            {
                Totalpaid = sum1.ToString("#,##00.00"),
                Balance = sum2.ToString("#,##00.00"),
                loans = balances
            };

            //return the datatable as list
            return loan;
        }

        public TransOfficer GetTransOfficer(string Officer, int selection, DateTime mStartdate, DateTime mEnddate)
        {

            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            decimal sumAm = 0;
            decimal sumAmd = 0;
            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetOfficerPotfolio", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            if (selection == 1)
            {
                cmd.Parameters.AddWithValue("@startDate", DateTime.Now );
                cmd.Parameters.AddWithValue("@endDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Officer", Officer);
                cmd.Parameters.AddWithValue("@code", 1);
            }
            else
            {
                cmd.Parameters.AddWithValue("@startDate", mStartdate);
                cmd.Parameters.AddWithValue("@endDate", mEnddate);
                cmd.Parameters.AddWithValue("@Officer", Officer);
                cmd.Parameters.AddWithValue("@code", 2);
            }
            

            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];
            DataTable dataTable1 = ds.Tables[1];



            // close and distroy the datebase.
            cmd.Dispose();
            con.Close();

            Depsit depsit = new Depsit();
            List<Depsit> depsits = new List<Depsit>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                depsit = new Depsit();
                depsit.AccountNumber = row["AccountNumber"].ToString();
                depsit.AccountHolderName = row["AccountHolderName"].ToString();
                depsit.AccountName = row["AccountName"].ToString();
                depsit.Balance = row["savingsBalance"].ToString();

                decimal a = Convert.ToDecimal(row["savingsBalance"]);
                sumAm += a;

                depsits.Add(depsit);

            }

            LoanBal loanBal = new LoanBal();
            List<LoanBal> loanBals = new List<LoanBal>();
            foreach (DataRow rows in ds.Tables[1].Rows)
            {
                loanBal = new LoanBal();
                loanBal.AccountNumber = rows["AccountNumber"].ToString();
                loanBal.AccountHolderName = rows["AccountHolderName"].ToString();
                loanBal.AccountName = rows["AccountName"].ToString();
                loanBal.Balance = rows["loanBalance"].ToString();

                decimal b = Convert.ToDecimal(rows["loanBalance"]);
                sumAmd += b;

                loanBals.Add(loanBal);

            }



            TransOfficer transction = new TransOfficer()
            {
                TotalSavingsBal = Convert.ToDecimal(sumAm).ToString("#,##00.00"),
                TotalLoanBal = Convert.ToDecimal(sumAmd).ToString("#,##00.00"),
                deposite = depsits,
                loans = loanBals,
            };

            return transction;

        }

        public string smsCharge(smsCharge charge, string user, string branchcode)
        {
            string connection = Configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            string msgs = string.Empty;
            //create the connection
            SqlConnection con = new SqlConnection(connection);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetSMScharge", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            // create the adapter
            SqlDataAdapter adapter = new SqlDataAdapter();
            //create a new instance of data set
            DataSet ds = new DataSet();
            // DataTable dt = new DataTable();

            adapter.SelectCommand = cmd;
            //fill the data set with data from database
            adapter.Fill(ds);

           
            if (ds.Tables[0].Rows.Count > 0) {
            //convert the dataset to a datatable
            DataTable dataTable = ds.Tables[0];
             

                foreach (DataRow row in dataTable.Rows)
                {
                    string acc = row["AccountNumber"].ToString();
                    string mob = row["Contact_Mobile"].ToString();
                    con = new SqlConnection(connection);
                    con.Open();
                    cmd = new SqlCommand("Trans_Save_SMSCharge", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Amount", charge.Amount);
                    cmd.Parameters.AddWithValue("@AccountNumber", acc);
                    cmd.Parameters.AddWithValue("@TransReference", charge.Naration);
                    cmd.Parameters.AddWithValue("@CreateBy", user);
                    cmd.Parameters.AddWithValue("@TransDate", DateTime.Now);

                    cmd.Parameters.Add("@RetCode", SqlDbType.VarChar, 50);
                    cmd.Parameters["@RetCode"].Direction = ParameterDirection.Output;
                    //execute query
                    cmd.ExecuteNonQuery();
                    //get the output parameters
                    string bal = (string)cmd.Parameters["@RetCode"].Value;
                    con.Close();

                    if (bal != "InsurficientFunds")
                    {
                    //get the output parameters
                    string accn = acc.Substring(0, 3);
                    string accno = acc.Substring(acc.Length - 3);
                    string domacc = accn + "XXX" + accno;

                    string mobile = mob.TrimStart(new char[] { '0' });
                    bool sms = charge.SMS;
                    if (sms == true)
                    {
                        string msg = "Debit:" + domacc + " Amt:NGN" + Convert.ToDecimal(charge.Amount).ToString("#,##00.00") + " Date:" + DateTime.Now + " Desc:" + charge.Naration + " Bal:NGN" + Convert.ToDecimal(bal).ToString("#,##00.00") + ". Thank you";
                        sendSMS("234" + mobile, msg);
                    }
                    }
                }
                msgs = "Posted";
               
            }

            return msgs;
        }

        public async void sendSMS(string dst, string Messages)
        {
            string userID = "55883602";
            string Password = "valentineS09";
            string destination = dst;
            string senderId = "JOYZONE";
            string message = Messages;
            string Url = "http://developers.cloudsms.com.ng/api.php?userid=" + userID + "&password=" + Password + "&type=5&destination=" + destination + "&sender=" + senderId + "&message=" + message;

            using (var httpClient = new HttpClient())
            {
                //httpClient.DefaultRequestHeaders.Add("x-lapo-eve-proc", middlekey);
                using (var response = await httpClient.GetAsync(Url))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string getRes = await response.Content.ReadAsStringAsync();

                    }
                    else
                    {

                    }

                }
            }
        }
    }
}


