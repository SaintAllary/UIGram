using System.Windows.Media;

namespace RuslanMessager
{
    internal class MyColorPalette
    {
        private MyColorPalette() {
            StackMenu = new SolidColorBrush(Color.FromRgb(58, 64, 71));
            Primary = new SolidColorBrush(Color.FromRgb(40, 46, 51));
            Secondary = new SolidColorBrush(Color.FromRgb(24, 25, 29));
            Acent = new SolidColorBrush(Color.FromRgb(0, 150, 135));
            ElemRight = new SolidColorBrush(Color.FromRgb(141, 147, 158));
            SearchBG = new SolidColorBrush(Color.FromRgb(61, 68, 75));
            Middle = new SolidColorBrush(Color.FromRgb(127, 127, 127));
            TextPrim = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            TextSec = new SolidColorBrush(Color.FromRgb(127, 127, 127));
            CloseForm = new SolidColorBrush(Color.FromRgb(232, 17, 35));
        }

        public SolidColorBrush StackMenu;
        public SolidColorBrush Primary;
        public SolidColorBrush Secondary;
        public SolidColorBrush Acent;
        public SolidColorBrush ElemRight;
        public SolidColorBrush SearchBG;
        public SolidColorBrush Middle;
        public SolidColorBrush TextPrim;
        public SolidColorBrush TextSec;
        public SolidColorBrush CloseForm;
    }
}