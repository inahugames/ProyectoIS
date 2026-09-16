using System.Drawing.Text;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using BLL_54CS;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using Servicios;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ProyectoIS
{
    public class UsuariosRoundedPanel : Panel
    {
        public int CornerRadius { get; set; } = 18;
        public Color BorderColor { get; set; } = Color.Transparent;
        public int BorderThickness { get; set; } = 1;

        public UsuariosRoundedPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            Padding = new Padding(14);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Parent == null ? BackColor : Parent.BackColor);
            Rectangle surface = new Rectangle(2, 2, Math.Max(1, Width - 5), Math.Max(1, Height - 5));
            int radius = Math.Min(CornerRadius, Math.Min(surface.Width, surface.Height) / 2);
            using (GraphicsPath shadowPath = CreatePath(new Rectangle(2, 5, Math.Max(1, Width - 5), Math.Max(1, Height - 5)), radius))
            using (SolidBrush shadow = new SolidBrush(Color.FromArgb(18, Color.Black)))
            using (GraphicsPath path = CreatePath(surface, radius))
            using (SolidBrush brush = new SolidBrush(BackColor))
            using (Pen pen = new Pen(BorderColor == Color.Transparent ? Color.FromArgb(69, 69, 66) : BorderColor, BorderThickness))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(shadow, shadowPath);
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private static GraphicsPath CreatePath(Rectangle bounds, int radius)
        {
            int diameter = Math.Max(2, Math.Min(Math.Min(bounds.Width, bounds.Height), radius * 2));
            GraphicsPath path = new GraphicsPath();
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.X;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    public enum UsuariosStatIconKind
    {
        Users,
        Active,
        Blocked
    }

    public class UsuariosStatIcon : Control
    {
        public UsuariosStatIconKind Kind { get; set; }
        public Color Accent { get; set; } = Color.FromArgb(75, 125, 190);

        public UsuariosStatIcon()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Size = new Size(48, 48);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush background = new SolidBrush(Color.FromArgb(34, Accent))) e.Graphics.FillEllipse(background, 0, 0, Width - 1, Height - 1);
            using (Pen pen = new Pen(Accent, 2.2F))
            {
                if (Kind == UsuariosStatIconKind.Users)
                {
                    e.Graphics.DrawEllipse(pen, 14, 10, 10, 10);
                    e.Graphics.DrawEllipse(pen, 25, 13, 8, 8);
                    e.Graphics.DrawArc(pen, 8, 20, 23, 18, 180, 180);
                    e.Graphics.DrawArc(pen, 22, 22, 18, 14, 180, 180);
                }
                else if (Kind == UsuariosStatIconKind.Active)
                {
                    e.Graphics.DrawEllipse(pen, 18, 9, 12, 12);
                    e.Graphics.DrawArc(pen, 11, 22, 27, 19, 180, 180);
                    e.Graphics.DrawLine(pen, 29, 31, 33, 35);
                    e.Graphics.DrawLine(pen, 33, 35, 40, 27);
                }
                else
                {
                    e.Graphics.DrawArc(pen, 16, 9, 16, 15, 180, 180);
                    e.Graphics.DrawRectangle(pen, 12, 17, 24, 21);
                    e.Graphics.DrawLine(pen, 24, 24, 24, 31);
                    e.Graphics.DrawEllipse(pen, 22, 22, 4, 4);
                }
            }
        }
    }

    public class UsuariosThemeSwitch : UsuariosRoundedButton
    {
        private readonly Timer slideTimer;
        private bool darkMode;
        private float position;

        public bool DarkMode
        {
            get => darkMode;
            set
            {
                darkMode = value;
                if (IsHandleCreated) slideTimer.Start();
                else position = value ? 1F : 0F;
                Invalidate();
            }
        }

        public UsuariosThemeSwitch()
        {
            slideTimer = new Timer { Interval = 15 };
            slideTimer.Tick += (sender, args) =>
            {
                float target = darkMode ? 1F : 0F;
                position += (target - position) * 0.24F;
                if (Math.Abs(target - position) < 0.005F)
                {
                    position = target;
                    slideTimer.Stop();
                }
                Invalidate();
            };
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            AccessibleName = Text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) slideTimer.Dispose();
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PintarFondoContenedor(e);
            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color track = darkMode ? Color.FromArgb(40, 40, 39) : Color.FromArgb(229, 233, 240);
            Color border = darkMode ? Color.FromArgb(64, 64, 61) : Color.FromArgb(208, 215, 225);
            Color thumb = darkMode ? Color.FromArgb(29, 29, 28) : Color.White;
            float diameter = Height - 8F;
            float left = 4F + position * (Width - diameter - 8F);
            using (GraphicsPath path = new GraphicsPath())
            using (SolidBrush trackBrush = new SolidBrush(track))
            using (Pen borderPen = new Pen(border, 1.5F))
            using (SolidBrush thumbBrush = new SolidBrush(thumb))
            {
                float arc = Height - 3F;
                path.AddArc(1.5F, 1.5F, arc, arc, 90, 180);
                path.AddArc(Width - arc - 1.5F, 1.5F, arc, arc, 270, 180);
                path.CloseFigure();
                graphics.FillPath(trackBrush, path);
                graphics.DrawPath(borderPen, path);
                graphics.FillEllipse(thumbBrush, left, 4F, diameter, diameter);
                graphics.DrawEllipse(borderPen, left, 4F, diameter, diameter);
            }
            float cy = Height / 2F;
            float sunX = 4F + diameter / 2F;
            Color sun = darkMode ? Color.FromArgb(185, 185, 180) : Color.FromArgb(52, 103, 190);
            using (Pen pen = new Pen(Enabled ? sun : SystemColors.GrayText, 1.7F))
            {
                graphics.DrawEllipse(pen, sunX - 4F, cy - 4F, 8F, 8F);
                for (int i = 0; i < 8; i++)
                {
                    double angle = i * Math.PI / 4;
                    float x = (float)Math.Cos(angle);
                    float y = (float)Math.Sin(angle);
                    graphics.DrawLine(pen, sunX + x * 7F, cy + y * 7F, sunX + x * 9F, cy + y * 9F);
                }
            }
            float moonX = Width - 4F - diameter / 2F;
            using (GraphicsPath moon = new GraphicsPath())
            using (SolidBrush brush = new SolidBrush(Enabled ? (darkMode ? Color.FromArgb(238, 162, 126) : Color.FromArgb(109, 117, 132)) : SystemColors.GrayText))
            {
                moon.AddArc(moonX - 9F, cy - 9F, 18F, 18F, 0, 270);
                moon.AddBezier(moonX, cy - 9F, moonX - 6F, cy + 1F, moonX - 1F, cy + 7F, moonX + 9F, cy);
                moon.CloseFigure();
                graphics.FillPath(brush, moon);
            }
            if (Focused && ShowFocusCues)
                ControlPaint.DrawFocusRectangle(graphics, new Rectangle(7, 7, Width - 14, Height - 14), sun, track);
        }
    }

    public enum UsuariosButtonIcon { None, Edit, Unlock, Power, Delete, Key, Arrow, Check, UserAdd, Filter, Document, Folder, Database, Refresh }

    public class UsuariosRoundedButton : Button
    {
        public UsuariosButtonIcon Icon { get; set; }
        public Color HoverBackColor { get; set; } = Color.Empty;
        private bool hovering;
        private Timer hoverTimer;
        private int hoverProgress;

        public UsuariosRoundedButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            hoverTimer = new Timer { Interval = 15 };
            hoverTimer.Tick += (s, e) =>
            {
                int target = hovering ? 100 : 0;
                hoverProgress += target > hoverProgress ? 12 : -12;
                if ((target == 100 && hoverProgress >= 100) || (target == 0 && hoverProgress <= 0))
                {
                    hoverProgress = target;
                    hoverTimer.Stop();
                }
                Invalidate();
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hoverTimer?.Stop();
                hoverTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovering = true;
            hoverTimer.Start();
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovering = false;
            hoverTimer.Start();
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected void PintarFondoContenedor(PaintEventArgs e)
        {
            if (Parent != null)
            {
                GraphicsState state = e.Graphics.Save();
                try
                {
                    e.Graphics.TranslateTransform(-Left, -Top);
                    Rectangle clip = e.ClipRectangle;
                    clip.Offset(Left, Top);
                    using (PaintEventArgs parentArgs = new PaintEventArgs(e.Graphics, clip))
                    {
                        InvokePaintBackground(Parent, parentArgs);
                        InvokePaint(Parent, parentArgs);
                    }
                }
                finally
                {
                    e.Graphics.Restore(state);
                }
            }
            else
            {
                e.Graphics.Clear(SystemColors.Control);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PintarFondoContenedor(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(0, 0, Width - 1, Height - 1);
            using (GraphicsPath path = CreatePath(bounds, Math.Min(14, Height / 2)))
            {
            Color fill = BackColor;
            Color hoverColor = HoverBackColor == Color.Empty ? FlatAppearance.MouseOverBackColor : HoverBackColor;
            if (hoverColor != Color.Empty && hoverProgress > 0)
                fill = Color.FromArgb(BackColor.R + (hoverColor.R - BackColor.R) * hoverProgress / 100, BackColor.G + (hoverColor.G - BackColor.G) * hoverProgress / 100, BackColor.B + (hoverColor.B - BackColor.B) * hoverProgress / 100);
            using (SolidBrush brush = new SolidBrush(fill))
            using (Pen pen = new Pen(FlatAppearance.BorderColor, Math.Max(1, FlatAppearance.BorderSize)))
            using (StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                e.Graphics.FillPath(brush, path);
                if (FlatAppearance.BorderSize > 0) e.Graphics.DrawPath(pen, path);
                Color contentColor = Enabled ? ForeColor : SystemColors.GrayText;
                RectangleF textBounds = bounds;
                if (Icon != UsuariosButtonIcon.None)
                {
                    float textWidth = Math.Min(e.Graphics.MeasureString(Text, Font).Width, Width - 46F);
                    float start = Math.Max(12F, (Width - textWidth - 24F) / 2F);
                    DrawIcon(e.Graphics, start, (Height - 16F) / 2F, contentColor);
                    textBounds = new RectangleF(start + 24F, 0, Math.Max(1F, Width - start - 34F), Height - 1);
                    format.Alignment = StringAlignment.Near;
                    format.FormatFlags = StringFormatFlags.NoWrap;
                    format.Trimming = StringTrimming.EllipsisCharacter;
                }
                using (SolidBrush textBrush = new SolidBrush(contentColor))
                    e.Graphics.DrawString(Text, Font, textBrush, textBounds, format);
                if (Focused) ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(bounds, -4, -4), ForeColor, fill);
            }
            }
        }

        private void DrawIcon(Graphics graphics, float x, float y, Color color)
        {
            GraphicsState state = graphics.Save();
            try
            {
                graphics.TranslateTransform(x, y);
                using (Pen pen = new Pen(color, 1.6F) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                {
                    if (Icon == UsuariosButtonIcon.Refresh)
                    {
                        graphics.DrawArc(pen, 1, 1, 14, 14, 35, 285);
                        graphics.DrawLines(pen, new[] { new PointF(11, 1), new PointF(14, 4), new PointF(15, 0) });
                    }
                    else if (Icon == UsuariosButtonIcon.Folder)
                    {
                        graphics.DrawLines(pen, new[] { new PointF(1, 13), new PointF(1, 2), new PointF(6, 2), new PointF(8, 4), new PointF(14, 4), new PointF(14, 6) });
                        graphics.DrawPolygon(pen, new[] { new PointF(1, 14), new PointF(4, 6), new PointF(16, 6), new PointF(13, 14) });
                    }
                    else if (Icon == UsuariosButtonIcon.Database)
                    {
                        graphics.DrawEllipse(pen, 2, 0, 12, 4);
                        graphics.DrawLine(pen, 2, 2, 2, 13);
                        graphics.DrawLine(pen, 14, 2, 14, 13);
                        graphics.DrawArc(pen, 2, 6, 12, 4, 0, 180);
                        graphics.DrawArc(pen, 2, 11, 12, 4, 0, 180);
                    }
                    else if (Icon == UsuariosButtonIcon.Filter)
                    {
                        graphics.DrawPolygon(pen, new[] { new PointF(1, 1), new PointF(15, 1), new PointF(10, 7), new PointF(10, 14), new PointF(6, 12), new PointF(6, 7) });
                    }
                    else if (Icon == UsuariosButtonIcon.Document)
                    {
                        graphics.DrawPolygon(pen, new[] { new PointF(3, 0), new PointF(10, 0), new PointF(14, 4), new PointF(14, 16), new PointF(3, 16) });
                        graphics.DrawLines(pen, new[] { new PointF(10, 0), new PointF(10, 4), new PointF(14, 4) });
                        graphics.DrawLine(pen, 6, 8, 11, 8);
                        graphics.DrawLine(pen, 6, 12, 11, 12);
                    }
                    else if (Icon == UsuariosButtonIcon.UserAdd)
                    {
                        graphics.DrawEllipse(pen, 3, 0, 7, 7);
                        graphics.DrawArc(pen, 0, 9, 12, 12, 180, 180);
                        graphics.DrawLine(pen, 10, 12, 16, 12);
                        graphics.DrawLine(pen, 13, 9, 13, 15);
                    }
                    else if (Icon == UsuariosButtonIcon.Check)
                    {
                        graphics.DrawEllipse(pen, 0, 0, 16, 16);
                        graphics.DrawLines(pen, new[] { new PointF(4, 8), new PointF(7, 11), new PointF(12, 5) });
                    }
                    else if (Icon == UsuariosButtonIcon.Arrow)
                    {
                        graphics.DrawLine(pen, 1, 8, 15, 8);
                        graphics.DrawLines(pen, new[] { new PointF(9, 2), new PointF(15, 8), new PointF(9, 14) });
                    }
                    else if (Icon == UsuariosButtonIcon.Key)
                    {
                        graphics.DrawEllipse(pen, 8, 1, 7, 7);
                        graphics.DrawLine(pen, 9, 7, 2, 14);
                        graphics.DrawLine(pen, 2, 14, 1, 11);
                        graphics.DrawLine(pen, 5, 11, 3, 9);
                    }
                    else if (Icon == UsuariosButtonIcon.Edit)
                    {
                        graphics.DrawPolygon(pen, new[] { new PointF(2, 14), new PointF(3, 10), new PointF(12, 1), new PointF(15, 4), new PointF(6, 13) });
                        graphics.DrawLine(pen, 10, 3, 13, 6);
                    }
                    else if (Icon == UsuariosButtonIcon.Unlock)
                    {
                        graphics.DrawRectangle(pen, 2, 7, 10, 8);
                        graphics.DrawArc(pen, 7, 0, 7, 8, 180, 200);
                        graphics.DrawLine(pen, 7, 4, 7, 7);
                        graphics.DrawLine(pen, 7, 10, 7, 12);
                    }
                    else if (Icon == UsuariosButtonIcon.Power)
                    {
                        graphics.DrawArc(pen, 1, 2, 14, 13, -55, 290);
                        graphics.DrawLine(pen, 8, 0, 8, 8);
                    }
                    else if (Icon == UsuariosButtonIcon.Delete)
                    {
                        graphics.DrawLine(pen, 2, 4, 14, 4);
                        graphics.DrawRectangle(pen, 6, 1, 4, 3);
                        graphics.DrawLines(pen, new[] { new PointF(4, 5), new PointF(5, 15), new PointF(11, 15), new PointF(12, 5) });
                        graphics.DrawLine(pen, 7, 7, 7, 12);
                        graphics.DrawLine(pen, 9, 7, 9, 12);
                    }
                }
            }
            finally { graphics.Restore(state); }
        }

        private static GraphicsPath CreatePath(Rectangle bounds, int radius)
        {
            int diameter = Math.Max(2, radius * 2);
            GraphicsPath path = new GraphicsPath();
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.X;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    public sealed class PrincipalButton : UsuariosRoundedButton
    {
        private string caption;
        public string Caption { get => caption; set { caption = value; AccessibleName = value; Invalidate(); } }
        public string Subtitle { get; set; }
        public PrincipalGlyph Glyph { get; set; }
        public bool Large { get; set; }
        public bool Expandable { get; set; }
        public bool Expanded { get; set; }
        public Color Accent { get; set; }
        public Color Muted { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int iconSize = Large ? 38 : 20;
            int left = Glyph == PrincipalGlyph.None ? 14 : Large ? 76 : 42;
            int right = Large || Expandable ? 30 : 10;
            var textBounds = new Rectangle(left, Large ? Height / 2 - 28 : 0, Math.Max(1, Width - left - right), Large ? 28 : Height);
            TextRenderer.DrawText(e.Graphics, Caption, Font, textBounds, Enabled ? ForeColor : SystemColors.GrayText, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
            if (Large)
            {
                using (Font font = new Font(Font.FontFamily, 9.5F))
                    TextRenderer.DrawText(e.Graphics, Subtitle, font, new Rectangle(left, Height / 2 + 5, Math.Max(1, Width - left - right), 40), Muted, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
            }
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (Glyph != PrincipalGlyph.None)
            {
                GraphicsState state = e.Graphics.Save();
                e.Graphics.TranslateTransform(Large ? 22 : 12, (Height - iconSize) / 2F);
                e.Graphics.ScaleTransform(iconSize / 24F, iconSize / 24F);
                using (Pen pen = new Pen(Enabled ? Accent : SystemColors.GrayText, 1.5F) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round }) DrawGlyph(e.Graphics, pen);
                e.Graphics.Restore(state);
            }
            using (Pen pen = new Pen(Accent, 1.6F))
            {
                int x = Width - 20, y = Height / 2;
                if (Large) { e.Graphics.DrawLine(pen, x - 5, y, x + 5, y); e.Graphics.DrawLines(pen, new[] { new Point(x, y - 5), new Point(x + 5, y), new Point(x, y + 5) }); }
                else if (Expandable) e.Graphics.DrawLines(pen, new[] { new Point(x - 4, y + (Expanded ? 2 : -2)), new Point(x, y + (Expanded ? -2 : 2)), new Point(x + 4, y + (Expanded ? 2 : -2)) });
            }
        }

        private void DrawGlyph(Graphics g, Pen p)
        {
            switch (Glyph)
            {
                case PrincipalGlyph.Home:
                    g.DrawLines(p, new[] { new Point(2, 11), new Point(12, 2), new Point(22, 11), new Point(20, 11), new Point(20, 22), new Point(14, 22), new Point(14, 15), new Point(10, 15), new Point(10, 22), new Point(4, 22), new Point(4, 11) }); break;
                case PrincipalGlyph.Users:
                case PrincipalGlyph.Account:
                    g.DrawEllipse(p, 5, 2, 9, 9); g.DrawArc(p, 1, 13, 17, 17, 180, 180);
                    if (Glyph == PrincipalGlyph.Users) { g.DrawArc(p, 14, 4, 7, 8, 250, 210); g.DrawArc(p, 14, 14, 9, 13, 270, 90); } break;
                case PrincipalGlyph.Shield:
                    g.DrawPolygon(p, new[] { new Point(3, 5), new Point(12, 1), new Point(21, 5), new Point(19, 16), new Point(12, 23), new Point(5, 16) }); break;
                case PrincipalGlyph.Layers:
                    g.DrawPolygon(p, new[] { new Point(1, 7), new Point(12, 2), new Point(23, 7), new Point(12, 12) });
                    g.DrawLines(p, new[] { new Point(1, 12), new Point(12, 17), new Point(23, 12) });
                    g.DrawLines(p, new[] { new Point(1, 17), new Point(12, 22), new Point(23, 17) }); break;
                case PrincipalGlyph.Events:
                    for (int y = 4; y <= 20; y += 8) { g.DrawEllipse(p, 1, y - 1, 2, 2); g.DrawLine(p, 8, y, 23, y); } break;
                case PrincipalGlyph.Database:
                    g.DrawEllipse(p, 3, 2, 18, 6); g.DrawLine(p, 3, 5, 3, 19); g.DrawLine(p, 21, 5, 21, 19); g.DrawArc(p, 3, 9, 18, 6, 0, 180); g.DrawArc(p, 3, 16, 18, 6, 0, 180); break;
                case PrincipalGlyph.Help:
                    g.DrawEllipse(p, 2, 2, 20, 20); g.DrawArc(p, 9, 6, 6, 6, 180, 270); g.DrawLine(p, 12, 12, 12, 14); g.DrawEllipse(p, 11.5F, 17, 1, 1); break;
                default:
                    for (int x = 3; x < 20; x += 11) for (int y = 3; y < 20; y += 11) g.DrawRectangle(p, x, y, 7, 7); break;
            }
        }
    }

    public sealed class LoginGlyph : Control
    {
        public bool Lock { get; set; }
        public bool Key { get; set; }
        public LoginGlyph()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.ScaleTransform(Width / 24F, Height / 24F);
            using (Pen pen = new Pen(ForeColor, 1.4F) { LineJoin = LineJoin.Round })
            {
                if (Key)
                {
                    e.Graphics.DrawEllipse(pen, 11, 2, 11, 11);
                    e.Graphics.DrawEllipse(pen, 16, 5, 3, 3);
                    e.Graphics.DrawLines(pen, new[] { new PointF(12, 11), new PointF(2, 21), new PointF(2, 23), new PointF(6, 23), new PointF(6, 20), new PointF(9, 20), new PointF(9, 17), new PointF(15, 12) });
                }
                else if (Lock)
                {
                    e.Graphics.DrawArc(pen, 7, 2, 10, 12, 180, 180);
                    e.Graphics.DrawRectangle(pen, 4, 10, 16, 12);
                    e.Graphics.DrawEllipse(pen, 10, 14, 4, 4);
                    e.Graphics.DrawLine(pen, 12, 18, 12, 20);
                }
                else
                {
                    e.Graphics.DrawEllipse(pen, 7, 2, 10, 10);
                    e.Graphics.DrawEllipse(pen, 3, 15, 18, 7);
                }
            }
        }
    }

    public sealed class LoginEyeButton : UsuariosRoundedButton
    {
        public bool Revealed { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(ForeColor, 1.5F))
            {
                e.Graphics.DrawEllipse(pen, 5, 8, 20, 12);
                e.Graphics.DrawEllipse(pen, 12, 11, 6, 6);
                if (!Revealed) e.Graphics.DrawLine(pen, 6, 5, 25, 23);
            }
        }
    }

    public sealed class AltaGlyph : Control
    {
        public int Kind { get; set; }
        public AltaGlyph() { SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.ScaleTransform(Width / 24F, Height / 24F);
            using (Pen pen = new Pen(ForeColor, 1.5F) { LineJoin = LineJoin.Round })
            {
                if (Kind == 0)
                {
                    e.Graphics.DrawEllipse(pen, 5, 2, 10, 10);
                    e.Graphics.DrawArc(pen, 2, 14, 16, 12, 180, 180);
                    e.Graphics.DrawLine(pen, 15, 18, 23, 18);
                    e.Graphics.DrawLine(pen, 19, 14, 19, 22);
                }
                else if (Kind == 1)
                {
                    e.Graphics.DrawRectangle(pen, 2, 4, 20, 16);
                    e.Graphics.DrawEllipse(pen, 5, 7, 5, 5);
                    e.Graphics.DrawArc(pen, 4, 13, 8, 6, 180, 180);
                    e.Graphics.DrawLine(pen, 14, 9, 19, 9);
                    e.Graphics.DrawLine(pen, 14, 14, 19, 14);
                }
                else if (Kind == 2)
                {
                    e.Graphics.DrawRectangle(pen, 2, 5, 20, 14);
                    e.Graphics.DrawLines(pen, new[] { new PointF(2, 5), new PointF(12, 13), new PointF(22, 5) });
                }
                else if (Kind == 4)
                {
                    e.Graphics.DrawPolygon(pen, new[] { new PointF(3, 21), new PointF(5, 15), new PointF(17, 3), new PointF(22, 8), new PointF(10, 20) });
                    e.Graphics.DrawLine(pen, 14, 6, 19, 11);
                }
                else e.Graphics.DrawPolygon(pen, new[] { new PointF(3, 4), new PointF(12, 1), new PointF(21, 4), new PointF(20, 15), new PointF(12, 23), new PointF(4, 15) });
            }
        }
    }

    public sealed class BitacoraGlyph : Control
    {
        public BitacoraGlyph() { SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.ScaleTransform(Width / 24F, Height / 24F);
            using (var pen = new Pen(ForeColor, 1.5F))
            {
                e.Graphics.DrawRectangle(pen, 4, 2, 16, 20);
                for (int y = 7; y <= 17; y += 5) { e.Graphics.DrawLine(pen, 7, y, 8, y); e.Graphics.DrawLine(pen, 11, y, 17, y); }
            }
        }
    }

    public sealed class RestoreGlyph : Control
    {
        public int Kind { get; set; }
        public RestoreGlyph() { SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.ScaleTransform(Width / 24F, Height / 24F);
            using (var pen = new Pen(ForeColor, 1.5F) { LineJoin = LineJoin.Round })
            {
                if (Kind == 0)
                {
                    e.Graphics.DrawEllipse(pen, 4, 2, 16, 6);
                    e.Graphics.DrawLine(pen, 4, 5, 4, 19);
                    e.Graphics.DrawLine(pen, 20, 5, 20, 19);
                    e.Graphics.DrawArc(pen, 4, 9, 16, 6, 0, 180);
                    e.Graphics.DrawArc(pen, 4, 16, 16, 6, 0, 180);
                }
                else if (Kind == 1)
                {
                    e.Graphics.DrawLines(pen, new[] { new PointF(2, 19), new PointF(2, 4), new PointF(9, 4), new PointF(12, 7), new PointF(21, 7), new PointF(21, 10) });
                    e.Graphics.DrawPolygon(pen, new[] { new PointF(2, 20), new PointF(6, 10), new PointF(23, 10), new PointF(19, 20) });
                }
                else
                {
                    e.Graphics.DrawPolygon(pen, new[] { new PointF(12, 2), new PointF(23, 22), new PointF(1, 22) });
                    e.Graphics.DrawLine(pen, 12, 8, 12, 15);
                    e.Graphics.DrawEllipse(pen, 11.5F, 18, 1, 1);
                }
            }
        }
    }

    public sealed class OpcionIdiomaButton : UsuariosRoundedButton
    {
        public string Codigo { get; set; }
        public string NombreIdioma { get; set; }
        public bool Seleccionado { get; set; }
        public Color Accent { get; set; }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var tile = new Rectangle(16, 12, 54, 44);
            using (var brush = new SolidBrush(Color.FromArgb(35, Accent))) e.Graphics.FillRectangle(brush, tile);
            TextRenderer.DrawText(e.Graphics, Codigo, Font, tile, Accent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(e.Graphics, NombreIdioma, Font, new Rectangle(86, 0, Width - 140, Height), ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            if (Seleccionado)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var pen = new Pen(Accent, 2.5F)) e.Graphics.DrawLines(pen, new[] { new Point(Width - 40, 34), new Point(Width - 33, 41), new Point(Width - 21, 27) });
            }
        }
    }

    public sealed class IdiomaGlyph : Control
    {
        public IdiomaGlyph() { SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true); }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.ScaleTransform(Width / 24F, Height / 24F);
            using (var pen = new Pen(ForeColor, 1.3F))
            {
                e.Graphics.DrawEllipse(pen, 2, 2, 20, 20);
                e.Graphics.DrawEllipse(pen, 7, 2, 10, 20);
                e.Graphics.DrawLine(pen, 2, 12, 22, 12);
                e.Graphics.DrawLine(pen, 4, 6, 20, 6);
                e.Graphics.DrawLine(pen, 4, 18, 20, 18);
            }
        }
    }
}
