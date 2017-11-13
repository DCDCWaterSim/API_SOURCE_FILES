using System.Text;
using System;
using System.Windows.Forms;
using WaterSimDCDC;
using WaterSimDCDC.Documentation;

namespace WaterSimDCDC.Processes
{
    /// <summary>   Form for viewing the track deficit feedback process. </summary>
    #region
    /// <summary>   Agto muni web feedback process. </summary>
    

    public class AgtoMuniWebFeedbackProcess : WaterSimDCDC.AnnualFeedbackProcess
    {
        internal ProviderIntArray CountList_1 = new ProviderIntArray(0);
        /// <summary>
        /// a provider array with the total AF of deficit for each provide across all years
        /// </summary>
        internal ProviderDoubleArray TotalList_1 = new ProviderDoubleArray(0.0);
        /// <summary> A provider array how many continuous years in deficit, 0 if not in deficit. </summary>
        internal ProviderIntArray ContinuousList_1 = new ProviderIntArray(0);
        /// <summary> A provider array of longest period of continuous deficit </summary>
        internal ProviderIntArray LongestContinuous_1 = new ProviderIntArray(0);

        int FAgriculture_sustainability = 0;
        
        internal int FAgToMuni_sustainability = 0;
        
        IndicatorWeighting IW;
        // Quay ADD ---------------------
        double AgPCTDemandMet = 0;
        int FAgPCTDemandMet = 0;
        double AgPCTOfTotalEffluent = 0;
        int FAgPCTOfTotalEffluent = 0;
        // Quay ADD ---------------------
        

