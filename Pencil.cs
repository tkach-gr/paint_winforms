using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class Pencil : Tool
    {
        Graphics graphics;
        Control destination;
        bool isKeyPressed;
        Point mousePosition;
        Pen pen;

        public Pencil(Graphics graphics, Control destination, Pen pen)
        {
            this.destination = destination;
            this.pen = pen;
            this.graphics = graphics;

            isKeyPressed = false;

            mousePosition = new Point(-1, -1);
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            isKeyPressed = true;
            mousePosition = e.Location;
            Draw(mousePosition, e.Location);
        }
        public override void MouseUp(object sender, MouseEventArgs e)
        {
            isKeyPressed = false;
            mousePosition = new Point(-1, -1);
        }
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (isKeyPressed)
            {
                Draw(mousePosition, e.Location);
                mousePosition = e.Location;
            }
        }

        private void Draw(Point first, Point second)
        {
            graphics.DrawLine(pen, first, second);
            destination.Refresh();
        }
    }
}
