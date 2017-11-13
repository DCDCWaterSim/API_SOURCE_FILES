using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaterSimDCDC
{
    //=======================================================
    // Enums 
    //==========================================================
    

    public enum eProvider
    {
        Provider1, Provider2, Provider3, AllProviders
    }

     public static partial class ProviderClass
    {
        // Provider Routines, Constants and enums
        /// <summary>
        /// The last valid provider enum value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider LastProvider = eProvider.Provider3;

        /// <summary>
        /// The first valid enum value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider FirstProvider = eProvider.Provider1;

        /// <summary>
        /// The Last valid Aggregator value
        /// </summary>
        /// <value>eProvider enum</value>
        public const eProvider LastAggregate = eProvider.AllProviders;

        /// <summary>
        /// The number of valid Provider (eProvider) enum values for use with WaterSimModel and ProviderIntArray.
        /// </summary>
        /// <value>count of valid eProvider enums</value>
        /// <remarks>all providers after LastProvider are not considered one of the valid eProvider enum value</remarks>
        public const int NumberOfProviders = (int)LastProvider + 1;

        /// <summary>
        /// The number of valid Provide Aggregate (eProvider) enum values.
        /// </summary>
        /// <value>count of valid eProvider enums</value>
        /// <remarks>all providers after LastProvider are not considered one of the valid eProvider enum value</remarks>
        public static int NumberOfAggregates = ((int)LastAggregate - (int)LastProvider);

        internal const int TotalNumberOfProviderEnums = ((int)LastAggregate)+ 1;

        private static string[] ProviderNameList = new string[TotalNumberOfProviderEnums]    {       
  
            "Provider1",
            "Provider2",
            "Provider3",
            "AllProviders"
             };

        private static string[] FieldNameList = new string[TotalNumberOfProviderEnums]  {       
 
            "p1", "p2", "p3", "all"
    };

        private static eProvider[] RegionProviders = new eProvider[NumberOfProviders] {
            eProvider.Provider1,
            eProvider.Provider2,
            eProvider.Provider3,
        };

 
        public static eProvider[] GetRegion(eProvider ep)
        {
            switch (ep)
            {
                case eProvider.AllProviders:
                    return RegionProviders;
                default:
                    return null;
            }
        }

    }

    //********************************************************************************
    //
    //
    // *******************************************************************************

    ///-------------------------------------------------------------------------------------------------
    /// <summary>   Manager for water simulations. </summary>
    ///
    /// <seealso cref="WaterSimDCDC.WaterSimManagerClass"/>
    ///-------------------------------------------------------------------------------------------------

    public partial class WaterSimManager : WaterSimManagerClass
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor. </summary>
        ///
        /// <param name="DataDirectoryName">    Pathname of the data directory. </param>
        /// <param name="TempDirectoryName">    Pathname of the temp directory. </param>
        ///-------------------------------------------------------------------------------------------------

        public WaterSimManager(string DataDirectoryName, string TempDirectoryName)
            : base(DataDirectoryName, TempDirectoryName)
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Simulation cleanup. </summary>
        ///
        /// <seealso cref="WaterSimDCDC.WaterSimManagerClass.Simulation_Cleanup()"/>
        ///-------------------------------------------------------------------------------------------------

        protected override void Simulation_Cleanup()
        {
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the year operation. </summary>
        ///
        /// <param name="year"> The year. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="WaterSimDCDC.WaterSimManagerClass.runYear(int)"/>
        ///-------------------------------------------------------------------------------------------------

        protected override bool runYear(int year)
        {
            return true;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes the extended documentation. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <seealso cref="WaterSimDCDC.WaterSimManagerClass.initialize_ExtendedDocumentation()"/>
        ///-------------------------------------------------------------------------------------------------

        protected override void initialize_ExtendedDocumentation()
        {
            throw new NotImplementedException();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Executes the model year operation. </summary>
        ///
        /// <exception cref="NotImplementedException"> Thrown when the requested operation is
        ///     unimplemented. </exception>
        ///
        /// <param name="year"> The year. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="WaterSimDCDC.WaterSimManagerClass.RunModelYear(int)"/>
        ///-------------------------------------------------------------------------------------------------

        protected override int RunModelYear(int year)
        {
            return 1;
         }
        
    }


}
