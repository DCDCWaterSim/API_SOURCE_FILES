// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       Adds derived sustainability parameters as measures of model output

//       WaterSimDCDC_API_SustainableParameters
//       Version 4.0
//       Keeper Ray Quay  ray.quay@asu.edu
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
using WaterSimDCDC;
using WaterSimDCDC.Documentation;
using UniDB;



namespace WaterSimDCDC
{
    ////=============================================
    ////   TrackProviderDeficits : AnnualFeedbackProcess
    ////
    //// =============================================
    /////-------------------------------------------------------------------------------------------------
    ///// <summary>  AnnualFeedbackProcess that tracks provider demand deficits. </summary>
    ///// <seealso cref="AnnualFeedbackProcess"/>
    /////-------------------------------------------------------------------------------------------------

    //public class TrackProviderDeficits :  AnnualFeedbackProcess
    //{
    //    // Max used to trigger count
    //    int FMaxDificit = 5;
    //    /// <summary>
    //    /// A provider array with number of years of Deficit for each provider
    //    /// </summary>
    //    public ProviderIntArray CountList = new ProviderIntArray(0);
    //    /// <summary>
    //    /// a provider array with the total AF of deficit for each provide across all years
    //    /// </summary>
    //    public ProviderDoubleArray TotalList = new ProviderDoubleArray(0.0);
    //    /// <summary> A provider array how many continuous years in deficit, 0 if not in deficit. </summary>
    //    public ProviderIntArray ContinuousList = new ProviderIntArray(0);
    //    /// <summary> A provider array of longest period of continuous deficit </summary>
    //    public ProviderIntArray LongestContinuous = new ProviderIntArray(0);

    //    //--------------------------------------------------------------
    //    /// <summary>
    //    /// Default Constructor
    //    /// </summary>
    //    public TrackProviderDeficits()
    //        : base()
    //    {
    //        Fname = "Count Provider Deficits";

    //    }
    //    //--------------------------------------------------------------
    //    /// <summary>
    //    /// By Name Constructor
    //    /// </summary>
    //    public TrackProviderDeficits(string aName)
    //        : base(aName)
    //    {

    //    }
    //    //--------------------------------------------------------------
    //    /// <summary>
    //    /// Default Constructor
    //    /// </summary>
    //    public TrackProviderDeficits(WaterSimManager WSim)
    //        : base(WSim)
    //    {
    //        Fname = "Count Provider Deficits";

    //    }
    //    //--------------------------------------------------------------
    //    /// <summary>
    //    /// By Name Constructor
    //    /// </summary>
    //    public TrackProviderDeficits(string aName, WaterSimManager WSim)
    //        : base(aName,WSim)
    //    {

    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> Sets up the process data using InstanceData string. </summary>
    //    ///
    //    /// <remarks> THis method parce the InstanceData string to set the objects fields after a basic create</remarks>
    //    ///
    //    /// <param name="InstanceData"> Information describing the instance. </param>
    //    ///-------------------------------------------------------------------------------------------------

    //    public override void SetupProcessData(string InstanceData)
    //    {
    //    List<Tools.DynamicTextData>  InstanceDataList = Tools.FetchDataFromTextLine(InstanceData,Tools.DataFormat.CommaDelimited);
    //    string Myclassname = InstanceDataList[0].ValueString;
    //    string MyObjectname = InstanceDataList[0].ValueString;
    //    int temp = 5;
    //    if (InstanceDataList[0].CanBeInt())
    //        temp = InstanceDataList[0].ValueInt;
    //    FMaxDificit = temp;
    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> Documentation </summary>
    //    /// <remarks> Returns a string that documents the instance of this class.  Must override for all classes</remarks>
    //    /// <returns> Documentation in a string </returns>
    //    ///-------------------------------------------------------------------------------------------------

