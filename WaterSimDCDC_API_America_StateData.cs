﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Drawing;
using UniDB;
using ConsumerResourceModelFramework;


namespace WaterSimDCDC.America

{

    /// <summary>   Resource consumer link. </summary>
    public class SDResourceConsumerLink
    {
        string FRes = "";
        string FCons = "";
        string FFlux = "";

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="ResourceField">    The resource field. </param>
        /// <param name="ConsumerField">    The consumer field. </param>
        /// <param name="FluxField">        The flux field. </param>
        ///-------------------------------------------------------------------------------------------------

        public SDResourceConsumerLink(string ResourceField, string ConsumerField, string FluxField)
        {
            FRes = ResourceField;
            FCons = ConsumerField;
            FFlux = FluxField;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the resource. </summary>
        ///
        /// <value> The resource. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Resource
        {
            get { return FRes; }
            set { FRes = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the consumer. </summary>
        ///
        /// <value> The consumer. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Consumer
        {
            get { return FCons; }
            set { FCons = value; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the flux. </summary>
        ///
        /// <value> The flux. </value>
        ///-------------------------------------------------------------------------------------------------

        public string Flux
        {
            get { return FFlux; }
            set { FFlux = value; }
        }
    }

    /// <summary>   State data. </summary>
    public class StateData
    {
        public enum eResource {erSurfaceFresh, erGroundwater, erReclained, erSurfaceSaline,erAugmented};
        public enum eConsumer { ecUrban, ecRural, ecAg, ecInd, ecPower };
 
        DataTable FStateDataTable;
        bool FDataLoaded = false;
        string errMessage = "";
        List<SDResourceConsumerLink> FFluxes = new List<SDResourceConsumerLink>();

        const string StateNameField = "SN";
        // Resources
        const string FGroundWaterFld = "GW";
        const string FSurfaceWaterFld = "SUR";
        const string FReclaimedWaterFld = "REC";
        const string FSaltWaterFld = "SAL";
        const string FAugmentedFld = "AUG";
        // Consumers
        const string FUrbanDemandFld = "UTOT";
        const string FRuralDemandFld = "RTOT";
        const string FAgricultureDemandFld = "ATOT";
        const string FIndustrialDemandFld = "ITOT";
        const string FPowerDemandFld = "PTOT";



        public string[] ResourceList = new string[] { FSurfaceWaterFld, FGroundWaterFld, FReclaimedWaterFld, FSaltWaterFld, FAugmentedFld };
        public string[] ResourceListLabel = new string[] { "Surface Water (Fresh)", "Groundwater", "Reclaimed Water (effluent)", "Surface Water (Saline)", "Augmented (desal or other)" };
        public string[] ConsumerList = new string[] { FUrbanDemandFld, FRuralDemandFld, FAgricultureDemandFld, FIndustrialDemandFld, FPowerDemandFld };
        public string[] ConsumerListLabel = new string[] { "Urban Public Supply Demand", "Non-Urban Residential Demand", "Agricultural Demand", "Industrial Demand", "Power Generation Demand" };

 
        public string[] StateNames = new string[] {"Alabama","Alaska","Arizona","Arkansas","California","Colorado","Connecticut","Dist. of Columbia",
                                                   "Delaware","Florida","Georgia","Hawaii","Idaho","Illinois","Indiana","Iowa","Kansas","Kentucky",
                                                   "Louisiana","Maine","Maryland","Massachusetts","Michigan","Minnesota","Mississippi","Missouri",
                                                   "Montana","Nebraska","Nevada","New Hampshire","New Jersey","New Mexico","New York","North Carolina",
                                                   "North Dakota","Ohio","Oklahoma","Oregon","Pennsylvania","Puerto Rico","Rhode Island","South Carolina",
                                                   "Tennessee","Texas","Utah","Vermont","Virginia","Washington","West Virginia","Wisconsin","Wyoming"};

        Color[] ResColors = new Color[] { Color.Aqua, Color.Blue, Color.Beige, Color.LightSeaGreen };
        Color[] ConsColors = new Color[] { Color.LightGray, Color.LightCoral, Color.DarkGreen, Color.SandyBrown, Color.LightSkyBlue };

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="Filename"> Filename of the file. </param>
        ///-------------------------------------------------------------------------------------------------

        public StateData(string Filename)
        {
            bool isErr = false;
            string Database = Path.GetDirectoryName(Filename);
            string TableName = Path.GetFileName(Filename);
            UniDbConnection DbConnect = new UniDbConnection(SQLServer.stText,"",Database,"","","");
            DbConnect.Open();
            DbConnect.UseFieldHeaders = true;
            FStateDataTable = Tools.LoadTable(DbConnect, TableName, ref isErr, ref errMessage);
            if (isErr)
            {
                FStateDataTable = null;
                FDataLoaded = false;
            }
            else
            {
                FDataLoaded = true;
                FFluxes = ConstructFluxList();
            }
        }

        public string ResourceField(eResource value)
        {
            return ResourceList[(int)value];
        }

        public string ResourceLabel(eResource value)
        {
            return ResourceListLabel[(int)value];
        }

        public string ConsumerField(eConsumer value)
        {
            return ConsumerList[(int)value];
        }

        public string ConsumerLabel(eConsumer value)
        {
            return ConsumerListLabel[(int)value];
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether the data was loaded. </summary>
        ///
        /// <value> true if data loaded, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool DataLoaded
        {
            get { return FDataLoaded; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Construct flux list. </summary>
        ///
        ///-------------------------------------------------------------------------------------------------

        public List<SDResourceConsumerLink> ConstructFluxList()
        {
            List<SDResourceConsumerLink> FluxList = new List<SDResourceConsumerLink>();
            SDResourceConsumerLink Temp;
            Temp = new SDResourceConsumerLink("SUR", "UTOT", "USUR"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SUR", "RTOT", "RSUR"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SUR", "ATOT", "ASUR"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SUR", "ITOT", "ISUR"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SUR", "PTOT", "PSUR"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SAL", "UTOT", "USAL"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SAL", "RTOT", "RSAL"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SAL", "ATOT", "ASAL"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SAL", "ITOT", "ISAL"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("SAL", "PTOT", "PSAL"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("GW", "UTOT", "UGW"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("GW", "RTOT", "RGW"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("GW", "ATOT", "AGW"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("GW", "ITOT", "IGW"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("GW", "PTOT", "PGW"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("REC", "UTOT", "UREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("REC", "RTOT", "RREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("REC", "ATOT", "AREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("REC", "ITOT", "IREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("REC", "PTOT", "PREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("AUG", "UTOT", "UREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("AUG", "RTOT", "RREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("AUG", "ATOT", "AREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("AUG", "ITOT", "IREC"); FluxList.Add(Temp);
            Temp = new SDResourceConsumerLink("AUG", "PTOT", "PREC"); FluxList.Add(Temp);




            return FluxList;
        }

        public List<SDResourceConsumerLink> Fluxes
        {
            get { return Fluxes; }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value. </summary>
        ///
        /// <param name="State">    The state. </param>
        /// <param name="Field">    The field. </param>
        ///
        /// <returns>   The value. </returns>
        ///-------------------------------------------------------------------------------------------------

        public int GetValue(string State, string Field)
        {
            int result = 0;
            if (FDataLoaded)
            {
                if (FStateDataTable.Columns.Contains(Field))
                {
                    bool isErr = false;
                    string errMessage = "";
                    foreach (DataRow DR in FStateDataTable.Rows)
                    {
                        string StateName = DR[StateNameField].ToString().Trim();
                        if (StateName == State)
                        {
                            
                            string ValStr = DR[Field].ToString();
                            //double tempD = Tools.ConvertToDouble(ValStr, ref isErr, ref errMessage);
                            int tempint = Tools.ConvertToInt32(ValStr, ref isErr, ref errMessage);
                            if (!isErr)
                            {
                                result = tempint;
                               // result = Convert.ToInt32(tempD * 1000);
                            }
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public CRF_Network BuildNetwork(string State)
        {
            CRF_Network TheNetwork = null;
            if (FDataLoaded)
            {
                // build resources list
                CRF_ResourceList ResList = new CRF_ResourceList();
                for (int i = 0; i < ResourceList.Length; i++)
                {   
                    int Value = GetValue(State, ResourceList[i]);
                    CRF_Resource TempRes = new CRF_Resource(ResourceList[i], ResourceListLabel[i], ResColors[i], Value);
                    ResList.Add(TempRes);
                }
                // Build Consumer List
                CRF_ConsumerList ConsList = new CRF_ConsumerList();
                for (int i = 0; i < ConsumerList.Length; i++)
                {
                    int Value = GetValue(State, ConsumerList[i]);
                    CRF_Consumer TempCons = new CRF_Consumer(ConsumerList[i], ConsumerListLabel[i], ConsColors[i], Value);
                    ConsList.Add(TempCons);
                }
                // Ok now the hard part, add fluxes
                // Go through each of the resources
                foreach (CRF_Resource Res in ResList)
                {
                    // go through each of the SDResourceConsumerLinks looking for a match
                    foreach (SDResourceConsumerLink RCL in FFluxes)
                    {   
                        // found one
                        if (Res.Name == RCL.Resource)
                        {
                            // lookin for a match consumer
                            foreach (CRF_Consumer Cons in ConsList)
                            {
                                // found it. add this flux
                                if (RCL.Consumer == Cons.Name)
                                {
                                    int Value = GetValue(State, RCL.Flux);
                                    Res.AddConsumer(Cons,Value,CRF_Flux.Method.amAbsolute);
                                }
                            }
                        }
                    }
                }
                TheNetwork = new CRF_Network("WaterSim " + State, ResList, ConsList, null);
            }
            return TheNetwork;
        }
    }

    
}

/*
 * 
    temp = new CombItem("USUR" , "Urban (Public Supply) Surface", new string[] {"PSSF"});  CombList.Add(temp); 
    temp = new CombItem("USAL" , "Urban Surface Saline", new string[] {"PSSS"});  CombList.Add(temp); 
    temp = new CombItem("UGW" , "Urban Groundwater", new string[] {"PSGW"});  CombList.Add(temp); 
    temp = new CombItem("UREC" , "Urban Reclaimed", new string[] {"PSRW"});  CombList.Add(temp); 
    temp = new CombItem("URES" , "Urban Residential", new string[] {"PSDD"});  CombList.Add(temp); 
    temp = new CombItem("UIND" , "Urban Industrial", new string[] {"PSDI"});  CombList.Add(temp); 
    temp = new CombItem("UTOT" , "Urban Total", new string[] {"PSD"});  CombList.Add(temp); 
    temp = new CombItem("UPOP" , "Urban Population Served", new string[] {"PSPOP"});  CombList.Add(temp); 
    temp = new CombItem("RSUR" , "Rural (Self Supplied Residential ) Surface", new string[] {"DMSF"});  CombList.Add(temp); 
    temp = new CombItem("RSAL" , "Rural (Self Supplied Residential ) Surface Saline", new string[] {"DMSS"});  CombList.Add(temp); 
    temp = new CombItem("RGW" , "Rural (Self Supplied Residential ) Groundwater", new string[] {"DMGW"});  CombList.Add(temp); 
    temp = new CombItem("RTOT" , "Rural (Self Supplied Residential ) Total", new string[] {"DMD"});  CombList.Add(temp); 
    temp = new CombItem("ASUR" , "Agriculture Surface", new string[] {"LSSF","LVGS","LASF","AQSF","IRSF"});  CombList.Add(temp); 
    temp = new CombItem("ASAL" , "Agriculture Surface Saline", new string[] {"LSSS","LASS","AQSS"});  CombList.Add(temp); 
    temp = new CombItem("AGW" , "Agriculture Groundwater", new string[] {"LSGW","LVGF","LAGW","AQGW","IRGF"});  CombList.Add(temp); 
    temp = new CombItem("AREC" , "Agriculture Reclaimed", new string[] {"IRRW"});  CombList.Add(temp); 
    temp = new CombItem("ATOT" , "Agriculture Total", new string[] {"LSD","LVD","LAD","AQD","IRD"});  CombList.Add(temp); 
    temp = new CombItem("ISUR" , "Industry / Mining (non urban - self supplied) Surface Water", new string[] {"IDSF","MNSF"});  CombList.Add(temp); 
    temp = new CombItem("ISAL" , "Industry / Mining (non urban - self supplied) Surface Saline", new string[] {"IDSS","MNSS"});  CombList.Add(temp); 
    temp = new CombItem("IGW" , "Industry / Mining  (non urban - self supplied) Groundwater", new string[] {"IDGW","MNGW"});  CombList.Add(temp); 
    temp = new CombItem("IREC" , "Industry / Mining  (non urban - self supplied) Reclaimed", new string[] {"IDRW","MNRW"});  CombList.Add(temp); 
    temp = new CombItem("ITOT" , "Industry / Mining  (non urban - self supplied) Total", new string[] {"IDD","MND"});  CombList.Add(temp); 
    temp = new CombItem("PSUR" , "Power Surface Water", new string[] {"TPSF","HPSF"});  CombList.Add(temp); 
    temp = new CombItem("PSAL" , "Power Surface Saline", new string[] {"TPSS","HPSS"});  CombList.Add(temp); 
    temp = new CombItem("PGW" , "Power Groundwater", new string[] {"TPGW"});  CombList.Add(temp); 
    temp = new CombItem("PREC" , "Power Reclaimed", new string[] {"TPRW"});  CombList.Add(temp); 
    temp = new CombItem("PTOT" , "Power Total", new string[] {"TPD","HPSW"});  CombList.Add(temp); 
            
 * 
 * temp = new CombItem("SUR" , "Surface Water Total", new string[] {"PSSF","DMSF","LSSF","LVGS","LASF","AQSF","IRSF","IDSF","MNSF","TPSF","HPSF"});  CombList.Add(temp); 
    temp = new CombItem("SAL" , "Surface Saline Total", new string[] {"PSSS","DMSS","LSSS","LASS","AQSS","IDSS","MNSS","TPSS","HPSS"});  CombList.Add(temp); 
    temp = new CombItem("GW" , "GroundWater Total", new string[] {"PSGW","DMGW","LSGW","LVGF","LAGW","AQGW","IRGF","IDGW","MNGW","TPGW"});  CombList.Add(temp); 
    temp = new CombItem("REC" , "Reclaimed  Total", new string[] {"PSRW","IRRW","IDRW","MNRW","TPRW"});  CombList.Add(temp);
    temp = new CombItem("ALL", "All Water Total", new string[] { "PSSF", "PSSS", "PSGW", "PSRW", "DMSF", "DMSS", "DMGW", "LSSF", "LVGS", "LASF", "AQSF", "IR
    
 */

