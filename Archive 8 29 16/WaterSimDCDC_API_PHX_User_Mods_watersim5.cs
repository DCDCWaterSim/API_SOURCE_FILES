using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WaterSimDCDC
{
  
    public partial class WaterSimManager
    {
        //
        public int[] NetAg = new int[ProviderClass.NumberOfProviders];
        public int[] ProviderAgCAPandPumpingCurveIndex = new int[ProviderClass.NumberOfProviders];
        public int[] ProviderEnvIndex = new int[ProviderClass.NumberOfProviders];
        public int[] ProviderEnvCOdeltaAF = new int[ProviderClass.NumberOfProviders];
        //
        //09.17.14 DAS - RAY does not have this code as of 01.07.2015
        // --------------------------------------------------------------------------------------------------
        public bool _DecisonGame_2014 = false;
        public bool _COdeltaAZ = false;
        int DecisionGame2014 = 0;
        int[] PCT_Residential_decisionGame = new int[ProviderClass.NumberOfProviders];
        int[] PCT_Commercial_decisionGame = new int[ProviderClass.NumberOfProviders];
        int[] PCT_Industrial_decisionGame = new int[ProviderClass.NumberOfProviders];
        //
       // public bool _COdeltaBurdenToAZ = true;
        int COdeltaBurdenToAZint = 0;
        //------------------------------------------------------------------------
        int AlterGPCDall=100;
        //public partial class eModelParam
        //{
        //    public const int epSaltVerde_Annual_SurfaceDeliveries_SRP = 134;

        //}
        // Values 0 to 200 reserved for Basic Model Inpus and Outputs
        /// <summary> The maximum basic parameter. </summary>
        public const int MaxBasicParameter = 200;
        public const int mpProvider_StormWaterCapacity = 145;
        /// <summary>
        /// Creates a Directory of directoryName if it does not exist.  Relative references is from the directory the program is executing from.
        /// This is primarily here as a demonstration of how the WaterSimDECDC_User_Mods file can be used to add user methods and parameters to\
        /// the WaterSimManager class.  The User can add their own methods and parameters using this file. 
        /// </summary>
        /// <param name="directoryName">Name of new or existing directory</param>
        public void CreateDirectory(string directoryName)
        {
            try
            {
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(directoryName);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch
            {
            }
        }
        //---------------------------------------------------------------------------
        //
        static int[] PCT_Residential_default = new int[ProviderClass.NumberOfProviders];
        static int[] PCT_Commercial_default = new int[ProviderClass.NumberOfProviders];
        static int[] PCT_Industrial_default = new int[ProviderClass.NumberOfProviders];
        int[] GPCD_RAW = new int[ProviderClass.NumberOfProviders];
        //
        ProviderIntArray AgWaterIndex = new ProviderIntArray(0);
        int[] pct_agWaterCurveIndex = new int[ProviderClass.NumberOfProviders];
        int Static ;

        // This routine is called by initialize_ModelParameters
        // This is how User Defined Model Parameters are added to WaterSimManager Class Parameter Manager
         partial void initialize_Other_ModelParameters()
        {
             // these are not in Ray's code set as of 01.07.2015
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGame2014, "Decision Game 2014", "DECSNGAME", rangeChecktype.rctCheckRange, 0, 0, geti_DecisionGame2014, seti_DecisionGame2014, RangeCheck.NoSpecialBase));
            //
            PCT_residential_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameRes, get_DecisionGameRes, set_DecisionGameRes, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameRes, "Decision Game 2014 Res", "DGAMERES", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameRes, null, set_DecisionGameRes, null, null, PCT_residential_decisionGame));
            //
            PCT_commercial_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameCom, get_DecisionGameCom, set_DecisionGameCom, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameCom, "Decision Game 2014 Com", "DGAMECOM", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameCom, null, set_DecisionGameCom, null, null, PCT_commercial_decisionGame));
            //
            PCT_industrial_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameInd, get_DecisionGameInd, set_DecisionGameInd, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameInd, "Decision Game 2014 Ind", "DGAMEIND", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameInd, null, set_DecisionGameInd, null, null, PCT_industrial_decisionGame));
             //
            PCT_agWater_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisionGameAg, get_DecisionGameAg, set_DesisionGameAg, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisionGameAg, "Decision Game 2014 Ag water curve index", "DGAMEAG", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameAg, null, set_DesisionGameAg, null, null, PCT_agWater_decisionGame));
             //
            PCT_envWater_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisionGameEnv, get_DecisionGameEnv, set_DesisionGameEnv, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisionGameEnv, "Decision Game 2014 Env water curve index", "DGAMEENV", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameEnv, null, set_DesisionGameEnv, null, null, PCT_envWater_decisionGame));
             //
            EnvWater_decisionGame_AF = new providerArrayProperty(_pm, eModelParam.epDecisionGameEnvAF, get_DecisionGameEnvAF, set_DesisionGameEnvAF, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisionGameEnvAF, "Decision Game 2014 Env water AF to CO delta", "DGAMEENVAF", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameEnv, null, set_DesisionGameEnv, null, null, EnvWater_decisionGame_AF));
             //
            RATIO_envWater_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisionGameEnvRatio, get_DecisionGameEnvRatio, null, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisionGameEnvRatio, "Decision Game 2015 Env water CO delta ratio", "DGAMEDELTA", modelParamtype.mptInputProvider, rangeChecktype.rctNoRangeCheck, 0, 0, null, get_DecisionGameEnvRatio, null, null, null, null, RATIO_envWater_decisionGame));

             //
             PCT_residential_default = new providerArrayProperty(_pm, eModelParam.epRESdefault, get_residentialDefault, set_residentialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epRESdefault, "The default value for residential water use", "RESDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_residentialDefault, null, set_residentialDefault, null, null, PCT_residential_default));
             //
            PCT_commercial_default = new providerArrayProperty(_pm, eModelParam.epCOMdefault, get_commercialDefault,set_commercialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epCOMdefault, "The default value for residential water use", "COMDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_commercialDefault, null, set_commercialDefault, null, null, PCT_commercial_default));
            //
            PCT_industrial_default = new providerArrayProperty(_pm, eModelParam.epINDdefault, get_industrialDefault, set_industrialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epINDdefault, "The default value for residential water use", "INDDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_industrialDefault, null, set_industrialDefault, null, null, PCT_industrial_default));
            // Johnston et al. project
            SurfaceWater_to_Agriculture = new providerArrayProperty(_pm, eModelParam.epNewAgWaterFromSurface, get_surfaceWaterToAg, set_surfaceWaterToAg, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epNewAgWaterFromSurface, "Added Surface Water to Ag", "ADDAGSURF", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 500000, null, get_surfaceWaterToAg, null, set_surfaceWaterToAg, null, null, SurfaceWater_to_Agriculture));
             //
            Net_Agriculture_Water_AF = new providerArrayProperty(_pm, eModelParam.epNetAgWater, get_netAgWater, set_netAgWater, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epNetAgWater, "Net Water for Ag", "NETAGWATER", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100000, null, get_netAgWater, null, set_netAgWater, null, null, Net_Agriculture_Water_AF));
             //
             // 01.20.15 DAS
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epCOdeltaBurden, "Burden for AZ for CO delta Water from M&I budget", "CODELTAB", rangeChecktype.rctCheckRange, 0, 1, geti_COdeltaBurdenForAZ, seti_COdeltaBurdenForAZ, RangeCheck.NoSpecialBase));
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epCOdeltaRatioOfBurden, "Ratio of Burden for AZ for CO delta Water from M&I budget", "CODELTAR", rangeChecktype.rctCheckRange, 0, 0, geti_COdeltaBurdenRatioForAZ, seti_COdeltaBurdenRatioForAZ, RangeCheck.NoSpecialBase));
             //
            _pm.AddParameter(new ModelParameterClass(eModelParam.epBaseYear, "Base Year: current calendar", "BASEYR", rangeChecktype.rctCheckRange, 2000, 2020, geti_BaseYear, seti_BaseYear, RangeCheck.NoSpecialBase));

             // 01.21.15 DAS
            GPCD_Res = new providerArrayProperty(_pm, eModelParam.epProvider_GPCDres, get_GPCD_Res, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epProvider_GPCDres, "Residential GPCD", "GPCDRES", GPCD_Res, 0, 300));
             // 06.19.15
            GPCD_ComInd = new providerArrayProperty(_pm, eModelParam.epProvider_GPCDcomInd, get_GPCD_ComInd, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epProvider_GPCDcomInd, "Com and Ind GPCD", "GPCDCOMIND", GPCD_ComInd, 0, 500));

             // 01.19.15
            Gross_Agriculture_WaterPumped_AF = new providerArrayProperty(_pm, eModelParam.epProvider_AgWaterPumped, get_Gross_Ag_pumped, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epProvider_AgWaterPumped, "Agricultural Water Pumped", "AGPUMPED", Gross_Agriculture_WaterPumped_AF, 0, 400000));
            // 02.17.15
            PCT_AgWaterUsedToThresh = new providerArrayProperty(_pm, eModelParam.epProvider_AgWaterUsedvsThresh, get_AgWaterUsed_ratio, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epProvider_AgWaterUsedvsThresh, "Ag Water Used Relative to Threshold", "AGUSEDPCT", PCT_AgWaterUsedToThresh, 0, 100));
            //
            SaltVerde_Annual_SurfaceDeliveries_SRP = new providerArrayProperty(_pm, eModelParam.epSaltVerde_Annual_SurfaceDeliveries_SRP, get_SRP_SurfaceDeliveries, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Annual_SurfaceDeliveries_SRP, "Salt-Verde Surface Water", "SRPSURF", SaltVerde_Annual_SurfaceDeliveries_SRP, 0, 1000000));
            //
            //StormWaterStructural_Capacity = new providerArrayProperty(_pm, mpProvider_StormWaterCapacity, get_StormWaterStructure_Capacity,set_StormWaterStructure_Capacity, eProviderAggregateMode.agSum);
            //this.ParamManager.AddParameter(new ModelParameterClass(mpProvider_StormWaterCapacity, "Storm Water Structural Capacity m3", "SWCAP", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 24500, null,get_StormWaterStructure_Capacity, null,set_StormWaterStructure_Capacity, null, null, StormWaterStructural_Capacity));
            //
             //
            // added here pn 05.12.14 - tell Ray and check to make certain no ill effects on his code
            initialize_Provider_Default_ModelParameters();
            //
             // Default - Initialize the Decision Game variables
             for(int p = 0; p < ProviderClass.NumberOfProviders; p++){
                 PCT_residential_default[p] = PCT_WaterSupply_to_Residential.getvalues().Values[p];
                 PCT_commercial_default[p] = PCT_WaterSupply_to_Commercial.getvalues().Values[p];
                 PCT_industrial_default[p] = PCT_WaterSupply_to_Industrial.getvalues().Values[p];
                 //
                 this.PCT_residential_decisionGame[p] = PCT_residential_default[p];
                 this.PCT_commercial_decisionGame[p] =  PCT_commercial_default[p] ;
                 this.PCT_industrial_decisionGame[p] = PCT_industrial_default[p];
                 this.PCT_agWater_decisionGame[p] = 0;
                 this.PCT_envWater_decisionGame[p] = 0;
                // this.Decision_Game_2014 = false;
                 seti_DecisionGame2014(0);
                 this.COdeltaBurdenToAZ = false;
            }
           
             //
            //----------------------------------------------------------------------------------------------------
        }
         // Storm water potential storage
        /// <summary>
        /// 03.30.2016
        /// </summary>
        /// <returns></returns>
         //internal int[] get_StormWaterStructure_Capacity() { return _ws.set_StormWaterCapacity; }
         //internal void set_StormWaterStructure_Capacity(int[] value)
         //{     
         //    if (!FModelLocked) _ws.set_StormWaterCapacity = value;
         //}

         /// <summary>Capacity of an individual Storm Water capture facility  </summary>
         /// <remarks>m3  </remarks>  
         /// <exception cref="WaterSim_Exception"></exception>
         ///
         public providerArrayProperty StormWaterStructural_Capacity;

         #region DecisionGame
         public bool Decision_Game_2014
         {
             set
             {
                 if ((!_inRun) & (!FModelLocked))
                 {
                     _DecisonGame_2014 = value;
                     if (_DecisonGame_2014)
                     {
                         _ws.set_modelDecisionGame = true;
                         //
                         string game = "DecisionGame";
                         AnnualFeedbackProcess AFP;
                         AFP = this.ProcessManager.Find(game);
                            if (AFP == null)
                             {
                                 bool quiet = true;
                                 AnnualFeedbackProcess SP = new DecisionGame(game, this, quiet);
                                 this.ProcessManager.AddProcess(SP);
                             }
                      }
                 }
             }
             get { return _DecisonGame_2014; }

         }
         private int geti_DecisionGame2014() { return Convert.ToInt32(Decision_Game_2014); }
         private void seti_DecisionGame2014(int value)
         {
             DecisionGame2014 = value;
         

             bool test = Convert.ToBoolean(DecisionGame2014);
             Decision_Game_2014 = test;
             //Decision_Game_2014 = Convert.ToBoolean(DecisionGame2014);
         }
         
         public providerArrayProperty PCT_residential_decisionGame;
         internal void set_DecisionGameRes(int[] value)
         { if (!FModelLocked) PCT_Residential_decisionGame = value; }
         internal int[] get_DecisionGameRes() { return PCT_Residential_decisionGame; }
         //
         public providerArrayProperty PCT_commercial_decisionGame;
         internal void set_DecisionGameCom(int[] value)
         { if (!FModelLocked) PCT_Commercial_decisionGame = value; }
         internal int[] get_DecisionGameCom() { return PCT_Commercial_decisionGame; }
         //
         public providerArrayProperty PCT_industrial_decisionGame;
         internal void set_DecisionGameInd(int[] value)
         { if (!FModelLocked) PCT_Industrial_decisionGame = value; }
         internal int[] get_DecisionGameInd() { return PCT_Industrial_decisionGame; }
        /// <summary>
        /// 
        /// </summary>
        //    02.05.15 DAS
        //    This is fixed in the model- users supply between 0 and 100, and the model
        // converts it to 10 to 0 (as an index that the model is expecting)
        // That is, a value of 10 is the baseline (100%) of projected use. Users of the
        // Decision Game can leave it at default, or increase water to Agriculture.
        // see subroutine setProviderAgCreditCurvePCT(count, values) for specs
        // located in KernelInterface.f90
        //
         // Default is set by Web_AgricultureTransferToMuni_PCT
         // -----------------------------------------------------------------------------------------
        //
         int[] MyOut = new int[33];
         int[] pop = new int[33];
         int[] gpcd = new int[33];

         public providerArrayProperty PCT_agWater_decisionGame;
         ProviderIntArray demand = new ProviderIntArray(0);
         internal void set_DesisionGameAg(int[] value)
         {
          
             if (!FModelLocked)
                 DecisionGameAg(value, MyOut);
            _ws.set_ProviderAgCAPandPumpingCurveIndex = MyOut;
            ProviderAgCAPandPumpingCurveIndex = value;
         }
         internal int[] get_DecisionGameAg() { return _ws.get_AgWaterUsedRelativeTo2014_PCT; }
              
                  //ProviderAgCAPandPumpingCurveIndex; }
              internal int[] AgMaxS()
              {
                  return _ws.get_WaterFromAgSurfaceMax ;
              }
              internal int[] AgMaxP()
              {
                  return _ws.get_WaterFromAgPumpingMax;
              }
              internal int[] TotalDemand()
              {
                  return _ws.get_WaterDemand;
              }
               const int defaultAg = 10;
              
               double[] proportion = new double[ProviderClass.NumberOfProviders];
               double[] AgToGet = new double[ProviderClass.NumberOfProviders];
               double[] AgDifference = new double[ProviderClass.NumberOfProviders];
               double[] demT = new double[ProviderClass.NumberOfProviders];
               int[] MaxS = new int[ProviderClass.NumberOfProviders];
               int[] MaxP = new int[ProviderClass.NumberOfProviders];
               int[] DemandT = new int[ProviderClass.NumberOfProviders];
               double[] Agtotal = new double[ProviderClass.NumberOfProviders];
               ProviderIntArray Dem = new ProviderIntArray(0);
             public void DecisionGameAg(int[] value,int[] Rout){
                    const double slope = -0.1;
                    const int NewIntercept = defaultAg;
                         MaxS = AgMaxS();
                         MaxP = AgMaxP();
                         DemandT = TotalDemand();
                         for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
                       {
                           Agtotal[i] = Convert.ToDouble(MaxS[i]);
                           Agtotal[i]+=Convert.ToDouble(MaxP[i]);
                           demT[i] = Convert.ToDouble(DemandT[i]);
                        }
                 _ws.set_AgCreditTransferPCT = 80;
                 for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
                 {
                     // retain any values previously created for another provider
                     if (0 < value[p])
                     {
                         proportion[p] = 0.01 * value[p] * demT[p];
                         if (0 < Agtotal[p])
                         {
                             if (0 < value[p])
                             {
                                 AgToGet[p] = Math.Abs((1 - ((Agtotal[p] + proportion[p]) / Agtotal[p])) * 100);
                             }
                         }
                         AgDifference[p] = AgToGet[p] - 100;
                         if (AgToGet[p] <= 100)
                         {
                             Rout[p] = Convert.ToInt32(slope * AgToGet[p] + NewIntercept);
                         }
                         else {
                             Rout[p] = 0;
                         }
                     }
                     else
                     {
                         if (AgToGet[p] == 0)
                         {
                             
                             Rout[p] = 10;
                         }
                     }

                  
                 }
         }
        //
        /// <summary>
         /// The Provider equilivant to the   protected internal int set_FlowToCOdelta
        /// </summary>
       //  public ProviderIntArray EnvWater_decisionGame_AF = new ProviderIntArray(0);
        /// <summary>
        /// PCT passed in by the Decision Game players
        /// </summary>
         public providerArrayProperty PCT_envWater_decisionGame ;
         internal void set_DesisionGameEnv(int[] value)
         {
             if (!FModelLocked)           
               ProviderEnvIndex = value;
         
         }
         internal int[] get_DecisionGameEnv() { return ProviderEnvIndex; }
         //            
        int[] DGenv = new int[ProviderClass.NumberOfProviders];
          public providerArrayProperty EnvWater_decisionGame_AF;
         internal void set_DesisionGameEnvAF(int[] value)
         {
             if (!FModelLocked)
                 DGenv = value;
                 //Web_FlowForEnvironment_AF =
               _ws.set_ProviderFlowToCOdelta = value;
             ProviderEnvCOdeltaAF = value;
         }
         internal int[] get_DecisionGameEnvAF() { return ProviderEnvCOdeltaAF; }

        /// <summary>
        /// Net after evap and percolation
        /// </summary>
         public providerArrayProperty  Net_Agriculture_Water_AF ;
         internal void set_netAgWater(int[] value)
         { if (!FModelLocked) NetAg = value; }
         internal int[] get_netAgWater() { return _ws.get_NetAgricultureWater_AF; }


         public providerArrayProperty RATIO_envWater_decisionGame;

         internal int[] get_DecisionGameEnvRatio() {
             
             return _ws.get_ProviderCOdeltaRatio; }


         // --------------------------------------------------------------------------------------------------------------------------



         //
         public providerArrayProperty SurfaceWater_to_Agriculture;
         internal void set_surfaceWaterToAg(int[] value)
         { if (!FModelLocked) _ws.WaterToAgriculture_AF = value; }
         internal int[] get_surfaceWaterToAg() { return _ws.WaterToAgriculture_AF; } 
         // --------------------------------------------------------------------------------------------------------------------------
           
         public providerArrayProperty PCT_residential_default;
         internal int[] get_residentialDefault()
         { return PCT_Residential_default; }
         internal void set_residentialDefault(int[] value)
         {  if (!FModelLocked) 
             PCT_Residential_default = value; }

        // ====================================
         public providerArrayProperty PCT_commercial_default;
         internal int[] get_commercialDefault()
         { return PCT_Commercial_default; }
         internal void set_commercialDefault(int[] value)
         {
             if (!FModelLocked) PCT_Commercial_default = value;
         }
        // ====================================
         public providerArrayProperty PCT_industrial_default;
         internal int[] get_industrialDefault()
         { return PCT_Industrial_default; }
         internal void set_industrialDefault(int[] value)
         { if (!FModelLocked) PCT_Industrial_default = value;
         }
        #endregion
         // 01.20.15 DAS ----------------------------------------------------------------------------------------
         private int geti_COdeltaBurdenForAZ() { return Convert.ToInt32(COdeltaBurdenToAZ); }
         private void  seti_COdeltaBurdenForAZ(int value)
         {
             COdeltaBurdenToAZint = value;
             COdeltaBurdenToAZ = Convert.ToBoolean(COdeltaBurdenToAZint);
         }
         public bool COdeltaBurdenToAZ
         {
             set
             {
                 _ws.set_COdeltaBurden = value;
                 _COdeltaAZ = value;
             }
             get
             {
                 return _COdeltaAZ;
             }
         }
        /// <summary>
        ///  01.20.15 DAS
        ///  This is the Sustainability Indicator for the Environmental SI for CO river delta water
         ///  from AZ CAP MandI water and implications based on COdeltaBurdenToAZ
        /// </summary>
          private int geti_COdeltaBurdenRatioForAZ() { return COdeltaBurdenRatioForAZ; }
         private void seti_COdeltaBurdenRatioForAZ(int value)
         {
             COdeltaBurdenRatioForAZ = value;
         }
         public int COdeltaBurdenRatioForAZ
         {
             set
             {
                 Static= value;
             }
             get
             {
                 return _ws.get_COdeltaRatio;
             }
         }
        // DAS 02.04.16 
         private int geti_BaseYear() { return _BaseYear; }

         private void seti_BaseYear(int value) { BaseYear = value; }

         public int BaseYear
         {
             set
             {
                 _BaseYear = value;
             }
             get
             {
                 return _BaseYear;
             }
         }





        //
        // Provider level Agriculture Water used divided by threshold (now, 2014 as basis)
        // or, cummulative credit transfer (100 minus) to municipal water users (100% is 100% ag use)
         private int[] get_AgWaterUsed_ratio()
         { return _ws.get_AgWaterUsedRelativeTo2014_PCT; }

         /// <summary> The gpcd used </summary>
         ///<remarks>The GPCD used to estimate demand for the completed simulation year. When Provider_Demand_Option is 1,2, or 3, this is the calculated GPCD used to estimate demand.  
         ///		   if the Provider_Demand_Option =4, this is the GPCD specified by Use_GPCD parameter. Units: gpcd-gallons per capita per daya of water use</remarks>
         /// <seealso cref="Provider_Demand_Option"/>
         /// <seealso cref="Use_GPCD"/>
         public providerArrayProperty PCT_AgWaterUsedToThresh;


         //---------------------------------------
         //ProviderGPCD residential
        // 01.21.15 DAS
         private int[] get_GPCD_Res()
         { return _ws.get_ProviderGPCDres; }

         /// <summary> The gpcd used </summary>
         ///<remarks>The GPCD used to estimate demand for the completed simulation year. When Provider_Demand_Option is 1,2, or 3, this is the calculated GPCD used to estimate demand.  
         ///		   if the Provider_Demand_Option =4, this is the GPCD specified by Use_GPCD parameter. Units: gpcd-gallons per capita per daya of water use</remarks>
         /// <seealso cref="Provider_Demand_Option"/>
         /// <seealso cref="Use_GPCD"/>
         public providerArrayProperty GPCD_Res;

         //---------------------------------------
         int[] GPCD_Difference = new int[ProviderClass.NumberOfProviders];

         //ProviderGPCD commercial & Industrial
         // 06.19.15 DAS
         private int[] get_GPCD_ComInd()
         {
             for (int p = 0; p < ProviderClass.NumberOfProviders; p++) { 
              GPCD_Difference[p]=_ws.ProviderGPCD[p] - _ws.get_ProviderGPCDres[p];
             }
             return GPCD_Difference;
         }

         /// <summary> The gpcd used </summary>
         ///<remarks>The GPCD used to estimate demand for the completed simulation year. When Provider_Demand_Option is 1,2, or 3, this is the calculated GPCD used to estimate demand.  
         ///		   if the Provider_Demand_Option =4, this is the GPCD specified by Use_GPCD parameter. Units: gpcd-gallons per capita per daya of water use</remarks>
         /// <seealso cref="Provider_Demand_Option"/>
         /// <seealso cref="Use_GPCD"/>
         public providerArrayProperty GPCD_ComInd;



         //---------------------------------------
         //Gross Ag water Pumped
         // 01.29.15 DAS
         private int[] get_Gross_Ag_pumped()
         { return _ws.get_GrossAgricultureWaterPumped_AF; }

         /// <summary> The pumped agriculture water </summary>
         ///<remarks>Depends on the  Web_AgricultureTransferToMuni_PCT variable which
         /// controls the  _ws.set_AgPumpingCurveIndex. The index sets the curve for Ag pumping
         /// See the index for more info
         /// </remarks>
         /// <seealso cref="Provider_Demand_Option"/>
         /// <seealso cref="Use_GPCD"/>
         public providerArrayProperty Gross_Agriculture_WaterPumped_AF;


         int[] SRP_pump = new int[ProviderClass.NumberOfProviders];
         int[] SRP_total = new int[ProviderClass.NumberOfProviders];
         int[] SRP_surf = new int[ProviderClass.NumberOfProviders];
      
         public providerArrayProperty SaltVerde_Annual_SurfaceDeliveries_SRP;
         private int[] get_SRP_SurfaceDeliveries()
         {
             for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
             {
                 SRP_pump[p] = _ws.get_ClassBCpumpedSRP[p];
                 SRP_total[p] = _ws.get_SVTAnnualDeliveriesSRP[p];
                 SRP_surf[p] = Math.Max(0,SRP_total[p] - SRP_pump[p]);
                
             }
          
            // return SRP_surf;
             return SRP_surf;
         }

         /// <summary> The salt verde annual deliveries srp </summary>
         ///<remarks>The total annual surface water and pumped groundwater delivered by SRP. Units AF</remarks>
         /// <seealso cref="Groundwater_Bank_Used"/>
         /// 
         // ProviderIntArray alterGPCD = new ProviderIntArray(0);
       
         //private void seti_AlterAllGPCD(int value) {

         //    AlterAllGPCD = value;
         //}
         //private int geti_AlterAllGPCD() { return AlterAllGPCD; }
         //public int AlterAllGPCD
         //{
         //    set{
         //        if ((!_inRun) & (!FModelLocked))
         //        {
         //            _pm.CheckBaseValueRange(eModelParam.epAlterGPCDall, value);
         //            AlterGPCDall = value;
         //            for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
         //            {
         //                alterGPCD[p] = value;
         //            }
         //            PCT_alter_GPCD.setvalues(alterGPCD);
         //            //_ws.AlterProviderGPCDpct = alterGPCD.Values();
         //        }
         //    }
         //    get
         //    {
         //        return AlterGPCDall;
         //    }
         //}
       

         
        // stop
        // ---------------------------------------------------------------------------------------------------------




        //
    }
   
}
