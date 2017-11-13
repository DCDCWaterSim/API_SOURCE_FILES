//      WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       This is support classes for the C# WaterSim API

//       Version 4.1
//       Keeper Ray Quay ray.quay@asu.edu
//       
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

/***************************************************************
 * Water Sim API Utilities 
 * Version 4.1
 * 
 *   7/24/12
 * 
 * Keeper Ray Quay
 * 
 * 
 * ************************************************************/
namespace WaterSimDCDC
{
    //********************************************
    // Enums and Constants
    // 
    // *******************************************
    // 
    #region Enums_Constants

    ///-------------------------------------------------------------------------------------------------
    /// <summary> Special values Used to represent Missing and Infinite values </summary>
    ///
    /// <remarks> Ray, 1/24/2013. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public static class SpecialValues
    {
        /// <summary> The missing int value. </summary>
        public const int MissingIntValue = -2147483648 ;  // Smallest int can get
        /// <summary> The infinite int value. </summary>
        public const int InfiniteIntValue = MissingIntValue + 1 ; // 
        
    }
    
    /// <summary>   Values that represent ModelParameter.  </summary>
    /// <remarks> Unique identifier for each ModelParameter</remarks>
    public static class eModelParam
    {
        // Values 0 to 200 reserved for Basic Model Inpus and Outputs
        /// <summary> The maximum basic parameter. </summary>
        public const int MaxBasicParameter = 200;
        /// <summary> The colorado river flow.  Parameter </summary>
        public const int epColorado_River_Flow = 0;
        /// <summary> The powell storage.  Parameter </summary>
        public const int epPowell_Storage = 1;
        /// <summary> The mead storage.  Parameter </summary>
        public const int epMead_Storage = 2;
        /// <summary> The salt verde river flow.  Parameter </summary>
        public const int epSaltVerde_River_Flow = 3;
        /// <summary> The salt verde storage.  Parameter </summary>
        public const int epSaltVerde_Storage = 4;
        /// <summary> The effluent to agriculture.  Parameter </summary>
        public const int epEffluent_To_Agriculture = 5;
        /// <summary> The groundwater pumped municipal.  Parameter </summary>
        public const int epGroundwater_Pumped_Municipal = 6;
        /// <summary> The groundwater balance.  Parameter </summary>
        public const int epGroundwater_Balance = 7;
        /// <summary> The salt verde annual deliveries srp.  Parameter </summary>
        public const int epSaltVerde_Annual_Deliveries_SRP = 8;
        /// <summary> The salt verde class bc designations.  Parameter </summary>
        public const int epSaltVerde_Class_BC_Designations = 9;
        /// <summary> The colorado annual deliveries.  Parameter </summary>
        public const int epColorado_Annual_Deliveries = 10;
        /// <summary> The groundwater bank used.  Parameter </summary>
        public const int epGroundwater_Bank_Used = 11;
        /// <summary> The groundwater bank balance.  Parameter </summary>
        public const int epGroundwater_Bank_Balance = 12;
        /// <summary> The reclaimed water used.  Parameter </summary>
        public const int epReclaimed_Water_Used = 13;
        /// <summary> The reclaimed water to vadose.  Parameter </summary>
        public const int epReclaimed_Water_To_Vadose = 14;
        /// <summary> The reclaimed water discharged.  Parameter </summary>
        public const int epReclaimed_Water_Discharged = 15;
        /// <summary> The reclaimed water to direct inject.  Parameter </summary>
        public const int epReclaimed_Water_to_DirectInject = 16;
        /// <summary> The ro reclaimed water used.  Parameter </summary>
        public const int epRO_Reclaimed_Water_Used = 17;
        /// <summary> The ro reclaimed water to direct inject.  Parameter </summary>
        public const int epRO_Reclaimed_Water_to_DirectInject = 18;
        /// <summary> The effluent created.  Parameter </summary>
        public const int epEffluent_Reused = 19;
        /// <summary> The effluent to vadose.  Parameter </summary>
        public const int epEffluent_To_Vadose = 20;
        /// <summary> The effluent to power plant.  Parameter </summary>
        public const int epEffluent_To_PowerPlant = 21;
        /// <summary> The effluent discharged.  Parameter </summary>
        public const int epEffluent_Discharged = 22;
        /// <summary> The demand deficit.  Parameter </summary>
        public const int epDemand_Deficit = 23;
        /// <summary> The total demand.  Parameter </summary>
        public const int epTotal_Demand = 24;
        /// <summary> The on project demand.  Parameter </summary>
        public const int epOnProjectDemand = 25;
        /// <summary> The off project demand.  Parameter </summary>
        public const int epOffProjectDemand = 26;
        /// <summary> The gpcd used.  Parameter </summary>
        public const int epGPCD_Used = 27;
        /// <summary> The population used.  Parameter </summary>
        public const int epPopulation_Used = 28;
        /// <summary> The salt verde spillage.  Parameter </summary>
        public const int epSaltVerde_Spillage = 29;
        /// <summary> The simulation start year.  Parameter </summary>
        public const int epSimulation_Start_Year = 30;
        /// <summary> The simulation end year.  Parameter </summary>
        public const int epSimulation_End_Year = 31;
        /// <summary> The colorado historical extraction start year.  Parameter </summary>
        public const int epColorado_Historical_Extraction_Start_Year = 32;
        /// <summary> The colorado historical data source.  Parameter </summary>
        public const int epColorado_Historical_Data_Source = 33;
        /// <summary> The colorado climate adjustment percent.  Parameter </summary>
        public const int epColorado_Climate_Adjustment_Percent = 34;
        /// <summary> The colorado user adjustment percent.  Parameter </summary>
        public const int epColorado_User_Adjustment_Percent = 35;
        /// <summary> The colorado user adjustment start year.  Parameter </summary>
        public const int epColorado_User_Adjustment_StartYear = 36;
        /// <summary> The colorado user adjustment stop year.  Parameter </summary>
        public const int epColorado_User_Adjustment_Stop_Year = 37;
        /// <summary> The salt verde historical extraction start year.  Parameter </summary>
        public const int epSaltVerde_Historical_Extraction_Start_Year = 38;
        /// <summary> Information describing The salt verde historical.  Parameter </summary>
        public const int epSaltVerde_Historical_Data = 39;
        /// <summary> The salt verde climate adjustment percent.  Parameter </summary>
        public const int epSaltVerde_Climate_Adjustment_Percent = 40;
        /// <summary> The salt verde user adjustment percent.  Parameter </summary>
        public const int epSaltVerde_User_Adjustment_Percent = 41;
        /// <summary> The salt verde user adjustment start year.  Parameter </summary>
        public const int epSaltVerde_User_Adjustment_Start_Year = 42;
        /// <summary> The salt verde user adjustment stop year.  Parameter </summary>
        public const int epSaltVerde_User_Adjustment_Stop_Year = 43;
        /// <summary> The provider demand option.  Parameter </summary>
        public const int epProvider_Demand_Option = 44;
        /// <summary> The pct reduce gpcd.  Parameter </summary>
        public const int epPCT_Alter_GPCD = 45;
        /// <summary> The modfy normal flow.  Parameter </summary>
        public const int epModfyNormalFlow = 46;
       
        /// <summary> An Amount of Water supply Augmentation. Parameter </summary>
        public const int epWaterAugmentation = 47;
        /// <summary> The undefined 48.  Parameter </summary>
        public const int Undefined_48 = 48;
        /// <summary> The use gpcd.  Parameter </summary>
        public const int epUse_GPCD = 49;
        /// <summary> The pct waste water to reclaimed.  Parameter </summary>
        public const int epPCT_WasteWater_to_Reclaimed = 50;
        /// <summary> The pct wastewater to effluent.  Parameter </summary>
        public const int epPCT_Wastewater_to_Effluent = 51;
        /// <summary> The pct reclaimed to ro.  Parameter </summary>
        public const int epPCT_Reclaimed_to_RO = 52;
        /// <summary> The pct ro to water supply.  Parameter </summary>
        public const int epPCT_RO_to_Water_Supply = 53;
        /// <summary> The pct reclaimed to direct inject.  Parameter </summary>
        public const int epPCT_Reclaimed_to_DirectInject = 54;
        /// <summary> The pct reclaimed to water supply.  Parameter </summary>
        public const int epPCT_Reclaimed_to_Water_Supply = 55;
        /// <summary> The pct reclaimed to vadose.  Parameter </summary>
        public const int epPCT_Reclaimed_to_Vadose = 56;
        /// <summary> The pct effluent to vadose.  Parameter </summary>
        public const int epPCT_Effluent_to_Vadose = 57;
        /// <summary> The pct effluent to power plant.  Parameter </summary>
        public const int epPCT_Effluent_to_PowerPlant = 58;
        /// <summary> The surface water to vadose.  Parameter </summary>
        public const int epSurfaceWater__to_Vadose = 59;
        /// <summary> The surface to vadose time lag.  Parameter </summary>
        public const int epSurface_to_Vadose_Time_Lag = 60;
        /// <summary> The water bank source option.  Parameter </summary>
        public const int epWaterBank_Source_Option = 61;
        /// <summary> The pct surface water to water bank.  Parameter </summary>
        public const int epPCT_SurfaceWater_to_WaterBank = 62;
        /// <summary> The use surface water to water bank.  Parameter </summary>
        public const int epUse_SurfaceWater_to_WaterBank = 63;
        /// <summary> The pct water supply to residential.  Parameter </summary>
        public const int epPCT_WaterSupply_to_Residential = 64;
        /// <summary> The pct water supply to commercial.  Parameter </summary>
        public const int epPCT_WaterSupply_to_Commercial = 65;
        /// <summary> The use water supply to direct inject.  Parameter </summary>
        public const int epUse_WaterSupply_to_DirectInject = 66;
        /// <summary> The pct outdoor water use.  Parameter </summary>
        public const int epPCT_Outdoor_WaterUse = 67;
        /// <summary> The pct groundwater treated.  Parameter </summary>
        public const int epPCT_Groundwater_Treated = 68;
        /// <summary> The pct reclaimed outdoor use.  Parameter </summary>
        public const int epPCT_Reclaimed_Outdoor_Use = 69;
        /// <summary> The pct growth rate adjustment on project.  Parameter </summary>
        public const int epPCT_Growth_Rate_Adjustment_OnProject = 70;
        /// <summary> The pct maximum demand reclaim.  Parameter </summary>
        public const int epPCT_Max_Demand_Reclaim = 71;
        /// <summary> The pct register growth rate adjustment.  Parameter </summary>
        public const int epPCT_REG_Growth_Rate_Adjustment = 72;
        // ***** Changed 8 13 12 
        // public const int epSetPopulations = 73;
        // public const int Undefined_74 = 74;
        /// <summary> The set populations on.  Parameter </summary>
        public const int epSetPopulationsOn = 73;
        /// <summary> The set populations other.  Parameter </summary>
        public const int epSetPopulationsOther = 74;
        //*********
        /// <summary> The pct water supply to industrial.  Parameter </summary>
        public const int epPCT_WaterSupply_to_Industrial = 75;
        /// <summary> The pct outdoor water use resource.  Parameter </summary>
        public const int epPCT_Outdoor_WaterUseRes = 76;
        /// <summary> The pct outdoor water use com.  Parameter </summary>
        public const int epPCT_Outdoor_WaterUseCom = 77;
        /// <summary> The pct outdoor water use ind.  Parameter </summary>
        public const int epPCT_Outdoor_WaterUseInd = 78;
        /// <summary> The on project population.  Parameter </summary>
        public const int epOnProjectPopulation = 79;
        /// <summary> The other population.  Parameter </summary>
        public const int epOtherPopulation = 80;
        /// <summary> The mead level.  Parameter </summary>
        public const int epMeadLevel = 81;
        /// <summary> The the ws annual gw limit.  Parameter </summary>
        public const int epAWSAnnualGWLimit = 82;
        /// <summary> The pct growth rate adjustment other.  Parameter </summary>
        public const int epPCT_Growth_Rate_Adjustment_Other = 83;
        /// <summary> The annual incidental.  Parameter </summary>
        public const int epAnnualIncidental = 84;
        /// <summary> The vadose to aquifer.  Parameter </summary>
        public const int epVadoseToAquifer = 85;
        /// <summary> The regional natural recharge.  Parameter </summary>
        public const int epRegionalNaturalRecharge = 86;
        /// <summary> The regional cagrd recharge.  Parameter </summary>
        public const int epRegionalCAGRDRecharge = 87;
        /// <summary> The regional inflow.  Parameter </summary>
        public const int epRegionalInflow = 88;
        /// <summary> The regional ag to vadose.  Parameter </summary>
        public const int epRegionalAgToVadose = 89;
        /// <summary> The regional provider recharge.  Parameter </summary>
        public const int epRegionalProviderRecharge = 90;
        /// <summary> The regional ag other pumping.  Parameter </summary>
        public const int epRegionalAgOtherPumping = 91;
        /// <summary> The regional outflow.  Parameter </summary>
        public const int epRegionalOutflow = 92;
        /// <summary> The regional gw balance.  Parameter </summary>
        public const int epRegionalGWBalance = 93;
        /// <summary> The provider level adjustment of the temporal trend in GPCD </summary>

        // DAVID Add 94 - 104 2/24/14
        public const int epAlterGPCDpct = 94;
        public const int epGPCDraw = 95;
        public const int epRESdefault = 96;
        public const int epCOMdefault = 97;
        public const int epINDdefault = 98;
        public const int epProvider_Max_NormalFlow = 99;
        public const int epProvider_WaterFromAgPumping = 100;
        public const int epProvider_WaterFromAgSurface = 101;
        public const int epProvider_WaterFromAgPumpingMax = 102;
        public const int epProvider_WaterFromAgSurfaceMax = 103;
        public const int epWebPop_GrowthRateAdj_PCT = 104;
 
        //==================================
        // Values 700 to 900 Reserved for GroundWaterModel
        /// <summary> The first gw parameter.  Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int FirstGWParameter = 701;
        /// <summary> The last gw parameter.  Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int LastGWParameter = 900;
        //----------------------------   
        /// <summary> The groundwater model.  Parameter </summary>
        public const int epGroundwater_Model = 701;
        /// <summary> The credit deficits.  Parameter </summary>
        public const int epCreditDeficits = 702;
        
