using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    abstract class Tool
    {
        public abstract void MouseDown(object sender, MouseEventArgs mouseArgs);
        public abstract void MouseUp(object sender, MouseEventArgs mouseArgs);
        public abstract void MouseMove(object sender, MouseEventArgs mouseArgs);
    }
}