    //    public override DocTreeNode Documentation()
    //    {
    //        DocTreeNode node = base.Documentation();
    //        node.AddChildField("MaxDeficit", FMaxDificit.ToString());
    //        return node;
    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> Saves info from this instance in a string. </summary>
    //    ///
    //    /// <remarks>  This instance string can be used to recreate this object using the static CreateProcess method.</remarks>
    //    ///
    //    /// <returns> . </returns>
    //    ///-------------------------------------------------------------------------------------------------

    //    public override string SaveInstance()
    //    {

    //        return this.GetType().Name;
    //    }
        
    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> Gets or sets the deficit limit. </summary>
        
    //    /// <value> The deficit limit. </value>
    //    ///-------------------------------------------------------------------------------------------------

    //    public int DeficitLimit
    //    {
    //        get { return FMaxDificit; }
    //        set { FMaxDificit = value; }
    //    }
    //   //-------------------------------------------------------------------------------------------------
    //   // Build the description strings. </summary>
    //   //-------------------------------------------------------------------------------------------------

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> Builds the description strings. </summary>
    //    /// <remarks>Override to change class description for your new class</remarks>
    //    ///-------------------------------------------------------------------------------------------------

    //    protected override void BuildDescStrings()
    //    {
    //        FProcessDescription = "Counts Provider Yrs for Deficit > "+ FMaxDificit.ToString();
    //        FProcessLongDescription = "Counts the number of years a Provider experienced demand deficit > " + FMaxDificit.ToString() ;
    //        FProcessCode = "CNTDEF_"+ FMaxDificit.ToString();
    //    }

    //    new static public string ClassDescription()
    //    {
    //        return "Counts Years of Provider Deficit based on MaxDeficit trigger.";
    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> method that is called right before the first year of a simulation is called.
    //    ///     </summary>
    //    /// <param name="year"> The year about to be run. </param>
    //    /// <param name="WSim"> The WaterSimManager that is making call. </param>
    //    /// <returns> true if it succeeds, false if it fails. </returns>
    //    ///-------------------------------------------------------------------------------------------------

    //    public override bool ProcessStarted(int year, WaterSimManager WSim)
    //    {
    //        // Reset all the counters
    //        ClearDeficitCounts();
    //        return base.ProcessStarted(year, WSim);
    //    }
    //    //----------------------------------------------------
    //    /// <summary>
    //    /// This is the ProcessMethod that keeps track of Deficits by provider
    //    /// </summary>
    //    /// <param name="year">Simulation Year (not used)</param>
    //    /// <param name="WSim">The WaterSimManager object (used to get data)</param>
    //    /// <returns></returns>
    //    override public bool PostProcess(int year, WaterSimManager WSim)
    //    {
    //        ProviderIntArray Deficits = new ProviderIntArray();
    //        // get the deficit data
    //        Deficits = WSim.Demand_Deficit.getvalues();

    //        foreach (eProvider ep in ProviderClass.providers())
    //        {
    //            if (Deficits[ep] > 0)
    //            {
    //                CountList[ep]++;
    //                TotalList[ep] += Deficits[ep];
    //                ContinuousList[ep]++;
    //                if (LongestContinuous[ep] < ContinuousList[ep]) LongestContinuous[ep] = ContinuousList[ep];
    //            }
    //            else
    //            {
    //                ContinuousList[ep] = 0;
    //            }
    //        }
    //        return true;
    //    }
    //    //----------------------------------------------------

    //    /// <summary>   Clears the deficit counts and totals. </summary>
    //    /// <remarks> Called by SimulationInitialize</remarks>

    //    internal void ClearDeficitCounts()
    //    {
    //        for (int i = 0; i < TotalList.Length; i++)
    //            TotalList[i] = 0.0;
    //        for (int i = 0; i < CountList.Length; i++)
    //            CountList[i] = 0;
    //        for (int i = 0; i < ContinuousList.Length; i++)
    //            ContinuousList[i] = 0;
    //        for (int i = 0; i < LongestContinuous.Length; i++)
    //            LongestContinuous[i] = 0;

