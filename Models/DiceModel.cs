using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace DiceChallengeMVVM.Models
{
    public class DiceModel
    {
        public BitmapImage BitmapImage { get; set; }
        public int Value { get; set; }
    }
}