        //======================================
        // Values 201 to 300 Reserved for Derived Parameters
        /// <summary> The first derived parameter.  Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int FirstDerivedParameter = 201;
        /// <summary> The total supply used.  Parameter </summary>
        public const int epTotalSupplyUsed = 201;
        /// <summary> The pctg wof demand.  Parameter </summary>
        public const int epPCTGWofDemand = 202;
        /// <summary> The total reclaimed used.  Parameter </summary>
        public const int epTotalReclaimedUsed = 203;
        /// <summary> The provder effluent to ag.  Parameter </summary>
        public const int epProvderEffluentToAg = 204;
        /// <summary> The total effluent.  Parameter </summary>
        public const int epTotalEffluent = 205;
        /// <summary> The projected on project POP.  Parameter </summary>
        public const int epProjectedOnProjectPop = 206;
        /// <summary> The projected other POP.  Parameter </summary>
        public const int epProjectedOtherPop = 207;
        /// <summary> The projected total POP.  Parameter </summary>
        public const int epProjectedTotalPop = 208;
        /// <summary> The difference projected other POP.  Parameter </summary>
        public const int epDifferenceProjectedOtherPop = 209;
        /// <summary> The difference projected on POP.  Parameter </summary>
        public const int epDifferenceProjectedOnPop = 210;
        /// <summary> The difference total POP.  Parameter </summary>
        public const int epDifferenceTotalPop = 211;
        /// <summary> The pct difference projected on POP.  Parameter </summary>
        public const int epPCTDiffProjectedOnPop = 212;
        /// <summary> The pct difference projected other POP.  Parameter </summary>
        public const int epPCTDiffProjectedOtherPop = 213;
        /// <summary> The pct difference projected total POP.  Parameter </summary>
        public const int epPCTDiffProjectedTotalPop = 214;
        /// DAVID Added 250 to 261 2/24/14
        public const int epEnvironmentalFlow_PCT = 250;
        public const int epEnvironmentalFlow_AFa = 251;
        public const int epWebUIeffluent = 252;
        public const int epWebUIeffluent_Ag = 253;
        public const int epWebUIeffluent_Env = 254;
        public const int epWebUIAgriculture = 255;
        public const int epWebUIPersonal = 256;

        public const int epTotalReclaimedCreated_AF = 260;
        public const int epTWWTPCreated_AF = 261;

        /// DAVID  



        /// <summary> The last derived parameter.  Parameter </summary>
        public const int LastDerivedParameter = 300;
        //----------------------------   


        //======================================
        // Values 301 to 400 Reserved for Sustianbel Parameters
        /// <summary> The first sustainable Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int FirstSustainableParameter = 301;
        /// <summary> The last ssustainable Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int LastSustainableParameter = 400;
        //----------------------------   
        /// <summary> The pct deficit.  Parameter </summary>
        public const int epPCT_Deficit = 301;
        /// <summary> The deficit in years.  Parameter </summary>
        public const int epDeficit_Years = 302;
        /// <summary> The deficit total.  Parameter </summary>
        public const int epDeficit_Total = 303;
        /// <summary> The pct gw available.  Parameter </summary>
        public const int epPCT_GWAvailable = 304;
        /// <summary> The yrs gw zero.  Parameter </summary>
        public const int epYrsGWZero = 305;
        /// <summary> The year gw goes zero.  Parameter </summary>
        public const int epYearGWGoesZero = 306;
        /// <summary> The years not assured.  Parameter </summary>
        public const int epYearsNotAssured = 307;
        /// <summary> The percent of total supply that is reclaimed</summary>
        public const int epPctRecOfTotal = 308;
        //======================================
        // Values 501 to 600 Reserved for Process Parameters
        /// <summary> The first process parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int FirstProcessParameter = 501;
        /// <summary> The percent deficit limit.  Parameter </summary>
        public const int epPercentDeficitLimit = 502;
        /// <summary> The minimum gpcd.  Parameter </summary>
        public const int epMinimumGPCD = 503;
        /// <summary> The years of non the ws trigger.  Parameter </summary>
        public const int epYearsOfNonAWSTrigger = 504;
        /// <summary> The years of non a wsgpcd trigger.  Parameter </summary>
        public const int epYearsOfNonAWSGPCDTrigger = 505;
         /// <summary> The last process paramete </summary>
        /// <remarks> Reserved Value</remarks>
        public const int LastProcessParameter = 600;
        //----------------------------   
        //===================================
        // Values 1501 to 1700 Reserved for User Defined Parameters
        /// <summary> The first user defined Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int FirstUserDefinedParameter = 1501;
        /// <summary> The last user defined Parameter </summary>
        /// <remarks> Reserved Value</remarks>
        public const int LastUserDefinedParameter = 1700;
        //-------------


        //=========================================
        /// <summary> The names  of the Parameters. </summary>
        public static string[] Names = new string[MaxBasicParameter + 1] 
        {
"epColorado_River_Flow",//                       = 0;
"epPowell_Storage",//                            = 1;
"epMead_Storage",//                              = 2;
"epSaltVerde_River_Flow",//                      = 3;
"epSaltVerde_Storage",//                         = 4;
"epEffluent_To_Agriculture",//                   = 5;
"epGroundwater_Pumped_Municipal",//              = 6;
"epGroundwater_Balance",//                       = 7;
"epSaltVerde_Annual_Deliveries_SRP",//           = 8;
"epSaltVerde_Class_BC_Designations",//           = 9;
"epColorado_Annual_Deliveries",//                = 10;
"epGroundwater_Bank_Used",//                     = 11;
"epGroundwater_Bank_Balance",//                  = 12;
"epReclaimed_Water_Used",//                      = 13;
"epReclaimed_Water_To_Vadose",//                 = 14;
"epReclaimed_Water_Discharged",//                = 15;
"epReclaimed_Water_to_DirectInject",//           = 16;
"epRO_Reclaimed_Water_Used",//                   = 17;
"epRO_Reclaimed_Water_to_DirectInject",//        = 18;
"epEffluent_Created",//                          = 19;
"epEffluent_To_Vadose",//                        = 20;
"epEffluent_To_PowerPlant",//                    = 21;
"epEffluent_Discharged",//                       = 22;
"epDemand_Deficit",//                            = 23;
"epTotal_Demand",//                              = 24;
"epOnProjectDemand",//                           = 25;
"epOffProjectDemand",//                          = 26;
"epGPCD_Used",//                                 = 27;
"epPopulation_Used",//                           = 28;
"epSaltVerde_Spillage",//                        = 29;
 
"epSimulation_Start_Year",//                     = 30;
"epSimulation_End_Year",//                       = 31;
"epColorado_Historical_Extraction_Start_Year",// = 32;
"epColorado_Historical_Data_Source",//           = 33;
"epColorado_Climate_Adjustment_Percent",//       = 34;
"epColorado_User_Adjustment_Percent",//          = 35;
"epColorado_User_Adjustment_StartYear",//        = 36;
"epColorado_User_Adjustment_Stop_Year",//        = 37;
"epSaltVerde_Historical_Extraction_Start_Year",//= 38;
"epSaltVerde_Historical_Data",//                 = 39;
"epSaltVerde_Climate_Adjustment_Percent",//      = 40;
"epSaltVerde_User_Adjustment_Percent",//         = 41;
"epSaltVerde_User_Adjustment_Start_Year",//      = 42;
"epSaltVerde_User_Adjustment_Stop_Year",//       = 43;
"epProvider_Demand_Option",//                    = 44;
"epPCT_Alter_GPCD",//                           = 45;
"epModfyNormalFlow",//                           = 46;
"Undefined_47",//                        = 47;
"epCreditDeficits",//                            = 48;
"epUse_GPCD",//                                  = 49;
"epPCT_WasteWater_to_Reclaimed",//               = 50;
"epPCT_Wastewater_to_Effluent",//                = 51;
"epPCT_Reclaimed_to_RO",//                       = 52;
"epPCT_RO_to_Water_Supply",//                    = 53;
"epPCT_Reclaimed_to_DirectInject",//             = 54;
"epPCT_Reclaimed_to_Water_Supply",//             = 55;
"epPCT_Reclaimed_to_Vadose",//                   = 56;
"epPCT_Effluent_to_Vadose",//                    = 57;
"epPCT_Effluent_to_PowerPlant",//                = 58;
"epSurfaceWater__to_Vadose",//        =  59 
"epSurface_to_Vadose_Time_Lag",//                = 60;
"epWaterBank_Source_Option",//                   = 61;
"epPCT_SurfaceWater_to_WaterBank",//             = 62;
"epUse_SurfaceWater_to_WaterBank",//             = 63;
"epPCT_WaterSupply_to_Residential",//            = 64;
"epPCT_WaterSupply_to_Commercial",//                 = 65;
"epUse_WaterSupply_to_DirectInject",//           = 66;
"epPCT_Outdoor_WaterUse",//                      = 67;
"epPCT_Groundwater_Treated",//                   = 68;
"epPCT_Reclaimed_Outdoor_Use",//                 = 69;
"epPCT_Growth_Rate_Adjustment",//                = 70;
"epPCT_Max_Demand_Reclaim",//                    = 71;
"epPCT_REG_Growth_Rate_Adjustment",//            = 72;
"epSetPopulationsOn",//                            = 73;
"epSetPopulationsOther",  //= 74",
"Undefined 75", //75
"Undefined 76", //76
"Undefined 77", //77
"Undefined 78", //78
"Undefined 79", //79
"Undefined 80", //80
"Undefined 81", //81
"Undefined 82", //82
"Undefined 83", //83
"Undefined 84", //84
"Undefined 85", //85
"Undefined 86", //86
"Undefined 87", //87
"Undefined 88", //88
"Undefined 89", //89
"Undefined 90", //90
"Undefined 91", //91
"Undefined 92", //92
"Undefined 93", //93
"Undefined 94", //94
"Undefined 95", //95
"Undefined 96", //96
"Undefined 97", //96
"Undefined 98", //96
"Undefined 99", //96
"Undefined 100" //100
        };


    };


    //---------------------------------------------------------
    /// <summary>   Values that represent modelParamtype.  </summary>
    public enum modelParamtype
    {

        /// <summary> Unknown so ignore.  </summary>
        mptUnknown,

        /// <summary> a base output paramter.  </summary>
        /// <remarks> Single integer value that represents some output</remarks>
        mptOutputBase,

        /// <summary> a provider output paramter.  </summary>
        /// <remarks> An array of integer values that represents some output for each provider</remarks>
        mptOutputProvider,

        /// <summary> a base input parameter.  </summary>
        /// <remarks> Single integer value that represents a model Input</remarks>
        mptInputBase,

        /// <summary> a provider input parameter. </summary>
        /// <remarks> An array of integer values that represents some input for each provider</remarks>
        mptInputProvider,
        /// <summary> Input to Non-Model Function</summary>
        mptInputOther,
        /// <summary> Output from Non-Model Fucntion. </summary>
        mptOutputOther,

        
        //mptOutput,

        //mptInput,

        /// <summary> A Grid2D Output Parameter    </summary>
        mptOutput2DGrid,
        /// <summary>  A Grid2D input Parameter</summary>
        mptInput2DGrid,
        /// <summary>   A Grid3D Output Parameter. </summary>
        mptOutput3DGrid,
        /// <summary>  A Grid3D Input Parameter . </summary>
        mptInput3DGrid,
    };
    /// <summary>   Values that represent rangeChecktype.  </summary>
    public enum rangeChecktype
    {

        /// <summary>  Unknown, no range check done.  </summary>
        rctUnknown,

        /// <summary> parameter needs no range check.  </summary>
        rctNoRangeCheck,

        /// <summary> parameter has a valid low and high range.  </summary>
        rctCheckRange,

        /// <summary> parameter has a valid low and high range. which is affected by or affects the range of other model parameters.  </summary>
        rctCheckRangeSpecial,

        /// <summary> parameter value must be positive.  </summary>
        rctCheckPositive
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Range check Static Class </summary>
    ///-------------------------------------------------------------------------------------------------

    static class RangeCheck
    {
        /// <summary>   Const (null) for  no special provider Range Check Routine. </summary>
        static public DcheckProvider NoSpecialProvider = null;
        /// <summary>   Const (null) for  no special provider Range Check Routine. </summary>
        static public DcheckBase NoSpecialBase = null;
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Default Grid values Used for Grid2D and Grid3D structures for use with Maricopa ModFlow Model</summary>
    ///-------------------------------------------------------------------------------------------------

    static public class GridValues
    {
        /// <summary>   The row number. </summary>
        public static int RowNumber = 125; 
        /// <summary>   The column number. </summary>
        public static int ColumnNumber = 222;
        /// <summary>   The layer number. </summary>
        public static int LayerNumber = 3;
    }
    //=========================================
    ////===============================================
    //// Post and Pre Process delegates, fields, and properties
    ////=============================================================
    ///// <summary>
    ///// Enumeration of Process Type, Pre or Post <see cref="AnnualFeedbackProcess"/>
    ///// </summary>
    ///// <remarks>AnnualFeedbackProcess objects, or ProcessMethod delegate methods can either be evoke before the model begins executing an 
    ///// annual Simulation, or after an annual Simulation is complete</remarks> 
    ///// <seealso cref="AnnualFeedbackProcess"/>

    //public enum ProcessType
    //{

    //    /// <summary> Undefined so do not call this.  </summary>
    //    ptUnknown,
    //    /// <summary>
    //    /// Process runs before annual model run
    //    /// </summary>
    //    ptPre,
    //    /// <summary>
    //    /// Process runs after annual model run
    //    /// </summary>
    //    ptPost
    //}
    // Provider AGGREGATE MODE

    ///-------------------------------------------------------------------------------------------------
    /// <summary> Values that represent eProviderAggregateMode. </summary>
    ///
    /// <remarks> These values indicate how a parameter should be summarized at a regional level based on individual provider values. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum eProviderAggregateMode
    {
        /// <summary> No Aggregation, each provider has same value, use first provider value. </summary>
        agNone,  // No aggregation
        /// <summary> Sum all the providers. </summary>
        agSum,  // Add all provider values
        /// <summary> Average all the providers. </summary>
        agAverage, // Average all provider values
        /// <summary> Average all the providers by weighting each based on demand. </summary>
        agWeighted,  // Weighted Average  // 
       
    };

    /// <summary>
    /// enum values for each of the Water Providers
    /// </summary>
    /// <remarks>This is a key enumerator for all the provide input and output paramters, data stored in 
    /// provider arrays is stored in the order of these enum values.  Most provider data structures allow indexing based
    /// on these enum values and the ProviderClass provides an IEnumerator that returns enum values using a foreach () loop.  
    /// The exception to this is the providers past the ProviderClass.LastProvider eProvider value which are not considered a valid provider by most routines
    /// Test for a valid provider using ProviderClass.valid(int index)</remarks>
    public enum eProvider
    {
        /// <summary>Adaman Mutual Utility Company</summary>
        Adaman_Mutual,
        /// <summary> White Tanks Utility Company</summary>
        White_Tanks,
        /// <summary> Paradise Valley Utility Company</summary>
        Paradise_Valley,
        /// <summary> Sun City Utility Company </summary>
        Sun_City,
        /// <summary> Sun City West Utility Company </summary>
        Sun_City_West,
        /// <summary> City of Avondale. </summary>
        Avondale,
        /// <summary> Berneil Utility Company </summary>
        Berneil,
        /// <summary> City of Buckeye. </summary>
        Buckeye,
        /// <summary> Carefree Utility Company </summary>
        Carefree,
        /// <summary> Town of Cave Creek. </summary>
        Cave_Creek,
        /// <summary> City of Chandler. </summary>
        Chandler,
        /// <summary>Chaparral City Utility Company  . </summary>
        Chaparral_City,
        /// <summary> City of Surprise. </summary>
        Surprise,
        /// <summary> Utility Company . </summary>
        Clearwater_Utilities,
        /// <summary> Utility Company  </summary>
        Desert_Hills,
        /// <summary> Town of El Mirage . </summary>
        El_Mirage,
        /// <summary> City of Gilbert. </summary>
        Gilbert,
        /// <summary> City of Glendale. </summary>
        Glendale,
        /// <summary> City of Goodyear. </summary>
        Goodyear,
        /// <summary> Utility Company  </summary>
        Litchfield_Park,
        /// <summary> City of Mesa. </summary>
        Mesa,
        /// <summary> City of Peoria. </summary>
        Peoria,
        /// <summary> City of Phoenix. </summary>
        Phoenix,
        /// <summary> Town of Queen Creek. </summary>
        Queen_Creek,
        /// <summary> Rigby Utility Company  </summary>
        Rigby,
        /// <summary> Rio Verde Utility Company . </summary>
        Rio_Verde,
        /// <summary> Rose Valley Utility Company . </summary>
        Rose_Valley,
        /// <summary> City of Scottsdale. </summary>
        Scottsdale,
        /// <summary> Sunrise Utility Company . </summary>
        Sunrise,
        /// <summary> City of Tempe. </summary>
        Tempe,
        /// <summary> City of Tolleson. </summary>
        Tolleson,
        /// <summary> Valley Utility Company . </summary>
        Valley_Utilities,
        /// <summary> West End Utility Company . </summary>
        West_End,
        // All eProviders from this point on are not consider a valid provider enum by most routines
        /// <summary> Regional Summary. </summary>
        /// <remarks>Currently Not Supported</remarks>
        Regional,
        /// <summary> Summary of Areas On Project. </summary>
        /// <remarks>Currently Not Supported</remarks>
        OnProject,
        /// <summary> Summary of Areas Off Project. </summary>
        /// <remarks>Currently Not Supported</remarks>
        OffProject

     };
    //---------------------------------------------------------

