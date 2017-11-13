using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WaterSimDCDC
{
  
    public partial class WaterSimManager
    {
         public int[] NetAg = new int[ProviderClass.NumberOfProviders];
        //09.17.14 DAS
        public bool _DecisonGame_2014 = false;
       int DecisionGame2014 = 0;
       int[] PCT_Residential_decisionGame = new int[ProviderClass.NumberOfProviders];
       int[] PCT_Commercial_decisionGame = new int[ProviderClass.NumberOfProviders];
       int[] PCT_Industrial_decisionGame = new int[ProviderClass.NumberOfProviders];

         //
        //------------------------------------------------------------------------
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
        int[] PCT_Residential_default = new int[ProviderClass.NumberOfProviders];
        int[] PCT_Commercial_default = new int[ProviderClass.NumberOfProviders];
        int[] PCT_Industrial_default = new int[ProviderClass.NumberOfProviders];
        //
        //ProviderIntArray DGtemp = new ProviderIntArray(0);

        // This routine is called by initialize_ModelParameters
        // This is how User Defined Model Parameters are added to WaterSimManager Class Parameter Manager
         partial void initialize_Other_ModelParameters()
        {
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGame2014, "Decision Game 2014", "DECSNGAME", rangeChecktype.rctCheckRange, 0, 0, geti_DecisionGame2014,seti_DecisionGame2014, RangeCheck.NoSpecialBase));
             //
             PCT_residential_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameRes, get_DecisionGameRes, set_DecisionGameRes, eProviderAggregateMode.agSum);
             this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameRes, "Decision Game 2014 Res", "DGAMERES", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRangeSpecial, 0, 0, null, get_DecisionGameRes, null, set_DecisionGameRes, null, null, PCT_residential_decisionGame));
             //
             PCT_commercial_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameCom, get_DecisionGameCom, set_DecisionGameCom, eProviderAggregateMode.agSum);
             this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameCom, "Decision Game 2014 Com", "DGAMECOM", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRangeSpecial, 0, 0, null, get_DecisionGameCom, null, set_DecisionGameCom, null, null, PCT_commercial_decisionGame));
            //
             PCT_industrial_decisionGame = new providerArrayProperty(_pm, eModelParam.epDecisonGameInd, get_DecisionGameInd, set_DecisionGameInd, eProviderAggregateMode.agSum);
             this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epDecisonGameInd, "Decision Game 2014 Ind", "DGAMEIND", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRangeSpecial, 0, 0, null, get_DecisionGameInd, null, set_DecisionGameInd, null, null, PCT_industrial_decisionGame));
            //
              PCT_residential_default = new providerArrayProperty(_pm, eModelParam.epRESdefault, get_residentialDefault, set_residentialDefault, eProviderAggregateMode.agWeighted);
           this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epRESdefault, "The default value for residential water use", "RESDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_residentialDefault, null, set_residentialDefault, null, null, PCT_residential_default));
            //
            PCT_commercial_default = new providerArrayProperty(_pm, eModelParam.epCOMdefault, get_commercialDefault,set_commercialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epCOMdefault, "The default value for residential water use", "COMDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_commercialDefault, null, set_commercialDefault, null, null, PCT_commercial_default));
            //
            PCT_industrial_default = new providerArrayProperty(_pm, eModelParam.epINDdefault, get_industrialDefault, set_industrialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epINDdefault, "The default value for residential water use", "INDDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_industrialDefault, null, set_industrialDefault, null, null, PCT_industrial_default));
            //
            SurfaceWater_to_Agriculture = new providerArrayProperty(_pm, eModelParam.epNewAgWaterFromSurface, get_surfaceWaterToAg, set_surfaceWaterToAg, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epNewAgWaterFromSurface, "Added Surface Water to Ag", "ADDAGSURF", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 500000, null, get_surfaceWaterToAg, null, set_surfaceWaterToAg, null, null, SurfaceWater_to_Agriculture));
             //
            Net_Agriculture_Water_AF = new providerArrayProperty(_pm, eModelParam.epNetAgWater, get_netAgWater, set_netAgWater, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epNetAgWater, "Net Water for Ag", "NETAGWATER", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100000, null, get_netAgWater, null, set_netAgWater, null, null, Net_Agriculture_Water_AF));

            // added here pn 05.12.14 - tell Ray and check to make certain no ill effects on his code

            initialize_Provider_Default_ModelParameters();

                 // Create, in memory, default values for the SpecialProjects.cs file
                for (int m = 0; m < 33; m++)
                {
                    PCT_Residential_default[m] = PCT_WaterSupply_to_Residential.getvalues().Values[m];
                    PCT_Commercial_default[m] = PCT_WaterSupply_to_Commercial.getvalues().Values[m];
                    PCT_Industrial_default[m] = PCT_WaterSupply_to_Industrial.getvalues().Values[m];
                    //
                    PCT_Residential_decisionGame[m] = 50;
                    PCT_Commercial_decisionGame[m] = 30;
                    PCT_Industrial_decisionGame[m] = 20;
  
                }
                //DGtemp.Values = PCT_Residential_decisionGame;
                //PCT_residential_decisionGame.setvalues(DGtemp);

         }

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
                             string game = "DecisionGame";
                             AnnualFeedbackProcess AFP;
                             AFP = this.ProcessManager.Find(game);
                             try
                             {
                                 if (AFP == null)
                                 {
                                     bool quiet = true;
                                     AnnualFeedbackProcess SP = new DecisionGame(game, this, quiet);
                                     this.ProcessManager.AddProcess(SP);
                                 }
                                 else
                                 {
                                     if (this.ProcessManager.FindindexByClassname(game) == -1) 
                                     {

                                     }
                                 }
                             }
                             finally
                             {
                                 if (AFP != null)
                                 {
                                     ((IDisposable)AFP).Dispose();
                                 }
                             }
                         }
                     }
                 }
                 get { return _DecisonGame_2014; }

             }
             private int geti_DecisionGame2014() { return Convert.ToInt32( Decision_Game_2014); }
             private void seti_DecisionGame2014(int value) { DecisionGame2014 = value;
             Decision_Game_2014 = Convert.ToBoolean(DecisionGame2014);
             }  
            //
             public providerArrayProperty PCT_residential_decisionGame;
             internal void set_DecisionGameRes(int[] value)
             { if (!FModelLocked) PCT_Residential_decisionGame = value; }
             internal int[] get_DecisionGameRes() { return PCT_Residential_decisionGame; }
              //
             public providerArrayProperty PCT_commercial_decisionGame;
             internal void set_DecisionGameCom(int[] value)
             { if (!FModelLocked) PCT_Commercial_decisionGame= value; }
             internal int[] get_DecisionGameCom() { return PCT_Commercial_decisionGame; }
            //
             public providerArrayProperty PCT_industrial_decisionGame;
             internal void set_DecisionGameInd(int[] value)
             { if (!FModelLocked) PCT_Industrial_decisionGame = value; }
             internal int[] get_DecisionGameInd() { return PCT_Industrial_decisionGame; }
            //
             public providerArrayProperty  Net_Agriculture_Water_AF ;
             internal void set_netAgWater(int[] value)
             { if (!FModelLocked) NetAg = value; }
             internal int[] get_netAgWater() { return _ws.get_NetAgricultureWater_AF; }
             // --------------------------------------------------------------------------------------------------------------------------
             //
             public providerArrayProperty SurfaceWater_to_Agriculture;
             internal void set_surfaceWaterToAg(int[] value)
             { if (!FModelLocked) _ws.WaterToAgriculture_AF = value; }
             internal int[] get_surfaceWaterToAg() { return _ws.WaterToAgriculture_AF; } 
             // --------------------------------------------------------------------------------------------------------------------------
           
             public providerArrayProperty PCT_residential_default;
             internal int[] get_residentialDefault()
             { return  PCT_Residential_default; }
             internal void set_residentialDefault(int[] value)
             {  if (!FModelLocked) PCT_Residential_default = value; }

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

           


    }
     
}
