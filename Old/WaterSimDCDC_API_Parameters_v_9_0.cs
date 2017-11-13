// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       A Model Paramter Class System that provides dynamic support for model parameters

//       WaterSimDCDC_API_Parameters
//        Version 4.1
//       Keeper Ray Quay  ray.quay@asu.edu
//       Copyright (C) 2011,2012, Arizona Board of Regents
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
//====================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace WaterSimDCDC
{
    #region Params
    //----------------------------------------------------------------

    /// <summary>   Model parameter class.  </summary>
    public static class ModelParamClass
    {

        //LIST of parameters  
        //static int FNumberOfModelParameters = eModelParam.LastUserDefinedParameter + 1;
        //--------------------------------------------

        /// <summary>   Valids. </summary>
        /// <param name="p">    a EModelParam  </param>
        ///
        /// <returns>   true if p is a valid ModelParameter. </returns>

        public static bool valid(int p)
        {
            if ((p >= 0) & (eModelParam.LastUserDefinedParameter >= p)) return true;
            else return false;
        }
        //------------------------------------------

        /// <summary>   Gets the number of valid ModelParameters. </summary>
        /// <returns>   the length of the ModelParameter list </returns>

        //public static int Length()
        //{ return FNumberOfModelParameters; }
        //------------------------------------------
        internal static string Name(int p) { return eModelParam.Names[p]; }
        //-------------------------------------------------------------------
        //internal static void SeteModelParamLength(int Size)
        //{
        //    FNumberOfModelParameters = Size;
        // }
        //----------------------------------------------------

    }

    //---------------------------------------------------------
    /// <summary>   Delegate for handling Reload events. </summary>
    ///
    /// <remarks>   Ray, 8/5/2011. </remarks>
    ///
    /// <param name="source">   Source for the. </param>
    /// <param name="e">        Event information. </param>
    public delegate void ReloadEventHandler(object source, System.EventArgs e);

    //-------------------------------------------------------------------
    /// <summary>   Additional information for model parameter events.  </summary>
    public class ModelParameterEventArgs : System.EventArgs
    {
        /// <summary> The ModelParameter evoking the event</summary>
        public ModelParameterBaseClass modelParameter;

        /// <summary>   Constructor. </summary>
        /// <param name="MP">   The ModelParameter. </param>

        public ModelParameterEventArgs(ModelParameterBaseClass MP)
        {
            modelParameter = MP;
        }
    }

    //====================================================
    //---------------------------------------------------------

    /// <summary>   Model parameter base class.  </summary>

    public abstract class ModelParameterBaseClass : IDisposable
    {
        //----------------------------------------
        //Fields

        /// <summary> The model parameter </summary>
        // REMOVED 5 21 12  protected eModelParam FModelParam;

        internal int FModelParam;

        /// <summary> The label </summary>
        protected string FLabel = "";

        /// <summary> The fieldname </summary>
        protected string FFieldname = "";

        /// <summary> The ftype </summary>
        protected modelParamtype Ftype = modelParamtype.mptUnknown;

        /// <summary> Type of the range check </summary>
        protected rangeChecktype FRangeCheckType = rangeChecktype.rctUnknown;

        /// <summary> The low range </summary>
        protected int FLowRange = 0;

        /// <summary> The high range </summary>
        protected int FHighRange = 0;

        /// <summary> true if need to notify for relaod on value change </summary>
        protected bool FReload = false;

        // method references
        /// <summary> special Base Range check </summary>
        protected DcheckBase FSpecialBaseCheck;

        /// <summary> special provider Range check </summary>
        public DcheckProvider FSpecialProviderCheck;

        /// <summary> Event queue for all listeners interested in Reload events. </summary>
        public event ReloadEventHandler ReloadEvent = null;
        
        /// <summary> Manager for parameter </summary>
        public ParameterManagerClass FParameterManager = null;

        /// <summary>   Default constructor. </summary>

        public ModelParameterBaseClass() { }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        ///     </summary>
        ///-------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            CleanUp();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Clean up. </summary>
        /// <remarks>Override this class to do cleanup of managed and unmanged resources</remarks>
        ///-------------------------------------------------------------------------------------------------

        public virtual void CleanUp()
        {
            // override to add clean up unmanged code or release managed resources

        }
        //----------------------------------------
        // properties
        /// <summary> special Base Range check </summary>
        public DcheckBase SpecialBaseCheck { get { if (FSpecialBaseCheck != null) return FSpecialBaseCheck; else return null; } }

        /// <summary> special provider Range check </summary>
        public DcheckProvider SpecialProviderCheck { get { return FSpecialProviderCheck; } }

        /// <summary>   Gets the ParameterManager that owns this Model Parameter. </summary>
        /// <value> The parameter manager. </value>
        public ParameterManagerClass ParameterManager
        { get { return FParameterManager; } }

        /// <summary>   Gets the ParameterManager that owns this Model Parameter. </summary>
        /// <value> The parameter manager. </value>
        internal ParameterManagerClass ParameterManagerForFriends
        { get { return FParameterManager; } set { FParameterManager = value; } }

        /// <summary>   Gets the model parameter emumerator. </summary>
        ///
        /// <value> The eModelParam enumerator. </value>

        public int ModelParam
        { get { return FModelParam; } }
        //----------------------------------------

        /// <summary>   Gets or sets the label. </summary>
        ///
        /// <value> The label. </value>

        public string Label
        // Text Lable for the model parameter for use text label
        { get { return FLabel; } set { FLabel = value; } }
        //----------------------------------------

        /// <summary>   Gets or sets the fieldname. </summary>
        ///
        /// <value> The fieldname. that is used in data tables as a column name</value>

        public string Fieldname
        { get { return FFieldname; } set { FFieldname = value; } }
        //----------------------------------------

        /// <summary>   Gets or sets the type of the parameter. </summary>
        ///
        /// <value> The type of the parameter. </value>

        public modelParamtype ParamType
        { get { return Ftype; } set { Ftype = value; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the special base check. </summary>
        ///
        /// <value> The special base check. </value>
        ///-------------------------------------------------------------------------------------------------

        //public  DcheckBase SpecialBaseCheck
        //{ get { return FSpecialBaseCheck;  } }
        //----------------------------------------

        /// <summary>   Gets a value indicating whether this object is input parameter. </summary>
        ///
        /// <value> true if this object is input parameter, false if not. </value>

        public bool isInputParam
        { get { return ((Ftype == modelParamtype.mptInputBase) | (Ftype == modelParamtype.mptInputProvider) | (Ftype == modelParamtype.mptInputOther)); } }        //----------------------------------------

        /// <summary>   Gets a value indicating whether this object is output parameter. </summary>
        ///
        /// <value> true if this object is output parameter, false if not. </value>

        public bool isOutputParam
        { get { return ((Ftype == modelParamtype.mptOutputBase) | (Ftype == modelParamtype.mptOutputProvider) | (Ftype == modelParamtype.mptOutputOther)); } }
        //----------------------------------------

        /// <summary>   Gets a value indicating whether this object is provider parameter. </summary>
        /// <value> true if this object is provider parameter, false if not. </value>

        public bool isProviderParam
        { get { return ((Ftype == modelParamtype.mptOutputProvider) | (Ftype == modelParamtype.mptInputProvider)); } }
        //----------------------------------------

        /// <summary>   Gets a value indicating whether this object is base parameter. </summary>
        /// <value> true if this object is base parameter, false if not. </value>

        public bool isBaseParam
        { get { return ((Ftype == modelParamtype.mptOutputBase) | (Ftype == modelParamtype.mptInputBase)); } }

        //----------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this object is mptOutPutOther or mptInputOther parameter. </summary>
        ///
        /// <value> true if this object is other parameter, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool isOtherParam
        { get { return ((Ftype == modelParamtype.mptOutputOther) | (Ftype == modelParamtype.mptInputOther)); } }

        //----------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this object is 2DGrid parameter. </summary>
        ///
        /// <value> true if this object is grid 2 d parameter, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool isGrid2DParam
        { get { return ((Ftype == modelParamtype.mptOutput2DGrid) | (Ftype == modelParamtype.mptInput2DGrid)); } }

        //----------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets a value indicating whether this object is 3DGrid parameter. </summary>
        ///
        /// <value> true if this object is grid 3 d parameter, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool isGrid3DParam
        { get { return ((Ftype == modelParamtype.mptOutput3DGrid) | (Ftype == modelParamtype.mptInput3DGrid)); } }

        //----------------------------------------

        /// <summary>   Gets or sets the type of the range check. </summary>
        /// <value> The type of the range check. </value>

        public rangeChecktype RangeCheckType
        { get { return FRangeCheckType; } set { FRangeCheckType = value; } }
        //----------------------------------------

        /// <summary>   Gets or sets the low range for conducting range checks. </summary>
        /// <value> The low range. </value>

        public int LowRange
        { get { return FLowRange; } set { FLowRange = value; } }
        //----------------------------------------

        /// <summary>   Gets or sets the high range for conducting range checks. </summary>
        /// <value> The high range. </value>

        public int HighRange
        { get { return FHighRange; } set { FHighRange = value; } }
 
        //------------------------------------------
        /// <summary>   Gets or sets the ModelParameter value. </summary>
        /// <remarks> This is the basic access to base model parameter</remarks>
        /// <value> The value. </value>
        /// <exception cref="WaterSim_Exception">if value or values are out of range for the parameter or if parameter is not a base Parameter</exception>

        public abstract int Value { get; set; }

        //------------------------------------------

        /// <summary>   Gets the providerArrayPropertyBaseClass for the ModelParameter. </summary>
        /// <remarks> This provides access to the provider model parameter</remarks>
        /// <value> The provider property. </value>
        /// <exception cref="WaterSim_Exception">if parameter is not a Provider Paramemter</exception>

        public abstract providerArrayProperty ProviderProperty { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the grid property. </summary>
        ///
        /// <value> The grid property. </value>
        /// <exception cref="WaterSim_Exception">if parameter is not a Grid Paramemter</exception>
        ///-------------------------------------------------------------------------------------------------

        public abstract Grid2DProperty GridProperty { get; }


        //--------------------------------------------------------
        /// <summary>   Queries if we should check range./ </summary>
        /// <returns>   true if should check range, otherwise false</returns>

        public bool ShouldCheckRange()
        {
            return ((FRangeCheckType == rangeChecktype.rctCheckRange) |
                    (FRangeCheckType == rangeChecktype.rctNoRangeCheck) |
                    (FRangeCheckType == rangeChecktype.rctCheckPositive) |
                    (FRangeCheckType == rangeChecktype.rctCheckRangeSpecial));
        }
        //----------------------------------------

        /// <summary>   Query if this object is special range check. </summary>
        /// <returns>   true if special range check, false if not. </returns>

        public bool isSpecialRangeCheck()
        { return (FRangeCheckType == rangeChecktype.rctCheckRangeSpecial); }


        //----------------------------------------

        /// <summary>   Formats a Range check error string. </summary>
        /// <param name="value">    The value. </param>
        ///
        /// <returns> formatted string  . </returns>

        string rangeCheckErrString(int value)
        {
            return "Range Error: " + Label + "  = " + value.ToString() + " is not in the range of " + LowRange.ToString() + " to " + HighRange.ToString();
        }
        //----------------------------------------

        /// <summary>   formats a Positive range check error string. </summary>
        /// <param name="value">  formatted string. </param>
        ///
        /// <returns>   . </returns>

        string positiveCheckErrString(int value)
        {
            return "Range Error: " + Label + "  = " + value.ToString() + " must be positive.";
        }
        //----------------------------------------

        /// <summary>   Query if 'value' is in base ModelPArameter range. </summary>
        /// <param name="value">    The value to be checked. </param>
        ///
        /// <returns>   true if base value in range, false if not. </returns>

        public bool isBaseValueInRange(int value)
        {
            string junk = "";
            return isBaseValueInRange(value, ref junk);
        }
        //----------------------------------------

        /// <summary>   Query if 'value' is in base ModelParameter range. </summary>
        /// <exception cref="WaterSim_Exception">   Thrown when ModelParameter is not a modelParamtype.mptInputBase Parameter </exception>
        ///
        /// <param name="value">        The value to be checked. </param>
        /// <param name="errMessage">   [in,out] Message describing the error. </param>
        ///
        /// <returns>   true if base value in range, false if not. </returns>

        public bool isBaseValueInRange(int value, ref string errMessage)
        {
            bool test = true;
            // changed RQ 4 15 12 old - if ((isBaseParam) & (isInputParam))
            if ((isBaseParam) & (isInputParam) || (isOtherParam) & (isInputParam))
            {
                switch (FRangeCheckType)
                {
                    case rangeChecktype.rctCheckRange:
                        test = ((value >= FLowRange) & (value <= FHighRange));
                        if (!test) errMessage += rangeCheckErrString(value);
                        break;
                    //--------------------
                    case rangeChecktype.rctCheckRangeSpecial:
                        string temp = "";
                        test = FSpecialBaseCheck(value, ref temp, this);
                        if (!test) errMessage += temp;
                        break;
                    //--------------------
                    case rangeChecktype.rctCheckPositive:
                        test = (value >= 0);
                        if (!test) errMessage += positiveCheckErrString(value);
                        break;
                    //--------------------
                    case rangeChecktype.rctUnknown:
                        break;
                    //--------------------
                    case rangeChecktype.rctNoRangeCheck:
                        break;
                    //--------------------
                    default:
                        break;
                }
            }
            else
                throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputBase);
            return test;
        }
        //-------------------------------------------------------------------------------

        /// <summary>   Query if 'value' is in provider ModelPArameter range. </summary>
        /// <param name="value">    The value to be checked. </param>
        /// <param name="provider"> The provider. </param>
        ///
        /// <returns>   true if value in range for this provider, false if not. </returns>

        public bool isProviderValueInRange(int value, eProvider provider)
        {
            string junk = "";
            return isProviderValueInRange(value, provider, ref junk);
        }
        //-------------------------------------------------------------------------------

        /// <summary>   Query if 'value' is in provider ModelPArameter range. </summary>
        /// <exception cref="WaterSim_Exception">   Thrown when ModelParameter is not a modelParamtype.mptInputProvider parameter. </exception>
        ///
        /// <param name="value">        The value to be checked. </param>
        /// <param name="provider">     The provider. </param>
        /// <param name="errMessage">   [in,out] Message describing the error. </param>
        ///
        /// <returns>   true if value in range for this provider, false if not. </returns>

        public bool isProviderValueInRange(int value, eProvider provider, ref string errMessage)
        {
            bool test = true;
            if ((isProviderParam) & (isInputParam))
            {
                switch (FRangeCheckType)
                {
                    case rangeChecktype.rctCheckRange:
                        test = ((value >= FLowRange) & (value <= FHighRange));
                        if (!test) errMessage += rangeCheckErrString(value);
                        break;
                    //--------------------
                    case rangeChecktype.rctCheckRangeSpecial:
                        string temp = "";
                        test = FSpecialProviderCheck(value, provider, ref temp, this);
                        if (!test) errMessage += temp;
                        break;
                    //--------------------
                    case rangeChecktype.rctCheckPositive:
                        test = (value >= 0);
                        if (!test) errMessage += positiveCheckErrString(value);
                        break;
                    //--------------------
                    case rangeChecktype.rctUnknown:
                        break;
                    //--------------------
                    case rangeChecktype.rctNoRangeCheck:
                        break;
                    //--------------------
                    default:
                        break;
                }
            }
            else
                throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputProvider);
            return test;
        }
        //------------------- 

        /// <summary>   Raises the reload event. </summary>
        /// <param name="e">    Event information to send to registered event handlers. </param>

        protected virtual void OnReloadEvent(ModelParameterEventArgs e)
        {
            if (ReloadEvent != null) ReloadEvent(this, e);
        }
        //---------------------------------

        /// <summary>   Evokes reload event for any object listening to this ModelParameter </summary>
        /// <seealso cref="ModelParameterEventArgs"/>
        /// <seealso cref="ReloadEvent"/>
        public void EvokeReloadEvent()
        {
            ModelParameterEventArgs MP = new ModelParameterEventArgs(this);
            OnReloadEvent(MP);
        }
    }


    //=============================================================================
    /// <summary>   Model parameter class.  </summary>
    /// <remarks>   This class allows for flexible use (iteration across all obaject) of all parameter classes managed 
    ///             by the parameter for those experienced in using the different types of parameters and what type of prarameter
    ///             will be returned by each method and property of the parameter manager.  However, this class is less 
    ///             preferred than the individual parameter classes.  
    ///             Evntually the access properties and methods in this class that are specific to individual model parameter
    ///             types will be deprecated, thus using the individual parameter classes is advised.  These provide higher level 
    ///             of type and method safe use with out generating exceptions.  Use of object created from this class
    ///             in a manner not appropriate for the specific model parameter type 
    ///             (inputbase,outputnase, providerinput, provideroutout, grid, etc) can generate exceptions.
    ///             This is the heart of the ModelParameter management system.  Each parameter for the Fortran model 
    ///             exposed in the API has a unique ModelParameter object.. </remarks>

    public class ModelParameterClass : ModelParameterBaseClass
    {
        //----------------------------------------
        //Fields

        /// <summary> The provider array for provider paramter types property </summary>
        protected providerArrayProperty FProviderProperty;


        // NOT IN BASE.............................
        Dget Fget;
        Dset Fset;
        //..................................................

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Specialised default constructor for use only by derived classes. </summary>
        ///-------------------------------------------------------------------------------------------------
        protected ModelParameterClass() : base() { }

        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   For now this constructor is hidden and is included only for compatibility with ealier version
        ///             , it has been replaced with specialized constructors </remarks>
        ///
        /// <param name="aModelParam">                  a model parameter. </param>
        /// <param name="aLabel">                       a label. </param>
        /// <param name="aFieldname">                   a fieldname. </param>
        /// <param name="aParamType">                   Type of a parameter. </param>
        /// <param name="aRangeCheckType">              Type of a range check. </param>
        /// <param name="aLowRange">                    a low range. </param>
        /// <param name="aHighRange">                   a high range. </param>
        /// <param name="agetint">                      The agetint. </param>
        /// <param name="agetintarray">                 The agetintarray. </param>
        /// <param name="asetint">                      The asetint. </param>
        /// <param name="asetintarray">                 The asetintarray. </param>
        /// <param name="specialBaseRangeCheck">        The special base range check. </param>
        /// <param name="specialProviderRangeCheck">    The special provider range check. </param>
        /// <param name="Providerproperty">             The providerproperty. </param>
        
        public ModelParameterClass(int aModelParam, string aLabel, string aFieldname, modelParamtype aParamType,
            rangeChecktype aRangeCheckType, int aLowRange, int aHighRange, Dget agetint, Dgetarray agetintarray, Dset asetint, Dsetarray asetintarray,
            DcheckBase specialBaseRangeCheck, DcheckProvider specialProviderRangeCheck, providerArrayProperty Providerproperty)
            : base()
        {
            FModelParam = aModelParam;
            FLabel = aLabel;
            FFieldname = aFieldname;
            Ftype = aParamType;
            FRangeCheckType = aRangeCheckType;
            FLowRange = aLowRange;
            FHighRange = aHighRange;
            Fget = agetint;
            // Fgetarray = agetintarray;
            Fset = asetint;
            // Fsetarray = asetintarray;
            FSpecialBaseCheck = specialBaseRangeCheck;
            FSpecialProviderCheck = specialProviderRangeCheck;
            FProviderProperty = Providerproperty;
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor for InputProvider Model Parameters. </summary>
        /// <param name="aModelParam">                  a model parameter. </param>
        /// <param name="aLabel">                       a label. </param>
        /// <param name="aFieldname">                   a fieldname. </param>
        /// <param name="aRangeCheckType">              Type of a range check. </param>
        /// <param name="aLowRange">                    a low range. </param>
        /// <param name="aHighRange">                   a high range. </param>
        /// <param name="specialProviderRangeCheck">    The special provider range check. </param>
        /// <param name="Providerproperty">             The providerproperty. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelParameterClass(int aModelParam, string aLabel, string aFieldname, rangeChecktype aRangeCheckType, int aLowRange, int aHighRange, DcheckProvider specialProviderRangeCheck, providerArrayProperty Providerproperty)
            : base()
        {
                FModelParam = aModelParam;
                FLabel = aLabel;
                FFieldname = aFieldname;
                Ftype = modelParamtype.mptInputProvider;
                FRangeCheckType = aRangeCheckType;
                FLowRange = aLowRange;
                FHighRange = aHighRange;
                FSpecialBaseCheck = null;
                FSpecialProviderCheck = specialProviderRangeCheck;
                FProviderProperty = Providerproperty;
                Fget = null;
                Fset = null;
            
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specialised default constructor for OutputProvider ModelParameters  </summary>
        /// <param name="aModelParam">      a model parameter. </param>
        /// <param name="aLabel">           a label. </param>
        /// <param name="aFieldname">       a fieldname. </param>
        /// <param name="Providerproperty"> The providerproperty. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelParameterClass(int aModelParam, string aLabel, string aFieldname, providerArrayProperty Providerproperty)
            : base()
        {
                FModelParam = aModelParam;
                FLabel = aLabel;
                FFieldname = aFieldname;
                Ftype = modelParamtype.mptOutputProvider;
                FRangeCheckType = rangeChecktype.rctUnknown;
                FLowRange = 0;
                FHighRange = 0;
                FSpecialBaseCheck = null;
                FSpecialProviderCheck = null;
                FProviderProperty = Providerproperty;
                Fget = null;
                Fset = null;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor for InputBase Model Parameters. </summary>
        /// <exception cref="WaterSim_Exception">   Thrown when a water simulation error condition
        ///                                         occurs. </exception>
        ///
        /// <param name="aModelParam">              a model parameter. </param>
        /// <param name="aLabel">                   a label. </param>
        /// <param name="aFieldname">               a fieldname. </param>
        /// <param name="aRangeCheckType">          Type of a range check. </param>
        /// <param name="aLowRange">                a low range. </param>
        /// <param name="aHighRange">               a high range. </param>
        /// <param name="agetint">                  The agetint. </param>
        /// <param name="asetint">                  The asetint. </param>
        /// <param name="specialBaseRangeCheck">    The special base range check. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelParameterClass(int aModelParam, string aLabel, string aFieldname,
            rangeChecktype aRangeCheckType, int aLowRange, int aHighRange, Dget agetint, Dset asetint, DcheckBase specialBaseRangeCheck)
            : base()
        {
            FModelParam = aModelParam;
            FLabel = aLabel;
            FFieldname = aFieldname;
            Ftype = modelParamtype.mptInputBase;
            FRangeCheckType = aRangeCheckType;
            FLowRange = aLowRange;
            FHighRange = aHighRange;
            Fget = agetint;
            Fset = asetint;
            FSpecialBaseCheck = specialBaseRangeCheck;
            FSpecialProviderCheck = null;
            FProviderProperty = null;

        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specialised constructor for OutputBase ModelParameters. </summary>
        /// <param name="aModelParam">  a model parameter. </param>
        /// <param name="aLabel">       a label. </param>
        /// <param name="aFieldname">   a fieldname. </param>
        /// <param name="agetint">      The agetint. </param>
        /// <param name="asetint">      The asetint. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelParameterClass(int aModelParam, string aLabel, string aFieldname, Dget agetint)
            : base()
        {
            FModelParam = aModelParam;
            FLabel = aLabel;
            FFieldname = aFieldname;
            Ftype = modelParamtype.mptOutputBase;
            FRangeCheckType = rangeChecktype.rctUnknown;
            FLowRange = 0;
            FHighRange = 0;
            Fget = agetint;
            Fset = null;
            FSpecialBaseCheck = RangeCheck.NoSpecialBase;
            FSpecialProviderCheck = null;
            FProviderProperty = null;
        }

        //------------------------------------------

        /// <summary>   Gets or sets the ModelParameter value. </summary>
        /// <remarks> This is the basic access to base model parameter</remarks>
        /// <value> The value. </value>
        /// <exception cref="WaterSim_Exception">if value or values are out of range for the parameter</exception>

        //public override int Value

        public override int Value
        {
            get
            {
                if (!isBaseParam) throw new WaterSim_Exception(WS_Strings.wsMustBeBaseParameter);
                if (GetInt != null)
                    return GetInt();
                else
                    return -1;
            }
            set
            {
                if ((!isBaseParam) | (!isInputParam)) throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputBase);
                if (Fset != null)
                    Fset(value);
            }
        }
        //------------------------------------------

        /// <summary>   Gets the providerArrayPropertyBaseClass for the ModelParameter. </summary>
        /// <remarks> This provides access to the provider model parameter</remarks>
        /// <value> The provider property. </value>
        /// <exception cref="WaterSim_Exception">if value or values are out of range for the parameter</exception>


        public override providerArrayProperty ProviderProperty
        {
            get
            {
                if (!isProviderParam) throw new WaterSim_Exception(WS_Strings.wsMustBeProviderParameter);
                return FProviderProperty;
            }
        }
        // ------------------------------------------. 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the grid property. </summary>
        ///
        /// <value> The grid property. </value>
        ///
        /// ### <exception cref="WaterSim_Exception">   if parameter is not a Grid Paramemter. </exception>
        ///-------------------------------------------------------------------------------------------------

        public override Grid2DProperty GridProperty 
        {
            get
            {
                throw new WaterSim_Exception(WS_Strings.wsMustBeGridParameter);
 
            }
        }

        //------------------------------------------
        private Dget GetInt { get { return Fget; } }
        private Dset SetInt { get { return Fset; } }
    }

    //---------------------------------------------
    // 

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Model base input parameter class. </summary>
    /// <remarks> Class used to represent Base Model Inputs</remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ModelBaseInputParameterClass : ModelParameterClass
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor for InputBase Model Parameters. </summary>
        /// <exception cref="WaterSim_Exception">   Thrown when a water simulation error condition
        ///                                         occurs. </exception>
        ///
        /// <param name="aModelParam">              a model parameter. </param>
        /// <param name="aLabel">                   a label. </param>
        /// <param name="aFieldname">               a fieldname. </param>
        /// <param name="aRangeCheckType">          Type of a range check. </param>
        /// <param name="aLowRange">                a low range. </param>
        /// <param name="aHighRange">               a high range. </param>
        /// <param name="agetint">                  The agetint. </param>
        /// <param name="asetint">                  The asetint. </param>
        /// <param name="specialBaseRangeCheck">    The special base range check. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelBaseInputParameterClass(int aModelParam, string aLabel, string aFieldname,
            rangeChecktype aRangeCheckType, int aLowRange, int aHighRange, Dget agetint, Dset asetint, DcheckBase specialBaseRangeCheck)
            : base(aModelParam, aLabel, aFieldname, aRangeCheckType, aLowRange, aHighRange, agetint, asetint, specialBaseRangeCheck)
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets the ModelParameter value. </summary>
        ///
        /// <value> The value. </value>
        ///
        /// ### <exception cref="WaterSim_Exception">   if value or values are out of range for the
        ///                                             parameter. </exception>
        ///-------------------------------------------------------------------------------------------------

        public override int Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }
 
    }
    /// <summary>   Model base output parameter class. </summary>
    public class ModelBaseOutputParameterClass : ModelParameterClass
    {
      
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specialised constructor for OutputBase ModelParameters. </summary>
        /// <param name="aModelParam">  a model parameter. </param>
        /// <param name="aLabel">       a label. </param>
        /// <param name="aFieldname">   a fieldname. </param>
        /// <param name="agetint">      The agetint. </param>
        /// <param name="asetint">      The asetint. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelBaseOutputParameterClass(int aModelParam, string aLabel, string aFieldname, Dget agetint)
            : base(aModelParam, aLabel, aFieldname, agetint)
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the ModelParameter value. </summary>
        ///
        /// <value> The value. </value>
        ///
        /// ### <exception cref="WaterSim_Exception">   if value or values are out of range for the
        ///                                             parameter. </exception>
        ///-------------------------------------------------------------------------------------------------

        public override int Value
        {
            get
            {
                return base.Value;
            }
        }
    }

    public class ModelProviderInputParameterCLass : ModelParameterClass
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor for InputProvider Model Parameters. </summary>
        /// <param name="aModelParam">                  a model parameter. </param>
        /// <param name="aLabel">                       a label. </param>
        /// <param name="aFieldname">                   a fieldname. </param>
        /// <param name="aRangeCheckType">              Type of a range check. </param>
        /// <param name="aLowRange">                    a low range. </param>
        /// <param name="aHighRange">                   a high range. </param>
        /// <param name="specialProviderRangeCheck">    The special provider range check. </param>
        /// <param name="Providerproperty">             The providerproperty. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelProviderInputParameterCLass(int aModelParam, string aLabel, string aFieldname, rangeChecktype aRangeCheckType, int aLowRange, int aHighRange, DcheckProvider specialProviderRangeCheck, providerArrayProperty Providerproperty)
            : base(aModelParam,aLabel,aFieldname,aRangeCheckType,aLowRange,aHighRange,specialProviderRangeCheck,Providerproperty)
        {
        }

        public override providerArrayProperty ProviderProperty
        {
            get
            {
                return base.ProviderProperty;
            }
        }
 

    }
    
    public class ModelProviderOutputParameterClass : ModelParameterClass
    {
               ///-------------------------------------------------------------------------------------------------
        /// <summary>   Specialised default constructor for OutputProvider ModelParameters  </summary>
        /// <param name="aModelParam">      a model parameter. </param>
        /// <param name="aLabel">           a label. </param>
        /// <param name="aFieldname">       a fieldname. </param>
        /// <param name="Providerproperty"> The providerproperty. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelProviderOutputParameterClass(int aModelParam, string aLabel, string aFieldname, providerArrayProperty Providerproperty)
            : base(aModelParam,aLabel,aFieldname,Providerproperty)
        {
        }

        public ModelProviderOutputParameterClass(int aModelParam, string aLabel, string aFieldname, eProviderAggregateMode AgMode, providerArrayProperty Providerproperty)
            : base(aModelParam, aLabel, aFieldname, Providerproperty)
        {
            
        }

        public override providerArrayProperty ProviderProperty
        {
            get
            {
                return base.ProviderProperty;
            }
        }
    }
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Modelgridparameterclass - Implements a DataGrid2D Property parameter. </summary>
    ///
    /// <remarks>   Ray Quay, 5/23/2013. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public class ModelGridParameterClass : ModelParameterBaseClass
    {

        Grid2DProperty FGridProperty;

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Ray Quay, 11/17/2013. </remarks>
        ///
        /// <param name="aModelParam">      The model parameter. </param>
        /// <param name="aLabel">           The label. </param>
        /// <param name="aFieldname">       The fieldname. </param>
        /// <param name="aGridProperty">    The grid property. </param>
        ///-------------------------------------------------------------------------------------------------

        public ModelGridParameterClass (int aModelParam, string aLabel, string aFieldname, Grid2DProperty aGridProperty)
        {
            FModelParam = aModelParam;
            FLabel = aLabel;
            FFieldname = aFieldname;
            Ftype = modelParamtype.mptOutput2DGrid;
            FGridProperty = aGridProperty;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Value is not implemented in this class</summary>
        /// ### <exception cref="WaterSim_Exception">   In all cases this returns a not implemented exception </exception>
        ///-------------------------------------------------------------------------------------------------

        public override int Value
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   ProviderProperty is not implemented in this class
        ///             </summary>
        /// ### <exception cref="WaterSim_Exception">   In all cases this returns a not implemented exception </exception>
        ///-------------------------------------------------------------------------------------------------

        public override providerArrayProperty ProviderProperty
        {
            get { throw new NotImplementedException(); }
        }

        // ------------------------------------------. 

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the grid property. </summary>
        ///
        /// <value> The grid property. </value>
        
        ///-------------------------------------------------------------------------------------------------

        public override Grid2DProperty GridProperty
        {
            get
            {
                return FGridProperty;
            }
        }


    }
    //===========================================================
    // PAREAMETER MANAGER CLASS
    // 
    // 
    // ================================================================
    // 

    ///-------------------------------------------------------------------------------------------------
    /// <summary> Values that represent ParameterManagerEventType. </summary>
    ///
    /// <remarks> Used in the EventArgs to indicate the type of Parameter Manager Event. </remarks>
    ///-------------------------------------------------------------------------------------------------

    public enum ParameterManagerEventType { 
        /// <summary> Parameter Added. </summary>
        pmeAdd, 
        /// <summary> Parameter Deleted. </summary>
        pmeDelete };

    ///-------------------------------------------------------------------------------------------------
    /// <summary> Information passed with parameter manager events. </summary>
    ///-------------------------------------------------------------------------------------------------

    public class ParameterManagerEventArgs : EventArgs
    {
        /// <summary> Type of the event. </summary>
        public ParameterManagerEventType TheEventType;
        /// <summary> the Model parameter involved. </summary>
        public ModelParameterBaseClass TheParameter;
        /// <summary> the model parameter code. </summary>
        public int The_eModelParamCode;
        /// <summary> the result. </summary>
        public bool TheResult;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Constructor. </summary>
        /// <param name="EventType">       Type of the event. </param>
        /// <param name="aParameter">      The parameter. </param>
        /// <param name="eModelParamCode"> The model parameter code. </param>
        /// <param name="EventResult">     true to event result. </param>
        ///-------------------------------------------------------------------------------------------------

        public ParameterManagerEventArgs(ParameterManagerEventType EventType, ModelParameterBaseClass aParameter, int eModelParamCode, bool EventResult)
        {
            TheEventType = EventType;
            TheParameter = aParameter;
            The_eModelParamCode = eModelParamCode;
            TheResult = EventResult;
        }

    }
    /// <summary>   Parameter manager class. / </summary>
    /// <remarks> This class is used by the WaterSimManager class to manage the model parameters.  It can be access from a WaterSimManager object through the ParamManager property<see cref="WaterSimManager.ParamManager"/></remarks>
    /// The constructor is not exposed becuase only the WaterSimManager constructor should instantiate an object of this class.   The AddPArameter method is also not exposed at this time.  The intent is to evntually allow user define parameters to be added.

    public class ParameterManagerClass
    {
        /// <summary> A list of ModelParameters </summary>
        protected List<ModelParameterBaseClass> FModelParameters;
        /// <summary> The Model api version. </summary>
        protected string FAPIVersion = "";
        /// <summary> The Interface version. </summary>
        protected string FModelVersion = "";

        internal ParameterManagerClass()
            : base()
        {
            FModelParameters = new List<ModelParameterBaseClass>();
        }
        //----------------------------------------------------------------------------------------
        internal ParameterManagerClass(string anAPIVersion, string ModelVersion)
            : base()
        {
            FModelParameters = new List<ModelParameterBaseClass>();
        }
        //------------------------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the api version. </summary>
        ///
        /// <value> The api version. </value>
        ///-------------------------------------------------------------------------------------------------

        public string APIVersion
        { get { return FAPIVersion; } }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Gets the model version. </summary>
        ///
        /// <value> The model version. </value>
        ///-------------------------------------------------------------------------------------------------

        public string ModelVersion
        { get { return FModelVersion; } }


        /// <summary> Event queue for all listeners interested in ParameterManagerChange events. </summary>
        /// <remarks> Events evoked after a paraneter is added or deleted</remarks>
        public event EventHandler<ParameterManagerEventArgs> ParameterManagerChangeEvent;

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Raises the ParameterManager Change event. </summary>
        /// <param name="e"> Event information to send to registered event handlers. </param>
        ///-------------------------------------------------------------------------------------------------

        protected virtual void OnParameterManagerChangeEvent(ParameterManagerEventArgs e)
        {
            if (ParameterManagerChangeEvent != null) ParameterManagerChangeEvent(this, e);
        }

        
        //------------------------------------------------------------------------
        // Once a ModelParameterClass object has been added, PrameterManager becomes the owner of the object, and 
        // its dispose method is called when it is deleted from the ParameterList
        // 

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Adds a ModelParameterClass object to the ParameterManager parameter list. </summary>
        /// <remarks>Evokes a ParameterManagerChangeEvent 
        ///                  Once a ModelParameterClass object has been added, PrameterManager becomes the owner of the object, and 
        ///                   its dispose method is called when it is deleted from the ParameterList
        /// </remarks>
        /// <exception cref="WaterSim_Exception">   Thrown when a the parameter.ModelParam value is already in the 
        ///                       Parameter List, ie can not have two parameters with the same ModelParam code
        ///                                         </exception>
        /// <param name="parameter"> The parameter. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        /// <seealso cref="ModelParameterClass"/>
        /// <seealso cref="eModelParam"/>
        ///-------------------------------------------------------------------------------------------------

        public bool AddParameter(ModelParameterClass parameter)
        {
            bool mresult = false;
            if (parameter != null)
            {
                if (ModelParamClass.valid(parameter.ModelParam))
                {
                    if (null == FModelParameters.Find(delegate(ModelParameterBaseClass item) { return item.ModelParam == parameter.ModelParam; }))
                    {
                        FModelParameters.Add(parameter);
                        parameter.FParameterManager = this;
                        mresult = true;
                    }
                    else
                        throw new WaterSim_Exception(WS_Strings.wsDuplicateModelParam);
                    int index = (int)parameter.ModelParam;
                }
            }
            OnParameterManagerChangeEvent(new ParameterManagerEventArgs(ParameterManagerEventType.pmeAdd, parameter, parameter.ModelParam, mresult));
            return mresult;
        }

        //public bool AddParameter(ModelParameterClass parameter)   // 7/29 BaseModelParameterClass parameter)
        //{
        //    bool mresult = false;
        //    if (parameter != null)
        //    {
        //        if (ModelParamClass.valid(parameter.ModelParam))
        //        {
        //            if (null == FModelParameters.Find(delegate(ModelParameterBaseClass item) { return item.ModelParam == parameter.ModelParam; }))
        //            {
        //                FModelParameters.Add(parameter);
        //                parameter.FParameterManager = this;
        //                mresult = true;
        //            }
        //            else
        //                throw new WaterSim_Exception(WS_Strings.wsDuplicateModelParam);
        //            int index = (int)parameter.ModelParam;
        //        }
        //    }
        //    OnParameterManagerChangeEvent(new ParameterManagerEventArgs(ParameterManagerEventType.pmeAdd, parameter, parameter.ModelParam, mresult));
        //    return mresult;
        //}

        //------------------------------------------------------------------

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Deletes the ModelParameter described by parameter. </summary>
        ///<remarks>Evokes a ParameterManagerChangeEvent </remarks>     
        /// <param name="parameter"> The parameter. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool DeleteParameter(ModelParameterClass parameter)
        {
            bool test = FModelParameters.Remove(parameter);
            OnParameterManagerChangeEvent(new ParameterManagerEventArgs(ParameterManagerEventType.pmeDelete, parameter, parameter.ModelParam, test));
            parameter.Dispose();
            return test;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Deletes the ModelParameter described by eModelParam value. </summary>
        ///<remarks>Evokes a ParameterManagerChangeEvent </remarks>     
        /// <param name="ModelParam"> The model parameter. </param>
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool DeleteParameter(int ModelParam)
        {
            bool test = false;
            ModelParameterBaseClass MP = Model_ParameterBaseClass(ModelParam);
            if (MP != null)
            { 
                test = FModelParameters.Remove(MP);
            }
            OnParameterManagerChangeEvent(new ParameterManagerEventArgs(ParameterManagerEventType.pmeDelete, MP, MP.ModelParam, test));
            if (MP!=null)
                MP.Dispose();
            return test;
        }

        //------------------------------------------------------------------

        //---------------------------------------------------

        /// <summary>   Gets the number of parameters being managed. </summary>
        /// <returns>   The total number of parameters. </returns>

        public int NumberOfParameters()
        {
            return FModelParameters.Count;
        }

        // Test is valid eModelParam value

        ///-------------------------------------------------------------------------------------------------
        /// <summary> Test if valid eModelParam value </summary>
        /// <param name="p"> The eModelParam value. </param>
        ///
        /// <returns> true if it succeeds, false if it fails. </returns>
        ///-------------------------------------------------------------------------------------------------

        public bool valid(int p)
        {

            return (null != FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.ModelParam == p); }));
        }
        
        //---------------------------------------------------------

        /// <summary>   Queries the number of parameters being managed that are mpt modelParamtype. </summary>
        /// <param name="mpt">  The modelParamtype to be queried. </param>
        ///
        /// <returns>   The total number of parameters. </returns>

        public int NumberOfParameters(modelParamtype mpt)
        {
            int cnt = 0;
            foreach (int mp in eModelParameters())
            {
                if (Model_ParameterBaseClass(mp).ParamType == mpt) cnt++;
            }
            return cnt;
        }

        //---------------------------------------------------------

        /// <summary>   Enumerates all model parameters managed in this collection of ModelPArameters./ </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process model parameters in this
        ///     collection.
        /// </returns>

        public IEnumerable<int> eModelParameters()
        {
            int length = FModelParameters.Count;
            for (int i = 0; i < length; i++)
            {
                yield return FModelParameters[i].ModelParam;
            }
        }
        //---------------------------------------------------------

        /// <summary>   Enumerates all model parameters managed in this collection of ModelPArameters./ </summary>
        /// <returns>
        ///     An enumerator that allow foreach to be used to process model parameters in this
        ///     collection.
        /// </returns>

        public IEnumerable<ModelParameterBaseClass> AllModelParameters()
        {
            int length = NumberOfParameters();
            for (int i = 0; i < length; i++)
            {
                yield return FModelParameters[i];
            }
        }      //---------------------------------------------------------


        /// <summary>   Enumerates base inputs in this collection./ </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process base inputs in this collection.
        /// </returns>
        /// <example><code>
        ///        bool SetOrFail(int [] values)
        ///       {
        ///            bool test = true;
        ///            int index = 0;
        ///             foreach (ModelParameterClass MP in BaseInputs())
        ///             {
        ///             if (MP.isBaseValueInRange(values[index])) MP.Value = values[index];
        ///                    else
        ///                     {
        ///                        test = false;
        ///                         break;
        ///                     }
        ///             index++;
        ///             }
        ///             return test;
        ///        }
        ///        </code>
        ///        </example>
        public IEnumerable<ModelParameterBaseClass> BaseInputs()
        {
            int length = NumberOfParameters();

            for (int i = 0; i < length; i++)
            {
                if (FModelParameters[i].ParamType == modelParamtype.mptInputBase)
                    yield return FModelParameters[i];
            }
        }
        //---------------------------------------------------------

        /// <summary>   Enumerates base outputs in this collection./ </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process base outputs in this collection.
        /// </returns>
        /// <example><code>
        ///     ModelParameterProviderArray MyData = NewOutputBaseArray();
        ///     int index = 0;
        ///     foreach (ModelParameterClass MP in BaseOutputs())
        ///     {
        ///     MyData.Values[index].Values = MP.ProviderProperty.getvalues();
        ///     index++;
        ///     }
        ///     </code>
        /// </example>

        public IEnumerable<ModelParameterBaseClass> BaseOutputs()
        {
            int length = NumberOfParameters();

            for (int i = 0; i < length; i++)
            {
                if (FModelParameters[i].ParamType == modelParamtype.mptOutputBase)
                    yield return FModelParameters[i];
            }
        }
        //---------------------------------------------------------

        /// <summary>   Enumerates provider inputs in this collection./ </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process provider inputs in this
        ///     collection.
        /// </returns>
        /// <example><code>
        ///        bool SetOrFail(ProviderIntArray values)
        ///       {
        ///            bool test = true;
        ///             foreach (eProvider ep in ProviderClass.providers())
        ///                 foreach (ModelParameterClass MP in ProviderOutputs())
        ///                     if (MP.isProviderValueInRange(values[ep], ep)) MP.Value = values[ep];
        ///                    else
        ///                     {
        ///                        test = false;
        ///                         break;
        ///                     }
        ///             return test;
        ///        }
        ///        </code>
        ///        </example>

        public IEnumerable<ModelParameterBaseClass> ProviderInputs()
        {
            int length = NumberOfParameters();

            for (int i = 0; i < length; i++)
            {
                if (FModelParameters[i].ParamType == modelParamtype.mptInputProvider)
                    yield return FModelParameters[i];
            }
        }
        //---------------------------------------------------------

        /// <summary>   Enumerates provider outputs in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process provider outputs in this
        ///     collection.
        /// </returns>
        ///
        /// <example><code>
        ///     ModelParameterProviderArray MyData = NewOutputProviderArray();
        ///     int index = 0;
        ///     foreach (ModelParameterClass MP in ProviderOutputs())
        ///     {
        ///     MyData.Values[index].Values = MP.ProviderProperty.getvalues();
        ///     index++;
        ///     }
        ///     </code>
        /// </example>

        public IEnumerable<ModelParameterBaseClass> ProviderOutputs()
        {
            int length = NumberOfParameters();

            for (int i = 0; i < length; i++)
            {
                if (FModelParameters[i].ParamType == modelParamtype.mptOutputProvider)
                    yield return FModelParameters[i];
            }
        }
        //------------------------------------------------------

        /// <summary>   Creates a new ModelParameterBaseArray for modePAramType.mptInputBase ModelParameters.  <see cref="ModelParameterProviderArray"/> </summary>
        /// <returns> ModelParameterBaseArray  . </returns>
        ///<seealso cref="ModelParameterProviderArray"/>

        public ModelParameterBaseArray NewInputBaseArray()
        {
            return new ModelParameterBaseArray(NumberOfParameters(modelParamtype.mptInputBase));
        }
        //------------------------------------------------------

        /// <summary>   Creates a new ModelParameterBaseArray for modePAramType.mptOutputBase ModelParameters.  /// <see cref="ModelParameterProviderArray"/></summary>
        /// <returns> ModelParameterBaseArray  . </returns>
        /// <seealso cref="ModelParameterProviderArray"/>

        public ModelParameterBaseArray NewOutputBaseArray()
        {
            return new ModelParameterBaseArray(NumberOfParameters(modelParamtype.mptOutputBase));
        }
        //------------------------------------------------------
        /// <summary>   Creates a new ModelParameterProviderArray for modeParamtype.mptInputProvider ModelParameters.  /// <see cref="ModelParameterProviderArray"/></summary>
        /// <returns> ModelParameterProviderArray  . </returns>
        ///  /// <seealso cref="ModelParameterProviderArray"/>

        public ModelParameterProviderArray NewInputProviderArray()
        {
            return new ModelParameterProviderArray(NumberOfParameters(modelParamtype.mptInputProvider));
        }
        //------------------------------------------------------
        /// <summary>   Creates a new ModelParameterProviderArray for modeParamtype.mptOutputProvider ModelParameters. <see cref="ModelParameterProviderArray"/></summary>
        /// <returns> ModelParameterProviderArray  . </returns>
        /// <seealso cref="ModelParameterProviderArray"/>
        public ModelParameterProviderArray NewOutputProviderArray()
        {
            return new ModelParameterProviderArray(NumberOfParameters(modelParamtype.mptOutputProvider));
        }
        //-------------------------------------------------------
        /// <summary>
        /// Creates a new SimulationInputs struct
        /// </summary>
        /// <returns>a new SimulationInputs struct</returns>
        public SimulationInputs NewSimulationInputs()
        {
            SimulationInputs SI = new SimulationInputs(NumberOfParameters(modelParamtype.mptInputBase
                ), NumberOfParameters(modelParamtype.mptInputProvider));
            int index = 0;
            foreach (ModelParameterClass mpc in BaseInputs())
            {
                SI.BaseInputModelParam[index] = mpc.ModelParam;
                index++;
            }
            index = 0;
            foreach (ModelParameterClass mpc in ProviderInputs())
            {
                SI.ProviderInputModelParam[index] = mpc.ModelParam;
                index++;
            }
            return SI;
        }
        //-------------------------------------------------------
        /// <summary>
        /// Creates a new SimulationOutputs struct
        /// </summary>
        /// <returns>a new SimulationOuput struct</returns>
        public SimulationOutputs NewSimulationOutputs()
        {

            SimulationOutputs SO = new SimulationOutputs(NumberOfParameters(modelParamtype.mptOutputBase
                ), NumberOfParameters(modelParamtype.mptOutputProvider));
            int index = 0;
            foreach (ModelParameterClass mpc in BaseOutputs())
            {
                SO.BaseOutputModelParam[index] = mpc.ModelParam;
                index++;
            }
            index = 0;
            foreach (ModelParameterClass mpc in ProviderInputs())
            {
                SO.ProviderOutputModelParam[index] = mpc.ModelParam;
                index++;
            }
            return SO;
        }
        //-------------------------------------------------------
        /// <summary>
        /// Gets All Simulation Input Values from the model and returns them in a SimulationInputs struct
        /// </summary>
        /// <remarks>Some foresight should be used with this method.  This reads the input parameters regardless of the state of the model.  
        /// 		 Though BaseInputs can not be changed once a Simulation has started, the provider inputs can, so the values of these can change 
        /// 		 from one annual run to another.   </remarks>
        /// <returns> all current input parmeter values in a SimulationInputs struct</returns>
        public SimulationInputs GetSimulationInputs()
        {
            SimulationInputs si = NewSimulationInputs();
            int index = 0;
            foreach (ModelParameterClass mp in BaseInputs())
            {
                si.BaseInput[index] = mp.Value; //GetInt();
                si.BaseInputModelParam[index] = mp.ModelParam;
                index++;
            }
            index = 0;
            ProviderIntArray pdata;
            foreach (ModelParameterClass mp in ProviderInputs())
            {
                pdata = si.ProviderInput[index];
                //pdata.Values =mp.GetIntArray();
                si.ProviderInput[index] = mp.ProviderProperty.getvalues();
                si.ProviderInputModelParam[index] = mp.ModelParam;

                index++;
            }
            si.When = DateTime.Now;

            return si;
        }

        internal List<string> SetSimErrors = new List<string>();

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the SetSimulation(SimulationInputs) errors. </summary>
        ///
        /// <remarks>   3/10/2013. </remarks>
        ///
        /// <returns>   List of SetSimulation errors. </returns>
        ///-------------------------------------------------------------------------------------------------

        public List<string> GetSetSimulationErrors()
        {
            List<string> temp = new List<string>();
            foreach (string errstr in SetSimErrors) temp.Add(errstr);
            SetSimErrors.Clear();
            return temp;
        }
        //-------------------------------------------------------
        /// <summary>
        /// Sets all model parameter values using values in a SimulationInputs struct
        /// </summary>
        /// <remarks>Some caution should be used with this method, a true return value only means thet values were in range, not that the
        /// 		 ModelParameters were actually set.  Write access to baseinputs is blocked once a Simulation starts.  Attempting to set them at this Simulation state 
        /// 		 will not succeed, but will return a true value and evoke no exception.
        /// 		 Though teh order of the modelparameter arrays is controled by the order of the enum, there is no guarantee that
        /// 		 they will come into this method in that order.  Thusthis method does not use the parameter iteratires, istead it
        /// 		 uses the eModelParam values in the BaseInputModelParam and ProviderInputModelParam to fetch the appropriate ModelParamter to set its value.
        /// 		   This means the these eModelParm values must be set (which GetSimulationInputs does do)
        ///           if this returns false, then erros ocurred, and GetSetSimulationErros() can be used to retrieve a lits of errors.</remarks>
        /// <param name="si">values to set in model</param>
        /// <returns>true if no errors, including range errors, otherwise false if errors</returns>
        
        public bool SetSimulationInputs(SimulationInputs si)
        {
            bool noerrors = true;
                int ASize = si.BaseInput.Length;
                for (int i = 0; i < ASize; i++)
                {
                    int emp = si.BaseInputModelParam[i];
                    if (ModelParamClass.valid(emp))
                    {
                        ModelParameterClass mp = Model_Parameter(emp);   // 7/29 BaseModelParameterClass mp = BaseModel_ParameterBaseClass(emp);
                        
                        try
                        {
                            if (mp.ParamType == modelParamtype.mptInputBase)
                                mp.Value = si.BaseInput[i];
                            else
                            {
                                SetSimErrors.Add("SetSimulationInputs Error: Bad SimulationInputs form.  " + emp.ToString() + " was found in BaseInputParam array, but is not modelParamtype.mptInputBase.");
                                noerrors = false;
                            }
                        }
                        catch (Exception wsimE)
                        {
                            SetSimErrors.Add("SetSimulationInputs Error: Parameter: " + emp.ToString() + " - " + wsimE.Message);
                            noerrors = false;
                        }
                    }
                    else
                    {
                        SetSimErrors.Add("SetSimulationInputs Error: Undefined Parameter Constant " + emp.ToString() + " was found in BaseInputModelParam array.");
                        noerrors = false;
                    }
                }
                ASize = si.ProviderInput.Length;
                for (int i = 0; i < ASize; i++)
                {
                    int emp = si.ProviderInputModelParam[i];
                    if (ModelParamClass.valid(emp))
                    {
                        ModelParameterClass mp = Model_Parameter(emp);
                        try
                        {
                            if (mp.ParamType == modelParamtype.mptInputProvider)
                                mp.ProviderProperty.setvalues(si.ProviderInput[i]);
                            else
                            {
                                SetSimErrors.Add("SetSimulationInputs Error: Bad SimulationInputs form.  " + emp.ToString() + " was found in ProviderInputParam array, but is not modelParamtype.mptInputProvider.");
                                noerrors = false;
                            }
                        }
                        catch (Exception wsimE)
                        {
                            string badval = "";
                            foreach (int value in si.ProviderInput[i].Values) badval += value.ToString() + ","; 
                            SetSimErrors.Add("SetSimulationInputs Error: Parameter: " + emp.ToString() + " in ProviderInput with values = "+badval+" - " + wsimE.Message);
                            noerrors = false;
                        }
                    }
                    else
                    {
                        SetSimErrors.Add("SetSimulationInputs Error: Undefined Parameter Constant " + emp.ToString() + " was found in ProviderInputModelParam array.");
                        noerrors = false;
                    }
                }
            
            return noerrors;
        }
        //-------------------------------------------------------
        //-------------------------------------------------------

        /// <summary>   Retreives the ModelParameter based on eModelParam value. </summary>
        /// <exception cref="WaterSim_Exception">   Thrown when invlaid eModelParam is used for p. </exception>
        ///<remarks>This is the primary way to access specific ModelParameters.  Each ModelParameter has a unique eModelParam value that can be used to identify and access each ModelParameter.</remarks>
        /// <param name="p">    The eModelParam value. </param>
        /// <see cref="eModelParam"/>
        /// <returns>  a ModelParameterClass based on unique p value, null if not found. </returns>

        public ModelParameterBaseClass Model_ParameterBaseClass(int p)    
        {
            if (ModelParamClass.valid(p))
            {
                ModelParameterBaseClass MP = FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.ModelParam == p); });
                return MP;
            }
            else
            {
                throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + ((int)p).ToString());
            }
        }

        public ModelParameterClass Model_Parameter(int p)
        {
            if (ModelParamClass.valid(p))
            {
                ModelParameterBaseClass MP = FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.ModelParam == p); });
                
                Type Test = MP.GetType();
                Type Test2 = typeof(ModelParameterClass);
                if ((Test.IsSubclassOf(Test2))||(Test==Test2))
                    return (MP as ModelParameterClass);
                else
                    throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + ((int)p).ToString()+"Not ModselParameterClass");
            }
            else
            {
                throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + ((int)p).ToString());
            }
        }

        //public BaseModelParameterClass BaseModel_ParameterBaseClass(int p)
        //{
        //    if (ModelParamClass.valid(p))
        //    {
        //        ModelParameterBaseClass MP =  FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.ModelParam == p); });
        //        if (MP.isBaseParam)
        //            return (MP as BaseModelParameterClass);
        //        else
        //            throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + ((int)p).ToString()+" is not a Base Parameter");
        //    }
        //    else
        //    {
        //        throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + ((int)p).ToString());
        //    }
        //}
        //---------------------------------------------------------------------------
        /// <summary>
        /// Finds the ModelParameter based on a fieldname
        /// </summary>
        /// <param name="fieldname"></param>
        /// <returns>If found returns a ModelParameterClass else returns a null</returns>

        public ModelParameterBaseClass Model_ParameterBaseClass(string fieldname)
        {
            fieldname.ToUpper();
            ModelParameterBaseClass MP = FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.Fieldname.ToUpper() == fieldname); });
            if (MP!=null)
              return MP;
            else
                throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + fieldname );

        }
        
        public ModelParameterClass Model_Parameter(string fieldname)
        {
            fieldname.ToUpper();
            ModelParameterBaseClass MP = FModelParameters.Find(delegate(ModelParameterBaseClass item) { return (item.Fieldname.ToUpper() == fieldname); });
            if ((MP.GetType().IsSubclassOf(typeof(ModelParameterClass)))||(MP.GetType() == typeof(ModelParameterClass)))
            //if (MP.GetType().IsSubclassOf(typeof(ModelParameterClass)))
                return (MP as ModelParameterClass);
            else
                throw new WaterSim_Exception(WS_Strings.Get(WS_Strings.wsInvalid_EModelPAram) + " " + fieldname + " Not ModelParameterClass");

        }

           //---------------------------------------------------------------------------
        //Parameter Groups  

        /// <summary>  List of Reclaimed ModelParameters <remarks>used in range check to check to see if their values add up to more than 100</remarks> </summary>

        public int[] reclaimedchecklist = new[] {
                eModelParam.epPCT_Reclaimed_to_RO, // Deleted 9/1/11 added back 2/12/14
                eModelParam.epPCT_Reclaimed_to_DirectInject,
                eModelParam.epPCT_Reclaimed_to_Water_Supply,
                eModelParam.epPCT_Reclaimed_to_Vadose };

        /// <summary>  List of Effluent ModelParameters <remarks>used in range check to check to see if their values add up to more than 100</remarks> </summary>

        public int[] effluentchecklist = new[] {
 //               eModelParam.epPCT_Effluent_Reclaimed,  // opps this is mislabled, should be wasetWater to reclaimed, not effluent
                eModelParam.epPCT_Effluent_to_PowerPlant,
                eModelParam.epPCT_Effluent_to_Vadose };

        /// <summary>  List of Water Use ModelParameters <remarks>used in range check to check to see if their values add up to more than 100</remarks> </summary>

        public int[] wateruserchecklist = new[] {
                eModelParam.epPCT_WaterSupply_to_Commercial,
                eModelParam.epPCT_WaterSupply_to_Residential,
                eModelParam.epPCT_WaterSupply_to_Industrial
        };

        //------------------------------------------------------------------------

        /// <summary>   Parameter group total. </summary>
        ///
        /// <remarks>   Used to check if the list of ModelParamters adds up to more than 100</remarks>
        ///
        /// <param name="paramlist">    The paramlist. </param>
        /// <param name="provider">     The provider to whose values are checked. </param>
        ///
        /// <returns>   total of provider values for paramlist . </returns>

        public int eParamGroupTotal(int[] paramlist, eProvider provider)
        {
            int total = 0;
            ProviderIntArray pvalues = new ProviderIntArray(0);
            foreach (int ep in paramlist)
            {
                pvalues = Model_ParameterBaseClass(ep).ProviderProperty.getvalues();
                total += pvalues[provider];
            }
            return total;
        }
        //------------------------------------------------------------------------

        /// <summary>   Parameter group message. </summary>
        /// <remarks>Primarily used to create a range error message if values of paramlist will exceed 100</remarks>
        /// <param name="paramlist">    The paramlist. </param>
        /// <param name="provider">     The provider to whose values are checked. </param>
        ///
        /// <returns>   a string that provides information on value for each ModelParameter in paramlist. </returns>

        public string eParamGroupMessage(int[] paramlist, eProvider provider)
        {
            string pmessage = "For Provider" + ProviderClass.Label(provider);
            ProviderIntArray pvalues = new ProviderIntArray(0);
            foreach (int ep in paramlist)
            {
                pvalues = Model_ParameterBaseClass(ep).ProviderProperty.getvalues();
                pmessage += "[ " + Model_ParameterBaseClass(ep).Label + " = " + pvalues[provider].ToString() + " ] ";
            }
            return pmessage;
        }
        ////------------------------------------------------------------------------
        /// <summary>
        ///  Range Check for Base Input Parameters
        /// </summary>
        /// <remarks>Does throw exception on failed range check</remarks>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <returns>true if in range, otherwise throws exception</returns>
        /// <exception cref="WaterSim_Exception"> if range check fails</exception>
        public bool CheckBaseValueRange(int eparam, int value)
        {
            string junk = "";
            bool test = CheckBaseValueRange(eparam, value, true, ref junk);
            return test;
        }
        ////------------------------------------------------------------------------
        /// <summary>
        ///  Range Check for Base Input Parameters
        /// </summary>
        /// <remarks>Does NOT throw exception on failed range check</remarks>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <param name="errMessage">a string that contains an error message if rnage check returns a false vakue</param>
        /// <returns>true if in range, otherwise fakse</returns>

        public bool CheckBaseValueRange(int eparam, int value, ref string errMessage)
        {
            bool test = CheckBaseValueRange(eparam, value, false, ref errMessage);
            return test;
        }
        ////------------------------------------------------------------------------
        /// <summary>
        ///  Protected Base Input Range Check
        /// </summary>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <param name="throwexception">if true throws and exception if range check fails, other wise returns false if range check fails</param>>
        /// <param name="errMessage">a string that contains an error message if rnage check returns a false vakue</param>
        /// <returns>true if in range, otherwise fakse</returns>
        /// <exception cref="WaterSim_Exception"> if throwexception = true and range check fails</exception>
        protected bool CheckBaseValueRange(int eparam, int value, bool throwexception, ref string errMessage)
        {
            string temp = errMessage;
            ModelParameterBaseClass MP;
            bool test = true;
            // Do Safety Checks
            if (!ModelParamClass.valid(eparam)) throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
            MP = Model_ParameterBaseClass(eparam);
            if (!(MP.ParamType == modelParamtype.mptInputBase)) throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputBase);
            // David added this, but need to think if this is correct
            //  {
            //    if (!(MP.ParamType == modelParamtype.mptInputOther))
            //        throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputBase);
            //}

            if (MP.ShouldCheckRange())
                test = MP.isBaseValueInRange(value, ref temp);
            if (throwexception & (!test)) throw new WaterSim_Exception(temp);
            if (!test) errMessage = temp;
            return test;
        }
        ////------------------------------------------------------------------------
        /// <summary>
        ///  Range Check for Provider Input Parameters
        /// </summary>
        /// <remarks>Does throw exception on failed range check</remarks>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <param name="provider">eProvider identifier for provider</param>
        /// <returns>true if in range, otherwise throws exception</returns>
        /// <exception cref="WaterSim_Exception"> if range check fails</exception>
        /// 
        public bool CheckProviderValueRange(int eparam, int value, eProvider provider)
        {
            string junk = "";
            bool test = CheckProviderValueRange(eparam, value, provider, true, ref junk);
            return test;
        }
        ////------------------------------------------------------------------------
        /// <summary>
        ///  Range Check for Provider Input Parameters
        /// </summary>
        /// <remarks>Does NOT throw exception on failed range check</remarks>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <param name="provider">eProvider identifier for provider</param>
        /// <param name="errMessage">a string that contains an error message if rnage check returns a false vakue</param>
        /// <returns>true if in range, otherwise false</returns>
        public bool CheckProviderValueRange(int eparam, int value, eProvider provider, ref string errMessage)
        {
            bool test = CheckProviderValueRange(eparam, value, provider, false, ref errMessage);
            return test;
        }

        ////------------------------------------------------------------------------
        /// <summary>
        ///  protected Provider Input Range Check
        /// </summary>
        /// <param name="eparam">ModelParameter eModelParam identifier</param>
        /// <param name="value">value to be checked</param>
        /// <param name="provider">eProvider identifier for provider</param>
        /// <param name="throwexception">if true throws and exception if range check fails, other wise returns false if range check fails</param>>
        /// <param name="errMessage">a string that contains an error message if rnage check returns a false vakue</param>
        /// <returns>true if in range, otherwise fakse</returns>
        /// <exception cref="WaterSim_Exception"> if throwexception = true and range check fails</exception>
        protected bool CheckProviderValueRange(int eparam, int value, eProvider provider, bool throwexception, ref string errMessage)
        {
            string temp = errMessage;
            ModelParameterBaseClass MP;
            bool test = false;
            // Do Safety Checks
            if (!ModelParamClass.valid(eparam)) throw new WaterSim_Exception(WS_Strings.wsInvalid_EModelPAram);
            MP = Model_ParameterBaseClass(eparam);
            if (MP.ParamType != modelParamtype.mptInputProvider) throw new WaterSim_Exception(WS_Strings.wsModelParamMustBeInputProvider);
            if (MP.ShouldCheckRange())
                test = MP.isProviderValueInRange(value, provider, ref temp);
            if (throwexception & (!test)) throw new WaterSim_Exception(temp);
            if (!test) errMessage = temp;
            return test;
        }
        //--------------------------------------------------

        /// <summary>   Check range for all provider values for specified ModelParameter. </summary>
        /// <param name="eparam">   ModelParameter eModelParam identifier. </param>
        /// <param name="values">   The values to check. </param>
        ///
        /// <returns>   true if all pass range check, throws an exception if any one falis. </returns>
        /// <exception cref="WaterSim_Exception"> if throwexception = true and range check fails</exception>

        public bool CheckProviderValuesRange(int eparam, ProviderIntArray values)
        {
            bool test = true;
            string junk = "";
            test = CheckProviderValuesRange(eparam, values, true, ref junk);
            return test;
        }

        //--------------------------------------------------

        /// <summary>   Protected Check range for all provider values for specified ModelParameter. </summary>
        ///
        /// <remarks>   Ray, 8/5/2011. </remarks>
        ///
        /// <exception cref="WaterSim_Exception">   Thrown if throwexception true and range check fails </exception>
        ///
        /// <param name="eparam">           ModelParameter eModelParam identifier. </param>
        /// <param name="values">           The values to check. </param>
        /// <param name="throwexception">   if throwexception = true and range check fails, other wise
        ///                                 returns false if range check fails. </param>
        /// <param name="errMessage">       [in,out] a string that contains an error message if range
        ///                                 check returns a false vakue. </param>
        ///
        /// <returns>   true if all pass range check, if range check fails, false if throwexception false, throws an exception if throwexception true. </returns>

        protected bool CheckProviderValuesRange(int eparam, ProviderIntArray values, bool throwexception, ref string errMessage)
        {
            bool isValid = true;
            bool test = true;
            string bigerrMessage = "";
            string temp = "";
            foreach (eProvider ep in ProviderClass.providers())
            {
                test = CheckProviderValueRange(eparam, values[ProviderClass.index(ep)], ep, false, ref temp);
                if (!test)
                {
                    isValid = false;
                    bigerrMessage += "[" + ProviderClass.Label(ep) + " " + temp + "]";
                }
            }
            if ((!test) & throwexception) throw new WaterSim_Exception("Invalid Value Found: " + bigerrMessage);
            return isValid;
        }
        //--------------------------------------------------
        internal bool testModelParameters(ref string iferrMessage)//, bool displayerrors)
        // Test to make sure all the Modelparameters are connected.
        {
            string errors = "";
            string temp = "";
            bool test = true;
            //ModelParameterClass MP;
            for (int i = 0; i < FModelParameters.Count; i++)
            {
                if (FModelParameters[i] == null)
                {
                    temp = eModelParam.Names[i] + " is not implemented!";
                    errors += "[" + temp + "]";
                    test = false;
                }
            }
            if (!test)
            {
                iferrMessage = temp;
                //if (displayerrors) System.Windows.Forms.MessageBox.Show(temp);
            }
            else
                iferrMessage = "";

            return test;
        }
    }
    //----------------------------------------
    // End of PramaterManagerClass
    #endregion
}
