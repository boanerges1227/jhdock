using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace JHDock
{

    public class SectionComparer : IComparer
    {

        public int Compare(object x, object y)
        {
            string s1 =  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ ((Key)x).Name.ToLower();
            string s2 =  /* TRANSINFO: .NET Equivalent of Microsoft.VisualBasic NameSpace */ ((Key)y).Name.ToLower();
            return s1.CompareTo(s2);
        }
        // interface methods implemented by Compare
        int System.Collections.IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        //public int Compare(object x, object y)
        //{
        //    string s1 = Strings.LCase(x.Name);
        //    string s2 = Strings.LCase(y.Name);
        //    return s1.CompareTo(s2);
        //}
    }

}