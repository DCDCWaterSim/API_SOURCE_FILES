using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaterSimDCDC.Documentation;


namespace WaterSimDCDC
{
    public partial class WaterSimManager
    {
       // -------------------------------------------------------------------------------------------------------------------------------------------
       //-------------------------------------------------------------
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
       
        // This routine is called by initialize_ModelParameters
        // This is how User Defined Model Parameters are added to WaterSimManager Class Parameter Manager
        //   ExtendDoc.Add(new WaterSimDescripItem(eModelParam., "Description", "Short Unit", "Long Unit", "", new string[] { }, new int[] { }, new ModelParameterGroupClass[] { }));

         partial void initialize_Other_ModelParameters()
        {
            ParameterManagerClass FPM = ParamManager;
            Extended_Parameter_Documentation ExtendDoc = FPM.Extended;
            //
            // WaterSim 5 DOES NOT HAVE THIS CAPACITY
            StormWaterStructural_Capacity = new providerArrayProperty(_pm, mpProvider_StormWaterCapacity, get_StormWaterStructure_Capacity, set_StormWaterStructure_Capacity, eProviderAggregateMode.agSum);
            this.ParamManager.AddParameter(new ModelParameterClass(mpProvider_StormWaterCapacity, "Storm Water Structural Capacity m3", "SWCAP", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 24500, null, get_StormWaterStructure_Capacity, null, set_StormWaterStructure_Capacity, null, null, StormWaterStructural_Capacity));
             ExtendDoc.Add(new WaterSimDescripItem(mpProvider_StormWaterCapacity, "Maximum Capacity of a Commercial Storm Water Retention Unit", "m3", "cubic meters", "", new string[] { }, new int[] { }, new ModelParameterGroupClass[] { }));
            //
            //----------------------------------------------------------------------------------------------------
        }
        // WaterSim_6
         // Storm water potential storage
        /// <summary>
        /// 03.30.2016
        /// </summary>
        /// <returns></returns>
         internal int[] get_StormWaterStructure_Capacity() { return _ws.set_StormWaterCapacity; }
         internal void set_StormWaterStructure_Capacity(int[] value)
         {
             if (!FModelLocked) _ws.set_StormWaterCapacity = value;
         }
         /// <summary>Capacity of an individual Storm Water capture facility  </summary>
         /// <remarks>m3  </remarks>  
         /// <exception cref="WaterSim_Exception"></exception>
         ///
         public providerArrayProperty StormWaterStructural_Capacity;
      
        // stop
        // ---------------------------------------------------------------------------------------------------------
        //
    }
   
}
