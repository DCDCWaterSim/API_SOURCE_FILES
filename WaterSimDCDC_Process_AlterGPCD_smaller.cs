using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using WaterSimDCDC;
using WaterSimDCDC.Documentation;

namespace WaterSimDCDC.Processes
{
    // Form used to set initial values
    /// <summary>   AlterGPCDFeedbackProcessForm- Form for viewing the alter gpcd feedback process. </summary>
    #region Form
    public partial class AlterGPCDFeedbackProcessForm : Form
    {
        const int DEFAULTMINDEFICIT = 0;
        const int DEFAULTMAXDEFICIT = 100;
        const int DEFAULTMINGPCD = 30;
        const int DEFAULTMAXGPCD = 1000;

        internal string FProcessName = "";
        internal int FMaxDeficit = 10;
        internal int FMinGPCD = 30;

        public AlterGPCDFeedbackProcessForm()
        {
            InitializeComponent();
            textBox_ProcessName.Text = FProcessName;
            textBoxMaxDeficit.Text = FMaxDeficit.ToString();
            textBoxMinGPCD.Text = FMinGPCD.ToString();
        }

        private bool TestMaxDeficit(ref int value)
        {
            bool testresult = false;
            string textmax = textBoxMaxDeficit.Text.Trim();
            try
            {
                int Test = Convert.ToInt32(textmax);
                if ((Test < DEFAULTMINDEFICIT) || (Test > DEFAULTMAXDEFICIT))
                    testresult = false;
                else
                {
                    testresult = true;
                    value = Test;
                }
            }
            catch
            {
                testresult = false;
            }
            return testresult;
        }

        private bool TestMinGPCD(ref int value)
        {
            bool testresult = false;
            string textmin = textBoxMinGPCD.Text.Trim();
            try
            {
                int Test = Convert.ToInt32(textmin);
                if ((Test < DEFAULTMINGPCD) || (Test > DEFAULTMAXGPCD))
                    testresult = false;
                else
                {
                    testresult = true;
                    value = Test;
                }
            }
            catch
            {
                testresult = false;
            }
            return testresult;
        }
        
        private void buttonOK_Click(object sender, EventArgs e)
        {
            string ExitMessage = "";
            bool OKtoClose = true;
           // Do field checks
            if (textBox_ProcessName.Text == "")
            {
                ExitMessage = "Process name can not be blank.  ";
                textBox_ProcessName.Text = FProcessName;
                OKtoClose = false;
            }
            int dummy = 0;
            if (!TestMaxDeficit(ref dummy))
                {
                    ExitMessage += "Deficit trigger must be in range " + DEFAULTMINDEFICIT.ToString() + " to " + DEFAULTMAXDEFICIT.ToString()+".  ";
                    textBoxMaxDeficit.Text = FMaxDeficit.ToString();
                    OKtoClose = false;
                }
            if (!TestMinGPCD(ref dummy))
                    {
                        ExitMessage += "Minimum GPCD must be in range " + DEFAULTMINDEFICIT.ToString() + " to " + DEFAULTMAXDEFICIT.ToString()+".  ";
                        textBoxMinGPCD.Text = FMinGPCD.ToString();
                        OKtoClose = false;
                    }

            if (OKtoClose)
            {
               this.DialogResult = System.Windows.Forms.DialogResult.OK;
               this.Close();
            }
            else
            {
                MessageBox.Show(ExitMessage);
             }
            
        }

