using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    enum Shape
    {
        Rectangle,
        Ellipse,
        Triangle,
        Star,
    }

    class ShapeTool : Tool
    {
        Image originalImage;
        Image currentImage;
        Graphics currentGraphics;
        bool isKeyPressed;
        Point mousePosition;
        Pen pen;
        Shape shape;

        public ShapeTool(Image originalImage, Pen pen, Shape shape)
        {
            this.pen = pen;
            this.originalImage = originalImage;
            this.shape = shape;

            isKeyPressed = false;
            mousePosition = new Point(-1, -1);
        }

        public override void MouseDown(object sender, MouseEventArgs e)
        {
            isKeyPressed = true;
            mousePosition = e.Location;

            currentImage = originalImage;
            currentGraphics = Graphics.FromImage(currentImage);

            originalImage = new Bitmap(currentImage);
        }
        public override void MouseUp(object sender, MouseEventArgs e)
        {
            isKeyPressed = false;
            mousePosition = new Point(-1, -1);

            originalImage = currentImage;
        }
        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (isKeyPressed)
            {
                Draw(mousePosition, e.Location);
            }
        }

        private void Draw(Point first, Point second)
        {
            if (first.X > second.X)
            {
                int temp = first.X;
                first.X = second.X;
                second.X = temp;
            }

            if (first.Y > second.Y)
            {
                int temp = first.Y;
                first.Y = second.Y;
                second.Y = temp;
            }

            Point position = new Point(first.X, first.Y);
            Size size = new Size(second.X - first.X, second.Y - first.Y);

            if (size.Width == 0 || size.Height == 0)
                return;

            size.Width += (int)pen.Width;
            size.Height += (int)pen.Width;

            currentGraphics.Clear(Color.Transparent);
            currentGraphics.DrawImage(originalImage, 0, 0);

            if(shape == Shape.Rectangle)
                DrawRectangle(position, size);
            else if (shape == Shape.Ellipse)
                DrawEllipse(position, size);
            else if (shape == Shape.Triangle)
                DrawTriangle(position, size);
            else if (shape == Shape.Star)
                DrawStar(position, size);
        }
        private void DrawRectangle(Point position, Size size)
        {
            currentGraphics.DrawRectangle(pen, new Rectangle(position, size));
        }
        private void DrawEllipse(Point position, Size size)
        {
            currentGraphics.DrawEllipse(pen, new Rectangle(position, size));
        }
        private void DrawTriangle(Point position, Size size)
        {
            Point[] points = new Point[]
            {
                new Point(position.X + size.Width / 2, position.Y),
                new Point(position.X, position.Y + size.Height),
                new Point(position.X + size.Width, position.Y + size.Height),
                new Point(position.X + size.Width / 2, position.Y),
            };

            currentGraphics.DrawLines(pen, points);
        }
        private void DrawStar(Point position, Size size)
        {
            PointF[] points = new PointF[]
            {
                new PointF(position.X + size.Width / 2, position.Y),
                new PointF(position.X + size.Width * (float)0.35, position.Y + size.Height * (float)0.33),
                new PointF(position.X, position.Y + size.Height * (float)0.33),
                new PointF(position.X + size.Width * (float)0.25, position.Y + size.Height * (float)0.66),
                new PointF(position.X + size.Width * (float)0.15, position.Y + size.Height),
                new PointF(position.X + size.Width / 2, position.Y + size.Height * (float)0.75),
                new PointF(position.X + size.Width * (float)0.85, position.Y + size.Height),
                new PointF(position.X + size.Width * (float)0.75, position.Y + size.Height * (float)0.66),
                new PointF(position.X + size.Width, position.Y + size.Height * (float)0.33),
                new PointF(position.X + size.Width * (float)0.65, position.Y + size.Height * (float)0.33),
                new PointF(position.X + size.Width / 2, position.Y),

                //new PointF(position.X, position.Y + size.Height * (float)0.3),
                //new PointF(position.X + size.Width * (float)0.1, position.Y + size.Height * (float)0.9),
                //new PointF(position.X + size.Width * (float)0.9, position.Y + size.Height * (float)0.9),
                //new PointF(position.X  + size.Width, position.Y + size.Height * (float)0.3),
                //new PointF(position.X + size.Width / 2, position.Y),
            };

            currentGraphics.DrawLines(pen, points);
        }
    }
}
