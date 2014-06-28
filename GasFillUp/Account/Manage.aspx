<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="GasFillUp.Account.Manage" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">




    

        <h2></h2>
    <h2>Vehicles</h2>

         <div class="panel-body">
             <asp:Label ID="NoDataFlag" runat="server"></asp:Label>
             <br />
             <br />   
             <div class="table-responsive">
                    
                  <asp:UpdatePanel runat="server" ID ="UP_GridView" UpdateMode="Always">
                      <ContentTemplate>
                    <asp:GridView ID="GrdVehicles" runat="server" 
                        ShowHeaderWhenEmpty="false" 
                        OnRowDataBound="GrdVehicles_RowDataBound" 
                        OnRowCancelingEdit="GrdVehicles_RowCancelingEdit" 
                        OnRowDeleting="GrdVehicles_RowDeleting" 
                        OnRowEditing="GrdVehicles_RowEditing" 
                        OnRowUpdating="GrdVehicles_RowUpdating" 
                        EmptyDataText="No Vehicles Added" 
                        CssClass="table table-bordered table-hover table-striped tablesorter"
                        AutoGenerateColumns="False">
                        
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true"  ShowCancelButton="true" />
                            <asp:BoundField DataField="VehicleName" HeaderStyle-CssClass="fa fa-sort" HeaderText="Name">
                           
                            </asp:BoundField>
                            <asp:BoundField DataField="VehicleModel" HeaderStyle-CssClass="fa fa-sort" HeaderText="Model">
                                
                            </asp:BoundField>
                            <asp:BoundField DataField="VehicleManufacturer" HeaderStyle-CssClass="fa fa-sort" HeaderText="Manufacturer">
                                
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="VehicleYear" HeaderStyle-CssClass="fa fa-sort" HeaderText="Year">
                                
                            </asp:BoundField>

                       
                          
                            <asp:BoundField DataField="Vehicleid" HeaderStyle-CssClass="fa fa-sort" HeaderText="Id">
                             
                            </asp:BoundField>

                        </Columns>

                    </asp:GridView>
                          </ContentTemplate>
</asp:UpdatePanel>
                </div>
                
        <div class="col-md-2 form-group">
            <button class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Add Vehicle</button>
        </div>          </div>

              
         <hr /> 

    <h2><%: Title %></h2>

    <div>
        <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>

    </div>

    <div class="row">
        <div class="col-md-12">
            <section id="passwordForm">
                <asp:PlaceHolder runat="server" ID="setPassword" Visible="false">
                    <p>
                        You do not have a local password for this site. Add a local
                        password so you can log in without an external login.
                    </p>
                    <div class="form-horizontal">
                        <h4>Set Password Form</h4>
                        <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                        <hr />
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="password" CssClass="col-md-2 control-label">Password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="password" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="password"
                                    CssClass="text-danger" ErrorMessage="The password field is required."
                                    Display="Dynamic" ValidationGroup="SetPassword" />
                                <asp:ModelErrorMessage runat="server" ModelStateKey="NewPassword" AssociatedControlID="password"
                                    CssClass="text-error" SetFocusOnError="true" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="confirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="confirmPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="confirmPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required."
                                    ValidationGroup="SetPassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="confirmPassword"
                                    CssClass="text-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match."
                                    ValidationGroup="SetPassword" />

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" Text="Set Password" ValidationGroup="SetPassword" OnClick="SetPassword_Click" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>

                <asp:PlaceHolder runat="server" ID="changePasswordHolder" Visible="false">
                    <p>You're logged in as <strong><%: User.Identity.GetUserName() %></strong>.</p>
                    <div class="form-horizontal">
                        <h4>Change Password Form</h4>
                        <hr />
                        <asp:ValidationSummary runat="server" ShowModelStateErrors="true" CssClass="text-danger" />
                        <div class="form-group">
                            <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword" CssClass="col-md-2 control-label">Current password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="CurrentPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="text-danger" ErrorMessage="The current password field is required."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword" CssClass="col-md-2 control-label">New password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="NewPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                    CssClass="text-danger" ErrorMessage="The new password is required."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword" CssClass="col-md-2 control-label">Confirm new password</asp:Label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" TextMode="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="Confirm new password is required."
                                    ValidationGroup="ChangePassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The new password and confirmation password do not match."
                                    ValidationGroup="ChangePassword" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <asp:Button runat="server" Text="Change Password" ValidationGroup="ChangePassword" OnClick="ChangePassword_Click" CssClass="btn btn-default" />
                            </div>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </section>

            <section id="externalLoginsForm">

                <asp:ListView runat="server"
                    ItemType="Microsoft.AspNet.Identity.UserLoginInfo"
                    SelectMethod="GetLogins" DeleteMethod="RemoveLogin" DataKeyNames="LoginProvider,ProviderKey">

                    <LayoutTemplate>
                        <h4>Registered Logins</h4>
                        <table class="table">
                            <tbody>
                                <tr runat="server" id="itemPlaceholder"></tr>
                            </tbody>
                        </table>

                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%#: Item.LoginProvider %></td>
                            <td>
                                <asp:Button runat="server" Text="Remove" CommandName="Delete" CausesValidation="false"
                                    ToolTip='<%# "Remove this " + Item.LoginProvider + " login from your account" %>'
                                    Visible="<%# CanRemoveExternalLogins %>" CssClass="btn btn-default" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>

                <uc:OpenAuthProviders runat="server" ReturnUrl="~/Account/Manage" />
            </section>
            <h2></h2>
            <section id="DeactivateUserSection">
                <h2>Deactivate</h2>
                        <hr />

                <p>
       If you have any questions <a href="GasFillUp@gmail.com">email me</a>
    </p>
                
                <asp:Button ID="BtnDeactivateUser" runat="server" CssClass="btn btn-danger" 
                     Text="Deactivate Account" OnClick="BtnDeactivateUser_Click" />
            </section>



           
        </div>
    </div>
    
   <!--Add Vehicle Modal-->
    <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="VechicleModal">Add Vehicle</h4>
                </div>
                <div class="form-horizontal ">
                    <h2></h2>

                    <div class="form-group">
                       
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="TbVehcileName" CssClass="col-md-offset-1 form-control" placeholder="Vehicle Name (Ex:Black Mamba)" />
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                     
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbMake" CssClass="col-md-offset-1 form-control" placeholder="Make (Ex:Corolla)" />

                        </div>
                    </div>

                    <br />
                    <div class="form-group">
                        
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbManufacturer" CssClass="col-md-offset-1 form-control" placeholder="Manufacturer (Ex:Toyota)" />
                        </div>
                    </div>
                    <br />



                    <div class="form-group">
                        
                        <div class="col-md-10">
                           
                           
                             <asp:TextBox runat="server" ID="tbYear" CssClass="col-md-offset-1 form-control" placeholder="Year (Ex:1990)" />
                            
                               
                                
                               </div>
                            
                        </div>
                    
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <!-- <asp:Button runat="server" Text="Close" class="btn btn-default" data-dismiss="modal" /> -->
                          <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <asp:Button runat="server" Text="Save changes" CssClass="btn btn-primary" OnClick="SaveVehcileDetails_Click" />

                        </div>

                    </div>
                </div>

            </div>
        </div>
   
        </div>
</asp:Content>