    //    }

    //    ///-------------------------------------------------------------------------------------------------
    //    /// <summary> method that is called when a Simulation is initialized. </summary>
    //    /// <param name="WSim"> The WaterSimManager that is making call. </param>
    //    /// <returns> true if it succeeds, false if it fails. </returns>
    //    ///<remarks> Clears Deficit year count</remarks>
    //    ///-------------------------------------------------------------------------------------------------

    //    public override bool ProcessInitialized(WaterSimManager WSim)
    //    {
    //        ClearDeficitCounts();
    //        return base.ProcessInitialized(WSim);
    //    }
    //    //----------------------------------------------------
    //}

   

    //============================================
    //
    //
    // ==============================================

    public class TrackAvailableGroundwater : WaterSimDCDC. AnnualFeedbackProcess
    {
        public ProviderIntArray FYearsOfZeroOrBelow = new ProviderIntArray(0);
        public ProviderDoubleArray FAvailSlope = new ProviderDoubleArray(0.0);
        public ProviderDoubleArray FSlopeIntercept = new ProviderDoubleArray(0.0);

        public ProviderIntArray FYearOfZero = new ProviderIntArray(0);
        public ProviderIntArray FYearsNotAssured = new ProviderIntArray(0);
        public ProviderIntArray FInitialLevel = new ProviderIntArray(0);
        public ProviderIntArray FPctGwAvail = new ProviderIntArray(0);

        WaterSimManager WSimForDispose;
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Default constructor. </summary>
        ///-------------------------------------------------------------------------------------------------

