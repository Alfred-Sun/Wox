﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Wox.Helper;
using Wox.Plugin;

namespace Wox.Models
{
    [Serializable]
    public class UserSelectedRecords
    {
        private static int hasAddedCount = 0;

        public Dictionary<string,int> Records = new  Dictionary<string, int>();

        public void Add(Result result)
        {
            if (Records.ContainsKey(result.ToString()))
            {
                Records[result.ToString()] += 1;
            }
            else
            {
                Records.Add(result.ToString(), 1);
            }

            //hasAddedCount++;
            //if (hasAddedCount == 10)
            //{
            //    hasAddedCount = 0;
            //}
                CommonStorage.Instance.Save();

        }

        public int GetSelectedCount(Result result)
        {
            if (Records.ContainsKey(result.ToString()))
            {
                return Records[result.ToString()];
            }
            return 0;
        }
    }
}
