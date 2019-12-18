using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Paint
{
    class PaintBucket : Tool
    {
        Image image;
        Pen pen;

        public PaintBucket(Image image, Pen pen)
        {
            this.image = image;
            this.pen = pen;
        }

        public override void MouseDown(object sender, MouseEventArgs mouseArgs)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Bitmap bitmap = (Bitmap)image;
            bool[,] cells = new bool[bitmap.Height, bitmap.Width];

            Color newColor = pen.Color;
            Color filledColor = bitmap.GetPixel(mouseArgs.X, mouseArgs.Y);

            Stack<Point> stack = new Stack<Point>(bitmap.Width * bitmap.Height);
            stack.Push(new Point(mouseArgs.X, mouseArgs.Y));
            bitmap.SetPixel(mouseArgs.X, mouseArgs.Y, newColor);

            while (stack.Count != 0)
            {
                Point currentPosition = stack.Pop();
                Point checkedPosition;

                checkedPosition = new Point(currentPosition.X, currentPosition.Y - 1);
                if (checkedPosition.Y >= 0 && CanPaint(bitmap, cells, checkedPosition, filledColor))
                {
                    Paint(bitmap, stack, cells, checkedPosition, newColor);
                }

                checkedPosition = new Point(currentPosition.X - 1, currentPosition.Y);
                if (checkedPosition.X >= 0 && CanPaint(bitmap, cells, checkedPosition, filledColor))
                {
                    Paint(bitmap, stack, cells, checkedPosition, newColor);
                }

                checkedPosition = new Point(currentPosition.X + 1, currentPosition.Y);
                if (checkedPosition.X < bitmap.Width && CanPaint(bitmap, cells, checkedPosition, filledColor))
                {
                    Paint(bitmap, stack, cells, checkedPosition, newColor);
                }

                checkedPosition = new Point(currentPosition.X, currentPosition.Y + 1);
                if (checkedPosition.Y < bitmap.Height && CanPaint(bitmap, cells, checkedPosition, filledColor))
                {
                    Paint(bitmap, stack, cells, checkedPosition, newColor);
                }
            }

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }
        public override void MouseUp(object sender, MouseEventArgs mouseArgs)
        {
        }
        public override void MouseMove(object sender, MouseEventArgs mouseArgs)
        {
        }
        private bool CanPaint(Bitmap bitmap, bool[,] cells, Point checkedPosition, Color filledColor)
        {
            if (cells[checkedPosition.Y, checkedPosition.X] != true && bitmap.GetPixel(checkedPosition.X, checkedPosition.Y) == filledColor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Paint(Bitmap bitmap, Stack<Point> stack, bool[,] cells, Point checkedPosition, Color newColor)
        {
            stack.Push(checkedPosition);
            cells[checkedPosition.Y, checkedPosition.X] = true;
            bitmap.SetPixel(checkedPosition.X, checkedPosition.Y, newColor);
        }
    }
}
