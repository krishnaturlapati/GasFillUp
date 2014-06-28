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
    public partial class Receipts : System.Web.UI.Page
    {
        int ddlSelectedVehicle = 0;
                
        protected void Page_Load(object sender, EventArgs e)
        {
         

            if (User.Identity.IsAuthenticated)
            {

                
                    BindVehicleNameDropDown();
                    BindGridReceipts();
                    

                
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

            
        }



        private void BindVehicleNameDropDown()
        {

            ddlChooseVehicle1.Items.Clear();
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

                ddlChooseVehicle1.DataSource = ds;
                ddlChooseVehicle1.DataTextField = "VehicleName";
                ddlChooseVehicle1.DataValueField = "VehicleId";
                ddlChooseVehicle1.DataBind();
                ddlChooseVehicle1.Items.Insert(0, new ListItem("Choose Vehicle"));
                lblVehicleName.Text = ddlChooseVehicle1.DataTextField;
                
                conn.Dispose();
                adp.Dispose();
                ds.Dispose();


            }
            catch (Exception e)
            {

            }
        }



        //private void GetSatesDropDown()
        //{

        //    //ddl_states.Items.Add(new ListItem("New Jersey"));
        //    //ddl_states.Items.Add(new ListItem("New York"));

        //    //ddl_states.Items.Clear();
        //    DataSet ds = new DataSet();
        //    MySqlDataAdapter adp = new MySqlDataAdapter();
        //    try
        //    {
        //        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
        //        conn.Open();
        //        MySqlCommand cmd = new MySqlCommand("usp_GetStateId;", conn);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        adp.SelectCommand = cmd;
        //        adp.Fill(ds, "StatesDropDown");

        //        ddl_states.Items.Add(new ListItem("--Select--"));

        //        ddl_states.DataSource = ds;
        //        ddl_states.DataTextField = "StateName";
        //        ddl_states.DataValueField = "Stateid";
        //        ddl_states.DataBind();
        //        conn.Dispose();
        //        adp.Dispose();
        //        ds.Dispose();
        //    }
        //    catch
        //    {

        //    }
        //}


        //}

       
      
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
                    GridReceipts.DataSource = ds;
                    GridReceipts.DataBind();
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
                    //cmd.Parameters.Add(new MySqlParameter("v_Vehicleid", ddlChooseVehicle.SelectedItem.Value)); //ddlSelectedVehicle)); //ddlChooseVehicle.SelectedItem.Value));
                    cmd.Parameters.Add(new MySqlParameter("v_Vehicleid", Session["SelectedVehicleId"])); 
                    
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
                    GridReceipts.EditIndex = -1;
                    BindGridReceipts();
                    ClearForm();
                }
                catch (Exception e1)
                {
                    Response.Write(e1.Message);
                }

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

        protected void ddlChooseVehicle1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblVehicleName.Text = ddlChooseVehicle1.SelectedValue;
            Session["SelectedVehicleId"] = ddlChooseVehicle1.SelectedIndex;
            BindGridReceipts();
        }

      
        protected void GridReceipts_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridReceipts.PageIndex = e.NewPageIndex;
            BindGridReceipts();
        }

        // Used for hiding the primary key of the table
        protected void GridReceipts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }

        protected void GridReceipts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridReceipts.EditIndex = -1;
            BindGridReceipts();

        }

        protected void GridReceipts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridReceipts.EditIndex = e.NewEditIndex;
            BindGridReceipts();
        }

        protected void GridReceipts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string VehicleId = GridReceipts.Rows[e.RowIndex].Cells[2].Text;
                string ReceiptId = GridReceipts.Rows[e.RowIndex].Cells[1].Text;

                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_DeleteReceiptInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleId", Convert.ToInt32(VehicleId)));
                cmd.Parameters.Add(new MySqlParameter("v_ReceiptId", Convert.ToInt32(ReceiptId)));
                
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                BindGridReceipts();

            }
            catch (Exception e1)
            {
            }
            
        }

       
    }
}