        public TrackAvailableGroundwater() : base ()
        {

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        public TrackAvailableGroundwater(WaterSimManager WSim) : base (WSim)
        {
            SetupParameters(WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="aName"> The name. </param>
        /// <param name="WSim">  The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        public TrackAvailableGroundwater(string aName, WaterSimManager WSim): base(aName, WSim)
        {
            SetupParameters(WSim);
        }

        internal void SetupParameters(WaterSimManager WSim)
        {
            // Add % Groundwater
            Percent_Groundwater_Available = new providerArrayProperty(WSim.ParamManager, eModelParam.epPCT_GWAvailable, get_PCT_GWAvail, eProviderAggregateMode.agWeighted);
            WSim.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epPCT_GWAvailable, "Percent of Initial Groundwater Available", "PCTGWAVL", modelParamtype.mptOutputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_PCT_GWAvail, null, null, null, null, Percent_Groundwater_Available));
            // Add Years of groundwater below zero
            Years_GW_At_or_Below_Zero = new providerArrayProperty(WSim.ParamManager, eModelParam.epYrsGWZero, get_YearsOfZero, eProviderAggregateMode.agAverage);
            WSim.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epYrsGWZero, "# of Years of Groundwater At or Below Zero", "YRGW0", modelParamtype.mptOutputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_YearsOfZero, null, null, null, null, Years_GW_At_or_Below_Zero));
            // add Year GW goes zero estimator
            Year_Groundwater_Will_Be_Zero = new providerArrayProperty(WSim.ParamManager, eModelParam.epYearGWGoesZero, get_YearGWWillBeZero, eProviderAggregateMode.agAverage);
            WSim.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epYearGWGoesZero, "The Year Estimated Groundwater Will Be Zero", "GW0WHEN", modelParamtype.mptOutputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_YearGWWillBeZero, null, null, null, null, Year_Groundwater_Will_Be_Zero));
            // add       Years_Groundwater_Not_Assured/
            Years_Groundwater_Not_Assured = new providerArrayProperty(WSim.ParamManager, eModelParam.epYearsNotAssured, get_Years_NotAssured, eProviderAggregateMode.agAverage);
            WSim.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epYearsNotAssured, "# of Years Groundwater is Not ADWR Assured", "GWNOTAS", modelParamtype.mptOutputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_Years_NotAssured, null, null, null, null, Years_Groundwater_Not_Assured));

            WSimForDispose = WSim;   

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Clean up. </summary>
        ///-------------------------------------------------------------------------------------------------

        public override void CleanUp()
        {
            // OK clean up by delete this parameter
            WSimForDispose.ParamManager.DeleteParameter(eModelParam.epPCT_GWAvailable);
            WSimForDispose.ParamManager.DeleteParameter(eModelParam.epYrsGWZero);
            WSimForDispose.ParamManager.DeleteParameter(eModelParam.epYearGWGoesZero);
            WSimForDispose.ParamManager.DeleteParameter(eModelParam.epYearsNotAssured); 
             }

        new static public string ClassDescription()
        {
            return "Tracks groundwater levels and adds groundwater parameters.";
        }
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Builds the description strings. </summary>
        ///-------------------------------------------------------------------------------------------------

        protected override void BuildDescStrings()
        {
            FProcessDescription = "Creates parameters to track groundwater stats ";
            FProcessLongDescription = "Creates groundwater (GW) parameters to track Percent Available GW Credits, Years Available GW Credits Below Zero, Estimated Year GW Credits Go Zero, Years GW Pumping Does Not Meet AWS rule." ;
            FProcessCode = "TRKGW" ;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> method that is called right before the first year of a simulation is called.
        ///     </summary>
        /// <param name="year"> The year about to be run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool ProcessStarted(int year, WaterSimManager WSim)
        {
            // zero out accumulating arrays
            for (int i = 0; i < FYearsOfZeroOrBelow.Length; i++)
            {
                FYearsOfZeroOrBelow[i] = 0;
                FYearOfZero[i] = 0;
                FPctGwAvail[i] = 0;
                FYearsNotAssured[i] = 0;
                FInitialLevel[i] = 0;
                FAvailSlope[i] = 0.0;
                FSlopeIntercept[i] = 0.0;
                
               // Get Initial Levels
            }
            // Save this WaterSimManager For the process in not already set
            if (WSimForDispose==null)
              WSimForDispose = WSim;
            return base.ProcessStarted(year, WSim);
        }

        const int ASSURREDYEARS = 100;
        ///-------------------------------------------------------------------------------------------------
        /// <summary> Method that is called after each annual run. </summary>
        /// <param name="year"> The year about to be run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        /// <returns> true if it succeeds, false if it fails. Error should be placed in FErrorMessage.
        ///     </returns>
        ///-------------------------------------------------------------------------------------------------
        
        public override bool PostProcess(int year, WaterSimManager WSim)
        {
             // check if GW is zero or below
            ProviderIntArray GWBal = new ProviderIntArray(0);
            GWBal = WSim.Groundwater_Balance.getvalues();
            if (year == WSim.Simulation_Start_Year)
            {
                for (int i = 0; i < GWBal.Length; i++)
                {
                    FInitialLevel[i] = GWBal[i];
                    if (GWBal[i] > 0)
                        FPctGwAvail[i] = 100;
                    else
                        FPctGwAvail[i] = 0;
                }
            }
            else
            {
              
                for (int i = 0; i < GWBal.Length; i++)
                {
                    if (GWBal[i] < 1)
                    // If zero or below, start counting
                    {
                        FYearsOfZeroOrBelow[i]++;
                        if (FYearOfZero[i] == 0)
                        {
                            FYearOfZero[i] = year;
                        }
                    }
                    // calculate slope and X intercept (ie projected year 0)
                    double Y1 = Convert.ToDouble(GWBal[i]);
                    double Rise = Y1 - Convert.ToDouble(FInitialLevel[i]);
                    double X1 = Convert.ToDouble(year);
                    double Run = Convert.ToDouble(year - WSim.Simulation_Start_Year);
                    if ((Rise < 0)&&(Run>0))
                        {
                            FAvailSlope[i] = Rise / Run;
                            double eqconstant = Y1 - (FAvailSlope[i] * X1);
                            if ((FAvailSlope[i] != 0)&&(eqconstant!=0))
                                FSlopeIntercept[i] = (-1 * eqconstant) / FAvailSlope[i];  //(FAvailSlope[i] * Convert.ToDouble(-1 * year)) + Convert.ToDouble(GWBal[i]);
                            else
                                FSlopeIntercept[i] = 0;
                        }
                        else
                        {
                            FAvailSlope[i] = 0;
                            FSlopeIntercept[i] = 0;
                        }
                    // check if first year
                    if ((FSlopeIntercept[i]>WSim.Simulation_Start_Year)&&(FSlopeIntercept[i]<year+ASSURREDYEARS))
                    {
                        FYearsNotAssured[i]++;
                    }
                        // This should only happen if the utility has no groundwater
                    if (FInitialLevel[i] > 0)
                        {
                            // calculate PCT GW Avail
                            double InitLevel = Convert.ToDouble(FInitialLevel[i]);
                            FPctGwAvail[i] = Convert.ToInt32((Convert.ToDouble((GWBal[i]) / InitLevel) * 100));
                        }
                     else
                        {  // no groundwater 
                            // OK here is a wierd rule.  It is possible for a utility to start with no ground water and acquire ground water,
                            // If that is the case, then the first time groundwater is acquired, then balance is set to that.
                            if (GWBal[i] > 0)
                            {
                                FInitialLevel[i] = GWBal[i];
                                FPctGwAvail[i] = 100;
                            }
                            else
                                FPctGwAvail[i] = 0;
                        }
                }
            }

            return base.PostProcess(year, WSim);
        }


        //=========================================================
        //Percent Ground water Available
        //---------------------------------------     

        private int[] get_PCT_GWAvail()
        {
            ProviderIntArray FPCT_GW = new ProviderIntArray(0);
            // get deficit and demand
   
            for (int i = 0; i < FPCT_GW.Length; i++)
            {
                FPCT_GW[i] = FPctGwAvail[i];
            }
            return FPCT_GW.Values;
        }


        /// <summary> Groundwater as a percent of Initial Groundwater Available (100 = 100%) </summary>
        ///<remarks>0 if Deficit is 0 </remarks>
        /// <seealso cref="Demand_Deficit"/>
        /// 
        public providerArrayProperty Percent_Groundwater_Available;

        //=========================================================
        //Years of GW zero or Below
        //---------------------------------------     

        private int[] get_YearsOfZero()
        {
            ProviderIntArray YearsOfZero = new ProviderIntArray(0);
            // get deficit and demand

            for (int i = 0; i < YearsOfZero.Length; i++)
            {
                YearsOfZero[i] = FYearsOfZeroOrBelow[i];
            }
            return YearsOfZero.Values;
        }
        /// <summary> The years groundwater Balance at or below zero. </summary>
        /// <seealso cref="Groundwater_Balance"/>
        public providerArrayProperty Years_GW_At_or_Below_Zero;

        //=========================================================
        //Year GW will go zero
        //---------------------------------------     

        private int[] get_YearGWWillBeZero()
        {
            ProviderIntArray YearWillBeZero = new ProviderIntArray(0);
            // get deficit and demand
            for (int i = 0; i < YearWillBeZero.Length; i++)
            {
                if (FSlopeIntercept[i] > 3000)
                    YearWillBeZero[i] = 3000;
                else
                    if (FSlopeIntercept[i] < 0)
                        YearWillBeZero[i] = 0;
                    else
                        YearWillBeZero[i] = Convert.ToInt32(FSlopeIntercept[i]);
            }
            return YearWillBeZero.Values;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> The years grounwater will be zero. </summary>
        /// <seealso cref="Groundwater_Balance"/>
        ///<seealso cref="Years_Groundwater_Not_Assured"/>
        ///-------------------------------------------------------------------------------------------------

        public providerArrayProperty Year_Groundwater_Will_Be_Zero;
        //=========================================================
        //Count of Years during which Groundwater can not neet Assured Supply rule.
        //---------------------------------------     

        private int[] get_Years_NotAssured()
        {
            ProviderIntArray YearsNotAssured = new ProviderIntArray(0);
            // get deficit and demand

            for (int i = 0; i < YearsNotAssured.Length; i++)
            {
                YearsNotAssured[i] = FYearsNotAssured[i];
            }
            return YearsNotAssured.Values;
        }

        /// <summary> The Number of years groundwater can not be assured. </summary>
        ///<seealso cref="Groundwater_Balance"/>
        ///<seealso cref="Year_Groundwater_Will_Be_Zero"/>
        
        public providerArrayProperty Years_Groundwater_Not_Assured;
    }




    //================================================
    public partial class WaterSimManager
    {
       // TrackProviderDeficitsParameter TPD;

        //=========================================================
        //Percent Deficit
        //---------------------------------------     

        private int[] get_PCT_Deficit()
        {
            ProviderIntArray FPCT_Deficit = new ProviderIntArray(0);
            // get deficit and demand
            int[] PDeficit = Demand_Deficit.getvalues().Values;
            int[] PDemand = Total_Demand.getvalues().Values;

            for (int i = 0; i < FPCT_Deficit.Length; i++)
            {
                // caculated percent as integer 100=100%
                if (PDemand[i] > 0)
                    FPCT_Deficit[i] = (PDeficit[i] * 100) / PDemand[i];
            }
            return FPCT_Deficit.Values;
        }


        /// <summary> Deficit as a percent of Demand (100 = 100%) </summary>
        ///<remarks>0 if Deficit is 0 </remarks>
        /// <seealso cref="Demand_Deficit"/>
        /// 
        public providerArrayProperty Percent_Deficit;

        //============================================================
        //  Percent Reclaimed of Total Supply
        //---------------------------------------------  
        //
        private int[] get_PCT_Reclaimed_Of_Total()
        {
            ProviderIntArray FPCT_Rec = new ProviderIntArray(0);
            // get deficit and demand
            ProviderIntArray PTotal = Total_Water_Supply_Used.getvalues();
            ProviderIntArray PRec = Total_Reclaimed_Used.getvalues();

            for (int i = 0; i < PRec.Length; i++)
            {
                // caculated percent as integer 100=100%
                if (PTotal[i] > 0)
                    FPCT_Rec[i] = (PRec[i] * 100) / PTotal[i];
            }
            return FPCT_Rec.Values;
        }


        /// <summary> Deficit as a percent of Demand (100 = 100%) </summary>
        ///<remarks>0 if Deficit is 0 </remarks>
        /// <seealso cref="Demand_Deficit"/>
        /// 
        public providerArrayProperty Percent_Reclaimed_of_Total;
        //==================================
        partial void     initialize_Sustainable_ModelParameters()
        {
            Percent_Deficit = new providerArrayProperty(_pm,eModelParam.epPCT_Deficit, get_PCT_Deficit, eProviderAggregateMode.agWeighted);
            Percent_Reclaimed_of_Total = new providerArrayProperty(_pm, eModelParam.epPctRecOfTotal, get_PCT_Reclaimed_Of_Total, eProviderAggregateMode.agWeighted);

            _pm.AddParameter(new ModelProviderOutputParameterClass(eModelParam.epPctRecOfTotal, "Percent Supply Reclaimed", "PCTSUPREC", Percent_Reclaimed_of_Total));


            _pm.AddParameter(new ModelProviderOutputParameterClass(eModelParam.epPCT_Deficit, "Percent Demand Deficit", "PCTDEMDEF",  Percent_Deficit));
        

            
         }
    }
}