    enum ColoradoRiverRecord { eUnspecified, ePaleoRecord, eBureauRecord, eUserSupplied };
#endregion

    /*********************************************************
     *   Provider Classes
     * 
     * 
     * **************************************************************/
    #region provider
    //===========================================
    // ProviderClass

    /// <summary>
    /// This a static class that provides support methods and constants for the eProvider enum
    /// </summary>
    public static class ProviderClass
    {
        // Provider Routines, Constants and enums
        /// <summary>
        /// The last valid provider enum value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider LastProvider = eProvider.West_End;

        /// <summary>
        /// The first valid enum value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider FirstProvider = eProvider.Adaman_Mutual;

        /// <summary>
        /// The Last valid Aggregator value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider LastAggregate = eProvider.OffProject;

        /// <summary>
        /// The number of valid Provider (eProvider) enum values for use with WaterSimModel and ProviderIntArray.
        /// </summary>
        /// <value>count of valid eProvider enums</value>
        /// <remarks>all providers after LastProvider are not considered one of the valid eProvider enum value</remarks>
        public const int NumberOfProviders = (int)LastProvider + 1;

        /// <summary>
        /// The number of valid Provide Aggregate (eProvider) enum values.
        /// </summary>
        /// <value>count of valid eProvider enums</value>
        /// <remarks>all providers after LastProvider are not considered one of the valid eProvider enum value</remarks>
        public const int NumberOfAggregates = ((int)LastAggregate - (int)LastProvider );

        internal const int TotalNumberOfProviderEnums = NumberOfProviders + NumberOfAggregates;
        //---------------------------------------------------------

