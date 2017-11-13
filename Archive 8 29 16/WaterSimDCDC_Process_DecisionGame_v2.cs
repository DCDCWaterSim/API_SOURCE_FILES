using System;
using System.Collections.Generic;
using WaterSimDCDC;
using WaterSimDCDC.Documentation;
using System.Text;

namespace WaterSimDCDC
{
    // 05.05.2014
    public class DecisionGame : AnnualFeedbackProcess
    {

        const double GalToAF = 0.000003068883233;
        const string _APIVersion = "4.2";  // latest version of API
        //
        ProviderIntArray One = new ProviderIntArray(0);
         public int[]  Demand = new int[ProviderClass.NumberOfProviders];
        internal int[] Res = new int[ProviderClass.NumberOfProviders];
         internal int[] dRes = new int[ProviderClass.NumberOfProviders];
        internal int[] Com = new int[ProviderClass.NumberOfProviders];
         internal int[] dCom = new int[ProviderClass.NumberOfProviders];
        internal int[] Ind = new int[ProviderClass.NumberOfProviders];
         internal int[] dInd = new int[ProviderClass.NumberOfProviders];
        internal int[] Env = new int[ProviderClass.NumberOfProviders];
        internal int[] Ag = new int[ProviderClass.NumberOfProviders];
        //
        internal int[] InVoked = new int[ProviderClass.NumberOfProviders];
        internal int[] Environment = new int[ProviderClass.NumberOfProviders];
        internal int[] Agriculture = new int[ProviderClass.NumberOfProviders];
        //
        internal int[] Zero = new int[ProviderClass.NumberOfProviders];
        //
        protected double[] Tres = new double[ProviderClass.NumberOfProviders];
        protected double[] Tcom = new double[ProviderClass.NumberOfProviders];
        protected double[] Tind = new double[ProviderClass.NumberOfProviders];
        //
        protected double[] res = new double[ProviderClass.NumberOfProviders];
        protected double[] com = new double[ProviderClass.NumberOfProviders];
        protected double[] ind = new double[ProviderClass.NumberOfProviders];
        protected double[] env = new double[ProviderClass.NumberOfProviders];
        protected double[] ag = new double[ProviderClass.NumberOfProviders];
        //
        protected double[] uRes = new double[ProviderClass.NumberOfProviders];
        protected double[] uCom = new double[ProviderClass.NumberOfProviders];
        protected double[] uInd = new double[ProviderClass.NumberOfProviders];
        //
        protected double[] vRes = new double[ProviderClass.NumberOfProviders];
        protected double[] vCom = new double[ProviderClass.NumberOfProviders];
        protected double[] vInd = new double[ProviderClass.NumberOfProviders];
        //
        double[] GPCDTminus1 = new double[ProviderClass.NumberOfProviders];
        double[] GPCDnew = new double[ProviderClass.NumberOfProviders];
        double[] GPCDind = new double[ProviderClass.NumberOfProviders];
        double[] GPCDenv = new double[ProviderClass.NumberOfProviders];
        //
        double[] resDefault = new double[ProviderClass.NumberOfProviders];
        double[] comDefault = new double[ProviderClass.NumberOfProviders];
        double[] indDefault = new double[ProviderClass.NumberOfProviders];
        //
        int[] iRes = new int[ProviderClass.NumberOfProviders];
        int[] iCom = new int[ProviderClass.NumberOfProviders];
        int[] iInd = new int[ProviderClass.NumberOfProviders];
        //
        double[] gpcdRaw = new double[ProviderClass.NumberOfProviders];
        //double[] gpcdRawInd = new double[ProviderClass.NumberOfProviders];
        double[] gpcdDiffInd = new double[ProviderClass.NumberOfProviders];
        double[] GPCDAg = new double[ProviderClass.NumberOfProviders];

        double[] AF_total = new double[ProviderClass.NumberOfProviders];
        //
        double AddAFt = 0;
        int[] AF_environment = new int[ProviderClass.NumberOfProviders];
        int[] AF_agriculture = new int[ProviderClass.NumberOfProviders];
        //
        static double[] GPCDstaticRes = new double[ProviderClass.NumberOfProviders];
        static double[] GPCDstaticCom = new double[ProviderClass.NumberOfProviders];
        static double[] GPCDstaticInd = new double[ProviderClass.NumberOfProviders];
        static double[] residential = new double[ProviderClass.NumberOfProviders];
        static double[] commercial = new double[ProviderClass.NumberOfProviders];
        static double[] industrial = new double[ProviderClass.NumberOfProviders];

