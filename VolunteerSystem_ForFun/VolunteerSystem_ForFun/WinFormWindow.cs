using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VolunteerSystem_ForFun
{
    public partial class WinFormWindow : Form, IVolunteerSystemUI
    {
        private IVolunteerSystem _system;

        Button test;
        Panel mainPanel;
        Panel menuButtonPanel;
        Size fullClientWindowSize;
        HomepageUIStuff.Homepage _homepage;

        int buttonPanelHeight;

        public WinFormWindow(IVolunteerSystem system)
        {
            _system = system;
            InitializeComponent();
            Width = 1600;
            Height = 800;
            fullClientWindowSize = RectangleToScreen(this.ClientRectangle).Size;
            buttonPanelHeight = 50;
            
            //Button panel
            menuButtonPanel = new Panel();
            menuButtonPanel.Size = new Size(fullClientWindowSize.Width - 10, buttonPanelHeight);
            menuButtonPanel.Location = new Point(5, 5);
            menuButtonPanel.BackColor = Color.Green;
            menuButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

            //Main panel
            mainPanel = new Panel();
            mainPanel.Size = new Size(fullClientWindowSize.Width - 10, fullClientWindowSize.Height - (menuButtonPanel.Location.Y + menuButtonPanel.Height) - 10);
            mainPanel.Location = new Point(5, menuButtonPanel.Location.Y + menuButtonPanel.Height + 5);
            mainPanel.BackColor = Color.Blue;
            mainPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            
            //Test popup button
            test = new Button();
            test.Location = new Point(10, 10);
            test.Text = "Create a popup";
            test.AutoSize = true;
            test.Click += test_Click;
            
            //The pages
            _homepage = new HomepageUIStuff.Homepage(_system);

            //Start on the homepage
            mainPanel.Controls.Add(_homepage.GetHomepagePanel(mainPanel));
            
            //Adds to the window
            Controls.Add(mainPanel);
            Controls.Add(menuButtonPanel);
            //Adds to the main panel
            mainPanel.Controls.Add(test);
            //Adds to the button panel
        }

        private void test_Click(object sender, EventArgs e)
        {
            Popup("TEST header", "test body");
        }

        //Interface ting
        public event VolunteerPopupEvent Popup;

        public void DisplayPopup(string Header, string body)
        {
            PopupUI popup = new PopupUI(Header, body);
            popup.Show();
        }

        public void DisplayGeneralError()
        {
            throw new NotImplementedException();
        }

        public void DisplayHomepage()
        {
            throw new NotImplementedException();
        }
        
        public void DisplaySettings()
        {
            throw new NotImplementedException();
        }

        public void DisplayVolunteerOverview()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
