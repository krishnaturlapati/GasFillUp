using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql.Data.Types;
using System.Configuration;
using System.Data;

namespace GasFillUp
{
    public partial class Dashboard : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
              

                if (!IsPostBack)
                {

                    BindVehicleDropDown();
                    //BindYearDropDown(ddlSelectYear.SelectedIndex);
                    
                }
         
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            
        }

      

        //private void BindYearDropDown(int SelectedVechile)
        //{
        //    ddlSelectYear.Items.Clear();
        //    DataSet ds = new DataSet();
        //    MySqlDataAdapter adp = new MySqlDataAdapter();
        //    try
        //    {
        //        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("usp_GetReceiptsYearForDashboard", conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
        //        cmd.Parameters.Add(new MySqlParameter("v_Vehicleid", SelectedVechile));
        //        adp.SelectCommand = cmd;
        //        adp.Fill(ds, "DistinctReceiptsYears");

        //        ddlSelectYear.DataSource = ds;

        //        ddlSelectYear.DataValueField = "ReceiptYear";
        //        ddlSelectYear.DataBind();
        //        if (ddlSelectYear.Items.Count > 0)
        //        {
        //            ddlSelectYear.Items.Insert(0, new ListItem("Select Year"));
        //            lblChartLabel.Visible = false;
        //            ddlSelectYear.Visible = true;
        //        }
        //        else
        //        {
        //            ddlSelectYear.Visible = false;
        //            lblChartLabel.Text = "No data";
        //        }

                
                




        //        conn.Dispose();
        //        adp.Dispose();
        //        ds.Dispose();


        //    }
        //    catch (Exception e)
        //    {

        //    }

        //}

        private void BindVehicleDropDown()
        {
            ddl_SelectVehicle.Items.Clear();
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter();
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_GetVehicleName", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));

                adp.SelectCommand = cmd;
                adp.Fill(ds, "VehicleNamesDropDown");

                ddl_SelectVehicle.DataSource = ds;
                ddl_SelectVehicle.DataTextField = "VehicleName";
                ddl_SelectVehicle.DataValueField = "VehicleId";
                ddl_SelectVehicle.DataBind();
                ddl_SelectVehicle.Items.Insert(0, new ListItem("Choose Vehicle"));

                if (ddl_SelectVehicle.Items.Count > 1)
                {
                    BindDashboardLabels(ddl_SelectVehicle.SelectedIndex);
                   
                }
                else
                {
                    Response.Redirect("Account\\Manage.aspx");
                }
                
                conn.Dispose();
                adp.Dispose();
                ds.Dispose();


            }
            catch (Exception e)
            {

            }
        }



        private void BindDashboardLabels(int SelectedCar)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString;
            DataSet dsTotalGasTrips = new DataSet();
            DataSet dsTotalGasCost = new DataSet();
            DataSet dsGasMileage = new DataSet();
            DataSet dsOdometer = new DataSet();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmdTotalGasTrips = new MySqlCommand("usp_GetTotalGasTrips", connection);
                MySqlCommand cmdTotalGasCost = new MySqlCommand("usp_GetTotalGasCost", connection);
                MySqlCommand cmdGasMileage = new MySqlCommand("usp_GetCarMileage", connection);
                MySqlCommand cmdOdometer = new MySqlCommand("usp_GetOdometerReading", connection);

                connection.Open();
                // cmdTotalGasTrips
                cmdTotalGasTrips.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTotalGasTrips.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmdTotalGasTrips.Parameters.Add(new MySqlParameter("v_Vehicleid", SelectedCar));
                using (MySqlDataReader dr = cmdTotalGasTrips.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dr.Read();
                        lblTotalGasTrips.Text = Convert.ToString(dr.GetInt32(0));
                    }
                }

                //cmdTotalGasCost
                cmdTotalGasCost.CommandType = System.Data.CommandType.StoredProcedure;
                cmdTotalGasCost.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmdTotalGasCost.Parameters.Add(new MySqlParameter("v_Vehicleid", SelectedCar));
                using (MySqlDataReader dr = cmdTotalGasCost.ExecuteReader())
                {
                    if (dr.HasRows)
                    {

                        dr.Read();
                        lblTotalGasExpense.Text = Convert.ToString(dr.GetDecimal(0));
                    
                    }
                }

                //cmdGasMileage
                cmdGasMileage.CommandType = System.Data.CommandType.StoredProcedure;
                cmdGasMileage.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmdGasMileage.Parameters.Add(new MySqlParameter("v_Vehicleid", SelectedCar));
                using (MySqlDataReader dr = cmdGasMileage.ExecuteReader())
                {
                    if (dr.HasRows)
                    {

                        dr.Read();
                        lblCarMileage.Text = Convert.ToString(dr.GetDecimal(0));

                    }
                }

                cmdOdometer.CommandType = System.Data.CommandType.StoredProcedure;
                cmdOdometer.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmdOdometer.Parameters.Add(new MySqlParameter("v_Vehicleid", SelectedCar));
                using (MySqlDataReader dr = cmdOdometer.ExecuteReader())
                {
                    if (dr.HasRows)
                    {

                        dr.Read();
                        lblOdometer.Text = Convert.ToString(dr.GetInt32(0));

                    }
                }


            }
        }

        public string VehicleName
        {
            get;
            set;
        }
        public string chartData
        {
            get;
            set;
        }

       


        public void renderChart(int selectedYear)
        {
            DataTable dt = GetData(selectedYear); //Assuming that GetData already populating with data as datatable   
            string _VehicleName = Convert.ToString(Session["SelectedVehicleName"]);
            List<decimal> _data = new List<decimal>();
            foreach (DataRow row in dt.Rows)
            {
                _data.Add((decimal)row["TotalGas"]);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            chartData = jss.Serialize(_data); //this make your list in jSON format like [88,99,10]
            VehicleName = jss.Serialize(_VehicleName);



        }

        public DataTable GetData(int selectedyear)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString;
            DataTable dt = new DataTable();


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("usp_GetFinalToalByMonth", connection);
                connection.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_Vehicleid", ddl_SelectVehicle.SelectedIndex));
                cmd.Parameters.Add(new MySqlParameter("v_ReceiptYear", selectedyear));
                using(MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }
                }

            }



            return dt;
        }

  

        protected void ddl_SelectVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbReceiptYear.Text = "";
            if (ddl_SelectVehicle.SelectedIndex == 0)
            {
                lblCarMileage.Text = "0";
                lblOdometer.Text = "0";
                lblTotalGasExpense.Text = "0";
                lblTotalGasTrips.Text = "0";
                
                System.Web.HttpContext.Current.Response.Write("<script language=\"JavaScript\">alert(\"Please select vehicle\")</script>");
                btnGetGraph.Enabled = false;
                
            }
            else
            {
            Session["SelectedVehicleId"] = ddl_SelectVehicle.SelectedIndex;
            Session["SelectedVehicleName"] = ddl_SelectVehicle.SelectedItem.Text;
            
            BindDashboardLabels(ddl_SelectVehicle.SelectedIndex);
            }
             

        }

        protected void btnGetGraph_Click(object sender, EventArgs e)
        {
            if (tbReceiptYear.Text == "")
                tbReceiptYear.Text = "0";


            renderChart(Convert.ToInt32(tbReceiptYear.Text));
        }
        

    }
}