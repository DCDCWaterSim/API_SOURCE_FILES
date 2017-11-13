using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterSimDCDC.Documentation;

namespace WaterSimDCDC
{
    public partial class WaterSimManager
    {
        const string FileBuild = "06.02.16_12:00:00";
        //
        public int PCT_Agriculture = 0;
        public int Web_FlowForEnvironment_memory = 0;
        public int PCT_FlowForEnvironment_memory = 0;
        public int PCT_Personal = 0;
        public int Local_Pop_Rate = 0;
        public int PCT_Augment = 0;
        public int AF_Augment = 0;
        //
        public int AgToWebProcess = 0;
        //
        partial void initialize_WebInterface_DerivedParameters()
        {
            bool quiet = true;
            WaterSimManager WSim = (this as WaterSimManager);

            WaterSimDCDC.Processes.AlterGPCDFeedbackProcess alterGPCD = new WaterSimDCDC.Processes.AlterGPCDFeedbackProcess(WSim,quiet);
             WSim.ProcessManager.AddProcess(alterGPCD);

            ParameterManagerClass FPM = ParamManager;
            Extended_Parameter_Documentation ExtendDoc = FPM.Extended;
            //template is: ExtendDoc.Add(new WaterSimDescripItem(eModelParam., "Description", "Short Unit", "Long Unit", "", new string[] { }, new int[] { }, new ModelParameterGroupClass[] { }));

            // Group Definitions
            ModelParameterGroupClass EffGroup = new ModelParameterGroupClass("WebEffluent Dependencies", new int[2] { eModelParam.epWebUIeffluent_Ag, eModelParam.epWebUIeffluent_Env });
            ParamManager.GroupManager.Add(EffGroup);
            //
           
            // Base Inputs
            // Policies in the WaterSim-Phoenix User Interface (as of 06.02.16)
            // -----------------------------------------------------------------------------------------------
            //
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebReclaimedWater_PCT, " Regional % Reclaimed Wastewater", "REGRECEFF", rangeChecktype.rctCheckRange, 0, 100, geti_Web_Reclaimed_Water, seti_Web_Reclaimed_Water, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIAgriculture, "Ag Transfer To Muni", "WEBAGTR1", rangeChecktype.rctCheckRange, 0, 100, geti_WebAg, seti_WebAg, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epEnvironmentalFlow_AFa, "Environmental Flows", "ENFLOAMT", rangeChecktype.rctCheckRange, 0, 100, geti_FlowEnvironment_AF, seti_FlowEnvironment_AF, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIPersonal_PCT, "Personal Water Use", "WEBPRPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebPersonal, seti_WebPersonal, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebPop_GrowthRateAdj_PCT, "Adjust Pop Growth Rate", "WEBPOPGR", rangeChecktype.rctCheckRange, 0, 150, geti_WebPop, seti_WebPop, RangeCheck.NoSpecialBase));
            //
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebAugmentation_PCT, "Augment Water Supply PCT", "WEBAUGPCT", rangeChecktype.rctCheckRange, 0, 25, geti_WebAugmentPCT, seti_WebAugmentPCT, RangeCheck.NoSpecialBase));