        //=====================================================
        public DecisionGame()
            : base()
        {
            Fname = "SP Alter GPCD dynamically";
        }
        public DecisionGame(string aName)
            : base(aName)
        {
            BuildDescStrings();

        }
        public DecisionGame(string aName,WaterSimManager WSim, bool quiet)
            : base(WSim, quiet)

        {
            Fname = aName;
            BuildDescStrings();
            this.Name = this.GetType().Name;
            FWsim = WSim;
 
        }
        public DecisionGame(string aName, WaterSimManager WSim)
            : base(aName, WSim)
        {
            Fname = aName;
            BuildDescStrings();
            this.Name = this.GetType().Name;
            FWsim = WSim;
        }
        //public override void SetupProcessData(string InstanceData)
        //{
        //    List<dbTool.DynamicTextData> InstanceDataList = dbTool.FetchDataFromTextLine(InstanceData, dbTool.DataFormat.CommaDelimited);
        //    string Myclassname = InstanceDataList[0].ValueString;
        //    string MyObjectname = InstanceDataList[0].ValueString;
        //    int temp = 5;
        //    if (InstanceDataList[0].CanBeInt())
        //        temp = InstanceDataList[0].ValueInt;
        //    FMax = temp;

        //    base.SetupProcessData(InstanceData);
        //}
        internal double scaler(double range, double _default, double _input)
        {
            bool skip = false;
            double ErrorUp = _input + 0.01;
            double ErrorDn = _input - 0.01;
            double result=0;
            double Default = _default;// *0.01;
            double input = _input * 100;
             double response = 0;
             double Calc = 0;
            double min = Math.Max(0,Default - (Default* (range)));
            double max = Math.Min(100, Default + (Default * (range)));
 
            result = 0;
              if(0 < min)
                {
                    if (Default >= ErrorDn && Default <= ErrorUp) skip = true;
                    if (skip)
                    {
                        result = Default;
                    }
                    else
                    {
                        response =  ((input - Default) / Default) ;
                        Calc = (response * Default) + Default;
                        result = Math.Min(Math.Max(Calc, min), max) /100;
                    }
                }
            return result;
        }
        //
        internal void defaults(WaterSimManager WSim)
        {
            for (int m = 0; m < 33; m++)
            {
                resDefault[m]  = Convert.ToDouble(WSim.PCT_residential_default.getvalues().Values[m]);
                 dRes[m] = WSim.PCT_residential_default.getvalues().Values[m];
                //
                comDefault[m] = Convert.ToDouble(WSim.PCT_commercial_default.getvalues().Values[m]);
                 dCom[m] = WSim.PCT_commercial_default.getvalues().Values[m];
                indDefault[m]  = Convert.ToDouble(WSim.PCT_industrial_default.getvalues().Values[m]);
                 dInd[m] = WSim.PCT_industrial_default.getvalues().Values[m];
                gpcdRaw[m] = WSim.GPCD_raw.getvalues().Values [m];
                //
                residential[m] = resDefault[m] / 100;
                commercial[m] = comDefault[m] / 100;
                industrial[m] = indDefault[m] / 100;
                //
             }
        
        }
        internal void initialize(WaterSimManager WSim)
        {
            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                Res[i] = WSim.PCT_residential_decisionGame.getvalues().Values[i] ; //DGAMERES
                Com[i] = WSim.PCT_commercial_decisionGame.getvalues().Values[i] ; //DGAMECOM
                Ind[i] = WSim.PCT_industrial_decisionGame.getvalues().Values[i] ; //DGAMEIND
                Env[i] = WSim.PCT_envWater_decisionGame.getvalues().Values[i]; //DGAMEENV, 02.10.15
                Ag[i] = WSim.PCT_agWater_decisionGame[i]; // DGAMEENVAF - 02.10.15 DAS
             }
         }
        //
        internal void userInputs(WaterSimManager WSim)
        {
            // set local inputs from global inputs collected from players
            for (int m = 0; m < 33; m++)
            {
                res[m] = Convert.ToDouble(Res[m]) / 100;
                com[m] = Convert.ToDouble(Com[m]) / 100;
                ind[m] = Convert.ToDouble(Ind[m]) / 100;
                env[m] = Convert.ToDouble(Env[m]) / 100;
                //ag[m] = Convert.ToDouble(Ag[m]) / 100;
                //
                InVoked[m] = 1;
                if (Res[m] != dRes[m]) InVoked[m] = 0;
            }
            // Re-set the three equilivant to equal one
            for (int n = 0; n < 33; n++)
            {
                double sum = 1;
                sum = res[n] + com[n] + ind[n];
                Tres[n] = res[n] / sum;
                Tcom[n] = com[n] / sum;
                Tind[n] = ind[n] / sum;
            }

        }
        //
        internal void scaleAdjust(int n, WaterSimManager WSim)
        {
            double add = 0;
            double Ssum = 1;
            double Mod = 1;
            double Ratio = 1;
            const double mult = 0.97;
             const double basis2 = 0.1;
            //
                 Ssum = uRes[n] + uCom[n] + uInd[n];
                if (Ssum < 1)
                {
                    Mod = Ratio / Ssum;
                }
                else
                {
                    if (1 < Ssum)
                    {
                        Mod = Ssum / Ratio;
                    }
                }
                vRes[n] = uRes[n] * Mod;
                vCom[n] = uCom[n] * Mod;
                vInd[n] = uInd[n] * Mod;
               //
                 add = (vRes[n] + vCom[n] + vInd[n]);
                if (add != 1.000000)
                {
                      if((1 + basis2) < add) {
                        vRes[n] *=mult;
                        vCom[n] *= mult;
                        vInd[n] = Math.Max(0, 1.0 - vRes[n] - vCom[n]);
                    }
                    else
                    {
                        vInd[n] = Math.Max(0, 1.0 - vRes[n] - vCom[n]);
                    }
                }
               //
                gpcdRaw[n] = WSim.GPCD_raw.getvalues().Values[n];
                GPCDstaticRes[n] = gpcdRaw[n] * residential[n];
                GPCDstaticCom[n] = gpcdRaw[n] * commercial[n];
                GPCDstaticInd[n] = gpcdRaw[n] * industrial[n];
            //
        }
        //
        int[] Population = new int[33];
        bool invoke = false;
        double Difference = 0.20;

