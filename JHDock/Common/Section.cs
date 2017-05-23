using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace JHDock
{

    public class Section : ArrayList
    {
        public string Name;

        public bool IsCommented;
        public Section(string SectionName, bool SectionIsCommented = false)
        {
            Name = SectionName;
            IsCommented = SectionIsCommented;
        }

        public override bool Equals(object obj)
        {
            if (obj == null | (!object.ReferenceEquals(this.GetType(), obj.GetType())))
            {
                return false;
            }
            Section s = (Section)obj;
            return this.Name == s.Name & this.IsCommented == s.IsCommented;
        }

        public override int GetHashCode()
        {
            string s = Name;
            return s.GetHashCode();
        }
    }

}