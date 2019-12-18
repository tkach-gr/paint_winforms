using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Paint
{
    class PropertiesPanel : Panel
    {
        public event Action<Color> ColorChanged;
        public event Action<uint> ToolSizeChanged;
        public event Action<int> ZoomChanged;

        public PropertiesPanel()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            FlowLayoutPanel layoutPanel = new FlowLayoutPanel();
            Label colorHintLabel = new Label();
            PictureBox p1 = new PictureBox();
            PictureBox p2 = new PictureBox();
            PictureBox p3 = new PictureBox();
            PictureBox p4 = new PictureBox();
            PictureBox p5 = new PictureBox();
            PictureBox p6 = new PictureBox();
            Label sizeHintLabel = new Label();
            TrackBar sizeBar = new TrackBar();
            Label sizeInfoLabel = new Label();
            Label zoomHintLabel = new Label();
            Button increaseZoomButton = new Button();
            Button decreaseZoomButton = new Button();
            Label zoomKeysHintLabel = new Label();

            layoutPanel.Name = "MainPanel";
            layoutPanel.FlowDirection = FlowDirection.LeftToRight;
            layoutPanel.Dock = DockStyle.Fill;
            layoutPanel.Controls.Add(colorHintLabel);
            layoutPanel.Controls.Add(p1);
            layoutPanel.Controls.Add(p2);
            layoutPanel.Controls.Add(p3);
            layoutPanel.Controls.Add(p4);
            layoutPanel.Controls.Add(p5);
            layoutPanel.Controls.Add(p6);
            layoutPanel.Controls.Add(sizeHintLabel);
            layoutPanel.Controls.Add(sizeBar);
            layoutPanel.Controls.Add(sizeInfoLabel);
            layoutPanel.Controls.Add(zoomHintLabel);
            layoutPanel.Controls.Add(increaseZoomButton);
            layoutPanel.Controls.Add(decreaseZoomButton);
            layoutPanel.Controls.Add(zoomKeysHintLabel);

            colorHintLabel.Text = "Color:";
            colorHintLabel.AutoSize = true;
            colorHintLabel.Margin = new Padding(10, 7, 0, 0);

            p1.BackColor = Color.Red;
            p1.Size = new Size(20, 20);
            p1.Click += Picture_Click;

            p2.BackColor = Color.Green;
            p2.Size = new Size(20, 20);
            p2.Click += Picture_Click;

            p3.BackColor = Color.Blue;
            p3.Size = new Size(20, 20);
            p3.Click += Picture_Click;

            p4.BackColor = Color.Yellow;
            p4.Size = new Size(20, 20);
            p4.Click += Picture_Click;

            p5.BackColor = Color.White;
            p5.Size = new Size(20, 20);
            p5.Click += Picture_Click;

            p6.BackColor = Color.Black;
            p6.Size = new Size(20, 20);
            p6.Click += Picture_Click;

            sizeHintLabel.Text = "Size:";
            sizeHintLabel.AutoSize = true;
            sizeHintLabel.Margin = new Padding(30, 7, 5, 0);

            sizeBar.Width = 200;
            sizeBar.Value = 10;
            sizeBar.Minimum = 1;
            sizeBar.Maximum = 100;
            sizeBar.ValueChanged += SizeBar_ValueChanged;

            sizeInfoLabel.Name = "SizeInfoLabel";
            sizeInfoLabel.AutoSize = true;
            sizeInfoLabel.Margin = new Padding(0, 7, 0, 0);
            sizeInfoLabel.Text = sizeBar.Value.ToString();

            zoomHintLabel.Text = "Zoom:";
            zoomHintLabel.AutoSize = true;
            zoomHintLabel.Margin = new Padding(30, 7, 5, 0);

            increaseZoomButton.Text = "+";
            increaseZoomButton.Size = new Size(20, 20);
            increaseZoomButton.Click += IncreaseZoomButton_Click;

            decreaseZoomButton.Text = "-";
            decreaseZoomButton.Size = new Size(20, 20);
            decreaseZoomButton.Click += DecreaseZoomButton_Click;

            zoomKeysHintLabel.Text = "(or Ctrl + MouseWheel)";
            zoomKeysHintLabel.AutoSize = true;
            zoomKeysHintLabel.Margin = new Padding(0, 7, 5, 0);

            // PropertiesPanel
            Dock = DockStyle.Fill;
            BackColor = Color.LightGray;
            Controls.Add(layoutPanel);
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            PictureBox picture = sender as PictureBox;
            ColorChanged.Invoke(picture.BackColor);
        }
        private void SizeBar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar sizeBar = sender as TrackBar;
            FlowLayoutPanel mainPanel = Controls["MainPanel"] as FlowLayoutPanel;
            Label sizeInfoLabel = mainPanel.Controls["SizeInfoLabel"] as Label;

            sizeInfoLabel.Text = sizeBar.Value.ToString();
            ToolSizeChanged.Invoke((uint)sizeBar.Value);
        }
        private void IncreaseZoomButton_Click(object sender, EventArgs e)
        {
            ZoomChanged.Invoke(1);
        }
        private void DecreaseZoomButton_Click(object sender, EventArgs e)
        {
            ZoomChanged.Invoke(-1);
        }
    }
}
