// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       A Class the adds graphic support classes for WaterSimDCDC Controls

//       WaterSimDCDC.Controls_Graphic_Utils
//       Version 4.1
//       Keeper Ray Quay  ray.quay@asu.edu
//       Copyright (C) 2011,2012 , The Arizona Board of Regents
//              on behalf of Arizona State University

//       All rights reserved.

//       Developed by the Decision Center for a Desert City
//       Lead Model Development - David A. Sampson <david.a.sampson@asu.edu>

//       This program is free software: you can redistribute it and/or modify
//       it under the terms of the GNU General Public License version 3 as published by
//       the Free Software Foundation.

//       This program is distributed in the hope that it will be useful,
//       but WITHOUT ANY WARRANTY; without even the implied warranty of
//       MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//       GNU General Public License for more details.

//       You should have received a copy of the GNU General Public License
//       along with this program.  If not, please see <http://www.gnu.org/licenses/>.
//
//====================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using WaterSimDCDC;


namespace WaterSimDCDC.Controls
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary> Fieldinfo. </summary>
    /// <remarks> A structure that holds information about a tabke column/field</remarks>
    ///-------------------------------------------------------------------------------------------------

    public struct fieldinfo
        {
           
           string FFieldname;
           string FLabel;

           ///-------------------------------------------------------------------------------------------------
           /// <summary> Constructor. </summary>
           /// <param name="fieldname">  The fieldname. </param>
           /// <param name="fieldlabel"> The fieldlabel. </param>
           ///-------------------------------------------------------------------------------------------------

           public fieldinfo ( string fieldname, string fieldlabel)
           {
               FFieldname = fieldname;
               FLabel = fieldlabel;
           }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the fieldname. </summary>
        /// <value> The fieldname. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Fieldname
        { get {return FFieldname;} }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the label. </summary>
        /// <value> The label. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Label 
        { get {return FLabel;}}

        }
    //------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Providerinfo. </summary>
        ///
        /// <remarks> a structure that hold information about a provider </remarks>
        ///-------------------------------------------------------------------------------------------------

        public struct providerinfo
        {
           string FPcode;
           string FLabel;

           ///-------------------------------------------------------------------------------------------------
           /// <summary> Constructor. </summary>
           /// <param name="pcode"> The pcode. </param>
           /// <param name="label"> The label. </param>
           ///-------------------------------------------------------------------------------------------------

           public providerinfo ( string pcode, string label)
           {
               FPcode = pcode;
               FLabel = label;
           }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the providercode. </summary>
        /// <value> The providercode. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Providercode
        { get {return FPcode;} }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the label. </summary>
        ///
        /// <value> The label. </value>
         ///-------------------------------------------------------------------------------------------------

        public string Label 
        { get {return FLabel;}}

        }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> Chart points by year. </summary>
    ///
    /// <remarks> A structire that holds arrays of in data for a chart of data by year </remarks>
    ///-------------------------------------------------------------------------------------------------

    public struct ChartPointsByYear
    {
        int[] FDataValues;
        int[] FYearValues;
        int[] FRawDataValues;
        string FLabel;
        
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <exception cref="Exception"> Thrown when the length of data and years arrays are not the same. </exception>
        /// <param name="years">  The years. </param>
        /// <param name="data">   The data. </param>
        /// <param name="aLabel"> The label for the data series. </param>
        ///-------------------------------------------------------------------------------------------------

        public ChartPointsByYear(int[] years, int[] data, string aLabel)
        {
            FRawDataValues = data;
            FYearValues = years;
            FLabel = aLabel;
            if (data.Length != years.Length)
            { throw new Exception("Number of years does not equal number of data items"); }
            FDataValues = new int[data.Length];
            for (int i=0;i<data.Length;i++)  FDataValues[i] = FRawDataValues[i]; 
        }
        ////---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the series label. </summary>
        /// <value> The series label. </value>
        ///-------------------------------------------------------------------------------------------------

        public string SeriesLabel
        { get { return FLabel;}}

        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the years data array </summary>
        /// <value> The years. </value>
        ///-------------------------------------------------------------------------------------------------

        public int[] Years
        {
            get { return FYearValues; }
           // set { FYearValues = value; }
        }
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the data values array. </summary>
        /// <remarks>This data may have been altered to map multiple scales</remarks>
        /// <value> The values. </value>
        ///-------------------------------------------------------------------------------------------------

        public int[] Values
        {
            get { return FDataValues; }
          //  set { FDataValues = value; }
        }
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the data value array in its original form. </summary>
        ///
        /// <value> the data values. </value>
        ///-------------------------------------------------------------------------------------------------

        public int[] RawData
        {
            get { return FRawDataValues; }
            //  set { FDataValues = value; }
        }
        //----------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the maximum value in the Last year of data array. </summary>
        /// <returns> Maximum Value </returns>
        ///-------------------------------------------------------------------------------------------------

        public int MaxValue()
        {
            int Max = FDataValues[FDataValues.Length-1];
            foreach (int value in FDataValues)
            {
                if (value > Max) Max = value;
            }
            return Max;
        }
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the minimum value in the data array. </summary>
        /// <returns> Minimum Value </returns>
        ///-------------------------------------------------------------------------------------------------

        public int MinValue()
        {
            int Min = FDataValues[FDataValues.Length - 1];
            foreach (int value in FDataValues)
            {
                if (value < Min) Min = value;
            }
            return Min;
        }
        //-----------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the maximum year in year array. </summary>
        /// <returns> Max Year </returns>
        ///-------------------------------------------------------------------------------------------------

        public int MaxYear()
        {
            int Max = FYearValues[FDataValues.Length - 1];
            foreach (int value in FYearValues)
            {
                if (value > Max) Max = value;
            }
            return Max;
        }
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the minimum year in year array. </summary>
        /// <returns> Minimum Year. </returns>
        ///-------------------------------------------------------------------------------------------------

        public int MinYear()
        {
            int Min = FYearValues[FDataValues.Length - 1];
            foreach (int value in FYearValues)
            {
                if (value < Min) Min = value;
            }
            return Min;
        }
        //-------------------------------------------
        public void ScaleData(double scaler)
        {
            for (int i = 0; i < FDataValues.Length; i++)
            {
                FDataValues[i] = Convert.ToInt32(Convert.ToDouble(FRawDataValues[i]) * scaler);

            }
        }
        //---------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> a method used to compare data for sorting
        ///            </summary>
        /// <param name="P1"> The first ChartPointsByYear. </param>
        /// <param name="P2"> The second ChartPointsByYear. </param>
        /// <returns>0 are equal, 1 P2 greater than P1,  -1 P1 greater than P2 . </returns>
        ///-------------------------------------------------------------------------------------------------

        public static int CompareForSort_UseMax(ChartPointsByYear P1, ChartPointsByYear P2)
        {
            int P1Max = P1.MaxValue();
            int P2Max = P2.MaxValue();
            if (P1Max == P2Max) return 0;
            else
                if (P1Max < P2Max) return 1;
                else
                {
                    return -1;
                }
        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary> A Class that moves data from a data table into a Chart object . </summary>
    ///-------------------------------------------------------------------------------------------------

    public class ChartManager
    {
        System.Windows.Forms.DataVisualization.Charting.Chart FChart;
        string FTitle;
        SeriesChartType FChartType = SeriesChartType.Line;

        //------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="AChart">     The chart. </param>
        /// <param name="ChartTitle"> The chart title. </param>
        ///-------------------------------------------------------------------------------------------------

        public ChartManager(System.Windows.Forms.DataVisualization.Charting.Chart AChart, string ChartTitle)
        {
            FChart = AChart;
            FTitle = ChartTitle;
        }
        //------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the chart title. </summary>
        /// <value> The chart title. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ChartTitle
        {
            get { return FTitle; }
            set { FTitle = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the type of the chart. </summary>
        ///
        /// <value> The type of the chart. </value>
        ///-------------------------------------------------------------------------------------------------

        public SeriesChartType ChartType
        {
            get { return FChartType; }
            set
            {
                switch (value)
                {
                    case SeriesChartType.Line:
                        {
                            FChartType = value;
                            break;
                        }
                    case SeriesChartType.StackedArea:
                        {
                            FChartType = value;
                            break;
                        }
                }
            }
        }
        //------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Builds an annual provider graph. </summary>
        /// <param name="DT">             The datatable. </param>
        /// <param name="DbConnection">   The database connection. </param>
        /// <param name="ScenarioName"> The Scenario to Graph, graphs first scenario in table if ScenarioName = ""</param>
        /// <param name="dbFields">       The database fields to include in the chart. </param>
        /// <param name="targetProvider">  The provider whose data should be charted. </param>
        /// <param name="aLabel">         The chart label. </param>
        ///-------------------------------------------------------------------------------------------------

        public void BuildAnnualProviderGraph(SimulationResults SimResults, List<string> Fields, string targetUnitName, string aLabel)
        {
            // create a lits for data
            List<ChartPointsByYear> ChartSeriesList = new List<ChartPointsByYear>();
            // check to make sure data is there
            if (SimResults.Length > 0)
            {
                string pcode = "";

                int year = 0;
                int parmvalue = 0;
                int yearCount = 0;
                int yearIndex = 0;

                // figure out max min years
                int MaxYear = -1;
                int MinYear = 9999;
                foreach (AnnualSimulationResults ARS in SimResults)
                {
                    year = ARS.year;
                    if (year > MaxYear) MaxYear = year;
                    if (year < MinYear) MinYear = year;
                }
                yearCount = (MaxYear - MinYear) + 1;
                // OK can set up the data
                int ParameterCount = Fields.Count;
                int[][] ValueData = new int[ParameterCount][];
                for (int i = 0; i < ParameterCount; i++)
                    ValueData[i] = new int[yearCount];
                // How many data values, based on years, only collect this many values
                int NumberOfYears = (MaxYear - MinYear) + 1;
                int YearCnt = 0;
                // Cycle through each record and stick data into the right slot
                bool iserr = false;
                string errMessage = "";
                // go through each year
                foreach (AnnualSimulationResults ARS in SimResults)
                {
                    // do outputs
                    for(int i=0; i<ARS.Outputs.
                    foreach (SimulationOutputs SimOutput in ARS.Outputs)
                    {

                    }
                }
            }

        }
        public void BuildAnnualProviderGraph(DataTable DT, UniDbConnection DbConnection, string ScenarioName, List<fieldinfo> dbFields, string targetProvider, string aLabel )
        {
            // create a lits for data
            List<ChartPointsByYear> ChartSeriesList = new List<ChartPointsByYear>();
         
            // get data
            int RowCount = DT.Rows.Count;
            // check to make sure there is somthing there
            if (RowCount > 0)
                try
                {
                    string YearFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfSimYear;
                    string PrvdFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfProviderCode;
                    string ScnFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfScenarioName;
                    string pcode = "";

                    //int pIndex = -1;
                    int year = 0;
                    int parmvalue = 0;
                    int yearCount = 0;
                    int yearIndex = 0;
                    // Fetch the first records ScenarioName
                    DataRow FirstColumn = DT.Rows[0];
                    string FirstScnName = FirstColumn[ScnFldStr].ToString().Trim().ToUpper();
                    // Standardize Scenario Name
                    ScenarioName = ScenarioName.ToUpper().Trim();

                    // OK, figure out the years Min Max and Count
                    int MaxYear = -1;
                    int MinYear = 9999;
                    foreach (DataRow DR in DT.Rows)
                    {
                        year = Convert.ToInt32(DR[YearFldStr].ToString());
                        if (year > MaxYear) MaxYear = year;
                        if (year < MinYear) MinYear = year;
                    }
                    yearCount = (MaxYear - MinYear) + 1;
                    // OK can set up the dta
                    int ParameterCount = dbFields.Count;

                    int[][] ValueData = new int[ParameterCount][];
                    for (int i = 0; i < ParameterCount; i++)
                        ValueData[i] = new int[yearCount];
                    // How many data values, based on years, only collect this many values
                    int NumberOfYears = (MaxYear - MinYear) + 1;
                    int YearCnt = 0;
                    // Cycle through each record and stick data into the right slot
                    bool iserr = false;
                    string errMessage = "";
                    foreach (DataRow DR in DT.Rows)
                    {
                        // fetch Scenarioname
                        string ScnStr = DR[ScnFldStr].ToString().ToUpper().Trim();
                        // fetch year value and calculate index
                        year = UniDB.Tools.ConvertToInt32(DR[YearFldStr].ToString(), ref iserr, ref errMessage);
                        yearIndex = year - MinYear;

                        // Fetch only records that match the scenario name or if "" then just the first scenario
                        if (((ScenarioName == "") && (ScnStr == FirstScnName)) || (ScnStr == ScenarioName))
                        {
                            // fetch only records for the target provider
                            pcode = DR[PrvdFldStr].ToString().Trim();
                            if (pcode.ToUpper() == targetProvider.ToUpper())
                            {
                                int pIndex = 0;
                                // Ok get all the fields requested
                                foreach (fieldinfo info in dbFields)
                                {
                                    if (DT.Columns.Contains(info.Fieldname))
                                    {
                                        parmvalue = UniDB.Tools.ConvertToInt32(DR[info.Fieldname].ToString(), ref iserr, ref errMessage);
                                        ValueData[pIndex][yearIndex] = parmvalue;
                                        pIndex++;
                                    }
                                }
                                // OK get a years worth of data, increment year count
                                YearCnt++;
                            }
                        }
                        // OK, if we have read values for all the years, then break
                        if (YearCnt == NumberOfYears) break; // from foreach
                        
                    }
                    if (iserr)
                    {
                        // Do What?
                    }
                    // OK create the series
                    // First create a year array
                    int[] allYearValues = new int[yearCount];
                    for (int i = 0; i < yearCount; i++)
                        allYearValues[i] = MinYear + i;
                    for (int i = 0; i < ParameterCount; i++)
                    {
                        ChartPointsByYear points = new ChartPointsByYear(allYearValues, ValueData[i], dbFields[i].Label);
                        ChartSeriesList.Add(points);
                    }
                    //  Now Sort By Size
                    ChartSeriesList.Sort(ChartPointsByYear.CompareForSort_UseMax);

                    // ok clear up chart
                    FChart.Series.Clear();
                    FChart.Legends.Clear();
                    FChart.ChartAreas.Clear();
                    //                    ProviderPopChart.Update();

                    // Setup legend

                    FChart.ChartAreas.Add(FTitle);
                    System.Windows.Forms.DataVisualization.Charting.Legend MyLegend = new System.Windows.Forms.DataVisualization.Charting.Legend("Default");
                    FChart.Legends.Add(MyLegend);
                    int index = 0;
                    foreach (ChartPointsByYear points in ChartSeriesList)
                    {

                        Series dSeries = new Series();
                        int[] Years = points.Years;
                        int[] Values = points.Values;
                        dSeries.LegendText = points.SeriesLabel;
                        dSeries.BorderWidth = 2;
                        dSeries.ChartType = FChartType;
                        dSeries.LabelToolTip = points.SeriesLabel;
                        //dSeries.Label = points.SeriesLabel;
                        for (int j = 0; j < Years.Length; j++)
                        {
                            dSeries.Points.AddXY(Years[j], Values[j]);
                        }
                        FChart.Series.Add(dSeries);
                        index++;
                    }
                    // Chart Label
                    // First build field label
                    string fldlabel = "";
                    foreach (fieldinfo fi in dbFields)
                    {
                        if (DT.Columns.Contains(fi.Fieldname))
                        {
                            if (fldlabel != "")
                            {
                                fldlabel += ", ";
                            }
                            if (fi.Label=="")
                               fldlabel += " [" + fi.Fieldname + "]";
                            else
                              fldlabel += fi.Label;
                        }
                    }
                    bool addTitle = false;
                    Title myTitle = null;
                    if (FChart.Titles.Count > 0)
                    {
                        myTitle = FChart.Titles.FindByName("MAIN");
                    }
                    if (myTitle == null)
                    {
                        myTitle = new Title();
                        addTitle = true;
                    }
                    myTitle.Name = "MAIN";
                    myTitle.Text = aLabel.Trim() + " by " + fldlabel + " and Year";
                    myTitle.Alignment = ContentAlignment.MiddleCenter;
                   
                    myTitle.Font = new Font(myTitle.Font.Name, 14.0F, FontStyle.Bold, myTitle.Font.Unit);
                    if (addTitle)
                    {
                        FChart.Titles.Add(myTitle);
                    }
                    // Add Legend
                    LegendCellColumn secondColumn = new LegendCellColumn();
                    secondColumn.ColumnType = LegendCellColumnType.Text;
                    secondColumn.HeaderText = "Name";
                    secondColumn.Text = "#LEGENDTEXT";
                    secondColumn.HeaderBackColor = Color.WhiteSmoke;
                    FChart.Legends["Default"].CellColumns.Add(secondColumn);

                    // Set Min cell column attributes
                    LegendCellColumn maxColumn = new LegendCellColumn();
                    maxColumn.Text = "#MAX{N1}";
                    maxColumn.HeaderText = "Max";
                    maxColumn.Name = "MaxColumn";
                    maxColumn.HeaderBackColor = Color.WhiteSmoke;
                    FChart.Legends["Default"].CellColumns.Add(maxColumn);
                    // Add Color column
                    LegendCellColumn firstColumn = new LegendCellColumn();
                    firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                    firstColumn.HeaderText = "Color";
                    firstColumn.HeaderBackColor = Color.WhiteSmoke;
                    FChart.Legends["Default"].CellColumns.Add(firstColumn);

                }

                catch (Exception e)
                {
                    //. Yikes
                    MessageBox.Show("Error while building chart : " + e.Message);
                }
            // OK let's see what happens
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Builds an annual parameter graph. </summary>
        /// <param name="DT">           The datatable. </param>
        /// <param name="DbConnection"> The database connection. </param>
        /// <param name="Providers">    The providers to be charted. </param>
        /// <param name="fldname">      The fldname or column name to be charted. </param>
        ///-------------------------------------------------------------------------------------------------

        public void BuildAnnualParameterGraph(DataTable DT, UniDbConnection DbConnection,string ScenarioName,List<providerinfo> Providers, string fldname, string fieldlabel)
        {
            List<string> debug = new List<string>();

                // create a lits for data
                List<ChartPointsByYear> ChartSeriesList = new List<ChartPointsByYear>();
                // ok clear up chart
                FChart.Series.Clear();
                FChart.Legends.Clear();
                FChart.ChartAreas.Clear();
                //                    ProviderPopChart.Update();

                // Setup legend

                FChart.ChartAreas.Add(FTitle);
                System.Windows.Forms.DataVisualization.Charting.Legend MyLegend = new System.Windows.Forms.DataVisualization.Charting.Legend("Default");
                FChart.Legends.Add(MyLegend);
                // get data
                int RowCount = DT.Rows.Count;

                string YearFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfSimYear;
                string PrvdFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfProviderCode;
                string ScnFldStr = WaterSimDCDC.WaterSimManager_DB.rdbfScenarioName;
                string PopFldStr = fldname;

                // Fetch the first records ScenarioName
                DataRow FirstColumn = DT.Rows[0];
                string FirstScnName = FirstColumn[ScnFldStr].ToString().ToUpper().Trim();
                ScenarioName = ScenarioName.ToUpper().Trim();

                int pIndex = -1;
                int year = 0;
                int pop = 0;
                string pcode = "";
                int yearCount = 0;
                int yearIndex = 0;
                // OK, figure out the years Min Max and Count
                int MaxYear = -1;
                int MinYear = 9999;
                // There must be a way to speed this up!
                foreach (DataRow DR in DT.Rows)
                {
                    year = Convert.ToInt32(DR[YearFldStr].ToString());
                    if (year > MaxYear) MaxYear = year;
                    if (year < MinYear) MinYear = year;
                }
                yearCount = (MaxYear - MinYear) + 1;
                // OK can set up the dta
                int ProviderCount = Providers.Count;

                int[][] ValueData = new int[ProviderCount][];
                for (int i = 0; i < ProviderCount; i++)
                    ValueData[i] = new int[yearCount];

                // Cycle through each record and stick data into the right slot
                foreach (DataRow DR in DT.Rows)
                {
                    bool iserror = false;
                    string errMessage = "";
                    // get the year and calculate an index into the value array
                    // fetch Scenarioname
                    string ScnStr = DR[ScnFldStr].ToString().ToUpper().Trim();
                    debug.Add(ScnStr+":"+ScenarioName+":"+(ScnStr==ScenarioName).ToString());
                    // Fetch only records that match the scenario name or if "" then just the first scenario
                    if (((ScenarioName == "") && (ScnStr == FirstScnName)) || (ScnStr == ScenarioName))
                    {
                        year = UniDB.Tools.ConvertToInt32(DR[YearFldStr].ToString(), ref iserror, ref errMessage);
                        yearIndex = year - MinYear;
                        pop = UniDB.Tools.ConvertToInt32(DR[PopFldStr].ToString(), ref iserror, ref errMessage);
                        pcode = DR[PrvdFldStr].ToString().Trim().ToUpper();
                        // Find the index for this pcode in the list of Providers codes to graft
                        pIndex = Providers.FindIndex(delegate(providerinfo pi) { return pi.Providercode.ToUpper() == pcode; });
                        // if index>-1 then add otherwise it is not in the list so just skip it
                        if (pIndex > -1)
                        {
                            ValueData[pIndex][yearIndex] = pop;
                        }
                        if (iserror)
                        {
                            // Do What here?
                        }
                    }
                }

                // OK create the series
                // First create a year array
                int[] allYearValues = new int[yearCount];
                for (int i = 0; i < yearCount; i++)
                    allYearValues[i] = MinYear + i;
                for (int i = 0; i < ProviderCount; i++)
                {
                    ChartPointsByYear points = new ChartPointsByYear(allYearValues, ValueData[i], Providers[i].Label );
                    ChartSeriesList.Add(points);
                }
                //  Now Sort By Size
                ChartSeriesList.Sort(ChartPointsByYear.CompareForSort_UseMax);
                
                
                foreach (ChartPointsByYear points in ChartSeriesList)
                {
                    Series dSeries = new Series();
                    int[] Years = points.Years;
                    int[] Values = points.Values;
                    dSeries.LegendText = points.SeriesLabel;
                    dSeries.ChartType = FChartType;
                    dSeries.BorderWidth = 3;
                    dSeries.MarkerSize = 20;
                    dSeries.LabelToolTip = points.SeriesLabel;
                    for (int j = 0; j < Years.Length; j++)
                    {
                        dSeries.Points.AddXY(Years[j], Values[j]);
                    }
                    FChart.Series.Add(dSeries);
                }

                FChart.ChartAreas[0].AxisX.Minimum = MinYear;
                FChart.ChartAreas[0].AxisX.Maximum = MaxYear;
                FChart.ChartAreas[0].AxisX.Interval = 5;
                // Chart Label
                bool addTitle = false;
                Title myTitle = null;
                if (FChart.Titles.Count > 0)
                {
                    myTitle = FChart.Titles.FindByName("MAIN");
                }
                if (myTitle == null)
                {
                    myTitle = new Title();
                    addTitle = true;
                }
                myTitle.Name = "MAIN";
                myTitle.Text = fieldlabel + " by Water Provider and Year";
                myTitle.Font = new Font(myTitle.Font.Name, 14.0F, FontStyle.Bold, myTitle.Font.Unit);
                myTitle.Alignment = ContentAlignment.MiddleCenter;
                if (addTitle)
                {
                    FChart.Titles.Add(myTitle);
                }
                // Chart Legend
                /// <summary> The second column. </summary>
                LegendCellColumn secondColumn = new LegendCellColumn();
                secondColumn.ColumnType = LegendCellColumnType.Text;
                secondColumn.HeaderText = "Name";
                secondColumn.Text = "#LEGENDTEXT";
                secondColumn.HeaderBackColor = Color.WhiteSmoke;
                FChart.Legends["Default"].CellColumns.Add(secondColumn);

                // Set Min cell column attributes
                LegendCellColumn maxColumn = new LegendCellColumn();
                maxColumn.Text = "#MAX{N1}";
                maxColumn.HeaderText = "Max";
                maxColumn.Name = "MaxColumn";
                maxColumn.HeaderBackColor = Color.WhiteSmoke;
                FChart.Legends["Default"].CellColumns.Add(maxColumn);
                // Add Color column
                LegendCellColumn firstColumn = new LegendCellColumn();
                firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol;
                firstColumn.HeaderText = "Color";
                firstColumn.HeaderBackColor = Color.WhiteSmoke;
                FChart.Legends["Default"].CellColumns.Add(firstColumn);



                // OK let's see what happens
            }

    }

    }