        //---------------------------------------------------------
        /// <summary>
        /// Enumerable Collection of eProviders
        /// </summary>
        /// <returns>eProvider</returns>
        public static IEnumerable<eProvider> providers()
        {
            for (eProvider i = FirstProvider; i < (LastProvider+1); i++)
            {
                yield return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IncludeAggregates"></param>
        /// <returns></returns>
        public static IEnumerable<eProvider> providers(bool IncludeAggregates)
        {
            if (IncludeAggregates)
            {
                for (eProvider i = FirstProvider; i <= LastAggregate; i++)
                {
                    yield return i;
                }
            }
            else
            {
                for (eProvider i = FirstProvider; i <= LastAggregate; i++)
                {
                    yield return i;
                }
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Enumerates providers all in this collection. </summary>
        ///
        /// <remarks>   Ray Quay, 1/28/2014. </remarks>
        ///
        /// <returns>   An enumerator that allows foreach to be used to process providers all in this
        ///             collection. </returns>
        ///-------------------------------------------------------------------------------------------------

        public static IEnumerable<eProvider> providersAll()
        {
            for (eProvider i = FirstProvider; i <=LastAggregate; i++)
            {
                yield return i;
            }
        }
        //-------------------------------------------------------------
        /// <summary>
        /// Determines of index is a valid eProvider array index
        /// </summary>
        /// <param name="index">an index to check</param>
        /// <returns>true if valid, otherwise false</returns>
        public static bool valid(int index)
        {
            return ((index >= 0) & (index <= (int)LastProvider));
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Determines if valid eProvider value
        /// </summary>
        /// <param name="p">an eProvider value to check</param>
        /// <returns>true if valid</returns>
        public static bool valid(eProvider p)
        {
            return (((int)p >= 0) & (p <= LastProvider));
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Determines if valid eProvider fieldname
        /// </summary>
        /// <param name="fieldname">a fieldname to check</param>
        /// <returns>true if valid</returns>
        public static bool valid(string fieldname)
        {
            bool found = false;
            int pindex = -1;
            fieldname = fieldname.ToUpper().Trim();
            for (int i = 0; i < FieldNameList.Length; i++)
            {
                if (FieldNameList[i].ToUpper() == fieldname)
                {
                    pindex = i;
                    found = true;
                    break;
                }
            }
            return found;
        }
        //-------------------------------------------------------------
        /// <summary>
        /// Determines if index is a valid eProvider Aggregate index
        /// </summary>
        /// <param name="index">an index to check</param>
        /// <returns>true if valid, otherwise false</returns>
        public static bool validAggregate(int index)
        {
            return ((index > (int)LastProvider) & (index <= (int)LastAggregate));
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Determines if valid eProvider Aggregate value
        /// </summary>
        /// <param name="p">an eProvider value to check</param>
        /// <returns>true if valid</returns>
        public static bool validAggregate(eProvider p)
        {
            return (((int)p > (int)LastProvider) & (p <= LastAggregate));
        }
        //-----------------------------------------------------------------
        //-----------------------------------------------------------------
        internal static void TestAndThrowInvalidProviderException(eProvider p, Boolean IncludeAggregate)
        {
            bool test = true;
            if (!valid(p))
                if (!IncludeAggregate)
                    test = false;
                else
                    if (!validAggregate(p))
                        test = false;
            if (!test)
               { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalidProvider) + p.ToString()); }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Returns an eProvider based on its fieldname
        /// </summary>
        /// <param name="fieldname">a fieldname to look up</param>
        /// <returns>a valid eProvider value</returns>
        /// <exception cref="WaterSim_Exception"> if not a valid fieldname</exception>
        public static eProvider provider(string fieldname)
        {
            if (valid(fieldname))
            {
                int pindex = -1;
                FastFindFieldname(fieldname, ref pindex);
                return (eProvider)pindex;
            }
            else
            {
                throw new WaterSim_Exception("Invlaid Provider Fieldname: " + fieldname);

            }

        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Returns the eProvider value that corresponds to the eProvider array index
        /// </summary>
        /// <param name="index">index for eProvider</param>
        /// <returns>a valid eProvider value</returns>
        /// <exception cref="WaterSim_Exception"> if not a valid index</exception>
        public static eProvider provider(int index)
        {
            if (valid(index))
            {
                return (eProvider)index;
            }
            else
            {
                throw new WaterSim_Exception("Provider index -" + index.ToString() + " is out of range of " + FirstProvider.ToString() + " to " + LastProvider.ToString());

            }
        }

        public static eProvider providerAll(int index)
        {
            if (valid(index))
            {
                return (eProvider)index;
            }
            else
            {
                if (validAggregate(index))
                {
                    return (eProvider)index;
                }
                else
                   throw new WaterSim_Exception("Provider index -" + index.ToString() + " is out of range of " + FirstProvider.ToString() + " to " + LastAggregate.ToString());

            }
        }

        //-----------------------------------------------------------------
        /// <summary>
        /// Provider Array index for eProvider value
        /// </summary>
        /// <param name="p">provider</param>
        /// <returns>index of provider</returns>
        public static int index(eProvider p)
        {
            TestAndThrowInvalidProviderException(p,false);
            return (int)p;
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Provider Array index for eProvider value
        /// </summary>
        /// <param name="p">provider</param>
        /// <param name="includeAggregate"> true to include, false to exclude the aggregate. </param>
        /// <returns>index of provider</returns>
        ///-------------------------------------------------------------------------------------------------

        public static int index(eProvider p, bool includeAggregate)
        {
            TestAndThrowInvalidProviderException(p, includeAggregate);
            return (int)p;
        } 
        
        //-----------------------------------------------------------------
        /// <summary>
        /// Returns the index of the eProvider based on is fieldname.  Very slow, First checks if valid and thorws exception of not
        /// </summary>
        /// <param name="fieldname">an eProvider fieldname</param>
        /// <returns>index of eProvider</returns>
        /// <exception cref="WaterSim_Exception"> if no valid fieldname</exception>
        public static int index(string fieldname)
        {
            if (!valid(fieldname)) throw new WaterSim_Exception("Invlaid provider fieldname: fieldname");
            int pindex = -1;
            FastFindFieldname(fieldname, ref pindex);
            return pindex;
        }
        //-----------------------------------------------------------------
        /// <summary>
        /// Returns if fieldname is found.  Much faster, one search no exceptions, does not check for valid eProvider values, will find index for Aggregates, Use with Caution.  Should not be used for finding
        /// an index for a ProviderIntArray
        /// </summary>
        /// <param name="fieldname">an eProvider fieldname</param>
        /// <param name="index">ref for index, returns -1 if returns false</param>
        /// <returns>true of valid eProvider fieldname, false otherwise</returns>
        public static bool FastFindFieldname(string fieldname, ref int index)
        {
            bool found = false;
            int pindex = -1;
            fieldname = fieldname.ToUpper().Trim();
            for (int i = 0; i < FieldNameList.Length; i++)
            {
                if (FieldNameList[i].ToUpper() == fieldname)
                {
                    pindex = i;
                    found = true;
                    break;
                }
            }
            index = pindex;
            return found;
        }
        //-------------------------------------------------------------
        /// <summary>
        /// Full text label for eProvider and Aggregate
        /// </summary>
        /// <param name="p">provider</param>
        /// <returns>label</returns>
        public static string Label(eProvider p)
        {
            TestAndThrowInvalidProviderException(p,true);
            return ProviderNameList[(int)p];
        }
        //-------------------------------------------------------------
        /// <summary>
        /// Fieldname for Provider and Aggregate
        /// </summary>
        /// <param name="p">provider</param>
        /// <returns>fieldname</returns>
        public static string FieldName(eProvider p)
        {
            TestAndThrowInvalidProviderException(p,true);
            return FieldNameList[(int)p];
        }
        //-------------------------------------------------------------
        //-------------------------------------------------------------
        private static string[] ProviderNameList = new string[TotalNumberOfProviderEnums]    {       
  
            "Adaman Mutual",
            "White Tanks",
            "Paradise Valley",
            "Sun City",
            "Sun City West",
            "Avondale",
            "Berneil",
            "Buckeye",
            "Carefree",
            "Cave Creek",
            "Chandler",
            "Chaparral City",
            "Surprise",
            "Clearwater Utilities",
            "Desert Hills",
            "El Mirage",
            "Gilbert",
            "Glendale",
            "Goodyear",
            "Litchfield Park",
            "Mesa",
            "Peoria",
            "Phoenix",
            "Queen Creek",
            "Rigby",
            "Rio Verde",
            "Rose Valley",
            "Scottsdale",
            "Sunrise",
            "Tempe",
            "Tolleson",
            "Valley Utilities",
            "West End",
            "Region",

            "On Project",
            "Off Project"
             };

        private static string[] FieldNameList = new string[TotalNumberOfProviderEnums]  {       
  
       "ad", //providers.Adaman_Mutual;
       "wt", //providers.White_Tanks;
       "pv", //providers.Paradise_Valley;
       "su", //providers.Sun_City;
       "sw", //providers.Sun_City_West;
       "av", //providers.Avondale;
       "be", //providers.Berneil;
       "bu", //providers.Buckeye;
       "cf", //providers.Carefree;
       "cc", //providers.Cave_Creek;
       "ch", //providers.Chandler;
       "cp", //providers.Chaparral_City;
       "sp", //providers.Surprise;
       "cu", //providers.Clearwater_Utilities;
       "dh", //providers.Desert_Hills;
       "em", //providers.El_Mirage;
       "gi", //providers.Gilbert;
       "gl", //providers.Glendale;
       "go", //providers.Goodyear;
       "lp", //providers.Litchfield_Park;
       "me", //providers.Mesa;
       "pe", //providers.Peoria;
       "ph", //providers.Phoenix;
       "qk", //providers.Queen_Creek;
       "rg", //providers.Rigby;
       "rv", //providers.Rio_Verde;
       "ry", //providers.Rose_Valley;
       "sc", //providers.Scottsdale;
       "sr", //providers.Sunrise;
       "te", //providers.Tempe;
       "to", //providers.Tolleson;
       "vu", //providers.Valley_Utilities;
       "we", //providers.West_End;
       "reg", // eProvider.Region 
       "onp", // eProvider.OnProject
       "ofp" // eProvider.OffProject
       // "aj", // Apache Junction
       // "an", // American Water Anthem
    };
        
        private static eProvider[] RegionProviders = new eProvider[NumberOfProviders] {
            eProvider.Adaman_Mutual,
            eProvider.White_Tanks,
            eProvider.Paradise_Valley,
            eProvider.Sun_City,
            eProvider.Sun_City_West,
            eProvider.Avondale,
            eProvider.Berneil,
            eProvider.Buckeye,
            eProvider.Carefree,
            eProvider.Cave_Creek,
            eProvider.Chandler,
            eProvider.Chaparral_City,
            eProvider.Surprise,
            eProvider.Clearwater_Utilities,
            eProvider.Desert_Hills,
            eProvider.El_Mirage,
            eProvider.Gilbert,
            eProvider.Glendale,
            eProvider.Goodyear,
            eProvider.Litchfield_Park,
            eProvider.Mesa,
            eProvider.Peoria,
            eProvider.Phoenix,
            eProvider.Queen_Creek,
            eProvider.Rigby,
            eProvider.Rio_Verde,
            eProvider.Rose_Valley,
            eProvider.Scottsdale,
            eProvider.Sunrise,
            eProvider.Tempe,
            eProvider.Tolleson,
            eProvider.Valley_Utilities,
            eProvider.West_End 
        };

        private static eProvider[] OnProjectProviders = new eProvider[10] {
            eProvider.Avondale,
            eProvider.Chandler,
            eProvider.Gilbert,
            eProvider.Glendale,
            eProvider.Mesa,
            eProvider.Peoria,
            eProvider.Phoenix,
            eProvider.Scottsdale,
            eProvider.Tempe,
            eProvider.Tolleson,
        };

        private static eProvider[] OffProjectProviders = new eProvider[23] {
            eProvider.Adaman_Mutual,
            eProvider.White_Tanks,
            eProvider.Paradise_Valley,
            eProvider.Sun_City,
            eProvider.Sun_City_West,
            eProvider.Berneil,
            eProvider.Buckeye,
            eProvider.Carefree,
            eProvider.Cave_Creek,
            eProvider.Chaparral_City,
            eProvider.Surprise,
            eProvider.Clearwater_Utilities,
            eProvider.Desert_Hills,
            eProvider.El_Mirage,
            eProvider.Goodyear,
            eProvider.Litchfield_Park,
            eProvider.Queen_Creek,
            eProvider.Rigby,
            eProvider.Rio_Verde,
            eProvider.Rose_Valley,
            eProvider.Sunrise,
            eProvider.Valley_Utilities,
            eProvider.West_End 
        
        };

        public static eProvider[] GetRegion(eProvider ep)
        {
            switch (ep)
            {
                case eProvider.Regional:
                    return RegionProviders;
                case eProvider.OffProject:
                    return OffProjectProviders;
                case eProvider.OnProject:
                    return OnProjectProviders;
                default:
                    return null;
            }
        }
    }  // end or ProviderClass
    // Speical Range Check
#endregion

    //**************************************************
    // Data classes and Structures 
    // 
    // *************************************************
    #region DataClasses
    /// <summary>   Dcheck base. </summary>
    /// <remarks>   Delegate for special range check routines. </remarks>
    /// <param name="value">            The value to check. </param>
    /// <param name="errorstr">         [in,out] if error return a string. </param>
    /// <param name="aModelParameter">  a model parameter. </param>
    /// <returns>  True if valeu passes range check, else false </returns>

    public delegate bool DcheckBase(int value, ref string errorstr, ModelParameterBaseClass aModelParameter);

    //--------------------------------------------------------------------------------------------
    /// <summary>   Dcheck provider. </summary>
    /// <remarks>  Delegate for special range checks of provider input paramteres. </remarks>
    /// <param name="value"> The value to check. </param>
    /// <param name="ep">  the provider </param>
    /// <param name="errorstr">         [in,out] if error return a string. </param>
    /// <param name="aModelParameter">  a model parameter. </param>
    /// <returns>  True if valeu passes range check, else false </returns>

    public delegate bool DcheckProvider(int value, eProvider ep, ref string errorstr, ModelParameterBaseClass aModelParameter);

    //================================================================
    // GET AND SET MOCDEL VALUE DELEGATES
    // 
    // =================================================================
    //--------------------------------------------------------------------------------------------
    // INT DELEGATES
    // get int delegate
    /// <summary>   Delagate for "Gets an int" method </summary>
    /// <returns>  just an int </returns>
    public delegate int Dget();
    // Set int delegate
    /// <summary>   Delagate for "Sets an int" method </summary>
    public delegate void Dset(int value);

    // DOUBLE DELEGATES
    // get double delegate
    /// <summary>   Delagate for "Gets a double" method </summary>
    /// <returns>  just a double </returns>
    public delegate int DgetDouble();
    // Set double delegate
    /// <summary>   Delagate for "Sets a double" method </summary>
    public delegate void DsetSouble(double value);
    
    // INT ARRAY DELGATES
    // Getarray delegate
    /// <summary>   Delagate for "Gets an int[]" method </summary>
    /// <returns>  just an int[] </returns>
    public delegate int[] Dgetarray();
    // Setarayy delegate
    /// <summary>   Delagate for "Sets an int[]" method </summary>
    public delegate void Dsetarray(int[] value);

    // DOPUBLE ARRAY DELGATES
    // GetDoublearray delegate
    /// <summary>   Delagate for "Gets a double[]" method </summary>
    /// <returns>  just an int[] </returns>
    public delegate double[] DgetDoublearray();
    // SetDoublearayy delegate
    /// <summary>   Delagate for "Sets a double[]" method </summary>
    public delegate void DsetDoublearray(double[] value);

    // GRID DELEGATES
    // GetGrid2D delegate
    /// <summary>   Delagate for "Gets a double[,]" method </summary>
    /// <returns>  just an double[,] </returns>
    public delegate double[,] DGetGrid2D();
    // SetGrid2D delegate
    /// <summary>   Delagate for "Sets a double[,]" method </summary>
    public delegate void DSetGrid2D(double[,] value);
    // GetGrid3D delegate
    /// <summary>   Delagate for "Gets a double[,,]" method </summary>
    /// <returns>  just an double[,,] </returns>
    public delegate double[,,] DGetGrid3D();
    // SetGrid3D delegate
    /// <summary>   Delagate for "Sets a double[,,]" method </summary>
    public delegate void DSetGrid3D(double[,,] value);
 

    //================================================================
    //  providerArrayPropertyBaseClassBaseClass  Initial Structure for providing access get/set to
    //  provider data
    // 
    // =================================================================

    /// <summary>
    /// Base class for All Provider Model Parameters
    /// </summary>
    /// <remark>This is the abstract base class that sets up a structure used to provde an indexed property of provider data </remark>
   
    public abstract class providerArrayPropertyBaseClass
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public providerArrayPropertyBaseClass() : base() { }

        ////------------------
        /// <summary>
        /// ABSTRACT indexer for indexed array gets and sets based on eProvider enum values
        /// </summary>
        /// <param name="index">eProvider index</param>
        /// <value>indexed Model Parameter value</value>
        /// <returns>value in provider array for eProvider index</returns>
        /// <exception cref="WaterSim_Exception">Should throw exception if set and value viloates a range rule</exception>
        public abstract int this[eProvider index] { get; set; }
   
        
        /// <summary>
        /// indexer for indexed array gets and sets
        /// </summary>
        /// <param name="index">index to array</param>
        /// <value>indexed Model Parameter value</value>
        /// <returns>value in provider array for index</returns>
        /// <exception cref="WaterSim_Exception">if set and value viloates a range rule</exception>
        public virtual int this[int index]
        {
            get
            {
                return this[(eProvider)index];
            }
            set
            {
                this[(eProvider)index] = value;
            }
        }        //----------------------
        /// <summary>
        /// ABSTRACT Gets Model Parameter Array values 
        /// </summary>
        /// <returns>an Array of Model Values</returns>
        public abstract ProviderIntArray getvalues(); 

        //----------------------
        /// <summary>
        /// ABSTRACT Set Model Parameter array using value array
        /// </summary>
        /// <param name="value">values to set Model Parameter</param>
        /// <exception cref="WaterSim_Exception">if any numbers in value violate a range rule</exception>
        public abstract void setvalues(ProviderIntArray value);
    }
    //-------------------------------------------------------------------------------------
    // This is the class used to provde and indexed property to wrap around the provider input array parammeters
    /// <summary>
    /// Class for All Provider Model Parameters that access the WaterSimManager model directly
    /// </summary>
     /// <remarks>This is the class used to provde an indexed property to wrap around the provider input array parammeters that need direct access to the 
     /// 		  Fortan model Parameters, with a layer of rangechacking that throws exception when values violate range check rules.  
     /// 		  The standard constructor for this class is hidden.  Parameters of this class are created by the WaterSimManager class and managed by the 
    /// 		  WaterSimManager.ParamManager, thus the constructor is hidden.  However default cconstructor is exposed so new classes can be derived.</remarks>
   public class providerArrayProperty : providerArrayPropertyBaseClass
   {
       // get data references
       /// <summary> A function used to get an array of values. </summary>
       protected Dgetarray Fget;
       /// <summary> A function used to set and array of values. </summary>
       protected Dsetarray Fset;
       /// <summary> The ParameterManager. </summary>
       protected ParameterManagerClass Fpm;
       /// <summary> The code for this parameter. </summary>
       protected int Femp;
       /// <summary> The aggregate mode. </summary>
       protected eProviderAggregateMode FAggregateMode;
       //------------------
       
       ///-------------------------------------------------------------------------------------------------
       /// <summary> Default constructor. </summary>
       ///
       /// <remarks> This base constructor has been exposed.  Does not implement anything, thus to create a new providerArrayProperty, a new class will
       /// 		  need to be created, with a new constructor to overload this base constructor.</remarks>
       ///-------------------------------------------------------------------------------------------------

       public providerArrayProperty() : base() { }
       
       //------------------
       // 
       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Constructor. </summary>
       /// <remarks>readonly constructor, no need for Parameter Manager or AggregateMode becuse there is no range checking or Aggregation for read only
       /// </remarks>
       /// <param name="modelparam">    The modelparam. </param>
       /// <param name="getcall">       The getcall. </param>
       ///-------------------------------------------------------------------------------------------------

       public providerArrayProperty(int modelparam, Dgetarray getcall)
       {
           SetFields(modelparam, getcall, null, eProviderAggregateMode.agNone, null);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Constructor. </summary>
       /// <remarks>        Ok, this parameter has aggregation so we need the parameter manager but this is still readonly
       /// </remarks>
       /// <param name="PM">            The ParameterManager. </param>
       /// <param name="modelparam">    The modelparam. </param>
       /// <param name="getcall">       The getcall. </param>
       /// <param name="AggregateMode"> The aggregate mode. </param>
       ///-------------------------------------------------------------------------------------------------

       public providerArrayProperty(ParameterManagerClass PM, int modelparam, Dgetarray getcall, eProviderAggregateMode AggregateMode)
       {
           SetFields(modelparam, getcall, null, AggregateMode, PM);
       }

       internal void SetFields(int modelparam, Dgetarray getcall, Dsetarray setcall, eProviderAggregateMode AggregateMode, ParameterManagerClass PM)
       {
           if (getcall == null) throw new WaterSim_Exception(WS_Strings.wsdbNullParameter);
           if (!ModelParamClass.valid(modelparam)) throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
          
           Fget = getcall;
           Fset = setcall;
           Fpm = PM;
           Femp = modelparam;
           FAggregateMode = AggregateMode;
       }
       // Read Write Constructor, Need it all baby!

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Constructor. </summary>
       ///
       /// <remarks>        OK, This is the full thing, read write and all the works
       /// </remarks>
       ///
       /// <exception cref="WaterSim_Exception"> Thrown when watersim_. </exception>
       ///
       /// <param name="pm">            The pm. </param>
       /// <param name="modelparam">    The modelparam. </param>
       /// <param name="getcall">       The getcall. </param>
       /// <param name="setcall">       The setcall. </param>
       /// <param name="AggregateMode"> The aggregate mode. </param>
       ///-------------------------------------------------------------------------------------------------

       internal providerArrayProperty(ParameterManagerClass pm, int modelparam, Dgetarray getcall, Dsetarray setcall, eProviderAggregateMode AggregateMode)
           : base()
       {
           SetFields(modelparam, getcall, setcall, AggregateMode, pm);
       }
       //------------------------------------
       /// <summary>
       /// the eModelParam value
       /// <value> the eModelParam for the ModelParameter this property is supporting</value>
       /// </summary>
       public int ModelParam
       { get { return Femp; } set { Femp = value; } }

       //----------------------
       /// <summary>
       /// indexer for indexed array gets and sets based on eProvider enum values
       /// </summary>
       /// <param name="index">eProvider index</param>
       /// <value>indexed Model Parameter value</value>
       /// <returns>value in provider array for eProvider index</returns>
       /// <exception cref="WaterSim_Exception">if set and value viloates a range rule</exception>
       public override int this[eProvider index]
       {
           get
           {
               if (ProviderClass.valid(index))
               {
                   int[] temp;
                   temp = Fget();
                   return temp[(int)index];
               }
               else
               {
                  if (!ProviderClass.validAggregate(index))
                   {
                       throw new WaterSim_Exception(WS_Strings.wsInvalidProvider);
                   }
                   else
                   {
                      //  insert aggregation code
                    return RegionalValue(index);
                   }
               
              }
           }
           set
           {
               if (Fset == null) throw new WaterSim_Exception(WS_Strings.wsReadOnly);
               if (!ProviderClass.valid(index)) throw new WaterSim_Exception(WS_Strings.wsInvalidProvider);
               string errMessage = "";
               if (Fpm.CheckProviderValueRange(Femp, value, (eProvider)index, ref errMessage))
               {
                   int[] temp = Fget();
                   temp[(int)index] = value;
                   Fset(temp);
               }
               else throw new WaterSim_Exception(errMessage);
           }
       }
       //------------------------------------------------------------
       /// <summary>
       /// Gets Model Parameter Array values 
       /// </summary>
       /// <returns>an Array of Model Values</returns>
       public override ProviderIntArray getvalues()
       // public int[] getvalues()
       {
           ProviderIntArray pia = new ProviderIntArray(0);
           pia.Values = Fget();
           return pia;
           //return Fget();
       }
       //----------------------
       /// <summary>
       /// Set Model Parameter array using value array
       /// </summary>
       /// <param name="value">values to set Model Parameter</param>
       /// <exception cref="WaterSim_Exception">if any numbers in value violate a range rule</exception>
       public override void setvalues(ProviderIntArray value)
       {
           if (Fset == null) throw new WaterSim_Exception(WS_Strings.wsReadOnly);
           Fpm.CheckProviderValuesRange(Femp, value);
           Fset(value.Values);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Gets the aggregate mode. </summary>
       ///
       /// <value> The aggregate mode. </value>
       ///-------------------------------------------------------------------------------------------------

       public eProviderAggregateMode AggregateMode
       { get { return FAggregateMode; } }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Regional value. </summary>
       /// <param name="agmode"> The agmode. </param>
       /// <param name="Data">   The data. </param>
       /// <param name="Pop">    The POP. </param>
       /// <remarks> Static version of the </remarks>
       /// <returns> The value of all the providers in the regiona aggregated </returns>
       ///-------------------------------------------------------------------------------------------------

       public static int RegionalValue(eProviderAggregateMode agmode, int[] Data, int[] Weight)
       {

           int temp = SpecialValues.MissingIntValue;

           switch (agmode)
           {
               case eProviderAggregateMode.agNone:
                   {
                       break;  // no need to do anything altready set at missing
                   }
               case eProviderAggregateMode.agAverage:
                   {
                       int total = 0;
                       int cnt = 0;
                       foreach (int value in Data)
                       {
                           cnt++;
                           total += value;
                       }
                       if (cnt > 0)
                           temp = total / cnt;
                       break;
                   } //agAverage
               case eProviderAggregateMode.agSum:
                   {
                       int total = 0;
                       foreach (int value in Data)
                       {
                           total += value;
                       }
                       temp = total;
                       break;
                   } // ag sum
                   
               case eProviderAggregateMode.agWeighted:
                   {
                       Int64 totalweight = 0;
                       Int64 total = 0;
                         for (int i = 0; i < Data.Length; i++)
                               {  Int64 Pop64 = Convert.ToInt64(Weight[i]);
                                   totalweight += Pop64;
                                   total += Convert.ToInt64(Data[i]) * Pop64;
                               }
                               if (totalweight > 0)
                               {  Int64 result64 = total / totalweight;
                                   if (result64<2147483647)
                                       temp =  Convert.ToInt32(result64);
                               }
                         
                       }
                       break; // agweighted
               default:
                   {
                       break;
                   }
           }  // switch


           return temp;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary> Regional value. </summary>
       ///
       /// <remarks> Ray, 1/24/2013. </remarks>
       ///
       /// <returns> The value of all the providers in the regiona aggregated. </returns>
       ///-------------------------------------------------------------------------------------------------

       public int RegionalValue(eProvider ep)
       {
           int result = SpecialValues.MissingIntValue;
           ProviderIntArray pia = new ProviderIntArray(0);
           ModelParameterClass MP = Fpm.Model_Parameter(eModelParam.epPopulation_Used);
           ProviderIntArray pop = MP.ProviderProperty.getvalues();
           pia.Values = Fget();
           eProvider[] RegionProviders = ProviderClass.GetRegion(ep);
           if (RegionProviders != null)
           {
               int[] Values = new int[RegionProviders.Length];
               int[] Weights = new int[RegionProviders.Length];

               for (int i = 0; i < RegionProviders.Length; i++)
               {
                   Values[i] = pia[ProviderClass.index(RegionProviders[i])];
                   Weights[i] = pop[ProviderClass.index(RegionProviders[i])];
               }
               result = RegionalValue(FAggregateMode, Values, Weights);
           }
           return result;
       }  // region value
       

   }
    //---------------------------------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   DoubleGrid2D. </summary>
        ///
        /// <remarks>   This is a prototype object that represents a 2D grid of double values used with the Mode FLow Model </remarks>
        ///-------------------------------------------------------------------------------------------------

     public struct DoubleGrid2D
     {
            double[,] FGridData;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="defaultValue"> The default value. </param>
        ///-------------------------------------------------------------------------------------------------

        public DoubleGrid2D(double defaultValue)
        {
            FGridData = new double[GridValues.RowNumber, GridValues.ColumnNumber];
            for (int row = 0; row<GridValues.RowNumber; row++)
                for (int col = 0; col<GridValues.ColumnNumber; col++)
                    FGridData[row,col] = defaultValue;
        }

        internal bool validIndex(int row, int column)
        {
            return ((row > -1) && (column > -1) && (row < RowLength) && (column < ColumnLength));
        }

         ///-------------------------------------------------------------------------------------------------
         /// <summary>
         ///    Indexer to get or set items within this collection using array index syntax.
         /// </summary>
         ///
         /// <param name="row">     The row. </param>
         /// <param name="column">  The column. </param>
         ///
         /// <returns>  The indexed item. </returns>
         ///-------------------------------------------------------------------------------------------------
         public double this[int row, int column]
         {
            get
            {
                if (validIndex( row, column))
                    return FGridData[ row, column];
                else
                    throw new WaterSim_Exception(WS_Strings.wsInvalidGridIndex);
            }
            set
            {
                if (validIndex( row, column))
                    FGridData[row, column] = value;
                else
                    throw new WaterSim_Exception(WS_Strings.wsInvalidGridIndex);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the information describing the grid. </summary>
        ///
        /// <value> Information describing the grid. </value>
        ///-------------------------------------------------------------------------------------------------

        public double[,] GridData
        {
            get { return FGridData;}
            set { FGridData = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the Number of the rows. </summary>
        ///
        /// <value> The Number of rows. </value>
        ///-------------------------------------------------------------------------------------------------

        public int RowLength
        { get { return GridValues.RowNumber; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of columns. </summary>
        ///
        /// <value> The number of columns. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ColumnLength
        { get { return GridValues.ColumnNumber; } }

    }
    //--------------------------------------------------------------------------

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   DoubleGrid3D. </summary>
     /// <remarks>   This is a prototype object that represents a 3D grid of double values used with the Mode FLow Model </remarks>
     ///
    ///-------------------------------------------------------------------------------------------------

    public struct DoubleGrid3D
    {
        double[,,] FGridData;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="defaultValue"> The default value. </param>
        ///-------------------------------------------------------------------------------------------------

        public DoubleGrid3D(double defaultValue)
        {
            FGridData = new double[GridValues.LayerNumber, GridValues.RowNumber, GridValues.ColumnNumber];
            for (int lyr = 0; lyr<GridValues.LayerNumber; lyr++)
              for (int row = 0; row < GridValues.RowNumber; row++)
                for (int col = 0; col < GridValues.ColumnNumber; col++)
                    FGridData[lyr,row, col] = defaultValue;

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Valid index. </summary>
        /// <remarks> Returns a true valeu of the Layer, Row, and Col indexes are valid for this 3D array</remarks>
        /// <param name="layer">    The layer. </param>
        /// <param name="row">      The row. </param>
        /// <param name="column">   The column. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        internal bool validIndex(int layer, int row, int column)
        {
            return ((layer > -1) && (row > -1) && (column > -1) && (layer < LayerLength) && (row < RowLength) && (column < ColumnLength));
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Indexer to get or set items within this collection using array index syntax.
        /// </summary>
        ///
        /// <param name="layer">    The layer. </param>
        /// <param name="row">      The row. </param>
        /// <param name="column">   The column. </param>
        ///
        /// <returns>   The indexed item. </returns>
        ///-------------------------------------------------------------------------------------------------

        public double this[int layer, int row, int column]
        {
            get
            {
                if (validIndex(layer, row, column))
                    return FGridData[layer, row, column];
                else
                    throw new WaterSim_Exception(WS_Strings.wsInvalidGridIndex);
            }
            set
            {
                if (validIndex(layer, row, column))
                    FGridData[layer, row, column] = value;
                else
                    throw new WaterSim_Exception(WS_Strings.wsInvalidGridIndex);
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the information describing the grid. </summary>
        ///
        /// <value> Information describing the grid. </value>
        ///-------------------------------------------------------------------------------------------------

        public double[,,] GridData
        {
            get { return FGridData; }
            set { FGridData = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of the layers. </summary>
        ///
        /// <value> The number of layers. </value>
        ///-------------------------------------------------------------------------------------------------

        public int LayerLength
        { get { return GridValues.LayerNumber; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of rows. </summary>
        ///
        /// <value> The number of rows. </value>
        ///-------------------------------------------------------------------------------------------------

        public int RowLength
        { get { return GridValues.RowNumber; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of columns. </summary>
        ///
        /// <value> The number of columns. </value>
        ///-------------------------------------------------------------------------------------------------

        public int ColumnLength
        { get { return GridValues.ColumnNumber; } }
    }
    //----------------------------------------------------------------------------

   ///-------------------------------------------------------------------------------------------------
   /// <summary>    Grid2d property. </summary>
   ///-------------------------------------------------------------------------------------------------

   public class Grid2DProperty
   {
       /// <summary> The ParameterManager. </summary>
       protected ParameterManagerClass Fpm;
       /// <summary> The code for this parameter. </summary>
       protected int Femp;

       // Get Grid method
       DGetGrid2D FGet;
       // Set Grid Method
       DSetGrid2D FSet;

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Constructor. </summary>
       /// <exception cref="WaterSim_Exception">    Thrown when invlaid modelparam or get or set = null
       ///                                          </exception>
       ///
       /// <param name="pm">            The ParameterManager. </param>
       /// <param name="modelparam">    the eModelParam for the ModelParameter this property is
       ///                              supporting. </param>
       /// <param name="getcall">       The getcall. </param>
       /// <param name="setcall">       The setcall. </param>
       ///-------------------------------------------------------------------------------------------------

       public Grid2DProperty(ParameterManagerClass pm, int modelparam, DGetGrid2D getcall, DSetGrid2D setcall)
       {
           if ((pm == null) | (getcall == null)) throw new WaterSim_Exception(WS_Strings.wsdbNullParameter);
           if (!ModelParamClass.valid(modelparam)) throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
           Fpm = pm;
           Femp = modelparam;
           FGet = getcall;
           FSet = setcall;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Constructor. </summary>
       /// <exception cref="WaterSim_Exception">    Thrown when a water simulation error condition
       ///                                          occurs. </exception>
       ///
       /// <param name="pm">            The ParameterManager. </param>
       /// <param name="modelparam">    the eModelParam for the ModelParameter this property is
       ///                              supporting. </param>
       /// <param name="getcall">       The getcall. </param>
       ///-------------------------------------------------------------------------------------------------

       public Grid2DProperty(ParameterManagerClass pm, int modelparam, DGetGrid2D getcall)
       {
           if ((pm == null) | (getcall == null)) throw new WaterSim_Exception(WS_Strings.wsdbNullParameter);
           if (!ModelParamClass.valid(modelparam)) throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
           Fpm = pm;
           Femp = modelparam;
           FGet = getcall;
           FSet = null;
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Gets a value indicating whether this object is read only. </summary>
       ///
       /// <value>  true if this object is read only, false if not. </value>
       ///-------------------------------------------------------------------------------------------------

       public bool IsReadOnly
       { get { return (FSet == null); } }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Gets the values in a DoubleGrid2D struct. </summary>
       ///
       /// <exception cref="WaterSim_Exception">    Thrown when Fget is not set. </exception>
       ///
       /// <returns>    DoubleGrid2D struct. </returns>
       ///-------------------------------------------------------------------------------------------------

       public DoubleGrid2D GetValues()
       {
           if (FGet != null)
           {
               DoubleGrid2D temp = new DoubleGrid2D();
               temp.GridData = FGet();
               return temp;
           }
           else
               throw new WaterSim_Exception(WS_Strings.wsGetIsNull);
       }

       ///-------------------------------------------------------------------------------------------------
       /// <summary>    Sets the values using a DoubleGrid2D struct. </summary>
       ///
       /// <exception cref="WaterSim_Exception">    Thrown when a property is read only </exception>
       ///
       /// <param name="value"> The value. </param>
       ///-------------------------------------------------------------------------------------------------

       public void SetValues(DoubleGrid2D value)
       {
           if (FSet != null)
           {
               FSet(value.GridData);
           }
           else
               throw new WaterSim_Exception(WS_Strings.wsSetIsNull);
       }

       //------------------------------------
       /// <summary>
       /// the eModelParam value
       /// <value> the eModelParam for the ModelParameter this property is supporting</value>
       /// </summary>
       public int ModelParam
       { get { return Femp; } set { Femp = value; } }
   }
    #endregion
    //----------------------------------------------------------------
    //BASIC DATA / PROVIDER ARRAYS
    //--------------------------------------------------------------------------------- 
    #region DataArrays
    /// <summary>
   /// An array structure for provider integer data.  Each cell corresponds to a different provider.  Used to set and retrieve ModelParameter data.  See ProviderClass for indexes to array 
   /// <see cref="ProviderClass"/> 
   /// </summary>
    public struct ProviderIntArray
    {
        internal bool FIncludesAggregates;
        /// <summary> The int values array</summary>
        internal int[] FValues;  // change to internal 1/20/14
        /// <summary>   The length of the Values array </summary>
        /// <value> The length zero based. </value>
        public int Length
        { get 
            {
                TestValues();
                return FValues.Length;
            }
        }

        // This is a dumb feature of structs that you can not override the parameterless constructor!
        //  Default is not to create an array for aggregates with paramterless constructor
        internal void TestValues()
        {
            if (FValues == null)
            {
                FValues = new int[ProviderClass.NumberOfProviders];
                FIncludesAggregates = false;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the values. </summary>
        /// <remarks> This only gets an array ProviderClass.NumberOfProviders in length.  
        ///           If set passes an array of only ProviderClass.NumberOfProviders in length, resets 
        ///           IncludeAggregates to false;   Use ValuesAll to get aggregate values if there</remarks>
        /// <seealso cref="IncludesAggregates"/>
        /// <value> The values. </value>
        ///------------------------------------------------------------------------------------------------
        public int[] Values
        {
            get
            {
                TestValues();
                // OK, this get complicate  To Avoid crashing the model, this only returns provider values if aggregates is true
                if (FIncludesAggregates)
                    return FValues;
                else
                {
                    int[] shortValues = new int[ProviderClass.NumberOfProviders];
                    for (int i = 0; i < shortValues.Length; i++)
                        shortValues[i] = FValues[i];
                    return shortValues;
                }
            }
            set
            {
                FValues = value;
                // ok, passing a value with only providers changes this to a provider only if it did IncludeAggregates before;
                if (value.Length == ProviderClass.NumberOfProviders)
                    FIncludesAggregates = false;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Indexer to get or set items within this array using index syntax based on an
        ///             eProvider vakue. </summary>
        ///
        /// <param name="ep">   The ep. </param>
        ///
        /// <value> The indexed item. </value>
        ///-------------------------------------------------------------------------------------------------

        public int this[eProvider ep]
        {
            get
            {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return FValues[index];
            }
            set {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception (WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                FValues[index] = value;
                }
        }

        /// <summary>
        ///     Indexer to get or set items within this array using an int index (overloaded)
        /// </summary>
        ///
        /// <value> The indexed item. </value>
        public int this[int index]
        {
            get
            {
                TestValues();
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return FValues[index];
            }
            set {
                TestValues();
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception (WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                FValues[index] = value;
                }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the array includes aggregates. </summary>
        ///
        /// <value> true if includes aggregates, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IncludesAggregates
        { get { return FIncludesAggregates; } }

        /// <summary>   Constructor. </summary>
        /// <param name="Default">  The default int value to set each cell at instantiation. </param>
        public ProviderIntArray(int Default)
        {
            FIncludesAggregates = false;
            FValues = new int[ProviderClass.NumberOfProviders];
            if (Default != 0) for (int i = 0; i < ProviderClass.NumberOfProviders; i++) FValues[i] = Default;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <remarks> Used to create a ProviderIntArray that include Aggregate results</remarks>
        /// <param name="Default">      The default int value to set each cell at instantiation. </param>
        /// <param name="IncludeAll">   true to include, false to exclude all. </param>
        ///-------------------------------------------------------------------------------------------------

        public ProviderIntArray(int Default, bool IncludeAll)
        {
            if (IncludeAll)
            {
                FIncludesAggregates = true;
                int allep = ProviderClass.NumberOfProviders + ProviderClass.NumberOfAggregates;
                FValues = new int[allep];
            }
            else
            {
                FIncludesAggregates = false;
                FValues = new int[ProviderClass.NumberOfProviders];

            }
            if (Default != 0) for (int i = 0; i < FValues.Length; i++) FValues[i] = Default;
        }
  
    }
    //-------------------------------------------------
    /// <summary>
    /// An array structure for provider double data.  Each cell corresponds to a different provider.  Used to set and retrieve Derived ModelParameter data.  See ProviderClass for indexes to array 
    /// <see cref="ProviderClass"/> 
    /// </summary>
    public struct ProviderDoubleArray
    {
        internal bool FIncludesAggregates;
        /// <summary> The Double values array</summary>
        public double[] FValues;
        /// <summary>   The length of the Values array </summary>
        /// <value> The length zero based. </value>
        public int Length
        { get 
            {
                TestValues();
                return FValues.Length; 
            }
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets or sets the values. </summary>
        ///
        /// <value> The values. </value>
        ///------------------------------------------------------------------------------------------------
        public double[] Values
        {
            get
            {
                TestValues();
                return FValues;
            }
            set
            {
                FValues = value;
            }
        }
        // This is a dumb feature of structs that you can not override the parameterless constructor!
        internal void TestValues()
        {
            if (FValues == null)
                FValues = new double[ProviderClass.NumberOfProviders];
        }

        /// <summary>
        ///     Indexer to get or set items within this array using index syntax based on an eProvider vakue.
        /// </summary>
        ///
        /// <value> The indexed item. </value>

        public double this[eProvider ep]
        { 
            get
            {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return FValues[index];
            }
            set {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception (WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                FValues[index] = value;
                }
        }
        /// <summary>
        ///     Indexer to get or set items within this array using an int index (overloaded)
        /// </summary>
        ///
        /// <value> The indexed item. </value>
        public double this[int index]
        {
            get
            {
                TestValues();
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return FValues[index];
            }
            set
            {
                TestValues();
                if ((index < 0) || (index >= FValues.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                FValues[index] = value;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the array includes aggregates. </summary>
        ///
        /// <value> true if includes aggregates, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IncludesAggregates
        { get { return FIncludesAggregates; } }

        /// <summary>   Constructor. </summary>
        /// <param name="Default">  The default int value to set each cell at instantiation. </param>
        public ProviderDoubleArray(double Default)
        {
            FIncludesAggregates = true;
            FValues = new double[ProviderClass.NumberOfProviders];
            if (Default != 0.0) for (int i = 0; i < ProviderClass.NumberOfProviders; i++) FValues[i] = Default;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="Default">      The default int value to set each cell at instantiation. </param>
        /// <param name="IncludeAll">   true to include, false to exclude all. </param>
        ///-------------------------------------------------------------------------------------------------

        public ProviderDoubleArray(double Default, bool IncludeAll)
        {
            if (IncludeAll)
            {
                FIncludesAggregates = true;
                int allep = ProviderClass.NumberOfProviders + ProviderClass.NumberOfAggregates;
                FValues = new double[allep];
            }
            else
            {
                FIncludesAggregates = true;
                FValues = new double[ProviderClass.NumberOfProviders];
            }
            if (Default != 0.0) for (int i = 0; i < FValues.Length; i++) FValues[i] = Default;
        }

    }
    //--------------------------------------------------------------------------------- 
   /// <summary>
   /// An array structure for provider strings.  Each cell corresponds to a different provider.  Used primarily for labeling
   /// </summary>
    public struct ProviderStringArray
    {

        bool FIncludesAggregates;        
        /// <summary> The string values </summary>
        public string[] Values;

        /// <summary>   Constructor. </summary>
        /// <param name="Default">  The default string value to set each string to on instantiation.. </param>

        public ProviderStringArray(string Default)
        {
            FIncludesAggregates = false;
            Values = new string[ProviderClass.NumberOfProviders];
            if (Default != "") for (int i = 0; i < ProviderClass.NumberOfProviders; i++) Values[i] = Default;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="Default">    The default string value to set each string to on instantiation.. </param>
        /// <param name="IncludeAll">   true to include, false to exclude all. </param>
        ///-------------------------------------------------------------------------------------------------

        public ProviderStringArray(string Default, bool IncludeAll)
        {
            if (IncludeAll)
            {
                FIncludesAggregates = true;
                int allep = ProviderClass.NumberOfProviders + ProviderClass.NumberOfAggregates;
                Values = new string[allep];
            }
            else
            {
                FIncludesAggregates = false;
                Values = new string[ProviderClass.NumberOfProviders];
            }
            if (Default != "") for (int i = 0; i < Values.Length; i++) Values[i] = Default;
        }

        /// <summary>   The length of the Values array </summary>
        /// <value> The length zero based. </value>

        public int Length
        { get { return Values.Length; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the array includes aggregates. </summary>
        ///
        /// <value> true if includes aggregates, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IncludesAggregates
        { get { return FIncludesAggregates; } }

        // This is a dumb feature of structs that you can not override the parameterless constructor!
        internal void TestValues()
        {
            if (Values == null)
                Values = new string[ProviderClass.NumberOfProviders];
        }

        /// <summary>
        ///     Indexer to get or set items within this array using index syntax based on an eProvider vakue.
        /// </summary>
        ///
        /// <value> The indexed item. </value>

        public string this[eProvider ep]
        {
            get
            {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= Values.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return Values[index];
            }
            set
            {
                TestValues();
                int index = (int)ep;
                if ((index < 0) || (index >= Values.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                Values[index] = value;
            }
        }
        /// <summary>
        ///     Indexer to get or set items within this array using an int index (overloaded)
        /// </summary>
        ///
        /// <value> The indexed item. </value>
        public string this[int index]
        {
            get
            {
                TestValues();
                if ((index < 0) || (index >= Values.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                return Values[index];
            }
            set
            {
                TestValues();
                if ((index < 0) || (index >= Values.Length))
                { throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wseProviderOutOfRange)); }
                Values[index] = value;
            }
        }

    }
    //----------------------------------------------------------------
   /// <summary>
   /// An array structure for ModelParamter integer data for Base parameters.
    /// <remarks>Each cell corresponds to a different ModelParmeter value. See ParameterManager for indexes to array.</remarks>  
   /// </summary>
    public struct ModelParameterBaseArray
    {
        /// <summary> The values </summary>
        public int[] Values;

        /// <summary>   The length of the Values array </summary>
        /// <value> The length zero based. </value>

        public int Length
        { get { return Values.Length; } }

        /// <summary>
        ///     Indexer to get or set items within this array using an int index (overloaded)
        /// </summary>
        ///
        /// <value> The indexed item. </value>

        public int this[int index]
        {
            get 
            {
               return Values[index];
            }
            set { Values[index] = value; }
        }

        /// <summary>   Constructor. </summary>
        /// <param name="size"> The size of the BaseArray. </param>

        public ModelParameterBaseArray(int size)
        {
            Values = new int[size];
        }
    }
    //----------------------------------------------------------------
   /// <summary>
    /// An Array structure for ModelParameters for Provider Data.  
    /// <remarks>Each cell corresponds to a different ModelParmeter value. See ParameterManager for indexes to array.</remarks> 
   /// </summary>
    public struct ModelParameterProviderArray
    {

        /// <summary> The values </summary>
        
        public ProviderIntArray[] Values;

        /// <summary>   Constructor. </summary>
        /// <param name="size"> The size of the array</param>

        public ModelParameterProviderArray(int size)
        {
            Values = new ProviderIntArray[size];
            for (int i = 0; i < size; i++) Values[i] = new ProviderIntArray(0);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="size">                 The size of the array. </param>
        /// <param name="IncludeAggregates">    true to include, false to exclude the aggregates. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelParameterProviderArray(int size, bool IncludeAggregates)
        {
            Values = new ProviderIntArray[size];
            for (int i = 0; i < size; i++) Values[i] = new ProviderIntArray(0,IncludeAggregates);
        }

        /// <summary>   The length of the Values array </summary>
        /// <value> The length zero based. </value>

        public int Length
        { get { return Values.Length; } }


        /// <summary>
        ///     Indexer to get or set items within this array using an int index (overloaded)
        /// </summary>
        ///
        /// <value> The indexed item. </value>
        public ProviderIntArray this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }
    }
    //--------------------------------------------------------------------------

  /// <summary>
  /// Struct to hold base and Provider Inputs
  /// </summary>
  /// <remarks>The ParameterManagerClass provides a method to create this struct without passing sizes to the constructor.
  ///          This is a dynamic class and the contents and structure are not static.  Even when constructed with the ParamManager method
  ///          the contents can be dynamic over time.  For example, if a SimulationInputs object is created, and then an input parameter is added (or deleted)
  ///          using the ParamManger, the next object created with NewSimulationInputs will be different.</remarks>
  /// <seealso cref="ParameterManagerClass.NewSimulationInputs"/>
  /// <example><code>
  ///          WaterSimManager WSim = new WaterSimManager("Temp","Data");
  /// 		   SimulationInputs MySI = WSim.ParamManager.NewSimulationInputs();
  ///		   
  /// </code></example>
    public struct SimulationInputs
  {

      /// <summary> Name of the scenario for these inputs</summary>
      public string ScenarioName;

      /// <summary> The version of API and Model used to create these Inputs</summary>
      public string Version;

      /// <summary> Date/Time of when simulation inputs were set</summary>
      public DateTime When; 
        
      /// <summary>
      /// Base Input Values
      /// </summary>
      public ModelParameterBaseArray BaseInput;
      /// <summary>
      /// the eModelParam for each parameter in BaseInput
      /// </summary>
      public int[] BaseInputModelParam;

      /// <summary>
      /// Provider Input Values
      /// </summary>
      public ModelParameterProviderArray ProviderInput;

      /// <summary>
      /// the eModelParam for each parameter in ProviderInput
      /// </summary>
      public int[] ProviderInputModelParam;

      ///-------------------------------------------------------------------------------------------------
      /// <summary> Index For eModelParam in BaseInputs. </summary>
      /// <param name="emp"> The eModelParam to find an index  </param>
      /// <returns> index into Baseinputs if found otherwise -1 if not</returns>
      ///-------------------------------------------------------------------------------------------------
      public int BaseIndex(int emp)
      {
          int index = -1;
          int cnt = 0;
          if (ModelParamClass.valid(emp))
          {
              foreach (int _emp in BaseInputModelParam)
              {
                  if (_emp == emp)
                  {
                      index = cnt;
                      break;
                  }
                  else
                      cnt++;
              }
          }
          return index;
      }

      ///-------------------------------------------------------------------------------------------------
      /// <summary> Index For eModelParam in ProviderInputs. </summary>
      /// <param name="emp"> The eModelParam to find an index  </param>
      /// <returns> index into ProviderInputs if found otherwise -1 if not</returns>
      ///-------------------------------------------------------------------------------------------------
      public int ProviderIndex(int emp)
      {
          int index = -1;
          int cnt = 0;
          if (ModelParamClass.valid(emp))
          {
              foreach (int _emp in ProviderInputModelParam)
              {
                  if (_emp == emp)
                  {
                      index = cnt;
                      break;
                  }
                  else
                      cnt++;
              }
          }
          return index;
      }
        
        /// <summary>   Constructor. </summary>
      /// <param name="BaseInputSize">        Size of the base input array. </param>
      /// <param name="ProviderInputSize">    Size of the provider input array. </param>
      
      public SimulationInputs(int BaseInputSize, int ProviderInputSize)
      {
          When = DateTime.Now;
          Version = "";
          ScenarioName = "";
          BaseInput = new ModelParameterBaseArray(BaseInputSize);
          BaseInputModelParam = new int[BaseInputSize];
          ProviderInput = new ModelParameterProviderArray(ProviderInputSize);
          ProviderInputModelParam = new int[ProviderInputSize];
      }
  }
    //----------------------------------------------------------------------
    /// <summary>
    /// Struct to hold base and Provider Outputs
    /// <remarks>The ParameterManagerClass provides a method to create this struct without passing sizes to the constructor</remarks>
    /// <seealso cref="ParameterManagerClass.NewSimulationOutputs"/>
    /// <example><code>
    ///          WaterSimManager WSim = new WaterSimManager("Temp","Data");
    /// 		   SimulationOutputs MySO = WSim.ParamManager.NewSimulationOutputs();
    ///		   
    /// </code></example>
    /// </summary>
    public struct SimulationOutputs
    {
        internal bool FIncludeAggregates;
        /// <summary> Name of the scenario for these outputs</summary>
        public string ScenarioName;
        /// <summary> The version of API and Model used to create these outputs</summary>
        public string Version;
        /// <summary> Date/Time of when simulation outputs were set</summary>
        public DateTime When;
        /// <summary>
        /// Base Output Values
        /// </summary>
        public ModelParameterBaseArray BaseOutput;
        /// <summary>
        /// Provider Output Values
        /// </summary>
        public ModelParameterProviderArray ProviderOutput;

        /// <summary>
        /// the eModelParam for each parameter in BaseInput
        /// </summary>
        public int[] BaseOutputModelParam;

        /// <summary>
        /// the eModelParam for each parameter in ProviderInput
        /// </summary>
        public int[] ProviderOutputModelParam;

        public bool AggregatesIncluded
        { get { return FIncludeAggregates; } }
       
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Index For eModelParam in BaseOutputs. </summary>
        /// <param name="emp"> The eModelParam to find an index  </param>
        /// <returns> index into BaseOutputs if found otherwise -1 if not</returns>
        ///-------------------------------------------------------------------------------------------------
        public int BaseIndex(int emp)
        {
            int index = -1;
            int cnt = 0;
            if (ModelParamClass.valid(emp))
            {
                foreach (int _emp in BaseOutputModelParam)
                {
                    if (_emp == emp)
                    {
                        index = cnt;
                        break;
                    }
                    else
                        cnt++;
                }
            }
            return index;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Index For eModelParam in ProviderOutputs. </summary>
        /// <param name="emp"> The eModelParam to find an index  </param>
        /// <returns> index into ProviderOutputs if found otherwise -1 if not</returns>
        ///-------------------------------------------------------------------------------------------------
        public int ProviderIndex(int emp)
        {
            int index = -1;
            int cnt = 0;
            if (ModelParamClass.valid(emp))
            {
                foreach (int _emp in ProviderOutputModelParam)
                {
                    if (_emp == emp)
                    {
                        index = cnt;
                        break;
                    }
                    else
                        cnt++;
                }
            }
            return index;
        }


        /// <summary>   Constructor. </summary>
        /// <param name="BaseOutputSize">        Size of the base output array. </param>
        /// <param name="ProviderOutputSize">    Size of the provider output array. </param>

        public SimulationOutputs(int BaseOutputSize, int ProviderOutputSize)
        {
            FIncludeAggregates = false;
            When = DateTime.Now;
            Version = "";
            ScenarioName = "";
            BaseOutput = new ModelParameterBaseArray(BaseOutputSize);
            BaseOutputModelParam = new int[BaseOutputSize];
            ProviderOutput = new ModelParameterProviderArray(ProviderOutputSize);
            ProviderOutputModelParam = new int[ProviderOutputSize];
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>

        /// <param name="BaseOutputSize">       Size of the base output array. </param>
        /// <param name="ProviderOutputSize">   Size of the provider output array. </param>
        /// <param name="IncludeAggregates">    true to include, false to exclude the aggregates. </param>
        ///-------------------------------------------------------------------------------------------------

        public SimulationOutputs(int BaseOutputSize, int ProviderOutputSize, bool IncludeAggregates)
        {
            FIncludeAggregates = IncludeAggregates;
            When = DateTime.Now;
            Version = "";
            ScenarioName = "";
            BaseOutput = new ModelParameterBaseArray(BaseOutputSize);
            BaseOutputModelParam = new int[BaseOutputSize];
            ProviderOutput = new ModelParameterProviderArray(ProviderOutputSize,IncludeAggregates);
            ProviderOutputModelParam = new int[ProviderOutputSize];
        }

    }
    //----------------------------------------------------------------------
    
    /// <summary>
    /// A structure for one years worth of Simulation Results
    /// </summary>
    public struct AnnualSimulationResults
    {
        /// <summary>
        ///  Year of Simulation
        /// </summary>
        public int year;
        /// <summary> The outputs. </summary>
        public SimulationOutputs Outputs;
        /// <summary> The inputs. </summary>
        public SimulationInputs Inputs;

        /// <summary>   Constructor. </summary>
        /// <param name="BaseOutputSize">       Size of the base output array. </param>
        /// <param name="ProviderOutputSize">   Size of the provider output array. </param>
        /// <param name="BaseInputSize">        Size of the base input array. </param>
        /// <param name="ProviderInputSize">    Size of the provider input array. </param>

        public AnnualSimulationResults(int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize)
        {
            year = 0;
            Outputs = new SimulationOutputs(BaseOutputSize, ProviderOutputSize);
            Inputs = new SimulationInputs(BaseInputSize, ProviderInputSize);

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="BaseOutputSize">       Size of the base output array. </param>
        /// <param name="ProviderOutputSize">   Size of the provider output array. </param>
        /// <param name="BaseInputSize">        Size of the base input array. </param>
        /// <param name="ProviderInputSize">    Size of the provider input array. </param>
        /// <param name="IncludeAggregates">    true to include, false to exclude the aggregates. </param>
        ///-------------------------------------------------------------------------------------------------

        public AnnualSimulationResults(int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize, bool IncludeAggregates)
        {
            year = 0;
            Outputs = new SimulationOutputs(BaseOutputSize, ProviderOutputSize, IncludeAggregates);
            Inputs = new SimulationInputs(BaseInputSize, ProviderInputSize);

        }
    }

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Simulation results. </summary>
    ///-------------------------------------------------------------------------------------------------

    public class SimulationResults
    {
        AnnualSimulationResults[] FSimResults;
        int FStartYear = 0;
        int FStopYear = 0;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="years">                The years. </param>
        /// <param name="StartYear">            The start year. </param>
        /// <param name="BaseOutputSize">       Size of the base output. </param>
        /// <param name="ProviderOutputSize">   Size of the provider output. </param>
        /// <param name="BaseInputSize">        Size of the base input. </param>
        /// <param name="ProviderInputSize">    Size of the provider input. </param>
        ///-------------------------------------------------------------------------------------------------

        public SimulationResults(int years, int StartYear, int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize)
        {
            SetUpFields(years, StartYear, BaseOutputSize, ProviderOutputSize, BaseInputSize, ProviderInputSize, false);
        }

        public SimulationResults(int years, int StartYear, int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize, bool IncludeAggregates)
        {
            SetUpFields(years, StartYear, BaseOutputSize, ProviderOutputSize, BaseInputSize, ProviderInputSize, IncludeAggregates);
        }

        internal void SetUpFields(int years, int StartYear, int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize, bool IncludeAggregates)
        {

            ProviderIntArray Outblank = new ProviderIntArray(0, IncludeAggregates);
            ProviderIntArray Inblank = new ProviderIntArray(0, false);

            FSimResults = new AnnualSimulationResults[years];
            for (int yr = 0; yr < years; yr++)
            {
                AnnualSimulationResults TheASR = new AnnualSimulationResults(BaseOutputSize, ProviderOutputSize, BaseInputSize, ProviderInputSize, IncludeAggregates );

                int cnt = BaseOutputSize;
                for (int i = 0; i < cnt; i++)
                {
                    TheASR.Outputs.BaseOutput[i] = 0;
                    TheASR.Outputs.BaseOutputModelParam[i] = -1;
                }
                cnt = ProviderOutputSize;
                for (int parmi = 0; parmi < cnt; parmi++)
                {
                    TheASR.Outputs.ProviderOutput[parmi] = Outblank;
                    TheASR.Outputs.ProviderOutputModelParam[parmi] = -1;
                }
                cnt = BaseInputSize;
                for (int parmi = 0; parmi < cnt; parmi++)
                {
                    TheASR.Inputs.BaseInput[parmi] = 0;
                    TheASR.Inputs.BaseInputModelParam[parmi] = -1;
                }
                cnt = ProviderInputSize;
                for (int parmi = 0; parmi < cnt; parmi++)
                {
                    TheASR.Inputs.ProviderInput[parmi] = Inblank;
                    TheASR.Inputs.ProviderInputModelParam[parmi] = -1;
                }
                FSimResults[yr] = TheASR;
            }
            FStartYear = StartYear;
            FStopYear = (StartYear + years) - 1;
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
        ///
        /// <param name="index">    Zero-based index of the entry to access. </param>
        ///
        /// <returns>   The indexed item. </returns>
        ///-------------------------------------------------------------------------------------------------

        public AnnualSimulationResults this[int index]
        {
            get { return FSimResults[index]; }
            set { FSimResults[index] = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the last year. </summary>
        ///
        /// <value> The last year. </value>
        ///-------------------------------------------------------------------------------------------------

        public int LastYear
        {
            get { return FStopYear; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the start year. </summary>
        ///
        /// <value> The start year. </value>
        ///-------------------------------------------------------------------------------------------------

        public int StartYear
        { 
            get { return FStartYear; }
            set
            { 
                FStartYear = value;
                FStopYear = (FStartYear + Length) - 1;
            }
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the number of years in the array of AnnualResults</summary>
        /// <value> The length. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Length
        {
            get { return FSimResults.Length; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets AnnualResults By year. </summary>
        ///
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        ///
        /// <param name="year"> The year. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public AnnualSimulationResults ByYear(int year)
        {
            if ((year >= FStartYear) & (year <= FStopYear))
            {
                int index = year - FStartYear;
                return FSimResults[index];
            }
            else
            {
                throw new Exception("Year [" + year.ToString() + "] is out of range : " + FStartYear.ToString() + " to " + FStopYear.ToString());
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the array of AnnualResults. </summary>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public AnnualSimulationResults[] GetAllYears()
        {
            return FSimResults;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the enumerator. </summary>
        /// <returns>   The enumerator. </returns>
        ///-------------------------------------------------------------------------------------------------

        public IEnumerator<AnnualSimulationResults> GetEnumerator()
        {
            foreach (AnnualSimulationResults ARS in FSimResults)
            {
                yield return ARS;
            }
        }
    }
   //----------------------------------------------------------------------------

    /// <summary>   Simulation strings for Labels.  </summary>
    public struct SimulationStrings
    {

        /// <summary> The base output </summary>
        public string[] BaseOutput;

        /// <summary> The base input </summary>
        public string[] BaseInput;

        /// <summary> The provider output </summary>
        public string[] ProviderOutput;

        /// <summary> The provider input </summary>
        public string[] ProviderInput;

        /// <summary>   Constructor. </summary>
        /// <param name="BaseOutputSize">       Size of the base output. </param>
        /// <param name="ProviderOutputSize">   Size of the provider output. </param>
        /// <param name="BaseInputSize">        Size of the base input. </param>
        /// <param name="ProviderInputSize">    Size of the provider input. </param>

        public SimulationStrings(int BaseOutputSize, int ProviderOutputSize, int BaseInputSize, int ProviderInputSize)
        {
            BaseOutput = new string[BaseOutputSize];
            BaseInput = new string[BaseInputSize];
            ProviderOutput = new string[ProviderOutputSize];
            ProviderInput = new string[ProviderInputSize];
        }
    }
        //----------------------------------------------------------------------------
    #endregion

        #region Utils

        internal static class util
    {
        #region apiconstants
        //====================================================================
        // CONSTANTS  ENUMS STRUCTS
        // THESE CAN NOT BE CHANGED WITH OUT CHANGING DATA INPUTS!
        // There is no error checking for this
        internal const int ModelParamterUseDefault = -1;

        internal const int WaterSimDCDC_Default_Simulation_StartYear = 2000; // changed RQ 4 15 2012  OLD - 2006;
        internal const int WaterSimDCDC_Default_Simulation_EndYear = 2085;
        //internal const int WaterSimDCDC_Default_Colorado_Historical_Extraction_Start_Year = 1978;
        //internal const int WaterSimDCDC_Default_SaltVerde_Historical_Extraction_Start_Year = 1978;

        internal const int WaterSimDCDC_Default_ProviderDemandOption = 0;
        internal const int WaterSimDCDC_Default_SaltVerde_Historical_Data = 1; // changed RQ 4 15 2012  OLD - 2;
        //internal const int WaterSimDCDC_Default_Colorado_Historical_Data = 1; 
        // 05.11.12
        internal const int WaterSimDCDC_Default_SaltVerde_Trace_length = 30;
        internal const int WaterSimDCDC_Default_Colorado_Trace_length = 30;
 
        // Fixes
        internal const int WaterSimDCDC_Default_Colorado_User_Adjustment_StartYear = 2006;
        //internal const int WaterSimDCDC_Default_SaltVerde_Historical_Data = 1;
        internal const int WaterSimDCDC_Default_SaltVerde_User_Adjustment_Start_Year = 2006;
        internal const int WaterSimDCDC_Provider_Demand_Option = 3;
        internal const int DefaultReduceGPCDValue = 10; // changed RQ 4 15 2012 OLD - 1;
        // Fix for provider inputs
        internal const int WaterSimDCDC_Default_Use_GPCD = 150;
        internal const int WaterSimDCDC_Default_Surface_to_Vadose_Time_Lag_in_Years = 25;
        internal const int WaterSimDCDC_Default_SurfaceWater_to_WaterBank = 0;

        //=================================================================================
        internal const int WaterSimDCDC_Default_Colorado_Historical_Data_Source = (int)ColoradoRiverRecord.eBureauRecord;

        //public const int    ProviderClass.NumberOfProviders  = 33;

        public const int UseDefaultInput = 0;
        #endregion

        #region  utilroutines
        /************************************************
         * ROUTINES
         * 
         * 
         * *********************************************/
        //----------------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Fillzeroes. </summary>
        ///
        /// <remarks> Fills an int array with zeros</remarks>
        ///
        /// <param name="values"> The int array to fill </param>
        ///-------------------------------------------------------------------------------------------------

        public static void fillzero(int[] values)
        {
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            { values[i] = 0; }
        }
        //----------------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Fillvalues. </summary>
        ///
        /// <remarks> Fills and int array wit the specified value </remarks>
        ///
        /// <param name="values"> The int array to fill. </param>
        /// <param name="value">  The value to use for fill. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void fillvalues(int[] values, int value)
        {
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            { values[i] = value; }
        }

        //----------------------------------------------------------------


        const double GALLONSINACREFOOT = 325851.429;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Fill default. </summary>
        /// <remarks> Fills an int array with the default int value = UseDefaultInput </remarks>
        /// <param name="values"> The int array to fill. </param>
        ///-------------------------------------------------------------------------------------------------

        public static void fillDefault(int[] values)
        {
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            { values[i] = UseDefaultInput; }
        }
        #endregion

        public static double ConvertAFtoGallons(int Acrefeet)
        {
            return Convert.ToDouble(Acrefeet) * GALLONSINACREFOOT;
        }

        public static int ConvertGallonstoAF(double Gallons)
        {
            return Convert.ToInt32(Gallons / GALLONSINACREFOOT);
        }
        public static double CalcGPCD(double Gallons, int Pop)
        {
            return ((Gallons / Convert.ToDouble(Pop)) / 365.0);
        }
    }
        #endregion
        /****************************************************************
     * Parameter Classes, Routines, Contants and enums
     * ************************************************************/

    //====================================================================================
    #region Report
    //----------------------------------------------------

    /// <summary>   Report header.  </summary>
    
    public class ReportHeader
    {

        /// <summary> The lines of the header, 1, 2 or 3</summary>
        public string[] Lines;

        /// <summary>   Default constructor. </summary>
  
        public ReportHeader()
        {
            Lines = new string[3];
        }
        // indexer

        /// <summary>
        ///     Indexer to get or set items within this collection using array index syntax.
        /// </summary>
        ///
        /// <value> The indexed item. </value>

        public string this[int index]
        {
            get { return Lines[index]; }
            set { Lines[index] = value; }
        }

        /// <summary>   Gets the length of the lines array 1, 2 or 3. </summary>
        ///
        /// <value> The length. </value>

        public int Length
        { get { return Lines.Length; } }
    }
    //---------------------------------------------------------

    /// <summary>   Report class.  </summary>
    /// <remarks> static class with methods for creating and processing reports and report headers</remarks>
    public static class ReportClass
    {
        const int top = 0;
        const int bottom = 1;
        //---------------------------------------------------------
        static string[] ParseLabel(string value, int size)
        {
            string[] templines = new string[2];
            if (value.Length > size)
            {
                int Len = value.Length;
                int start = (Len - size);
                int pos = 0;
                int i = start;
                do
                {
                    if (value[i] == ' ')
                        pos = i;
                    i++;
                } while ((i < Len) & (pos == 0));
                if (pos == 0)
                {
                    templines[top] = value.Substring(0, size);
                    templines[bottom] = value.Substring(size, start);
                }
                else
                {
                    templines[top] = value.Substring(0, pos);
                    pos++;
                    templines[bottom] = value.Substring(pos, (Len - pos));
                }
            }
            else
            {
                templines[bottom] = value;
                templines[top] = "";
            }

            return templines;
        }

        /// <summary> The parameter input label used for the report header, labels each paramter as input</summary>
        public static string ParamInputLabel = "Input";

        /// <summary> The parameter output label used for the report header, labels each paramter as output</summary>
        public static string ParamOutputLabel = "Output";
        //---------------------------------------------------------

        /// <summary>   Parameter header. </summary>
        ///
        /// <remarks>   Ray, 8/5/2011. </remarks>
        ///
        /// <param name="_header">      [in,out] The header. </param>
        /// <param name="ClearHeader">  true to clear header otherwise adds to what ever is passed with _header. </param>
        /// <param name="columnSize">   width of each column. </param>
        /// <param name="UseTabs">      true to use tabs otherwise pads with spaces. </param>
        /// <param name="Use2ndLine">   true to use 2nd line for label wrapping into column size, otherwise truncates label to fit column size. </param>
        /// <param name="Parms">        The parms enumerator to use. </param>

        public static void ParamHeader(ref ReportHeader _header, bool ClearHeader, int columnSize, bool UseTabs, bool Use2ndLine, IEnumerable<ModelParameterBaseClass> Parms)
        {
            string[] tempLines = new string[_header.Length];
            string[] parsedLines = new string[2];
            string temp = "";
            int LineNum = _header.Length;
            if (ClearHeader)   // set header to ""
                for (int i = 0; i < _header.Length; i++)
                    _header[i] = "";
            // get the input Base values
            foreach (ModelParameterClass Mp in Parms) //WSim.ParamManager.BaseInputs())
            {
                temp = Mp.Label;
                if (Mp.isInputParam) tempLines[2] = ParamInputLabel;
                else tempLines[2] = ParamOutputLabel; ;


                if ((columnSize > 0) & (temp.Length > (columnSize - 1)))
                {
                    if (Use2ndLine)
                    {
                        parsedLines = ParseLabel(temp, columnSize - 1);
                        tempLines[0] = parsedLines[0];
                        tempLines[1] = parsedLines[1];

                    }
                    else
                    {
                        tempLines[bottom] = temp.Substring(0, columnSize - 1);
                    }
                }
                else
                {
                    if (Use2ndLine)
                    {
                        tempLines[bottom] = temp;
                        tempLines[top] = "";
                    }
                    else
                    {
                        tempLines[0] = temp;
                        tempLines[1] = "";
                    }
                }

                if (!UseTabs)
                {
                    if (columnSize > 0)
                    {
                        for (int i = 0; i < LineNum; i++)
                        {
                            if (tempLines[i].Length > columnSize)
                                tempLines[i] = tempLines[i].Substring(0, columnSize - 1);
                            _header.Lines[i] += tempLines[i].PadLeft(columnSize, ' ');
                        }
                    }
                    else
                    {
                        for (int i = 0; i < LineNum; i++)
                            _header.Lines[i] += " " + tempLines[i];
                    }
                }
                else
                {
                    for (int i = 0; i < LineNum; i++)
                        _header.Lines[i] += " \t" + tempLines[i];
                }

            }
        }
        ////---------------------------------------------------------
        
        //-------------------------------------------------------------------------------------

        /// <summary>   Full header. </summary>
        ///
        /// <remarks>   Creates a full reportheader, all parameters plus year, scenario name, and provider label and proivider fieldname </remarks>
        ///
        /// <param name="WSim">         The WaterSimManager class object being used for Simulation. </param>
        /// <param name="columnSize">   width of each column. </param>
        /// <param name="UseTabs">      true to use tabs otherwise pads with spaces. </param>
        /// <param name="Use2ndLine">   true to use 2nd line for label wrapping into column size,
        ///                             otherwise truncates label to fit column size. </param>
        ///
        /// <returns> a ReportHeader <see cref="ReportHeader"/> </returns>

        public static ReportHeader FullHeader(WaterSimManager WSim, int columnSize, bool UseTabs, bool Use2ndLine)
        {
            ReportHeader temp = new ReportHeader();
            ReportHeader header = new ReportHeader();

            string pTemp = "Provider";
            string yTemp = "Year";
            string sTemp = "Scenario";
            string blank = "";

            if (columnSize != 0)
            {
                if (pTemp.Length > columnSize) pTemp = pTemp.Substring(0, columnSize - 1);
                if (yTemp.Length > columnSize) yTemp = yTemp.Substring(0, columnSize - 1);
                if (sTemp.Length > columnSize) sTemp = sTemp.Substring(0, columnSize - 1);
            }
            if (UseTabs)
            {
                pTemp = " \t" + pTemp;
                yTemp = " \t" + yTemp;
                sTemp = " \t" + sTemp;
                blank = " \t" + blank; ;
            }
            else
            {
                if (columnSize == 0)
                {
                    pTemp = " " + pTemp;
                    yTemp = " " + yTemp;
                    sTemp = " " + sTemp;
                    blank = " " + blank;
                }
                else
                {
                    pTemp = pTemp.PadLeft(columnSize, ' ');
                    yTemp = yTemp.PadLeft(columnSize, ' ');
                    sTemp = sTemp.PadLeft(columnSize, ' ');
                    blank = blank.PadLeft(columnSize, ' ');
                }

            }
            if (Use2ndLine)
            {

                header[1] = sTemp + yTemp + pTemp;
                header[0] = blank + blank + blank;
            }
            else
                header[0] = sTemp + yTemp + pTemp;


            header[2] = blank + blank + blank;
            ParamHeader(ref header, false, columnSize, UseTabs, Use2ndLine, WSim.ParamManager.BaseOutputs());
            //header[0] += temp[0];
            //header[1] += temp[1];
            ParamHeader(ref header, false, columnSize, UseTabs, Use2ndLine, WSim.ParamManager.ProviderOutputs());
            //header[0] += temp[0];
            //header[1] += temp[1];
            ParamHeader(ref header, false, columnSize, UseTabs, Use2ndLine, WSim.ParamManager.BaseInputs());
            //header[0] += temp[0];
            //header[1] += temp[1];
            ParamHeader(ref header, false, columnSize, UseTabs, Use2ndLine, WSim.ParamManager.ProviderInputs());
            //header[0] += temp[0];
            //header[1] += temp[1];

            return header;
        }

        //--------------------------------------------------------------------------------------

        /// <summary>   Annual full data. </summary>
        ///
        /// <remarks>   returns an array of strings, one string for each provider with the value of all parameters in order to match report header. Essentiall the report header strings plus these strings is all the input and output data for one Simulation year.  Reads the current data from the paramters for the WaterSimManager class object passed.</remarks>
        ///
        /// <param name="WSim">         The WaterSimManager class object being used for Simulation. </param>
        /// <param name="ScenarioName"> Name of the scenario. </param>
        /// <param name="year">         The year. </param>
        /// <param name="columnSize">   width of each column. </param>
        /// <param name="UseTabs">      true to use tabs otherwise pads with spaces. </param>
        ///
        /// <returns>   . </returns>

        public static string[] AnnualFullData(WaterSimManager WSim, string ScenarioName, int year, int columnSize, bool UseTabs)
        {
            ModelParameterBaseClass Mp;
            int parmvalue = 0;
            int index = 0;
            string temp = "";
            string close = "";
            string InputBaseLine = "";
            string OutputBaseLine = "";
            string OutputProviderLine = "";
            string InputProviderLine = "";
            int ProviderSize = ProviderClass.NumberOfProviders;
            ProviderStringArray AllStrings = new ProviderStringArray("");


            if (UseTabs)
            {
                close = System.Environment.NewLine;
            }

            // get the output values

            foreach (int emp in WSim.ParamManager.eModelParameters())
            {
                Mp = WSim.ParamManager.Model_Parameter(emp);

                if (Mp.ParamType == modelParamtype.mptOutputBase)
                {
                    parmvalue = (Mp as ModelParameterClass).Value;  // 7/29 (Mp as BaseModelParameterClass).Value;
                    if (!UseTabs) temp = parmvalue.ToString().PadLeft(columnSize, ' ');
                    else temp = " \t" + parmvalue.ToString();
                    OutputBaseLine += temp;
                }
            }
            // get the input values
            foreach (int emp in WSim.ParamManager.eModelParameters())
            {
                Mp = WSim.ParamManager.Model_Parameter(emp);
                if (Mp.ParamType == modelParamtype.mptInputBase)
                {
                    parmvalue = (Mp as ModelParameterClass).Value;  // 7/29 (Mp as BaseModelParameterClass).Value;
                    if (!UseTabs) temp = parmvalue.ToString().PadLeft(columnSize, ' ');
                    else temp = " \t" + parmvalue.ToString();
                    InputBaseLine += temp;
                }
            }
            // Now Get providers Data
            ProviderIntArray ProviderArray = new ProviderIntArray(0);

            //Provider Output Data
            int OutputProviderCnt = WSim.ParamManager.NumberOfParameters(modelParamtype.mptOutputProvider);
            int[,] ProviderOutputData = new int[OutputProviderCnt, ProviderSize];//ProviderClass.NumberOfProviders);

            index = 0;
            foreach (int emp in WSim.ParamManager.eModelParameters())
            {
                Mp = WSim.ParamManager.Model_Parameter(emp);
                if (Mp.ParamType == modelParamtype.mptOutputProvider)
                {
                    ProviderArray = Mp.ProviderProperty.getvalues();
                    foreach (eProvider p in ProviderClass.providers())
                        ProviderOutputData[index, ProviderClass.index(p)] = ProviderArray.Values[ProviderClass.index(p)];
                    index++;
                }
            }
            //Provider Input Data
            int InputProviderCnt = WSim.ParamManager.NumberOfParameters(modelParamtype.mptInputProvider);
            int[,] ProviderInputData = new int[InputProviderCnt, ProviderSize];//ProviderClass.NumberOfProviders);

            index = 0;
            foreach (int emp in WSim.ParamManager.eModelParameters())
            {
                Mp = WSim.ParamManager.Model_Parameter(emp);
                if (Mp.ParamType == modelParamtype.mptInputProvider)
                {
                    ProviderArray = Mp.ProviderProperty.getvalues();
                    foreach (eProvider p in ProviderClass.providers())
                        ProviderInputData[index, ProviderClass.index(p)] = ProviderArray.Values[ProviderClass.index(p)];
                    index++;
                }
            }

            foreach (eProvider p in ProviderClass.providers())
            {
                // Output Line
                OutputProviderLine = "";
                for (int i = 0; i < OutputProviderCnt; i++)
                {
                    if (UseTabs) OutputProviderLine += " \t" + ProviderOutputData[i, ProviderClass.index(p)].ToString();
                    else OutputProviderLine += ProviderOutputData[i, ProviderClass.index(p)].ToString().PadLeft(columnSize, ' ');
                }
                // Input Line
                InputProviderLine = "";
                for (int i = 0; i < InputProviderCnt; i++)
                {
                    if (UseTabs) InputProviderLine += " \t" + ProviderInputData[i, ProviderClass.index(p)].ToString();
                    else InputProviderLine += ProviderInputData[i, ProviderClass.index(p)].ToString().PadLeft(columnSize, ' ');
                }
                // Get the year and provider 
                temp = ProviderClass.Label(p);
                if (temp.Length > (columnSize - 1)) temp = temp.Substring(0, columnSize - 1);
                if (UseTabs) { temp = "\t" + ScenarioName + "\t" + year.ToString() + " \t" + temp; }
                else
                {
                    temp = ScenarioName.PadLeft(columnSize, ' ') + year.ToString().PadLeft(columnSize, ' ') + temp.PadLeft(columnSize, ' ');
                }

                AllStrings.Values[ProviderClass.index(p)] = temp + OutputBaseLine + OutputProviderLine + InputBaseLine + InputProviderLine + close;
            }
            return AllStrings.Values;
        }  // FullAnnualData

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Annual full data. </summary>
        ///
        /// <remarks>   Ray Quay, 1/28/2014. </remarks>
        ///
        /// <param name="SimResults">   The simulation results. </param>
        /// <param name="ScenarioName"> Name of the scenario. </param>
        /// <param name="year">         The year. </param>
        /// <param name="columnSize">   width of each column. </param>
        /// <param name="UseTabs">      true to use tabs otherwise pads with spaces. </param>
        ///
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public static string[] AnnualFullData(SimulationResults SimResults,string ScenarioName, int year, int columnSize, bool UseTabs)
        {
           
            int parmvalue = 0;
            string temp = "";
            string close = "";
            string InputBaseLine = "";
            string OutputBaseLine = "";
            string OutputProviderLine = "";
            string InputProviderLine = "";
            int ProviderInSize = ProviderClass.NumberOfProviders;
            int ProviderOutSize = ProviderInSize;
            bool useAggregates = false;
            AnnualSimulationResults ASR = SimResults.ByYear(year);

            if ((ASR.Outputs.ProviderOutput.Length > 0) && (ASR.Outputs.AggregatesIncluded))
            {
                useAggregates = true;
                ProviderOutSize = ASR.Outputs.ProviderOutput[0].Length;
            }
            ProviderStringArray AllStrings = new ProviderStringArray("",useAggregates);

            if (UseTabs)
            {
                close = System.Environment.NewLine;
            }

            // get the output values
            for (int i = 0; i < ASR.Outputs.BaseOutput.Length; i++)
            {
                parmvalue = ASR.Outputs.BaseOutput[i];  
                if (!UseTabs) temp = parmvalue.ToString().PadLeft(columnSize, ' ');
                else temp = " \t" + parmvalue.ToString();
                OutputBaseLine += temp;
            }

            for (int i = 0; i < ASR.Inputs.BaseInput.Length; i++)
            {
                parmvalue = ASR.Inputs.BaseInput[i];
                if (!UseTabs) temp = parmvalue.ToString().PadLeft(columnSize, ' ');
                else temp = " \t" + parmvalue.ToString();
                InputBaseLine += temp;
            }

            // Now Get providers Data
            //Provider Output Data
            int[,] ProviderOutputData = new int[ASR.Outputs.ProviderOutput.Length, ProviderOutSize];//ProviderClass.NumberOfProviders);
            for (int i = 0; i < ASR.Outputs.ProviderOutput.Length; i++)
            {
                ProviderIntArray ProviderArray = new ProviderIntArray(0,ASR.Outputs.AggregatesIncluded);
                for (int j = 0; j < ProviderArray.Length; j++)
                {
                    ProviderArray.Values[j] = ASR.Outputs.ProviderOutput[i].Values[j];
                    foreach (eProvider p in ProviderClass.providers(ASR.Outputs.AggregatesIncluded))
                    {
                            ProviderOutputData[i, ProviderClass.index(p, ASR.Outputs.AggregatesIncluded)] = ProviderArray.Values[ProviderClass.index(p, ASR.Outputs.AggregatesIncluded)];
                    }
                }
            }

            int[,] ProviderInputData = new int[ASR.Inputs.ProviderInput.Length, ProviderInSize];//ProviderClass.NumberOfProviders);
            for (int i = 0; i < ASR.Inputs.ProviderInput.Length; i++)
            {
                ProviderIntArray ProviderArray = new ProviderIntArray();
                ProviderArray.Values = ASR.Inputs.ProviderInput[i].Values;
                foreach (eProvider p in ProviderClass.providers())
                    ProviderInputData[i, ProviderClass.index(p)] = ProviderArray.Values[ProviderClass.index(p)];
            }

            //for (int pindex=0;pindex<ProviderOutSize;pindex++)
            foreach (eProvider p in ProviderClass.providers(ASR.Outputs.AggregatesIncluded))
            {
                // Output Line
                OutputProviderLine = "";
                for (int i = 0; i <  ASR.Outputs.ProviderOutput.Length; i++)
                {
                    if (UseTabs) OutputProviderLine += " \t" + ProviderOutputData[i, ProviderClass.index(p, useAggregates)].ToString();
                    else OutputProviderLine += ProviderOutputData[i, ProviderClass.index(p,useAggregates)].ToString().PadLeft(columnSize, ' ');
                }
                // Input Line
                InputProviderLine = "";
                if (p > ProviderClass.LastProvider)
                {
                    for (int i = 0; i < ASR.Inputs.ProviderInput.Length; i++)
                    {
                        if (UseTabs) InputProviderLine += " \t" + "na";
                        else InputProviderLine += "na".PadLeft(columnSize, ' ');
                    }
                }
                else
                {
                    for (int i = 0; i < ASR.Inputs.ProviderInput.Length; i++)
                    {
                        if (UseTabs) InputProviderLine += " \t" + ProviderInputData[i, ProviderClass.index(p,useAggregates)].ToString();
                        else InputProviderLine += ProviderInputData[i, ProviderClass.index(p,useAggregates)].ToString().PadLeft(columnSize, ' ');
                    }
                }
                // Get the year and provider 
                temp = ProviderClass.Label(p);
                if (temp.Length > (columnSize - 1)) temp = temp.Substring(0, columnSize - 1);
                if (UseTabs) { temp = "\t" + ScenarioName + "\t" + year.ToString() + " \t" + temp; }
                else
                {
                    temp = ScenarioName.PadLeft(columnSize, ' ') + year.ToString().PadLeft(columnSize, ' ') + temp.PadLeft(columnSize, ' ');
                }

                AllStrings.Values[ProviderClass.index(p,useAggregates)] = temp + OutputBaseLine + OutputProviderLine + InputBaseLine + InputProviderLine + close;
            }
            return AllStrings.Values;
        }  // FullAnnualData

    }  // Report Class
    #endregion
    //=================================================================
    #region WaterSimException
    /// <summary>   Exception for throwing water simulation errors.  </summary>

    public class WaterSim_Exception : Exception
    {

        /// <summary> Identifier string for this exception</summary>
        protected const string Pre = "WaterSim Error: ";

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Ray, 1/24/2013. </remarks>
        ///
        /// <param name="message"> The message. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSim_Exception(string message) : base(Pre + message) { }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        ///
        /// <remarks> Ray, 1/24/2013. </remarks>
        ///
        /// <param name="index"> Zero-based index of the. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSim_Exception(int index) : base(Pre + WS_Strings.Get(index)) { }
    }

     #endregion


}


//================================================================================================
       
