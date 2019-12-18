using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class ToolsPanel : Panel
    {
        PictureBox selectedToolPictureBox;

        public event Action PensilClick;
        public event Action RectangleClick;
        public event Action EllipseClick;
        public event Action TriangleClick;
        public event Action StarClick;
        public event Action BucketClick;

        public ToolsPanel()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
            PictureBox pencilPicture = new PictureBox();
            PictureBox rectanglePicture = new PictureBox();
            PictureBox ellipsePicture = new PictureBox();
            PictureBox trianglePicture = new PictureBox();
            PictureBox starPicture = new PictureBox();
            PictureBox bucketPicture = new PictureBox();
            
            layoutPanel.Name = "MainPanel";
            layoutPanel.Dock = DockStyle.Left;
            layoutPanel.Width = 50;
            layoutPanel.Dock = DockStyle.Left;
            layoutPanel.Controls.Add(pencilPicture);
            layoutPanel.Controls.Add(rectanglePicture);
            layoutPanel.Controls.Add(ellipsePicture);
            layoutPanel.Controls.Add(trianglePicture);
            layoutPanel.Controls.Add(starPicture);
            layoutPanel.Controls.Add(bucketPicture);

            pencilPicture.Name = "PencilPicture";
            pencilPicture.Image = Image.FromFile("Sources/pencil.png");
            pencilPicture.BackColor = Color.Gray;
            pencilPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pencilPicture.Size = new Size(40, 40);
            pencilPicture.Click += PencilPicture_Click;

            rectanglePicture.Name = "RectanglePicture";
            rectanglePicture.Image = Image.FromFile("Sources/rectangle.png");
            rectanglePicture.SizeMode = PictureBoxSizeMode.Zoom;
            rectanglePicture.Size = new Size(40, 40);
            rectanglePicture.Click += RectanglePicture_Click;

            ellipsePicture.Name = "EllipsePicture";
            ellipsePicture.Image = Image.FromFile("Sources/ellipse.png");
            ellipsePicture.SizeMode = PictureBoxSizeMode.Zoom;
            ellipsePicture.Size = new Size(40, 40);
            ellipsePicture.Click += EllipsePicture_Click;

            trianglePicture.Name = "TrianglePicture";
            trianglePicture.Image = Image.FromFile("Sources/triangle.png");
            trianglePicture.SizeMode = PictureBoxSizeMode.Zoom;
            trianglePicture.Size = new Size(40, 40);
            trianglePicture.Click += TrianglePicture_Click;

            starPicture.Name = "TrianglePicture";
            starPicture.Image = Image.FromFile("Sources/star.png");
            starPicture.SizeMode = PictureBoxSizeMode.Zoom;
            starPicture.Size = new Size(40, 40);
            starPicture.Click += StarPicture_Click;

            bucketPicture.Name = "BucketPicture";
            bucketPicture.Image = Image.FromFile("Sources/bucket.png");
            bucketPicture.SizeMode = PictureBoxSizeMode.Zoom;
            bucketPicture.Size = new Size(40, 40);
            bucketPicture.Click += BucketPicture_Click;

            // ToolsPanel
            BackColor = Color.LightGray;
            AutoSize = true;
            Dock = DockStyle.Fill;
            Controls.Add(layoutPanel);

            selectedToolPictureBox = pencilPicture;
        }

        private void PencilPicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox pencilPicture = sender as PictureBox;
            pencilPicture.BackColor = Color.Gray;

            selectedToolPictureBox = pencilPicture;

            PensilClick.Invoke();
        }
        private void RectanglePicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox rectanglePicture = sender as PictureBox;
            rectanglePicture.BackColor = Color.Gray;

            selectedToolPictureBox = rectanglePicture;

            RectangleClick.Invoke();
        }
        private void EllipsePicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox ellipsePicture = sender as PictureBox;
            ellipsePicture.BackColor = Color.Gray;

            selectedToolPictureBox = ellipsePicture;

            EllipseClick.Invoke();
        }
        private void TrianglePicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox trianglePicture = sender as PictureBox;
            trianglePicture.BackColor = Color.Gray;

            selectedToolPictureBox = trianglePicture;

            TriangleClick.Invoke();
        }
        private void StarPicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox starPicture = sender as PictureBox;
            starPicture.BackColor = Color.Gray;

            selectedToolPictureBox = starPicture;

            StarClick.Invoke();
        }
        private void BucketPicture_Click(object sender, EventArgs e)
        {
            selectedToolPictureBox.BackColor = Color.LightGray;

            PictureBox pencilPicture = sender as PictureBox;
            pencilPicture.BackColor = Color.Gray;

            selectedToolPictureBox = pencilPicture;

            BucketClick.Invoke();
        }
    }
}
