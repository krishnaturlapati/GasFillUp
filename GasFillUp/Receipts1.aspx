<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receipts1.aspx.cs" Inherits="GasFillUp.Receipts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <h2></h2>
        <div class="col-md-3 form-group">
            <asp:DropDownList runat="server" ID="ddlChooseVehicle1" OnSelectedIndexChanged="ddlChooseVehicle1_SelectedIndexChanged" CssClass="selectpicker"></asp:DropDownList>
        </div>
       
    </div>


        <div class="row">
 
            <h3>Recent Fill Ups</h3> 
            <br />
            <asp:Label ID="NoReceiptsFlag" runat="server"></asp:Label>
            <br />
            <br />  
            <div class="panel-body">
                <div class="table-responsive">

                    <asp:GridView 
                        ID="GridReceipts" 
                        EmptyDataText="No Receipt Info" 
                        runat="server" 
                        CssClass="table table-bordered table-hover table-striped tablesorter" 
                        AllowPaging="True" 
                        AllowSortig="true" 
                        AutoGenerateColumns="False" 
                        OnPageIndexChanging="GridReceipts_PageIndexChanging"
                        OnRowDataBound="GridReceipts_RowDataBound" OnRowDeleting="GridReceipts_RowDeleting" OnRowCancelingEdit="GridReceipts_RowCancelingEdit" OnRowEditing="GridReceipts_RowEditing"
                         >
                        <Columns>
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="true"  ShowCancelButton="true" />

                        
                            <asp:BoundField DataField="ReceiptID" ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="RID" ShowHeader="true">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="VehicleID" ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="VID" ShowHeader="true">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>


                            <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:d}" ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="Fill Up Date" ShowHeader="true">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="GasStationName"  ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="Gas Station">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>
                             <asp:BoundField DataField="FinalTotal" DataFormatString="{0:C}" ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="Total Amount">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle></asp:BoundField>
                               <asp:BoundField DataField="TotalGas"  ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="Total Gas">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                                   </asp:BoundField>
                                  <asp:BoundField DataField="Odometer"  ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="Odometer">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>
                                  <asp:BoundField DataField="PricePerGallon" DataFormatString="{0:C}"  ReadOnly="true" HeaderStyle-CssClass="fa fa-sort" HeaderText="$/G">
<HeaderStyle CssClass="fa fa-sort"></HeaderStyle>
                            </asp:BoundField>

                        </Columns>
                        
                    </asp:GridView>
              
                </div>
                  <div class="col-md-2 form-group">
            <button class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Enter Receipt Info</button>
        </div>  
              </div>
      
          </div>



     <!--Add Receipts Modal-->
    <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="ReceiptsModal">Enter Receipt Info for Vehicle: <asp:Label ID="lblVehicleName" runat="server"></asp:Label></h4>
                </div>
                <div class="form-horizontal">
                    <h2></h2>

                    
                    <div class="form-group">
                       
                        <div class="col-md-10">
                             <asp:TextBox runat="server" ID="tbReceiptDate" CssClass="col-md-offset-1 datepicker form-control" placeholder="Receipt Date" />
                        </div>
                    </div>
                    <br />

                    <div class="col-md-offset-2 form-group">
                     
                        <div class="col-md-10">
                               <asp:TextBox runat="server" ID="tbOdometer" CssClass="col-md-offset-1 form-control" placeholder="Odometer" />

                        </div>
                    </div>

                    <br />
                    <div class="col-md-offset-2 form-group">
                        
                        <div class="col-md-10">
                             <asp:TextBox runat="server" ID="tbGasStationName" CssClass="col-md-offset-1 form-control" placeholder="Gas Station Name" />
                        </div>
                    </div>
                    <br />

                    <div class="col-md-offset-2 form-group">
                        
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbPricePerGallon" CssClass="col-md-offset-1 form-control" placeholder="$/Gallon" />
                        </div>
                    </div>
                    <br />
                    <div class="col-md-offset-2 form-group">
                        
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="tbTotalGas" CssClass="col-md-offset-1 form-control" placeholder="Total Gas" />
                        </div>
                    </div>
                    <br />
                    <div class="col-md-offset-2 form-group">
                        
                        <div class="col-md-10">
                             <asp:TextBox runat="server" ID="tbFinalTotal" CssClass="col-md-offset-1 form-control" placeholder="Final Total" />
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <!-- <asp:Button runat="server" Text="Close" class="btn btn-default" data-dismiss="modal" /> -->
                         <%-- <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                             <asp:Button runat="server" ID="btnClearReceiptForm" OnClick="btnClearReceiptForm_Click" class="btn btn-default" Text="Clear" />
                
                            <asp:Button runat="server" ID="btnSaveGasReceipt" OnClick="btnSaveGasReceipt_Click" Text="Save changes" CssClass="btn btn-primary" />

                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>


 

<%--    <asp:Panel ID="ReceiptForm" runat="server">
    
         <div class="row">
        <h3><asp:Label runat="server" ID="lblReceiptFormTitle" Text="Enter Receipt Info"></asp:Label></h3>
       <hr />
            
        </div>
<div class="row">
 <%-- <div  class="col-md-offset-0 col-md-3 form-group">
           
    </div>
         </div>
    <div class="col-md-offset-0 col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbReceiptDate" CssClass=" datepicker form-control" placeholder="Receipt Date" />
    </div>

    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbOdometer" CssClass="form-control" placeholder="Odometer" />
    </div>
    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbGasStationName" CssClass="form-control" placeholder="Gas Station Name" />
    </div>

    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbPricePerGallon" CssClass="form-control" placeholder="$/Gallon" />
    </div>
    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbTotalGas" CssClass="form-control" placeholder="Total Gas" />
    </div>
    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbFuelTotal" CssClass="form-control" placeholder="Fuel Total" />
    </div>
    
    <div class="col-md-3 form-group">
        <asp:TextBox runat="server" ID="tbTax" CssClass="form-control" placeholder="Tax" />
    </div>
    
    <div class="col-md-3 form-group">
        
        <asp:TextBox runat="server" ID="tbFinalTotal" CssClass="form-control" placeholder="Final Total" />
    </div>
    <%--    <span class="clearfix">--%>


<%--    <div class="row">
        <h2></h2>

        <div class="form-group">
            <div class="col-md-offset-5 col-md-4">
                <asp:Button runat="server" ID="btnClearReceiptForm" OnClick="btnClearReceiptForm_Click" class="btn btn-default" Text="Clear" />
                <asp:Button runat="server" ID="btnSaveGasReceipt" OnClick="btnSaveGasReceipt_Click" Text="Save changes" CssClass="btn btn-primary" />

            </div>
        </div>

        
    </div>
     
    </asp:Panel>--%>


   <script src="scripts/bootstrap-select.js"></script>
    <link href="Content/bootstrap-select.css" rel="stylesheet" />


    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">

        $(document).ready(function () {

            $(".selectpicker").selectpicker();
            $(".datepicker").datepicker();

        });

</script>
    </asp:Content>
