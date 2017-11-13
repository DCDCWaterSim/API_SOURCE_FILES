using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace WaterSimDCDC
{
    public partial class WaterSimManager
    {
        // public const int epWebPop_GrowthRateAdj_PCT = 102;

        public int PCT_Agriculture = 0;
        public int PCT_FlowForEnvironment_memory = 0;
        public int PCT_Personal = 0;
        public int Local_Pop_Rate = 0;
        // 
        ProviderIntArray Outdoor = new ProviderIntArray(0);
        int[] alter = new int[ProviderClass.NumberOfProviders];
        int[] GPCDr = new int[ProviderClass.NumberOfProviders];
        int[] GPCDbuffer = new int[ProviderClass.NumberOfProviders];
        internal int[] GPCDt = new int[ProviderClass.NumberOfProviders];

        // int AF_MaxAgSurface = 1385;
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
            ModelParameterGroupClass EffGroup = new ModelParameterGroupClass("WebEffluent", this.ParamManager, new int[2] { eModelParam.epWebUIeffluent_Ag, eModelParam.epWebUIeffluent_Env });
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent, "Web Input Effluent", "WEBEFPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent, seti_WebEffluent, RangeCheck.NoSpecialBase, EffGroup, null));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent_Ag, "Web Sub-input Effluent Agriculture", "WEBEFAG", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent_Ag, seti_WebEffluent_Ag, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIeffluent_Env, "Web Sub-input Effluent Environment", "WEBEFENV", rangeChecktype.rctCheckRange, 0, 100, geti_WebEffluent_Env, seti_WebEffluent_Env, RangeCheck.NoSpecialBase));
            //
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIAgriculture, "Web Input Ag Transfer", "WEBAGTR1", rangeChecktype.rctCheckRange, 0, 80, geti_WebAg, seti_WebAg, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebPop_GrowthRateAdj_PCT, "Web Input Pop GR adj %", "WEBPOPGR", rangeChecktype.rctCheckRange, -30, 50, geti_WebPop, seti_WebPop, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epWebUIPersonal, "Web Input Personal", "WEBPRPCT", rangeChecktype.rctCheckRange, 0, 100, geti_WebPersonal, seti_WebPersonal, RangeCheck.NoSpecialBase));

            // now a percent (0 to 25 % of annual river flow for each river system);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epEnvironmentalFlow_PCT, "Environ Flows %", "ENFLOPCT", rangeChecktype.rctCheckRange, 0, 100, geti_FlowEnvironment, seti_FlowEnvironment, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epEnvironmentalFlow_AFa, "Environ Flows AF", "ENFLOAMT", rangeChecktype.rctCheckRange, 0, 500000, geti_FlowEnvironment_AF, seti_FlowEnvironment_AF, RangeCheck.NoSpecialBase));

            //
            setDefaultValues();
            //
        }
        private void setDefaultValues()
        {
            Web_AgricultureTransferToMuni_PCT = 0;
            WebFlowForEnvironment_PCT = 100;
            Web_Personal_PCT = 100;
            Web_PopulationGrowthRate_PCT = 20;
            Web_Effluent_PCT = 100;
        }
        // Web Interface
        /// DAS 02.06.14

        //------------------------------------------------------------------------
        private int geti_FlowEnvironment() { return PCT_FlowForEnviron_memory; }
        private void seti_FlowEnvironment(int value)
        {
            WebFlowForEnvironment_PCT = value;
            PCT_FlowForEnviron_memory = value;
        }
        //------------------------------------------------------------------------
        private int geti_FlowEnvironment_AF() { return FlowForEnvironment_AF; }
        private void seti_FlowEnvironment_AF(int value)
        {
            FlowForEnvironment_AF = value;
        }
        //------------------------------------------------------------------------
        //------------------------------------------------------------------------

        #region Web gets and sets
        //------------------------------------------------------------------------
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
        public ProviderIntArray Web_AgricultureTransferToMuni;
        public int Web_AgricultureTransferToMuni_PCT
        {
            set
            {
                //if ((!_inRun) & (!FModelLocked))
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
                    WaterForEnvironment();
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
        private int annual(int rateIn)
        {
            int Rate = 0;
            double basePop = 100;
            double percent = ((basePop + rateIn) / basePop) * 100;
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
        private int geti_WebEffluent() { return returnEffluent; }// Web_Effluent_PCT; }
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
        public int geti_WebAg()
        {
            // Re-examine this in the cold (warm) look of morning.....!!
            // This is a sustainability indicator
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
            {
                one[p] = (_ws.WaterFromAgPumping[p] + _ws.WaterFromAgSurface[p]) / Total_Demand[p];
            }
            return PCT_Agriculture;
        }
        private void seti_WebAg(int value)
        {
            // Why am I using maximum pumping and maximum surface? Check this and comment the code
            if (!FModelLocked)
            {
                double[] MaxPump = new double[ProviderClass.NumberOfProviders];
                int[] i_MaxPump = new int[ProviderClass.NumberOfProviders];
                int[] MaxSurface = new int[ProviderClass.NumberOfProviders];
                double[] MyAg = new double[ProviderClass.NumberOfProviders];
                //int SurfaceTotal = 0;

                for (int p = 0; p < MaxPump.Length; p++)
                {
                    MyAg[p] = 0;
                    one[p] = 0;
                    if (0 < _ws.get_WaterFromAgPumpingMax[p])
                    {
                        MaxPump[p] = Convert.ToDouble(_ws.get_WaterFromAgPumpingMax[p]);
                        i_MaxPump[p] = _ws.get_WaterFromAgPumpingMax[p];
                        MyAg[p] = Convert.ToDouble(value) * 0.01 * MaxPump[p];
                        one[p] = Convert.ToInt32(MyAg[p]);
                    }
                    //
                    if (0 < _ws.get_WaterFromAgSurfaceMax[p])
                    {
                        MaxSurface[p] = _ws.get_WaterFromAgSurfaceMax[p];
                        MyAg[p] = Convert.ToDouble(value) * 0.01;
                        two[p] = Convert.ToInt32(MyAg[p] * Convert.ToDouble(MaxSurface[p]));
                    }
                }
                //
                One.Values = one;
                Use_WaterFromAgPumping.setvalues(One);
                Two.Values = two;
                Use_WaterFromAgSurface.setvalues(Two);
            }
            PCT_Agriculture = value;
        }
        #endregion
        #region Environment
        /// <summary>
        /// Flows for the Environment left ON RIVER - this represents a combination of the three
        /// systems (for simplicity as of now 11.15.13 DAS)
        /// NEED TO SET a default value</summary>
        public int FlowForEnvironment_PCT
        {
            set
            {
                if (!FModelLocked)
                {
                    _pm.CheckBaseValueRange(eModelParam.epEnvironmentalFlow_PCT, value);

                    PCT_FlowForEnvironment_memory = value;
                    WaterForEnvironment();
                }
            }
            get
            {
                return PCT_FlowForEnvironment_memory;
            }
        }
        public int FlowForEnvironment_AF
        {
            set
            {
                WaterForEnvironment();
            }
            get
            {
                WaterForEnvironment();
                return EnvironmentOut;
            }
        }

        /// <summary>
        int EnvironmentOut = 0;
        string FlowDiversion = " Flow diversion Excess";
        public void WaterForEnvironment()
        {
            // Units Acre-feet per annum-1
            double value = 0;
            // Maximum returnable
            double percent = 0.145;
            if (!FModelLocked)
            //if ((!_inRun) & (!FModelLocked))
            {
                double CO = percent * _ws.get_ColoradoRiverFlow; // _ws.MaxEnvFlowCO
                double ST = percent * _ws.get_SaltTontoAnnualFlow;
                double V = percent * _ws.get_VerdeAnnualFlow;//_ws.MaxEnvFlowVerde;
                double CO_1 = 0;
                double ST_1 = 0;
                double V_1 = 0;
                //
                try
                {
                    CO_1 = Convert.ToDouble(PCT_FlowForEnvironment_memory) * 0.01 * CO;
                    int CO_BI = Convert.ToInt32(CO_1);
                    _ws.set_FlowToEnvironment_CO = CO_BI;
                    //
                    ST_1 = Convert.ToDouble(PCT_FlowForEnvironment_memory) * 0.01 * ST;
                    int ST_BI = _ws.set_FlowToEnvironment_Salt = Convert.ToInt32(ST_1);
                    //
                    V_1 = Convert.ToDouble(PCT_FlowForEnvironment_memory) * 0.01 * V;
                    int V_BI = _ws.set_FlowToEnvironment_Verde = Convert.ToInt32(V_1);
                    value = CO_BI + ST_BI + V_BI;
                }
                catch (Exception e)
                {
                    FlowDiversion = e.Message;


                }
                EnvironmentOut = Convert.ToInt32(value);
            }
        }

        #endregion
        #region Personal

        internal int[] GPCDuse = new int[ProviderClass.NumberOfProviders];
        internal int[] OutDoor = new int[ProviderClass.NumberOfProviders];
        int[] RESOut = new int[ProviderClass.NumberOfProviders];

        private int geti_WebPersonal() { return PCT_Personal; }
        private void seti_WebPersonal(int value)
        {
            Personal(value);
            PCT_Personal = value;
        }
        private void Personal(int Switch)
        {
            //
            int year = _CurrentYear;
            double run = year - 2012;
            //int LowerThreshold = 80;
            // Web_Effluent_PCT = Switch;
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
                    //
                    int subt = 0;
                    subt = Class(Sout);
                    //
                    double mod = 0.5;
                    Provider_Demand_Option = 4;
                    //
                    OutDoor = PCT_Outdoor_WaterUseRes.getvalues().Values;

                    GPCDr = GPCD_raw.getvalues().Values;
                    //
                    for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
                    {
                        GPCDt[p] = 0;
                        GPCDuse[p] = 0;
                    }
                    //
                    for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
                    {

                        if (year < 2013)
                        {
                            GPCDt[p] = GPCDr[p];
                        }
                        else
                        {
                            subt = Convert.ToInt32(Convert.ToDouble((1 - (mod / run)) * subt));

                            // Constant trends
                            if (alter[p] == 0)
                            {
                                GPCDt[p] = GPCDr[p] - subt;
                            }
                            // Increasing trends
                            else if (0 < alter[p])
                            {
                                GPCDt[p] = GPCDr[p] - subt;
                                if (GPCDt[p] < GPCDbuffer[p]) GPCDt[p] = GPCDbuffer[p];
                            }
                            ///Decreasing Trend
                            else
                            {

                                if (GPCDbuffer[p] == GPCDr[p])
                                {
                                    GPCDt[p] = GPCDr[p];
                                }
                                else
                                {
                                    GPCDt[p] = GPCDr[p] - subt;
                                }
                            }
                        }
                        GPCDuse[p] = GPCDt[p];
                        GPCDbuffer[p] = GPCDuse[p];
                    }
                    //
                    Temp.Values = RESOut;
                    PCT_Outdoor_WaterUseRes.setvalues(Temp);
                    //
                    Temp.Values = GPCDuse;
                    Use_GPCD.setvalues(Temp);
                    //
                    break;

            }

        }
        private int Class(int Switch)
        {
            int gpcd = 0;
            int caseSwitch = 0;
            if (Switch == 0) { caseSwitch = 11; }
            else if (Switch > 0 && Switch <= 10) { caseSwitch = 10; }
            else if (Switch > 10 && Switch <= 20) { caseSwitch = 9; }
            else if (Switch > 20 && Switch <= 30) { caseSwitch = 8; }
            else if (Switch > 30 && Switch <= 40) { caseSwitch = 7; }
            else if (Switch > 40 && Switch <= 50) { caseSwitch = 6; }
            else if (Switch > 50 && Switch <= 60) { caseSwitch = 5; }
            else if (Switch > 60 && Switch <= 70) { caseSwitch = 4; }
            else if (Switch > 70 && Switch <= 80) { caseSwitch = 3; }
            else if (Switch > 80 && Switch <= 90) { caseSwitch = 2; }
            else if (Switch > 90 && Switch < 100) { caseSwitch = 1; }

            switch (caseSwitch)
            {

                case 1:
                    gpcd = 1;
                    break;
                case 2:
                    gpcd = 2;
                    break;
                case 3:
                    gpcd = 3;
                    break;
                case 4:
                    gpcd = 4;
                    break;
                case 5:
                    gpcd = 5;
                    break;
                case 6:
                    gpcd = 7;
                    break;
                case 7:
                    gpcd = 9;
                    break;
                case 8:
                    gpcd = 11;
                    break;
                case 9:
                    gpcd = 13;
                    break;
                case 10:
                    gpcd = 15;
                    break;
                case 11:
                    gpcd = 17;
                    break;

                default:
                    gpcd = 0;
                    break;

            }
            return gpcd;
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
            if (!FModelLocked)
            {
                Rate = Local_Pop_Rate;
            }
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

    }
}