        public void ProcessTminus1(int year, WaterSimManager WSim)
        {
            for (int m = 0; m < 33; m++)
            {
                if (year < 2012) {
                    invoke = false;
                    GPCDTminus1[m] = WSim.GPCD_raw.getvalues().Values[m];
                } 
                else
                { 
                    invoke = true;
                    GPCDTminus1[m] = WSim.GPCD_Used.getvalues().Values[m];
                };
            }
        }
        ProviderIntArray AgWaterIndex = new ProviderIntArray(0);
        int[] pct_agWaterCurveIndex = new int[ProviderClass.NumberOfProviders];

        public void Process(int year, WaterSimManager WSim)
        {

            if (invoke)
            {
                userInputs(WSim);
                 // Set the user inputs to constrained (quasi real world) limits based on "Difference" value
                for (int n = 0; n < 33; n++)
                {
                    uRes[n] = scaler(Difference, resDefault[n], Tres[n]);
                    uCom[n] = scaler(Difference, comDefault[n], Tcom[n]);
                    uInd[n] = scaler(Difference, indDefault[n], Tind[n]);
                    //
                     scaleAdjust(n,WSim);
                    //
                }
                AddAFt = 0;
                double efficiency = 0.01;
                double mod = 1 - (efficiency += efficiency);
                for (int mSet = 0; mSet < 33; mSet++)
                {
                    //
                    GPCDind[mSet] = gpcdRaw[mSet] * vInd[mSet];
                    // Delta Industry changes GPCD
                    gpcdDiffInd[mSet] = GPCDind[mSet] - GPCDstaticInd[mSet];
                    //
                    // why am I using defaults here?
                    GPCDnew[mSet] = (GPCDstaticRes[mSet] + GPCDstaticCom[mSet] + GPCDstaticInd[mSet] + gpcdDiffInd[mSet] ) ;
                    //
                    Population[mSet] = WSim.Population_Used[mSet];
                    // do not know if this should be  GPCDnew[mSet]  or GPCDTminus1[mSet] ... 05.16.14
                    AF_total[mSet] = (Population[mSet] * GPCDTminus1[mSet] * GalToAF * DayToYear(year));
                    //
                    AF_environment[mSet] = Convert.ToInt32(AF_total[mSet] * env[mSet]);
                    AF_agriculture[mSet] = 0; // THIS is not needed
                    //
                    GPCDenv[mSet] = AF_environment[mSet] / ((Population[mSet] * GalToAF * DayToYear(year)));
                    GPCDAg[mSet] = AF_agriculture[mSet] / ((Population[mSet] * GalToAF * DayToYear(year)));
                    AddAFt += AF_environment[mSet];
                }
                //
                WSim.Provider_Demand_Option = 4;
                //
                for (int p = 0; p < 33; p++)
                {
                    Zero[p] = 0;
                   
                    WSim.Use_GPCD[p] = Convert.ToInt32(GPCDnew[p]);
                    Demand[p] = Convert.ToInt32( GPCDnew[p] * Population[p]);
                }
                 One.Values = Zero;
                WSim.PCT_WaterSupply_to_Residential.setvalues(One);
                WSim.PCT_WaterSupply_to_Commercial.setvalues(One);
                WSim.PCT_WaterSupply_to_Industrial.setvalues(One);
                int add = 0;
                for (int p = 0; p < 33; p++)
                {
                    iRes[p] = dRes[p];
                    iCom[p] = dCom[p];
                    iInd[p] = dInd[p];
                     Environment[p] = 0;
                     //Agriculture[p]
                    if (InVoked[p] < 1)
                    {
                        iRes[p] = Convert.ToInt32(vRes[p] * 100);
                        iCom[p] = Convert.ToInt32(vCom[p] * 100);
                        if (100 < (iRes[p] + iCom[p]))
                        {
                            iCom[p] = 100 - iRes[p];
                        }
                        iInd[p] = Math.Max(0, 100 - iRes[p] - iCom[p]);
                        Environment[p] = AF_environment[p];
                        Agriculture[p] = WSim.PCT_agWater_decisionGame.getvalues().Values[p];

                    }
                    else
                    {
                        add = iRes[p] + iCom[p] + iInd[p];
                        if (add != 100) iInd[p] = 100 - iRes[p] - iCom[p];
                     }
                     //
                 }
                // Set the values to send to the FORTRAN model
                One.Values = iRes;
                WSim.PCT_WaterSupply_to_Residential.setvalues(One);
                //
                //   Return the "corrected" values to the Decision Game variables
                // so that the Interface knows the actual value used.
                  WSim.PCT_residential_decisionGame.setvalues(One);
                One.Values = iCom;
                WSim.PCT_WaterSupply_to_Commercial.setvalues(One);
                 WSim.PCT_commercial_decisionGame.setvalues(One);
                One.Values = iInd;
                WSim.PCT_WaterSupply_to_Industrial.setvalues(One);
                 WSim.PCT_industrial_decisionGame.setvalues(One);
                //
                // 02.10.15 DAS
                 One.Values = Environment;
                //One.Values = AF_environment;
                WSim.EnvWater_decisionGame_AF.setvalues(One);
                //
                // 03.04.15 test -- not finished
                //for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
                //{
                //    WSim.Web_FlowForEnvironment_AF = AF_environment[p]; 
                //}
                //Ag set in initialize
                //
                // 02.05.15 - asssume that all parties share in the Delta Water Burden
                // WSim.COdeltaBurdenToAZ = false;
            
 
            }
            else
            {
                //WSim.PCT_WaterSupply_to_Residential = WSim.PCT_residential_default;
                //WSim.PCT_WaterSupply_to_Commercial = WSim.PCT_commercial_default;
                //WSim.PCT_WaterSupply_to_Industrial = WSim.PCT_industrial_default;
                ////
                //// 02.10.15 DAS
                WSim.Provider_Demand_Option = 4;
                for (int p = 0; p < 33; p++)
                {
                WSim.EnvWater_decisionGame_AF[p]=0;
                }
                //
                //WSim.Use_GPCD=WSim.GPCD_raw;
            }
              
        }
        //

