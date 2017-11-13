// ===========================================================
//     WaterSimDCDC Regional Water Demand and Supply Model Version 5.0

//       A method that initializes the initial state of Provider Parameters

//       WaterSimDCDC_API_Provider_Default_parms 
//       Version 1
//       Keeper Ray Quay  ray.quay@asu.edu
//       
//       NOTE This will be replaced eventually with a database.
//       
//       Copyright (C) 2011,2012 , The Arizona Board of Regents
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

namespace WaterSimDCDC
{
     public partial class WaterSimManager
    {
     partial void initialize_Provider_Default_ModelParameters()   
        {

            ProviderIntArray temp = new ProviderIntArray(0);

            // Wastewater to Reclaimed

            int[] WWtoReclaim = new int[ProviderClass.NumberOfProviders] {
                    0	, //0 ad :       Adaman_Mutual,
                    0	, //1 wt :        White_Tanks,
                    0	, //2 pv :        Paradise_Valley,
                    0	, //3 su :        Sun_City,
                    0	, //4 sw :        Sun_City_West,
                    0	, //5 av :        Avondale,
                    0	, //6 be :        Berneil,
                    33	, //7 bu :        Buckeye,
                    100	, //8 cf :        Carefree,
                    0	, //9 cc :        Cave_Creek,
                    0	, //10 ch :        Chandler,
                    100	, //11 cp :        Chaparral_City,
                    83	, //12 sp :        Surprise,
                    0	, //13 cu :        Clearwater_Utilities,
                    0	, //14 dh :        Desert_Hills,
                    0	, //15 em :        El_Mirage,
                    0	, //16 gi :        Gilbert,
                    0	, //17 gl :        Glendale,
                    0	, //18 go :        Goodyear,
                    100	, //19 lp :        Litchfield_Park,
                    1	, //20 me :        Mesa,
                    3	, //23 pe :        Peoria,
                    1	, //24 ph :        Phoenix,
                    0	, //25 qk :        Queen_Creek,
                    0	, //26 rg :        Rigby,
                    100	, //27 rv :        Rio_Verde,
                    0	, //28 ry :        Rose_Valley,
                    16	, //29 sc :        Scottsdale,
                    0	, //30 sr :        Sunrise,
                    6	, //31 te :        Tempe,
                    0	, //32 to :        Tolleson,
                    0	, //33 vu :        Valley_Utilities,
                    0	  //34 we :        West_End,
            
                   };

            temp.Values = WWtoReclaim;
            PCT_Wastewater_Reclaimed.setvalues(temp);



            // Reclaimed to Water Supply

            int[] ReclaimtoSupply = new int[ProviderClass.NumberOfProviders] {
                0	, //0 ad :       Adaman_Mutual,
                0	, //1 wt :        White_Tanks,
                0	, //2 pv :        Paradise_Valley,
                0	, //3 su :        Sun_City,
                0	, //4 sw :        Sun_City_West,
                0	, //5 av :        Avondale,
                0	, //6 be :        Berneil,
                100	, //7 bu :        Buckeye,
                100	, //8 cf :        Carefree,
                0	, //9 cc :        Cave_Creek,
                0	, //10 ch :        Chandler,
                100	, //11 cp :        Chaparral_City,
                100	, //12 sp :        Surprise,
                0	, //13 cu :        Clearwater_Utilities,
                0	, //14 dh :        Desert_Hills,
                0	, //15 em :        El_Mirage,
                0	, //16 gi :        Gilbert,
                0	, //17 gl :        Glendale,
                0	, //18 go :        Goodyear,
                100	, //19 lp :        Litchfield_Park,
                100	, //20 me :        Mesa,
                //0	, //21 no :       No provider
                //0	, //22 op :       Other Provider
                100	, //23 pe :        Peoria,
                100	, //24 ph :        Phoenix,
                0	, //25 qk :        Queen_Creek,
                0	, //26 rg :        Rigby,
                100	, //27 rv :        Rio_Verde,
                0	, //28 ry :        Rose_Valley,
                0	, //29 sc :        Scottsdale,
                0	, //30 sr :        Sunrise,
                100	, //31 te :        Tempe,
                0	, //32 to :        Tolleson,
                0	, //33 vu :        Valley_Utilities,
                0	 //34 we :        West_End,

            };

            temp.Values = ReclaimtoSupply;
            PCT_Reclaimed_to_Water_Supply.setvalues(temp);
            //
            // Reclaimed Water to Vadose

            int[] ReclaimtoVadose = new int[ProviderClass.NumberOfProviders] {
                0	, //0 ad :       Adaman_Mutual,
                0	, //1 wt :        White_Tanks,
                0	, //2 pv :        Paradise_Valley,
                0	, //3 su :        Sun_City,
                0	, //4 sw :        Sun_City_West,
                0	, //5 av :        Avondale,
                0	, //6 be :        Berneil,
                0	, //7 bu :        Buckeye,
                0	, //8 cf :        Carefree,
                0	, //9 cc :        Cave_Creek,
                0	, //10 ch :        Chandler,
                0	, //11 cp :        Chaparral_City,
                0	, //12 sp :        Surprise,
                0	, //13 cu :        Clearwater_Utilities,
                0	, //14 dh :        Desert_Hills,
                0	, //15 em :        El_Mirage,
                0	, //16 gi :        Gilbert,
                0	, //17 gl :        Glendale,
                0	, //18 go :        Goodyear,
                0	, //19 lp :        Litchfield_Park,
                0	, //20 me :        Mesa,
                //0	, //21 no :       No provider
                //0	, //22 op :       Other Provider
                0	, //23 pe :        Peoria,
                0	, //24 ph :        Phoenix,
                0	, //25 qk :        Queen_Creek,
                0	, //26 rg :        Rigby,
                0	, //27 rv :        Rio_Verde,
                0	, //28 ry :        Rose_Valley,
                0	, //29 sc :        Scottsdale,
                0	, //30 sr :        Sunrise,
                0	, //31 te :        Tempe,
                0	, //32 to :        Tolleson,
                0	, //33 vu :        Valley_Utilities,
                0	 //34 we :        West_End,

            };

            temp.Values = ReclaimtoVadose;
            PCT_Reclaimed_to_Vadose.setvalues(temp);
            //
            // Reclaimed Water to Vadose

            int[] ReclaimtoDI = new int[ProviderClass.NumberOfProviders] {
                0	, //0 ad :       Adaman_Mutual,
                0	, //1 wt :        White_Tanks,
                0	, //2 pv :        Paradise_Valley,
                0	, //3 su :        Sun_City,
                0	, //4 sw :        Sun_City_West,
                0	, //5 av :        Avondale,
                0	, //6 be :        Berneil,
                0	, //7 bu :        Buckeye,
                0	, //8 cf :        Carefree,
                0	, //9 cc :        Cave_Creek,
                0	, //10 ch :        Chandler,
                0	, //11 cp :        Chaparral_City,
                0	, //12 sp :        Surprise,
                0	, //13 cu :        Clearwater_Utilities,
                0	, //14 dh :        Desert_Hills,
                0	, //15 em :        El_Mirage,
                0	, //16 gi :        Gilbert,
                0	, //17 gl :        Glendale,
                0	, //18 go :        Goodyear,
                0	, //19 lp :        Litchfield_Park,
                0	, //20 me :        Mesa,
                //0	, //21 no :       No provider
                //0	, //22 op :       Other Provider
                0	, //23 pe :        Peoria,
                0	, //24 ph :        Phoenix,
                0	, //25 qk :        Queen_Creek,
                0	, //26 rg :        Rigby,
                0	, //27 rv :        Rio_Verde,
                0	, //28 ry :        Rose_Valley,
                0	, //29 sc :        Scottsdale,
                0	, //30 sr :        Sunrise,
                0	, //31 te :        Tempe,
                0	, //32 to :        Tolleson,
                0	, //33 vu :        Valley_Utilities,
                0	 //34 we :        West_End,

            };

            temp.Values = ReclaimtoDI;
            PCT_Reclaimed_to_DirectInject.setvalues(temp);
            //

            // Reclaimed to Reverse Osmosis

            int[] ReclaimtoRO = new int[ProviderClass.NumberOfProviders] {

                        0	, //0 ad :       Adaman_Mutual,
                        0	, //1 wt :        White_Tanks,
                        0	, //2 pv :        Paradise_Valley,
                        0	, //3 su :        Sun_City,
                        0	, //4 sw :        Sun_City_West,
                        0	, //5 av :        Avondale,
                        0	, //6 be :        Berneil,
                        0	, //7 bu :        Buckeye,
                        0	, //8 cf :        Carefree,
                        0	, //9 cc :        Cave_Creek,
                        0	, //10 ch :        Chandler,
                        0	, //11 cp :        Chaparral_City,
                        0	, //12 sp :        Surprise,
                        0	, //13 cu :        Clearwater_Utilities,
                        0	, //14 dh :        Desert_Hills,
                        0	, //15 em :        El_Mirage,
                        0	, //16 gi :        Gilbert,
                        0	, //17 gl :        Glendale,
                        0	, //18 go :        Goodyear,
                        0	, //19 lp :        Litchfield_Park,
                        0	, //20 me :        Mesa,
                        //0	, //21 no :       No provider
                        //0	, //22 op :       Other Provider
                        0	, //23 pe :        Peoria,
                        0	, //24 ph :        Phoenix,
                        0	, //25 qk :        Queen_Creek,
                        0	, //26 rg :        Rigby,
                        0	, //27 rv :        Rio_Verde,
                        0	, //28 ry :        Rose_Valley,
                        100	, //29 sc :        Scottsdale,
                        0	, //30 sr :        Sunrise,
                        0	, //31 te :        Tempe,
                        0	, //32 to :        Tolleson,
                        0	, //33 vu :        Valley_Utilities,
                        0	  //34 we :        West_End,
                };

            temp.Values = ReclaimtoRO;
            PCT_Reclaimed_to_RO.setvalues(temp);

            // Reverse Osmosis to Water Supply

            int[] ROtoSupply = new int[ProviderClass.NumberOfProviders] {

                        0	, //0 ad :       Adaman_Mutual,
                        0	, //1 wt :        White_Tanks,
                        0	, //2 pv :        Paradise_Valley,
                        0	, //3 su :        Sun_City,
                        0	, //4 sw :        Sun_City_West,
                        0	, //5 av :        Avondale,
                        0	, //6 be :        Berneil,
                        0	, //7 bu :        Buckeye,
                        0	, //8 cf :        Carefree,
                        0	, //9 cc :        Cave_Creek,
                        0	, //10 ch :        Chandler,
                        0	, //11 cp :        Chaparral_City,
                        0	, //12 sp :        Surprise,
                        0	, //13 cu :        Clearwater_Utilities,
                        0	, //14 dh :        Desert_Hills,
                        0	, //15 em :        El_Mirage,
                        0	, //16 gi :        Gilbert,
                        0	, //17 gl :        Glendale,
                        0	, //18 go :        Goodyear,
                        0	, //19 lp :        Litchfield_Park,
                        0	, //20 me :        Mesa,
                        //0	, //21 no :       No provider
                        //0	, //22 op :       Other Provider
                        0	, //23 pe :        Peoria,
                        0	, //24 ph :        Phoenix,
                        0	, //25 qk :        Queen_Creek,
                        0	, //26 rg :        Rigby,
                        0	, //27 rv :        Rio_Verde,
                        0	, //28 ry :        Rose_Valley,
                        100	, //29 sc :        Scottsdale,
                        0	, //30 sr :        Sunrise,
                        0	, //31 te :        Tempe,
                        0	, //32 to :        Tolleson,
                        0	, //33 vu :        Valley_Utilities,
                        0	  //34 we :        West_End,
                };

            temp.Values = ROtoSupply;
            PCT_RO_to_Water_Supply.setvalues(temp);

            // Max Reclaimed to demand

            int[] ReclaimedDemand = new int[ProviderClass.NumberOfProviders] {

                        25	, //0 ad :       Adaman_Mutual,
                        25	, //1 wt :        White_Tanks,
                        25	, //2 pv :        Paradise_Valley,
                        25	, //3 su :        Sun_City,
                        25	, //4 sw :        Sun_City_West,
                        25	, //5 av :        Avondale,
                        25	, //6 be :        Berneil,
                        25	, //7 bu :        Buckeye,
                        25	, //8 cf :        Carefree,
                        25	, //9 cc :        Cave_Creek,
                        25	, //10 ch :        Chandler,
                        25	, //11 cp :        Chaparral_City,
                        25	, //12 sp :        Surprise,
                        25	, //13 cu :        Clearwater_Utilities,
                        25	, //14 dh :        Desert_Hills,
                        25	, //15 em :        El_Mirage,
                        25	, //16 gi :        Gilbert,
                        25	, //17 gl :        Glendale,
                        25	, //18 go :        Goodyear,
                        25	, //19 lp :        Litchfield_Park,
                        25	, //20 me :        Mesa,
                        //25	, //21 no :       No provider
                        //25	, //22 op :       Other Provider
                        25	, //23 pe :        Peoria,
                        25	, //24 ph :        Phoenix,
                        25	, //25 qk :        Queen_Creek,
                        25	, //26 rg :        Rigby,
                        25	, //27 rv :        Rio_Verde,
                        25	, //28 ry :        Rose_Valley,
                        25	, //29 sc :        Scottsdale,
                        25	, //30 sr :        Sunrise,
                        25	, //31 te :        Tempe,
                        25	, //32 to :        Tolleson,
                        25	, //33 vu :        Valley_Utilities,
                        25	, //34 we :        West_End,
                    };
            temp.Values = ReclaimedDemand;
            PCT_Max_Demand_Reclaim.setvalues(temp);


            // Wastewater to Effluent

            int[] WWtoEffluent = new int[ProviderClass.NumberOfProviders] {
              
                        0	, //0 ad :       Adaman_Mutual,
                        0	, //1 wt :        White_Tanks,
                        0	, //2 pv :        Paradise_Valley,
                        0	, //3 su :        Sun_City,
                        0	, //4 sw :        Sun_City_West,
                        100	, //5 av :        Avondale,
                        0	, //6 be :        Berneil,
                        52	, //7 bu :        Buckeye,
                        0	, //8 cf :        Carefree,
                        0	, //9 cc :        Cave_Creek,
                        100	, //10 ch :        Chandler,
                        100	, //11 cp :        Chaparral_City,
                        100	, //12 sp :        Surprise,
                        0	, //13 cu :        Clearwater_Utilities,
                        0	, //14 dh :        Desert_Hills,
                        96	, //15 em :        El_Mirage,
                        0	, //16 gi :        Gilbert,
                        91	, //17 gl :        Glendale,
                        45	, //18 go :        Goodyear,
                        100	, //19 lp :        Litchfield_Park,
                        71	, //20 me :        Mesa,
                        //0	, //21 no :       No provider
                       // 0	, //22 op :       Other Provider
                        56	, //23 pe :        Peoria,
                        81	, //24 ph :        Phoenix,
                        0	, //25 qk :        Queen_Creek,
                        0	, //26 rg :        Rigby,
                        100	, //27 rv :        Rio_Verde,
                        0	, //28 ry :        Rose_Valley,
                        61	, //29 sc :        Scottsdale,
                        0	, //30 sr :        Sunrise,
                        100	, //31 te :        Tempe,
                        0	, //32 to :        Tolleson,
                        0	, //33 vu :        Valley_Utilities,
                        0	, //34 we :        West_End,
                 };

            temp.Values = WWtoEffluent;
            PCT_Wastewater_to_Effluent.setvalues(temp);


            // Effluent to Vadose

            int[] EffluenttoVadose = new int[ProviderClass.NumberOfProviders] {


                    0	, //0 ad :       Adaman_Mutual,
                    0	, //1 wt :        White_Tanks,
                    0	, //2 pv :        Paradise_Valley,
                    0	, //3 su :        Sun_City,
                    0	, //4 sw :        Sun_City_West,
                    100	, //5 av :        Avondale,
                    0	, //6 be :        Berneil,
                    5	, //7 bu :        Buckeye,
                    0	, //8 cf :        Carefree,
                    0	, //9 cc :        Cave_Creek,
                    54	, //10 ch :        Chandler,
                    0	, //11 cp :        Chaparral_City,
                    16	, //12 sp :        Surprise,
                    0	, //13 cu :        Clearwater_Utilities,
                    0	, //14 dh :        Desert_Hills,
                    96	, //15 em :        El_Mirage,
                    0	, //16 gi :        Gilbert,
                    49	, //17 gl :        Glendale,
                    45	, //18 go :        Goodyear,
                    0	, //19 lp :        Litchfield_Park,
                    0	, //20 me :        Mesa,
                    //0	, //21 no :       No provider
                   // 0	, //22 op :       Other Provider
                    38	, //23 pe :        Peoria,
                    4	, //24 ph :        Phoenix,
                    0	, //25 qk :        Queen_Creek,
                    0	, //26 rg :        Rigby,
                    0	, //27 rv :        Rio_Verde,
                    0	, //28 ry :        Rose_Valley,
                    18	, //29 sc :        Scottsdale,
                    0	, //30 sr :        Sunrise,
                    16	, //31 te :        Tempe,
                    0	, //32 to :        Tolleson,
                    0	, //33 vu :        Valley_Utilities,
                    0	, //34 we :        West_End,
               };

            temp.Values = EffluenttoVadose;
            PCT_Effluent_to_Vadose.setvalues(temp);

            // Effluent to POWER

            int[] EffluenttoPower = new int[ProviderClass.NumberOfProviders] {

                    0	, //0 ad :       Adaman_Mutual,
                    0	, //1 wt :        White_Tanks,
                    0	, //2 pv :        Paradise_Valley,
                    0	, //3 su :        Sun_City,
                    0	, //4 sw :        Sun_City_West,
                    0	, //5 av :        Avondale,
                    0	, //6 be :        Berneil,
                    0	, //7 bu :        Buckeye,
                    0	, //8 cf :        Carefree,
                    0	, //9 cc :        Cave_Creek,
                    0	, //10 ch :        Chandler,
                    0	, //11 cp :        Chaparral_City,
                    0	, //12 sp :        Surprise,
                    0	, //13 cu :        Clearwater_Utilities,
                    0	, //14 dh :        Desert_Hills,
                    0	, //15 em :        El_Mirage,
                    0	, //16 gi :        Gilbert,
                    17	, //17 gl :        Glendale,
                    0	, //18 go :        Goodyear,
                    0	, //19 lp :        Litchfield_Park,
                    14	, //20 me :        Mesa,
                   // 0	, //21 no :       No provider
                   // 0	, //22 op :       Other Provider
                    0	, //23 pe :        Peoria,
                    36	, //24 ph :        Phoenix,
                    0	, //25 qk :        Queen_Creek,
                    0	, //26 rg :        Rigby,
                    0	, //27 rv :        Rio_Verde,
                    0	, //28 ry :        Rose_Valley,
                    19	, //29 sc :        Scottsdale,
                    0	, //30 sr :        Sunrise,
                    55	, //31 te :        Tempe,
                    0	, //32 to :        Tolleson,
                    0	, //33 vu :        Valley_Utilities,
                    0	, //34 we :        West_End,

               };

            temp.Values = EffluenttoPower;
            PCT_Effluent_to_PowerPlant.setvalues(temp);

            // Water Banking

            int[] AmntToBank = new int[ProviderClass.NumberOfProviders] {

                    0	, //0 ad :       Adaman_Mutual,
                    0	, //1 wt :        White_Tanks,
                    0	, //2 pv :        Paradise_Valley,
                    0	, //3 su :        Sun_City,
                    0	, //4 sw :        Sun_City_West,
                    18814	, //5 av :        Avondale,
                    0	, //6 be :        Berneil,
                    25	, //7 bu :        Buckeye,
                    0	, //8 cf :        Carefree,
                    0	, //9 cc :        Cave_Creek,
                    0	, //10 ch :        Chandler,
                    0	, //11 cp :        Chaparral_City,
                    10249	, //12 sp :        Surprise,
                    0	, //13 cu :        Clearwater_Utilities,
                    0	, //14 dh :        Desert_Hills,
                    508	, //15 em :        El_Mirage,
                    0	, //16 gi :        Gilbert,
                    145	, //17 gl :        Glendale,
                    3531	, //18 go :        Goodyear,
                    0	, //19 lp :        Litchfield_Park,
                    376	, //20 me :        Mesa,
                   // 0	, //21 no :       No provider
                   // 0	, //22 op :       Other Provider
                    0	, //23 pe :        Peoria,
                    2017	, //24 ph :        Phoenix,
                    0	, //25 qk :        Queen_Creek,
                    0	, //26 rg :        Rigby,
                    0	, //27 rv :        Rio_Verde,
                    0	, //28 ry :        Rose_Valley,
                    3963	, //29 sc :        Scottsdale,
                    0	, //30 sr :        Sunrise,
                    5	, //31 te :        Tempe,
                    0	, //32 to :        Tolleson,
                    0	, //33 vu :        Valley_Utilities,
                    0	, //34 we :        West_End,

               };

            temp.Values = AmntToBank;
            Use_SurfaceWater_to_WaterBank.setvalues(temp);
            // Initialize the banking method to 2
            for (int i = 0; i < AmntToBank.Length; i++)
            {
                if (AmntToBank[i] > 0) WaterBank_Source_Option[i] = 2;
            }
         //
         //
            // DAS
            // Based on toilet use-
            /// <summary>   The pct outdoor use for residential water users. </summary>
            int[] PCT_outdoor_use_residential = new int[ProviderClass.NumberOfProviders] {
                     72  , //0 ad :       Adaman_Mutual,
                     42  , //1 wt :        White_Tanks,
                     91  , //2 pv :        Paradise_Valley,
                     65  , //3 su :        Sun_City,
                     52  , //4 sw :        Sun_City_West,
                     39  , //5 av :        Avondale,
                     88  , //6 be :        Berneil,
                     61  , //7 bu :        Buckeye,
                     76  , //8 cf :        Carefree,
                     69  , //9 cc :        Cave_Creek,
                     60  , //10 ch :        Chandler,
                     64  , //11 cp :        Chaparral_City,
                     43  , //12 sp :        Surprise,
                     38  , //13 cu :        Clearwater_Utilities,
                     40  , //14 dh :        Desert_Hills,
                     43  , //15 em :        El_Mirage,
                     58  , //16 gi :        Gilbert,
                     50  , //17 gl :        Glendale,
                     49  , //18 go :        Goodyear,
                     71  , //19 lp :        Litchfield_Park,
                     52  , //20 me :        Mesa,
                   // 0	, //21 no :       No provider
                   // 0	, //22 op :       Other Provider
                     47  , //23 pe :        Peoria,
                     55  , //24 ph :        Phoenix,
                     65  , //25 qk :        Queen_Creek,
                     51  , //26 rg :        Rigby,
                     93  , //27 rv :        Rio_Verde,
                     52  , //28 ry :        Rose_Valley,
                     71  , //29 sc :        Scottsdale,
                     59  , //30 sr :        Sunrise,
                     70  , //31 te :        Tempe,
                     73  , //32 to :        Tolleson,
                     34  , //33 vu :        Valley_Utilities,
                     51  , //34 we :        West_End,                  

                };
            temp.Values = PCT_outdoor_use_residential;
            PCT_Outdoor_WaterUseRes.setvalues(temp);
            /// <summary>   The pct watersupply to residential water users. </summary>
            int[] PCT_Watersupply_to_Res = new int[ProviderClass.NumberOfProviders] {

                    31     , //0 ad :        Adaman_Mutual, (the remaining data come from the excell data sheet for the model)
                    70     , //1 wt :        White_Tanks, - used average from all data
                    65     , //2 pv :        Paradise_Valley,
                    79     , //3 su :        Sun_City,
                    75     , //4 sw :        Sun_City_West,
                    66     , //5 av :        Avondale,
                    98     , //6 be :        Berneil,
                    63     , //7 bu :        Buckeye,
                    75     , //8 cf :        Carefree,
                    65     , //9 cc :        Cave_Creek,
                    60     , //10 ch :       Chandler,
                    78     , //11 cp :       Chaparral_City,
                    65     , //12 sp :       Surprise,
                    99     , //13 cu :       Clearwater_Utiliti
                    96     , //14 dh :       Desert_Hills,
                    77     , //15 em :       El_Mirage,
                    72     , //16 gi :       Gilbert,
                    72     , //17 gl :       Glendale,
                    58     , //18 go :       Goodyear,
                    61     , //19 lp :       Litchfield_Park,
                    64     , //20 me :       Mesa,
                   // 0	   , //21 no :       No provider
                   // 0	   , //22 op :       Other Provider
                    72     , //23 pe :       Peoria,
                    61     , //24 ph :       Phoenix, - used data from a 4/23/12 document- GPCD city of phoenix 1990 to 2011
                    73     , //25 qk :       Queen_Creek,
                    95     , //26 rg :       Rigby,
                    25     , //27 rv :       Rio_Verde,
                    79     , //28 ry :       Rose_Valley,
                    70     , //29 sc :       Scottsdale,
                    96     , //30 sr :       Sunrise,
                    51     , //31 te :       Tempe,
                    17     , //32 to :       Tolleson,
                    76     , //33 vu :       Valley_Utilities,
                    91     , //34 we :       West_End,   
                };
            temp.Values = PCT_Watersupply_to_Res;
            // set all values to zero first
            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                PCT_WaterSupply_to_Residential[i] = 0;
            }
            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                PCT_WaterSupply_to_Commercial[i] = 0;
            }
            for (int i = 0; i < ProviderClass.NumberOfProviders; i++)
            {
                PCT_WaterSupply_to_Industrial[i] = 0;
            }


