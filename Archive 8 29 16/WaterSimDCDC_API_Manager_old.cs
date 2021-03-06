﻿using System;
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




    public partial class WaterSimManager : WaterSimManagerClass
    { 
        protected override void Simulation_Cleanup()
        {
        }

        
        protected override bool runYear(int year)
        {
            return true;
        }

        protected override void initialize_ExtendedDocumentation()
        {
            throw new NotImplementedException();
        }
        
    }


}
