/**********************************************************************************
    WaterSimDCDC 
	Version 5.0

	GetANameDialog Version 1  9/21/12  Quay

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
    
    Dilaog for selecting or specifying a name, such as a scenario name, based ob values passed to the dialog.
 * 
 * 
 * 

 *******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace WaterSimDCDC.Dialogs
{
    public partial class GetANameDialog : Form
    {
        string Fname = "";
        bool FMustBeUnique = false;
        bool FCaseSensitive = true;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Default constructor. </summary>
        ///
        /// <remarks> Ray, 8/26/2011. </remarks>
        ///-------------------------------------------------------------------------------------------------

        public GetANameDialog()
        {
            InitializeComponent();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Shows the dialog. </summary>
        ///
        /// <remarks> Ray, 8/26/2011. </remarks>
        ///
        /// <param name="title">      The title. </param>
        /// <param name="ValueLabel"> The value label. </param>
        /// <param name="names">      The names. </param>
        ///
        /// <returns> . </returns>
        ///-------------------------------------------------------------------------------------------------

        public DialogResult ShowDialog(string aTitle, string aValueLabel, List<string> names, bool MustBeUnique, bool CaseSensitive)
        {
            FMustBeUnique = MustBeUnique;
            this.Text = aTitle;
            ValueLabel.Text = aValueLabel;
            int RequiredWidth = ValueLabel.Width + NameComboBox.Width + 50;
            this.Width = RequiredWidth;
            int newleft = ValueLabel.Location.X+ValueLabel.Width+5;
            NameComboBox.Location = new Point(newleft, NameComboBox.Location.Y);
           if (names!=null)
            foreach(string value in names)
               NameComboBox.Items.Add(value);
            return ShowDialog();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the value entered in dialog. </summary>
        ///
        /// <value> The value. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Value
        { get { return Fname; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> List contains. </summary>
        ///
        /// <remarks> Ray, 8/26/2011. </remarks>
        ///
        /// <param name="value">         The value. </param>
        /// <param name="CaseSensitive"> true to case sensitive. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        protected virtual bool ListContains(string value, bool CaseSensitive)
        {
            bool found = false;
            string temp = "";
            if (!CaseSensitive) value = value.ToUpper();
            foreach (string item in NameComboBox.Items)
            {
                if (!CaseSensitive) temp = item.ToUpper();
                else temp = item;
                if (value == temp)
                {
                    found = true;
                    break;
                }
            }
            return found;    
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Event handler. Called by OKButton for click events. </summary>
        ///
        /// <remarks> Ray, 8/26/2011. </remarks>
        ///
        /// <param name="sender"> Source of the event. </param>
        /// <param name="e">      Event information. </param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OKButton_Click(object sender, EventArgs e)
        {
            bool oktoclose = true;
            Fname = NameComboBox.Text;
            if (FMustBeUnique)
            {
                oktoclose = !ListContains(Fname,FCaseSensitive);
            }
            if (oktoclose)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Value " + Fname + " is not unique.");
        }
    }
}
