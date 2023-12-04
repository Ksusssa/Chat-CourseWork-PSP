using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace TCPChat.ClassLibrary
{
    public static class UserColors
    {
        public static Color[] noobColors = new Color[5]
        {
            Color.FromArgb(255, 53, 240, 76),
            Color.FromArgb(255, 56, 255, 219), 
            Color.FromArgb(255, 56, 255, 149),
            Color.FromArgb(255, 106, 255, 56),
            Color.FromArgb(255, 56, 228, 255) 
        };
        public static Color[] intermidateColors = new Color[3]
        {
            Color.FromArgb(255, 186, 55, 255),
            Color.FromArgb(255, 255, 87, 168),
            Color.FromArgb(255, 142, 141, 255)
        };
        public static Color proColor = Color.FromArgb(255, 255, 0, 0);
    }
}
