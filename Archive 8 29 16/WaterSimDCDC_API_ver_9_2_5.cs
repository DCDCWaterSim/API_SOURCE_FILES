       //      WaterSimDCDC Regional Water Demand and Supply Model Version 5.0
       
       //       This is the C# API wrapper for the C# interface to the Fortran Model.
       
       //       Version 9.2.3
       //       3/15/14
       //       Based on Model_interface.cs version "03.16.13_1:45:00";

       //       Keeper: Ray Quay ray.quay@asu.edu
       //       
       //       Copyright (C) 2011,2012.2013 , The Arizona Board of Regents
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
       // NOTES
       //   9.0.6 Fixed WebPop parameter bug 
       //   9.0.7 Fixed Regional Value Bug for Input Parameters
       //   9.0.8 Fixed Stopyr bug, if set for one run and not set in next run, did not use default 2085 used value from previous run
       //====================================================================================
       using System;
       using System.Collections.Generic;
       using System.Text;
#if ExtendedParameter
       using WaterSimDCDC.Documentation;
#endif       
       
       /**************************************************
        * WaterSimDCDC_API
        * Version 4.1
        * 7/24/12
        * Keeper Ray Quay
        * ************************************************/
       namespace WaterSimDCDC
       {
           public class WaterSimManagerClass : IDisposable
           {
               static protected bool _isWaterSimInstatiated = false;  // used to keep track if a WAterSimManager object has been constructed

               public WaterSimManagerClass()
               {
                   // Some Basic Tests
                   if (_isWaterSimInstatiated) throw new WaterSim_Exception(WS_Strings.wsOnlyOneObject);
                   _isWaterSimInstatiated = true;
               }

               protected virtual void Dispose(bool disposing)
               {
                   WaterSimManager._isWaterSimInstatiated = false;
                   if (disposing)
                   {
                   }
               }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            /// <summary>
            /// Tests if a WaterSimManager Object can be instantiated
            /// </summary>
            /// <returns> Trur if new object can be instantiated, false of an object already exists</returns>
            static public bool isReadyToInstantiate
            {
                get { return (!_isWaterSimInstatiated); }
            }


           }

           /// <summary>
           ///  This is the root class for interface with the WaterSimManager model.
           /// </summary>
           public partial class WaterSimManager : WaterSimManagerClass
           {
               #region Fields_Constructor_Base_Methods
               // General Internal Constants
               const string _APIVersion = "9.2.4" ;  // latest version of API
               const int _defaultEndYear = 2001;  // Used to Set Default Start Year
               // 
               // private fields
               string _TempDirectoryName;
               string _DataDirectoryName;
               
               int _StartYear = 0;
               int _EndYear = 0;                   // used to keep track of start and end year for run routines Available for new classes should not be exposed as public
               int _CurrentYear = 0;               // used to keep track of years inside run routines
               int _NumberOfYears = 0;             // Used to keep track of the total years for simulation
               bool _inRun = false;                // used to keep track if runs have started
               bool _simulationStarted = false;    // used to keep track if Simulation has been initialized  False means not yet or stopped
               bool FModelLocked = false;                   // used to keep track if model is locked
               string _ModelBuild = "";
               string _TimeStamp = "";
               public static bool _FortranOutput = false;           // used to set Fortran debug mode

               static bool FSuspendRangeCheck = false;

               // KLUDGE FOR USE GPCD
               int[] GPCDDefaults = new int[ProviderClass.NumberOfProviders];
               int[] GPCDBuffer = new int[ProviderClass.NumberOfProviders];
       
               // STORAGE FOR SET POPULATION
               // **** Changed 8 13 12 int[] SetPopultaionsValues = new int[ProviderClass.NumberOfProviders];
               int[] SetPopultaionsValuesOn = new int[ProviderClass.NumberOfProviders];
               int[] SetPopultaionsValuesOther = new int[ProviderClass.NumberOfProviders];
               // **** 
               // 
               // STORAGE FOR Wateraugmentation
               // **** Added on 03.15.14
               // DAS
               int[] WaterAugmentationMemory = new int[ProviderClass.NumberOfProviders];

       
               internal ParameterManagerClass _pm;
               internal ProcessManager _ProcessManager;
               internal ProcessRegistry _ProcessRegistry;
       
               internal WaterSimU _ws;  // this is the interface to fortran model
       
               //----------------------------------------------------------------------------------------------------
               // Constructor
               /// <summary>
               ///  Constructor - Loads DLL (if not yet loaded) and initializes the model, only one instance of this class can be created.  Will throw an exception if more than one is created 
               /// </summary>
               /// <param name="TempDirectoryName">This is where the model places its output files, Creates a diectoy (TempDirectoryName) if it does not exist</param>
               /// <param name="DataDirectoryName">Location of data</param>
               /// <exception cref="WaterSim_Exception"> If an object has already been instantiated</exception>
               /// <exception cref="WaterSim_Exception">if the data directory is invalid</exception>
               /// <exception cref="WaterSim_Exception">Internal Error - If Model Parameters as not initialized properly </exception>
               
               public WaterSimManager(string DataDirectoryName, string TempDirectoryName
                   ) : base()
               {
            
                   if (!TestDataDirectory(DataDirectoryName)) throw new WaterSim_Exception(WS_Strings.wsBadDataDirectory);
       
                   // Ok Get started with create
                   UniDB.FileSupport.CreateDirectory(TempDirectoryName);
                   // does not create if alread exists
                   _TempDirectoryName = TempDirectoryName;
                   _DataDirectoryName = DataDirectoryName;
                   // Instantiate WaterSimU
                   _ws = new WaterSimU(_DataDirectoryName, _TempDirectoryName, _FortranOutput);
                   // Get version
                   _ModelBuild = _ws.get_Build + " " + _ws.get_TestVersion;
                   _TimeStamp = _ws.get_TestVersion;
                   _ModelBuild = _ws.get_Build;
       
       
                   // Setup Parameter Manager and Model Paramters
                   // Create a ParameterManager
                   _pm = new ParameterManagerClass(APiVersion, _ModelBuild);
                   // Ok create Process Manager
                   _ProcessManager = new ProcessManager();
                   // OK create a Process Registry
                   _ProcessRegistry = new ProcessRegistry();
                   
                   // Initialize all of the Model Parameters
                   initialize_ModelParameters();
                   
                   
                   // now test that they are all there
                   string TestMsg = "";
                   if (!testParmeters(ref TestMsg, false)) throw new WaterSim_Exception(WS_Strings.wsModelParameterMissing + " " + TestMsg);
                   // Add any parameter Processes
       
                   // Last thing before setup  Call the GPCD Fix  
                   GPCDFIX();
       
                   InitDefaultVariables();  // Sets Variables to Default Values
                 }
       
               //----------------------------------------------------------------------------------------------------
               // finalizer
       
               /// <summary>   Finaliser. </summary>
               /// <remarks>Do not want finalizer disposing of orphaned WaterSim_model.dll stuff</remarks>
               ~WaterSimManager()
               {
                   Dispose(false);
               }
               //------------------------------------------------------------------------------------------
               /// <summary>
               /// Called by Dispose() and ~WaterSimManager() Finalizer  Prevents Finalizer from closing model files.
               /// </summary>
               /// <param name="disposing">Finalizer will call with a false</param>
               protected override void Dispose(bool disposing)
               {
                   if (disposing)
                   {
                       WaterSimManager._isWaterSimInstatiated = false;
                       //  This is now done by WaterSimU class   _ws.CloseFiles();
                       _ws.Dispose();
                       
       
                   }
                   // Get rid of unmanged stuff, like dbConnections
                   base.Dispose(disposing);
               }
               //------------------------------------------------------------------------------------------
              /// <summary>
              /// Must be called before WaterSim object loses scope or is lost (reassigned).
              /// </summary>
               public new void Dispose()
               {
                   Dispose(true);
                   GC.SuppressFinalize(this);
               }
               /*****************************************************
                * General Properties
                * 
             
                * ***************************************************/
               // DAS September 2014
               /// <summary>
               /// Retains the core WaterSim properties of WaterSim_U - FORTRAN
               /// </summary>
               public WaterSimU DeepWaterSim
               {
                   get { return _ws; }
               }
               /// <summary>
               /// Exposes to inherited classes for read only the private StartYear field (int) that tracks the Start Year during Simulation Runs
               /// </summary>
               protected int Sim_StartYear
               { get { return _StartYear; } }
               
               // ------------------------------------------- 
               /// <summary>
               /// Exposes to inherited class for read only the private EndYear field (int) that tracks the End Year during Simulation Runs
               /// </summary>
               protected int Sim_EndYear
               { get { return _EndYear; } }
               
               // ------------------------------------------- 
               /// <summary>
               /// Exposes to inherited class for read only the private CurrentYear field (int) that tracks the Current Year during Simulation Runs
               /// </summary>
               protected int Sim_CurrentYear
               { get { return _CurrentYear; } }
               
               // ------------------------------------------- 
               /// <summary>
               /// Provides access to the Parameter Manager
               /// </summary>
               ///<remarks> This object is used to manage the model parameters, <see cref="ParameterManagerClass"/></remarks>
               /// <seealso cref="ParameterManagerClass"/>
               /// <seealso cref="ModelParameterClass"/>
                public ParameterManagerClass ParamManager { get { return _pm; } }

                ///-------------------------------------------------------------------------------------------------
                /// <summary>   If true, suspends Range checking for special range checks. </summary>
                ///
                /// <value> true if suspend range check, false if not. </value>
                ///-------------------------------------------------------------------------------------------------

                public bool Suspend100PctRangeCheck
                {
                    get { return FSuspendRangeCheck; }
                    set { FSuspendRangeCheck = value; }
                }


               // ------------------------------------------- 
               /// <summary>
               /// Provides access to the Process Manager
               /// </summary>
               /// <remarks>The process manager manages the pre and post processes of yearly Simulations <see cref="ProcessManager"/></remarks>
               /// <seealso cref="ProcessManager"/>
               /// <seealso cref="AnnualFeedbackProcess"/>
       
               public ProcessManager ProcessManager { get { return _ProcessManager; } }
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary>   Provides access to a registry of available AnnualFeedbackProcess classes. </summary>
               ///
               /// <value> The process registry. </value>
               /// <seealso cref="ProcessRegistry"/>
               /// <seealso cref="ProcessManager"/>
               /// <seealso cref="AnnualFeedbackProcess"/>
               ///-------------------------------------------------------------------------------------------------
       
               public ProcessRegistry ProcessRegistry { get { return _ProcessRegistry; } }
       
               // ------------------------------------------- 
               /// <summary>
               /// Version of the API interface
               /// </summary>
               public string APiVersion { get { return _APIVersion + " "+DateTime.Now.ToString("(M/d/y H:mm)"); } }
               // ------------------------------------------- 
               /// <summary>
               /// Verson of the Fortran Model
               /// </summary>
               public string ModelBuild { get { return _ModelBuild; } }
               // ------------------------------------------- 
               /// <summary>
               /// Tells FORTRAN model to write debug files.  Must be set TRUE before WaterSimManager constructor is called.
               /// </summary>
               public static bool CreateModelOutputFiles
               { get { return _FortranOutput; }  set {_FortranOutput = value; } }
               // ------------------------------------------- 
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary>   Gets the pathname of the data directory. </summary>
               /// <value> The pathname of the data directory. </value>
               ///-------------------------------------------------------------------------------------------------
       
               public string DataDirectory
               { get { return _DataDirectoryName; } }
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary>   Gets the pathname of the temp directory. </summary>
               /// <value> The pathname of the temp directory. </value>
               ///-------------------------------------------------------------------------------------------------
       
               public string TempDirectory
               { get { return _TempDirectoryName; } }
       
               
               //---------------------------------------------------
               /// <summary>
               /// Tests to make sure the specified data directoy (path) does contain input files the model needs.
               /// </summary>
               /// <param name="path">path to data directory</param>
               /// <returns>True if data files found, false if not.</returns>
               protected bool TestDataDirectory(string path)
               {
                   //string datafile = path + "App_Data\\Data\\Initial_storage.txt";
                   //return System.IO.File.Exists(datafile);
                   return true;
               }


               
       #endregion
       
               #region ClassInitialization
               /*************************************************************
                * Class Initialzation routines
                * 
                * ************************************************************/
               //----------------------------------------------------------------------------------------------------
               // This method is defined in WaterSimDCDC_User_Mods.cs
               partial void initialize_Other_ModelParameters();
               //----------------------------------------------------------------------------------------------------
               partial void initialize_Sensitivity_ModelParameters();

               // This method is defined in WaterSimDCDC_User_Mods.cs
               partial void initialize_Provider_Default_ModelParameters();
               //----------------------------------------------------------------------------------------------------
               // This method is defined in WaterSimDCDC_GWParameters.cs
               partial void initialize_GWModelParameters();
               //-----------------------------------------------------------------------
               partial void initialize_Sustainable_ModelParameters();
               //-----------------------------------------------------------------------
               partial void initialize_Derived_ModelParameters();
               //-----------------------------------------------------------------------
               partial void initialize_WebInterface_DerivedParameters();
               //-----------------------------------------------------------------------
    
               private void initialize_ModelParameters()
               {
                   // DAS
                   // Build some dependency groups
                   ModelParameterGroupClass DemandGroup = new ModelParameterGroupClass("Drill Down for OnOff Demand", new int[2] { eModelParam.epOnProjectDemand, eModelParam.epOffProjectDemand });
                   ParamManager.GroupManager.Add(DemandGroup);
                   ModelParameterGroupClass PopGroup = new ModelParameterGroupClass("Drill Down for OnOf Population", new int[2] { eModelParam.epOnProjectPopulation, eModelParam.epOtherPopulation });
                   ParamManager.GroupManager.Add(PopGroup);
                   
                   // Initialize Model ProviderArray Input properties
                   PCT_alter_GPCD = new providerArrayProperty(_pm, eModelParam.epAlterGPCDpct, get_alterGPCDpct, set_alterGPCDpct, eProviderAggregateMode.agWeighted);
       
                   Use_GPCD = new providerArrayProperty(_pm, eModelParam.epUse_GPCD, get_Use_GPCD, set_Use_GPCD, eProviderAggregateMode.agWeighted);
                   PCT_Wastewater_Reclaimed = new providerArrayProperty(_pm, eModelParam.epPCT_WasteWater_to_Reclaimed, get_PCT_Wastewater_Reclaimed, set_PCT_Wastewater_Reclaimed, eProviderAggregateMode.agWeighted);
                   PCT_Wastewater_to_Effluent = new providerArrayProperty(_pm, eModelParam.epPCT_Wastewater_to_Effluent, get_PCT_Wastewater_to_Effluent, set_PCT_Wastewater_to_Effluent, eProviderAggregateMode.agWeighted);
                   PCT_Reclaimed_to_RO = new providerArrayProperty(_pm, eModelParam.epPCT_Reclaimed_to_RO, get_PCT_Reclaimed_to_RO, set_PCT_Reclaimed_to_RO, eProviderAggregateMode.agWeighted);
                   PCT_RO_to_Water_Supply = new providerArrayProperty(_pm, eModelParam.epPCT_RO_to_Water_Supply, get_PCT_RO_to_Water_Supply, set_PCT_RO_to_Water_Supply, eProviderAggregateMode.agWeighted);
                   PCT_Reclaimed_to_DirectInject = new providerArrayProperty(_pm, eModelParam.epPCT_Reclaimed_to_DirectInject, get_PCT_Reclaimed_to_DirectInject, set_PCT_Reclaimed_to_DirectInject, eProviderAggregateMode.agWeighted);
                   PCT_Reclaimed_to_Water_Supply = new providerArrayProperty(_pm, eModelParam.epPCT_Reclaimed_to_Water_Supply, get_PCT_Reclaimed_to_Water_Supply, set_PCT_Reclaimed_to_Water_Supply, eProviderAggregateMode.agWeighted);
                   PCT_Reclaimed_to_Vadose = new providerArrayProperty(_pm, eModelParam.epPCT_Reclaimed_to_Vadose, get_PCT_Reclaimed_to_Vadose, set_PCT_Reclaimed_to_Vadose, eProviderAggregateMode.agWeighted);
                   PCT_Effluent_to_Vadose = new providerArrayProperty(_pm, eModelParam.epPCT_Effluent_to_Vadose, get_PCT_Effluent_to_Vadose, set_PCT_Effluent_to_Vadose, eProviderAggregateMode.agWeighted);
                   PCT_Effluent_to_PowerPlant = new providerArrayProperty(_pm, eModelParam.epPCT_Effluent_to_PowerPlant, get_PCT_Effluent_to_PowerPlant, set_PCT_Effluent_to_PowerPlant, eProviderAggregateMode.agWeighted);
                   SurfaceWater__to_Vadose = new providerArrayProperty(_pm, eModelParam.epSurfaceWater__to_Vadose, get_SurfaceWater__to_Vadose, set_SurfaceWater__to_Vadose, eProviderAggregateMode.agSum);
                   Surface_to_Vadose_Time_Lag = new providerArrayProperty(_pm, eModelParam.epSurface_to_Vadose_Time_Lag, get_Surface_to_Vadose_Time_Lag, set_Surface_to_Vadose_Time_Lag, eProviderAggregateMode.agAverage);
                   WaterBank_Source_Option = new providerArrayProperty(_pm, eModelParam.epWaterBank_Source_Option, get_WaterBank_Source_Option, set_WaterBank_Source_Option, eProviderAggregateMode.agNone);
                   PCT_SurfaceWater_to_WaterBank = new providerArrayProperty(_pm, eModelParam.epPCT_SurfaceWater_to_WaterBank, get_PCT_SurfaceWater_to_WaterBank, set_PCT_SurfaceWater_to_WaterBank, eProviderAggregateMode.agWeighted);
                   Use_SurfaceWater_to_WaterBank = new providerArrayProperty(_pm, eModelParam.epUse_SurfaceWater_to_WaterBank, get_Use_SurfaceWater_to_WaterBank, set_Use_SurfaceWater_to_WaterBank, eProviderAggregateMode.agSum);
                   PCT_WaterSupply_to_Residential = new providerArrayProperty(_pm, eModelParam.epPCT_WaterSupply_to_Residential, get_PCT_WaterSupply_to_Residential, set_PCT_WaterSupply_to_Residential, eProviderAggregateMode.agWeighted);
                   PCT_WaterSupply_to_Commercial = new providerArrayProperty(_pm, eModelParam.epPCT_WaterSupply_to_Commercial, get_PCT_WaterSupply_to_Commercial, set_PCT_WaterSupply_to_Commercial, eProviderAggregateMode.agWeighted);
                   Use_WaterSupply_to_DirectInject = new providerArrayProperty(_pm, eModelParam.epUse_WaterSupply_to_DirectInject, get_Use_WaterSupply_to_DirectInject, set_Use_WaterSupply_to_DirectInject, eProviderAggregateMode.agWeighted);
                   //PCT_Outdoor_WaterUse = new providerArrayProperty(_pm, eModelParam.epPCT_Outdoor_WaterUse, get_PCT_Outdoor_WaterUse, set_PCT_Outdoor_WaterUse, eProviderAggregateMode.agNone);
                   PCT_Groundwater_Treated = new providerArrayProperty(_pm, eModelParam.epPCT_Groundwater_Treated, get_PCT_Groundwater_Treated, set_PCT_Groundwater_Treated, eProviderAggregateMode.agWeighted);
                   PCT_Reclaimed_Outdoor_Use = new providerArrayProperty(_pm, eModelParam.epPCT_Reclaimed_Outdoor_Use, get_PCT_Reclaimed_Outdoor_Use, set_PCT_Reclaimed_Outdoor_Use, eProviderAggregateMode.agWeighted);
                   PCT_Growth_Rate_Adjustment_OnProject = new providerArrayProperty(_pm, eModelParam.epPCT_Growth_Rate_Adjustment_OnProject, get_PCT_Growth_Rate_Adjustment_OnProject, set_PCT_Growth_Rate_Adjustment_OnProject, eProviderAggregateMode.agWeighted);
                   PCT_Growth_Rate_Adjustment_Other = new providerArrayProperty(_pm, eModelParam.epPCT_Growth_Rate_Adjustment_Other, get_PCT_Growth_Rate_Adjustment_Other, set_PCT_Growth_Rate_Adjustment_Other, eProviderAggregateMode.agWeighted);
                   PCT_Max_Demand_Reclaim = new providerArrayProperty(_pm, eModelParam.epPCT_Max_Demand_Reclaim, get_PCT_Max_Demand_Reclaim, set_PCT_Max_Demand_Reclaim, eProviderAggregateMode.agWeighted);
                   // *** Changed 8 12 12 Population_Override = new providerArrayProperty(_pm, eModelParam.epSetPopulations, get_populations, set_populations, eProviderAggregateMode.agNone);
                   Population_Override_On = new providerArrayProperty(_pm, eModelParam.epSetPopulationsOn, get_populationsOn, set_populationsOn, eProviderAggregateMode.agSum);
                   Population_Override_Other = new providerArrayProperty(_pm, eModelParam.epSetPopulationsOther, get_populationsOther, set_populationsOther, eProviderAggregateMode.agSum);
                   //******
                   //**** QUAY Added 3/6/13
                   WaterAugmentation = new providerArrayProperty(_pm, eModelParam.epWaterAugmentation, get_NewWater, set_NewWater, eProviderAggregateMode.agSum);
                   //******
                   // DAS Added 03.15.14
                   WaterAugmentationUsed = new providerArrayProperty(_pm, eModelParam.epWaterAugmentationUsed, get_NewWaterUsed, null, eProviderAggregateMode.agSum);
                   //
                   Maximum_normalFlow_rights = new providerArrayProperty(_pm, eModelParam.epProvider_Max_NormalFlow, get_normalFlow_rights_max, set_normalFlow_rights_max, eProviderAggregateMode.agSum);
 
                   PCT_modify_normalFlow = new providerArrayProperty(_pm, eModelParam.epModfyNormalFlow, get_modifyNormalFlow, set_modifyNormalFlow, eProviderAggregateMode.agWeighted);
       
                   // Initialize Model ProviderArray Output properties
               
                   Groundwater_Pumped_Municipal = new providerArrayProperty(_pm,eModelParam.epGroundwater_Pumped_Municipal, get_Groundwater_Pumped_Municipal, eProviderAggregateMode.agSum);
                   Groundwater_Balance = new providerArrayProperty(_pm, eModelParam.epGroundwater_Balance, get_Groundwater_Balance, eProviderAggregateMode.agSum);
                   SaltVerde_Annual_Deliveries_SRP = new providerArrayProperty(_pm, eModelParam.epSaltVerde_Annual_Deliveries_SRP, get_SaltVerde_Annual_Deliveries_SRP, eProviderAggregateMode.agSum);
                   SaltVerde_Class_BC_Designations = new providerArrayProperty(_pm, eModelParam.epSaltVerde_Class_BC_Designations, get_SaltVerde_Class_BC_Designations, eProviderAggregateMode.agSum);
                   Colorado_Annual_Deliveries = new providerArrayProperty(_pm, eModelParam.epColorado_Annual_Deliveries, get_Colorado_Annual_Deliveries, eProviderAggregateMode.agSum);
                   Groundwater_Bank_Used = new providerArrayProperty(_pm, eModelParam.epGroundwater_Bank_Used, get_Groundwater_Bank_Used, eProviderAggregateMode.agSum);           
                   Groundwater_Bank_Balance = new providerArrayProperty(_pm, eModelParam.epGroundwater_Bank_Balance, get_Groundwater_Bank_Balance, eProviderAggregateMode.agSum);
                   Reclaimed_Water_Used = new providerArrayProperty(_pm, eModelParam.epReclaimed_Water_Used, get_Reclaimed_Water_Used, eProviderAggregateMode.agSum);          
                   Reclaimed_Water_To_Vadose = new providerArrayProperty(_pm, eModelParam.epReclaimed_Water_To_Vadose, get_Reclaimed_Water_To_Vadose, eProviderAggregateMode.agSum);            
                   Reclaimed_Water_Discharged = new providerArrayProperty(_pm, eModelParam.epReclaimed_Water_Discharged, get_Reclaimed_Water_Discharged, eProviderAggregateMode.agSum);          
                   Reclaimed_Water_to_DirectInject = new providerArrayProperty(_pm, eModelParam.epReclaimed_Water_to_DirectInject, get_Reclaimed_Water_to_DirectInject, eProviderAggregateMode.agSum);
                   RO_Reclaimed_Water_Used = new providerArrayProperty(_pm, eModelParam.epRO_Reclaimed_Water_Used, get_RO_Reclaimed_Water_Used, eProviderAggregateMode.agSum);          
                   RO_Reclaimed_Water_to_DirectInject = new providerArrayProperty(_pm, eModelParam.epRO_Reclaimed_Water_to_DirectInject, get_RO_Reclaimed_Water_to_DirectInject, eProviderAggregateMode.agSum);            
                   Total_Effluent_Reused = new providerArrayProperty(_pm, eModelParam.epEffluent_Reused, get_Total_Effluent_Reused, eProviderAggregateMode.agSum);            
                   Effluent_To_Vadose = new providerArrayProperty(_pm, eModelParam.epEffluent_To_Vadose, get_Effluent_To_Vadose, eProviderAggregateMode.agSum);            
                   Effluent_To_PowerPlant = new providerArrayProperty(_pm, eModelParam.epEffluent_To_PowerPlant, get_Effluent_To_PowerPlant, eProviderAggregateMode.agSum);            
                   Effluent_Discharged = new providerArrayProperty(_pm, eModelParam.epEffluent_Discharged, get_Effluent_Discharged, eProviderAggregateMode.agSum);
                   Demand_Deficit = new providerArrayProperty(_pm, eModelParam.epDemand_Deficit, get_Demand_Deficit, eProviderAggregateMode.agSum);
                   Total_Demand = new providerArrayProperty(_pm, eModelParam.epTotal_Demand, get_Total_Demand, eProviderAggregateMode.agSum);
                   GPCD_Used = new providerArrayProperty(_pm, eModelParam.epGPCD_Used, get_GPCD_Used, eProviderAggregateMode.agWeighted);
                   Population_Used = new providerArrayProperty(_pm, eModelParam.epPopulation_Used, get_Population_Used, eProviderAggregateMode.agSum);
                   PCT_WaterSupply_to_Industrial = new providerArrayProperty(_pm, eModelParam.epPCT_WaterSupply_to_Industrial, get_PCT_WaterSupply_to_Industrial, set_PCT_WaterSupply_to_Industrial, eProviderAggregateMode.agWeighted);
                   PCT_Outdoor_WaterUseRes = new providerArrayProperty(_pm, eModelParam.epPCT_Outdoor_WaterUseRes, get_PCT_Outdoor_WaterUseRes, set_PCT_Outdoor_WaterUseRes, eProviderAggregateMode.agWeighted);
                   PCT_Outdoor_WaterUseCom = new providerArrayProperty(_pm, eModelParam.epPCT_Outdoor_WaterUseCom, get_PCT_Outdoor_WaterUseCom, set_PCT_Outdoor_WaterUseCom, eProviderAggregateMode.agWeighted);
                   PCT_Outdoor_WaterUseInd = new providerArrayProperty(_pm, eModelParam.epPCT_Outdoor_WaterUseInd, get_PCT_Outdoor_WaterUseInd, set_PCT_Outdoor_WaterUseInd, eProviderAggregateMode.agWeighted);
                   Demand_On_Project = new providerArrayProperty(_pm, eModelParam.epOnProjectDemand, get_Demand_On_Project, eProviderAggregateMode.agSum);         
                   Demand_Off_Project = new providerArrayProperty(_pm, eModelParam.epOffProjectDemand, get_Demand_Off_Project, eProviderAggregateMode.agSum);
                   Population_On_Project = new providerArrayProperty(_pm, eModelParam.epOnProjectPopulation, get_Population_OnProject, eProviderAggregateMode.agSum);
                   Population_Other = new providerArrayProperty(_pm, eModelParam.epOtherPopulation, get_Population_Other, eProviderAggregateMode.agSum);
       
                   Incidental_Water_Credit = new providerArrayProperty(_pm, eModelParam.epAnnualIncidental, get_Incidental_Water_Credit, eProviderAggregateMode.agSum);
                   Total_Vadose_To_Aquifer = new providerArrayProperty(_pm, eModelParam.epVadoseToAquifer, get_Total_Vadose_To_Aquifer_Flux, eProviderAggregateMode.agSum);

                   Total_WWTP_Effluent = new providerArrayProperty(_pm, eModelParam.epTWWTPCreated_AF, get_TWWTP_Created, eProviderAggregateMode.agWeighted);
                   GPCD_raw = new providerArrayProperty(_pm, eModelParam.epGPCDraw, get_GPCDraw, eProviderAggregateMode.agSum);
                   Reclaimed_Water_Created = new providerArrayProperty(_pm, eModelParam.epTotalReclaimedCreated_AF, get_Reclaimed_Water_Created, eProviderAggregateMode.agWeighted);

               
                   /*************************************************************************
                    * Setup all Model Parameters in the Prameter Manager
                    * 
                    * 
                    *************************************************************************/
                   // Base Inputs
                 
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSimulation_Start_Year, "Simulation Start Year", "STARTYR",  rangeChecktype.rctCheckRange, 2000, 2006, geti_Simulation_Start_Year, seti_Simulation_Start_Year, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSimulation_End_Year, "Simulation End Year", "STOPYR",  rangeChecktype.rctCheckRange, _defaultEndYear, 2085, geti_Simulation_End_Year, seti_Simulation_End_Year, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_Historical_Extraction_Start_Year, "Colorado Flow Trace Start Year", "COEXTSTYR",  rangeChecktype.rctCheckRangeSpecial, 762, 1979, geti_Colorado_Historical_Extraction_Start_Year, seti_Colorado_Historical_Extraction_Start_Year, ColoradoYearRangeCheck));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_Historical_Data_Source, "Colorado Historical Data Source", "COSRC",  rangeChecktype.rctCheckRange, 1, 3, geti_Colorado_Historical_Data_Source, seti_Colorado_Historical_Data_Source, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_Climate_Adjustment_Percent, "CO: Adjust Flows", "COCLMADJ",  rangeChecktype.rctCheckRange, 0, 150, geti_Colorado_Climate_Adjustment_Percent, seti_Colorado_Climate_Adjustment_Percent, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_User_Adjustment_Percent, "CO: Adjust Drought", "COUSRADJ",  rangeChecktype.rctCheckRange, 0, 150, geti_Colorado_User_Adjustment_Percent, seti_Colorado_User_Adjustment_Percent, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_User_Adjustment_StartYear, "Colorado Drought Start Year", "COUSRSTR",  rangeChecktype.rctCheckRange, 2006, 2081, geti_Colorado_User_Adjustment_StartYear, seti_Colorado_User_Adjustment_StartYear,RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_User_Adjustment_Stop_Year, "Colorado Drought Stop Year", "COUSRSTP",  rangeChecktype.rctCheckRange, 2006, 2081, geti_Colorado_User_Adjustment_Stop_Year, seti_Colorado_User_Adjustment_Stop_Year, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Historical_Extraction_Start_Year, "SaltVerde Flows Trace Start Year", "SVEXTSTYR",  rangeChecktype.rctCheckRangeSpecial, 1330, 1979, geti_SaltVerde_Historical_Extraction_Start_Year, seti_SaltVerde_Historical_Extraction_Start_Year, SaltVerdeYearRangeCheck));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Historical_Data, "Salt-Verde Trace Data Source", "SVSRC",  rangeChecktype.rctCheckRange, 1, 3, geti_SaltVerde_Historical_Data, seti_SaltVerde_Historical_Data, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Climate_Adjustment_Percent, "Salt-Verde: Adjust Flows", "SVCLMADJ",  rangeChecktype.rctCheckRange, 0, 150, geti_SaltVerde_Climate_Adjustment_Percent, seti_SaltVerde_Climate_Adjustment_Percent, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_User_Adjustment_Percent, "Salt-Verde: Adjust Drought", "SVUSRADJ",  rangeChecktype.rctCheckRange, 0, 150, geti_SaltVerde_User_Adjustment_Percent, seti_SaltVerde_User_Adjustment_Percent, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_User_Adjustment_Start_Year, "Salt-Verde Drought Start Year", "SVUSRSTR",  rangeChecktype.rctCheckRange, 2006, 2081, geti_SaltVerde_User_Adjustment_Start_Year, seti_SaltVerde_User_Adjustment_Start_Year, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_User_Adjustment_Stop_Year, "Salt-Verde Drought Stop Year", "SVUSRSTP",  rangeChecktype.rctCheckRange, 2006, 2081, geti_SaltVerde_User_Adjustment_Stop_Year, seti_SaltVerde_User_Adjustment_Stop_Year, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epProvider_Demand_Option, "Provider Demand Option", "DMOPT",  rangeChecktype.rctCheckRange, 1, 4, geti_Provider_Demand_Option, seti_Provider_Demand_Option, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Alter_GPCD, "Reduction in GPCD, by 2085", "PCRDGPCD",  rangeChecktype.rctCheckRange, 0, 70, geti_PCT_Reduce_GPCD, seti_PCT_Reduce_GPCD, RangeCheck.NoSpecialBase));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epAWSAnnualGWLimit, "Ignore Rule to Limit Pumping to Annual AWS GW Credit (0=False)", "AWSLIMIT",  rangeChecktype.rctCheckRange, 0, 1, geti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit, seti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit, RangeCheck.NoSpecialBase));
                    _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_REG_Growth_Rate_Adjustment, "Regional Pop Growth Rate", "REGPOPGR", rangeChecktype.rctCheckRange, 0, 300, geti_PCT_REG_Growth_Rate_Adjustment, seti_PCT_REG_Growth_Rate_Adjustment, RangeCheck.NoSpecialBase));

              
                   // Provider Inputs
                    
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epAlterGPCDpct, "Alter the provider trend in GPCD", "ALTRGPCD", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, -70, 70, null, get_alterGPCDpct, null, set_alterGPCDpct, null, null, PCT_alter_GPCD));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epUse_GPCD, "Use GPCD", "USEGPCD", rangeChecktype.rctCheckRange, -1, 2000, RangeCheck.NoSpecialProvider,Use_GPCD));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_WasteWater_to_Reclaimed, "Effluent to Reclaimed Plant", "PCEFFREC", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, PCT_Wastewater_Reclaimed));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Wastewater_to_Effluent, "Total Wastewater is Usable Effluent", "PCWWEFF", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider,PCT_Wastewater_to_Effluent));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Reclaimed_to_RO, "Reclaimed to RO", "PCRECRO", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, PCT_Reclaimed_to_RO));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_RO_to_Water_Supply, "RO to Water Supply", "PCROWS", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider,PCT_RO_to_Water_Supply));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Reclaimed_to_DirectInject, "Reclaimed to DirectInject", "PCRECDI", rangeChecktype.rctCheckRangeSpecial, 0, 100, PCTReclaimedRangeCheck,PCT_Reclaimed_to_DirectInject));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Reclaimed_to_Water_Supply, "Reclaimed to Water Supply", "PCERECWS", rangeChecktype.rctCheckRangeSpecial, 0, 100, PCTReclaimedRangeCheck,PCT_Reclaimed_to_Water_Supply));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Reclaimed_to_Vadose, "Reclaimed to Vadose", "PCRECVAD", rangeChecktype.rctCheckRangeSpecial, 0, 100, PCTReclaimedRangeCheck,PCT_Reclaimed_to_Vadose));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Effluent_to_Vadose, "Effluent to Vadose", "PCEFFVAD", rangeChecktype.rctCheckRangeSpecial, 0, 100, PCTEffluentRangeCheck,PCT_Effluent_to_Vadose));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Effluent_to_PowerPlant, "Effluent to PowerPlant", "PCEFFPP", rangeChecktype.rctCheckRangeSpecial, 0, 100, PCTEffluentRangeCheck,PCT_Effluent_to_PowerPlant));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSurfaceWater__to_Vadose, "SurfaceWater to Vadose", "PCSWVAD", rangeChecktype.rctCheckRange, 0, 100000, RangeCheck.NoSpecialProvider,SurfaceWater__to_Vadose));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSurface_to_Vadose_Time_Lag, "Surface to Vadose Time Lag in Years", "VADLAG", rangeChecktype.rctCheckRange, 0, 50, RangeCheck.NoSpecialProvider,Surface_to_Vadose_Time_Lag));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epWaterBank_Source_Option, "WaterBank Source Option", "WBOPT", rangeChecktype.rctCheckRange, 1, 2, RangeCheck.NoSpecialProvider,WaterBank_Source_Option));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_SurfaceWater_to_WaterBank, "SurfaceWater to WaterBank", "PCSWWB", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider,PCT_SurfaceWater_to_WaterBank));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epUse_SurfaceWater_to_WaterBank, "SurfaceWater to WaterBank", "SWWB", rangeChecktype.rctCheckRange, 0, 100000, RangeCheck.NoSpecialProvider,Use_SurfaceWater_to_WaterBank));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_WaterSupply_to_Residential, "WaterSupply to Residential", "PCWSRES", rangeChecktype.rctCheckRangeSpecial, 0, 100, ResComPCTRangeCheck,PCT_WaterSupply_to_Residential));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_WaterSupply_to_Commercial, "WaterSupply to Commercial", "PCWSCOM", rangeChecktype.rctCheckRangeSpecial, 0, 100, ResComPCTRangeCheck,PCT_WaterSupply_to_Commercial));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epUse_WaterSupply_to_DirectInject, "Water Supply to DirectInject", "USEWSDI", rangeChecktype.rctCheckRange, 0, 100000, RangeCheck.NoSpecialProvider,Use_WaterSupply_to_DirectInject));
                   // Modified 7 23 12 _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Outdoor_WaterUse, "% Outdoor Water Use", "PGOUTUSE", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, get_PCT_Outdoor_WaterUse, RangeCheck.NoSpecialProvider, set_PCT_Outdoor_WaterUse,RangeCheck.NoSpecialProvider,RangeCheck.NoSpecialProvider,PCT_Outdoor_WaterUse));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Groundwater_Treated, "Groundwater Treated", "PCGWTRT", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider,PCT_Groundwater_Treated));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Reclaimed_Outdoor_Use, "Outdoor Can Use Reclaimed", "PCRECOUT", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider,PCT_Reclaimed_Outdoor_Use));
                   // Modified 7 23 12 _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Growth_Rate_Adjustment, "% Growth Rate Adjustment", "PCGRWRTE", rangeChecktype.rctCheckRange, 0, 300, RangeCheck.NoSpecialProvider, get_PCT_Growth_Rate_Adjustment, RangeCheck.NoSpecialProvider, set_PCT_Growth_Rate_Adjustment,RangeCheck.NoSpecialProvider,RangeCheck.NoSpecialProvider,PCT_Growth_Rate_Adjustment));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Growth_Rate_Adjustment_OnProject, "Growth Rate Adj On Project", "PCGRTON", rangeChecktype.rctCheckRange, 0, 300, RangeCheck.NoSpecialProvider,PCT_Growth_Rate_Adjustment_OnProject));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Growth_Rate_Adjustment_Other, "Growth Rate Adj Other", "PCGRTOFF", rangeChecktype.rctCheckRange, 0, 300, RangeCheck.NoSpecialProvider,PCT_Growth_Rate_Adjustment_Other));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Max_Demand_Reclaim, "Max Demand Reclaimed", "PCDEMREC", rangeChecktype.rctCheckRange, 0, 70, RangeCheck.NoSpecialProvider, PCT_Max_Demand_Reclaim));
                   // *** changed 8 13 12 _pm.AddParameter(new ModelParameterClass(eModelParam.epSetPopulations, "Provider Population Override", "POPOVRD", rangeChecktype.rctNoRangeCheck, 0, 0, RangeCheck.NoSpecialProvider, get_populations , RangeCheck.NoSpecialProvider, set_populations, RangeCheck.NoSpecialProvider, RangeCheck.NoSpecialProvider, Population_Override));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSetPopulationsOn, "Provider On-Project Pop Override", "POPOVON", rangeChecktype.rctNoRangeCheck, 0, 0, RangeCheck.NoSpecialProvider, Population_Override_On));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSetPopulationsOther, "Provider Off-Project Pop Override", "POPOVOFF", rangeChecktype.rctNoRangeCheck, 0, 0, RangeCheck.NoSpecialProvider, Population_Override_Other));
                   //*************
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epModfyNormalFlow, "Modify Normal Flow", "MNFLOW", rangeChecktype.rctCheckRange, 0, 554, RangeCheck.NoSpecialProvider, PCT_modify_normalFlow));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_WaterSupply_to_Industrial, "WaterSupply to Industrial", "PCWSIND", rangeChecktype.rctCheckRangeSpecial, 0, 100,  ResComPCTRangeCheck, PCT_WaterSupply_to_Industrial));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Outdoor_WaterUseRes, "Res Outdoor Water Use", "PROUTUSE", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, PCT_Outdoor_WaterUseRes));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Outdoor_WaterUseCom, "Com Outdoor Water Use", "PCOUTUSE", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, PCT_Outdoor_WaterUseCom));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPCT_Outdoor_WaterUseInd, "Ind Outdoor Water Use", "PIOUTUSE", rangeChecktype.rctCheckRange, 0, 100, RangeCheck.NoSpecialProvider, PCT_Outdoor_WaterUseInd));
                   // QUAY Added 3/6/13
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epWaterAugmentation, "Amount of Augmented Water", "WATAUG", rangeChecktype.rctCheckRange, 0, 100000, RangeCheck.NoSpecialProvider, WaterAugmentation));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epProvider_Max_NormalFlow, "The maximum right usable", "NFLOWMAX", modelParamtype.mptInputProvider, rangeChecktype.rctCheckRange, 0, 554, null, get_normalFlow_rights_max, null, set_normalFlow_rights_max, null, null, Maximum_normalFlow_rights));
             
                   //*****
       
                   // Base Outputs

                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_River_Flow, "Colorado River Flow", "CORFLOW", geti_Colorado_River_Flow, 5000000, 30000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPowell_Storage, "Powell Storage", "POWSTORE", geti_Powell_Storage, 5000000, 30000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epMead_Storage, "Mead Storage", "MEDSTORE", geti_Mead_Storage, 5000000, 30000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_River_Flow, "Salt-Verde River Flow", "SVRFLOW", geti_SaltVerde_River_Flow, 0, 5000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Storage, "Salt-Verde Storage", "SVRSTORE", geti_SaltVerde_Storage, 500000, 2500000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epEffluent_To_Agriculture, "Effluent To Agriculture", "EFLAG", geti_Effluent_To_Agriculture, 0, 550000 ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Spillage, "Reservoir Spillage", "SVTSPILL", geti_SVTspillage, 0, 3500000 ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epMeadLevel, "Mead Elevation", "MEADELEV", geti_ElevationMead, 800, 1300));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalNaturalRecharge, "Regional Aquifer Natural Recharge", "REGAQRCHG", geti_Regional_Natural_Recharge, 100000, 1200000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalCAGRDRecharge, "Regional CAGRD Recharge", "REGCAGRDR", geti_Regional_CAGRD_Recharge, 0, 250000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalInflow, "Regional Natural Aquifer Inflow", "REGAQIN", geti_Regional_Inflow, 20000, 50000 ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalAgToVadose, "Regional Ag to Vadose Recharge", "REGAGVAD", geti_Regional_Ag_To_Vadose ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalProviderRecharge, "Total Recharge All Providers", "REGPRRCHG", geti_Regional_Provider_Recharge, 0, 1500 ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalAgOtherPumping, "Regional Ag and Other Pumping", "REGAOPMP", geti_Regional_Ag_Other_Pumping, 0, 700000 ));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalOutflow, "Regional Aquifer Natural Outflow", "REGNAOUT", geti_Regional_Outflow, 30000, 40000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRegionalGWBalance, "Regional Aquifer Balance", "REGAQBAL", geti_Regional_Groundwater_Balance, 50000000, 10000000 ));
                    // DAS Added 02.16.14
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPowellLevel, "Powell Elevation (Feet-msl)", "POWELELEV", geti_ElevationPowell,3370, 3750));
                    // DAS Added 06.16.14
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltOther_Storage, "Salt-Other Storage", "SOSTORE", geti_SaltOther_Storage, 58000, 1500000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRoosevelt_Storage, "Roosevelt Storage", "ROOSSTORE", geti_Roosevelt_Storage, 100000, 2100000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epVerde_Storage, "Verde Storage", "VRSTORE", geti_Verde_Storage, 23000, 500000));

                   // Provider Outputs
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epGroundwater_Pumped_Municipal, "Groundwater Pumped Municipal", "MGWPUMP", Groundwater_Pumped_Municipal, 0, 150000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epGroundwater_Balance, "Available Groundwater", "GWAVAIL", Groundwater_Balance, 0, 7000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Annual_Deliveries_SRP, "SaltVerde Annual Deliveries SRP", "SRPDELIV", SaltVerde_Annual_Deliveries_SRP, 0, 300000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epSaltVerde_Class_BC_Designations, "SaltVerde Class BC Designations", "SRPBCDES", SaltVerde_Class_BC_Designations, 0, 300000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epColorado_Annual_Deliveries, "Colorado Annual Deliveries", "COLDELIV", Colorado_Annual_Deliveries, 0, 250000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epGroundwater_Bank_Used, "Groundwater Bank Used", "BNKUSED", Groundwater_Bank_Used, 0, 50000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epGroundwater_Bank_Balance, "Groundwater Bank Balance", "BNKAVAIL", Groundwater_Bank_Balance, 0, 700000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epReclaimed_Water_Used, "Reclaimed Water Used", "RECTOT", Reclaimed_Water_Used, 0, 50000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epReclaimed_Water_To_Vadose, "Reclaimed Water To Vadose", "RECVADOS", Reclaimed_Water_To_Vadose));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epReclaimed_Water_Discharged, "Reclaimed Water Discharged ", "RECDISC", Reclaimed_Water_Discharged, 0, 50000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epReclaimed_Water_to_DirectInject, "Reclaimed Water to DirectInject", "RECINJEC", Reclaimed_Water_to_DirectInject));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRO_Reclaimed_Water_Used, "RO Reclaimed Water Used", "ROTOT", RO_Reclaimed_Water_Used));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epRO_Reclaimed_Water_to_DirectInject, "RO Reclaimed DirectInject", "ROINJEC", RO_Reclaimed_Water_to_DirectInject));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epEffluent_Reused, "Total Effluent Reused", "EFLCRT", Total_Effluent_Reused, 0, 250000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epEffluent_To_Vadose, "Effluent To Vadose", "EFLVADOS", Effluent_To_Vadose, 0, 25000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epEffluent_To_PowerPlant, "Effluent To PowerPlant", "EFLPP", Effluent_To_PowerPlant, 0, 85000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epEffluent_Discharged, "Effluent Discharged", "EFLDISC", Effluent_Discharged, 0, 60000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epDemand_Deficit, "Demand Deficit", "DEMDEF", Demand_Deficit));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epGPCD_Used, "GPCD Used", "GPCDUSED", GPCD_Used, 0, 400));

                   _pm.AddParameter(new ModelParameterClass(eModelParam.epPopulation_Used, "Population", "POPUSED", Population_Used,PopGroup, 1000, 4000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epTotal_Demand, "Total Demand", "TOTDEM", Total_Demand, DemandGroup, 0, 500000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epOnProjectDemand, "On Project Demand (AF)", "ONDEM", Demand_On_Project, 0, 350000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epOffProjectDemand, "Off Project Demand (AF)", "OFFDEM", Demand_Off_Project, 0, 350000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epOnProjectPopulation, "On-project population (ppl)", "POPONPRJ", Population_On_Project, 0, 2000000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epOtherPopulation, "Other population (ppl)", "POPOTHER", Population_Other, 0, 2000000));


                   _pm.AddParameter(new ModelParameterClass(eModelParam.epAnnualIncidental, "Incidental Recharge Credit", "INCCREDIT", Incidental_Water_Credit, 0, 75000));
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epVadoseToAquifer, "Total Vadose Recharge", "VADRCHTOT", Total_Vadose_To_Aquifer, 0, 150000));

                   this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epTWWTPCreated_AF, "Total TWWTP Water Created", "TWWTP", Total_WWTP_Effluent, 0, 300000));
                   this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epGPCDraw, "GPCD raw", "GPCDRAW", GPCD_raw, 0, 600));
                   this.ParamManager.AddParameter(new ModelParameterClass(eModelParam.epTotalReclaimedCreated_AF, "Reclaimed Water Created", "TOTREC", Reclaimed_Water_Created, 0, 100000));

                   // DAS Added 02.15.14
                   _pm.AddParameter(new ModelParameterClass(eModelParam.epWaterAugmentationUsed, "Amount of Augmented Water Used", "WATAUGUSED", WaterAugmentationUsed, 0, 100000));

               // Male a Call to initialize the Groundwater Parameters if implementation exists (WaterSimDCDC_GWPaameters ver N.cs)
                   initialize_GWModelParameters();
                   // Make Call to initialize Derived model parameters, if implmentation exists 
                   initialize_Derived_ModelParameters();
                   // Make Call to initialize Sustainable model parameters, if implementation exists 
                   initialize_Sustainable_ModelParameters();
                   // Make Call to initialize user model parameters , implemented in WaterSimDCDC_User_Mods.cs
                  initialize_Other_ModelParameters();
                   // Call for Web Parameters
                  initialize_WebInterface_DerivedParameters();
                   // NOT YET USED
                  initialize_Sensitivity_ModelParameters();
               }
               //--------------------------------------------------------------     
               private void InitDefaultVariables()
               {
                   //
                   _ws.Startyear = _StartYear = _ws.SimulationStart = util.WaterSimDCDC_Default_Simulation_StartYear;
                   _ws.Endyear = _EndYear = _ws.SimulationEnd = util.WaterSimDCDC_Default_Simulation_EndYear;
                   // QUAY BEGIN EDIT 3 10 14
                   Simulation_End_Year = _ws.Endyear;
                   // QUAY end EDIT 3 10 14
                   // Call API.PARMS to Set up Default array
                   API.parms(_ws);
       
                   //TEMP FIX  CHANGE IN FUTURE IF FIXED
                   Colorado_User_Adjustment_StartYear= util.WaterSimDCDC_Default_Colorado_User_Adjustment_StartYear;
                   SaltVerde_Historical_Data_Source = util.WaterSimDCDC_Default_SaltVerde_Historical_Data;
                   SaltVerde_User_Adjustment_Start_Year = util.WaterSimDCDC_Default_SaltVerde_User_Adjustment_Start_Year;
                   Provider_Demand_Option = util.WaterSimDCDC_Provider_Demand_Option;
                   Assured_Water_Supply_Annual_Groundwater_Pumping_Limit = 1;
                   // GPCDFIX
                   GPCDBuffer = GPCDDefaults;
                   initialize_Provider_Default_ModelParameters();
                   //DAS 09.17.14
                   Decision_Game_2014 = false;
                }
               internal void GPCDFIX()
               {
                   // OK This is not exactly elegant but is in place to handle odd nature of Use_GPCD and GPCD_Used.
                   // There is only on parameter in the model for provider GPCD.  It can be set to a GPCD for Demand Option 4, for other options
                   //  it is calculated and can not be set.  The Get always reports the GPCD_Used in the last run, which for Option 4 is what it was set to, but for all other options 
                   // is a calculated value changing each year.
                   // To mimic all other values two parameters were created, one input one ouput, that use the same model function, the set for the input, and the get for output.
                   // The get for the input paramter uses two dummy arrays, one for option 4 which is changed on a set, and one for all other options which returns 
                   // the intitial default value used by the model in calculating GPCD.  These defaults can only be changed in the provider input file.
                   // A special exception is added if USE_GPCD is set when option 4 is not selected since that does not work
                   // This sets up these arrays.  In order to get the default values, the model has to be run for one year.  
                   // fetch the defaults
                   int OldStart = Simulation_Start_Year;
                   int OldEnd = Simulation_End_Year;
                   Simulation_Initialize();
                   Simulation_Start_Year = 2000;
                   Simulation_End_Year = 2012;
                   Simulation_NextYear();
                   //// OK now fetch the GCPD defaults
                   GPCDBuffer = GPCDDefaults = _ws.ProviderGPCD;
                   Simulation_Stop();
                   // reset the defaults
                   Simulation_Start_Year = OldStart;
                   Simulation_End_Year = OldEnd;
                   
                   // OK will use these inthe provider property routines
                   // NOTE, this is one of the exceptions where a get call and a run control is made into the model outside the ModelParamter interface.
                 }
                 #endregion
               ////------------------------------------------------------------------------
       
       
               /*************************************************************
                * Simulation Control Delegates, Fields, Properties and Methods
                * 
                * **************************************************************/
               #region Simulation_Control
       
               /**************************************
                 * SimulationLock , LockSimulation(), UnLockSimulation()
                 *  These can be used to lock down the simultaion so no input parameters can be set
                 *  In event driven environments this is advised
                 * ************************************/
               //----------------------------------------------------------------------------------------------------
               /// <summary>
               ///  Property Locks and unlocks (true false) the simulation so no parameters can be set until unlocked.  
               /// </summary>
               /// <remarks>I started with this and then moved to LockSimulation() and UnlockSimulation(), may remove this at some point.</remarks>
               /// <seealso cref="UnLockSimulation"/>
               /// <seealso cref="LockSimulation"/>
       
               public bool SimulationLock
               // Sets Model Locl
               {
                   get { return FModelLocked; }
                   set { FModelLocked = value; }
               }
               //----------------------------------------------------------------------------------------------------
               /// <summary>
               /// Locks the simulation so Base Input parameters can be set until unlocked. 
               /// </summary>
               /// <seealso cref="UnLockSimulation"/>
                public void LockSimulation()   {  FModelLocked = true;  }
               //----------------------------------------------------------------------------------------------------
       
                /// <summary>   Unlock simulation so Base Input parameters can be set . </summary>
               /// <seealso cref="LockSimulation"/>
               public  void UnLockSimulation()  { FModelLocked = false; }
               //----------------------------------------------------------------------------------------------------
               internal bool isLocked() { return FModelLocked; }
               //----------------------------------------------------------------------------------------------------
               //----------------------------------------------------------------------------------------------------
               /// <summary>
               /// Must be called to setup a Simulation . Simulation can be run in two ways, 
               ///      calling SimulationNextYear() for each year to be run, call SimulationAllYears() which runs all the years
               ///   All simulations should be stopped with StopSimulation(), which will make sure all files are closed
               ///  Will reset SimulationLock to false;
               /// </summary>
               public virtual void Simulation_Initialize()
               /* -------------------------------------------
                * Initializes a Simulation.  Simulation can be run in two ways, 
                *     calling SimulationNextYear() for each year to be run
                *     call SimulationAllYears() which runs all the years
                *     call fails if Simulation is locked
                * All simulations should be stopped with StopSimulation(), whihc will make sure all files are closed
                * --------------------------------------------*/
               {
                       SimulationLock = false;
                       _inRun = false;
                       _CurrentYear = 0;
       
                       if (_simulationStarted) { Simulation_Stop(); }
                       // call model reset
                       _ws.ResetAll();
                       // Initiallize defaults
                       InitDefaultVariables();  // Sets Variables to Default Values
                       // clear process errors
                       _ProcessManager.ProcessInitializeAll(this);// ClearProcessErrors();
                       // tell model and API we are ready tio start
                       _ws.StartSimulation = true;
                       _simulationStarted = true;
                       // Ready but not yet running
               }
               //----------------------------------------------------------------------------------------------------
               bool _inSimulationNextYear = false;
               /// <summary>
               /// Runs the next year in a series of years in a simulation, no pre or post process is evoked 
               /// </summary>
               /// <returns>The year of the simulation run</returns>
               
               public virtual int Simulation_NextYear()
               /* --------------------------------------------------------
                * Returns the year run, if has already run last year, returns 0;
                * Calls close files afer running last year
               ----------------------------------------------------------- */
               {
                   int runyear = 0;
                   if ((_simulationStarted)&(!_inSimulationNextYear))
                   {
                       // Keep from rentry
                       _inSimulationNextYear = true;
                       // Save Lock State and lock this down
                       bool locked = isLocked();
                       LockSimulation();
                       // Check if this is the first year
                       if (!_inRun)
                       {
                           // OK We are starting, set the STart and End years
                           _StartYear = _CurrentYear = Simulation_Start_Year;
                           _EndYear = Simulation_End_Year;
                           _NumberOfYears = (_EndYear - _StartYear)+1;

                           _inRun = true;
                       }
                       else
                       // OK, Not the first yeat, inc the year;
                           { _CurrentYear++; }
                       // OK we should never get to this code, but to be safe, check of we have already called the last year, if so, lets shut it down
                       if (_CurrentYear > _EndYear)
                       {
       
                           _simulationStarted = false;
                           runyear = 0;
                       }
                       else
                       {
                           //OK run a year
                           // do Process Start for first year and PreProcess for all other years
                           // Unlock to allo parmeters to be set
                           UnLockSimulation();
                           if (_StartYear != _CurrentYear)
                           {
                               _ProcessManager.PreProcessAll(_CurrentYear, this);
                           }
                           else
                           {
                               _ProcessManager.ProcessStartedAll(_CurrentYear, this);
                           }
                           // Lock it back up
                           LockSimulation();;
                           // OK run Model
                           // DAS 06.15.14
                           //if (RunManyYears)
                           //{
                           //    runManyYears(_CurrentYear);
                           //}
                           //else
                           //{
                               runYear(_CurrentYear);
                           //}
                           // Set STartSimulatio to false
                           _ws.StartSimulation = false;
                           // do PostProcess
                           // Simulation is locked for this, ie can not change parameters
                           _ProcessManager.PostProcessAll(_CurrentYear,this);
                           // Return the year just done
                           runyear = _CurrentYear;
                       }
                       SimulationLock = locked;
                   }
                   _inSimulationNextYear = false;
                   return runyear;
               }
               //----------------------------------------------------------------
       
               /// <summary>   Executes the year operation. </summary>
               /// <remarks>This has gone back and forth from protected to internal back again, might change in future</remarks>
               /// <param name="year"> The year. </param>
       
               internal virtual void runYear(int year)
               // Iternal routine Runs one more year, call groundwater
               // Fast as possible, no error checking, no reentry block
               {
                   // call model for one year
                   _ws.RunOneYear();
                   // clean up
                   _ws.GroundWater(year);
               }
               // DAS 06.15.14
               //internal virtual void runManyYears(int year)
               //// Iternal routine Runs one more year, call groundwater
               //// Fast as possible, no error checking, no reentry block
               //{
               //    // call model for one year
               //    _ws.RunManyYears();
               //    // clean up
               //    _ws.GroundWater(year);
               //}


               //----------------------------------------------------------------------------------
               bool _inSimulationAllYears = false;
               /// <summary>
               /// Runs all years of the specified simulation
               /// </summary>
               /// <remarks>Runs each year.  Before each year, except the first, it calls a preprocess processes.  After each year, including the last it calls all postprocess processes.  No data is retained.  If a post process does not collect data fater each year, the only output data available will be that for the last year run.</remarks>
               public virtual void Simulation_AllYears()
               {
                   
                   if ((_simulationStarted)&(!_inSimulationAllYears))
                   {
                       _inSimulationAllYears = true;
                       _StartYear = _CurrentYear = Simulation_Start_Year;
                       _EndYear = Simulation_End_Year;
                       _inRun = true;
                       foreach (int year in simulationYears())
                       {    // do PreProcess, unless first year then do ProcessStarted
                           // Unlock things first
                           UnLockSimulation();
                           if (_StartYear != _CurrentYear)
                           {
                               _ProcessManager.PreProcessAll(_CurrentYear, this);
                           }
                           else
                           {
                               _ProcessManager.ProcessStartedAll(_CurrentYear, this);
                           }
                           // lock it back up
                           LockSimulation();
                           //if (RunManyYears)
                           //{
                           //    runManyYears(_CurrentYear);
                           //    break;
                           //}
                           //else
                           //{
                               runYear(year);
                           //}
                           // OK, Not the first yeat, inc the year;
                           _CurrentYear++; 
                           // do PostProcess
                           _ProcessManager.PostProcessAll(_CurrentYear,this);
                       }
                       Simulation_Stop();
                       _inSimulationAllYears = false;
                   }
               }
               //----------------------------------------------------------------------------------
               bool _instopSimulation = false;
       
               /// <summary>   Stops simulation stop </summary>
               /// <remarks> Unlocks simulation, closes model files, unlocks base input parameters</remarks>
               public virtual void Simulation_Stop()
               // Stops RunByYear() closes out simulation.
               {
                   if (!_instopSimulation)
                   {
                       _instopSimulation = true;
                       // if all years not run, run out the model
                       while (_CurrentYear < Simulation_End_Year)
                       {
                           _ws.RunOneYear();
                           _CurrentYear++;
                       }
                       
                       // Commented out 8/5/13 When model runs last year, see abopve all files closed
                       // Close the model files
                       // _ws.CloseFiles();
                      
                       _ProcessManager.StopProcessAll(this);
                       // Unset lock, recurrence, and in simulation flags
                       
                        FModelLocked = false;
                       _inRun = false;
                       _simulationStarted = false;
                       _instopSimulation = false;
                   }
               }
       
               #endregion
               //----------------------------------------------------------------------------------
               // TESTING
               internal bool testParmeters(ref string Results, bool debug)
               {
                   return _pm.testModelParameters(ref Results);//,debug);
               }
               //----------------------------------------------------------------------------------
               internal bool testParmeters()
               {
                   string junk = " ";
                   return _pm.testModelParameters(ref junk);//,false);
               }
       
               //
               //======================================================
               // Model Parameters
               // =====================================================
               //
               #region Model Parameters

               // OutPut Properties for Model Variables  OutPut after model year run
       

               //************************************
               // BASE OUTPUT PARAMETERS
               // ************************************

               #region Base output Parameters

               // Regional  Water Supply Data =========================
               //---------------------------------------
               //get_ColoradoRiverFlow
       
               /// <summary>   Gets the colorado river flow. </summary>
               ///<remarks>The total annual flow in the Colorado River above Lake Powell. Units AF</remarks>
               /// <value> The colorado river flow. </value>
       
               public int Colorado_River_Flow
               { get { return _ws.get_ColoradoRiverFlow; } }
       
               //---------------------------------------
               //get_PowellStorage
       
               /// <summary>   Gets the powell storage. </summary>
               /// <value> The powell storage. </value>
               ///<remarks>The total water storage in Lake Powell. Units maf</remarks>
       
               public int Powell_Storage
               { get { return _ws.get_PowellStorage; } }
       
               //---------------------------------------
               //get_MeadStorage
       
               /// <summary>   Gets the mead storage. </summary>
               /// <value> The mead storage. </value>
               ///<remarks>The total annual water storage in Lake Mead Units maf</remarks>
       
               public int Mead_Storage
               { get { return _ws.get_MeadStorage; } }
       
               //---------------------------------------
               //get_SaltVerdeRiverFlow
       
               /// <summary>   Gets the salt verde river flow. </summary>
               /// <value> The salt verde river flow. </value>
               ///<remarks>The total annula flow of the Salt and Verde Rivers. Units AF</remarks>
       
               public int SaltVerde_River_Flow
               { get { return _ws.get_SaltVerdeRiverFlow; } }
       
               //---------------------------------------
               // DAS
               public int Roosevelt_Storage
               { get { return _ws.get_RooseveltStorage; } }
               public int SaltOther_Storage
               { get { return _ws.get_OtherSaltStorage; } }
               public int Verde_Storage
               { get { return _ws.get_VerdeStorage; } }

       
               /// <summary>   Gets the salt verde storage. </summary>
               /// <value> The salt verde storage. </value>
               ///<remarks>The total annual storage in the Salt River Project reservoirs. Units maf</remarks>
       
               public int SaltVerde_Storage
               { get { return _ws.get_SaltVerdeStorage; } }
       
               //---------------------------------------
               //get_EffluentToAgriculture
       
               /// <summary>   Gets the effluent to agriculture. </summary>
               /// <value> Units AF. </value>
               ///<remarks>The total amount of wastewater effluent delivered to agriculural users. Units AF</remarks>
       
               public int Effluent_To_Agriculture
               { get { return _ws.get_EffluentToAgriculture; } }
       
       
               //-------------------------------------------------------
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary> Gets the annual spillage over Granite Reef. </summary>
               /// <value> The svt spillage. Units Af</value>
               ///<remarks>The total amount of water that is spilled from Granite Reef in the year. Units AF</remarks>
               ///-------------------------------------------------------------------------------------------------
       
               public int SVT_Spillage
               { get { return _ws.get_SaltVerdeSpillage; } }
       
               //----------------------------------------------------------------------------
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary> Gets the elevation ofLake Mead. </summary>
               /// <value> The elevation of mead in feet. </value>
               ///<remarks> The Elevation of Lake Mead at the end of the year.  Units Feet</remarks>
               ///-------------------------------------------------------------------------------------------------
       
               public int Elevation_of_Mead
               { get { return _ws.get_MeadElevation; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary> Gets the elevation ofLake Mead. </summary>
               /// <value> The elevation of mead in feet. </value>
               ///<remarks> The Elevation of Lake Mead at the end of the year.  Units Feet</remarks>
               ///-------------------------------------------------------------------------------------------------
               public int Elevation_of_Powell
               { get { return _ws.get_PowellElevation; } }
              
               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional natural recharge. </summary>
               ///
               /// <value>  The regional natural recharge. Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Natural_Recharge
               { get { return _ws.get_RegionalNaturalRecharge; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional cagrd recharge. </summary>
               ///
               /// <value>  The regional cagrd recharge. Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_CAGRD_Recharge
               { get { return _ws.CAGRD; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional inflow. </summary>
               ///
               /// <value>  The regional inflow. Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Inflow
               { get { return _ws.get_RegionalInflow; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional ag to vadose. </summary>
               ///
               /// <value>  The regional ag to vadose flux. Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Ag_To_Vadose
               { get { return _ws.get_RegionalAgToVadoseFlux; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional provider recharge. </summary>
               ///
               /// <value>  The total amount of recharge from all providers.  Units AF</value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Provider_Recharge
               { get { return _ws.get_RegionalProviderRecharge; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional ag other pumping. </summary>
               ///
               /// <value>  The regional pumping from agriculture and other (non municiple ) uses. . Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Ag_Other_Pumping
               { get { return _ws.get_RegionalAgOtherPumping; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional outflow. </summary>
               ///
               /// <value>  The regional natural aquifer outflow. . Units AF</value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Outflow
               { get { return _ws.get_RegionalOutflow; } }

               ///-------------------------------------------------------------------------------------------------
               /// <summary>    Gets the regional groundwater balance. </summary>
               ///
               /// <value>  The balance of the regional groundwater aquifer (estimate, assumes all recharge is in aquifer). Units AF </value>
               ///-------------------------------------------------------------------------------------------------

               public int Regional_Groundwater_Balance
               { get { return _ws.get_RegionalGroundWaterBalance; } }

               #endregion

               // 
               // =========================================================
               // Provider Outputs
               //========================================================== 
               // 

               #region Provider Outputs

 

               //---------------------------------------
               /// <summary>
               /// Gets the original Raw GPCD curves
               /// </summary>
               public providerArrayProperty GPCD_raw;
               internal int[] get_GPCDraw() { return _ws.get_ProviderGPCDraw; }

               //---------------------------------------
               private int[] get_TWWTP_Created()
               { return _ws.get_TotalTWWTP; }

               /// <summary> The reclaimed water created </summary>
               ///<remarks>The total annual amount of reclaimed water created, used and discharged. Units AF</remarks>

               public providerArrayProperty Total_WWTP_Effluent;

       
               //---------------------------------------
               // get_ProviderGWPumpedMunicipal
               private int[] get_Groundwater_Pumped_Municipal()
               { return _ws.get_ProviderGWPumpedMunicipal; }  
       
               /// <summary> The groundwater pumped municipal </summary>
               ///<remarks>The total amount of annual groundwater pumped. Units AF</remarks>
       
               public providerArrayProperty Groundwater_Pumped_Municipal;
       
               //---------------------------------------
               //ProviderGroundwater
               private int[] get_Groundwater_Balance()
               //{ return _ws.ModelGroundwater; }  Changed based on new credit model
               { return _ws.get_WaterCreditTotals; }
               /// <summary> The groundwater balance </summary>
               ///<remarks>The total groundwater supply available at end of year. Units AF.  
               ///         A call to this method before the first year has been run returns 0 values</remarks>
       
               public providerArrayProperty Groundwater_Balance;
               
               //---------------------------------------
               //get_SVTAnnualDeliveriesSRP
               private int[] get_SaltVerde_Annual_Deliveries_SRP()
               { return _ws.get_SVTAnnualDeliveriesSRP; }
       
               /// <summary> The salt verde annual deliveries srp </summary>
               ///<remarks>The total annual surface water and pumped groundwater delivered by SRP. Units AF</remarks>
               /// <seealso cref="Groundwater_Bank_Used"/>
               /// 
               public providerArrayProperty SaltVerde_Annual_Deliveries_SRP;
       
               //---------------------------------------
               //get_SaltVerdeClassBCDesignations
               private int[] get_SaltVerde_Class_BC_Designations()
               { return _ws.get_SaltVerdeClassBCDesignationsUsed; }
               // Quay Changed to get_SaltVerdeClassBCDesignationsUsed from get_SaltVerdeClassBCDesignations 3/6/13
       
               /// <summary> The salt verde class bc designations </summary>
               ///<remarks>The total annual B / C designated surface water delivered by SRP. Units AF</remarks>
        
               public providerArrayProperty SaltVerde_Class_BC_Designations;
       
               //---------------------------------------
               //get_ColoradoAnnualDeliveries
               private int[] get_Colorado_Annual_Deliveries()
               { return _ws.get_ColoradoAnnualDeliveries; }
       
               /// <summary> The colorado annual deliveries </summary>
               ///<remarks>The total annual surface water deliveries by CAP, does not included banked water. Units AF</remarks>
               /// <seealso cref="Groundwater_Bank_Used"/>
               public providerArrayProperty Colorado_Annual_Deliveries;
       
               //---------------------------------------
               //get_GroundwaterBankUsed
               private int[] get_Groundwater_Bank_Used()
               { return _ws.get_GroundwaterBankUsed; }
       
               /// <summary> The groundwater bank used </summary>
               ///<remarks>The total annual amount of water delivered from water banking facilities.  These ground water facilities are assumed to be physically  outside the providers groundwater assets and would be delivered from these remote facilities to the provider.  Some of this may be delivered through the CAP or SRP canals but is not icnluded in SRP or CAP totals. Units AF</remarks>
               /// <seealso cref="SaltVerde_Annual_Deliveries_SRP"/>
               /// <seealso cref="Colorado_Annual_Deliveries"/>
               public providerArrayProperty Groundwater_Bank_Used;
       
               //---------------------------------------
               //get_GroundwaterBankBalance
               private int[] get_Groundwater_Bank_Balance()
               { return _ws.get_GroundwaterBankBalance; }
       
               /// <summary> The groundwater bank balance </summary>
               ///<remarks>The total banked water supply available at end of year. Units AF</remarks>
       
               public providerArrayProperty Groundwater_Bank_Balance;
       
               //---------------------------------------
               //get_ReclaimedWaterCreated
               private int[] get_Reclaimed_Water_Created()
               { return _ws.get_ReclaimedWaterCreated; }
       
                
               /// <summary> The reclaimed water created </summary>
               ///<remarks>The total annual amount of reclaimed water created, used and discharged. Units AF</remarks>
       
               public providerArrayProperty Reclaimed_Water_Created;
       
               //---------------------------------------
               //get_ReclaimedWaterUsed
               private int[] get_Reclaimed_Water_Used()
               { return _ws.get_ReclaimedWaterUsed; }
       
               /// <summary> The reclaimed water used </summary>
               ///<remarks>The total annual amount of reclaimed water used. Units AF</remarks>
               ///<see cref="Reclaimed_Water_Created"/>
       
               public providerArrayProperty Reclaimed_Water_Used;
       
               //---------------------------------------
               //get_ReclaimedWaterToVadose
               private int[] get_Reclaimed_Water_To_Vadose()
               { return _ws.get_ReclaimedWaterToVadose; }
       
               /// <summary> The reclaimed water to vadose </summary>
               ///<remarks>The annual amount of reclimed water used for vadose zone recharge. Units AF</remarks>
       
               public providerArrayProperty Reclaimed_Water_To_Vadose;
       
               //---------------------------------------
               //get_ReclaimedWaterDischarged
               private int[] get_Reclaimed_Water_Discharged()
               { return _ws.get_ReclaimedWaterDischarged; }
       
               /// <summary> The reclaimed water discharged </summary>
               public providerArrayProperty Reclaimed_Water_Discharged;
       
               //---------------------------------------
               //get_ReclaimedWaterDirectInject
               private int[] get_Reclaimed_Water_to_DirectInject()
               { return _ws.get_ReclaimedWaterDirectInject; }
       
               /// <summary> The reclaimed water to direct inject </summary>
               public providerArrayProperty Reclaimed_Water_to_DirectInject;
       
               //---------------------------------------
               //get_ROreclaimedWaterCreated
               private int[] get_RO_Reclaimed_Water_Created()
               { return _ws.get_ROreclaimedWaterCreated; }
       
               /// <summary> The ro reclaimed water created </summary>
               public providerArrayProperty RO_Reclaimed_Water_Created;
       
               //---------------------------------------
               //get_ROreclaimedWaterUsed
               private int[] get_RO_Reclaimed_Water_Used()
               { return _ws.get_ROreclaimedWaterUsed; }
       
               /// <summary> The ro reclaimed water used </summary>
               public providerArrayProperty RO_Reclaimed_Water_Used;
       
               //---------------------------------------
               //get_ROreclaimedWaterDirectInject
               private int[] get_RO_Reclaimed_Water_to_DirectInject()
               { return _ws.get_ROreclaimedWaterDirectInject; }
       
               /// <summary> The ro reclaimed water to direct inject </summary>
               public providerArrayProperty RO_Reclaimed_Water_to_DirectInject;
       
               //---------------------------------------
               //get_EffluentCreated
               private int[] get_Total_Effluent_Reused()
               { return _ws.get_TotalEffluentReused; }
       
               /// <summary> The effluent created </summary>
               ///<remarks>The total annual amount of wastewater effluent produced. Units </remarks>
       
               public providerArrayProperty Total_Effluent_Reused;
       
               //---------------------------------------
               //get_EffluentToVadose
               private int[] get_Effluent_To_Vadose()
               { return _ws.get_EffluentToVadose; }
       
               /// <summary> The effluent to vadose </summary>
               ///<remarks>The annual amount of reclaimed water used for vadose zone recharge. Units AF</remarks>
       
               public providerArrayProperty Effluent_To_Vadose;
       
               //---------------------------------------
               //get_EffluentToPowerPlant
               private int[] get_Effluent_To_PowerPlant()
               { return _ws.get_EffluentToPowerPlant; }
       
               /// <summary> The effluent to power plant </summary>
               ///<remarks>The annual amount of effluent delivered to power plants. Units AF</remarks>
       
               public providerArrayProperty Effluent_To_PowerPlant;
       
               //---------------------------------------
               //get_EffluentDischarged
               private int[] get_Effluent_Discharged()
               { return _ws.get_EffluentDischarged; }
       
               /// <summary> The effluent discharged </summary>
               ///<remarks>The annual amount of wastewater effluent discharged to a water course (envirionment). Units AF</remarks>
       
               public providerArrayProperty Effluent_Discharged;
       
 
   

               //  get_WaterCreditIncidental
               internal int[] get_Incidental_Water_Credit()
               { return _ws.get_WaterCreditIncidental; }

               /// <summary> The amount of annual incendental recharge credit by provider</summary>
               ///<remarks>The annual amount of incidental water use that is credited annually as recharge to the aquifer. Units AF</remarks>

               public providerArrayProperty Incidental_Water_Credit;

               //  get_VadoseToAquiferFlux
               internal int[] get_Total_Vadose_To_Aquifer_Flux()
               { return _ws.get_VadoseToAquiferFlux; }

               /// <summary> The amount of annual incendental recharge credit by provider</summary>
               ///<remarks>The annual amount of incidental water use that is credited annually as recharge to the aquifer. Units AF</remarks>

               public providerArrayProperty Total_Vadose_To_Aquifer;
               

               // On-Project Population - Provider level:  internal int[] get_PopulationOnProject
               private int[] get_Population_OnProject()
               { return _ws.get_PopulationOnProject; }
               /// <summary> The population project. </summary>
               public providerArrayProperty Population_On_Project;
       
               // On-Project Population - Provider level:  internal int[] get_PopulationOnProject
               private int[] get_Population_Other()
               { return _ws.get_PopulationOther; }
               /// <summary> The population other. </summary>
               public providerArrayProperty Population_Other;
       
               //---
               // ------------------------------------
               //get_DemandDeficit
               private int[] get_Demand_Deficit()
               { return _ws.get_DemandDeficit; }
       
               /// <summary> The demand deficit </summary>
               ///<remarks>The annual difference between demand and supply (demand - supply), 0 if supply is larger than demand. Units AF.  This is a good candidate as a indicator parameter.  
               ///		   There are a number of management actions that can be taken, and reflected in the model parameters to manage water supplies and demand.  
               ///		   A community's demand exceeding supply is an indicator of either a non-sustainable water resources budget or inadequate management of available water supplies and demand.    
               ///		   Both indicate an unresolved  water resource issue for the community.  Currently there is no feedback in the model to reduce growth (thus demand growth) for a community when a demand deficit occurs, nor a feedback to allocate this growth to other providers.</remarks>
          
               public providerArrayProperty Demand_Deficit;
       
               //---------------------------------------
               //ProviderGPCD
               private int[] get_GPCD_Used()
               { return _ws.ProviderGPCD; }
       
               /// <summary> The gpcd used </summary>
               ///<remarks>The GPCD used to estimate demand for the completed simulation year. When Provider_Demand_Option is 1,2, or 3, this is the calculated GPCD used to estimate demand.  
               ///		   if the Provider_Demand_Option =4, this is the GPCD specified by Use_GPCD parameter. Units: gpcd-gallons per capita per daya of water use</remarks>
               /// <seealso cref="Provider_Demand_Option"/>
               /// <seealso cref="Use_GPCD"/>
               public providerArrayProperty GPCD_Used;
       
               //---------------------------------------
               //TotalDemands
               private int[] get_Total_Demand()
               { return _ws.get_WaterDemand; }  //  { return _ws.TotalDemands; }
       
               /// <summary> The total demand </summary>
               ///<remarks>The total annual demand from all water customers. Units AF.  This calculated based on provider demand</remarks>
       
               public providerArrayProperty Total_Demand;
       
               //---------------------------------------
               //Population 
               private int[] get_Population_Used()
               { return _ws.get_Populations; }
       
               /// <summary> The population used </summary>
               ///<remarks>The population used to estimate demand for the completed simulation year. Units people</remarks>
       
               public providerArrayProperty Population_Used;
       
              //----------------------------------
               // Off Project Demand
               private int[] get_Demand_Off_Project()
               { return _ws.get_WaterDemandOther; }
       
               /// <summary> The demand off project. </summary>
               public providerArrayProperty Demand_Off_Project;
       
              //----------------------------------
               // Off Project Demand
               private int[] get_Demand_On_Project()
               { return _ws.get_WaterDemandOnProject; }
               /// <summary> The demand on project. </summary>
               public providerArrayProperty Demand_On_Project;

               #endregion

               /*****************************************************************
               * Base Input Parameters
               * 
               * ****************************************************************/

               #region Base Input Parameters
               //===============================================================
               // Model Dummy Input Parameters  (REMEMBER TO PUT NEW ONES IN THE DEFAULT INIIALIZE ROUTINE
               private int _Simulation_Start_Year = util.WaterSimDCDC_Default_Simulation_StartYear;
               private int _Simulation_End_Year = util.WaterSimDCDC_Default_Simulation_EndYear;
       
               //---------------------------------------
               //      Start year of simulation
               // Cannot be set while simulation in progress
               // Using shadow value _Simulation_Start_Year;  no get in WaterSimU
               /// <summary>
               /// Simulation_Start_Year
               /// </summary>
               /// <value>The first year of the Simulation.</value>
               /// <remarks> The first year of the Simulation. Range = 2000 to 2006  Cannot be set after Simulation starts.</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               
                public int Simulation_Start_Year
               {
                   set
                   {
                       if ((!_inRun)&(!FModelLocked))                
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSimulation_Start_Year, value);
                           _ws.SimulationStart = value;
                           _Simulation_Start_Year = value;
                       }
                   }
                   get { return _Simulation_Start_Year; }    // 
               }
       
               //---------------------------------------
               //End year of simulation	SimulationEnd
               // Cannot be set while simulation in progress
               // Using shadow value _Simulation_End_Year;  no get in WaterSimU
       
               /// <summary>   Gets or sets the simulation end year. </summary>
               /// <value> The simulation end year. </value>
               ///<remarks> The last year of the Simulation. Range = 2012 to 2085  Cannot be set after Simulation starts.</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               
               public int Simulation_End_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSimulation_End_Year, value);
                           _ws.SimulationEnd = value + 1;  // Added this to force model to run last year;
                           _Simulation_End_Year = value;
                       }
                       // ELSE do we throw an exception? No Just document that it is ignored
                   }
                   get { return _Simulation_End_Year; }
               }
       
               //---------------------------------------
               //Index Year	ColoradoHistoricalExtractionStartYear
               // Cannot be set while simulation in progress
               /// <summary>   Gets or sets the colorado historical extraction start year. </summary>
               /// <value> The colorado historical extraction start year. </value>
               ///<remarks> The first year of the Colorado River flow record that will be used to create a 25 year trace to simulate river flow conditions. Special Range Check Applies See ColoradoYearRangeCheck  Cannot be set after Simulation starts.
               ///	Valid ranges are defined as follows <code>
               ///	       internal RiverRange ColoradoPaleo = new RiverRange(762, 1982);
               ///        internal RiverRange ColoradoBureau = new RiverRange(1906, 1982);</code>		</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="RiverRange"/>
               /// <seealso cref="Colorado_Historical_Data_Source"/>
                
               public int Colorado_Historical_Extraction_Start_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_Historical_Extraction_Start_Year, value);
                           _ws.ColoradoHistoricalExtractionStartYear = value;
                       }
                       // ELSE do we throw an exception, A Message? No Just document that it is ignored
                   }
                   get { return _ws.ColoradoHistoricalExtractionStartYear; }
       
               }
               //---------------------------------------
               //Replace File	ColoradoHistoricalData    =1, paleo data; =2, Bireau of Rec data;=3, scenario data
               // Cannot be set while simulation in progress
               // using shadow value _Colorado_Historical_Data_Source no get in WaterSimU 
       
               /// <summary>   Gets or sets the colorado historical data source. </summary>
               /// <value> The colorado historical data source. </value>
               /// <remarks> The source of the Colorado River flow record: Value 1 uses the Bureau of Reclamation recorded record, Value 2 uses the tree ring reconstructed paleo record, Value 3 uses a user supplied river flow trace record Range = 1 to 3  Cannot be set after Simulation starts.</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
              
               public int Colorado_Historical_Data_Source
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_Historical_Data_Source, value);
                           // ok setting but start and stop may not be set, change these and document
                           ColoradoRiverHistoricalReSetStart(value);
                           _ws.ColoradoHistoricalData = value;
                           // _Colorado_Historical_Data_Source = value; changed 7 24 11
                       }
                   }
                   get { return _ws.ColoradoHistoricalData; } // _Colorado_Historical_Data_Source; } changed 7 24 11
               }
       
               //---------------------------------------
               //Climate Adjustment	ColoradoClimateAdjustmentPercent
       
               /// <summary>   Gets or sets the colorado climate adjustment percent. </summary>
               /// <value> The colorado climate adjustment percent. </value>
               /// <remarks> The percent (Value=50 is 50%) which is used to modify the Colorado river flow record, simulating impacts of climate change.  Change starts at beginning of Simulation. Range = 0 to 300  Cannot be set after Simulation starts.</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
              
               public int Colorado_Climate_Adjustment_Percent
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_Climate_Adjustment_Percent, value);
                           _ws.ColoradoClimateAdjustmentPercent = value;
                       }
                   }
                   get { return _ws.ColoradoClimateAdjustmentPercent; }
               }
               //---------------------------------------
               //Drought-Adjustment	ColoradoUserAdjustmentPercent
       
               /// <summary>   Gets or sets the colorado user adjustment percent. </summary>
               /// <value> The colorado user adjustment percent. </value>
               /// <remarks> The percent (Value=50 is 50%) which is used to modify the Colorado River flow record, starting and stopping in the years specified.  This is used to simulate a drought condition.  Range = 0 to 300  Cannot be set after Simulation starts.</remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="Colorado_User_Adjustment_StartYear"/> 
               /// <seealso cref="Colorado_User_Adjustment_Stop_Year"/> 
       
               public int Colorado_User_Adjustment_Percent
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_User_Adjustment_Percent, value);
                           _ws.ColoradoUserAdjustmentPercent = value;
                       }
                   }
                   get { return _ws.ColoradoUserAdjustmentPercent; }
               }
               //---------------------------------------
               //Drought-Start Year	ColoradoUserAdjustmentStartYear
       
               /// <summary>   Gets or sets the colorado user adjustment start year. </summary>
               /// <value> The colorado user adjustment start year. </value>
              /// <remarks> Determines the year the [Colorado User Adjustment %] will be start being applied.  Range = 2006 to 2081  Cannot be set after Simulation starts.</remarks>  
              /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
              /// <seealso cref="Colorado_User_Adjustment_Percent"/>
               /// <seealso cref="Colorado_User_Adjustment_Stop_Year"/> 
       
               public int Colorado_User_Adjustment_StartYear
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_User_Adjustment_StartYear, value);
                           _ws.ColoradoUserAdjustmentStartYear = value;
                       }
                   }
                   get { return _ws.ColoradoUserAdjustmentStartYear; }
               }
       
       
               //---------------------------------------
               //Drought_Stop year	ColoradoUserAdjustmentStopYear
       
               /// <summary>   Gets or sets the colorado user adjustment stop year. </summary>
               /// <value> The colorado user adjustment stop year. </value>
               /// <remarks> Determines the year the [Colorado User Adjustment %] will be stopped being applied. Range = 2006 to 2081  Cannot be set after Simulation starts.</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="Colorado_User_Adjustment_Percent"/>
               /// <seealso cref="Colorado_User_Adjustment_StartYear"/> 
                
               public int Colorado_User_Adjustment_Stop_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epColorado_User_Adjustment_Stop_Year, value);
                           _ws.ColoradoUserAdjustmentStopYear = value;
                       }
                   }
                   get { return _ws.ColoradoUserAdjustmentStopYear; }
               }
               //---------------------------------------
               //Index Year	SaltVerdeHistoricalExtractionStartYear
       
               /// <summary>   Gets or sets the salt verde historical extraction start year. </summary>
               /// <value> The salt verde historical extraction start year. </value>
               /// <remarks> The first year of the Salt Verde River flow record that will be used to create a 25 year trace to simulate river flow conditions. Special Range Check Applies See SaltVerdeYearRangeCheck  Cannot be set after Simulation starts.  
               ///	Valid ranges are defined as follows <code>
               ///   internal RiverRange SaltVerdePaleo = new RiverRange(1330, 1982);
               ///   internal RiverRange SaltVerdeBureau = new RiverRange(1945, 1982);
               ///   </code></remarks>
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="SaltVerde_Historical_Data_Source"/>
               /// <seealso cref="RiverRange"/>
       
               public int SaltVerde_Historical_Extraction_Start_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_Historical_Extraction_Start_Year, value);
                           _ws.SaltVerdeHistoricalExtractionStartYear = value;
                       }
                   }
                   get { return _ws.SaltVerdeHistoricalExtractionStartYear; }
               }
               //---------------------------------------
               //Replace File	SaltVerdeTontoHistoricalData
       
               /// <summary>   Gets or sets the salt verde historical data source. </summary>
               /// <value> The salt verde historical data source. </value>
               /// <remarks> The source of the Salt Verde Rivers flow record: Value=1 uses the tree ring reconstructed paleo record, Value=2 uses the Bureau of Reclamation recorded record, Value=3 uses a user supplied river flow trace record. Range = 1 to 3  Cannot be set after Simulation starts.</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public int SaltVerde_Historical_Data_Source
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_Historical_Data, value);
                           // ok setting but start and stop may not be set, change these and document
                           SaltVerdeRiverHistoricalReSetStart(value);
                           _ws.SaltVerdeTontoHistoricalData = value;
                           // _SaltVerde_Historical_Data = value;  // changed 7 24 11
                       }
                   }
                   get { return _ws.SaltVerdeTontoHistoricalData; } // _SaltVerde_Historical_Data; } changed 7 24 11
               }
               //---------------------------------------
               //Climate Adjustment	SaltVerdeClimateAdjustmentPercent
       
               /// <summary>   Gets or sets the salt verde climate adjustment percent. </summary>
               ///
               /// <value> The salt verde climate adjustment percent. </value>
                /// <remarks> The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, simulating impacts of climate change.  Change starts at beginning of Simulation. Range = 0 to 300  Cannot be set after Simulation starts.</remarks>  
                 /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public int SaltVerde_Climate_Adjustment_Percent
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_Climate_Adjustment_Percent, value);
                           _ws.SaltVerdeClimateAdjustmentPercent = value;
                       }
                   }
                   get { return _ws.SaltVerdeClimateAdjustmentPercent; }
               }
               //---------------------------------------
               //Drought-Adjustment	SaltVerdeUserAdjustmentPercent
       
               /// <summary>   Gets or sets the salt verde user adjustment percent. </summary>
               /// <value> The salt verde user adjustment percent. </value>
               /// <remarks> The percent (Value=50 is 50%) which is used to modify the Salt Verde River flow record, starting and stopping in the years specified.  This is used to simulate a drought condition.  Range = 0 to 300  Cannot be set after Simulation starts.</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="SaltVerde_User_Adjustment_Start_Year"/>
               /// <seealso cref="SaltVerde_User_Adjustment_Stop_Year"/>
               public int SaltVerde_User_Adjustment_Percent
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_User_Adjustment_Percent, value);
                           _ws.SaltVerdeUserAdjustmentPercent = value;
                       }
                   }
                   get { return _ws.SaltVerdeUserAdjustmentPercent; }
               }
               //---------------------------------------
               //Drought-Start Year	SaltVerdeUserAdjustmentStartYear
       
               /// <summary>   Gets or sets the salt verde user adjustment start year. </summary>
               /// <value> The salt verde user adjustment start year. </value>
                /// <remarks> Determines the year the [SaltVerde User Adjustment %] will be start being applied.  Range = 2006 to 2081  Cannot be set after Simulation starts.</remarks>  
                /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="SaltVerde_User_Adjustment_Stop_Year"/>
               /// <seealso cref="SaltVerde_User_Adjustment_Percent"/>
       
               public int SaltVerde_User_Adjustment_Start_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_User_Adjustment_Start_Year, value);
                           _ws.SaltVerdeUserAdjustmentStartYear = value;
                       }
                   }
                   get { return _ws.SaltVerdeUserAdjustmentStartYear; }
               }
               //---------------------------------------
               //Drought_Stop year	SaltVerdeUserAdjustmentStopYear
       
               /// <summary>   Gets or sets the salt verde user adjustment stop year. </summary>
               /// <value> The salt verde user adjustment stop year. </value>
               /// <remarks> Determines the year the [SaltVerde User Adjustment %] will be stopped being applied. Range = 2006 to 2081  Cannot be set after Simulation starts.</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>"
               /// <seealso cref="SaltVerde_User_Adjustment_Start_Year"/>
               /// <seealso cref="SaltVerde_User_Adjustment_Percent"/>
               public int SaltVerde_User_Adjustment_Stop_Year
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epSaltVerde_User_Adjustment_Stop_Year, value);
                           _ws.SaltVerdeUserAdjustmentStopYear = value;
                       }
                   }
                   get { return _ws.SaltVerdeUserAdjustmentStopYear; }
               }
       
               //---------------------------------------
               //Water demand option	ProviderDemandOption
       
               /// <summary> Demand from file </summary>
               /// <remarks> Use with Provider_Demand_Option <see cref="Provider_Demand_Option"/></remarks> <seealso cref="Provider_Demand_Option"/>
               public const int pdoDemandFromFile = 1;
       
               /// <summary> Average gpcd and pop </summary>
               /// <remarks> Use with Provider_Demand_Option <see cref="Provider_Demand_Option"/></remarks> <seealso cref="Provider_Demand_Option"/>
               public const int pdoAverageGPCDandPOP = 2;
       
               /// <summary> Declining gpcd and pop </summary>
               /// <remarks> Use with Provider_Demand_Option <see cref="Provider_Demand_Option"/></remarks> <seealso cref="Provider_Demand_Option"/>
               public const int pdoDecliningGPCDandPOP = 3;
       
               /// <summary> User gpcd and pop </summary>
               /// <remarks> Use with Provider_Demand_Option <see cref="Provider_Demand_Option"/></remarks> <seealso cref="Provider_Demand_Option"/>
               public const int pdoUserGPCDandPOP = 4;
       
       
               /// <summary>   Gets or sets the provider demand option. </summary>
               ///
               /// <value> The provider demand option. </value>
               /// <remarks> The method that will be used to estimate annual demand  for all providers.  Value=1 reads demand values from an external file, Value=2 calculates demand based on a six year average GPCD and population read from a file, Value=3 estimates demand based on population estimates read from and external file and a smoothing function that slowly declines GPCD, Value=4 uses same method as 3, but allows the user to chnage the GPCD with Use_GPCD Paramtere <see cref="Use_GPCD"/>used for each provider at the beginning or during the simulation. Range = 1 to 4  Cannot be set after Simulation starts.</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="Use_GPCD"/>
       
               public int Provider_Demand_Option
               {
                   set
                   {
                           _pm.CheckBaseValueRange(eModelParam.epProvider_Demand_Option, value);
                           _ws.ProviderDemandOption = value;
                       }
       
                   get { return _ws.ProviderDemandOption;} 
               }
               //===========================================================
       
               /// <summary>   Provider demand option label. </summary>
               /// <param name="value"> provider demand option value. </param>
               /// <returns>a string lable for the provider demand option values   </returns>
       
               public string Provider_Demand_Option_Label(int value)
               {
                   _pm.CheckBaseValueRange(eModelParam.epProvider_Demand_Option, value);
                   switch (value)
                   {
                       case pdoDemandFromFile:
                           return "Demand from file";
       
                       case pdoAverageGPCDandPOP:
                           return "6 Year Average GPCD and Population";
       
                       case pdoDecliningGPCDandPOP:
                           return "Declining GPCD and Population";
       
                       case pdoUserGPCDandPOP:
                           return "User Assigned GPCD";
       
                       default:
                           return "Bad Value";
       
                   }
               }
               //---------------------------------------------------------------------------------------------------------------
               //set_ReduceGPCDpct
               // Fix for no get on GOCD
               int ReduceGPCDValue = util.DefaultReduceGPCDValue;
       
               /// <summary>   Gets or sets the Percent to Reduce GPCD. </summary>
               /// <value> The percent reduction in gpcd. </value>
               /// <remarks>The amount by which GPCD is expected to decrease by the end  of the simulation (i.e., 2085). 
               /// 		 Use this when Provider Demand Option is = 3 or Provider Demand Option=4.</remarks>
               /// <seealso cref="Provider_Demand_Option"/>
               /// 
               public int PCT_Reduce_GPCD
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epPCT_Alter_GPCD, value);
                           _ws.set_AlterGPCDpct = value;
                           // Store is this shadow value becuase model does not have one.
                           ReduceGPCDValue = value;
                       }
                   }
                   get { return ReduceGPCDValue;}
               }
       
       
        
               //---------------------------------------
               // get_ProviderPopGrowthRateAdjustmentPct
               // provides for region wide adjusted growth rate
               // Must be 0 to use provider level pop growth rate adjustement
               //        public void seti_PCT_REG_Growth_Rate_Adjustment_Value(int value) { if (!FModelLocked) _ws.PopulationGrowthRateAdjustmentPercent = value; }
       
               ///-------------------------------------------------------------------------------------------------
               /// <summary>   Gets or sets the regional growth rate adjustment factor. </summary>
               ///
               /// <value> The regional growth rate adjustment factor </value>
               /// <remarks>Adjuses the project growth rate for all providers by this factor.  100 means no change, 50 means 50% less than projection, 150 means 150% more than projected</remarks>
               ///-------------------------------------------------------------------------------------------------
       
               public int Regional_Growth_Rate_Adjust
               {
                   set
                   {
                       /// DAVID Changed 2/24/14
                       //if ((!_inRun) & (!FModelLocked))
                       if (!FModelLocked)
                       {
                           _pm.CheckBaseValueRange(eModelParam.epPCT_REG_Growth_Rate_Adjustment, value);
                           seti_PCT_REG_Growth_Rate_Adjustment(value);
                       }
                       // ELSE do we throw an exception? No Just document that it is ignored
                   }
                   get { return geti_PCT_REG_Growth_Rate_Adjustment(); }
       
               }
       
               //--------------------------------------------------
       
               internal bool _Assured_Water_Supply_Annual_Groundwater_Pumping_Limit = false;

               ///-------------------------------------------------------------------------------------------------
               /// <summary> Gets or sets the assured water supply annual groundwater pumping limit.
               ///  </summary>
               ///
               /// <value> The assured water supply annual groundwater pumping limit. </value>
               ///-------------------------------------------------------------------------------------------------

               public int Assured_Water_Supply_Annual_Groundwater_Pumping_Limit
               {
                   set
                   {
                       if ((!_inRun) & (!FModelLocked))
                       {
                           _pm.CheckBaseValueRange(eModelParam.epAWSAnnualGWLimit, value);
                           seti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit(value);
                       }
                   }
                   get
                   {
                       return geti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit();
                   }
               }
               #endregion

               //=====================================
               // Provider Inputs
               //=====================================

               #region Provider Inputs

               //----------------------------------------------------------------
               /// <summary>
               /// 
               /// </summary>
               public providerArrayProperty Maximum_normalFlow_rights;
               internal int[] get_normalFlow_rights_max()
               { return _ws.NormalFlow_rights_max; }
               internal void set_normalFlow_rights_max(int[] value)
               {
                   if (!FModelLocked) _ws.NormalFlow_rights_max = value;
               }
               //----------------------------------------------------------------

               // QUAY revised 3/16/13
               //
               // DAS revised 03.15.14
                /// <summary>
               /// An Amount of Augmented Water to add to Supply
               /// </summary>
               /// <remarks> Factor used to add additional water from some source external to the region to a providers water supply</remarks>
               public providerArrayProperty WaterAugmentation;
               internal int[] get_NewWater() { return WaterAugmentationMemory; }
               internal void set_NewWater(int[] value)
               {
                   if (!FModelLocked)
                   { 
                       _ws.NewWaterSupplies = value;
                       WaterAugmentationMemory = value;
                   }
               }
            //
               public providerArrayProperty WaterAugmentationUsed;
               internal int[] get_NewWaterUsed() { return _ws.get_NewWaterSuppliesUsed ; }
          

         //
               public providerArrayProperty PCT_alter_GPCD;
               internal void set_alterGPCDpct(int[] value) { if (!FModelLocked)  _ws.AlterProviderGPCDpct = value; }
               internal int[] get_alterGPCDpct() {return _ws.AlterProviderGPCDpct ; }
              
              
               
               // Normal Flow
               internal int[] get_modifyNormalFlow() { return _ws.ModifyProviderNormalFlowPct; }
               internal void set_modifyNormalFlow(int[] value) { if (!FModelLocked)  _ws.ModifyProviderNormalFlowPct = value; }
               
       
               /// <summary> The pct modify normal flow </summary>
               /// <remarks> Factor used to adjust normal flow allocation per acre (SRP)</remarks>
               public providerArrayProperty PCT_modify_normalFlow;
       
       
               //WWtoRWWTP	parmWWtoRWWTPpct
               internal int[] get_PCT_Wastewater_Reclaimed() { return _ws.parmWWtoRWWTPpct; }
               internal void set_PCT_Wastewater_Reclaimed(int[] value)
               {  //pm.CheckValueRange(eModelParam.epPCT_Effluent_Reclaimed, value, true);    
                   if (!FModelLocked) _ws.parmWWtoRWWTPpct = value;
               }
       
               /// <summary> The percent wastewater reclaimed </summary>
                /// <remarks> The percent of wasterwater that is sent to a Reclaim Plant, Range 0 to 100 </remarks>  
                 /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty PCT_Wastewater_Reclaimed;
       
               //---------------------------------------
               //WWtoEffluent	parmWWtoEffluentPct
               internal int[] get_PCT_Wastewater_to_Effluent() { return _ws.parmWWtoEffluentPct; }
               internal void set_PCT_Wastewater_to_Effluent(int[] value) { if (!FModelLocked) _ws.parmWWtoEffluentPct = value; }
       
               /// <summary> The percent wastewater to effluent </summary>
                /// <remarks> The percent of wasterwater effluent that is used and not discharged into a water course. Range = 0 to 100</remarks>  
                /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty PCT_Wastewater_to_Effluent;
       
               //---------------------------------------
               //RWWtoRO	parmReclaimedWWtoROpct
               internal int[] get_PCT_Reclaimed_to_RO() { return _ws.parmReclaimedWWtoROpct; }
               internal void set_PCT_Reclaimed_to_RO(int[] value) { if (!FModelLocked)  _ws.parmReclaimedWWtoROpct = value; }
       
               /// <summary> The percent reclaimed to Reverse Osmosis </summary>
               /// <remarks> The percent of  reclaimed water that is sent to a Reverse Osmosis Plant (becomes potable). Special Range Check Applies See PCTReclaimedRangeCheck</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
               public providerArrayProperty PCT_Reclaimed_to_RO;
       
               //---------------------------------------
               //ROtoOutput	parmROReclaimedToOutputPct
               internal int[] get_PCT_RO_to_Water_Supply() { return _ws.parmROReclaimedToOutputPct; }
               internal void set_PCT_RO_to_Water_Supply(int[] value) { if (!FModelLocked)  _ws.parmROReclaimedToOutputPct = value; }
       
               /// <summary> The percent reverse osmosis water to water supply </summary>
               /// <remarks> The percent of  water from Reverse Osmosis Plant that is used for potable water demand. Range = 0 to 100</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               public providerArrayProperty PCT_RO_to_Water_Supply;
       
               //---------------------------------------
               //RtoDInjection	parmReclaimedToDirectInjectPct
               internal int[] get_PCT_Reclaimed_to_DirectInject() { return _ws.parmReclaimedToDirectInjectPct; }
               internal void set_PCT_Reclaimed_to_DirectInject(int[] value) { if (!FModelLocked)  _ws.parmReclaimedToDirectInjectPct = value; }
               
       
               /// <summary> The percent reclaimed to direct inject </summary>
                /// <remarks> The percent of  reclaimed ater that is used to recharge an aquifer by direct injection into an aquifer. Special Range Check Applies See PCTReclaimedRangeCheck</remarks> 
                /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
                /// <seealso cref="ParameterManagerClass.reclaimedchecklist"/>
               public providerArrayProperty PCT_Reclaimed_to_DirectInject;
       
               //---------------------------------------
               //RtoOutput	parmReclaimedToOutputPct
               internal int[] get_PCT_Reclaimed_to_Water_Supply() { return _ws.parmReclaimedToOutputPct; }
               internal void set_PCT_Reclaimed_to_Water_Supply(int[] value) { if (!FModelLocked)  _ws.parmReclaimedToOutputPct = value; }
       
               /// <summary> The percent reclaimed to water supply </summary>
               /// <remarks> The percent of  recliamed water that is used to meet qualified user demands (non-potable). Special Range Check Applies See PCTReclaimedRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
               /// <seealso cref="ParameterManagerClass.reclaimedchecklist"/>
               public providerArrayProperty PCT_Reclaimed_to_Water_Supply;
       
               //---------------------------------------
               //RtoVadose	parmReclaimedToVadosePct
               internal int[] get_PCT_Reclaimed_to_Vadose() { return _ws.parmReclaimedToVadosePct; }
               internal void set_PCT_Reclaimed_to_Vadose(int[] value) { if (!FModelLocked)  _ws.parmReclaimedToVadosePct = value; }
       
               /// <summary> The percent reclaimed to vadose </summary>
               /// <remarks> The percent of  reclaimed water that is delivered to a vadoze zone recharge basin. Special Range Check Applies See PCTReclaimedRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
               /// <seealso cref="ParameterManagerClass.reclaimedchecklist"/>
               public providerArrayProperty PCT_Reclaimed_to_Vadose;
       
               //---------------------------------------
               //get_EffluentToVadose	parmEffluentToVadosePct
               internal int[] get_PCT_Effluent_to_Vadose() { return _ws.parmEffluentToVadosePct; }
               internal void set_PCT_Effluent_to_Vadose(int[] value) { if (!FModelLocked)  _ws.parmEffluentToVadosePct = value; }
               
       
               /// <summary> The percent effluent to vadose </summary>
               /// <remarks> The percent of  wastewater effluent delivered to a vadose zone recharge basin. Special Range Check Applies See PCTEffluentRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTReclaimedRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
               /// <seealso cref="WaterSimDCDC.ParameterManagerClass.effluentchecklist"/>
               public providerArrayProperty PCT_Effluent_to_Vadose;
       
               //---------------------------------------
               //EffluentToPP	parmEffluentToPowerPlantPct
               internal int[] get_PCT_Effluent_to_PowerPlant() { return _ws.parmEffluentToPowerPlantPct; }
               internal void set_PCT_Effluent_to_PowerPlant(int[] value) { if (!FModelLocked)  _ws.parmEffluentToPowerPlantPct = value; }
               
       
               /// <summary> The percent effluent to power plant </summary>
               /// <remarks> The percent of  wastewater effluent delivered to a power plants for cooling towers. Special Range Check Applies See PCTEffluentRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTEffluentRangeCheck(int, WaterSimDCDC.eProvider, int)"/>
               /// <seealso cref="WaterSimDCDC.WaterSimManager.PCTEffluentRangeCheck(int, WaterSimDCDC.eProvider, ref string, WaterSimDCDC.ModelParameterBaseClass)"/>
               /// <seealso cref="ParameterManagerClass.effluentchecklist"/>
               public providerArrayProperty PCT_Effluent_to_PowerPlant;
       
               //---------------------------------------
               //SWtoVadose	parmSurfaceWaterToVadosePct
               internal int[] get_SurfaceWater__to_Vadose() { return _ws.parmSurfaceWaterToVadoseAmt; } // changed 7 22 11 from get_parmSurfaceWaterToVadosePCT
               internal void set_SurfaceWater__to_Vadose(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.parmSurfaceWaterToVadoseAmt = value;
                       //_ws.WaterToAgriculture_AF = value;
                   }
               
               } // changed 7 22 11 from get_parmSurfaceWaterToVadosePCT
       
               /// <summary> Surface water to vadose </summary>
               /// <remarks> The amount of surface water supply delivered to a vadose zone basin. Range = 0 to 100000</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               public providerArrayProperty SurfaceWater__to_Vadose;
       
               //---------------------------------------
               //SWtoWB	parmSurfaceWaterToWBankPct
               internal int[] get_PCT_SurfaceWater_to_WaterBank() { return _ws.parmSurfaceWaterToWbankPct; }  // _ws.parmSurfaceWaterToWbankPct chnaged in model 7 20 11
               internal void set_PCT_SurfaceWater_to_WaterBank(int[] value) { if (!FModelLocked)  _ws.parmSurfaceWaterToWbankPct = value; }  // _ws.parmSurfaceWaterToWbankPct chnaged in model 7 20 11
               
       
               /// <summary> The percent surface water to water bank </summary>
               /// <remarks> The percent of extra surface water that is sent to a water bank if [WaterBank Source Option] is set to a Value=1. Range = 0 to 100</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               public providerArrayProperty PCT_SurfaceWater_to_WaterBank;
       
               //---------------------------------------
               //    parmSurfaceWaterToWBankAmt
               internal int[] get_Use_SurfaceWater_to_WaterBank()
               { return _ws.parmSurfaceWaterToWBankAmt; }  // changed 7 26 11  API.set_parmSurfaceWaterToWbankAmt; } // Using array of myAPI as Dummy, have to think about this
               internal void set_Use_SurfaceWater_to_WaterBank(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.parmSurfaceWaterToWBankAmt = value;
                   }
               }
               /// <summary> The use surface water to water bank </summary>
               /// <remarks> The amount of water (source unknown) sent to a water bank if [WaterBank Source Option] is set to a Value=2. Range = 0 to 100000.  This parameter is essentially a way to create a new bucket of water that can be used when other supplies do not meet demand.  This could include fallowing of Colorado River farms, desalination of water in Mexico or California, sale of upper basic water rights etc.</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               public providerArrayProperty Use_SurfaceWater_to_WaterBank;
       
               //---------------------------------------
               //WStoRes	parmWaterSupplyToResidentialPct
               internal int[] get_PCT_WaterSupply_to_Residential() { return _ws.parmWaterSupplyToResPct; }
               internal void set_PCT_WaterSupply_to_Residential(int[] value) { if (!FModelLocked)  _ws.parmWaterSupplyToResPct = value; }
       
               /// <summary> The percent water supply to residential </summary>
                /// <remarks> The percent of total water supply used by residential customers. Special Range Check Applies See ResComPCTRangeCheck</remarks> 
                /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="ResComPCTRangeCheck"/>
               /// <seealso cref="ParameterManagerClass.wateruserchecklist"/>
               public providerArrayProperty PCT_WaterSupply_to_Residential;
       
               //---------------------------------------
               //WStoCio	parmWaterSupplyToComPct
               internal int[] get_PCT_WaterSupply_to_Commercial() { return _ws.parmWaterSupplyToComPct; }
               internal void set_PCT_WaterSupply_to_Commercial(int[] value) { if (!FModelLocked)  _ws.parmWaterSupplyToComPct = value; }
       
               /// <summary> The percent water supply to commercial </summary>
               /// <remarks> The percent of  total water supply used by commercial customers. Special Range Check Applies See ResComPCTRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="PCTResComRangeCheck"/>
               /// <seealso cref="ParameterManagerClass.wateruserchecklist"/>
               public providerArrayProperty PCT_WaterSupply_to_Commercial;
       
               //---------------------------------------
               internal int[] get_PCT_WaterSupply_to_Industrial() { return _ws.parmWaterSupplyToIndPct; }
               internal void set_PCT_WaterSupply_to_Industrial(int[] value) { if (!FModelLocked)  _ws.parmWaterSupplyToIndPct = value; }
       
               /// <summary> The percent water supply to industrial uses</summary>
               /// <remarks> The percent of  total water supply used by industiral vustomers. Special Range Check Applies See ResComPCTRangeCheck</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
               /// <seealso cref="PCTResComRangeCheck"/>
               /// <seealso cref="ParameterManagerClass.wateruserchecklist"/>
               public providerArrayProperty PCT_WaterSupply_to_Industrial;
               //
               //---------------------------------------
               ////OutDoorPct	parmOutdoorWaterUsePct
               //internal int[] get_PCT_Outdoor_WaterUse() { return _ws.get_parmOutdoorWaterUsePct; }
               //internal void set_PCT_Outdoor_WaterUse(int[] value) { if (!FModelLocked)  _ws.get_parmOutdoorWaterUsePct = value; }
       
               ///// <summary> The percent outdoor water use </summary>
               ///// <remarks> The percent of  potable water supply used for outdoor wate uses. Range = 0 to 100</remarks> 
               ///// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               //public providerArrayProperty PCT_Outdoor_WaterUse;
       
               //---------------------------------------
               //GWtoGWTP	parmGroundwaterToGWTPlantPct
               internal int[] get_PCT_Groundwater_Treated() { return _ws.parmGroundwaterToGWTPlantPct; }
               internal void set_PCT_Groundwater_Treated(int[] value) { if (!FModelLocked)  _ws.parmGroundwaterToGWTPlantPct = value; }
       
               /// <summary> The percent groundwater treated </summary>
               /// <remarks> The percent of  groundwater that is treated before it is used for potable water supply. Range = 0 to 100</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty PCT_Groundwater_Treated;
       
               //---------------------------------------
               //ReclaimedOutdoor	parmReclaimedOutdoorUsePct
               internal int[] get_PCT_Reclaimed_Outdoor_Use() { return _ws.parmReclaimedOutdoorUsePct; }
               internal void set_PCT_Reclaimed_Outdoor_Use(int[] value) { if (!FModelLocked)  _ws.parmReclaimedOutdoorUsePct = value; }
       
               /// <summary> The percent reclaimed outdoor use </summary>
               /// <remarks> The percent of  outdoor water use that can be supplied by reclaimed water. Range = 0 to 100</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
         
               public providerArrayProperty PCT_Reclaimed_Outdoor_Use;
       
               // New Added to version 3
       
               //---------------------------------------
               // Use_WaterSupply_to_DirectInject 
               internal int[] get_Use_WaterSupply_to_DirectInject()
               { return _ws.parmWaterSupplyToDirectInjectAmt; } // chnaged 7 24 11  API.set_parmWaterSupplyToDirectInjectAmt; } // Using array of myAPI as Dummy, have to think about this
               internal void set_Use_WaterSupply_to_DirectInject(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.parmWaterSupplyToDirectInjectAmt = value;
                   }
               }
       
               /// <summary> The amount of water to direct inject </summary>
               /// <remarks> A fixed amount of potable water supply used for aquifer recharge by directly injecting into a potable aquifer. Range = 0 to 100000</remarks> 
              /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty Use_WaterSupply_to_DirectInject;
       
               //---------------------------------------
               // ***** Changed 8 13 12
               //// SetPopulation  Set_Population  // added 12 19 11 based on changes to model interface
       
               //internal int[] get_populations()
               //{ return SetPopultaionsValues; }
               //internal void set_populations(int [] value)
               //   {
               //    if (!FModelLocked)
               //    {
               //        _ws.SetPopulations = value;
               //        SetPopultaionsValues = value;
               //    }
               //}
       
                 internal int[] get_populationsOn()
                 { return SetPopultaionsValuesOn; }
                 internal void set_populationsOn(int [] value)
                  {
                   if (!FModelLocked)
                   {
                       _ws.set_PopulationsOn = value;
                       SetPopultaionsValuesOn = value;
                   }
               }
              //*********
               /// <summary> Provider On Project Population Override</summary>
               /// <remarks> If set to a value other than 0, is used as the population of the provider, if zero then population calculated by model</remarks>
               // Changed 8 13 12 public providerArrayProperty Population_Override;
               public providerArrayProperty Population_Override_On;
       
               //******** Added 8 13 12
               internal int[] get_populationsOther()
               { return SetPopultaionsValuesOther; }
               internal void set_populationsOther(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.set_PopulationsOther = value;
                       SetPopultaionsValuesOther = value;
                   }
               }
               /// <summary> Provider Off Project  Population Override</summary>
               /// <remarks> If set to a value other than 0, is used as the population of the provider, if zero then population calculated by model</remarks>
               public providerArrayProperty Population_Override_Other;
               //*********
       
       
               // 
               //---------------------------------------
               // TimeLagVadoseYears  Surface_to_Vadose_Time_Lag
               internal int[] get_Surface_to_Vadose_Time_Lag()
               { return _ws.TimeLagVadoseYears; } // changed 7 26 11  API.set_TimeLagVadoseYears; } // Using array of myAPI as Dummy, have to think about this
               internal void set_Surface_to_Vadose_Time_Lag(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.TimeLagVadoseYears = value;
                   }
               }
       
               /// <summary> The surface to vadose time lag </summary>
               /// <remarks> The time in years it takes water recharged to the vadose zone to reach the aquifer used for groundwater supply. Range = 0 to 50</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty Surface_to_Vadose_Time_Lag;
       
               //---------------------------------------
               //ProviderGPCD
               // This is a complicated property depends on how Provider_demand_option is set
       
               internal int[] get_Use_GPCD()
               {
                   if (Provider_Demand_Option == 4)
                       return GPCDBuffer ;
                   else
                       return GPCDDefaults;
               }
              
               internal void set_Use_GPCD(int[] value)
               {
                   if (!FModelLocked)
                   {
                       if (Provider_Demand_Option == 4)
                       {
                           _ws.ProviderGPCD = value;
                           GPCDBuffer = value;
                       }
                       else 
                       {
                          // OK ignore for now -----  throw new WaterSim_Exception(WS_Strings.wsSetGPCDError);
                       }
                   }
               }
       
               /// <summary> The gpcd to use with User Supplied GPCD</summary>
                /// <remarks> The GPCD that will be used if [Provider Demand Option] is set to Value=4. Range = 30 to 500</remarks>  
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check, for now ignoring if Provider_Demand_Option is not set to 4 and values are set</exception>
               /// <seealso cref="Provider_Demand_Option"/>
               public providerArrayProperty Use_GPCD;
       
         
               ////---------------------------------------
               //// get_ProviderPopGrowthRateAdjustPct
               //// this is a bit compliated, in order for this to work, get_ProviderPopGrowthRateAdjustmentPct (see below) must be set to 0, Once set to 0, 
               //// it shold not be set back to 100 until a model run is over, otherwise growth curves will revert back to olld growth curves 
               //internal int[] get_PCT_Growth_Rate_Adjustment() { return _ws.get_ProviderPopGrowthRateAdjustPct; }
               //internal void set_PCT_Growth_Rate_Adjustment(int[] value)
               //{
               //    if (!FModelLocked)
               //    {
               //        // if regional growth factor is not 0 then check if value has non 100 values, if so set regional to 0
               //        //if (geti_PCT_REG_Growth_Rate_Adjustment() != 0)
               //        //{
               //        //    bool found = false;
               //        //    foreach(int pct in value)
               //        //        if (pct != 100)
               //        //        { 
               //        //            found = true;
               //        //            break;
               //        //        }
               //            if (found) seti_PCT_REG_Growth_Rate_Adjustment(0);
               //        //}
               //        _ws.get_ProviderPopGrowthRateAdjustPct = value;
               //    }
               //}
               
               ///// <summary> The percent growth rate adjustment </summary>
               ///// <remarks> A factor (percent) that is used to adjust the rate of population growth.  Used to create faster or slower growth rate scenarios.  100 (100%) means no factor is applied.  A zero means I pack my bags and move to one of the coasts. Range = 0 to 300</remarks> 
               ///// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               //public providerArrayProperty PCT_Growth_Rate_Adjustment;
       
       
               //=============================================================
               // ProviderPopGrowthRateOnProjectPct 
               internal int[] get_PCT_Growth_Rate_Adjustment_OnProject() { return _ws.ProviderPopGrowthRateOnProjectPct; }
               internal void set_PCT_Growth_Rate_Adjustment_OnProject(int[] value)
               {
                   if (!FModelLocked)
                   {
                       //if regional growth factor is not 0 then check if value has non 100 values, if so set regional to 0
                       //if (geti_PCT_REG_Growth_Rate_Adjustment() != 0)
                       //{
                       //    bool found = false;
                       //    foreach(int pct in value)
                       //        if (pct != 100)
                       //        { 
                       //            found = true;
                       //            break;
                       //        }
                       //if (found) seti_PCT_REG_Growth_Rate_Adjustment(0);
                       //}
                       _ws.ProviderPopGrowthRateOnProjectPct = value;
                   }
               }
       
               /// <summary> The percent growth rate adjustment for OnProject Demand</summary>
               /// <remarks> A factor (percent) that is used to adjust the rate of population growth.  Used to create faster or slower growth rate scenarios.  100 (100%) means no factor is applied.  A zero means I pack my bags and move to one of the coasts. Range = 0 to 300</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty PCT_Growth_Rate_Adjustment_OnProject;
       
               //=============================================================
               // ProviderPopGrowthRateOtherPct 
               internal int[] get_PCT_Growth_Rate_Adjustment_Other() { return _ws.ProviderPopGrowthRateOtherPct; }
               internal void set_PCT_Growth_Rate_Adjustment_Other(int[] value)
               {
                   if (!FModelLocked)
                   {
                       //if regional growth factor is not 0 then check if value has non 100 values, if so set regional to 0
                       //if (geti_PCT_REG_Growth_Rate_Adjustment() != 0)
                       //{
                       //    bool found = false;
                       //    foreach (int pct in value)
                       //        if (pct != 100)
                       //        {
                       //            found = true;
                       //            break;
                       //        }
                       //    if (found) seti_PCT_REG_Growth_Rate_Adjustment(0);
                       //}
                       _ws.ProviderPopGrowthRateOtherPct = value;
                   }
               }
       
               /// <summary> The percent growth rate adjustment for Off Project Demand</summary>
               /// <remarks> A factor (percent) that is used to adjust the rate of population growth.  Used to create faster or slower growth rate scenarios.  100 (100%) means no factor is applied.  A zero means I pack my bags and move to one of the coasts. Range = 0 to 300</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty PCT_Growth_Rate_Adjustment_Other;
       
       
               //---------------------------------------
               //_ws.WaterBankingOption = set_WaterBankingOption;
               internal int[] get_WaterBank_Source_Option() { return _ws.WaterBankingOption; } // API.set_WaterBankingOption; } changed 7 24 11
               internal void set_WaterBank_Source_Option(int[] value)
               {
                   if (!FModelLocked)
                   {
                       _ws.WaterBankingOption = value;
                   }
               }
        
               /// <summary> The water bank source option </summary>
               /// <remarks> The source of water used for external water banking (outside provider groundwater): Value=1 a percent [% SurfaceWater to WaterBank] of ""unused"" surface water is sent to a water bank, Value= 2 a fixed amount[Amount of SurfaceWater to WaterBank] of an unknown extra source of water is sent to a water bank. Range = 1 to 2</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>
       
               public providerArrayProperty WaterBank_Source_Option;
       
               // Added to Model 8 10 2011
               //---------------------------------------
               //parmReclaimedToInputPct 
               internal int[] get_PCT_Max_Demand_Reclaim() { return _ws.parmReclaimedToInputPct; }
               internal void set_PCT_Max_Demand_Reclaim(int[] value) { if (!FModelLocked)  _ws.parmReclaimedToInputPct = value; }
       
               /// <summary> The percent maximum demand reclaim </summary>
               /// <remarks> The maximum percent of total demand that can be met by reclaimed water Range = 0 to 70</remarks> 
               /// <exception cref="WaterSim_Exception">if setting a value that does not pass the range check</exception>"
       
               public providerArrayProperty PCT_Max_Demand_Reclaim;

               //-------------------------------------------------------------------------- 
               //OutDoorPct	internal int[] get_parmWaterSupplyToResPct
               // Changed 8 13 12 internal int[] get_PCT_Outdoor_WaterUseRes() { return _ws.get_parmOutdoorWaterUseResPct; } 
               internal int[] get_PCT_Outdoor_WaterUseRes() { return _ws.parmOutdoorWaterUseResPct; }  // changed 8 13 12
               // Changed 8 13 12 internal void set_PCT_Outdoor_WaterUseRes(int[] value) { if (!FModelLocked)  _ws.get_parmOutdoorWaterUseResPct = value; }
               internal void set_PCT_Outdoor_WaterUseRes(int[] value) { if (!FModelLocked)  _ws.parmOutdoorWaterUseResPct = value; }

               /// <summary> The percent outdoor water use for residential </summary>
               /// <remarks> Indoor water use is 100 - PCT_Outdoor_WaterUseRes</remarks>
               public providerArrayProperty PCT_Outdoor_WaterUseRes;

               //-----------------------------------------------------
               //OutDoorPct	internal int[] get_parmWaterSupplyToComPct
               // Changed 8 13 12  internal int[] get_PCT_Outdoor_WaterUseCom() { return _ws.get_parmOutdoorWaterUseComPct; }
               // Changed 8 13 12  internal void set_PCT_Outdoor_WaterUseCom(int[] value) { if (!FModelLocked)  _ws.get_parmOutdoorWaterUseComPct = value; }
               internal int[] get_PCT_Outdoor_WaterUseCom() { return _ws.parmOutdoorWaterUseComPct; }
               internal void set_PCT_Outdoor_WaterUseCom(int[] value) { if (!FModelLocked)  _ws.parmOutdoorWaterUseComPct = value; }

               /// <summary> The pct outdoor water use for commercial </summary>
               /// <remarks> Indoor water use is 100 - PCT_Outdoor_WaterUseCom</remarks>
               public providerArrayProperty PCT_Outdoor_WaterUseCom;

               //OutDoorPct	 :internal int[] get_parmWaterSupplyToIndPct
               // Changed 8 13 12  internal int[] get_PCT_Outdoor_WaterUseInd() { return _ws.get_parmOutdoorWaterUseIndPct; }
               // Changed 8 13 12  internal void set_PCT_Outdoor_WaterUseInd(int[] value) { if (!FModelLocked)  _ws.get_parmOutdoorWaterUseIndPct = value; }
               internal int[] get_PCT_Outdoor_WaterUseInd() { return _ws.parmOutdoorWaterUseIndPct; }
               internal void set_PCT_Outdoor_WaterUseInd(int[] value) { if (!FModelLocked)  _ws.parmOutdoorWaterUseIndPct = value; }

               /// <summary> The pct outdoor water use for industiral </summary>
               /// <remarks> Indoor water use is 100 - PCT_Outdoor_WaterUseInd</remarks>
               public providerArrayProperty PCT_Outdoor_WaterUseInd;

               #endregion

               //==========================================
               // Gets and Sets for Base input and out Properties
               //============================================
                
               #region Gets And Sets for Base Inputs/outputs

               
               // gets for Base outputs
              
               private int geti_Colorado_River_Flow() { return Colorado_River_Flow; }
               private int geti_Powell_Storage() { return Powell_Storage; }
               private int geti_Mead_Storage() { return Mead_Storage; }
               private int geti_SaltVerde_River_Flow() { return SaltVerde_River_Flow; }
               private int geti_SaltVerde_Storage() { return SaltVerde_Storage; }
               private int geti_Effluent_To_Agriculture() { return Effluent_To_Agriculture; }
               internal int geti_SVTspillage() { return SVT_Spillage; }
               internal int geti_ElevationMead() { return Elevation_of_Mead; }
               //DAS
               internal int geti_ElevationPowell() { return Elevation_of_Powell; }
               private int geti_SaltOther_Storage() { return SaltOther_Storage; }
               private int geti_Roosevelt_Storage() { return Roosevelt_Storage; }
               private int geti_Verde_Storage() { return Verde_Storage; }

               internal int geti_Regional_Natural_Recharge() { return Regional_Natural_Recharge; }
               internal int geti_Regional_CAGRD_Recharge() { return Regional_CAGRD_Recharge; }
               internal int geti_Regional_Inflow() { return Regional_Inflow; }
               internal int geti_Regional_Ag_To_Vadose() { return Regional_Ag_To_Vadose;  }
               internal int geti_Regional_Provider_Recharge(){ return Regional_Provider_Recharge; } 
               internal int geti_Regional_Ag_Other_Pumping(){ return Regional_Ag_Other_Pumping;  }
               internal int geti_Regional_Outflow() { return Regional_Outflow;  }
               internal int geti_Regional_Groundwater_Balance() { return Regional_Groundwater_Balance;  }


               // Gets for Base Inputs
               private int geti_Simulation_Start_Year() { return Simulation_Start_Year; }
               private int geti_Simulation_End_Year() { return Simulation_End_Year; }
               private int geti_Colorado_Historical_Extraction_Start_Year() { return Colorado_Historical_Extraction_Start_Year; }
               private int geti_Colorado_Historical_Data_Source() { return Colorado_Historical_Data_Source; }
               private int geti_Colorado_Climate_Adjustment_Percent() { return Colorado_Climate_Adjustment_Percent; }
               private int geti_Colorado_User_Adjustment_Percent() { return Colorado_User_Adjustment_Percent; }
               private int geti_Colorado_User_Adjustment_StartYear() { return Colorado_User_Adjustment_StartYear; }
               private int geti_Colorado_User_Adjustment_Stop_Year() { return Colorado_User_Adjustment_Stop_Year; }
               private int geti_SaltVerde_Historical_Extraction_Start_Year() { return SaltVerde_Historical_Extraction_Start_Year; }
               private int geti_SaltVerde_Historical_Data() { return SaltVerde_Historical_Data_Source; }
               private int geti_SaltVerde_Climate_Adjustment_Percent() { return SaltVerde_Climate_Adjustment_Percent; }
               private int geti_SaltVerde_User_Adjustment_Percent() { return SaltVerde_User_Adjustment_Percent; }
               private int geti_SaltVerde_User_Adjustment_Start_Year() { return SaltVerde_User_Adjustment_Start_Year; }
               private int geti_SaltVerde_User_Adjustment_Stop_Year() { return SaltVerde_User_Adjustment_Stop_Year; }
               private int geti_Provider_Demand_Option() { return Provider_Demand_Option; }
               private int geti_PCT_Reduce_GPCD() { return PCT_Reduce_GPCD; }
               private int geti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit()
               {
                   if (_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit) return 1;
                   else return 0;
               }
               // QUAY Changed 3 7 13 removing Model_Interface PopulationGrowthRateAdjustmentPercent to use Provider Growth Rates
               private int geti_PCT_REG_Growth_Rate_Adjustment()
               {
                   int Total = 0;
                   // Get the On Project Growth Provider array
                   int[] GrowthRates = _ws.ProviderPopGrowthRateOnProjectPct;
                   // add these up
                   foreach (int value in GrowthRates)
                       Total += value;
                   // get off project growth provider array
                   GrowthRates = _ws.ProviderPopGrowthRateOtherPct;
                   // continue adding
                   foreach (int value in GrowthRates)
                       Total += value;
                   // now calculate an un weighted average (since we do not no population of each provider at this point)
                   int AvgRate = Total / (GrowthRates.Length*2);
                   return AvgRate;
               }
               //***************
      
               // Sets for Base Inputs 
               private void seti_Simulation_Start_Year(int value) { Simulation_Start_Year = value; }
               private void seti_Simulation_End_Year(int value) { Simulation_End_Year = value; }
               private void seti_Colorado_Historical_Extraction_Start_Year(int value) { Colorado_Historical_Extraction_Start_Year = value; }
               private void seti_Colorado_Historical_Data_Source(int value) { Colorado_Historical_Data_Source = value; }
               private void seti_Colorado_Climate_Adjustment_Percent(int value) { Colorado_Climate_Adjustment_Percent = value; }
               private void seti_Colorado_User_Adjustment_Percent(int value) { Colorado_User_Adjustment_Percent = value; }
               private void seti_Colorado_User_Adjustment_StartYear(int value) { Colorado_User_Adjustment_StartYear = value; }
               private void seti_Colorado_User_Adjustment_Stop_Year(int value) { Colorado_User_Adjustment_Stop_Year = value; }
               private void seti_SaltVerde_Historical_Extraction_Start_Year(int value) { SaltVerde_Historical_Extraction_Start_Year = value; }
               private void seti_SaltVerde_Historical_Data(int value) { SaltVerde_Historical_Data_Source = value; }
               private void seti_SaltVerde_Climate_Adjustment_Percent(int value) { SaltVerde_Climate_Adjustment_Percent = value; }
               private void seti_SaltVerde_User_Adjustment_Percent(int value) { SaltVerde_User_Adjustment_Percent = value; }
               private void seti_SaltVerde_User_Adjustment_Start_Year(int value) { SaltVerde_User_Adjustment_Start_Year = value; }
               private void seti_SaltVerde_User_Adjustment_Stop_Year(int value) { SaltVerde_User_Adjustment_Stop_Year = value; }
               private void seti_Provider_Demand_Option(int value) { Provider_Demand_Option = value; }
               private void seti_PCT_Reduce_GPCD(int value) { PCT_Reduce_GPCD = value; }
               // QUAY Modified 3 7 13 to Remove old model Regional Growth Adjust
               private void seti_PCT_REG_Growth_Rate_Adjustment(int value)
               {
                   if (!FModelLocked)
                   {
                       int[] NewRates = new int[ProviderClass.NumberOfProviders];
                       for (int i = 0; i < NewRates.Length; i++)
                           NewRates[i] = value;
                       _ws.ProviderPopGrowthRateOnProjectPct = NewRates;
                       _ws.ProviderPopGrowthRateOtherPct = NewRates;
                   }
               }
               //************
               private void seti_Assured_Water_Supply_Annual_Groundwater_Pumping_Limit(int value)
               {
                   bool LimitToggle = false;
                   if (value > 0)
                   {
                       LimitToggle = true;
                   }
                   else
                       LimitToggle = false;
                   _ws.set_parmAllStrawsSucking = LimitToggle;
                   _Assured_Water_Supply_Annual_Groundwater_Pumping_Limit = LimitToggle;
               }
               #endregion

               // Special 
       
               /// <summary>   Fast get annual data.</summary>
               /// <param name="AS_Results">   [out] annual results all output parameters of Simulation. </param>
       
               public void FastGetAnnualData(ref AnnualSimulationResults AS_Results)
               {
                   ModelParameterBaseClass MP;
                   int ib_index = 0;
                   int ob_index = 0;
                   int ip_index = 0;
                   int op_index = 0;
       
                   foreach (int emp in _pm.eModelParameters())
                   {
                       MP = _pm.Model_ParameterBaseClass(emp);
                       switch (MP.ParamType)
                       {
                           case modelParamtype.mptInputBase :
                               AS_Results.Inputs.BaseInput[ib_index] = (MP as ModelParameterClass).Value;  // 7/29 (MP as BaseModelParameterClass).Value;
                               AS_Results.Inputs.BaseInputModelParam[ib_index] = MP.ModelParam;
                               //AS_Results.BaseInput[ib_index] = MP.Value;
                               ib_index++;
                               break;
                           case modelParamtype.mptInputProvider:;
                               AS_Results.Inputs.ProviderInput[ip_index] = MP.ProviderProperty.getvalues();
                               AS_Results.Inputs.ProviderInputModelParam[ip_index] = MP.ModelParam;
                               // AS_Results.ProviderInput.Values[ip_index] = MP.ProviderProperty.getvalues();;
                               ip_index++;
                               break;
                           case modelParamtype.mptOutputBase:
                               AS_Results.Outputs.BaseOutput[ib_index] = (MP as ModelParameterClass).Value;  // 7/29 (MP as BaseModelParameterClass).Value;
                               AS_Results.Outputs.BaseOutputModelParam[ib_index] = MP.ModelParam;
       //                        AS_Results.BaseOutput[ob_index] = MP.Value;
                               ob_index++;
                               break;
                           case modelParamtype.mptOutputProvider:
                               AS_Results.Outputs.ProviderOutput[ip_index] = MP.ProviderProperty.getvalues();
                               AS_Results.Outputs.ProviderOutputModelParam[ip_index] = MP.ModelParam;
                               //AS_Results.ProviderOutput.Values[op_index] = MP.ProviderProperty.getvalues();
                               op_index++;
                               break;
       
                       }
                   }
               }
               // SPecial Range Checks
       
             /// <summary> River range.  </summary>
             /// <remarks> a struct used to define the range in years of a river's flow record</remarks>
             public  class RiverRange
               {
       
                   /// <summary> The first </summary>
                   protected int _First = 0;
       
                   /// <summary> The last </summary>
                   protected int _Last = 0;
        
                   /// <summary> First year of record </summary>
                   /// <value>a year</value>
                   public int First 
                   { get { return _First; } }
       
                   /// <summary> Last year of record </summary>
                   /// <value>a year</value>
                   public int Last
                   { get { return _Last;} }
       
                   internal RiverRange(int first, int last)
                   {
                       _First = first;
                       _Last = last;
                   }
               }
       
               /// <summary>   User river range.  </summary>
               /// <remarks>   A struct used for defining the range of user supplied river flow records. </remarks>
       
               public class UserRiverRange : RiverRange
               {
               
                   /// <summary> First year of record </summary>
                   /// <value>a year</value>
                   new public int First 
                   { 
                       get { return _First; }
                       set { _First = value; }
                   }
       
                   /// <summary> Last year of record </summary>
                   /// <value>a year</value>
                   new public int Last
                   { 
                       get { return _Last;}
                       set { _Last = value; }
                   }
                   internal UserRiverRange(int first, int last) : base(first,last) {}
               }
       
               /// <summary> The Colorado River Paleo River Range</summary>
               /// <seealso cref="RiverRange"/>
               public static RiverRange ColoradoPaleo = new RiverRange(762, 1982);
       
               /// <summary> The Colorado River Bureau/Historical River Range </summary>
               /// <seealso cref="RiverRange"/>
               public static RiverRange ColoradoBureau = new RiverRange(1906, 1982);
       
       
               /// <summary> The Salt Verde Rivers Paleo/Tree Ring River Range </summary>
               /// <seealso cref="RiverRange"/>
               public static RiverRange SaltVerdePaleo = new RiverRange(1330, 1982);
       
               /// <summary> The Salt Verde Bureau/Historical River Range </summary>
               /// <seealso cref="RiverRange"/>
               public static RiverRange SaltVerdeBureau = new RiverRange(1945, 1982);
       
       
               /// <summary> Colorado river range for user supplied data</summary>
               /// <seealso cref="UserRiverRange"/>
               public static UserRiverRange ColoradoUser = new UserRiverRange(1906, 2005);
       
               /// <summary> Salt Verde river range for user supplied data</summary>
               /// <seealso cref="UserRiverRange"/>
               public static UserRiverRange SaltVerdeUser = new UserRiverRange(1945, 2005);
       
       
               /// <summary> Constant for Tree Ring Paleo River Record source </summary>
               /// <value> Riversource </value>
               public const int rsPaleosource = 2;
               /// <summary> Constant for Bureau Historical River Record source </summary>
               /// <value> Riversource </value>
               public const int rsBureausource = 1;
               /// <summary> Constant for User Riversource </summary>
               /// <value> Riversource </value>
               public const int rsUsersource = 3;
               /// <summary> Source of River Trace Information. </summary>
               public static string[] SourceString = new string[] { "Source = 0", "Bureau Records", "Paleo Records", "User Records", "Source = 4", "Source = 5", "Source =6" };
       
               internal const int rcColorado = 1;
               internal const int rcSaltVerde = 2;
              // string[] RiverString = new string[] {"River = 0","Colorado River","Salt and Verde Rivers"};
               //---------------------------------------------------------------------
               internal void ColoradoRiverHistoricalReSetStart(int source)
               {
                   int First = 0; int Last = 0;
                   int start = ParamManager.Model_Parameter(eModelParam.epColorado_Historical_Extraction_Start_Year).Value; // 7/29 ParamManager.BaseModel_ParameterBaseClass(eModelParam.epColorado_Historical_Extraction_Start_Year).Value;
                   if (!RiverHistoricalYearRangeCheck(start,source,rcColorado, ref First, ref Last))
                   {
                       ParamManager.Model_Parameter(eModelParam.epColorado_Historical_Extraction_Start_Year).Value = First; // 7/29 ParamManager.BaseModel_ParameterBaseClass(eModelParam.epColorado_Historical_Extraction_Start_Year).Value = First;
                       ParamManager.Model_Parameter(eModelParam.epColorado_Historical_Extraction_Start_Year).EvokeReloadEvent(); // 7/29 ParamManager.BaseModel_ParameterBaseClass(eModelParam.epColorado_Historical_Extraction_Start_Year).EvokeReloadEvent();
                   }
               }
               //---------------------------------------------------------------------
               internal void SaltVerdeRiverHistoricalReSetStart(int source)
               {
                   int First = 0; int Last = 0;
                   int start = ParamManager.Model_Parameter(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).Value;  // 7/29 ParamManager.BaseModel_ParameterBaseClass(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).Value;
                   if (!RiverHistoricalYearRangeCheck(start, source, rcSaltVerde , ref First, ref Last))
                   {
                       ParamManager.Model_Parameter(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).Value = First;  // 7/ 29 ParamManager.BaseModel_ParameterBaseClass(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).Value = First;
                       ParamManager.Model_Parameter(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).EvokeReloadEvent();  // 7/29  ParamManager.BaseModel_ParameterBaseClass(eModelParam.epSaltVerde_Historical_Extraction_Start_Year).EvokeReloadEvent();
                   }
               }
               //-----------------------------------------------
               internal static string RiverRangeString(int Source, int First, int Last)
               {
                   string rrstr = "";
                   if ((Source > 0) & (Source < SourceString.Length))
                       rrstr = ": " + SourceString[Source] + " (value = " + Source.ToString() + ") start " + First.ToString() + "and end " + Last.ToString();
                   return rrstr;
               }
               //-----------------------------------------------
               internal static bool RiverHistoricalYearRangeCheck(int Value, int source, int River, ref int FirstYear, ref int LastYear)
               {
                   int First = 0;
                   int Last = 0;
                   switch (River)
                   {
                       //------------------------------------
                       case 1:
                           switch (source)
                           {
                               case rsPaleosource:
                                   First = ColoradoPaleo.First;
                                   Last = ColoradoPaleo.Last;
                                   break;
                               //------------------
                               case rsBureausource:
                                   First = ColoradoBureau.First;
                                   Last = ColoradoBureau.Last;
                                   break;
                               //------------------
                               case rsUsersource:
                                   First = ColoradoUser.First;
                                   Last = ColoradoUser.Last;
                                   break;
                               //------------------
                               default:
                                   break;
                               //------------------
                           } // swithc case 1 source
                           break;
                       //------------------------------------
                       case 2:
                           switch (source)
                           {
                               case rsPaleosource:
                                   First = SaltVerdePaleo.First;
                                   Last = SaltVerdePaleo.Last;
                                   break;
                               //------------------
                               case rsBureausource:
                                  First = SaltVerdeBureau.First;
                                  Last = SaltVerdeBureau.Last;
                                   break;
                               //------------------
                               case rsUsersource:
                                  First = SaltVerdeUser.First;
                                  Last = SaltVerdeUser.Last;
                                   break;
                               //------------------
                               default:  
                                   break;
                               //------------------
                           } // switch case 2 source
                           break;
                       //------------------------------------
                       default:
                           break;
                   } // switch source
                   FirstYear = First;
                   LastYear = Last;
                   return  ((Value >= First) & (Value <= Last));
               }
               //-----------------------------------------------
       
               //-----------------------------------------------
              /// <summary>
               /// ColoradoYearRangeCheck
              /// </summary>
              /// <param name="Value">year</param>
              /// <param name="errMessage"></param>
              /// <param name="aModelParameter"></param>
              /// <returns></returns>
               internal bool ColoradoYearRangeCheck(int Value, ref string errMessage, ModelParameterBaseClass aModelParameter)
               {
                   if ((Value == SpecialValues.MissingIntValue) && (errMessage.ToUpper() == "INFO"))
                   {
                        ModelParameterGroupClass GroupCodes = new ModelParameterGroupClass("SaltVerde Range", new  int[2] {eModelParam.epColorado_Historical_Data_Source, aModelParameter.ModelParam} );

                       errMessage = BuildModelParameterGroupFieldList(aModelParameter.ParameterManager, GroupCodes, "Different Range for each source");
                       return true;
                   }
                   else
                   {
                       bool valid = false;
                       errMessage = "";
                       int First = 0;
                       int Last = 0; int source = aModelParameter.ParameterManager.Model_Parameter(eModelParam.epColorado_Historical_Data_Source).Value; // Colorado_Historical_Data_Source;  // 7/29 int Last = 0; int source = aModelParameter.ParameterManager.BaseModel_ParameterBaseClass(eModelParam.epColorado_Historical_Data_Source).Value;
                       valid = RiverHistoricalYearRangeCheck(Value, source, rcColorado, ref First, ref Last);
                       if (!valid) errMessage = "Invalid year of " + Value.ToString() + RiverRangeString(source, First, Last);
                       return valid;
                   }
               }
               //-----------------------------------------------
              /// <summary>
               /// Colorado_Historical_Extraction_Start_Year_RangeCheck
              /// </summary>
              /// <param name="Year">year to check</param>
              /// <returns>true if year is valid, false otherwise </returns>
               public bool Colorado_Historical_Extraction_Start_Year_RangeCheck(int Year)
               {
                   string junk = "";
                   return ColoradoYearRangeCheck(Year, ref junk, ParamManager.Model_ParameterBaseClass(eModelParam.epColorado_Historical_Extraction_Start_Year));
               }
               //-----------------------------------------------
               internal bool SaltVerdeYearRangeCheck(int Value, ref string errMessage, ModelParameterBaseClass aModelParameter)
               {
                   if ((Value == SpecialValues.MissingIntValue) && (errMessage.ToUpper() == "INFO"))
                   {
                       ModelParameterGroupClass GroupCodes = new ModelParameterGroupClass("Salt Verde Range", new  int[2] {eModelParam.epSaltVerde_Historical_Data,aModelParameter.ModelParam});
                       errMessage = BuildModelParameterGroupFieldList(aModelParameter.ParameterManager, GroupCodes, "Different Range for each source");
                       return true;
                   }
                   else
                   {
                       bool valid = false;
                       errMessage = "";
                       int source = aModelParameter.ParameterManager.Model_Parameter(eModelParam.epSaltVerde_Historical_Data).Value;// SaltVerde_Historical_Data_Source;   // 7/29 aModelParameter.ParameterManager.BaseModel_ParameterBaseClass(eModelParam.epSaltVerde_Historical_Data).Value;
                       int First = 0;
                       int Last = 0;
                       valid = RiverHistoricalYearRangeCheck(Value, source, rcSaltVerde, ref First, ref Last);
                       if (!valid) errMessage = "Invalid year of " + Value.ToString() + RiverRangeString(source, First, Last);
                       return valid;
                   }
               }
               //-----------------------------------------------
              /// <summary>
               /// SaltVerde_Historical_Extraction_Start_Year_RangeCheck
              /// </summary>
              /// <param name="Value"></param>
              /// <returns></returns>
               public bool SaltVerde_Historical_Extraction_Start_Year_RangeCheck(int Value)
               {
                   string junk = "";
                   return SaltVerdeYearRangeCheck(Value, ref junk, ParamManager.Model_ParameterBaseClass(eModelParam.epSaltVerde_Historical_Extraction_Start_Year));
               }
               //-----------------------------------------------
               //------------------------------------------------
               static internal string BuildModelParameterGroupFieldList(ParameterManagerClass PM,  ModelParameterGroupClass Group, string Rule)
               {
                   string temp = "{\"ParameterGroup\":[";
                   int cnt = 0;
                   foreach (int mpcode in Group.ModelParameters())
                   {
                       try
                       {
                           ModelParameterClass MP = PM.Model_Parameter(mpcode);
                           if (MP != null)
                           {
                               string fldstr = MP.Fieldname;
                               if (cnt > 0)
                               {
                                   temp += ",";
                               }
                               temp += '\"' + fldstr + '\"';
                               cnt++;
                           }
                       }
                       catch
                       {
                       }
                   }
                   temp += "], \"RULE\":\""+Rule+"\"}";
                   return temp;
               }

               //-----------------------------------------------
               /// <summary>
               /// PCTEffluentRangeCheck
               /// </summary>
               /// <param name="Value"></param>
               /// <param name="provider"></param>
               /// <param name="emp"></param>
               /// <returns></returns>
               public bool PCTEffluentRangeCheck(int Value, eProvider provider, int emp)
               {
                   bool test = false;
                   string junk = "";
                   if (ModelParamClass.valid(emp))
                   {
                   ModelParameterBaseClass MP = ParamManager.Model_ParameterBaseClass(emp);
                   test = PCTEffluentRangeCheck(Value, provider, ref junk, MP);
                   }
                   else
                       throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
                   return test;
               }
              //-----------------------------------------------
       
               /// <summary>PCTEffluentRangeCheck. </summary>
               ///
               /// <remarks>   Ray, 8/20/2011. </remarks>
               ///
               /// <param name="Value">        . </param>
               /// <param name="provider">     . </param>
               /// <param name="errMessage">   [in,out]. </param>
               /// <param name="MP">           The. </param>
               ///
               /// <returns>   true if it succeeds, false if it fails. </returns>
               internal static bool PCTEffluentRangeCheck(int Value, eProvider provider, ref string errMessage, ModelParameterBaseClass MP)
               {
                   if ((Value == SpecialValues.MissingIntValue) && (errMessage.ToUpper() == "INFO"))
                   {
                       errMessage = BuildModelParameterGroupFieldList(MP.ParameterManager, MP.ParameterManager.EffluentGroup,"Equal 100");
                       return true;
                   }
                   else
                   {
                       bool valid = true;
                       if (!FSuspendRangeCheck)
                       {
                           ProviderIntArray currentvalues = new ProviderIntArray(0);
                           currentvalues = MP.ProviderProperty.getvalues();
                           ProviderIntArray CTotals = MP.ParameterManager.EffluentGroup.Totals;
                           int currenttotal = CTotals[provider];
                           int total = currenttotal + Value - currentvalues[(int)provider];
                           valid = ((Value <= 100) & (Value >= 0) & (total <= 100));
                           if (!valid) errMessage = "Total effluent allocated can not exceed 100 (%) " +
                               MP.ParameterManager.eParamGroupMessage(MP.ParameterManager.EffluentGroup, provider);
                       }
                       return valid;
                   }
               }
               //-----------------------------------------------
              /// <summary>
               /// PCTReclaimedRangeCheck - Check to see of all the PCT reclaimed model paramters do nt eceeed 100% total
              /// </summary>
              /// <param name="Value">int: to be checked if violates over 100% rule</param>
              /// <param name="provider">eProvider: who will set the value</param>
              /// <param name="emp">eModelParam: This is the actual ModelParameter that is doing the check.</param>
              /// <returns>bool True if all values are less than or equal to 100, False of the do not</returns>
               public bool PCTReclaimedRangeCheck(int Value, eProvider provider, int emp)
               {
                   bool test = false;
                   string junk = "";
                   if (ModelParamClass.valid(emp))
                   {
                        ModelParameterBaseClass MP = ParamManager.Model_ParameterBaseClass(emp);
                        test = PCTReclaimedRangeCheck(Value, provider, ref junk, MP);
                   }
                   else
                       throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
                   return test;
               }
               //-----------------------------------------------------------------------
               internal static bool PCTReclaimedRangeCheck(int Value, eProvider provider, ref string errMessage, ModelParameterBaseClass MP)
               {
                   if ((Value == SpecialValues.MissingIntValue) && (errMessage.ToUpper() == "INFO"))
                   {
                       errMessage = BuildModelParameterGroupFieldList(MP.ParameterManager, MP.ParameterManager.ReclaimedGroup, "Equal 100");
                       return true;
                   }
                   else
                   {
                       bool valid = true;
                       if (!FSuspendRangeCheck)
                       {
                           ProviderIntArray currentvalues = new ProviderIntArray(0);
                           currentvalues = MP.ProviderProperty.getvalues();
                           int currenttotal = MP.ParameterManager.eParamGroupTotal(MP.ParameterManager.reclaimedchecklist, provider);
                           int total = currenttotal + Value - currentvalues[(int)provider];
                           valid = ((Value <= 100) & (Value >= 0) & (total <= 100));
                           if (!valid) errMessage = "Total reclaimed allocated can not exceed 100 (%) " +
                               MP.ParameterManager.eParamGroupMessage(MP.ParameterManager.ReclaimedGroup, provider);
                       }
                       return valid;
                   }
               }
              //-----------------------------------------------
              /// <summary>
               /// PCTResComRangeCheck - Checks to see if Residential an Commerical water use PCT does not excede 100.
              /// </summary>
               /// <param name="Value">int: to be checked if violates over 100% rule</param>
               /// <param name="provider">eProvider: who will set the value</param>
               /// <param name="emp">eModelParam: This is the actual ModelParameter that is doing the check.</param>
               /// <returns>bool True if all values are less than or equal to 100, False of the do not</returns>
               public bool PCTResComRangeCheck(int Value, eProvider provider, int emp)
               {
                   bool test = false;
                   string junk = "";
                   if (ModelParamClass.valid(emp))
                   {
                       ModelParameterBaseClass MP = ParamManager.Model_ParameterBaseClass(emp);
                       test = ResComPCTRangeCheck(Value, provider, ref junk, MP);
                   }
                   else
                       throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
                   return test;
               }
               
               //-----------------------------------------------

               internal static bool ResComPCTRangeCheck(int Value, eProvider provider, ref string errMessage, ModelParameterBaseClass MP)
               {
                   if ((Value == SpecialValues.MissingIntValue) && (errMessage.ToUpper() == "INFO"))
                   {
                       errMessage = BuildModelParameterGroupFieldList(MP.ParameterManager, MP.ParameterManager.WaterUseGroup, "Equal 100");
                       return true;
                   }
                   else
                   {
                       bool valid = true;
                       if (!FSuspendRangeCheck)
                       {
                           ProviderIntArray currentvalues = new ProviderIntArray(0);
                           currentvalues = MP.ProviderProperty.getvalues();
                           int currenttotal = MP.ParameterManager.eParamGroupTotal(MP.ParameterManager.wateruserchecklist, provider);
                           int total = currenttotal + Value - currentvalues[(int)provider];
                           valid = ((Value <= 100) & (Value >= 0) & (total <= 100));
                           if (!valid) errMessage = "Total water use can not exceed 100 (%) " +
                               MP.ParameterManager.eParamGroupMessage(MP.ParameterManager.WaterUseGroup, provider);
                       }
                       return valid;
                   }
               }
               #endregion
               //------------------------------------------------------------------------
               // STATIC MEMBERS AND CONST
       
       
       
               //---------------------------------------------------------
              /// <summary>
               /// IEnumerable for simulationYears()
              /// </summary>
               /// <returns>a year from range of Simulation_Start_Year to Simulation_End_Year</returns>
               public IEnumerable<int> simulationYears()
               {
                   for (int i = Simulation_Start_Year; i <= Simulation_End_Year; i++)
                   {
                       yield return i;
                   }
               }
          
 
               
            
               //============================================================
           }  // end of WaterSimManager
       
           
           
        
            
       }  // End of WaterSimDCDC