            PCT_WaterSupply_to_Residential.setvalues(temp);
            /// <summary>   The pct watersupply to commercial water users. </summary>
            int[] PCT_Watersupply_to_Com = new int[ProviderClass.NumberOfProviders] {

                  34       , //0 ad :       Adaman_Mutual, (the remaining data come from the excell data sheet for the model)
                  21      , //1 wt :        White_Tanks, - used average from all data
                  20      , //2 pv :        Paradise_Valley,
                  16      , //3 su :        Sun_City,
                  23      , //4 sw :        Sun_City_West,
                  20      , //5 av :        Avondale,
                  2       , //6 be :        Berneil,
                  19      , //7 bu :        Buckeye,
                  23      , //8 cf :        Carefree,
                  20      , //9 cc :        Cave_Creek,
                  18      , //10 ch :        Chandler,
                  16      , //11 cp :        Chaparral_City,
                  20      , //12 sp :        Surprise,
                  1       , //13 cu :        Clearwater_Utiliti
                  2       , //14 dh :        Desert_Hills,
                  23      , //15 em :        El_Mirage,
                  22      , //16 gi :        Gilbert,
                  22      , //17 gl :        Glendale,
                  17      , //18 go :        Goodyear,
                  18      , //19 lp :        Litchfield_Park,
                  19      , //20 me :        Mesa,
                // 0	 , //21 no :       No provider
                // 0	 , //22 op :       Other Provider
                  22      , //23 pe :        Peoria,
                  18      , //24 ph :        Phoenix, - used data from a 4/23/12 document- GPCD city of phoenix 1990 to 2011
                  22      , //25 qk :        Queen_Creek,
                  5       , //26 rg :        Rigby,
                  8       , //27 rv :        Rio_Verde,
                  16      , //28 ry :        Rose_Valley,
                  21      , //29 sc :        Scottsdale,
                  2       , //30 sr :        Sunrise,
                  15      , //31 te :        Tempe,
                  5       , //32 to :        Tolleson,
                  23      , //33 vu :        Valley_Utilities,
                  9       , //34 we :        West_End,   
                };
            temp.Values = PCT_Watersupply_to_Com;
            PCT_WaterSupply_to_Commercial.setvalues(temp);

