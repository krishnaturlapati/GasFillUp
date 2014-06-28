<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Receipts.aspx.cs" Inherits="GasFillUp.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h2></h2>
        <div class="col-md-3 form-group">
            <asp:DropDownList runat="server" AutoPostBack="True" ID="ddl_SelectVehicle" OnSelectedIndexChanged="ddl_SelectVehicle_SelectedIndexChanged" CssClass="selectpicker"></asp:DropDownList>

        </div>
    </div>
    <div class="row">
        <div class="col-md-3 form-group">
            <h4>
                <asp:Label ID="lblReceiptHeader" runat="server"></asp:Label>
            </h4>
        </div>


    </div>
    <div class="row">
        <div class="col-md-3 form-group">
            <asp:Label ID="NoReceiptsFlag" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">

        <div class="panel-body">
            <div class="table-responsive">
                <asp:UpdatePanel runat="server" ID="UP_GridView" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:GridView
                            ID="Grid_Receipts"
                            EmptyDataText="No Receipt Info"
                            runat="server"
                            AllowPaging="True"
                            AllowSortig="true"
                            CssClass="table table-bordered table-hover table-striped tablesorter"
                            AutoGenerateColumns="False"
                            OnRowDeleting="Grid_Receipts_RowDeleting"
                            OnRowDataBound="Grid_Receipts_RowDataBound"
                            OnRowCancelingEdit="Grid_Receipts_RowCancelingEdit"
                            OnPageIndexChanging="Grid_Receipts_PageIndexChanging" OnRowUpdating="Grid_Receipts_RowUpdating" OnRowEditing="Grid_Receipts_RowEditing">



                            <Columns>
                                <asp:CommandField ShowEditButton="True" ShowDeleteButton="true" ShowCancelButton="true" />


                                <asp:BoundField DataField="ReceiptID" HeaderStyle-CssClass="fa fa-sort" ItemStyle-CssClass="hideGridColumn" HeaderText="RID"></asp:BoundField>
                                <asp:BoundField DataField="ReceiptDate" DataFormatString="{0:d}" HeaderStyle-CssClass="fa fa-sort" HeaderText="Fill Up Date"></asp:BoundField>
                                <asp:BoundField DataField="GasStationName" HeaderStyle-CssClass="fa fa-sort" HeaderText="Gas Station"></asp:BoundField>
                                <asp:BoundField DataField="FinalTotal" DataFormatString="{0:C}" HeaderStyle-CssClass="fa fa-sort" HeaderText="Total Amount"></asp:BoundField>
                                <asp:BoundField DataField="TotalGas" HeaderStyle-CssClass="fa fa-sort" HeaderText="Total Gas"></asp:BoundField>
                                <asp:BoundField DataField="Odometer" HeaderStyle-CssClass="fa fa-sort" HeaderText="Odometer"></asp:BoundField>
                                <asp:BoundField DataField="PricePerGallon" DataFormatString="{0:C}" HeaderStyle-CssClass="fa fa-sort" HeaderText="$/G"></asp:BoundField>

                            </Columns>

                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                    <h4 class="modal-title" id="ReceiptsModal">Enter Receipt Info for Vehicle:
                        <asp:Label ID="lblVehicleName" runat="server"></asp:Label></h4>
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



    <script src="scripts/bootstrap-select.js"></script>
    <link href="Content/bootstrap-select.css" rel="stylesheet" />


    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

    <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
    <script type="text/javascript">

        $(document).ready(function () {

            $(".selectpicker").selectpicker();
            $(".datepicker").datepicker();

        });

    </script>

</asp:Content>
