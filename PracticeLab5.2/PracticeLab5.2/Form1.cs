using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeLab5._2
{
    public partial class Form1 : Form
    {
        const int A = 5;       // Кількість гілок з однієї точки (пучок)
        const int K = 6;       // Рекурсивна глибина (порядок фрактала)
        const float angleSpread = 60f; // Кутовий розкид між гілками
        const float scale = 0.6f;      // Коефіцієнт зменшення довжини гілки
        const float initialLength = 100f;

        public Form1()
        {
            this.Text = "Фрактал Папороть";
            this.Size = new Size(500, 300);
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            // Початкова точка - центр нижнього краю
            PointF start = new PointF(this.ClientSize.Width / 2, this.ClientSize.Height - 20);
            DrawFern(g, start, -90f, initialLength, K); // -90 градусів вгору
        }

        void DrawFern(Graphics g, PointF p1, float angle, float length, int depth)
        {
            if (depth == 0) return;

            // Малюємо A гілок
            for (int i = 0; i < A; i++)
            {
                // Розрахунок кута для кожної гілки
                float angleOffset = -angleSpread / 2 + i * (angleSpread / (A - 1));
                float newAngle = angle + angleOffset;

                // Обчислення нової точки
                float rad = newAngle * (float)Math.PI / 180f;
                PointF p2 = new PointF(
                    p1.X + (float)(length * Math.Cos(rad)),
                    p1.Y + (float)(length * Math.Sin(rad))
                );

                // Колір для кожної гілки
                Color color = ColorFromHue((360f / A) * i);
                using (Pen pen = new Pen(color, 1))
                {
                    g.DrawLine(pen, p1, p2);
                }

                // Рекурсивний виклик
                DrawFern(g, p2, newAngle, length * scale, depth - 1);
            }
        }

        Color ColorFromHue(float hue)
        {
            return ColorFromHSV(hue, 1.0, 1.0);
        }

        // HSV → RGB перетворення
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = (int)value;
            int p = (int)(value * (1 - saturation));
            int q = (int)(value * (1 - f * saturation));
            int t = (int)(value * (1 - (1 - f) * saturation));

            switch (hi)
            {
                case 0: return Color.FromArgb(255, v, t, p);
                case 1: return Color.FromArgb(255, q, v, p);
                case 2: return Color.FromArgb(255, p, v, t);
                case 3: return Color.FromArgb(255, p, q, v);
                case 4: return Color.FromArgb(255, t, p, v);
                default: return Color.FromArgb(255, v, p, q);
            }
        }
    }
}
