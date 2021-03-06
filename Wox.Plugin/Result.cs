﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Wox.Plugin
{
    public class Result
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string IcoPath { get; set; }
        public Action Action { get; set; }
        public int Score { get; set; }

        public bool DontHideWoxAfterSelect { get; set; }

        /// <summary>
        /// Auto add scores for MRU items
        /// </summary>
        public bool AutoAjustScore { get; set; }

        //todo: this should be controlled by system, not visible to users
        /// <summary>
        /// Only resulsts that originQuery match with curren query will be displayed in the panel
        /// </summary>
        public Query OriginQuery { get; set; }
        /// <summary>
        /// context results connected with current reuslt, usually, it can use <- or -> navigate context results
        /// </summary>
        public List<Result> ContextResults { get; set; }

        /// <summary>
        /// you don't need to set this property if you are developing a plugin
        /// </summary>
        public string PluginDirectory { get; set; }

        public new bool Equals(object obj)
        {
            if (obj == null || !(obj is Result)) return false;

            Result r = (Result)obj;
            return r.Title == Title && r.SubTitle == SubTitle;
        }

        public override string ToString()
        {
            return Title + SubTitle;
        }
    }
}