        private void textBox_ProcessName_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ProcessName.Text.Trim() != "")
            {
                FProcessName = textBox_ProcessName.Text.Trim();
                StatusLabel.Text = "";
            }
            else
                StatusLabel.Text = "Name cannot be blank.";
        }

        public string ProcessName
        {
            get {  return FProcessName; }
            set { textBox_ProcessName.Text = value; } // Should set FProcessname when TextChange event triggered
        }

        public int MaxDeficit
        {
            get { return FMaxDeficit; }
            set { textBoxMaxDeficit.Text = value.ToString(); }  // should evoke text change 
        }
 
        public int MinGPCD
        {
            get { return FMinGPCD; }
            set { textBoxMinGPCD.Text = value.ToString(); }  // should evoke text change 
        }
 
        private void textBoxMaxDeficit_TextChanged(object sender, EventArgs e)
        {
         
            int value = 0;
            if (! TestMaxDeficit(ref value))
                    StatusLabel.Text = "Deficit must be in range of "+DEFAULTMINDEFICIT.ToString()+" to "+ DEFAULTMAXDEFICIT.ToString();
            else
                {
                    FMaxDeficit = value;
                    StatusLabel.Text = "";
                }
        }

        private void textBoxMinGPCD_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            if (! TestMinGPCD(ref value))
                    StatusLabel.Text = "Deficit must be in range of "+DEFAULTMINGPCD.ToString()+" to "+ DEFAULTMAXGPCD.ToString();
            else
            {
                FMinGPCD = value;
                StatusLabel.Text = "";
            }
        }
    }

