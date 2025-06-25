using System;
using System.Drawing;
using System.Windows.Forms;


namespace PracticeLab5
{
    partial class Form1 : Form
    {
        // Контрольні точки та вектори
        PointF P1 = new PointF(100, 300);
        PointF P2 = new PointF(400, 100);
        PointF V1 = new PointF(100, -200);
        PointF V2 = new PointF(100, 200);

        public Form1()
        {
            InitializeComponent();
            this.Text = "Крива Ерміта";
            this.Size = new Size(600, 500);
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Малюємо контрольні точки та вектори
            DrawControlPoints(g);
            DrawHermiteCurve(g);
        }

        void DrawControlPoints(Graphics g)
        {
            Pen pen = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
            Brush brush = Brushes.Red;

            g.FillEllipse(brush, P1.X - 4, P1.Y - 4, 8, 8);
            g.FillEllipse(brush, P2.X - 4, P2.Y - 4, 8, 8);

            g.DrawLine(pen, P1, new PointF(P1.X + V1.X / 4, P1.Y + V1.Y / 4));
            g.DrawLine(pen, P2, new PointF(P2.X + V2.X / 4, P2.Y + V2.Y / 4));
        }

        void DrawHermiteCurve(Graphics g)
        {
            Pen pen = new Pen(Color.Blue, 2);
            PointF prev = Hermite(0);

            for (float t = 0.01f; t <= 1.0f; t += 0.01f)
            {
                PointF curr = Hermite(t);
                g.DrawLine(pen, prev, curr);
                prev = curr;
            }
        }

        PointF Hermite(float t)
        {
            float h1 = 2 * t * t * t - 3 * t * t + 1;
            float h2 = t * t * t - 2 * t * t + t;
            float h3 = -2 * t * t * t + 3 * t * t;
            float h4 = t * t * t - t * t;

            float x = h1 * P1.X + h2 * V1.X + h3 * P2.X + h4 * V2.X;
            float y = h1 * P1.Y + h2 * V1.Y + h3 * P2.Y + h4 * V2.Y;

            return new PointF(x, y);
        }
    }
}