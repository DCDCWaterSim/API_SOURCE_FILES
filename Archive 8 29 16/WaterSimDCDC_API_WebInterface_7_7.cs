using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WaterSimDCDC
{
    public partial class WaterSimManager
    {
        const string FileBuild = "04.20.15_12:45:00";
 
        public int PCT_Agriculture = 0;
        public int Web_FlowForEnvironment_memory = 0;
        public int PCT_FlowForEnvironment_memory = 0;
        public int PCT_Personal = 0;
        public int Local_Pop_Rate = 0;
        //
        ProviderIntArray One = new ProviderIntArray(0);
        ProviderIntArray Two = new ProviderIntArray(0);
        ProviderIntArray Three = new ProviderIntArray(0);
        ProviderIntArray Four = new ProviderIntArray(0);
        //
        //
        int[] Add = new int[ProviderClass.NumberOfProviders];
        int[] Zero = new int[ProviderClass.NumberOfProviders];
        int[] Five = new int[ProviderClass.NumberOfProviders];
        int[] Ten = new int[ProviderClass.NumberOfProviders];
        int[] Twenty = new int[ProviderClass.NumberOfProviders];
        int[] Fifty = new int[ProviderClass.NumberOfProviders];
        int[] Onehundred = new int[ProviderClass.NumberOfProviders];
        //
        int[] one = new int[ProviderClass.NumberOfProviders];
        int[] two = new int[ProviderClass.NumberOfProviders];

        public int AgToWebProcess = 0;
        //
        partial void initialize_WebInterface_DerivedParameters()
        {
            // Effluent
            // Ag
            // Environment
            //  Use FlowForEnvironment_PCT;
            // Personal
            //Population
            //  Use set -PopulationGrowthRateAdjustmentPercent
            // Base Inputs
            ModelParameterGroupClass EffGroup = new ModelParameterGroupClass("WebEffluent Dependencies", new int[2] { eModelParam.epWebUIeffluent_Ag, eModelParam.epWebUIeffluent_Env });
            ParamManager.GroupManager.Add(EffGroup);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent, "Effluent: reuse/return", "WEBEFPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent, seti_WebEffluent, RangeCheck.NoSpecialBase, EffGroup));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent_Ag, "Effluent for Ag", "WEBEFAG", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent_Ag, seti_WebEffluent_Ag, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent_Env, "Effluent for Environment", "WEBEFENV", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent_Env, seti_WebEffluent_Env, RangeCheck.NoSpecialBase));
            //
            //this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIAgriculture, "Ag Transfer To Muni", "WEBAGTR1", rangeChecktype.rctCheckRange, 50, 150, geti_WebAg, seti_WebAg, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIAgriculture, "Ag Transfer To Muni", "WEBAGTR1", rangeChecktype.rctCheckRange, 0, 100, geti_WebAg, seti_WebAg, RangeCheck.NoSpecialBase));

            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebPop_GrowthRateAdj_PCT, "Adjust Pop Growth Rate", "WEBPOPGR", rangeChecktype.rctCheckRange, 0, 150, geti_WebPop, seti_WebPop, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIPersonal, "Personal Water Use", "WEBPRPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebPersonal, seti_WebPersonal, RangeCheck.NoSpecialBase));

            // now a percent (0 to 25 % of annual river flow for each river system);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epEnvironmentalFlow_PCT, " Water for Environment", "ENFLOPCT", rangeChecktype.rctCheckRange, 0, 100, geti_FlowEnvironment, seti_FlowEnvironment, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epEnvironmentalFlow_AFa, "Environmental Flows", "ENFLOAMT", rangeChecktype.rctCheckRange, 0, 158088, geti_FlowEnvironment_AF, seti_FlowEnvironment_AF, RangeCheck.NoSpecialBase));

            // Reclaimed Water
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebReclaimedWater, " Regional % Reclaimed Wastewater", "REGRECEFF", rangeChecktype.rctCheckRange, 0, 100, geti_Web_Reclaimed_Water, seti_Web_Reclaimed_Water, RangeCheck.NoSpecialBase));

            // Water Bank Parameter
        //    this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebWaterBankPCT, "Store PCT Excess In Bank", "WEBWBPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebWaterBankPCT , seti_WebWaterBankPCT, RangeCheck.NoSpecialBase));
            
            //
            Web_Parms_setDefaultValues();
            //
        }
        private void Web_Parms_setDefaultValues()
        {
            //Web_AgricultureTransferToMuni_PCT = 100;
            WebFlowForEnvironment_PCT = 0;
            // quay edit
            PCT_FlowForEnvironment_memory = 0;
            // DAS edit
            Web_Personal_PCT = 100;
             PCT_Personal = Web_Personal_PCT;
            //
            Web_PopulationGrowthRate_PCT = 100;
            //Web_Effluent_PCT = 0;
            // Default - ADWR groundwater pumping estimate by 2085 target
            Web_AgricultureTransferToMuni = 31;
        }
        // Web Interface
        /// DAS 02.06.14

        //------------------------------------------------------------------------
        private int geti_FlowEnvironment()
