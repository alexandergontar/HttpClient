using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WebAPIClient
{
   public static class Methods
    {
        public static string getTag(object sender, RoutedEventArgs e) 
        {
            var tag = ((Button)sender).Tag;
            var i = Convert.ToInt32(tag);
            string strtag = i.ToString();
            Console.WriteLine(strtag);            
            return strtag;
        }

        public static int getTagInt(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag;
            var i = Convert.ToInt32(tag);      
            return i;
        }
    }
}
