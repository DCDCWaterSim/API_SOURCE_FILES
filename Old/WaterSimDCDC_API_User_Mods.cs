using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace WaterSimDCDC
{
  
    public partial class WaterSimManager
    {
        private int PCT_FlowForEnviron_memory = 0;

       
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
       
        // This routine is called by initialize_ModelParameters
        // This is how User Defined Model Parameters are added to WaterSimManager Class Parameter Manager
         partial void initialize_Other_ModelParameters()
        {
            PCT_residential_default = new providerArrayProperty(_pm, eModelParam.epRESdefault, get_residentialDefault, set_residentialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epRESdefault, "The default value for residential water use", "RESDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_residentialDefault, null, set_residentialDefault, null, null, PCT_residential_default));
             //
            PCT_commercial_default = new providerArrayProperty(_pm, eModelParam.epCOMdefault, get_commercialDefault,set_commercialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epCOMdefault, "The default value for residential water use", "COMDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_commercialDefault, null, set_commercialDefault, null, null, PCT_commercial_default));
            //
            PCT_industrial_default = new providerArrayProperty(_pm, eModelParam.epINDdefault, get_industrialDefault, set_industrialDefault, eProviderAggregateMode.agWeighted);
            this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epINDdefault, "The default value for residential water use", "INDDEFLT", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 100, null, get_industrialDefault, null, set_industrialDefault, null, null, PCT_industrial_default));
            //
 

            // This method is defined in WaterSimDCDC_API_WebInterface.cs
            //initialize_WebInterface_DerivedParameters();
            //----------------------------------------------------------------------------------------------------
        }
         //
         //public providerArrayProperty PCT_alter_GPCD;
         //internal void set_alterGPCDpct(int[] value) { if (!FModelLocked)  _ws.AlterProviderGPCDpct = value; }
         //internal int[] get_alterGPCDpct() {return _ws.AlterProviderGPCDpct ; }
         // --------------------------------------------------------------------------------------------------------------------------
         int[] PCT_Residential_default = new int[ProviderClass.NumberOfProviders];
         int[] PCT_Commercial_default = new int[ProviderClass.NumberOfProviders];
         int[] PCT_Industrial_default = new int[ProviderClass.NumberOfProviders];
             

         public providerArrayProperty PCT_residential_default;
         internal int[] get_residentialDefault()
         {  return PCT_Residential_default;  }

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
         {  return PCT_Industrial_default; }
         internal void set_industrialDefault(int[] value)
         { if (!FModelLocked) PCT_Industrial_default = value;
         }

    }
   
}