            //
            initialize_Provider_Default_ModelParameters();
            Web_Parms_setDefaultValues();
            //
        }
        int[] rdefault = new int[ProviderClass.NumberOfProviders];
        ProviderIntArray One = new ProviderIntArray(0);
        private void Web_Parms_setDefaultValues()
        {
              // quay edit
            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                rdefault[i] = Default_Wastewater_Reclaimed[i];
            }
            One.Values = rdefault;
            PCT_Wastewater_Reclaimed.setvalues(One);
            //
            PCT_FlowForEnvironment_memory = 0;
            // DAS edit
            Web_Personal_PCT = 100;
            PCT_Personal = Web_Personal_PCT;
            //
            Web_PopulationGrowthRate_PCT = 100;
            // Default - ADWR groundwater pumping estimate by 2085 target
            Web_AgricultureTransferToMuni = 31;
        }
        // Web Interface
        /// DAS 02.06.14

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //public const int epEnvironmentalFlow_PCT = 250;
        //public const int epEnvironmentalFlow_AFa = 251;
        //public const int epWebUIeffluent = 252;
        //public const int epWebUIAgriculture = 253;
        //public const int epWebUIPersonal_PCT = 254;
        //public const int epWebUIAgriculture = 255;
        //public const int epWebUIPersonal_PCT = 256;

        #region Web gets and sets
        //------------------------------------------------------------------------
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
                    _pm.CheckBaseValueRange(eModelParam.epWebUIPersonal_PCT, value);
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
  
        //---------------------------------------
        private int annual(int rateIn)
        {
            int Rate = 0;
            double percent = rateIn;
            return Rate = Convert.ToInt32(percent);
        }
        private int geti_FlowEnvironment()
        //           { return PCT_FlowForEnviron_memory; }
        { return WebFlowForEnvironment_PCT; }
        private void seti_FlowEnvironment(int value)
        {
            _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_PCT, value);
            double d = Convert.ToDouble(value) * 0.01;
            int dOut = Convert.ToInt32(d * 158088);

            Web_FlowForEnvironment_AF = dOut;
            WebFlowForEnvironment_PCT = value;
        }
        //------------------------------------------------------------------------
        private int geti_FlowEnvironment_AF()
        { return Web_FlowForEnvironment_AF; }
        private void seti_FlowEnvironment_AF(int value)
        {
            _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_AFa, value);
            Web_FlowForEnvironment_AF = value;
        }

        #endregion 
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
        //int Check = 0;
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
        // 10.01.15 DAS
        // October 2015 - in response to a re-evaluation of the GPCD estimates
        // brought upon by the simulations needed for the Gober et al. Paper 
        // being written
        // From User_Mods_2 move to this file
        // 09.02.15 DAS
        
       
        double[] _Reduce_GPCD = new double[ProviderClass.NumberOfProviders];
        double[] _Slope_GPCD = new double[ProviderClass.NumberOfProviders];
        double[] _Intercept_GPCD = new double[ProviderClass.NumberOfProviders];
        double[] _DefaultSlope_GPCD = new double[ProviderClass.NumberOfProviders];
        int[] _Target_GPCD = new int[ProviderClass.NumberOfProviders];
        int _BaseYear = 2015;
        
        public double[] API_Reduce_GPCD
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _Reduce_GPCD = value;
                }
            }
            get { return _Reduce_GPCD; }    // 
        }
        public double[] API_Slope_GPCD
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _Slope_GPCD = value;
                }
            }
            get { return _Slope_GPCD; }    // 
        }
        public double[] API_Intercept_GPCD
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _Intercept_GPCD = value;
                }
            }
            get { return _Intercept_GPCD; }    // 
        }
        public int[] API_Target_GPCD
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _Target_GPCD = value;
                }
            }
            get { return _Target_GPCD; }    // 
        }
        public double[] API_defaultSlope_GPCD
        {
            set
            {
                if ((!_inRun) & (!FModelLocked))
                {
                    _DefaultSlope_GPCD = value;
                }
            }
            get { return _DefaultSlope_GPCD; }    // 
        }


           // 
            ProviderIntArray New_slope = new ProviderIntArray(0);
            int[] ReduceGPCD = new int[ProviderClass.NumberOfProviders];
            double[] dNewTarget = new double[ProviderClass.NumberOfProviders];
            double[] Result = new double[ProviderClass.NumberOfProviders];
            int[] addDefault = new int[ProviderClass.NumberOfProviders];
            double[] gpcd2013 = new double[ProviderClass.NumberOfProviders];
            double[] gpcdRAW = new double[ProviderClass.NumberOfProviders];
            //double PCmin = 0;
            //double PCmax = 0;
            //double yr = 0;
            //double Cgpcd = 0;
            //double percent = 0;
            //double slope_ = 0;
            //double error = 0;
           //
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
                double[] dReduce = new double[ProviderClass.NumberOfProviders];
                //
              
                //    
                 switch (Switch)
                {
   
                    case 100:
                        Provider_Demand_Option = 3;
                        for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                        {
                            API_Reduce_GPCD[i] = 1 ;
                        }
                         break;
                    default:
                        //    These are the GPCD methods; = 0 then no change, negitive, then decreasing, positive then increasing              
                       // alter = PCT_alter_GPCD.getvalues().Values;
                        //int Sout = Switch;
                       // API_Default_Status = 1;
                        if (5 < Switch)
                        {
                            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                            {
                                ReduceGPCD[i] = -( 100 - Switch);
                                ReduceGPCD[i] = (100 - Switch);
                           }
                        }
                        else
                        {
                           for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                           {
                                ReduceGPCD[i] = -95;
                           }                     
                        }
                        //
                //        break;
                //}
               // send data to Process_AlterGPCD.cs
                 for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                 {
                     API_Reduce_GPCD[i] = 1 - (Convert.ToDouble(ReduceGPCD[i]) * 1 / 100);
                     API_Slope_GPCD[i] = SlopeGPCD[i];
                     API_Intercept_GPCD[i] = InterceptGPCD[i];
                     API_Target_GPCD[i] = defaultTargetGPCD[i];
                     API_defaultSlope_GPCD[i] =  modSlope[i];
                     gpcdRAW[i] = GPCD_raw.getvalues().Values[i];
                 }
                 break;
                }
  
            }
            // DAS 06.12.15
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
        #region WebReclained
            private int CalcNewReclaimedWaterValue(int value)
            {
                int newVal = 0;
                int defaultCnt = 0;
                int defaultTot = 0;
                int NewTotal = 0;
              
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
             
                NewTotal = (value * Default_Wastewater_Reclaimed.Length) - defaultTot;
                newVal = NewTotal / NewCnt;
                newVal =  Math.Max(0, NewTotal / NewCnt);
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
        // ===============================================================
        // These were removed (may be found in LegacyCode folder)
        //#region Effluent
        //#region Augmentation

            public int geti_WebAugmentPCT()
            {
                int regionvalue = 0;
                regionvalue = PCT_Augment;
                return regionvalue;
            }
            public void seti_WebAugmentPCT(int value)
            {

                PCT_Augment = value;
                seti_WebAugmentAF(value);
            }
            //
            int[] newVal_AF = new int[ProviderClass.NumberOfProviders];
            //
            private void seti_WebAugmentAF(int value)
            {

                CalcAugAsDemandWaterValue(value);
                // provider value
                ProviderIntArray New_Int = new ProviderIntArray(0);
                New_Int.Values = newVal_AF;
                WaterAugmentation.setvalues(New_Int);
                // regional value
                AF_Augment = geti_Web_Augmentation_Water();
            }
            private void CalcAugAsDemandWaterValue(int value)
            {
                double tempVal_AF = 0;
                double demand = 0;
                double proportion = Convert.ToDouble(value) / 100;
                //
                for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                {
                    int year = Sim_CurrentYear;
                    demand = Total_Demand[i];
                    if (year == 0)
                    {
                        tempVal_AF = proportion * demand;
                        newVal_AF[i] = Convert.ToInt32(tempVal_AF);
                    }
                    else
                    { //AF_Augment=0;
                        newVal_AF[i] = AF_Augment;
                    }


                }

            }
            private int geti_Web_Augmentation_Water()
            {
                int Total = 0;

                int L = ProviderClass.NumberOfProviders;
                for (int i = 0; i < L; i++)
                {
                    Total += newVal_AF[i];
                }

                return Total / L;
            }


        //#region Waterbanking


        /// Code parking
        /// 
        // ===============================================================================================================================================================================================
        //internal int[] get_Use_D()
        //{ return _ws.parm; } 
        //internal void set_Use_D(int[] value)
        //{
        //    if (!FModelLocked)
        //    {
        //        _ws.parm = value;
        //    }
        //}

        //

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

    }

}
