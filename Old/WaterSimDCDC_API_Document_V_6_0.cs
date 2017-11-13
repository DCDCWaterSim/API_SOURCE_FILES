// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       A Class the adds Doxumentation support to the WaterSimDCDC.WaterSim Class

//       WaterSimDCDC_API_Documentation Version 5.0
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
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aModelParam">  The model parameter. </param>
        /// <param name="aDescrip">     The descrip. </param>
        /// <param name="aUnit">        The unit. </param>
        /// <param name="aLongUnit">    The long unit. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSimDescripItem(int aModelParam, string aDescrip, string aUnit, string aLongUnit)
        {
            FModelParam = aModelParam;
            FDescription = aDescrip;
            FUnits = aUnit;
            FLongUnits = aLongUnit;
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
        }

    }


    /// <summary>   WaterSim Parameter and Data Documentation Support Class </summary>
    /// <remarks> This class enhances the amount of information about a parameter that is available from the Parameter Manager
    ///           Includes an extended descriptiopn of the parameter and the units of the parameter</remarks>
    public class Extended_Parameter_Documentation
    {
        List<WaterSimDescripItem> MPDesc = new List<WaterSimDescripItem>();

        ParameterManagerClass FPM;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="PM">   The Current ParameterManager </param>
        ///-------------------------------------------------------------------------------------------------

        public Extended_Parameter_Documentation(ParameterManagerClass PM)
        {
            FPM = PM;
            BuildDescripList();
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

        internal WaterSimDescripItem FindDescripDoc(int modelparam)
        {
            return MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
        }

        internal string FindDescription(int modelparam)
        {
            string temp = "";

            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            if (DI != null)
                temp = DI.Description;

            return temp;
        }
        internal string FindUnit(int modelparam)
        {
            string temp = "";

            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            if (DI != null)
                temp = DI.Unit;
            return temp;
        }
        internal string FindLongUnit(int modelparam)
        {
            string temp = "";

            WaterSimDescripItem DI = MPDesc.Find(delegate(WaterSimDescripItem Item) { return (Item.ModelParam == modelparam); });
            if (DI != null)
                temp = DI.LongUnit;
            return temp;
        }

        internal void BuildDescripList()
        {

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSimulation_Start_Year, "The first year of the Simulation (NOTE: in this release the start year SHOULD BE 2006 or 2000).", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSimulation_End_Year, "The last year of the Simulation.", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Historical_Extraction_Start_Year, "The first year of the Colorado River flow record that will be used to create a 25 year trace to simulate river flow conditions (from the text file input, the flow record that corresponds to the year chosen, with that year flow and the next 24 years duplicated throughout the entire simulation period).", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Historical_Data_Source, "The source of the Colorado River flow record: Value 1 uses the Bureau of Reclamation recorded record, Value 2 uses the tree ring reconstructed paleo record, Value 3 uses a user supplied river flow trace record (created by the user, and representing the flow for 2011 through 2085 [the historical record is used for 2000-2010]).", "Code"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Climate_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Colorado river flow record, simulating impacts of climate change.  Change starts (or impacts the flow record) in any year that the value differs from 100%.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Colorado River flow record (decrease, typically), starting and stopping in the years specified (i.e., User_Adjustment_StartYear, User_Adjustment_StopYear).  This is used to simulate a drought condition. ", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_StartYear, "Determines the year that the [Colorado User Adjustment %] will be first be applied. ", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_User_Adjustment_Stop_Year, "Determines the year the [Colorado User Adjustment %] will stop being applied.", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Historical_Extraction_Start_Year, "The first year of the Salt Verde River flow record that will be used to create a 25 year trace to simulate river flow conditions (see above: COEXTSTYR).", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Historical_Data, "The source of the Salt Verde Rivers flow record: Value=1 uses the Bureau of Reclamation recorded record, Value=2 uses the tree ring reconstructed paleo record, Value=3 uses a user supplied river flow trace record (created by the user, and representing the flow for 2011 through 2085[the historical record is used for 2000-2010]).", "Code"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Climate_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, simulating impacts of climate change.  Change starts at beginning of Simulation (or in any year where the value departs from 100%).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Percent, "The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, starting and stopping in the years specified.  This is used to (typically) simulate a drought condition. ", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Start_Year, "Determines the year the [SaltVerde User Adjustment %] will first be applied. ", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_User_Adjustment_Stop_Year, "Determines the year the [SaltVerde User Adjustment %] will stop being applied.", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_REG_Growth_Rate_Adjustment, "For all providers adjusts the growth rate for areas on and off project to this value.  100 (100%) leaves the rate at the projected rate.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvider_Demand_Option, "The method that will be used to estimate annual demand  for all providers.  Value=1 reads demand values from an external file, Value=2 calculates demand based on a six year average GPCD and population (separate for each water provider) read from a file, Value=3 estimates demand based on population estimates read from an external file and a smoothing function that slowly declines GPCD (i.e., using ReduceGPCDpct), Value=4 uses same method as 3, but allows the user to manually chanage the GPCD used for each provider at any time; please note that once a change is made that value is maintained throughout the simulation.", "Code"));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epPCT_Reduce_GPCD,"The amount by which GPCD is expected to decrease by the end of the simulation (i.e., 2085). Use this when Provider Demand Option is = 3 or Provider Demand Option=4.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epModfyNormalFlow, "This parameter was added to the model when it was determined that not all class A (normal flow) water is used by a provider on a given day and, thus, for the year.  This variable simply adjusts the Trott Table estimated designations for each provider for each threshold of river flow. This is done at the start of the simulation.  Units: acre feet per acre x 10 were needed because we are using integers (this is  float as used). That is, a user would enter 15 for 1.5 AF acre-1.  or 9 for a 0.9 AF acre-1., etc. (note true upper estimate is 5.4288 [see Kent Decree]).", "AF*10","acre feet per acre * 10"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvider_WaterFromAgPumping, "Water can be removed from the Phoenix AMA (SRV) estimate of agricultural pumping of groundwater provided by Dale Mason (Fall 2011). This water is added to a water providers groundwater designation", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_GPCD, "The GPCD that will be used if [Provider Demand Option] is set to Value=4.", "GPCD"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Wastewater_to_Effluent, "The percent of  Waste water effluent that is used and not discharged into a water course (note: if PCEFFREC [below] is set to 100% no waste water is sent to the traditional WWTP and, so, no effluent will be available for partitioning).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WasteWater_to_Reclaimed, "The percent of  Waste water effluent that is sent to a Reclaimed Plant (versus a traditional plant-see figure 1).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_RO, "The percent of  reclaimed water that is sent to a Reverse Osmosis Plant (thus becomming potable water for direct injection or potable water for use in the next time-step).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_RO_to_Water_Supply, "The percent of  water from Reverse Osmosis Plant that is used for potable water.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_DirectInject, "The percent of  reclaimed water that is used to recharge an aquifer by direct injection into an aquifer.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Max_Demand_Reclaim, "The amount of (percent of demand that can be met by) reclaimed water that WILL be used as input for the next year.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_Water_Supply, "The percent of  reclaimed water that is used to meet qualified user demands (non-potable).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_to_Vadose, "The percent of  reclaimed water that is delivered to a vadoze zone recharge basin.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Effluent_to_Vadose, "The percent of  wastewater effluent delivered to a vadose zone recharge basin.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Effluent_to_PowerPlant, "The percent of  wastewater effluent delivered to a power plants for cooling towers.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSurfaceWater__to_Vadose, "The amount of surface water supply delivered to a vadose zone basin.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSurface_to_Vadose_Time_Lag, "The time in years it takes water recharged to the vadose zone to reach the aquifer to be used as  groundwater.", "Years"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterBank_Source_Option, "The source of water used for external water banking (outside provider groundwater): Value=1 a percent [% SurfaceWater to WaterBank] of 'unused' surface water is sent to a water bank, Value= 2 a fixed amount[Amount of SurfaceWater to WaterBank] of an unknown extra source of water is sent to a water bank.", "Code"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_SurfaceWater_to_WaterBank, "The percent of extra [excess] surface water that is sent to a water bank if [WaterBank Source Option] is set to a Value=1.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_SurfaceWater_to_WaterBank, "The amount of water (source unknown) sent to a water bank if [WaterBank Source Option] is set to a Value=2.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Residential, "The percent of total water supply used by residential customers.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Commercial, "The percent of  total water supply used by commercial users", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_WaterSupply_to_Industrial, "The percen of total water supply used by industrial users", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epUse_WaterSupply_to_DirectInject, "A fixed amount of potable water supply used for aquifer recharge by directly injecting into a potable aquifer.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseRes, "The percent of  potable water supply used for outdoor water uses Residential (indoor water use = 1 - outdoor use).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseCom, "The percent of  potable water supply used for outdoor water uses Commercial (indoor water use = 1 - outdoor use).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Outdoor_WaterUseInd, "The percent of  potable water supply used for outdoor water uses Industrial (indoor water use = 1 - outdoor use).", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Groundwater_Treated, "The percent of  groundwater that is treated before it is used for potable water supply.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Reclaimed_Outdoor_Use, "The percent of  reclaimed water to be used outdoors. If all available reclaimed water is not used outdoors (i.e., not 100%) it is used indoors as black water.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Growth_Rate_Adjustment_Other, "For each provder adjusts the growth rate for areas off project (other) to this value.  100 (100%) leaves the rate at the projected rate.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Growth_Rate_Adjustment_OnProject, "For each provider adjusts the growth rate for areas on project (SRP Lands) to this value.  100 (100%) leaves the rate at the projected rate.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSetPopulationsOn, "For each provider sets the population for areas off project (other) to this value. ", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSetPopulationsOther, "For each provider sets the population for areas on project (SRP lands) to this value. ", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_River_Flow, "The total annual flow in the Colorado River above Lake Powell (the record from  Lees Ferry).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPowell_Storage, "The total water storage in Lake Powell.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMead_Storage, "The total water storage in Lake Mead", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_River_Flow, "The total annual flow of the Salt and Verde (and Tonto) Rivers.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Storage, "The total annual storage in the Salt River Project reservoirs.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Spillage, "Spill water over the SVT system- all reservoirs", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_Agriculture, "The total amount of wastewater effluent delivered to agriculural users.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadLevel, "Elevation of Lake Mead's water surface", "Ft-msl","Feet (Mean Sea Level)"));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epBasinPumpage,"Salt River Valley pumpage over all model grids", ""));
            //MPDesc.Add(new WaterSimDescripItem( eModelParam.epBasinNetChange,"Net change in the basin over all model grids", ""));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Pumped_Municipal, "The total amount of annual groundwater pumped.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Balance, "The total groundwater credits available at end of year.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Annual_Deliveries_SRP, "The total annual surface water and pumped groundwater delivered by SRP.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epSaltVerde_Class_BC_Designations, "The total annual B & C designated ground water delivered by SRP.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epColorado_Annual_Deliveries, "The total annual surface water deliveries by CAP, does not included banked water.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Bank_Used, "The total annual amount of water delivered from water banking facilities.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGroundwater_Bank_Balance, "The total banked water supply available at end of year.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_Used, "The total annual amount of reclaimed water used.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_To_Vadose, "The annual amount of reclaimed water used for vadose zone recharge.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_Discharged, "The annual amount of reclaimed water discharged to a water course (environment).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epReclaimed_Water_to_DirectInject, "The annual amount of reclaimed water recharged to an aquifer using direct injection.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRO_Reclaimed_Water_Used, "The annual amount of reverse osmosis reclaimed water used for potable water supply.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRO_Reclaimed_Water_to_DirectInject, "The annual amount of reverse osmosis reclaimed water used for aquifer recharge using direct injection.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_Reused, "The total annual amount of wastewater effluent reused.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_Vadose, "The annual amount of effluent used for vadose zone recharge.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_To_PowerPlant, "The annual amount of effluent delivered to power plants.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEffluent_Discharged, "The annual amount of wastewater effluent discharged to a water course (envirionment).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDemand_Deficit, "The annual difference between demand and supply (demand - supply), 0 if supply is larger than demand.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotal_Demand, "The total annual demand from all water customers.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGPCD_Used, "The GPCD used to estimate demand for the completed simulation year.", "GPCD","Gallons Per Capita Per Day"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOnProjectPopulation, "Population for SRP member lands", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOtherPopulation, "All other population within the provider boundary", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPopulation_Used, "The population used (along with GPCD) to estimate demand for the completed simulation year.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalGWBalance, "MODFLOW estimates of groundwater on a water provider basis.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDeficit_Total, "Total amount of annual deficit (Total supply minus total demand when total demand exceeds supply, else 0).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDeficit_Years, "Number of years to date (cumulative) in which there has been a deficit (not enough water supply to meet demand)", "Years"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_GWAvailable, "Percent of original groundwater credits that are available each year.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYrsGWZero, "Number of years to date (cumulative) that groundwater credits have been zero or less.", "Years"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearGWGoesZero, "The earliest year that groundwater credits became zero (or less).", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsNotAssured, "Number of years to date (cumulative) that a 100 year Assured Water Supply could not be demonstrated.", "Years"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPercentDeficitLimit, "Percent deficit trigger used to begin reducing GPCD to respond to deficit.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMinimumGPCD, "Lowest value to which GPCD can be reduced.","GPCD", "Gallons Per Capita Per Day"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsOfNonAWSTrigger, "Number of years with not being able to demonstrate 100 year Assured Water Supply before some policy action occurs.", "Years"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epYearsOfNonAWSGPCDTrigger, "Number of years with not being able to demonstrate 100 year Assured Water Supply before GPCD is reduced.", "Year","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalSupplyUsed, "The total of all supply sources used to meet demand.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTGWofDemand, "The percent of demand that is met by ground water pumping.", "%F","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalReclaimedUsed, "The total amount of reclaimed effluent produced.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProvderEffluentToAg, "The amount of effluent that is used for agricutural.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epTotalEffluent, "The total amount of effluent produced (does not include reclaimed).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedOnProjectPop, "The annual population initially projected for SRP membership lands.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedOtherPop, "The annual population initially projected for non-SRP membership lands.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epProjectedTotalPop, "The total annual population initially projected.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceProjectedOnPop, "The deviation (difference) between annual projected population and population used for SRP membership lands.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceProjectedOtherPop, "The deviation (difference) between annual projected population and population used for non-SRP membership lands.", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epDifferenceTotalPop, "The deviation (difference) between annual total projected population and total population used ", "People"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedOnPop, "The percent deviation (difference) between annual projected population and population used for SRP membership lands.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedOtherPop, "The percent deviation (difference) between annual projected population and population used for non-SRP membership lands.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCTDiffProjectedTotalPop, "The percent deviation (difference) between annual total projected population and total population used ", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPctRecOfTotal,"The percent of reclaimed water used to meet demand annually.","%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epCreditDeficits, "The annual negative balance in groundwater credits, if groundwater credits are greater than 0, this value is 0.", "AF","Acre Feet"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPctRecOfTotal, "The annual amount of reclaimed water used as a percent of total supply.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Deficit, "The annual total deficit (how much demand exceeds supply) as a percent of total demand.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterAugmentation, "The annual amount of water supply available from a new source of water not included in the original water portfolio.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalNaturalRecharge, "The amount of water that is naturally recharged to the regional aquifer annually.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalCAGRDRecharge, "The amount of water that the Central Arizona Groundwater Replishment District recharges to the regional aquifer annually.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalInflow, "The amount of water the moves into the regional aquifer annually from other aquifers.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalAgToVadose, "The amount of water used by aggriculture irrigation that ends up recharging the regional aquifer annually.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalProviderRecharge, "The total amount of water that is recharged to the aquifer annually by all water providers.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalAgOtherPumping, "The total amount of water that is pumped from the regional aquifer annually by all non-water provider users (agricultural and other).", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegionalOutflow, "The total amount of water that annually leaves the regional aquifer as stream flow or flow to another aquifer.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOnProjectDemand, "The annual demand from SRP member lands.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epOffProjectDemand, "The annual demand from non-SRP member lands.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epVadoseToAquifer, "The total amount of water that is added annually to the regional aquifer from vadose recharge.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAnnualIncidental, "The amount of water that is added to groundwater credits annually to account for incidental (outdoor water use) aquifer recharge.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAlterGPCDpct , "The goal in percent reduction in Gallons per Capita per Day to achieve by the end of the simulation run. 100 = no reduction, 80 = 20% reduction.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPCT_Alter_GPCD, "The regional goal in percent reduction in Gallons per Capita per Day to achieve by the end of the simulation run. 100 = no reduction, 80 = 20% reduction.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAWSAnnualGWLimit, "Indicates if the AWS rule to limit annual pumping to AWS designated annual credits should be applied (Code=0) or ignored (Code=1)", "Code"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegPctGWDemand, "Regional Sustainability Indicator:The percent of demand met by groundwater pumping.", "%","Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epGWYrsSustain, "Provider Sustainability Indicator:Years that provider groundwater pumping can be sustained until total groundwater credits reach zero.", "Yr","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epRegGWYrsSustain, "Regional Sustainability Indicator:Average years for the region that provider groundwater pumping can be sustained until total groundwater credits reach zero.", "Yr","Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epAgSustainIndicator, "Regional Sustainability Indicator: The amount of water agriculture uses as a percentage of total water rights that could be used for agriculture.", "%", "Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvSustainIndicator, "Regional Sustainability Indicator: The amount of water left in the Colorado and Salt-Verde Rivers as a percent of a maximum amount that could be allocated", "%", "Percentage"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterUseIndicator, "Regional Sustainability Indicator: The regional average water use expressed on a scale of 0 to 100 calculated as ((GPCD/30)+1))*100)/9 with GPCDs over 299 = 100", "0-100","0 Very Low to 100 Very High"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epPowellLevel, "Elevation of of Lake Powell's water surface", "Ft-msl", "Feet (Mean Sea Level)"));

            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent, "Allocates effluent to agriculture, urban reuse, environment, or aquifer recharge based on a scale of 0 to 100, with 100 allocating more water to agriculture and urban reusem=, and 0 allocating more water to the environment and aquifer recharge", "0-100","0 environment/recharge to 100 agriculture/urban reuse"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent_Ag, "Allocates effluent to agriculture based on a scale of 0 to 100, with 100 allocating more water to agriculture and 0 allocating less", "0-100", "0 low to 100 high"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIeffluent_Env, "Allocates effluent to environment based on a scale of 0 to 100, with 100 allocating more water to agriculture and 0 allocating less", "0-100", "0 low to 100 high"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIAgriculture, "Transfers water rights from agriculture to urban water providers, based on a scale of 0 to 100, with 100 allocating more water to agriculture and 0 allocating less", "0-100", "0 low to 100 high"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebPop_GrowthRateAdj_PCT, "Adjustes the population growth rate for all water providers on a scale of 0-150, with 0-no growth to 150-50% increase in growth rate", "0-150","0-0% to 150-150%"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWebUIPersonal, "Adjusts the Gallons per Capita per Day for all water providers on a scale of 0 to 100, with zero a decline to 50 GPCD and 100 a decline based on past trends.", "0-100","0-Repid decline to 100-Current trends"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvironmentalFlow_PCT , "Allocates water for envirionmental use by leaving it in the rivers based on a scale of 0 to 100, wiith 0=no water and 100=500,000 AF/yr.", "0-100","0=0 af/yr to 100=500,000 af/yr"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epEnvironmentalFlow_AFa, "Allocates a specified amount of water for envirionmental by leaving it in the rivers", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epWaterAugmentationUsed , "Amount of water that is used from augmented water supplies.", "AF/yr","Acre Feet per Year"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadDeadPool, "The elevation at which stored water can no longer be physcally released from Lake Mead.", "Ft-msl", "Feet (Mean Sea Level)"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadLowerSNWA, "The elevation of the lower intake the Southern Nevada Water Authority uses to withdraw water from Lake Mead", "Ft-msl", "Feet (Mean Sea Level)"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadShortageSharing, "The elevation that may trigger shortage sharing among lower basin states as per the 2007 Seven Basin States SHortage Sharing Agreement", "Ft-msl", "Feet (Mean Sea Level)"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadMinPower, "The minimum elevation at which the hydraulic turbines at Lake Mead can produce power.", "Ft-msl", "Feet (Mean Sea Level)"));
            MPDesc.Add(new WaterSimDescripItem(eModelParam.epMeadCapacity, "The maximum elevation that water can be stored in Lake Mead, after which water will spill from the dam.", "Ft-msl", "Feet (Mean Sea Level)"));
            //MPDesc.Add(new WaterSimDescripItem(eModelParam , "", ""));
            //MPDesc.Add(new WaterSimDescripItem(eModelParam , "", ""));
            /*
            //

 ///            _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadDeadPool, "Lake Mead Dead Pool", "MDDPL", get_MeadDeadPool, 800, 1300));
///            _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadLowerSNWA, "Lake Mead Lower SNWA Intake", "MDLIL", get_MeadSNWALowerIntake, 800, 1300));
///            _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadShortageSharing, "Lake Mead Shortage Sharing", "MDSSL", get_MeadShortageSharingLevel, 800, 1300));
///            _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadMinPower, "Lake Mead Minimum Power Pool", "MDMPL", get_MeadMinPowerLevel, 800, 1300));
///            _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadCapacity, "Lake Mead Capacity", "MDCPL", get_MeadCapacity, 800, 1300));
///        }      *
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
        /// <summary>   Create a Model Document Tree. </summary>
        /// <remarks> Creates a DocTreeNode Tree populated with Model information using WSim, tree includes parameters and processes</remarks>
        /// <param name="WSim"> The WaterSimManager node is based on </param>
        /// <returns> a DocTreeNode with model info </returns>
        ///-------------------------------------------------------------------------------------------------

        public static DocTreeNode ModelTree(WaterSimManager WSim)
        {
            DocTreeNode ModelTree = new DocTreeNode("WATERSIM", "");
            DocTreeNode APIVersionNode = new DocTreeNode("APIVERSION", WSim.APiVersion);
            ModelTree.AddChild(APIVersionNode);
            DocTreeNode ModelVersionNode = new DocTreeNode("MODELVERSION", WSim.ModelBuild);
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