        public AgtoMuniWebFeedbackProcess()
            : base()
        {
            Fname = "Water for Muni from Ag";        
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aName">    The name. </param>
        ///-------------------------------------------------------------------------------------------------

        public AgtoMuniWebFeedbackProcess(string aName)
            : base(aName)
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        public AgtoMuniWebFeedbackProcess(WaterSimManager WSim)
            : base("Water for Muni from Ag")
        {
            FetchValuesForAg(WSim);
            CreateAgSustainParameter(WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="aName">    The name. </param>
        /// <param name="WSim">     The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        public AgtoMuniWebFeedbackProcess(string aName, WaterSimManager WSim)
            : base(aName, WSim)
        {
            FetchValuesForAg(WSim);
            CreateAgSustainParameter(WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Fetches the values for ag. </summary>
        ///
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        protected void FetchValuesForAg(WaterSimManager WSim)
        {
           WSim.Web_AgricultureTransferToMuni_PCT = WSim.PCT_Agriculture;
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Method that is called before each annual run. </summary>
        ///
        /// <param name="year"> The year about to be run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. Error should be placed in FErrorMessage. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool PreProcess(int year, WaterSimManager WSim)
        {
            bool TempLock = WSim.isLocked();

            WSim.UnLockSimulation();
            FetchValuesForAg(WSim);
            if (TempLock) WSim.LockSimulation();

            return base.PreProcess(year, WSim);
        }

        internal int GetAgIndicator()
        {
            return FAgriculture_sustainability;
        }

       
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the agriculture sustainability. </summary>
        ///
        /// <value> The agriculture sustainability. </value>
        ///-------------------------------------------------------------------------------------------------

        public int Agriculture_sustainability
        { get { return FAgriculture_sustainability; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates an ag sustain parameter. </summary>
        ///
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///-------------------------------------------------------------------------------------------------

        void CreateAgSustainParameter(WaterSimManager WSim)
        {
            
            WSim.ParamManager.AddParameter(new ModelBaseOutputParameterClass(eModelParam.epAgSustainabilityIndicator, "Ag Sustinability Indicator", "AGSUSID", GetAgIndicator ));
           
        }

        /// <summary>   Builds the description strings. </summary>
        protected override void BuildDescStrings()
        {
            
            FProcessDescription = "Keeps track of agriculture water supply and calculates sustainability indicator";
            FProcessLongDescription = "Keeps track of agriculture pumped and CAP water supply allocation or reallocation and calculates an agricultural sustainability indicator based on amount of supply reallocated to urban use";
            FProcessCode = "TRKAGIND";
        }
        /// <summary>
        ///  My stab and an inicator for agriculture water transfer to municipal
        /// </summary>
        /// <param name="year"></param>
        /// <param name="WSim"></param>
        /// <returns></returns>
         //int[] totalEffluent = new int[ProviderClass.NumberOfProviders];

        //int[] AgToMuni = new int[ProviderClass.NumberOfProviders];
        public override bool PostProcess(int year, WaterSimManager WSim)
        {
            ProviderIntArray AgToMuni = new ProviderIntArray();
            ProviderIntArray AgToMuniS = new ProviderIntArray();
            ProviderIntArray AgToMuniP = new ProviderIntArray();
            ProviderIntArray AgToMuni_surfaceMax = new ProviderIntArray();
            ProviderIntArray AgToMuni_pumpingMax = new ProviderIntArray();
            //
            //ProviderIntArray AgEffluent = new ProviderIntArray();
            ProviderDoubleArray AgEffluent = new ProviderDoubleArray();
            ProviderIntArray Effluent = new ProviderIntArray();
            //
             int surfaceMax =0;
             int pumpMax = 0;
           
             //int _Agriculture_sustainability = 0;
             int Agriculture = 0;
             int Ratio = 0;
             //
             double ratio = 0;
             double AgIN = 0;
             double AgToMun = 0;
             double AddAg = 0;
             double d_Max = 0;
             double Ag_sustainability=0;
             //
             Effluent = WSim.Total_WWTP_Effluent.getvalues();
            //
            AgToMuniS = WSim.Use_WaterFromAgSurface.getvalues();
            AgToMuniP = WSim.Use_WaterFromAgPumping.getvalues();
            AgToMuni_surfaceMax = WSim.WaterFromAgSurfaceMax.getvalues();
            AgToMuni_pumpingMax = WSim.WaterFromAgPumpingMax.getvalues();
            AgIN =  Convert.ToDouble(WSim.AgToWebProcess)/100 ;
            // QUAY ADD ---------------------------
            double TotalToAg = 0;
            double TotalToAgMax = 0;
            double TotalEffToAg = 0;
            double TotalEffToAgMax = 0;
            // QUAY ADD --------------------------
  
            foreach (eProvider ep in ProviderClass.providers())
            {
                AgEffluent[ep] = 0;
                if (0 < Effluent[ep])
                {
                    AgEffluent[ep] = (AgIN * Convert.ToDouble(Effluent[ep]) ) / Convert.ToDouble(Effluent[ep]);
                }
                ratio += AgEffluent[ep];

                ////
                surfaceMax += AgToMuni_surfaceMax[ep];
                pumpMax += AgToMuni_pumpingMax[ep];
                AgToMuni[ep] = AgToMuniS[ep] + AgToMuniP[ep];

                // QUAY ADD ---------------------------
                // caculate total for ag water to muni
                TotalToAg += AgToMuniS[ep] + AgToMuniP[ep];
                // calculate total ag water demand for this year
                TotalToAgMax += AgToMuni_pumpingMax[ep] + AgToMuni_surfaceMax[ep];
                // calculate total efflunet going to ag
                if (0 < Effluent[ep])
                {
                    TotalEffToAg += AgIN * Convert.ToDouble(Effluent[ep]);
                }
                // calculate total potential effluent
                TotalEffToAgMax += Effluent[ep];
                // QUAY ADD --------------------------
                
                if (0 < AgToMuni[ep] )
                {
                    AgToMun = 0;
                    AgToMun = AgToMuni[ep];
                    d_Max = surfaceMax + pumpMax;
                    // Should this be weighted by population or total demand, or something else?
                    //
                    FAgToMuni_sustainability = 0;
                    if (0 < d_Max)
                    {
                        if (0 < AgToMun)
                        {
                            Ag_sustainability = ((1 - (AgToMun / d_Max)) * 100);
                        }
                    }
                    AddAg += Ag_sustainability;
                    FAgToMuni_sustainability = Convert.ToInt32(AddAg);
                     //
                    CountList_1[ep]++;
                    TotalList_1[ep] +=  FAgToMuni_sustainability;
                    ContinuousList_1[ep]++;
                    if (LongestContinuous_1[ep] < ContinuousList_1[ep]) LongestContinuous_1[ep] = ContinuousList_1[ep];
                }
                else
                {
                    ContinuousList_1[ep] = 0;
                }

            }
            // QUAY Added -------------------------
            // Calculate % of AG demand being met
            if (TotalToAgMax>0)
                AgPCTDemandMet = ((TotalToAgMax- TotalToAg) * 100) / TotalToAgMax;
            else
                AgPCTDemandMet = 100;
            FAgPCTDemandMet = Convert.ToInt32(AgPCTDemandMet);
            // calcualte % of total effluent to ag demand
            if (TotalEffToAgMax > 0)
                AgPCTOfTotalEffluent = TotalEffToAg / TotalEffToAgMax;
            FAgPCTOfTotalEffluent = Convert.ToInt32(AgPCTOfTotalEffluent * 100);
            // OK Let's weight this
            double Agweight = 0.7;
            double Effweight = 0.3;
            IW = new IndicatorWeighting(FAgPCTDemandMet, FAgPCTOfTotalEffluent, Agweight, Effweight);
            IW.Weight();
            FAgriculture_sustainability = IW.Pout;

            // Quay Added --------------------------
            //ratio = ratio/ ProviderClass.NumberOfProviders;
            //Ratio = Convert.ToInt32(ratio * 100);
            //FAgToMuni_sustainability = Convert.ToInt32(AddAg / ProviderClass.NumberOfProviders);
            //double one = 0.7;
            //double two = 0.3;
            //Agriculture = FAgToMuni_sustainability;
            //IW = new IndicatorWeighting(Agriculture, Ratio, one, two);
            //IW.Weight();
            //FAgriculture_sustainability = IW.Pout;
           
            return true;
        }



    }
    //
    public class Env_WebFeedbackProcess : WaterSimDCDC.AnnualFeedbackProcess
    {
        public ProviderIntArray CountList_2 = new ProviderIntArray(0);
        /// <summary>
        /// a provider array with the total AF of deficit for each provide across all years
        /// </summary>
        public ProviderDoubleArray TotalList_2 = new ProviderDoubleArray(0.0);
        /// <summary> A provider array how many continuous years in deficit, 0 if not in deficit. </summary>
        public ProviderIntArray ContinuousList_2 = new ProviderIntArray(0);
        /// <summary> A provider array of longest period of continuous deficit </summary>
        public ProviderIntArray LongestContinuous_2 = new ProviderIntArray(0);
        public int WaterForEnv_sustainability = 0;
        public double Threshold_Salt = 0;
        public double Threshold = 0;
        public double Threshold_CO = 0;
        IndicatorWeighting IW;

        public Env_WebFeedbackProcess()
            : base()
        {
            Fname = "Water for Environment";
        }
        public Env_WebFeedbackProcess(string aName)
            : base(aName)
        {
        }
        public Env_WebFeedbackProcess(WaterSimManager WSim)
            : base("huh")
        {
            FetchValuesForEnv(WSim);
        }
        public Env_WebFeedbackProcess(string aName, WaterSimManager WSim)
            : base(aName, WSim)
        {
        }
        protected void FetchValuesForEnv(WaterSimManager WSim)
        {
            WSim.WebFlowForEnvironment_PCT = WSim.PCT_FlowForEnvironment_memory;
        }
        public override bool PreProcess(int year, WaterSimManager WSim)
        {
            bool TempLock = WSim.isLocked();

            WSim.UnLockSimulation();
            FetchValuesForEnv(WSim);
            if (TempLock) WSim.LockSimulation();

            return base.PreProcess(year, WSim);
        }
        //
        double x = 0;
        double beta = 2.5;
        double gamma = 0.6;
        double flow = 0;
        double scale = 0;
        int asymptote = 0;
        //
        double _w1 = 0.4;
        double _w2 = 0.6;

        int[] dischargeEffluent = new int[ProviderClass.NumberOfProviders];
        int[] dischargeReclaimed = new int[ProviderClass.NumberOfProviders];
        int[] totalEffluent = new int[ProviderClass.NumberOfProviders];
        int[] totalReclaimed = new int[ProviderClass.NumberOfProviders];
        //double [] ratio = new double[ProviderClass.NumberOfProviders];
        float[] Ratio = new float[ProviderClass.NumberOfProviders];

        float addRatio = 0;
        /// <summary>
        ///  Chapman-Richard's equation, Sit-Poulin page 46
        ///  
        ///  and
        ///  
        /// Gompertz function, Sit-Poulin page 52
        ///  
        ///  Sit V, Poulin-Costello, M.1994. Catalog of Curve for Curve Fitting, Ministry of Forest 
        ///  Research Program, Biometric Infromation Handbook No. 4. Crown Publications, Victoria,
        ///  BC, Canada.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="WSim"></param>
        /// <returns>0.86 is the maximum, </returns>
        public override bool PostProcess(int year, WaterSimManager WSim)
        {
             // -----------------------------------------------------------------------------------
            Threshold_Salt = 0.15 * Convert.ToDouble(WSim.SaltVerde_River_Flow);
            Threshold_CO = 0.15 * Convert.ToDouble(WSim.Colorado_River_Flow);
            Threshold = Threshold_Salt + Threshold_CO;
            flow = WSim.FlowForEnvironment_AF;
            x = (flow/(Threshold_Salt+Threshold_CO) ) * 10;
            //scale =  Math.Pow((1 - Math.Exp(-beta * x)), gamma);
            scale =   Math.Exp(-Math.Exp(beta - gamma * x));
             // asymptote is scaled from 0 to 100
            asymptote = 0;
            if (0 < flow)
            {
                asymptote = Convert.ToInt32(scale * 100);
            }
            //
            double average = 0;
            int discharge = 0;
            // ----------------------------------------------------------------------------------
            //
            dischargeEffluent = DischargeEffluent(WSim);
            dischargeReclaimed = WSim.Reclaimed_Water_Discharged.getvalues().Values;
            totalEffluent = WSim.Total_WWTP_Effluent.getvalues().Values;
            totalReclaimed = WSim.Reclaimed_Water_Created.getvalues().Values;
            //
            // NOT FINISHED!!!! 02.21.14
            addRatio = 0;
            for (int p = 0; p < ProviderClass.NumberOfProviders; p++)
            {
                Ratio[p] = Ratio_(p, dischargeEffluent, totalEffluent);
                addRatio += Ratio[p];
                Ratio[p] = Ratio_(p, dischargeReclaimed, totalReclaimed);
                addRatio += Ratio[p];
                average = (addRatio/2) / 33;
                discharge = Convert.ToInt32(average * 100);
            }
             IW = new IndicatorWeighting(asymptote, discharge, w1, w2);

             WaterForEnv_sustainability= IW.Pout;
            //------------------------------------------------------------------------------------------
            return true;
        }
        //internal int Weight(int one,int two, double w1,double w2)
        //{
        //    double calc=0;
        //    int Pout=0;
        //    try
        //    {
        //        if (w1 + w2 == 1)
        //        {
        //            calc = Convert.ToDouble(one) * w1 + Convert.ToDouble(two) * w2;
        //            Pout = Convert.ToInt32(calc);
        //        }
        //    }
        //    catch { }

        //    return Pout;
        //}
        internal Single Ratio_(int p,int[] flux, int[] total)
        {
            Ratio[p] = 0;
            if (0 < total[p])
            {
                Ratio[p] = Convert.ToSingle(flux[p]) / Convert.ToSingle(total[p]);
            }
            return Ratio[p];
        }
        internal int[] DischargeEffluent(WaterSimManager WSim)
        {
            {
                return WSim.Effluent_Discharged.getvalues().Values;
            }
        }
        //
        double w1
        {
            get { return _w1; }
            set { w1 = value; }
        }
        double w2
        {
            get { return _w2; }
            set { w2 = value; }
        }
    }

    internal class IndicatorWeighting
    {
        double calc = 0;
        internal int Pout = 0;
        int One = 0;
        int Two = 0;
        int Three = 0;
        //
        double _w1 = 0.4;
        double _w2 = 0.6;
        double _w3 = 0;
        //
        string weight = "Weighting factors do not add to 1";
        string sum = "The number of passing weighted is not correct";
        //
         internal IndicatorWeighting() : base()
        {

        }
        internal IndicatorWeighting(int First, int Second, double W1,double W2) 
        {
            One = First;
            Two = Second;
            
            _w1 = W1;
            _w2 = W2;
        }
        internal IndicatorWeighting(int First, int Second, int Third,double W1, double W2, double W3)
        {
            One = First;
            Two = Second;
            Three = Third;
            _w1 = W1;
            _w2 = W2;
            _w3 = W3;
        }

        internal int Weight()
        {
             try
            {
                 if(0 <= One && 0 <= Two)
                {
                    if( Pcheck(w1, w2, w3))
                    { calc =( Convert.ToDouble(One) * w1) + (Convert.ToDouble(Two) * w2);}
                     
                }
                 else if (0 <= One && 0 <= Two && 0 <= Three)
                {
                    if (Pcheck(w1, w2, w3)) { calc = (Convert.ToDouble(One) * w1) + (Convert.ToDouble(Two) * w2) + (Convert.ToDouble(Three) * w3); }
                }
                 Pout = Convert.ToInt32(calc);

            }
            catch (Exception e)
             {
               sum = e.Message;
             }

            return Pout;
        }
        //-----------------------------------------------------------
        private bool Pcheck(double one, double two, double three)
        {
            bool check = false;
            double Total = one + two + three;
            if (Total.Equals(1.0))
            {
                try
                {
                    check = true;
                }
                catch(Exception e)
                {
                    weight = e.Message;
                }
            }
            return check;
        }
        internal double w1
        {
            get { return _w1; }
            set { _w1 = value; }
        }
        internal double w2
        {
            get { return _w2; }
            set { _w2 = value; }
        }
        internal double w3
        {
            get { return _w3; }
            set { _w3 = value; }
        }


    }
    public class Personal_GPCD_WebFeedbackProcess : WaterSimDCDC.AnnualFeedbackProcess
    {
        public Personal_GPCD_WebFeedbackProcess()
            : base()
        {
            Fname = "Personal Use of Water";
        }
        public Personal_GPCD_WebFeedbackProcess(string aName)
            : base(aName)
        {
        }
        public Personal_GPCD_WebFeedbackProcess(WaterSimManager WSim)
            : base("huh")
        {
            FetchValuesForPersonal(WSim);
        }
        public Personal_GPCD_WebFeedbackProcess(string aName, WaterSimManager WSim)
            : base(aName, WSim)
        {
        }
        protected void FetchValuesForPersonal(WaterSimManager WSim)
        {
            WSim.Web_Personal_PCT = WSim.PCT_Personal;
        }
        public override bool PreProcess(int year, WaterSimManager WSim)
        {
            bool TempLock = WSim.isLocked();

            WSim.UnLockSimulation();
            FetchValuesForPersonal(WSim);
            if (TempLock) WSim.LockSimulation();

            return base.PreProcess(year, WSim);
        }

    }

    #endregion
}
