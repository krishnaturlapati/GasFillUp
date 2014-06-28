using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql.Data.Types;
using System.Configuration;
using System.Data;


namespace GasFillUp
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsPostBack)
                {

                    BindVehicleDropDown();
                    BindGridReceipts();
                    if (Session["SelectedVehicleId"] == null)
                    {
                        lblReceiptHeader.Text = "Select vehicle! ";
                        NoReceiptsFlag.Visible = false;
                        

                    }
                    else
                    {
                        lblReceiptHeader.Text = "Recent Fill up's for:" + " " + Session["SelectedVehicleName"];
                        lblVehicleName.Text = Convert.ToString(Session["SelectedVehicleName"]);
                    }

                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

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

                if (ddl_SelectVehicle.Items.Count <2)
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

        protected void ddl_SelectVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["SelectedVehicleId"] = ddl_SelectVehicle.SelectedIndex;
            Session["SelectedVehicleName"] = ddl_SelectVehicle.SelectedItem.Text;
            //lblVehicleName.Text = ddl_SelectVehicle.SelectedItem.Text;
            Response.Redirect("Receipts.aspx");
        }

        private void BindGridReceipts()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter();
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_GetReceiptsInfo", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_Vehicleid", Session["SelectedVehicleId"]));// ddlChooseVehicle.SelectedIndex));

                adp.SelectCommand = cmd;
                adp.Fill(ds, "ReceiptsList");

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                if (ds.Tables[0].Rows.Count == 0)
                {
                    NoReceiptsFlag.Text = "No Receipts Added";

                }
                else
                {
                    Grid_Receipts.DataSource = ds;
                    Grid_Receipts.DataBind();
                    NoReceiptsFlag.Visible = false;
                }


                conn.Close();
                conn.Dispose();
                adp.Dispose();
                ds.Dispose();




            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
            }
        
        }

        // Used for hiding the primary key of the table
        protected void Grid_Receipts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;

            }
            catch (Exception a)
            {

            }
        }


        private void ClearForm()
        {
            // set dafaults 

            tbReceiptDate.Text = "";
            tbOdometer.Text = "";
            tbPricePerGallon.Text = "";
            tbTotalGas.Text = "";
            tbFinalTotal.Text = "";
            tbGasStationName.Text = "";
        }
        protected void btnClearReceiptForm_Click(object sender, EventArgs e)
        {
            // set dafaults 

            tbReceiptDate.Text = "";
            tbOdometer.Text = "";
            tbPricePerGallon.Text = "";
            tbTotalGas.Text = "";
            tbFinalTotal.Text = "";
            tbGasStationName.Text = "";
        }

        protected void btnSaveGasReceipt_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {

                    // set dafaults 
                    if (tbReceiptDate.Text == "")
                        tbReceiptDate.Text = Convert.ToString(System.DateTime.Now);

                    if (tbOdometer.Text == "")
                        tbOdometer.Text = "0";

                    if (tbPricePerGallon.Text == "")
                        tbPricePerGallon.Text = "00.00";

                    if (tbTotalGas.Text == "")
                        tbTotalGas.Text = "0.00";

                    if (tbFinalTotal.Text == "")
                        tbFinalTotal.Text = "0.00";

                    if (tbGasStationName.Text == "")
                        tbGasStationName.Text = "No Name";

                    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("usp_InsertReceiptInfo;", conn);



                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                    cmd.Parameters.Add(new MySqlParameter("v_Vehicleid",   Session["SelectedVehicleId"]));
                    cmd.Parameters.Add(new MySqlParameter("v_ReceiptDate", Convert.ToDateTime(tbReceiptDate.Text)));
                    cmd.Parameters.Add(new MySqlParameter("v_Odometer", Convert.ToInt32(tbOdometer.Text)));
                    cmd.Parameters.Add(new MySqlParameter("v_PricePerGallon", Convert.ToDecimal(tbPricePerGallon.Text)));
                    cmd.Parameters.Add(new MySqlParameter("v_TotalGas", Convert.ToDecimal(tbTotalGas.Text)));
                    cmd.Parameters.Add(new MySqlParameter("v_FinalTotal", Convert.ToDecimal(tbFinalTotal.Text)));
                    cmd.Parameters.Add(new MySqlParameter("v_GasStationName", tbGasStationName.Text));


                    cmd.Connection = conn;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    BindGridReceipts();
                    ClearForm();
                }
                catch (Exception e1)
                {
                    Response.Write(e1.Message);
                }

            }

        }

        protected void Grid_Receipts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

              
                string ReceiptId = Grid_Receipts.Rows[e.RowIndex].Cells[1].Text;

                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_DeleteReceiptInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleId", Session["SelectedVehicleId"]));
                cmd.Parameters.Add(new MySqlParameter("v_ReceiptId", Convert.ToInt32(ReceiptId)));

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                BindGridReceipts();
                Response.Redirect("Receipts.aspx");
            }
            catch (Exception e1)
            {
            }
        }

        protected void Grid_Receipts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Receipts.PageIndex = e.NewPageIndex;
            BindGridReceipts();
        }

        protected void Grid_Receipts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Grid_Receipts.EditIndex = -1;
            BindGridReceipts();
        }

        protected void Grid_Receipts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Grid_Receipts.EditIndex = e.NewEditIndex;
            
            BindGridReceipts();
        }

        protected void Grid_Receipts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                GridViewRow row = Grid_Receipts.Rows[e.RowIndex];
                
                int ReceiptId = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
                DateTime ReceiptDate = Convert.ToDateTime(((TextBox)(row.Cells[2].Controls[0])).Text);
                string GasStationName = Convert.ToString(((TextBox)(row.Cells[3].Controls[0])).Text);
                double FinalTotal = Convert.ToDouble(((TextBox)(row.Cells[4].Controls[0])).Text);
                double TotalGas = Convert.ToDouble(((TextBox)(row.Cells[5].Controls[0])).Text);
                int Odometer = Convert.ToInt32(((TextBox)(row.Cells[6].Controls[0])).Text); 
                double PricePerGallon = Convert.ToDouble(((TextBox)(row.Cells[7].Controls[0])).Text);
                



                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();


                MySqlCommand cmd = new MySqlCommand("usp_UpdateReceiptInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_ReceiptId", ReceiptId));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleId", Session["SelectedVehicleId"]));
                cmd.Parameters.Add(new MySqlParameter("v_ReceiptDate", ReceiptDate));
                cmd.Parameters.Add(new MySqlParameter("v_GasStationName", GasStationName));
                cmd.Parameters.Add(new MySqlParameter("v_FinalTotal", FinalTotal));
                cmd.Parameters.Add(new MySqlParameter("v_TotalGas", TotalGas));
                cmd.Parameters.Add(new MySqlParameter("v_Odometer", Odometer));
                cmd.Parameters.Add(new MySqlParameter("v_PricePerGallon", PricePerGallon));
                
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                Grid_Receipts.EditIndex = -1;
                BindGridReceipts();

            }
            catch (Exception e1)
            {
            }
        }
    }
}