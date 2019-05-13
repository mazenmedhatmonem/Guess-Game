using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Client
{
    class ComboBoxItem
    {
		/// <summary>
		/// Text of item
		/// </summary>
        public string Text { get; set; }
		/// <summary>
		/// Value of item
		/// </summary>
        public string Value { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
