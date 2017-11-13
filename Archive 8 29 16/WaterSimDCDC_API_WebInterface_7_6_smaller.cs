using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WaterSimDCDC
{
    public partial class WaterSimManager
    {
        const string FileBuild = "06.17.15_9:13:00";
        ProviderIntArray defaultTargetGPCD = new ProviderIntArray(0);
        ProviderIntArray defaultMinGPCD = new ProviderIntArray(0);
        //
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

        #region Effluent
        int[] Web_Effluent_Vadose = new int[ProviderClass.NumberOfProviders];
        int[] Web_Effluent_Agriculture = new int[ProviderClass.NumberOfProviders];
        int[] Web_Effluent_Environment = new int[ProviderClass.NumberOfProviders];
        //
        const int array = 11;
        const int Mycase = 5;
        int[,] Web_Effluent = new int[array, Mycase];
        int[] Web_Reclaimed = new int[array];
        //
        #region array
        private void Effluent_array_6()
        {
            // Case, 0=Env, 1=Ag, 2=Vadose, 3=Effluent PCT
            Web_Effluent[6, 0] = 100;
            Web_Effluent[6, 1] = 0;
            Web_Effluent[6, 2] = 0;
            Web_Effluent[6, 3] = 100 - Web_Effluent[6, 0];
            Web_Effluent[6, 4] = 100 - Web_Effluent[6, 1];
            Web_Reclaimed[6] = 0;
            //
            Web_Effluent[5, 0] = 75;
            Web_Effluent[5, 1] = 20;
            Web_Effluent[5, 2] = 5;
            Web_Effluent[5, 3] = 100 - Web_Effluent[5, 0];
            Web_Effluent[5, 4] = 100 - Web_Effluent[5, 1];
            Web_Reclaimed[5] = 20;
            //
            Web_Effluent[4, 0] = 50;
            Web_Effluent[4, 1] = 40;
            Web_Effluent[4, 2] = 10;
            Web_Effluent[4, 3] = 100 - Web_Effluent[4, 0];
            Web_Effluent[4, 4] = 100 - Web_Effluent[4, 1];
            Web_Reclaimed[4] = 40;
            //
            Web_Effluent[3, 0] = 25;
            Web_Effluent[3, 1] = 60;
            Web_Effluent[3, 2] = 15;
            Web_Effluent[3, 3] = 100 - Web_Effluent[3, 0];
            Web_Effluent[3, 3] = 100 - Web_Effluent[3, 1];
            Web_Reclaimed[3] = 60;
            //
            Web_Effluent[2, 0] = 10;
            Web_Effluent[2, 1] = 80;
            Web_Effluent[2, 2] = 10;
            Web_Effluent[2, 3] = 100 - Web_Effluent[2, 0];
            Web_Effluent[2, 4] = 100 - Web_Effluent[2, 1];
            Web_Reclaimed[2] = 80;
            //
            Web_Effluent[1, 0] = 0;
            Web_Effluent[1, 1] = 95;
            Web_Effluent[1, 2] = 5;
            Web_Effluent[1, 3] = 100 - Web_Effluent[1, 0];
            Web_Effluent[1, 3] = 100 - Web_Effluent[1, 1];
            Web_Reclaimed[1] = 95;
            //
            Web_Effluent[0, 0] = 0;
            Web_Effluent[0, 1] = 100;
            Web_Effluent[0, 2] = 0;
            Web_Effluent[0, 3] = 100 - Web_Effluent[0, 0];
            Web_Effluent[0, 4] = 100 - Web_Effluent[0, 1];
            Web_Reclaimed[0] = 100;
        }
        private void Effluent_array_11()
        {
            // Case, 0=Env, 1=Ag, 2=Vadose, 3=Effluent PCT

            Web_Effluent[10, 0] = 100;
            Web_Effluent[10, 1] = 0;
            Web_Effluent[10, 2] = 0;
            Web_Effluent[10, 3] = 100 - Web_Effluent[10, 0];
            Web_Effluent[10, 4] = 0;
            Web_Reclaimed[10] = 0;

            Web_Effluent[9, 0] = 80;
            Web_Effluent[9, 1] = 10;
            Web_Effluent[9, 2] = 10;
            Web_Effluent[9, 3] = 100 - Web_Effluent[9, 0];
            Web_Effluent[9, 4] = 100 - Web_Effluent[9, 1];
            Web_Reclaimed[9] = 10;

            Web_Effluent[8, 0] = 70;
            Web_Effluent[8, 1] = 20;
            Web_Effluent[8, 2] = 10;
            Web_Effluent[8, 3] = 100 - Web_Effluent[8, 0];
            Web_Effluent[8, 4] = 100 - Web_Effluent[8, 1];
            Web_Reclaimed[8] = 20;

            Web_Effluent[7, 0] = 55;
            Web_Effluent[7, 1] = 30;
            Web_Effluent[7, 2] = 15;
            Web_Effluent[7, 3] = 100 - Web_Effluent[7, 0];
            Web_Effluent[7, 4] = 100 - Web_Effluent[7, 1];
            Web_Reclaimed[7] = 30;

            Web_Effluent[6, 0] = 45;
            Web_Effluent[6, 1] = 40;
            Web_Effluent[6, 2] = 15;
            Web_Effluent[6, 3] = 100 - Web_Effluent[6, 0];
            Web_Effluent[6, 4] = 100 - Web_Effluent[6, 1];
            Web_Reclaimed[6] = 40;
            //
            Web_Effluent[5, 0] = 35;
            Web_Effluent[5, 1] = 50;
            Web_Effluent[5, 2] = 15;
            Web_Effluent[5, 3] = 100 - Web_Effluent[5, 0];
            Web_Effluent[5, 4] = 100 - Web_Effluent[5, 1];
            Web_Reclaimed[5] = 50;
            //
            Web_Effluent[4, 0] = 25;
            Web_Effluent[4, 1] = 60;
            Web_Effluent[4, 2] = 15;
            Web_Effluent[4, 3] = 100 - Web_Effluent[4, 0];
            Web_Effluent[4, 4] = 100 - Web_Effluent[4, 1];
            Web_Reclaimed[4] = 60;
            //
            Web_Effluent[3, 0] = 20;
            Web_Effluent[3, 1] = 70;
            Web_Effluent[3, 2] = 10;
            Web_Effluent[3, 3] = 100 - Web_Effluent[3, 0];
            Web_Effluent[3, 4] = 100 - Web_Effluent[3, 1];
            Web_Reclaimed[3] = 70;
            //
            Web_Effluent[2, 0] = 15;
            Web_Effluent[2, 1] = 80;
            Web_Effluent[2, 2] = 5;
            Web_Effluent[2, 3] = 100 - Web_Effluent[2, 0];
            Web_Effluent[2, 4] = 100 - Web_Effluent[2, 1];
            Web_Reclaimed[2] = 80;
            //
            Web_Effluent[1, 0] = 10;
            Web_Effluent[1, 1] = 90;
            Web_Effluent[1, 2] = 0;
            Web_Effluent[1, 3] = 100 - Web_Effluent[1, 0];
            Web_Effluent[1, 4] = 100 - Web_Effluent[1, 1];
            Web_Reclaimed[1] = 90;
            //
            Web_Effluent[0, 0] = 0;
            Web_Effluent[0, 1] = 100;
            Web_Effluent[0, 2] = 0;
            Web_Effluent[0, 3] = 100 - Web_Effluent[0, 0];
            Web_Effluent[0, 4] = 100 - Web_Effluent[0, 1];
            Web_Reclaimed[0] = 100;
        }

        private void count()
        {
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Add[p] = 0; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Zero[p] = 0; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Five[p] = 5; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Ten[p] = 10; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Twenty[p] = 20; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Fifty[p] = 50; }
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { Onehundred[p] = 100; }
        }
        #endregion
        #region case switches
        private void CaseSwitch_5()
        {

        }
        private int EffluentCaseSwitch_11(int Switch)
        {
            // Case values are basd on the "return" variable for Environment
            // i.e., returnEnvironment (that is: Web_Effluent[*, 4] )
            int caseSwitch = 0;
            if (Switch >= 1 && Switch <= 5) { caseSwitch = 0; }
            if (Switch > 5 && Switch <= 15) { caseSwitch = 1; }
            if (Switch > 15 && Switch <= 25) { caseSwitch = 2; }
            if (Switch > 25 && Switch <= 35) { caseSwitch = 3; }
            if (Switch > 35 && Switch <= 45) { caseSwitch = 4; }
            if (Switch > 45 && Switch <= 55) { caseSwitch = 5; }
            if (Switch > 55 && Switch <= 65) { caseSwitch = 6; }
            if (Switch > 65 && Switch <= 75) { caseSwitch = 7; }
            if (Switch > 75 && Switch <= 85) { caseSwitch = 8; }
            if (Switch > 85 && Switch <= 95) { caseSwitch = 9; }
            if (Switch > 95) { caseSwitch = 10; }
            return caseSwitch;
        }
        private int Effluent_Ag_CaseSwitch_11(int Mod)
        {
            //int Switch = 100 - Mod;
            int Switch = Mod;
            int caseSwitch = 0;
            if (Switch >= 1 && Switch <= 5) { caseSwitch = 0; }
            if (Switch > 5 && Switch <= 15) { caseSwitch = 1; }
            if (Switch > 15 && Switch <= 25) { caseSwitch = 2; }
            if (Switch > 25 && Switch <= 35) { caseSwitch = 3; }
            if (Switch > 35 && Switch <= 45) { caseSwitch = 4; }
            if (Switch > 45 && Switch <= 55) { caseSwitch = 5; }
            if (Switch > 55 && Switch <= 65) { caseSwitch = 6; }
            if (Switch > 65 && Switch <= 75) { caseSwitch = 7; }
            if (Switch > 75 && Switch <= 85) { caseSwitch = 8; }
            if (Switch > 85 && Switch <= 95) { caseSwitch = 9; }
            if (Switch > 95) { caseSwitch = 10; }
            return caseSwitch;
        }
        private int Effluent_Env_CaseSwitch_11(int Switch)
        {

            int caseSwitch = 0;
            if (Switch >= 1 && Switch <= 5) { caseSwitch = 0; }
            if (Switch > 5 && Switch <= 12) { caseSwitch = 1; }
            if (Switch > 12 && Switch <= 17) { caseSwitch = 2; }
            if (Switch > 17 && Switch <= 22) { caseSwitch = 3; }
            if (Switch > 22 && Switch <= 30) { caseSwitch = 4; }
            if (Switch > 30 && Switch <= 40) { caseSwitch = 5; }
            if (Switch > 40 && Switch <= 50) { caseSwitch = 6; }
            if (Switch > 50 && Switch <= 63) { caseSwitch = 7; }
            if (Switch > 63 && Switch <= 75) { caseSwitch = 8; }
            if (Switch > 75 && Switch <= 90) { caseSwitch = 9; }
            if (Switch > 90) { caseSwitch = 10; }
            return caseSwitch;
        }

        //
        private int EffluentCaseSwitch_6(int Switch)
        {
            // Case values are basd on the "return" variable for Environment
            // i.e., returnEnvironment (that is: Web_Effluent[*, 4] )
            int caseSwitch = 0;
            if (Switch >= 1 && Switch <= 15) { caseSwitch = 1; }
            else if (Switch > 15 && Switch <= 30) { caseSwitch = 2; }
            else if (Switch > 30 && Switch <= 50) { caseSwitch = 3; }
            else if (Switch > 50 && Switch <= 70) { caseSwitch = 4; }
            else if (Switch > 70 && Switch <= 90) { caseSwitch = 5; }
            else if (Switch > 90) { caseSwitch = 6; }
            return caseSwitch;
        }
        private int Effluent_Ag_CaseSwitch_6(int Mod)
        {
            int Switch = 100 - Mod;
            // Case values are basd on the "return" variable for Environment
            // i.e., returnEnvironment (that is: Web_Effluent[*, 4] )
            int caseSwitch = 0;
            if (Switch >= 1 && Switch <= 15) { caseSwitch = 1; }
            else if (Switch > 15 && Switch <= 30) { caseSwitch = 2; }
            else if (Switch > 30 && Switch <= 50) { caseSwitch = 3; }
            else if (Switch > 50 && Switch <= 70) { caseSwitch = 4; }
            else if (Switch > 70 && Switch <= 90) { caseSwitch = 5; }
            else if (Switch > 90) { caseSwitch = 6; }
            return caseSwitch;
        }




        //       int caseSwitch = 0;
        //       if (Switch >= 1 && Switch <= 16) { caseSwitch = 1; }
        //else if (Switch > 16 && Switch <= 32) { caseSwitch = 2; }
        //else if (Switch > 32 && Switch <= 48) { caseSwitch = 3; }
        //else if (Switch > 48 && Switch <= 64) { caseSwitch = 4; }
        //else if (Switch > 64 && Switch <= 80) { caseSwitch = 5; }
        //else if (Switch > 80 && Switch <= 100) { caseSwitch = 6; }
        //return caseSwitch;   

        #endregion
        //
        private ProviderIntArray Temp;
        private ProviderIntArray TempWebGPCD;

        private ProviderIntArray REC;
        // internal int[] Ag = new int[ProviderClass.NumberOfProviders];
        internal int agriculture = 0;
        internal int returnAgriculture = 0;
        internal int vadose = 0;
        internal int actualEnvironment = 0;
        internal int reclaimed = 0;
        internal int effluent = 0;
        internal int Effluent = 0;

        internal int returnEnvironment = 0;
        internal int returnEffluent = 0;

        //  internal int[] Env = new int[ProviderClass.NumberOfProviders];
        internal int[] Vad = new int[ProviderClass.NumberOfProviders];
        internal int[] Rec = new int[ProviderClass.NumberOfProviders];
        //internal int[] Total = new int[ProviderClass.NumberOfProviders];
        //
        internal int[] Get = new int[ProviderClass.NumberOfProviders];
        //  internal double[] Reclaimed = new double[ProviderClass.NumberOfProviders];
        //
        private int geti_WebEffluent() { return returnEffluent; }
        private void seti_WebEffluent(int value)
        {
            Set_Effluent(value);
        }
        private void Set_Effluent(int Switch)
        {
            Effluent_array_11();
            int caseSwitch = EffluentCaseSwitch_11(Switch);
            //
            count();
            //
            One.Values = Zero;
            PCT_Effluent_to_PowerPlant.setvalues(One);
            //
            REC = PCT_Wastewater_Reclaimed.getvalues();
            Get = PCT_Reclaimed_to_Water_Supply.getvalues().Values;
            //
            actualEnvironment = Web_Effluent[caseSwitch, 0];
            agriculture = Web_Effluent[caseSwitch, 1];
            vadose = Web_Effluent[caseSwitch, 2];
            effluent = Web_Effluent[caseSwitch, 3];

            returnEnvironment = Web_Effluent[caseSwitch, 4];

            reclaimed = Web_Reclaimed[caseSwitch];
            //
            setAPI();
            //               
            returnEffluent = Switch;
            returnAgriculture = 100 - returnEnvironment;
        }
        private void setAPI()
        {
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
            {
                Add[p] = agriculture + vadose;
                Vad[p] = vadose;
                Rec[p] = reclaimed;
            }
            One.Values = Add;
            PCT_Wastewater_to_Effluent.setvalues(One);
            Two.Values = Vad;
            PCT_Effluent_to_Vadose.setvalues(Two);
            Three.Values = Rec;
            PCT_Reclaimed_to_Water_Supply.setvalues(Three);
            Four.Values = Zero;
            PCT_Reclaimed_to_Vadose.setvalues(Four);
            PCT_Reclaimed_to_DirectInject.setvalues(Four);
            PCT_Reclaimed_to_RO.setvalues(Four);
            AgToWebProcess = agriculture;
        }
        // lower level controls for Effluent
        /// <summary>
        /// Sub control for Effluent One
        /// </summary>
        int[] temp = new int[ProviderClass.NumberOfProviders];

        public int Web_Effluent_Ag
        {
            set
            {
                seti_WebEffluent_Ag(value);
            }
            get
            {
                return geti_WebEffluent_Ag();
            }

        }
        private void seti_WebEffluent_Ag(int value)
        {
            Effluent_Agriculture(value);
        }
        private void Effluent_Agriculture(int Switch)
        {
            int caseSwitch = 0;
            agriculture = 0;
            Effluent_array_11();
            caseSwitch = Effluent_Ag_CaseSwitch_11(Switch);
            //
            agriculture = Web_Effluent[caseSwitch, 1];
            actualEnvironment = Web_Effluent[caseSwitch, 0];
            vadose = Web_Effluent[caseSwitch, 2];
            returnEnvironment = Web_Effluent[caseSwitch, 4];
            returnEffluent = Web_Effluent[caseSwitch, 4];
            returnAgriculture = Switch;
            //
            setAPI();
        }
        private int geti_WebEffluent_Ag()
        {
            return returnAgriculture; // agriculture;
        }
        public int Web_effluent_PCT
        {
            set
            {
                effluent = value;
            }
            get
            {
                return Web_effluent_PCT;
            }
        }
        //
        /// <summary>
        ///  Sub control for Effluent Two
        /// </summary>
        public int Web_Effluent_Env
        {
            set
            {
                seti_WebEffluent_Env(value);
            }
            get
            {
                return returnEnvironment;
            }

        }
        private void seti_WebEffluent_Env(int value)
        {
            Effluent_Environment(value);
        }
        private void Effluent_Environment(int Mod)
        {
            Effluent_array_11();
            int Switch = Effluent_Env_CaseSwitch_11(Mod);
            returnAgriculture = 100 - Mod;
            agriculture = Web_Effluent[Switch, 1];
            actualEnvironment = Web_Effluent[Switch, 0];
            vadose = Web_Effluent[Switch, 2];
            returnEnvironment = Mod; // Web_Effluent[Switch, 4];
            setAPI();
            returnEffluent = returnEnvironment;
        }
        private int geti_WebEffluent_Env()
        {
            return actualEnvironment;
        }


        //PCT_Effluent_to_Vadose
        // PCT_Wastewater_to_Effluent
        //
        #endregion
        // sub controls - need a "switch" to tell the API that the sub controls are being used
        //PCT_Wastewater_to_Effluent
        // PCT_Effluent_to_PowerPlant
        // PCT_Effluent_to_Vadose
        // PCT_Reclaimed_to_Water_Supply
        // NOTE: set PCT_Reclained_to_Vadose and PCT_Reclaimed_toDirectInject to zero
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
                double PCmin = 0;
                double PCmax = 0;
                double run = year - 2012;
                 int[]  addDefault = new int[ProviderClass.NumberOfProviders];
                 int[] empiricalGPCD = new int[ProviderClass.NumberOfProviders];

                double[]  Result = new double[ProviderClass.NumberOfProviders];
                double[] BoundsCheck = new double[ProviderClass.NumberOfProviders];
                double[] dReduce = new double [ProviderClass.NumberOfProviders];
                double[] dNewTarget = new double[ProviderClass.NumberOfProviders];

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
                                ReduceGPCD[i] = -95;
                           }                     
                        }
                        //Temp.Values = ReduceGPCD ;
                         for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                        {
                            // Based on the run in the API in the method  GPCDFIX();
                             // The value returned here is the last value of the simulation
                             // ran in this method. Now set for 2013
                            empiricalGPCD[i] = GPCD_raw[i];
                            //
                            dReduce[i] = Convert.ToDouble(ReduceGPCD[i]);
                            //
                            // FROM  partial void initialize_Provider_Default_ModelParameters()   
                            if (0 < defaultTargetGPCD[i])
                            {
                             }
                            else
                            {
                                // for equal weight Browns' simple exponential smoothing algorithm
                                defaultTargetGPCD[i] = empiricalGPCD[i];
                            }
                               dNewTarget[i] = defaultTargetGPCD[i] + defaultTargetGPCD[i] * (dReduce[i] * 1 / 100);
                               // for Brown's discount function
                               if (dNewTarget[i] <  defaultMinGPCD[i])
                               {
                                   dNewTarget[i] = defaultMinGPCD[i] + defaultTargetGPCD[i] * (dReduce[i] * 1 / 100); ;
                               }
                            //
                            //   
                            Result[i] = ((dNewTarget[i] - empiricalGPCD[i]) / empiricalGPCD[i]) * 100;
                            PCmin = 0;
                            PCmax = 0;
                             // Can only do an 85% increase.
                            PCmin = Math.Min(85,Result[i]);
                            PCmax = Math.Max(-95, PCmin);
                            addDefault[i] =  Convert.ToInt32(PCmax);
                          }
                        TempWebGPCD.Values = addDefault;
                        PCT_alter_GPCD.setvalues(TempWebGPCD);
                        break;
                }

            }
            // DAS 06.12.15
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
        /// Code parking
        /// 
        //internal int[] get_Use_D()
        //{ return _ws.parm; } 
        //internal void set_Use_D(int[] value)
        //{
        //    if (!FModelLocked)
        //    {
        //        _ws.parm = value;
        //    }
        //}

        // Outdoor Recalimed Water Use


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

    }

}
