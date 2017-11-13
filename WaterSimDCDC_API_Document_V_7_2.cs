// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 7.2

//       A Class the adds Doxumentation support to the WaterSimDCDC.WaterSim Class

//       WaterSimDCDC_API_Documentation Version 7.2
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
//       2
//       Changes
//       12/3/14 ver 7.
//       Load External Documentation File Support Added
//       Reads an external file, with the name ExternalDocumentation.txt with information to change the extended documentation.  ALlows Customized extended documentation with out rerolling the code.
//       External File has lines as follows   
//       Each Line has a series of objects {NAME:VALUE}
//       All Values are text    
//       {FIELDNAME: "AA"} {DESCRIP:"AA"} {UNIT:"AA"} {LONGUNIT:"AA"} {WEBLABEL:"AA"} {WEBSCALE:["AA","AA",,,,"AA"]} {WEBSCALEVALUES:["AA","AA",,,,"AA"]}
//       First Item {FIELDNAME: "AA"} is mandatory, all other items are optional, FIELDNAME Identifies the parameter that will be changed.
//       Only changes the items that are included.  ie, of WEBLABEL is the only object, changes the WebLabel extended documenetation to this value and leaves all other 
//       extended documentation fields as they are.  NOTE a {NAME:""} will clear the extended documentation with a "" value. 
//
//====================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaterSimDCDC.Documentation
{
    /// <summary>   Water Parameter description item. </summary>
    public class WaterSimDescripItem
    {
        /// <summary> The model parameter code. </summary>
        protected int FModelParam;
        /// <summary> The description. </summary>
        protected string FDescription;
        /// <summary> The units. </summary>
        protected string FUnits;
        /// <summary> The Long Form units. </summary>
        protected string FLongUnits;
        /// <summary>
        /// A Lable to use with web unterface inputs
        /// </summary>
        protected string FWebLabel;

        protected string[] FWebScale = new string[0];

        protected int[] FWebScaleValues = new int[0];

        /// <summary>  The Topic groups this parameter belongs to /// </summary>
        protected List<ModelParameterGroupClass> FTopicGroupList = new List<ModelParameterGroupClass>();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aModelParam">  The model parameter. </param>
        /// <param name="aDescrip">     The descrip. </param>
        /// <param name="aUnit">        The unit. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSimDescripItem(int aModelParam, string aDescrip, string aUnit)
        {
            FModelParam = aModelParam;
            FDescription = aDescrip;
            FUnits = aUnit;
            FLongUnits = aUnit;
            FWebLabel = "";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aModelParam">          The model parameter. </param>
        /// <param name="aDescrip">             The descrip. </param>
        /// <param name="aUnit">                The unit. </param>
        /// <param name="aLongUnit">            The long unit. </param>
        /// <param name="aWebLabel">            The web label. </param>
        /// <param name="theWebScale">          the web scale. </param>
        /// <param name="theWebScaleValues">    the web scale values. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSimDescripItem(int aModelParam, string aDescrip, string aUnit, string aLongUnit, string aWebLabel, 
            string[]  theWebScale, int[] theWebScaleValues)
        {
            FModelParam = aModelParam;
            FDescription = aDescrip;
            FUnits = aUnit;
            FLongUnits = aLongUnit;
            FWebLabel = aWebLabel;
            FWebScale = theWebScale;
            FWebScaleValues = theWebScaleValues;
        }

        internal void AddIfNotFound(ModelParameterGroupClass MPG)
        {
            double SID = MPG.ID;
            ModelParameterGroupClass FoundMPG = FTopicGroupList.Find(delegate(ModelParameterGroupClass Item) { return (Item.ID == SID); });
            if (FoundMPG == null)
            {
                FTopicGroupList.Add(MPG);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aModelParam">          The model parameter. </param>
        /// <param name="aDescrip">             The descrip. </param>
        /// <param name="aUnit">                The unit. </param>
        /// <param name="aLongUnit">            The long unit. </param>
        /// <param name="aWebLabel">            The web label. </param>
        /// <param name="theWebScale">          the web scale. </param>
        /// <param name="theWebScaleValues">    the web scale values. </param>
        /// <param name="theTopicGroups">    Model Parameter TopicGroups the parameter belongs to. </param>
        ///-------------------------------------------------------------------------------------------------
        
        public WaterSimDescripItem(int aModelParam, string aDescrip, string aUnit, string aLongUnit, string aWebLabel,
            string[] theWebScale, int[] theWebScaleValues, ModelParameterGroupClass[] theTopicGroups)
        {
            FModelParam = aModelParam;
            FDescription = aDescrip;
            FUnits = aUnit;
            FLongUnits = aLongUnit;
            FWebLabel = aWebLabel;
            FWebScale = theWebScale;
            FWebScaleValues = theWebScaleValues;
            foreach (ModelParameterGroupClass MPG in theTopicGroups)
            {
                if (MPG != null)
                {
                    FTopicGroupList.Add(MPG);
                    MPG.Add(FModelParam);  // will add if not already in list;
                }
                else
                {
                    string test = aDescrip;
                   // what the
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the model parameter. </summary>
        ///
        /// <value> The model parameter. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ModelParam
        {
            get
            {
                return FModelParam;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the description. </summary>
        /// <value> The description. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Description
        {
            get
            {
                if (FDescription == null)
                    return "";
                else
                    return FDescription;
            }
            set
            {
                if (value != "")
                {
                    FDescription = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the units </summary>
        /// <value> The unit. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Unit
        {
            get
            {
                if (FUnits == null)
                    return "";
                else
                    return FUnits;
            }
            set
            {
                if (value != "")
                {
                    FUnits = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the long unit. </summary>
        ///
        /// <value> The long unit. </value>
        ///-------------------------------------------------------------------------------------------------

        public string LongUnit
        {
            get
            {
                if (FLongUnits == null)
                    return "";
                else
                    return FLongUnits;

            }
            set
            {
                if (value != "")
                {
                    FLongUnits = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the web scale. </summary>
        ///
        /// <value> The web scale. </value>
        ///-------------------------------------------------------------------------------------------------

        public string[] WebScale
        {
            get { return FWebScale; }
            set
            {
                if ((value != null) && (value.Length != 0))
                {
                    FWebScale = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the web scale value. </summary>
        ///
        /// <value> The web scale value. </value>
        ///-------------------------------------------------------------------------------------------------

        public int[] WebScaleValue
        {
            get { return FWebScaleValues; }
            set
            {
                if ((value != null) && (value.Length != 0))
                {
                    FWebScaleValues = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the web label. </summary>
        ///
        /// <value> The web label. </value>
        ///-------------------------------------------------------------------------------------------------

        public string WebLabel
        {
            get { return FWebLabel; }
            set
            {
                if (value != "")
                {
                    FWebLabel = value;
                }
            }
        }
        /// <summary>
        /// The List of Topic Groups this belongs to
        /// </summary>
        public List<ModelParameterGroupClass> TopicGroups
        {
            get { return FTopicGroupList; }
        }

    }


    /// <summary>   WaterSim Parameter and Data Documentation Support Class </summary>
    /// <remarks> This class enhances the amount of information about a parameter that is available from the Parameter Manager
    ///           Includes an extended descriptiopn of the parameter and the units of the parameter</remarks>
    public class Extended_Parameter_Documentation
    {
        List<WaterSimDescripItem> MPDesc = new List<WaterSimDescripItem>();

        ParameterManagerClass FPM;

        string FLoadDocError = "";

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="PM">   The Current ParameterManager </param>
        ///-------------------------------------------------------------------------------------------------

        public Extended_Parameter_Documentation(ParameterManagerClass PM)
        {
            // Assign the Parameter Manager
            FPM = PM;
            // Build the various Groups
            BuildGroups();
            // Build the extended Documentation Internal
            BuildDescripList();

        }

        //---------------------------------------------------------------------
        // Topic Groups
       
        // Supply
        public ModelParameterGroupClass WaterSupplyGroup;

        /// <summary>  List of Supply Related ModelParameters <remarks> Used for Topic sorting</remarks> </summary>
        internal int[] WaterSupplyGroupList = new[] {
                eModelParam.epPCT_Reclaimed_to_Water_Supply,
                eModelParam.epPCT_SurfaceWater_to_WaterBank,
                eModelParam.epTotalReclaimedUsed,
                eModelParam.epTotalReclaimedCreated_AF,
                eModelParam.epTotalSupplyUsed,
                eModelParam.epUse_SurfaceWater_to_WaterBank,
                eModelParam.epWaterAugmentation,
                eModelParam.epWaterAugmentationUsed,
        };



        /// <summary>
        ///  Surface Water Group
        /// </summary>
        public ModelParameterGroupClass SurfaceWaterGroup;
    
        /// <summary>
        ///  Salt Verde SUrface Water Group 
        /// </summary>
        public ModelParameterGroupClass SRVSurfaceWaterGroup;

        // list of Altverde surface water parameters
        internal int[] SRVSurfaceWaterGroupList = new[] {
                eModelParam.epSaltVerde_Annual_Deliveries_SRP,
                eModelParam.epSaltVerde_Class_BC_Designations
        };

        public ModelParameterGroupClass GroundWaterGroup;

        internal int[] GroundWaterGroupList = new[] {
            eModelParam.epGroundwater_Pumped_Municipal,
            eModelParam.epGroundwater_Balance,
            eModelParam.epGroundwater_Bank_Used,
            eModelParam.epGroundwater_Bank_Balance,
            eModelParam.epWaterBank_Source_Option,
            eModelParam.epPCT_SurfaceWater_to_WaterBank,
            eModelParam.epUse_SurfaceWater_to_WaterBank,
            eModelParam.epRegionalAgOtherPumping,
            eModelParam.epRegionalGWBalance,
            eModelParam.epProvider_WaterFromAgPumping,
            eModelParam.epProvider_WaterFromAgPumpingMax,
            eModelParam.epPCT_GWAvailable,
            eModelParam.epYrsGWZero,
            eModelParam.epYearGWGoesZero,
        };

        // Colorado Storage Group
        public ModelParameterGroupClass Colorado_StorageGroup;

        internal int[] Colorado_StorageGroupGroupList = new[] {
          eModelParam.epPowell_Storage,
          eModelParam.epMead_Storage 
        };

        // Salt Verde Storage Group
        public ModelParameterGroupClass SRP_StorageGroup;

        internal int[] SRP_StorageGroupGroupList = new[] {
            eModelParam.epSaltOther_Storage,
            eModelParam.epRoosevelt_Storage,
            eModelParam.epVerde_Storage
        };

        public ModelParameterGroupClass Reservoir_Storage;

        // Salt Verde River flows
        public ModelParameterGroupClass SRP_River_Flow;

        internal int[] SRP_River_FlowGroupList = new[] {
            eModelParam.epSaltTonto_AnnualFlow,
            eModelParam.epVerde_AnnualFlow
        };

        public ModelParameterGroupClass Colorado_River_FlowGroup;
        // Demand

        // Provider

        // Base

        // Sustainable

       // Model COntrol
        public ModelParameterGroupClass Model_ControlGroup;

        // Population

        public ModelParameterGroupClass PopulationGroup;

        // Demand

        public ModelParameterGroupClass DemandGroup;
        // Climate

        public ModelParameterGroupClass ClimateGroup;

        // System 

        public ModelParameterGroupClass UrbanSystemGroup;

        // Inidcator

        public ModelParameterGroupClass IndicatorGroup;

        // feedback
        // 

        public ModelParameterGroupClass FeedbackGroup;

        // Policy
        // 
        public ModelParameterGroupClass PolicyGroup;

        public ModelParameterGroupClass Policy_Demand_Group;

        public ModelParameterGroupClass Policy_UrbanSystem_Group;


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the external documentation error. </summary>
        ///
        /// <value> The external documentation error. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ExternalDocumentationError
        {
            get { return FLoadDocError; }
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Model Parameter Description </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///
        /// <returns> string </returns>
        ///-------------------------------------------------------------------------------------------------

        public string Description(int modelparam)
        {
            return FindDescription(modelparam);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Units of Model Parameter </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public string Unit(int modelparam)
        {
            return FindUnit(modelparam);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Units of Model Parameter as Long Description. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public string LongUnit(int modelparam)
        {
            return FindLongUnit(modelparam);
        }

        /// <summary>
        /// A Label to use with a Web Interface, shorter than regular label
        /// </summary>
        /// <remarks> If this is not available, returns an "" empty string</remarks>
        /// <param name="modelparam"></param>
        /// <returns></returns>
        public string WebLabel(int modelparam)
        {
            return FindWebLabel(modelparam);
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Topic groups. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public List<ModelParameterGroupClass> TopicGroups(int modelparam)
        {
            return FindGroup(modelparam);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Web scale. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///<remarks>returns null if modelparam not found</remarks>
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public string[] WebScale(int modelparam)
        {
            return FindWebScale(modelparam);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Web scale value. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///<remarks>returns null if modelparam not found</remarks>
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public int[] WebScaleValue(int modelparam)
        {
            return FindWebScaleValue(modelparam);
        }


        internal WaterSimDescripItem FindDescripDoc(int modelparam)
        {
            return MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
        }

        // Fielname for external documentation
        const string ExternalFileName = "App_data\\DocumentationItems.txt";

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Loads external documentation. </summary>
        ///
        /// <param name="DataPath"> Full pathname of the data file. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool LoadExternalDocumentation(string DataPath)
        {
            // set up for failure!
            bool result = false;
            // build path
            string ExternalFileNamePath = DataPath +  ExternalFileName;
            // OK, going out 
            try
            {
                // get all the lines in the file
                string[] AllLines = System.IO.File.ReadAllLines(ExternalFileNamePath);
                // Setup for success now
                result = true;
                bool isChanged = false;
                // cycle through and execute Documentation command
                foreach (string linestr in AllLines)
                {
                    // process command
                    if (ProcessExternalDocItem(linestr))
                    {
                        isChanged = true;
                    }
                }

                // all done processing
            }
            // opps some heavy error here!
            catch (Exception e)
            {
                FLoadDocError = e.Message;
            }
            // All done, head back
            return result;
        }

        //----------------------------------------------------------------
        internal string ParseFirstPair(string PairList, ref string NextPairs)
        {
            string APair = "";
            if (PairList != "")
            {
                int start = PairList.IndexOf("{");
                if (start > -1)
                {
                    int end = PairList.IndexOf("}", start + 1);
                    if (end>-1)
                    {
                        APair = PairList.Substring(start+1,(end-start)-1);
                        NextPairs = PairList.Substring(end + 1).Trim();
                    }
                }
            }
            return APair;
        }
        //---------------------------------------------------------
        internal string TrimQuotes(string source)
        {
            string temp = "";
            int q1 = source.IndexOf("\"");
            if (q1 > -1)
            {
                int q2 = source.IndexOf("\"",q1+1);
                if (q2 > q1)
                {
                    temp = source.Substring(q1+1,(q2-q1)-1);
                }
            }
            return temp;
        }
        //---------------------------------------------------------
        internal bool ParsePair(string aPair, ref string Name, ref string Value)
        {
            bool result = false;
            // find the colon
            int colindex = aPair.IndexOf(":");
            if (colindex > 0)
            {
                // if there, everything before os the name (TRIMMED)
                Name = aPair.Substring(0, colindex).Trim();
                // find value markers
                int qindex = aPair.IndexOf("\"");
                int bindex = aPair.IndexOf("[");
                // ok, is this an array or a single value
                if ((bindex > colindex) && (bindex < qindex))
                {
                    // assume this is an array value
                    int arraylen = aPair.Length - (bindex + 3);
                    Value = aPair.Substring(qindex + 1, arraylen);
                    
                }
                else
                {   // suume this is a single value
                    if ((qindex > colindex) && (qindex < aPair.Length - 1))
                    {
                        int valuelen = aPair.Length - (qindex + 2);
                        Value = aPair.Substring(qindex + 1, valuelen);
                    }
                    else
                    {
                        Value = "";
                    }
                }
                if (Value.Length > 0)
                    result = true;
            }

            return result;
        }
        //----------------------------------------------------------------
        internal string[] ExtractArray(string aPairValue)
        {
            List<string> aElements = new List<string>();
            int start = 0;
            int comindex = aPairValue.IndexOf(",");
            while (comindex != -1)
            {
                string temp = TrimQuotes(aPairValue.Substring(start, comindex - start));
                aElements.Add(temp);
                start = comindex + 1;
                comindex = aPairValue.IndexOf(",", start);
            }
            if (start < aPairValue.Length)
            {
                string temp = TrimQuotes(aPairValue.Substring(start));
                aElements.Add(temp);
            }
            string[] ScaleArray = new string[aElements.Count];
            for (int i = 0; i < aElements.Count; i++)
            {
                ScaleArray[i] = aElements[i];
            }
            return ScaleArray;
        }
        //----------------------------------------------------------------
        internal int[] ConvertToIntArray(string[] StringArray)
        {
            int[] values = new int[StringArray.Length];
            for(int i=0;i<StringArray.Length;i++)
            {
                int temp = 0;
                if (int.TryParse(StringArray[i], out temp))
                {
                    values[i] = temp;
                }
                else
                {
                    values[i] = int.MinValue;
                }
            }
            return values;
        }
        // 
        internal bool ProcessExternalDocItem(string itemText)
        {
            bool result = false;
            List<string> Items = new List<string>();
            // get all the pairs
            string temp = itemText;
            while (!string.IsNullOrEmpty(temp))
            {
                string item = ParseFirstPair(temp, ref temp);
                if (!string.IsNullOrEmpty(item))
                {
                    Items.Add(item);
                }

            }
            string aPairName = "";
            string aPairValue = "";
            // get first pair
            if (ParsePair(Items[0],ref aPairName,ref aPairValue))
            {
                //if fieldname then proceed otherwise do nothing
                if (aPairName.ToUpper() == "FIELDNAME")
                {
                    // ok fetch the fieldname and get parameter
                    string fieldname = aPairValue;
                    // set up
                    ModelParameterClass MP = null;
                    // throws exception if bad so wrap in try
                    try
                    {
                        MP = FPM.Model_Parameter(fieldname);
                    }
                    catch
                    {
                        MP = null;
                    }

                    // ok check if found
                    if (MP != null)
                    {
                        // get the descriptitem
                        WaterSimDescripItem DI = FindDescripItem(MP.ModelParam);
                        // if found continue
                        if (DI != null)
                        {
                            // loop threw all the rest
                            for (int i = 1; i < Items.Count; i++)
                            {
                                // parse it
                                bool test = ParsePair(Items[i], ref aPairName, ref aPairValue);
                                // if parse ok , then take action on Name
                                if (test)
                                {
                                    switch (aPairName.ToUpper())
                                    {
                                        case "DESCRIP":
                                            DI.Description = aPairValue;
                                            result = true;
                                            break;
                                        case "UNIT":
                                            DI.Unit = aPairValue;
                                            result = true;
                                            break;
                                        case "LONGUNIT":
                                            DI.LongUnit = aPairValue;
                                            result = true;
                                            break;
                                        case "WEBLABEL":
                                            DI.WebLabel = aPairValue;
                                            result = true;
                                            break;
                                        case "WEBSCALE":
                                            // this comes as an array {"NN","NN",..."NN"}
                                            // ok now parse the array
                                            DI.WebScale = ExtractArray(aPairValue);
                                            result = true;
                                            break;
                                        case "WEBSCALEVALUES":
                                            // this comes as an array {"AAA","AAA",..."AAA"}
                                            DI.WebScaleValue = ConvertToIntArray( ExtractArray(aPairValue));
                                            result = true;
                                            break;

                                    } // switch aPairName
                                } // if test
                            } // for i items.count
                            if (result)
                            {
                                MP.setupExtended();
                            }
                        } // if DI
                    } //if MP
                
                } // if Fieldname
            } // ParsePair
            return result;
        }

        internal WaterSimDescripItem FindDescripItem(int modelparam)
        {
            return MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
        }

        /// <summary>
        /// Fetches the description from the documentation for this model param
        /// </summary>
        /// <param name="modelparam"></param>
        /// <returns></returns>
        internal string FindDescription(int modelparam)
        {
            string temp = "";

//            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.Description;

            return temp;
        }
        /// <summary>
        /// Fetches the short unit description from the documentstion
        /// </summary>
        /// <param name="modelparam"></param>
        /// <returns></returns>
        internal string FindUnit(int modelparam)
        {
            string temp = "";

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.Unit;
            return temp;
        }


        /// <summary>
        /// Fethces the long units description from the documentation
        /// </summary>
        /// <param name="modelparam"></param>
        /// <returns></returns>
        internal string FindLongUnit(int modelparam)
        {
            string temp = "";

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.LongUnit;
            return temp;
        }

        /// <summary>
        /// Fethces the Web label from documention for this modelparam
        /// </summary>
        /// <param name="modelparam"></param>
        /// <returns></returns>
        internal string FindWebLabel(int modelparam)
        {
            string temp = "";

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.WebLabel;
            return temp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first web scale. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///<remarks>Returns Null if ModelParam not found</remarks>
        /// <returns>   The found web scale. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal string[] FindWebScale(int modelparam)
        {
            string[] temp;

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.WebScale;
            else
                temp = null;
            return temp;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Searches for the first web scale value. </summary>
        ///
        /// <param name="modelparam">   The modelparam. </param>
        ///<remarks>Returns Null if ModelParam not found</remarks>
        /// <returns>   The found web scale value. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal int[] FindWebScaleValue(int modelparam)
        {
            int[] temp;

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
                temp = DI.WebScaleValue;
            else
                temp = null;
            return temp;
        }

        internal List<ModelParameterGroupClass> FindGroup(int modelparam)
        {

            //            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            WaterSimDescripItem DI = FindDescripItem(modelparam);
            if (DI != null)
            {
                return DI.TopicGroups;
            }
            else
            {
                return new List<ModelParameterGroupClass>();
            }
        }


        internal void BuildGroups()
        {
            // groundwater group
            GroundWaterGroup = new ModelParameterGroupClass("Groundwater Supply Group",GroundWaterGroupList);
            FPM.GroupManager.Add(GroundWaterGroup);
            // Salt Verde Surface water group
            SRVSurfaceWaterGroup = new ModelParameterGroupClass("SRV Surface Water", SRVSurfaceWaterGroupList);
            FPM.GroupManager.Add(SRVSurfaceWaterGroup);
            
            // Total Surface Water Group
            SurfaceWaterGroup = new ModelParameterGroupClass("Surface Water");
            SurfaceWaterGroup.Add(SRVSurfaceWaterGroup);
            SurfaceWaterGroup.Add(eModelParam.epColorado_Annual_Deliveries);
            SurfaceWaterGroup.Add(eModelParam.epProvider_WaterFromAgSurface);
            SurfaceWaterGroup.Add(eModelParam.epProvider_WaterFromAgSurfaceMax);

            FPM.GroupManager.Add(SurfaceWaterGroup);
 
            // Total Water Supply Group
            WaterSupplyGroup = new ModelParameterGroupClass("Water Supply", WaterSupplyGroupList);
            WaterSupplyGroup.Add(SurfaceWaterGroup);
            WaterSupplyGroup.Add(GroundWaterGroup);
            FPM.GroupManager.Add(WaterSupplyGroup);
 
            // Colorado STorage Gorup
            Colorado_StorageGroup = new ModelParameterGroupClass("Colorado Reservoir Storage", Colorado_StorageGroupGroupList); 
            FPM.GroupManager.Add(Colorado_StorageGroup);

            // Salt Verde Storage Group
            SRP_StorageGroup      = new ModelParameterGroupClass("Salt Verde Reservoir Storage", SRP_StorageGroupGroupList);
            FPM.GroupManager.Add(SRP_StorageGroup);

            // Total Reservoir Storage
            Reservoir_Storage = new ModelParameterGroupClass("Total Reservoir Storage");
            Reservoir_Storage.Add(Colorado_StorageGroup);
            Reservoir_Storage.Add(SRP_StorageGroup);
            FPM.GroupManager.Add(Reservoir_Storage);

            SRP_River_Flow = new ModelParameterGroupClass("Total Salt/Verde River Flow",SRP_River_FlowGroupList);
            FPM.GroupManager.Add(SRP_River_Flow);

            Colorado_River_FlowGroup = new ModelParameterGroupClass("Colorado River FLow");
            FPM.GroupManager.Add(Colorado_River_FlowGroup);

            // Model Control
            Model_ControlGroup = new ModelParameterGroupClass("Model Control");
            FPM.GroupManager.Add(Model_ControlGroup);

            // Climate

            ClimateGroup = new ModelParameterGroupClass("Climate");
            FPM.GroupManager.Add(ClimateGroup);

            PopulationGroup = new ModelParameterGroupClass("Population");
            FPM.GroupManager.Add(PopulationGroup);

            DemandGroup = new ModelParameterGroupClass("Demand");
            DemandGroup.Add(PopulationGroup);

            FPM.GroupManager.Add(DemandGroup);

            UrbanSystemGroup = new ModelParameterGroupClass("Urban System");
            FPM.GroupManager.Add(UrbanSystemGroup);

            IndicatorGroup = new ModelParameterGroupClass("Sustainable Indicators");
            FPM.GroupManager.Add(IndicatorGroup);

            FeedbackGroup = new ModelParameterGroupClass("Feedback");
            FPM.GroupManager.Add(FeedbackGroup);

            Policy_Demand_Group = new ModelParameterGroupClass("Policy Demand");
            FPM.GroupManager.Add(Policy_Demand_Group);

            Policy_UrbanSystem_Group = new ModelParameterGroupClass("Policy System");
            FPM.GroupManager.Add(Policy_UrbanSystem_Group);

            PolicyGroup = new ModelParameterGroupClass("Policy");
            PolicyGroup.Add(Policy_Demand_Group);
            PolicyGroup.Add(Policy_UrbanSystem_Group);
            FPM.GroupManager.Add(PolicyGroup);
        }

        internal void BuildDescripList()
        {

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSimulation_Start_Year, "The first year of the Simulation (NOTE: in this release the start year SHOULD BE 2006 or 2000).", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Model_ControlGroup   }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSimulation_End_Year, "The last year of the Simulation.", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {Model_ControlGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Historical_Extraction_Start_Year, "The first year of the Colorado River flow record that will be used to create a 25 year trace to simulate river flow conditions (from the text file input, the flow record that corresponds to the year chosen, with that year flow and the next 24 years duplicated throughout the entire simulation period).", "Year","Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {ClimateGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Historical_Data_Source, "The source of the Colorado River flow record: Value 1 uses the Bureau of Reclamation recorded record, Value 2 uses the tree ring reconstructed paleo record, Value 3 uses a user supplied river flow trace record (created by the user, and representing the flow for 2011 through 2085 [the historical record is used for 2000-2010]).", "", "Code", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Climate_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Colorado river flow record, simulating impacts of climate change.  Change starts (or impacts the flow record) in any year that the value differs from 100%.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Colorado River flow record (decrease, typically), starting and stopping in the years specified (i.e., User_Adjustment_StartYear, User_Adjustment_StopYear).  This is used to simulate a drought condition. ", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_StartYear, "Determines the year that the [Colorado User Adjustment %] will be first be applied. ", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_Stop_Year, "Determines the year the [Colorado User Adjustment %] will stop being applied.", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Historical_Extraction_Start_Year, "The first year of the Salt Verde River flow record that will be used to create a 25 year trace to simulate river flow conditions (see above: COEXTSTYR).", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Historical_Data, "The source of the Salt Verde Rivers flow record: Value=1 uses the Bureau of Reclamation recorded record, Value=2 uses the tree ring reconstructed paleo record, Value=3 uses a user supplied river flow trace record (created by the user, and representing the flow for 2011 through 2085[the historical record is used for 2000-2010]).", "", "Code", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Climate_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, simulating impacts of climate change.  Change starts at beginning of Simulation (or in any year where the value departs from 100%).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, starting and stopping in the years specified.  This is used to (typically) simulate a drought condition. ", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Start_Year, "Determines the year the [SaltVerde User Adjustment %] will first be applied. ", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Stop_Year, "Determines the year the [SaltVerde User Adjustment %] will stop being applied.", "Year", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { ClimateGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_REG_Growth_Rate_Adjustment, "For all providers adjusts the growth rate for areas on and off project to this value.  100 (100%) leaves the rate at the projected rate.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvider_Demand_Option, "The method that will be used to estimate annual demand  for all providers.  Value=1 reads demand values from an external file, Value=2 calculates demand based on a six year average GPCD and population (separate for each water provider) read from a file, Value=3 estimates demand based on population estimates read from an external file and a smoothing function that slowly declines GPCD (i.e., using ReduceGPCDpct), Value=4 uses same method as 3, but allows the user to manually chanage the GPCD used for each provider at any time; please note that once a change is made that value is maintained throughout the simulation.", "", "Code", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epPCT_Reduce_GPCD,"The amount by which GPCD is expected to decrease by the end of the simulation (i.e., 2085). Use this when Provider Demand Option is = 3 or Provider Demand Option=4.", "%","Percentage",""));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epModfyNormalFlow, "This parameter was added to the model when it was determined that not all class A (normal flow) water is used by a provider on a given day and, thus, for the year.  This variable simply adjusts the Trott Table estimated designations for each provider for each threshold of river flow. This is done at the start of the simulation.  Units: acre feet per acre x 10 were needed because we are using integers (this is  float as used). That is, a user would enter 15 for 1.5 AF acre-1.  or 9 for a 0.9 AF acre-1., etc. (note true upper estimate is 5.4288 [see Kent Decree]).", "AF*10", "acre feet per acre * 10", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvider_WaterFromAgSurface, "Water can be removed from the Phoenix AMA (SRV) estimate of agricultural surface water provided by Dale Mason (Fall 2011). This water is added to a water providers groundwater designation", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup, SurfaceWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvider_WaterFromAgPumping, "Water can be removed from the Phoenix AMA (SRV) estimate of agricultural pumping of groundwater provided by Dale Mason (Fall 2011). This water is added to a water providers groundwater designation", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_GPCD, "The GPCD that will be used if [Provider Demand Option] is set to Value=4.", "GPCD", "Gal / Capita / Day", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup , Policy_Demand_Group}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Wastewater_to_Effluent, "The percent of  Waste water effluent that is used and not discharged into a water course (note: if PCEFFREC [below] is set to 100% no waste water is sent to the traditional WWTP and, so, no effluent will be available for partitioning).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WasteWater_to_Reclaimed, "The percent of  Waste water effluent that is sent to a Reclaimed Plant (versus a traditional plant-see figure 1).", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_RO, "The percent of  reclaimed water that is sent to a Reverse Osmosis Plant (thus becomming potable water for direct injection or potable water for use in the next time-step).", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_RO_to_Water_Supply, "The percent of  water from Reverse Osmosis Plant that is used for potable water.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_DirectInject, "The percent of  reclaimed water that is used to recharge an aquifer by direct injection into an aquifer.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Max_Demand_Reclaim, "The amount of (percent of demand that can be met by) reclaimed water that WILL be used as input for the next year.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_Water_Supply, "The percent of  reclaimed water that is used to meet qualified user demands (non-potable).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_Vadose, "The percent of  reclaimed water that is delivered to a vadoze zone recharge basin.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Effluent_to_Vadose, "The percent of  wastewater effluent delivered to a vadose zone recharge basin.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Effluent_to_PowerPlant, "The percent of  wastewater effluent delivered to a power plants for cooling towers.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSurfaceWater__to_Vadose, "The amount of surface water supply delivered to a vadose zone basin.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSurface_to_Vadose_Time_Lag, "The time in years it takes water recharged to the vadose zone to reach the aquifer to be used as  groundwater.", "yrs", "Years", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterBank_Source_Option, "The source of water used for external water banking (outside provider groundwater): Value=1 a percent [% SurfaceWater to WaterBank] of 'unused' surface water is sent to a water bank, Value= 2 a fixed amount[Amount of SurfaceWater to WaterBank] of an unknown extra source of water is sent to a water bank.", "", "Code", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_SurfaceWater_to_WaterBank, "The percent of extra [excess] surface water that is sent to a water bank if [WaterBank Source Option] is set to a Value=1.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_SurfaceWater_to_WaterBank, "The amount of water (source unknown) sent to a water bank if [WaterBank Source Option] is set to a Value=2.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Residential, "The percent of total water supply used by residential customers.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Commercial, "The percent of  total water supply used by commercial users", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Industrial, "The percen of total water supply used by industrial users", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_WaterSupply_to_DirectInject, "A fixed amount of potable water supply used for aquifer recharge by directly injecting into a potable aquifer.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseRes, "The percent of  potable water supply used for outdoor water uses Residential (indoor water use = 1 - outdoor use).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseCom, "The percent of  potable water supply used for outdoor water uses Commercial (indoor water use = 1 - outdoor use).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseInd, "The percent of  potable water supply used for outdoor water uses Industrial (indoor water use = 1 - outdoor use).", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Groundwater_Treated, "The percent of  groundwater that is treated before it is used for potable water supply.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_Outdoor_Use, "The percent of  reclaimed water to be used outdoors. If all available reclaimed water is not used outdoors (i.e., not 100%) it is used indoors as black water.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Growth_Rate_Adjustment_Other, "For each provder adjusts the growth rate for areas off project (other) to this value.  100 (100%) leaves the rate at the projected rate.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, PopulationGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Growth_Rate_Adjustment_OnProject, "For each provider adjusts the growth rate for areas on project (SRP Lands) to this value.  100 (100%) leaves the rate at the projected rate.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, PopulationGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSetPopulationsOn, "For each provider sets the population for areas off project (other) to this value. ", "Pers", "People", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup , PopulationGroup, Policy_Demand_Group}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSetPopulationsOther, "For each provider sets the population for areas on project (SRP lands) to this value. ", "Pers", "People", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup , PopulationGroup, Policy_Demand_Group}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_River_Flow, "The total annual flow in the Colorado River above Lake Powell (the record from  Lees Ferry).", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_River_FlowGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPowell_Storage, "The total water storage in Lake Powell.", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMead_Storage, "The total water storage in Lake Mead", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_River_Flow, "The total annual flow of the Salt and Verde (and Tonto) Rivers.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_River_Flow }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltTonto_AnnualFlow, "The total annual flow of the Salt River and Tonto Creek.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_River_Flow }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epVerde_AnnualFlow, "The total annual flow of the Verde River and Tonto Creek.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_River_Flow }));

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Storage, "The total annual storage in the Salt River Project reservoirs.", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Spillage, "Spill water over the SVT system- all reservoirs", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_Agriculture, "The total amount of wastewater effluent delivered to agriculural users.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadLevel, "Elevation of Lake Mead's water surface", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epBasinPumpage,"Salt River Valley pumpage over all model grids", "",new string[] {},new int[] {},""));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epBasinNetChange,"Net change in the basin over all model grids", "",new string[] {},new int[] {},""));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Pumped_Municipal, "The total amount of annual groundwater pumped.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Balance, "The total groundwater credits available at end of year.", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Annual_Deliveries_SRP, "The total annual surface water and pumped groundwater delivered by SRP.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup, SRVSurfaceWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Class_BC_Designations, "The total annual B & C designated ground water delivered by SRP.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup, SRVSurfaceWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Annual_Deliveries, "The total annual surface water deliveries by CAP, does not included banked water.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup, SurfaceWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Bank_Used, "The total annual amount of water delivered from water banking facilities.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Bank_Balance, "The total banked water supply available at end of year.", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_Used, "The total annual amount of reclaimed water used.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_To_Vadose, "The annual amount of reclaimed water used for vadose zone recharge.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_Discharged, "The annual amount of reclaimed water discharged to a water course (environment).", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_to_DirectInject, "The annual amount of reclaimed water recharged to an aquifer using direct injection.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRO_Reclaimed_Water_Used, "The annual amount of reverse osmosis reclaimed water used for potable water supply.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRO_Reclaimed_Water_to_DirectInject, "The annual amount of reverse osmosis reclaimed water used for aquifer recharge using direct injection.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {},new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_Reused, "The total annual amount of wastewater effluent reused.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_Vadose, "The annual amount of effluent used for vadose zone recharge.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_PowerPlant, "The annual amount of effluent delivered to power plants.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_Discharged, "The annual amount of wastewater effluent discharged to a water course (envirionment).", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDemand_Deficit, "The annual difference between demand and supply (demand - supply), 0 if supply is larger than demand.  This is only valid if pumping is limited to AWS anual allocation", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotal_Demand, "The total annual demand from all water customers.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGPCD_Used, "The GPCD used to estimate demand for the completed simulation year.", "GPCD","Gallons Per Capita Per Day","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOnProjectPopulation, "Population for SRP member lands", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {  DemandGroup, PopulationGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOtherPopulation, "All other population within the provider boundary", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup, PopulationGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPopulation_Used, "The population used (along with GPCD) to estimate demand for the completed simulation year.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup, PopulationGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalGWBalance, "MODFLOW estimates of groundwater on a water provider basis.", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDeficit_Total, "Total amount of annual deficit (Total supply minus total demand when total demand exceeds supply, else 0).", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDeficit_Years, "Number of years to date (cumulative) in which there has been a deficit (not enough water supply to meet demand)", "yrs", "Years","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_GWAvailable, "Percent of original groundwater credits that are available each year.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYrsGWZero, "Number of years to date (cumulative) that groundwater credits have been zero or less.", "yrs","Years","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearGWGoesZero, "The earliest year that groundwater credits became zero (or less).", "Year","Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsNotAssured, "Number of years to date (cumulative) that a 100 year Assured Water Supply could not be demonstrated.", "yrs", "Years", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPercentDeficitLimit, "Percent deficit trigger used to begin reducing GPCD to respond to deficit.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMinimumGPCD, "Lowest value to which GPCD can be reduced.","GPCD", "Gallons Per Capita Per Day","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsOfNonAWSTrigger, "Number of years with not being able to demonstrate 100 year Assured Water Supply before some policy action occurs.", "yrs", "Years", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsOfNonAWSGPCDTrigger, "Number of years with not being able to demonstrate 100 year Assured Water Supply before GPCD is reduced.", "Yr","Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalSupplyUsed, "The total of all supply sources used to meet demand.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTGWofDemand, "The percent of demand that is met by ground water pumping.", "%F","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalReclaimedUsed, "The total amount of reclaimed effluent produced.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvderEffluentToAg, "The amount of effluent that is used for agricutural.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalEffluent, "The total amount of effluent produced (does not include reclaimed).", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { UrbanSystemGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedOnProjectPop, "The annual population initially projected for SRP membership lands.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup, PopulationGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedOtherPop, "The annual population initially projected for non-SRP membership lands.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup, PopulationGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedTotalPop, "The total annual population initially projected.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { DemandGroup, PopulationGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceProjectedOnPop, "The deviation (difference) between annual projected population and population used for SRP membership lands.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceProjectedOtherPop, "The deviation (difference) between annual projected population and population used for non-SRP membership lands.", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceTotalPop, "The deviation (difference) between annual total projected population and total population used ", "Pers","People","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedOnPop, "The percent deviation (difference) between annual projected population and population used for SRP membership lands.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedOtherPop, "The percent deviation (difference) between annual projected population and population used for non-SRP membership lands.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedTotalPop, "The percent deviation (difference) between annual total projected population and total population used ", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup, FeedbackGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPctRecOfTotal,"The percent of reclaimed water used to meet demand annually.","%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epCreditDeficits, "The annual negative balance in groundwater credits, if groundwater credits are greater than 0, this value is 0.", "AF","Acre Feet","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPctRecOfTotal, "The annual amount of reclaimed water used as a percent of total supply.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Deficit, "The annual total deficit (how much demand exceeds supply) as a percent of total demand.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterAugmentation, "The annual amount of water supply available from a new source of water not included in the original water portfolio.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[1] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalNaturalRecharge, "The amount of water that is naturally recharged to the regional aquifer annually.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalCAGRDRecharge, "The amount of water that the Central Arizona Groundwater Replishment District recharges to the regional aquifer annually.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalInflow, "The amount of water the moves into the regional aquifer annually from other aquifers.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalAgToVadose, "The amount of water used by aggriculture irrigation that ends up recharging the regional aquifer annually.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalProviderRecharge, "The total amount of water that is recharged to the aquifer annually by all water providers.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalAgOtherPumping, "The total amount of water that is pumped from the regional aquifer annually by all non-water provider users (agricultural and other).", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { GroundWaterGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalOutflow, "The total amount of water that annually leaves the regional aquifer as stream flow or flow to another aquifer.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { GroundWaterGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOnProjectDemand, "The annual demand from SRP member lands.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOffProjectDemand, "The annual demand from non-SRP member lands.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epVadoseToAquifer, "The total amount of water that is added annually to the regional aquifer from vadose recharge.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAnnualIncidental, "The amount of water that is added to groundwater credits annually to account for incidental (outdoor water use) aquifer recharge.", "AF/yr","Acre Feet per Year","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {GroundWaterGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAlterGPCDpct , "The goal in percent reduction in Gallons per Capita per Day to achieve by the end of the simulation run. 100 = no reduction, 80 = 20% reduction.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] {PolicyGroup, DemandGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Alter_GPCD, "The regional goal in percent reduction in Gallons per Capita per Day to achieve by the end of the simulation run. 100 = no reduction, 80 = 20% reduction.", "%","Percentage","",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAWSAnnualGWLimit, "Indicates if the AWS rule to limit annual pumping to AWS designated annual credits should be applied (Code=0) or ignored (Code=1)", "Code", "Code", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {PolicyGroup, GroundWaterGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegPctGWDemand, "Regional Sustainability Indicator:The percent of demand met by groundwater pumping.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGWYrsSustain, "Provider Sustainability Indicator:Years that provider groundwater pumping can be sustained until total groundwater credits reach zero.", "Yr", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegGWYrsSustain, "Regional Sustainability Indicator:Average years for the region that provider groundwater pumping can be sustained until total groundwater credits reach zero.", "Yr", "Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAgSustainIndicator, "Regional Sustainability Indicator: The amount of water agriculture uses as a percentage of total water rights that could be used for agriculture.", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvSustainIndicator, "Regional Sustainability Indicator: The amount of water left in the Colorado and Salt-Verde Rivers as a percent of a maximum amount that could be allocated", "%", "Percentage", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { IndicatorGroup}));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterUseIndicator, "Regional Sustainability Indicator: The regional average water use expressed on a scale of 0 to 100 calculated as ((GPCD/30)+1))*100)/9 with GPCDs over 299 = 100", "0-100", "0 Very Low to 100 Very High", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] {IndicatorGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPowellLevel, "Elevation of of Lake Powell's water surface", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup}));

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent, "Allocates effluent to agriculture, urban reuse, environment, or aquifer recharge based on a scale of 0 to 100, with 100 allocating more water to agriculture and urban reusem=, and 0 allocating more water to the environment and aquifer recharge", "0-100","0 environment/recharge to 100 agriculture/urban reuse",
                //   "NNAA5678901234567890123456789012345 ,new string[] {},new int[] {}
                "% Effluent Used for Environment",new string[] {},new int[] {}, new ModelParameterGroupClass[] {PolicyGroup })); // "NNAA5678901234567890123456789012345"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent_Ag, "Allocates effluent to agriculture based on a scale of 0 to 100, with 100 allocating more water to agriculture and 0 allocating less", "0-100", "0 low to 100 high",
                "% Effluent Used for Farming",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent_Env, "Allocates effluent to environment based on a scale of 0 to 100, with 100 allocating more water to environment and 0 allocating less", "0-100", "0 low to 100 high",
                "% Effluent Used for Environment",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIAgriculture, "Transfers water rights from agriculture to urban water providers, based on a scale of 0 to 100, with 100 allocating more water to agriculture and 0 allocating less", "0-100", "0 low to 100 high",
                "% Farming Water Used for Urban",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebPop_GrowthRateAdj_PCT, "Adjustes the population growth rate for all water providers on a scale of 0-150, with 0-no growth to 150-50% increase in growth rate", "0-150","0-0% to 150-150%",
                "% Change in Projected Growth Rate",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, PopulationGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIPersonal_PCT, "Adjusts the Gallons per Capita per Day for all water providers on a scale of 0 to 100, with zero a decline to 50 GPCD and 100 a decline based on past trends.", "0-100","0-Repid decline to 100-Current trends",
                "% Change in Per Capita Water Use",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, DemandGroup, Policy_Demand_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebWaterBank_PCT, "Store this percent of excess surface water in a longerterm water bank.", "%", "Percentage", "% Excess Water to Bank",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvironmentalFlow_PCT, "Allocates water for the Colorado River Delta based on a scale of 0 to 100 % of the AZ share, with 0=no water and 100=23% of 158088 AF/yr.", "% of AZ share", "0=0 af/yr to 100=23% of 158,088 af/yr", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvironmentalFlow_AFa, "Allocates a specified amount of water for envirionmental by leaving it in the rivers", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterAugmentationUsed, "Amount of water that is used from augmented water supplies.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadDeadPool, "The elevation at which stored water can no longer be physcally released from Lake Mead.", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadLowerSNWA, "The elevation of the lower intake the Southern Nevada Water Authority uses to withdraw water from Lake Mead", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadShortageSharingLow, "The last elevation that may trigger shortage sharing among lower basin states as per the 2007 Seven Basin States Shortage Sharing Agreement", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadShortageSharingMed, "The second elevation that may trigger shortage sharing among lower basin states as per the 2007 Seven Basin States Shortage Sharing Agreement", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadShortageSharingHigh, "The first elevation that may trigger shortage sharing among lower basin states as per the 2007 Seven Basin States Shortage Sharing Agreement", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { PolicyGroup, Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadMinPower, "The minimum elevation at which the hydraulic turbines at Lake Mead can produce power.", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadCapacity, "The maximum elevation that water can be stored in Lake Mead, after which water will spill from the dam.", "Ft-msl", "Feet (Mean Sea Level)", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { Colorado_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalReclaimedCreated_AF, "The total amount of reclaimed water that was produced.", "AF/yr", "Acre Feet per Year", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { WaterSupplyGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltOther_Storage, "The Acre Fet of water stored in reservoirs of the Salt River other than Lake Roosevelt", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRoosevelt_Storage, "The Acre Fet of water stored in Lake Roosevelt reservoir", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epVerde_Storage, "The Acre Fet of water stored in reservoirs on the Verde River", "AF", "Acre Feet", "",new string[] {},new int[] {}, new ModelParameterGroupClass[] { SRP_StorageGroup }));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebReclaimedWater_PCT, "Sets Regional Wastewater to Reclaimed Values for all providers", "%", "% Wastewater Reclaimed", "% of Wastewater Reclaimed", new string[] { }, new int[] { }, new ModelParameterGroupClass[] { PolicyGroup, UrbanSystemGroup, Policy_UrbanSystem_Group }));
            //MPDesc.Add(new WaterSimDescripItem(eModelParam , "", ""));
            //MPDesc.Add(new WaterSimDescripItem(eModelParam , "", ""));
            /*
            // 110,     //         epSaltOther_Storage;
            111,     //         epRoosevelt_Storage;
            112,     //         epVerde_Storage;

             */
        }
    }
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Model documentation. </summary>
    ///<Remark>This is the class used to create documentation for Model,Parameters, and Processes</Remark>
    ///-------------------------------------------------------------------------------------------------

    public static class ModelDocumentation
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Model parameter spatial string. </summary>
        ///
        /// <param name="mpt">  The mpt. </param>
        ///
        /// <returns>   String f. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ModelParamSpatialString(modelParamtype mpt)
        {
            string value = "";
            switch (mpt)
            {
                case modelParamtype.mptInput2DGrid:
                case modelParamtype.mptInput3DGrid:
                case modelParamtype.mptInputBase:
                    value = "Region";
                    break;
                case modelParamtype.mptInputOther:
                    value = "Other";
                    break;
                case modelParamtype.mptInputProvider:
                    value = "Utility/City";
                    break;
                case modelParamtype.mptOutput2DGrid:
                case modelParamtype.mptOutput3DGrid:
                case modelParamtype.mptOutputBase:
                    value = "Region";
                    break;
                case modelParamtype.mptOutputOther:
                    value = "Other";
                    break;
                case modelParamtype.mptOutputProvider:
                    value = "Utility/City";
                    break;
                case modelParamtype.mptUnknown:
                    value = "unknown";
                    break;
            }
            return value;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Model parameter type string. </summary>
        ///
        /// <param name="mpt">  The mpt. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ModelParamTypeString(modelParamtype mpt)
        {
            string value = "";
            switch (mpt)
            {
                case modelParamtype.mptInput2DGrid:
                    value = "Input 2D Grid";
                    break;
                case modelParamtype.mptInput3DGrid:
                    value = "Input 3D Grid";
                    break;
                case modelParamtype.mptInputBase:
                    value = "Input Base";
                    break;
                case modelParamtype.mptInputOther:
                    value = "Input Other";
                    break;
                case modelParamtype.mptInputProvider:
                    value = "Input Provider";
                    break;
                case modelParamtype.mptOutput2DGrid:
                    value = "Output 2D Grid";
                    break;
                case modelParamtype.mptOutput3DGrid:
                    value = "Output 3D Grid";
                    break;
                case modelParamtype.mptOutputBase:
                    value = "Output Base";
                    break;
                case modelParamtype.mptOutputOther:
                    value = "Output Other";
                    break;
                case modelParamtype.mptOutputProvider:
                    value = "Output Provider";
                    break;
                case modelParamtype.mptUnknown:
                    value = "unknown";
                    break;
            }
            return value;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Model parameter input output string. </summary>
        ///
        /// <param name="mpt">  The mpt. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string ModelParamInputOutputString(modelParamtype mpt)
        {
            string value = "";
            switch (mpt)
            {
                case modelParamtype.mptInput2DGrid:
                case modelParamtype.mptInput3DGrid:
                case modelParamtype.mptInputBase:
                case modelParamtype.mptInputOther:
                case modelParamtype.mptInputProvider:
                    value = "Input";
                    break;
                case modelParamtype.mptOutput2DGrid:
                case modelParamtype.mptOutput3DGrid:
                case modelParamtype.mptOutputBase:
                case modelParamtype.mptOutputOther:
                case modelParamtype.mptOutputProvider:
                    value = "Output";
                    break;
                case modelParamtype.mptUnknown:
                    value = "unknown";
                    break;
            }
            return value;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Create a Model Document Tree. </summary>
        /// <remarks> Creates a DocTreeNode Tree populated with Model information using WSim, tree includes parameters and processes</remarks>
        /// <param name="WSim"> The WaterSimManager node is based on </param>
        /// <returns> a DocTreeNode with model info </returns>
        ///-------------------------------------------------------------------------------------------------

        public static DocTreeNode ModelTree(WaterSimManager WSim)
        {
            DocTreeNode ModelTree = new DocTreeNode("WATERSIM", "");
            DocTreeNode APIVersionNode = new DocTreeNode("APIVERSION", WSim.API_Version);// WSim.APiVersion);
            ModelTree.AddChild(APIVersionNode);
            DocTreeNode ModelVersionNode = new DocTreeNode("MODELVERSION",WSim.Model_Version);// WSim.ModelBuild);
            ModelTree.AddChild(ModelVersionNode);
 
            DocTreeNode temp = ParameterTree(WSim.ParamManager, "");
            ModelTree.AddChild(temp);
            temp = ProcessTree(WSim.ProcessManager);
            ModelTree.AddChild(temp);

            return ModelTree;
        }

        ///---------------------------------------------------------------*----------------------------------
        /// <summary>   Create a Parameter Document tree. </summary>
        /// <remarks> Creates a DocTreeNode Tree populated with just Parameter information using the ParmManager</remarks>
        /// <param name="ParmManager">  The ParameterManagerClass used to create Node. </param>
        /// <param name="Label">        The label for the node. </param>
        /// <returns>  a DocTreeNode with parameters . </returns>
        ///-------------------------------------------------------------------------------------------------

        public static DocTreeNode ParameterTree(ParameterManagerClass ParmManager, string Label)
        {
            DocTreeNode ParmTree = new DocTreeNode("PARAMETERS", Label);
            foreach (ModelParameterClass MP in ParmManager.BaseInputs())
                ParmTree.AddChild(MP);
            foreach (ModelParameterClass MP in ParmManager.ProviderInputs())
                ParmTree.AddChild(MP);
            return ParmTree;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Create Process Document Tree. </summary>
        /// <remarks> Creates a DocTreeNode Tree populated with just Process information using the ProcManager</remarks>
        /// <param name="ProcManager">  ProcessManager used to create Tree. </param>
        /// <returns> DocTreeNode with Processes. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static DocTreeNode ProcessTree(ProcessManager ProcManager)
        {
            DocTreeNode ProcTree = new DocTreeNode("PROCESSES", "");
            foreach (AnnualFeedbackProcess AFP in ProcManager.AllProcesses())
                ProcTree.AddChild(AFP);
            return ProcTree;
        }
    }
    ///-------------------------------------------------------------------------------------------------
    /// <summary> Document tree node. </summary>
    ///<remarks>Tag must have a value and cannot be equal to ""</remarks>
    ///-------------------------------------------------------------------------------------------------

    public class DocTreeNode
    {
        string FTag = "";
        string FCode = "";
        string FFieldname = "";
        string FValue = "";
        string FLabel = "";
        static int FCount = 0;
        List<DocTreeNode> FChildren = new List<DocTreeNode>();

        DocTreeNode FParent = null;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Default constructor. </summary>
        /// <remarks>Tag is set to "TAGN" where N is an internal class counter</remarks>
        ///-------------------------------------------------------------------------------------------------

        public DocTreeNode()
        {
            Tag = "";
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <remarks>If aTag is null or "", the the tag is set to "TAGN" where N is an internal class counter</remarks>
        /// <param name="aTag">   The tag. </param>
        /// <param name="aValue"> The value. </param>
        ///-------------------------------------------------------------------------------------------------

        public DocTreeNode(string aTag, string aValue)
        {
            Tag = aTag;

            FValue = aValue;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <remarks>If aTag is null or "", the the tag is set to "TAGN" where N is an internal class counter</remarks>
        /// <param name="aTag">   The tag. </param>
        /// <param name="aValue"> The value. </param>
        /// <param name="aLabel"> The label. </param>
        ///-------------------------------------------------------------------------------------------------

        public DocTreeNode(string aTag, string aValue, string aLabel, string aCode, string aFieldname)
        {
            FTag = aTag;
            FValue = aValue;
            FLabel = aLabel;
            FCode = aCode;
            FFieldname = aFieldname;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the tag. </summary>
        /// <remarks>If value is null or "", the the tag is set to "TAGN" where N is an internal class counter</remarks>
        /// <value> The tag. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Tag
        { get {return FTag; }
            set
            {
                if (value == "")
                {
                    FTag = "Tag" + FCount.ToString();
                    FCount++;
                }
                else
                {
                    FTag = value;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the value. </summary>
        ///
        /// <value> The value. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Value 
        { get {return FValue; } set {FValue = value;} }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the label. </summary>
        ///
        /// <value> The label. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Label
        { get { return FLabel; } set { FLabel = value; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the code. </summary>
        ///
        /// <value> The code. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Code
        { get { return FCode; } set { FCode = value; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the fieldname. </summary>
        ///
        /// <value> The fieldname. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Fieldname
        { get { return FFieldname; } set { FFieldname = value; } }


        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the parent. </summary>
        ///
        /// <value> The parent. </value>
        ///-------------------------------------------------------------------------------------------------

        public DocTreeNode Parent
        { get { return FParent; } }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Indexer to get items within this collection using array index syntax. </summary>
       ///
       /// <param name="i"> Zero-based index of the entry to access. </param>
       ///
       /// <returns> The indexed item. </returns>
       ///-------------------------------------------------------------------------------------------------

       public DocTreeNode this[int i]
       {
           get { return FChildren[i]; }
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Gets the List of Child Nodes. </summary>
       ///
       /// <value> The children. </value>
       ///-------------------------------------------------------------------------------------------------

       public List<DocTreeNode> Children
       { get { return FChildren; } }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Adds a child. </summary>
       /// <param name="aNode"> The node. </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChild(DocTreeNode aNode)
       {
           aNode.FParent = this;
           FChildren.Add(aNode);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Adds a child. </summary>
       /// <param name="aTag">   The tag. </param>
       /// <param name="aValue"> The value. </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChild(string aTag, string aValue)
       {
           DocTreeNode temp = new DocTreeNode(aTag, aValue);
           AddChild(temp);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Adds a child. </summary>
       /// <param name="aTag">       The tag. </param>
       /// <param name="aValue">     The value. </param>
       /// <param name="aLabel">     The label. </param>
       /// <param name="aCode">      The code. </param>
       /// <param name="aFieldname"> The fieldname. </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChild(string aTag, string aValue, string aLabel, string aCode, string aFieldname)
       {
           DocTreeNode temp = new DocTreeNode(aTag, aValue, aLabel, aCode, aFieldname);
           AddChild(temp);
       }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets deocumentation in an XML format in a string. </summary>
        /// <param name="Level"> The level of indentention to start with. </param>
        ///
        /// <returns> The XML. </returns>
        ///-------------------------------------------------------------------------------------------------

        public string GetXML(int Level)
       {
           string temp = "<" + FTag;
           temp.PadLeft(Level * 3, ' ');
            if (FLabel !="")
                temp += " label='"+FLabel+"' ";
            if (FCode!="")
                temp += "code='"+FCode+"' ";
            if (FFieldname!="")
                temp += "fieldname='"+FFieldname+"' ";

            temp += ">";
            temp += ' '+ FValue.PadLeft(3*Level,' ');
            
           if (FChildren.Count>0)
           {
               Level++;
               foreach(DocTreeNode Node in FChildren)
               {
                   temp += Node.GetXML(Level);
               }
           }
           string close = "</" + FTag + ">\r\n";
           temp += close.PadLeft(3 * Level, ' ');
           return temp;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Gets documentation as A TreeNode  object for a TreeView control. </summary>
       ///
       /// <returns> The tree node. </returns>
       ///-------------------------------------------------------------------------------------------------

       public TreeNode GetTreeNode()
       {
           TreeNode Temp = new TreeNode();
           Temp.Name = FTag;
           if (FLabel != "")
           {
               Temp.Text = FTag + ": " + FLabel;
           }
           else
           {
               Temp.Text = FTag;
           }
           if (FValue != "")
           {
               TreeNode valuenode = new TreeNode("Value = " + FValue);
               Temp.Nodes.Add(valuenode);
           }
           if (FCode!="")
               Temp.Text += "Code: "+FCode;
           if (FFieldname!="")
               Temp.Text += "Fld: "+FFieldname;
           if (FChildren.Count > 0)
           {
               foreach (DocTreeNode node in FChildren)
               {
                   Temp.Nodes.Add(node.GetTreeNode());
               }
           }
           return Temp;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Gets Documentation as a text string. </summary>
       /// <param name="Level"> The level of indentention to start with. </param>
       ///
       /// <returns> The string. </returns>
       ///-------------------------------------------------------------------------------------------------

       public string GetString(int Level)
       {
           string temp = " <" + FTag + ">  "+FLabel ;
           temp = temp.PadLeft(Level * 3, ' ');
           if (FValue!="")
               temp = temp + " Value: [" + FValue.PadLeft(20)+"]  ";
           if (FCode!="")
               temp += "  Code: "+FCode.PadLeft(10);
           if (FFieldname!="")
               temp += "  Fld: "+FFieldname;
           
           temp += "\r\n";

           if (FChildren.Count > 0)
           {
               Level++;
               foreach (DocTreeNode Node in FChildren)
               {
                   temp += Node.GetString(Level);
               }
           }
           return temp;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Adds a child using ModelParameterClass object. </summary>
       ///
       /// <param name="MP"> The ModelParmeter. </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChild(ModelParameterClass MP)
       {
           string aValue = "";
           if (MP.isProviderParam)
           {
               ProviderIntArray PIA = MP.ProviderProperty.getvalues();
               aValue = PIA[0].ToString();
               for (int i = 1; i < PIA.Length; i++)
                   aValue += " , " + PIA[i].ToString();
           }
           else
           {
               aValue = MP.Value.ToString();
           }
           DocTreeNode Temp = new DocTreeNode("MODELPARAM", aValue, MP.Label, MP.ModelParam.ToString(), MP.Fieldname);
           AddChild(Temp);
       }
//       public DocTreeNode(string aTag, string aValue, string aLabel, string aCode, string aFieldname)

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Adds a child using an AnnualFeedbackProcess object. </summary>
       ///
       /// <remarks> Ray, 1/27/2013. </remarks>
       ///
       /// <param name="AFP"> The AnnualFeedbackProcess object </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChild(AnnualFeedbackProcess AFP)
       {
           DocTreeNode Temp = AFP.Documentation();
           AddChild(Temp);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Adds a child field to The Documentation. </summary>
       /// <param name="fieldLabel">    The field label. </param>
       /// <param name="fieldvalue">    The fieldvalue. </param>
       ///-------------------------------------------------------------------------------------------------

       public void AddChildField(string fieldLabel, string fieldvalue)
       {
           DocTreeNode Temp = new DocTreeNode("FIELD", fieldvalue, fieldLabel, "", "");
           AddChild(Temp);
       }
    }
}
