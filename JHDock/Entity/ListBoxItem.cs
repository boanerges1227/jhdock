using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JHDock
{
    public delegate void ItemClickEvent(object sender, EventArgs e);
    public class ListBoxItem
    {
        private string _name;
        private string _value;
        public event ItemClickEvent  OnItemClick;

        public void Click()
        {
            if (OnItemClick != null)
            {
                OnItemClick(this,new EventArgs());
            }
        }
        public ListBoxItem()
        {

        }
        public ListBoxItem(string name, string value)
        {
            _name = name;
            _value = value;
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public override string ToString()
        {
            return _name;
        }
        
    }
}
