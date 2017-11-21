<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" type="text/css" rel="stylesheet"/>
    <title></title>

</head>
<body>
    <form id="form1" runat="server">
 <ul id="menu" class="menu" runat="server">
 <li class="active"><a href="#">Schedule</a></li>

</ul>
    <h1 id="hej" runat="server">Aarhus Basketball Festival Volunteer Schedule</h1>
        <div id="container">
                   <div class="tasks" id="tasks" runat="server">
            <!-- 34 px is 6.00 - 60px is one hour-->
            <div class="task">
                <p>KITCHEN</p>



            </div>
            <div class="task">
                <p>KICHEN</p>

            </div>

        </div> 

     
	<div class="timeline">
		<ul>
			<li><span>06:00</span></li>
			<li><span>07:00</span></li>
			<li><span>08:00</span></li>
			<li><span>09:00</span></li>
			<li><span>10:00</span></li>
			<li><span>11:00</span></li>
			<li><span>12:00</span></li>
			<li><span>13:00</span></li>
			<li><span>14:00</span></li>
			<li><span>15:00</span></li>
			<li><span>16:00</span></li>
			<li><span>17:00</span></li>
			<li><span>18:00</span></li>
			<li><span>19:00</span></li>
			<li><span>20:00</span></li>
			<li><span>21:00</span></li>
			<li><span>22:00</span></li>
			<li><span>23:00</span></li>
			<li><span>24:00</span></li>
		</ul>
	</div>


          <div class="shifttasks">
            <!-- 34 px is 6.00 - 60px is one hour-->
            <div class="shifttask">
                <a id ="shift1" runat="server" href="#hej"><div class="shift" style="height: 60px; top: 94px;">Shift1</div></a>
                <div class="shift" style="height: 90px; top: 214px;">Shift2</div>
                <div class="shift" style="height: 60px; top: 34px;">Shift3</div>

            </div>

        </div> 
    </div>
        <div id="myModal" class="modal" runat="server">

  <!-- Modal content -->
  <div class="modal-content">
    <a class="close" id="cross" runat="server">&times;</a>
    <p>Shift infromation</p>
  </div>

</div>
    </form>
</body>
</html>