#endregion

    /// <summary>   Alter gpcd feedback process. </summary>

    #region Feedback Process
    public class AlterGPCDFeedbackProcess : WaterSimDCDC.AnnualFeedbackProcess
    {
        const int MINGPCDALLOWED = 70;
        const int MAXDEFICITALLOWED = 10;
        int FMaxDeficit = 0;
        int FMinGPCD = 0;
        int FGPCDDeclineFactor = 0;
        double AdjustFactor_Double = 0.0;
        ProviderDoubleArray GPCD_old = new ProviderDoubleArray(0);
        double YrCount = 0.0;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <param name="aName">    Name of Process. </param>
        ///-------------------------------------------------------------------------------------------------

        public AlterGPCDFeedbackProcess(string aName)
            : base(aName)
        {
            EvokeDialogAndFetchValues();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Default constructor. </summary>
        ///-------------------------------------------------------------------------------------------------

        public AlterGPCDFeedbackProcess() : base("Alter GPCD Based on Deficit")
        {
            EvokeDialogAndFetchValues();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="aName"> Name of Process. </param>
        /// <param name="WSim">  The WatewrSimManager who will register process</param>
        ///-------------------------------------------------------------------------------------------------

        public AlterGPCDFeedbackProcess(string aName, WaterSimManager WSim)  : base(aName)
        {
            EvokeDialogAndFetchValues();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="WSim"> The WatewrSimManager who will register process. </param>
        ///-------------------------------------------------------------------------------------------------

        public AlterGPCDFeedbackProcess(WaterSimManager WSim)
            : base("Alter GPCD Based on Deficit")
        {
            EvokeDialogAndFetchValues();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        /// <remarks>Does not evoke form for initial State Values.  Values set at defaults</remarks>
        /// <param name="WSim">     The WatewrSimManager who will register process. </param>
        /// <param name="Quiet">    true to quiet. </param>
        ///-------------------------------------------------------------------------------------------------

        public AlterGPCDFeedbackProcess(WaterSimManager WSim, bool Quiet)
            : base("Alter GPCD Based on Deficit")
        {
            if (Quiet)
            {
                Fname = "DEFAULT PROCESS";
                FMaxDeficit = MAXDEFICITALLOWED;
                FMinGPCD = MINGPCDALLOWED;
            }
            else
            {
                EvokeDialogAndFetchValues();
            }
        }


        ///-------------------------------------------------------------------------------------------------
        /// <summary> Evokes dialog and fetches values. </summary>
        ///-------------------------------------------------------------------------------------------------

        protected void EvokeDialogAndFetchValues()
        {
            AlterGPCDFeedbackProcessForm FPF = new AlterGPCDFeedbackProcessForm();
            if (Fname!="") FPF.ProcessName = Fname;
            if (FPF.ShowDialog() == DialogResult.OK)
            {
                Fname = FPF.ProcessName;
                // ===================================
                //Get data from form and set process values here
                // 
                FMaxDeficit = FPF.MaxDeficit;
                FMinGPCD = FPF.MinGPCD;
                // 
                // ====================================
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Builds the description strings. </summary>
        ///-------------------------------------------------------------------------------------------------

        protected override void BuildDescStrings()
        {
            FProcessDescription = "Alter's GPCD if deficit > " + FMaxDeficit.ToString() + "%";
            FProcessLongDescription = "When a provider's prior year deficit exceeds MaxDeficit (" + FMaxDeficit.ToString() + "%" +
                "(, lowers GPCD to eliminate Deficit, but no lower than MinGPCD (" + FMinGPCD.ToString() + ").";
            FProcessCode = "GPCD_DEF_"+FMaxDeficit.ToString()+"_MIN_"+FMinGPCD.ToString();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the class description. </summary>
        /// <returns> Class Description. </returns>
        ///-------------------------------------------------------------------------------------------------

        new static public string ClassDescription()
        {
            return "Alter GPCD based on MaxDeficit trigger and Minimum GPCD set with Dialog.";

        }

        internal int AdjustGPCD(int OriginalGPCD)
        {
            int newGPCD;
            double YearAdjustFactor = AdjustFactor_Double - (AdjustFactor_Double * (System.Math.Pow(1 + AdjustFactor_Double, YrCount) - 1));           // convert factors to double
            double NewGPCD_Double = Convert.ToDouble(OriginalGPCD);
            // calculate newgpcd by decline by adjustfactor
            NewGPCD_Double = NewGPCD_Double - (NewGPCD_Double * YearAdjustFactor);
            newGPCD = Convert.ToInt32(NewGPCD_Double);
            // check if below Min
            if (newGPCD < FMinGPCD)
                newGPCD = FMinGPCD;
            return newGPCD;
        }

        internal double AdjustGPCD(double OriginalGPCD)
        {
            // calculate newgpcd by decline by adjustfactor
            double YearAdjustFactor = AdjustFactor_Double - (AdjustFactor_Double * (System.Math.Pow(1 + AdjustFactor_Double, YrCount) - 1));
            double NewGPCD = OriginalGPCD - (OriginalGPCD * YearAdjustFactor);
            // check if below Min
            double MinGPCD = Convert.ToDouble(FMinGPCD);
            if (NewGPCD < MinGPCD)
                NewGPCD = MinGPCD;
            return NewGPCD;
        }

        public override DocTreeNode Documentation()
        {
            DocTreeNode node = base.Documentation();
            node.AddChildField("FMaxDeficit", FMaxDeficit.ToString());
            node.AddChildField("FMinGPCD", FMinGPCD.ToString());
            return node;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> method that is called when a Simulation is initialized. </summary>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        /// ### <seealso cref="ProcessStarted"/>
        ///-------------------------------------------------------------------------------------------------

        public override bool ProcessInitialized(WaterSimManager WSim)
        {
            return base.ProcessInitialized(WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> method that is called right before the first year of a simulation is called.
        ///     </summary>
        /// <param name="year"> The year about to be run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool ProcessStarted(int year, WaterSimManager WSim)
        {
            // OK Simulation Starting, Parameters have been set, fetch the ReduceGOCD factor and use it to calculate rate
            YrCount = 1;
            FGPCDDeclineFactor = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epPCT_Alter_GPCD).Value; // 7/29  BaseModel_ParameterBaseClass(eModelParam.epPCT_Reduce_GPCD).Value;
            double TotalYears = Convert.ToDouble((WSim.Simulation_End_Year - WSim.Simulation_Start_Year) + 1);
            AdjustFactor_Double = (Convert.ToDouble(FGPCDDeclineFactor) / 100) / TotalYears;
            return base.ProcessStarted(year, WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Method that is called before each annual run. </summary>
        /// <param name="year"> The year about to be run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        /// <returns> true if it succeeds, false if it fails. Error should be placed in FErrorMessage.
        ///     </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool PreProcess(int year, WaterSimManager WSim)
        {
            // Check if first run
            ProviderIntArray FUSED_GPCD = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epGPCD_Used).ProviderProperty.getvalues();
             YrCount++;
             // Set oldGOCD to these values.
             for (int i=0;i<FUSED_GPCD.Values.Length;i++)
                {
                    GPCD_old[i] = Convert.ToDouble(FUSED_GPCD[i]);
                }
            //}
            // not first year, do it
             WSim.ParamManager.Model_Parameter(eModelParam.epProvider_Demand_Option).Value = 4;  // 7/29  WSim.ParamManager.BaseModel_ParameterBaseClass(eModelParam.epProvider_Demand_Option).Value = 4;
                // OK Not First year
                // Check if model is locked, if so unlock it.
                bool TempLock = WSim.isLocked();
                if (TempLock) WSim.UnLockSimulation();

                // Get data we need to evaluate GPCD
                ModelParameterBaseClass MP;
                MP = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epDemand_Deficit);
                ProviderIntArray FDeficit = MP.ProviderProperty.getvalues();
                
                ProviderIntArray FPCT_Deficit = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epPCT_Deficit).ProviderProperty.getvalues();
                ProviderIntArray FPOP = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epPopulation_Used).ProviderProperty.getvalues();
                ProviderIntArray FDEMAND = WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epTotal_Demand).ProviderProperty.getvalues();
                // create and array to set GPCD
                ProviderIntArray FSet_GPCD = new ProviderIntArray(0);
                // Loop through providers and check for PCT_Deficit that exceed FMaxDeficit
                bool SetGPCDS = false;
                for (int i = 0; i < FPCT_Deficit.Length; i++)
                {
                    if (FPCT_Deficit[i] > FMaxDeficit)
                    {
                        eProvider ep = (eProvider)i;
                        // OK Calculated how much lower GPCD needs to go to accomodate deficit
                        int MyDeficit_AF = FDeficit[i];
                        double MyDeficit_G = util.ConvertAFtoGallons(MyDeficit_AF);
                        int MyPCTDeficit = FPCT_Deficit[i];
                        int MyPop = FPOP[i];
                        double GPCD_Decline_Double = 0.0;
                        int GPCD_Decline_Int = 0;
                        int MyGPCD = FUSED_GPCD[i];
                        // if (MyPop > MyDeficit) then it will be 1
                        GPCD_Decline_Double = util.CalcGPCD(MyDeficit_G, MyPop);
                        GPCD_Decline_Int = Convert.ToInt32(GPCD_Decline_Double);
                        int New_GPCD = MyGPCD - GPCD_Decline_Int;
                        // Checl if New GPCD not to low
                        if ((New_GPCD > FMinGPCD))
                        {
                            FUSED_GPCD[i] = New_GPCD;
                        }
                        else
                        {
                            FUSED_GPCD[i] = FMinGPCD;
                        }
                        GPCD_old[i] = FUSED_GPCD[i];
                        SetGPCDS = true;
                    }
                    else
                    {
                        GPCD_old[i] = AdjustGPCD(GPCD_old[i]);
                        FUSED_GPCD[i] = Convert.ToInt32(GPCD_old[i]); 
                    }
                }
                //
                 // All Done
                 if (SetGPCDS)
                        WSim.ParamManager.Model_ParameterBaseClass(eModelParam.epUse_GPCD).ProviderProperty.setvalues(FUSED_GPCD);
                

                // if model was locked comming in, set lock going out
                if (TempLock) WSim.LockSimulation();
            
             return base.PreProcess(year, WSim);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Method that is called before after each annual run. </summary>
        /// <param name="year"> The year just run. </param>
        /// <param name="WSim"> The WaterSimManager that is making call. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. Error should be placed in FErrorMessage.
        ///     </returns>
        ///-------------------------------------------------------------------------------------------------

        public override bool PostProcess(int year, WaterSimManager WSim)
        {
            return base.PostProcess(year, WSim);
        }
    }
#endregion
          
}
