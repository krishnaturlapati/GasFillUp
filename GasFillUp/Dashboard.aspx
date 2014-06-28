<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="GasFillUp.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       
    

    <script type="text/javascript">
        
        $(function () {  
            $('#container').highcharts({  
                chart: {  
                    type: 'line' 
                },  
                title: {  
                    text: 'Total Monthly Gas Cost' 
                },  
                xAxis: {  
                    categories: [  
                      'Jan',  
                      'Feb',  
                      'Mar',  
                      'Apr',  
                      'May',  
                      'Jun',  
                      'Jul',  
                      'Aug',  
                      'Sep',  
                      'Oct',  
                      'Nov',  
                      'Dec'  
                    ]  
                },  
                yAxis: {  
                    min: 0,  
                    title: {  
                        text: 'Cost ($)'  
                    }  
                },  
                tooltip: {  
                    headerFormat: '<span style="font-size:10px">{point.key}</span><table>',  
                    pointFormat: '<tr><td style="color:{series.color};padding:0">' +  
                      '<td style="padding:0"><b>${point.y:.2f} </b></td></tr>',  
                    footerFormat: '</table>',  
                    shared: true,  
                    useHTML: true  
                },  
                plotOptions: {  
                    column: {  
                        pointPadding: 0.2,  
                        borderWidth: 0  
                    }  
                },  
                series: [{  
                    name: <%= VehicleName %>, 
                    data: <%= chartData %>  
                    }]  
            });  
        });  
 </script>

  





<%--    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
            
                            <div class="row">
                <h2></h2>
                <div class="col-md-3 form-group">
                    <asp:DropDownList runat="server" AutoPostBack="True" ID="ddl_SelectVehicle" CssClass="selectpicker" OnSelectedIndexChanged="ddl_SelectVehicle_SelectedIndexChanged"></asp:DropDownList>
                </div>
               
            </div>
      <div class="row">
          <h2></h2>
          <div class="col-lg-3">
            <div class="panel panel-info">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-comments fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:Label runat="server" ID="lblTotalGasTrips">400</asp:Label></p>
                    <p class="announcement-text">Total Gas Trips</p>
                  </div>
                </div>
              </div>
             
            </div>
          </div>
          <div class="col-lg-3">
            <div class="panel panel-warning">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-check fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading">$<asp:Label runat="server" ID="lblTotalGasExpense" Text="1234"></asp:Label></p>
                    <p class="announcement-text">Total Gas Cost</p>
                  </div>
                </div>
              </div>
             
            </div>
          </div>
          <div class="col-lg-3">
            <div class="panel panel-danger">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-tasks fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:Label runat="server" ID="lblCarMileage" Text="30"></asp:Label>G/m</p>
                    <p class="announcement-text">Car Mileage</p>
                  </div>
                </div>
              </div>

            </div>
          </div>
          <div class="col-lg-3">
            <div class="panel panel-success">
              <div class="panel-heading">
                <div class="row">
                  <div class="col-xs-6">
                    <i class="fa fa-comments fa-5x"></i>
                  </div>
                  <div class="col-xs-6 text-right">
                    <p class="announcement-heading"><asp:Label runat="server" ToolTip="Tool tip" ID="lblOdometer" Text="45"></asp:Label></p>
                    <p class="announcement-text">Odometer</p>
                  </div>
                </div>
              </div>

            </div>
          </div>
        </div><!-- /.row -->

<%--            </ContentTemplate>
    </asp:UpdatePanel>--%>
<hr />
    
<asp:Panel ID="PanelChart" runat="server">
    <div class="row">
        <h2></h2>
        
        <%--<div class="col-md-1 form-group">
             <asp:DropDownList runat="server" ID="ddlSelectYear" OnSelectedIndexChanged="ddlSelectYear_SelectedIndexChanged" ></asp:DropDownList>
        </div>--%>
        <div class="form-group">
                        
        <div class="col-md-2 form-group">
        <asp:TextBox ID="tbReceiptYear" runat="server" CssClass="col-md-offset-1 form-control" placeholder="Enter year"/>
            </div>
            <div class="col-md-1 form-group">
        
            <asp:Button ID="btnGetGraph" CssClass="btn btn-primary" Text="Show Chart" OnClick="btnGetGraph_Click" runat="server" />
            </div>
            </div>
        <div>
            
            <asp:Label ID="lblChartLabel" runat="server"></asp:Label>  
          
        </div>
        </div>
            <!-- chart  -->
        <div class="row">
          <div id="container" style="min-width: 400px; height: 400px; margin: 10"><hr /></div>
    </div>
    </asp:Panel>
   
    <script src="http://code.jquery.com/jquery-1.9.1.min.js" type="text/javascript"></script>  
    <script src="http://code.highcharts.com/highcharts.js" type="text/javascript"></script>
    <script src="http://code.highcharts.com/modules/exporting.js"></script>

      <script src="scripts/bootstrap-select.js"></script>


    <script type="text/javascript">

        $(document).ready(function () {
          
            $(".selectpicker").selectpicker();

      
        });

    </script>

   


</asp:Content>
