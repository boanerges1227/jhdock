using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace JHDock
{

    public class Key
    {
        public string Name;
        public string Value;
        public bool IsCommented;

        public Key(string KeyName, string KeyValue, bool KeyIsCommented = false)
        {
            Name = KeyName;
            Value = KeyValue;
            IsCommented = KeyIsCommented;
        }

        public override bool Equals(object obj)
        {
            if (obj == null | (!object.ReferenceEquals(this.GetType(), obj.GetType())))
            {
                return false;
            }
            Key k = (Key)obj;
            return this.Name == k.Name & this.Value == k.Value & this.IsCommented == k.IsCommented;
        }

        public override int GetHashCode()
        {
            string s = Name + Value + IsCommented;
            return s.GetHashCode();
        }
    }

}