        public void TempProcess(int year, WaterSimManager WSim)
        {
            WSim.UnLockSimulation();
            if (year < 2012)
            {
                if (year < 2002)
                {
                    defaults(WSim);
                }
            }
            initialize(WSim);
            ProcessTminus1(year, WSim);
            Process(year, WSim);
            WSim.LockSimulation();
        }

        //
        public override bool ProcessStarted(int year, WaterSimManagerClass WSim)
        {
            return base.ProcessStarted(year, WSim);
        }
        //
        public override bool PreProcess(int year, WaterSimManagerClass WSimClass)
        {
            WaterSimManager WSim = (WSimClass as WaterSimManager);

            WSim.UnLockSimulation();
            if (year < 2012)
            {
                if (year < 2002)
                {
                    defaults(WSim);
                }
            }
            initialize(WSim);
            ProcessTminus1(year, WSim);
            Process(year, WSim);
            WSim.LockSimulation();
             // ----------------------------------------  
            return base.PreProcess(year, WSim);
        }
      
        // ---------------------------------------------------
        public override bool PostProcess(int year, WaterSimManagerClass WSim)
        {

            return base.PostProcess(year, WSim);
        }
       
        // =============================================================
        

        protected double DayToYear(int year)
        {
            { return Leap(year); }
        }

        protected double Leap(int year)
        {
            double Lyear;
            if (IsLeapYear(year) == true)
            {
                Lyear = 366;
            }
            else
            {
                Lyear = 365;
            }
            return Lyear;
        }
        public static bool IsLeapYear(int year)
        {
            if (year % 4 != 0)
            {
                return false;
            }
            if (year % 100 == 0)
            {
                return (year % 400 == 0);
            }
            return true;
        }
 
    }
    //

    //
}
