using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private bool _isLoggedIn;
    protected void Page_Load(object sender, EventArgs e)
    {
        _isLoggedIn = false;
        if (_isLoggedIn)
        {
            menu.InnerHtml +=  "<li><a href=\"#\"> My Shifts </a></li> <li><a href=\"#\"> Account </a></li>";
        }

        string[] strings = { "Kitchen", "Gym", "Eating" };

        foreach (var str in strings)
        {
            tasks.InnerHtml += $"<div class=\"task\"><span>{str}</span></div>\n";
             }




        shift1.ServerClick += ShiftClick;
        cross.ServerClick += CloseShift;
   

    }

    public void ShiftClick(object sender, EventArgs e)
    {
        myModal.Style["display"] = "block";
    }
    
    public void CloseShift(object sender, EventArgs e)
    {
        myModal.Style["display"] = "none";
    }


}