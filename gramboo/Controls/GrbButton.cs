using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Gramboo.Controls
{
     [Category("Gramboo Components")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public sealed  partial class GrbButton : Button
    {
        public GrbButton()
        {
            InitializeComponent();
            NormalImage = Gramboo.Properties.Resources.Normal;
            OnFocusImage = Gramboo.Properties.Resources.Focus;
            SelectedImage = Gramboo.Properties.Resources.Selected;
            MouseDownImage = Gramboo.Properties.Resources.MouseDown;
            SetBackground(NormalImage);
        }

        /// <summary>
        /// Sets Normal Background Image 
        /// </summary>
        [Browsable(true)]
        public Image NormalImage { get; set; }

        /// <summary>
        /// Sets Background Image When it gets Focus
        /// </summary>
        [Browsable(true)]
        public Image OnFocusImage { get; set; }
        /// <summary>
        /// Sets Background Image when it is selected
        /// </summary>
        [Browsable(true)]
        public Image SelectedImage { get; set; }
        /// <summary>
        /// Sets Background Image when mousedown even occurs
        /// </summary>
        [Browsable(true)]
        public Image MouseDownImage { get; set; }

        


     
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SetBackground(OnFocusImage);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            SetBackground(MouseDownImage);

        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            SetBackground(SelectedImage);

        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            SetBackground(SelectedImage);

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetBackground(NormalImage);

        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SetBackground(NormalImage);
        }

        
        private void SetBackground(Image img)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = img;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 2;
            Color c = new Color();
            c = Color.FromArgb(c.R+20, c.G+20, c.B + 20);
            c = ((Bitmap)this.BackgroundImage).GetPixel(10, 10);
            this.FlatAppearance.BorderColor = c;
        }

    }
}
