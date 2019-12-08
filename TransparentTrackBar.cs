using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VXPMenu
{
    class TransparentTrackBar : TrackBar
    {
        protected override void OnCreateControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            if (Parent != null)
                BackColor = Parent.BackColor;

            base.OnCreateControl();
        }
    }
}
