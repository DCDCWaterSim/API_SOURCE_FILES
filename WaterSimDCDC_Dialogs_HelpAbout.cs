using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
/**********************************************************************************
    WaterSimDCDC Scenario Ensemble Builder Analyzer Tool
	Version 5.0

	HelpAboutForm Version 7  11/21/11  Quay

    Copyright (C) 2011 , The Arizona Board of Regents
	on behalf of Arizona State University
	
    All rights reserved.

	Developed by the Decision Center for a Desert City
	Lead Model Development - David Sampson <dasamps1@asu.edu>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License version 3 as published by
    the Free Software Foundation.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
    
    Dilaog for selecting or specifying a new table in a database.
 * 
 * 
 * 

 *******************************************************************************/
namespace WaterSimDCDC.Dialogs
{
    /// <summary>   Values that represent eHelpAboutMode.  </summary>
    public enum eHelpAboutMode { haHelp, haAbout };
    ///-------------------------------------------------------------------------------------------------
    /// <summary> Form for viewing and dialog that shows about information and an rtf help file.  </summary>
    ///
    /// <remarks> </remarks>
    ///-------------------------------------------------------------------------------------------------

    public partial class HelpAboutForm : Form
    {
        int FMode = 0; // 0 == help 1 == about 5 == Secret
        string Fversion = "";
        string FApiVersion = "";
        string FBuild = "";
        string FHelpFilename = "";
        bool _helpfileloaded = false;
        //----------------------------------------

        /// <summary>   Default constructor. </summary>
        public HelpAboutForm()
        {
            InitializeComponent();
        }
        //----------------------------------------

        /// <summary>   Constructor for About Dialog. </summary>
        /// <param name="Version">      The version. </param>
        /// <param name="APIVersion">   The api version. </param>
        /// <param name="Build">        The build. </param>
        /// <remarks>Displays only the About info </remarks>
        public HelpAboutForm(string Version, string APIVersion, string Build)
            : this()
        {
            Fversion = Version;
            FApiVersion = APIVersion;
            FBuild = Build;
            ToolVersionLabel.Text = Version;
            APIVersionLabel.Text = APIVersion;
            BuildLabel.Text = Build;
        }
        //----------------------------------------

        /// <summary>   Constructor for Help Form. </summary>
        /// <param name="HelpFilename"> Filename of the help file. </param>
        /// <remarks> Displays only teh help file</remarks>
        public HelpAboutForm(string HelpFilename)
            : this()
        {
            try
            {
                HelpRichTextBox.LoadFile(HelpFilename);
                Mode = eHelpAboutMode.haHelp;
                _helpfileloaded = true;
            }
            catch
            {
                _helpfileloaded = false;
                MessageBox.Show("Could not load HelpFile " + HelpFilename);
            }
        }
        //----------------------------------------

        /// <summary>   Gets or sets the mode. </summary>
        /// <value> The mode. </value>

        public eHelpAboutMode Mode
        {

            get { return (eHelpAboutMode)FMode; }
            set
            {
                SetMode((int)value);
            }
        }
        //-------------------------------------------------------

        /// <summary>   Gets or sets the tool version. </summary>
        /// <value> The tool version string. </value>
        public string ToolVersion
        { get { return Fversion; } set { Fversion = value; } }

        /// <summary>   Gets or sets the api version. </summary>
        /// <value> The api version string. </value>
        public string APIVersion
        { get { return FApiVersion; } set { FApiVersion = value; } }

        /// <summary>   Gets or sets the model build version. </summary>
        /// <value> The model build version string. </value>
        public string ModelBuild
        { get { return FBuild; } set { FBuild = value; } }

        /// <summary>   Gets or sets the filename for the help file. </summary>
        /// <value> The filename of the help file. </value>
        /// <remarks>Loads the help file if can be found.</remarks>
        public string HelpFilename
        {
            get { return FHelpFilename; }
            set
            {
                FHelpFilename = value;
                try
                {
                    HelpRichTextBox.LoadFile(FHelpFilename);
                    Mode = eHelpAboutMode.haHelp;
                    _helpfileloaded = true;
                }
                catch
                {
                    _helpfileloaded = false;
                    FHelpFilename = "";
                    MessageBox.Show("Could not load HelpFile " + HelpFilename);
                }
            }
        }

        //--------------------------------------------------

        /// <summary>   Sets the form mode . </summary>
        /// <param name="value">0 = Help File, 1= About Dialog 5= Mystery. </param>
        protected void SetMode(int value)
        {
            switch (value)
            {
                case 0:
                    HelpPanel.Visible = true;
                    AboutPanel.Visible = false;
                    TopMost = false;
                    FMode = value;
                    break;
                case 1:
                    HelpPanel.Visible = false;
                    AboutPanel.Visible = true;
                    TopMost = true;
                    FMode = value;
                    break;
                case 5:
                    HelpPanel.Visible = false;
                    AboutPanel.Visible = false;
                    FMode = value;
                    break;
                default:
                    HelpPanel.Visible = false;
                    AboutPanel.Visible = false;
                    break;
            }
        }
        //---------------------------------------------------------------------

        /// <summary>   Event handler. Called by button1 for click events. </summary>
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        //------------------------------------------------------------------------

        /// <summary>   Shows the about dialog. </summary>
        public void ShowAboutDialog()
        {
            Mode = eHelpAboutMode.haAbout;
            this.ShowDialog();
        }
        //------------------------------------------------------------------------

        /// <summary>   Shows the help form. </summary>
        public void ShowHelp()
        {
            if (!_helpfileloaded) MessageBox.Show("Sorry!, No HelpFile was specified.");
            else
            {
                Mode = eHelpAboutMode.haHelp;
                this.Show();
            }
        }
    }
}// HelpAboutCLass
//================================================================

  
