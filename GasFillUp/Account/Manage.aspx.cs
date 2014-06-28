using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using GasFillUp.Models;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql.Data.Types;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;


namespace GasFillUp.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected string SuccessMessage
        {
            get;
            private set;
        }

        protected bool CanRemoveExternalLogins
        {
            get;
            private set;
        }

        private bool HasPassword(UserManager manager)
        {
            var user = manager.FindById(User.Identity.GetUserId());
            return (user != null && user.PasswordHash != null);
        }

        protected void Page_Load()
        {
            if (User.Identity.IsAuthenticated)
            {
               
                if(!IsPostBack)
                { 
                    BindVehiclesGrid();

                    // Determine the sections to render
                    UserManager manager = new UserManager();
                    if (HasPassword(manager))
                    {
                        changePasswordHolder.Visible = true;
                    }
                    else
                    {
                        setPassword.Visible = true;
                        changePasswordHolder.Visible = false;
                    }
                    CanRemoveExternalLogins = manager.GetLogins(User.Identity.GetUserId()).Count() > 1;

                    // Render success message
                    var message = Request.QueryString["m"];
                    if (message != null)
                    {
                        // Strip the query string from action
                        Form.Action = ResolveUrl("~/Account/Manage");

                        SuccessMessage =
                            message == "ChangePwdSuccess" ? "Your password has been changed."
                            : message == "SetPwdSuccess" ? "Your password has been set."
                            : message == "RemoveLoginSuccess" ? "The account was removed."
                            : String.Empty;
                        successMessage.Visible = !String.IsNullOrEmpty(SuccessMessage);
                    }

                }
                

                
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
           
        }


     

        private void BindVehiclesGrid()
        {
           
            //bind datagridview to binding source

            DataSet ds = new DataSet();
            MySqlDataAdapter adp = new MySqlDataAdapter();
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_GetVehicleList", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));

                adp.SelectCommand = cmd;
                adp.Fill(ds, "VehiclesList");

                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                if (ds.Tables[0].Rows.Count ==0)
                {
                    NoDataFlag.Text = "No Vehicles Added";
                }
                else
                {
                    GrdVehicles.DataSource = ds;
                    GrdVehicles.DataBind();
                    NoDataFlag.Visible = false;
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




        



 

    

        protected void SaveVehcileDetails_Click(object sender, EventArgs e)
        {
            try
            {
                // set dafaults 
                if (TbVehcileName.Text == "")
                    TbVehcileName.Text = "Default";

                if (tbYear.Text == "")
                    tbYear.Text = "0000";

                if (tbManufacturer.Text == "")
                    tbManufacturer.Text = "Default";

                if (tbMake.Text == "")
                    tbMake.Text = "Default";


                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_InsertVehicleInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleName", TbVehcileName.Text));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleManufacturer", tbManufacturer.Text));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleModel", tbMake.Text));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleYear", tbYear.Text));
                
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                ClearForm();
                BindVehiclesGrid();

            }
            catch (Exception e1)
            {
                Response.Write(e1.Message);
            }

        }

        private void ClearForm()
        {
            // set dafaults 
            TbVehcileName.Text = "";
            tbYear.Text = "";
            tbManufacturer.Text = "";
            tbMake.Text = "";
        }

        public IEnumerable<UserLoginInfo> GetLogins()
        {
            UserManager manager = new UserManager();
            var accounts = manager.GetLogins(User.Identity.GetUserId());
            CanRemoveExternalLogins = accounts.Count() > 1 || HasPassword(manager);
            return accounts;
        }

        public void RemoveLogin(string loginProvider, string providerKey)
        {
            UserManager manager = new UserManager();
            var result = manager.RemoveLogin(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            var msg = result.Succeeded
                ? "?m=RemoveLoginSuccess"
                : String.Empty;
            Response.Redirect("~/Account/Manage" + msg);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                UserManager manager = new UserManager();
                IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), CurrentPassword.Text, NewPassword.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/Manage?m=ChangePwdSuccess");
                }
                else
                {
                    AddErrors(result);
                }
            }
        }




        protected void SetPassword_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Create the local login info and link the local account to the user
                UserManager manager = new UserManager();
                IdentityResult result = manager.AddPassword(User.Identity.GetUserId(), password.Text);
                if (result.Succeeded)
                {
                    Response.Redirect("~/Account/Manage?m=SetPwdSuccess");
                }
                else
                {
                    AddErrors(result);
                }
            }
        }

    

        protected void GrdVehicles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                GridViewRow row = GrdVehicles.Rows[e.RowIndex];
                

                string VehicleName = ((TextBox)(row.Cells[1].Controls[0])).Text;
                string VehicleModel = ((TextBox)(row.Cells[2].Controls[0])).Text;
                string VehicleManufacturer = ((TextBox)(row.Cells[3].Controls[0])).Text;
                int VehicleYear = Convert.ToInt32(((TextBox)(row.Cells[4].Controls[0])).Text);
                int VehicleId = Convert.ToInt32(((TextBox)(row.Cells[5].Controls[0])).Text);

                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_UpdateVehicleInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_VehicleName", VehicleName));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleManufacturer", VehicleManufacturer));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleModel", VehicleModel));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleYear", VehicleYear));
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleId", Convert.ToInt32(VehicleId)));
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                GrdVehicles.EditIndex = -1;
                BindVehiclesGrid();

            }
            catch (Exception e1)
            {
            }
        }

        protected void GrdVehicles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GrdVehicles.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindVehiclesGrid();

        }

        protected void GrdVehicles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GrdVehicles.EditIndex = -1;
            BindVehiclesGrid();
        }

        protected void GrdVehicles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[5].Visible = false;
        }

        protected void GrdVehicles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                string VehicleId = GrdVehicles.Rows[e.RowIndex].Cells[5].Text;

                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_DeleteVehicleInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Parameters.Add(new MySqlParameter("v_VehicleId", Convert.ToInt32(VehicleId)));
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                GrdVehicles.EditIndex = -1;
                Response.Redirect("Manage.aspx");
 
            }
            catch (Exception e1)
            {
            }

        }

        protected void BtnDeactivateUser_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["GasFillUpConn"].ConnectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("usp_DeleteVehicleInfo;", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("v_UserName", User.Identity.Name));
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                Response.Redirect("Dashboard.aspx");
            }
            catch
            {

            }
        }

    }
}