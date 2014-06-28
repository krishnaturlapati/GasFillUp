<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GasFillUp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Welcome!</h2>
    <p>
        GasFillUp application aims to create awareness about carbon footprint/CO2 emission released by vehicles and the impact on nature. This app also helps you to digitize your gas receipts.  
    </p>
    <p>
    By registering you can see save the gas receipt information and access to dashboard which provides an easy to read, graphical representation of your data.   
    </p>
    <p>
       If you have any questions <a href="GasFillUp@gmail.com">email me</a>
    </p>

    <div class="row">
        <h2></h2>
         <div class="col-md-3">
             <h3>Fuel Tips</h3>
                  <iframe frameborder='0' width='265' height='375' src='http://www.fueleconomy.gov/feg/widgets/gas-tips/tipwidget.html'></iframe>

        </div>
       
        <div class="col-md-3">

        </div>
        <div class="col-md-3">
              <h3>What is carbon footprint?</h3>
              <iframe frameborder='0' width="560" height="315" src="//www.youtube.com/embed/8q7_aV8eLUE"></iframe>
                </div>
        
         
    </div>

    

   

</asp:Content>