//           { return PCT_FlowForEnviron_memory; }
        { return WebFlowForEnvironment_PCT; }
        private void seti_FlowEnvironment(int value)
        {
            WebFlowForEnvironment_PCT = value;
            //PCT_FlowForEnviron_memory = value;
        }
        //------------------------------------------------------------------------
        private int geti_FlowEnvironment_AF() 
            { return Web_FlowForEnvironment_AF; }
        private void seti_FlowEnvironment_AF(int value)
        {
            _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_AFa, value);
            Web_FlowForEnvironment_AF = value;
        }
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------

        #region Web gets and sets
        //------------------------------------------------------------------------
        /// <summary>
        /// Effluent use for Web Interface
        /// </summary>
        public int Web_Effluent_PCT
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _pm.CheckBaseValueRange(eModelParam.epWebUIeffluent, value);
                    seti_WebEffluent(value);
                    CoupleEffluentToAPI(value);

                }
                // ELSE do we throw an exception? No Just document that it is ignored
            }
            get { return geti_WebEffluent(); }
        }
        //------------------------------------------------------------------------
        private void CoupleEffluentToAPI(int mod)
        {
            int caseSwitch = 0;

            Effluent_array_11();
            caseSwitch = Effluent_Ag_CaseSwitch_11(mod);

            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                temp[i] = Web_Effluent[caseSwitch, 3];
            }
            Temp.Values = temp;
            PCT_Wastewater_to_Effluent.setvalues(Temp);
        }
        // Need a control in the Browser that limits the value to a maximum of 80
        // (the control will only GO to 80 %)
        // 02/18/14
        //ProviderIntArray Web_AgricultureTransferToMuni = new ProviderIntArray();
        //public ProviderIntArray Web_AgricultureTransferToMuni;
       // public providerArrayProperty Web_AgricultureTransferToMuni ;
        public int Web_AgricultureTransferToMuni_PCT
        {
            set
            {
                if (!FModelLocked)
                {
                    _pm.CheckBaseValueRange(eModelParam.epWebUIAgriculture, value);
                    seti_WebAg(value);
                }
                // ELSE do we throw an exception? No Just document that it is ignored
            }
            get { return geti_WebAg(); }
        }
        //------------------------------------------------------------------------
        public int WebFlowForEnvironment_PCT
        {
            set
            {
                if (!FModelLocked)
                {
                    _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_PCT, value);
                    PCT_FlowForEnvironment_memory = value;
                    WaterForDelta(value);
                }
            }
            get
            {
                return PCT_FlowForEnvironment_memory;
            }
        }
        //------------------------------------------------------------------------
        public int Web_Personal_PCT
        {
            set
            {
                if (!FModelLocked)
                {
                    _pm.CheckBaseValueRange(eModelParam.epWebUIPersonal, value);
                    PCT_Personal = value;
                    seti_WebPersonal(value);
                }
                // ELSE do we throw an exception? No Just document that it is ignored
            }
            get { return geti_WebPersonal(); }
        }
        // 
        //------------------------------------------------------------------------
        public int Web_PopulationGrowthRate_PCT
        {
            set
            {
                int scaled = 0;
                if (!FModelLocked)
                {
                    // zero to 150 ONLY!
                     _pm.CheckBaseValueRange(eModelParam.epWebPop_GrowthRateAdj_PCT, value);
                    scaled = annual(value);
                    seti_WebPop(scaled);
                    Local_Pop_Rate = value;
                }
                // ELSE do we throw an exception? No Just document that it is ignored
            }
            get { return geti_WebPop(); }
        }
        //------------------------------------------------------------------------
        public int Web_WaterBankPercent
        {
            set
            {
                seti_WebWaterBankPCT(value);
            }
            get
            {
                return geti_WebWaterBankPCT();
            }
        }

        //---------------------------------------
        private int annual(int rateIn)
        {
            int Rate = 0;
            double percent = rateIn;
            return Rate = Convert.ToInt32(percent);
        }
        #endregion
        //public const int epEnvironmentalFlow_PCT = 250;
        //public const int epEnvironmentalFlow_AFa = 251;
        //public const int epWebUIeffluent = 252;
        //public const int epWebUIAgriculture = 253;
        //public const int epWebUIPersonal = 254;
        //public const int epWebUIAgriculture = 255;
        //public const int epWebUIPersonal = 256;

        #region Agriculture




        /// <summary>
        /// Agriculture
        /// </summary>
        /// <returns></returns>
        /// 
      
        public int geti_WebAg()
        {
             return PCT_Agriculture;
        }
        //const double slope = -0.2;
        //const double intercept = 30; // 29
        const double slope = 0.3;
        const double intercept = 0; // 29
        //const double convertSlope = -1;
        //const double convertIntercept = 150;
        int newValue = 0;
        int Check = 0;
        int myReturnValue = 0;
        //
        // new slope = 0.31; no intercept
        //
        private void seti_WebAg(int value)
        {
            // Why am I using maximum pumping and maximum surface? Check this and comment the code
            if (!FModelLocked)
            {
                // This is the Decision Game Override to the Model
              
                    myReturnValue = value;
                    //    02.24.15 DAS
                    //    This converts the original scale of 50% to 150% 
                    // (50% was hightest transfer, 100 was default, 150 was no transfer)
                    // to 0% to 100% i.e., 0% is no transfer, 100% is 50% of default or maximum transfer
                    // ----------------------------------------------------------------------------------------------------
                    //newValue = Convert.ToInt32(convertSlope * value + convertIntercept);
                    newValue = Convert.ToInt32(slope * value + intercept);
                    _ws.set_AgPumpingCurveIndex = newValue;
                    //
                     // This needs a starting value, Ray needs to add his thoughts...
                    _ws.set_AgCreditTransferPCT = 80;
                    //
              
            }
            PCT_Agriculture = myReturnValue;
        }
        /// <summary>
        ///  Transform 50% to 100% into an index scaled from 20 to zero;
        /// </summary>
        public int Web_AgricultureTransferToMuni
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    seti_WebAg(value);
                }
            }
            get
            {
                return geti_WebAg();
            }
        }

        #endregion
        #region Environment
        /// <summary>
        /// Flows for the Environment left ON RIVER - for the CO RIver Delta (as of 01.20.15)
        /// NEED TO SET a default value</summary>
        public int FlowForEnvironment_PCT
        {
            set
            {
                if (!FModelLocked)
                {
                    _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_PCT, value);

                    PCT_FlowForEnvironment_memory = value;
                    WaterForDelta(value);
                }
            }
            get
            {
                return PCT_FlowForEnvironment_memory;
            }
        }
        //
        // THIS is the NEW web control variable for the User Interface
        // 02.21.15 DAS
        //
        public int Web_FlowForEnvironment_AF
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_AFa, value);
                    EnvironmentOut = value;
                    _ws.set_FlowToCOdelta = Convert.ToInt32(value);
                    PCT_FlowForEnvironment_memory = COdeltaBurdenRatioForAZ;
                    PCT_FlowForEnvironment_memory =  Convert.ToInt32( 100* (value / total));
                }
            }
            get
            {
                if (0 < PCT_FlowForEnvironment_memory)
                {
                    WaterForDelta(PCT_FlowForEnvironment_memory);
                }
                return EnvironmentOut;
            }
        }

        /// <summary>
        /// New Code as of 01.20.15 based on conversations with Ray last week. We are going to use
        /// only Colorado River water, and only water for the Delta. The % in the indicator is simply
        /// going to reflect what percent of the total estimated, needed, is provided for the delta.
        /// The current estimate is: 158,088 acre-feet (195 million cubic meters) 
        /// Reference:  http://ibwc.state.gov/Files/Minutes/Minute_319.pdf
        int EnvironmentOut = 0;
        string FlowDiversion = " Flow diversion Excess";
        internal const double total=158088;
        const double convertToDecmal = 0.01;
        // Units Acre-feet per annum-1
        double Value = 0;

        public void WaterForDelta(int value)
        {
             // Maximum returnable
          
            if (!FModelLocked)
            //if ((!_inRun) & (!FModelLocked))
            {
                //
                try
                {
                    Value = value *convertToDecmal* total;
                   // _ws.set_FlowToEnvironment_CO = Convert.ToInt32(Value);
                    Web_FlowForEnvironment_AF = Convert.ToInt32(Value);
                }
                catch (Exception e)
                {
                    FlowDiversion = e.Message;
                }
                EnvironmentOut = Convert.ToInt32(Value);
        
            }
        }
     
        #endregion
        #region Personal
             int[] alter = new int[ProviderClass.NumberOfProviders];
            int[] ReduceGPCD = new int[ProviderClass.NumberOfProviders];
            private int geti_WebPersonal() { return PCT_Personal; }
            private void seti_WebPersonal(int value)
            {
                Personal(value);
                // ADDED QUAY 3/9/2014
                PCT_Personal = value;
                //--------------
            }
            private void Personal(int Switch)
            {
                //
                int year = _CurrentYear;
                double run = year - 2012;
                double multDefault = 7.3;
                int[]  addDefault = new int[ProviderClass.NumberOfProviders];
                //    
                 switch (Switch)
                {
                    //All (relevant- provider specific) waste water effluent going to Surface Discharge

                    case 100:
                        Provider_Demand_Option = 3;
                         break;

                    default:
                        //    These are the GPCD methods; = 0 then no change, negitive, then decreasing, positive then increasing              
                        alter = PCT_alter_GPCD.getvalues().Values;
                        int Sout = Switch;
                       // API_Default_Status = 1;
                        //
                        if (5 < Switch)
                        {
                            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                            {
                                ReduceGPCD[i] = -( 100 - Switch);
                            }
                        }
                        else
                        {
                           for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                           {
                                if (0 <= Switch)
                                {
                                      ReduceGPCD[i] = -95;
                                }
                               
                          }
                            
                        }
                        Temp.Values = ReduceGPCD ;
 
                         for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                        {
                            addDefault[i] = Math.Max(0,Math.Min(100,  ReduceGPCD[i] + Convert.ToInt32(multDefault * Convert.ToDouble(defaultGPCD[i]))));
                        }
                        TempWebGPCD.Values = addDefault;
                         //
                      // PCT_alter_GPCD.setvalues(Temp);
                       PCT_alter_GPCD.setvalues(TempWebGPCD);

                        //
                        break;
                }

            }
            // DAS 03.18.14
     
     
        #endregion
        #region WaterBankPercent

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the geti web water bank Percent. </summary>
        /// <remarks> Designed for the Web UI, Summarizes all the provide WaterBanlk Pecent values to a regional value, using the regional aggregation</remarks>
        /// <returns>   . </returns>
        ///-------------------------------------------------------------------------------------------------

        public int geti_WebWaterBankPCT()
        {
            int regionvalue = 0;
            regionvalue = PCT_SurfaceWater_to_WaterBank[eProvider.Regional];
        
            return regionvalue;

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Seti web water bank Percent. </summary>
        /// <remarks> Designed for the Web UI, Sets all the provider WaterBanlk Pecent values to a the value passed </remarks>
        /// <param name="value">    The value. </param>
        ///-------------------------------------------------------------------------------------------------

        public void seti_WebWaterBankPCT(int value)
        {
            ProviderIntArray PIA = new ProviderIntArray(value);
            PCT_SurfaceWater_to_WaterBank.setvalues(PIA);
            ProviderIntArray PITo1 = new ProviderIntArray(1);
            WaterBank_Source_Option.setvalues(PITo1);
        }

        #endregion
        #region Population
        /// <summary>
        ///  Population from the Web UI to impact model parameter, i.e., in WaterSimDCDC_API_ver_7_?.cs
        ///    //int[] NewRates = new int[ProviderClass.NumberOfProviders];
        // for (int i = 0; i < NewRates.Length; i++)
        //    NewRates[i] = value;
        //_ws.ProviderPopGrowthRateOnProjectPct = NewRates;
        //_ws.ProviderPopGrowthRateOtherPct = NewRates;

        /// </summary>
        /// <returns></returns>
        private int geti_WebPop()
        {
            int Rate = 0;
            // QUAY EDIT BEGIN 3 10 14
            //if (!FModelLocked)
            // {
            Rate = Local_Pop_Rate;
            //}
            // QUAY EDIT END 3 10 14
            return Rate;
        }
        private void seti_WebPop(int value)
        {
            Local_Pop_Rate = value;
            if (!FModelLocked)
            {
                int[] NewRates = new int[ProviderClass.NumberOfProviders];
                for (int i = 0; i < NewRates.Length; i++)
                    NewRates[i] = value;
                _ws.ProviderPopGrowthRateAdjustPct = NewRates;
            }

        }
        #endregion
    
        //PCEFFREC
        //PCT_Wastewater_Reclaimed
        //The percent of  Waste water effluent that is sent to a Reclaimed Plant (versus a traditional plant-see figure 1).
        // This muts be set first

        //PCRECOUT
        //PCT_Reclaimed_Outdoor_Use
        //The percent of  reclaimed water to be used outdoors. If all available reclaimed water is not used outdoors (i.e., not 100%) it is used indoors as black water.


        //PCRECRO
        //PCT_Reclaimed_to_RO)
        //The percent of  reclaimed water that is sent to a Reverse Osmosis Plant (thus becomming potable water for direct injection or potable water for use in the next time-step).

        //PCRECDI
        //PCT_Reclaimed_to_DirectInject
        //The percent of  reclaimed water that is used to recharge an aquifer by direct injection into an aquifer.

        //PCERECWS
        //PCT_Reclaimed_to_Water_Supply
        // The percent of  reclaimed water that is used to meet qualified user demands (non-potable).

        //PCRECVAD
        // PCT_Reclaimed_to_Vadose
        // The percent of  reclaimed water that is delivered to a vadoze zone recharge basin.

        //PCDEMREC
        //PCT_Max_Demand_Reclaim
        //The amount of (percent of demand that can be met by) reclaimed water that WILL be used as input for the next year.

        #region NewReclaimed
        private int CalcNewReclaimedWaterValue(int value)
        {
            int newVal = 0;
            int defaultCnt = 0;
            int defaultTot = 0;
            // calculate the total and count for those with default
            for (int i = 0; i < Default_Wastewater_Reclaimed.Length; i++)
            {
                if (Default_Wastewater_Reclaimed[i] > value)
                {
                    defaultCnt++;
                    defaultTot += Default_Wastewater_Reclaimed[i];
                }
            }
            int NewCnt = Default_Wastewater_Reclaimed.Length - defaultCnt;
            int NewTotal = (value * Default_Wastewater_Reclaimed.Length) - defaultTot;
            newVal = NewTotal / NewCnt;
            return newVal;
        }

        private void seti_Web_Reclaimed_Water(int value)
        {
            ProviderIntArray New_PCT_Wastewater_Reclaimed = new ProviderIntArray();

            int TheNewValueforNonDefaults = CalcNewReclaimedWaterValue(value);

            for (int i = 0; i < New_PCT_Wastewater_Reclaimed.Length; i++)
            {
                if (value > Default_Wastewater_Reclaimed[i])
                {
                   New_PCT_Wastewater_Reclaimed[i] = TheNewValueforNonDefaults;
                }
                else
                {
                   New_PCT_Wastewater_Reclaimed[i] = Default_Wastewater_Reclaimed[i];
                }
            }
            PCT_Wastewater_Reclaimed.setvalues(New_PCT_Wastewater_Reclaimed);
            ProviderIntArray TestMe = PCT_Wastewater_Reclaimed.getvalues();
        }

        private int geti_Web_Reclaimed_Water()
        {
            int Total = 0;
            ProviderIntArray TestMe = PCT_Wastewater_Reclaimed.getvalues();
            int L = TestMe.Length;
            for (int i = 0; i < L; i++)
            {
                Total += TestMe[i];
            }

            return Total / L;
        }
        #endregion
    }

}
