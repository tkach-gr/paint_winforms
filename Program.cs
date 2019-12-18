using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class MyForm : Form
    {
        PictureBox pictureBox;
        Bitmap originalImage;
        Graphics pictureBoxGraphics;
        Pen pen;
        Tool tool;

        public MyForm(int width = 800, int height = 600)
        {
            InitializeComponents(width, height);

            pen = new Pen(Color.Green, 10);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            pictureBoxGraphics = Graphics.FromImage(pictureBox.Image);
            pictureBoxGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            originalImage = new Bitmap(width, height);

            tool = new Pencil(Graphics.FromImage(originalImage), pictureBox, pen);
        }

        private void InitializeComponents(int width, int height)
        {
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem fileMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem openFileMenuItem = new ToolStripMenuItem();
            ToolStripMenuItem saveFileMenuItem = new ToolStripMenuItem();
            TableLayoutPanel mainTable = new TableLayoutPanel();
            ToolsPanel toolsPanel = new ToolsPanel();
            PropertiesPanel propertiesPanel = new PropertiesPanel();
            Panel picturePanel = new Panel();
            pictureBox = new PictureBox();

            menu.Items.Add(fileMenuItem);

            fileMenuItem.Text = "File";
            fileMenuItem.DropDownItems.Add(openFileMenuItem);
            fileMenuItem.DropDownItems.Add(saveFileMenuItem);

            openFileMenuItem.Text = "Open";
            openFileMenuItem.Click += OpenFileMenuItem_Click;

            saveFileMenuItem.Text = "Save";
            saveFileMenuItem.Click += SaveFileMenuItem_Click;

            mainTable.RowCount = 3;
            mainTable.ColumnCount = 2;
            mainTable.Dock = DockStyle.Fill;
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 52));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            mainTable.Controls.Add(toolsPanel, 0, 2);
            mainTable.Controls.Add(propertiesPanel, 1, 1);
            mainTable.Controls.Add(picturePanel, 1, 2);

            toolsPanel.PensilClick += ToolsPanel_PensilClick;
            toolsPanel.RectangleClick += ToolsPanel_RectangleClick;
            toolsPanel.EllipseClick += ToolsPanel_EllipseClick;
            toolsPanel.TriangleClick += ToolsPanel_TriangleClick;
            toolsPanel.StarClick += ToolsPanel_StarClick;
            toolsPanel.BucketClick += ToolsPanel_BucketClick;

            propertiesPanel.ColorChanged += PropertiesPanel_ColorChanged;
            propertiesPanel.ToolSizeChanged += PropertiesPanel_ToolSizeChanged;
            propertiesPanel.ZoomChanged += PropertiesPanel_ZoomChanged;

            picturePanel.Dock = DockStyle.Fill;
            picturePanel.AutoScroll = true;
            picturePanel.Controls.Add(pictureBox);

            pictureBox.Image = new Bitmap(width, height);
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseWheel += PictureBox_MouseWheel;

            ClientSize = new Size(width + 65, height + 70);
            MinimumSize = Size;
            Controls.Add(menu);
            Controls.Add(mainTable);
        }

        private void OpenFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "PNG|*.png|JPG|*.jpg";
            DialogResult result = fileDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                originalImage = new Bitmap(fileDialog.FileName);

                pictureBox.Image = new Bitmap(originalImage);
                pictureBox.Refresh();

                pictureBoxGraphics = Graphics.FromImage(pictureBox.Image);
            }
        }
        private void SaveFileMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "PNG|*.png|JPG|*.jpg";
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                pictureBox.Image.Save(fileDialog.FileName);
            }
        }
        private void ToolsPanel_PensilClick()
        {
            tool = new Pencil(Graphics.FromImage(originalImage), pictureBox, pen);
        }
        private void ToolsPanel_RectangleClick()
        {
            tool = new ShapeTool(originalImage, pen, Shape.Rectangle);
        }
        private void ToolsPanel_EllipseClick()
        {
            tool = new ShapeTool(originalImage, pen, Shape.Ellipse);
        }
        private void ToolsPanel_TriangleClick()
        {
            tool = new ShapeTool(originalImage, pen, Shape.Triangle);
        }
        private void ToolsPanel_StarClick()
        {
            tool = new ShapeTool(originalImage, pen, Shape.Star);
        }
        private void ToolsPanel_BucketClick()
        {
            tool = new PaintBucket(originalImage, pen);
        }
        private void PropertiesPanel_ColorChanged(Color color)
        {
            pen.Color = color;
        }
        private void PropertiesPanel_ToolSizeChanged(uint size)
        {
            pen.Width = size;
        }
        private void PropertiesPanel_ZoomChanged(int scale)
        {
            ZoomImage(scale);
        }
        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                int x = (int)(e.X / ((float)pictureBox.Image.Width / originalImage.Width));
                int y = (int)(e.Y / ((float)pictureBox.Image.Height / originalImage.Height));
                MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, e.Clicks, x, y, e.Delta);

                tool.MouseDown(sender, mouseEvent);

                DrawImage();
            }
        }
        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (int)(e.X / ((float)pictureBox.Image.Width / originalImage.Width));
                int y = (int)(e.Y / ((float)pictureBox.Image.Height / originalImage.Height));
                MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, e.Clicks, x, y, e.Delta);

                tool.MouseUp(sender, mouseEvent);

                DrawImage();
            }
        }
        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (int)(e.X / ((float)pictureBox.Image.Width / originalImage.Width));
                int y = (int)(e.Y / ((float)pictureBox.Image.Height / originalImage.Height));
                MouseEventArgs mouseEvent = new MouseEventArgs(MouseButtons.Left, e.Clicks, x, y, e.Delta);

                tool.MouseMove(sender, mouseEvent);

                DrawImage();
            }
        }
        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys != Keys.Control) return;

            ZoomImage(e.Delta);
        }
        private void ZoomImage(int scale)
        {
            int width;
            int height;
            if (scale > 0)
            {
                width = pictureBox.Image.Width * 2;
                height = pictureBox.Image.Height * 2;
            }
            else
            {
                width = pictureBox.Image.Width / 2;
                height = pictureBox.Image.Height / 2;
            }

            if (width < 100 || height < 100 || width >= 5000 || height >= 5000) return;

            pictureBox.Size = new Size(width, height);
            pictureBox.Image = new Bitmap(width, height);
            pictureBoxGraphics = Graphics.FromImage(pictureBox.Image);

            DrawImage();
        }
        private void DrawImage()
        {
            pictureBoxGraphics.Clear(Color.Transparent);
            pictureBoxGraphics.DrawImage(originalImage, new Rectangle(0, 0, pictureBox.Width, pictureBox.Height), new Rectangle(0, 0, originalImage.Width, originalImage.Height), GraphicsUnit.Pixel);
            pictureBox.Refresh();
        }
        public void Run()
        {
            Application.Run(this);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MyForm form = new MyForm();
            form.Run();
        }
    }
}