            /// <summary>   The pct watersupply to industrial water users. </summary>
            int[] PCT_Watersupply_to_Ind = new int[ProviderClass.NumberOfProviders] {

                    35   , //0 ad :       Adaman_Mutual, (the remaining data come from the excell data sheet for the model)
                     9   , //1 wt :        White_Tanks, - used average from all data
                    15   , //2 pv :        Paradise_Valley,
                     5   , //3 su :        Sun_City,
                     2   , //4 sw :        Sun_City_West,
                    14   , //5 av :        Avondale,
                     0   , //6 be :        Berneil,
                    18   , //7 bu :        Buckeye,
                     2   , //8 cf :        Carefree,
                    15   , //9 cc :        Cave_Creek,
                    22   , //10 ch :        Chandler,
                     6   , //11 cp :        Chaparral_City,
                    15   , //12 sp :        Surprise,
                     0   , //13 cu :        Clearwater_Utiliti
                     2   , //14 dh :        Desert_Hills,
                     0   , //15 em :        El_Mirage,
                     6   , //16 gi :        Gilbert,
                     6   , //17 gl :        Glendale,
                    25   , //18 go :        Goodyear,
                    21   , //19 lp :        Litchfield_Park,
                    17   , //20 me :        Mesa,
                  // 0	 , //21 no :       No provider
                  // 0	 , //22 op :       Other Provider
                     6   , //23 pe :        Peoria,
                    21   , //24 ph :        Phoenix, - used data from a 4/23/12 document- GPCD city of phoenix 1990 to 2011
                     5   , //25 qk :        Queen_Creek,
                     0   , //26 rg :        Rigby,
                    67   , //27 rv :        Rio_Verde,
                     5   , //28 ry :        Rose_Valley,
                     9   , //29 sc :        Scottsdale,
                     2   , //30 sr :        Sunrise,
                    34   , //31 te :        Tempe,
                    78   , //32 to :        Tolleson,
                     1   , //33 vu :        Valley_Utilities,
                     0   , //34 we :        West_End,   
                };
            temp.Values = PCT_Watersupply_to_Ind;
            PCT_WaterSupply_to_Industrial.setvalues(temp);
         //
            /// <summary>   The pct watersupply to industrial water users. </summary>
            int[] pct_alter_GPCD = new int[ProviderClass.NumberOfProviders] {

                    0    , //0 ad :       Adaman_Mutual, (the remaining data come from the excell data sheet for the model)
                     2   , //1 wt :        White_Tanks, - used average from all data
                    0    , //2 pv :        Paradise_Valley,
                    -3   , //3 su :        Sun_City,
                     0   , //4 sw :        Sun_City_West,
                    0   , //5 av :        Avondale,
                     0   , //6 be :        Berneil,
                    -2   , //7 bu :        Buckeye,
                     0   , //8 cf :        Carefree,
                    0   , //9 cc :        Cave_Creek,
                    0   , //10 ch :        Chandler,
                     -3   , //11 cp :        Chaparral_City,
                    -5   , //12 sp :        Surprise,
                     0   , //13 cu :        Clearwater_Utiliti
                     -2   , //14 dh :        Desert_Hills,
                     -7   , //15 em :        El_Mirage,
                     0   , //16 gi :        Gilbert,
                     -2   , //17 gl :        Glendale,
                    0   , //18 go :        Goodyear,
                    -10   , //19 lp :        Litchfield_Park,
                    -6   , //20 me :        Mesa,
                  // 0	 , //21 no :       No provider
                  // 0	 , //22 op :       Other Provider
                     0   , //23 pe :        Peoria,
                    -2   , //24 ph :        Phoenix, - used data from a 4/23/12 document- GPCD city of phoenix 1990 to 2011
                     -9   , //25 qk :        Queen_Creek,
                     0   , //26 rg :        Rigby,
                    0   , //27 rv :        Rio_Verde,
                     -3   , //28 ry :        Rose_Valley,
                     0   , //29 sc :        Scottsdale,
                     5   , //30 sr :        Sunrise,
                    -7   , //31 te :        Tempe,
                    7   , //32 to :        Tolleson,
                     0   , //33 vu :        Valley_Utilities,
                     0   , //34 we :        West_End,   
                };
            temp.Values = pct_alter_GPCD;
            PCT_alter_GPCD.setvalues(temp);
         // 
        }

    }
}