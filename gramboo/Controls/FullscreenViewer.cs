using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
namespace Gramboo.Controls
{
    public partial class FullscreenViewer : Form
    {
        PropertyList pl;
        public FullscreenViewer()
        {
            InitializeComponent();
        }
        public FullscreenViewer(Control controlname )
        {
            InitializeComponent();
            ControlName = controlname; 
        }

        public Control ControlName { get; set; }

        public void View()
        {
              pl = new PropertyList();

            pl.Anchor = ControlName.Anchor;
            pl.Dock = ControlName.Dock;
            pl.Location = ControlName.Location;
            pl.Parent = ControlName.Parent;
            pl.Size = ControlName.Size; 

            this.Controls.Add(ControlName);
            ControlName.Dock = DockStyle.Fill;
            label1.Text = "Press Esc Key To Exit";
            label1.ForeColor = Color.Black;
            label1.BringToFront();
            timer1.Interval = 100;
            timer1.Start();
            this.Show();
            this.BringToFront();
            this.Focus();
            this.Refresh();
        }
        public void Exit()
        {



            ControlName.Anchor = pl.Anchor;
            ControlName.Dock = pl.Dock;
            ControlName.Location = pl.Location;
            ControlName.Parent = pl.Parent;
            ControlName.Size = pl.Size; 
            this.Close();
            this.Dispose();
            

        }
        public static void CopyPropertyValues(object source, object destination)
        {

            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        try
                        {
                            destProperty.SetValue(destination, sourceProperty.GetValue(
                                source, new object[] { }), new object[] { });

                            Console.WriteLine ("Set " + sourceProperty.Name);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message +   " "+ sourceProperty.Name);
                        }
                        break;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int fadingSpeed = 3;
            label1.ForeColor = Color.FromArgb(label1.ForeColor.R + fadingSpeed, label1.ForeColor.G + fadingSpeed, label1.ForeColor.B + fadingSpeed);

            if (label1.ForeColor.R >= this.BackColor.R)
            {
                timer1.Stop();
                label1.ForeColor = this.BackColor;
                label1.Visible = false;
            }
        }

        private void FullscreenViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Escape)
            {
                this.Exit();
            }
        }

     
     
    }

    public class PropertyList
    { 
        public PropertyList()
        {
            
        }

        public AnchorStyles Anchor { get; set; }
        public Point Location { get; set; }
        public DockStyle Dock { get; set; }
        public Size Size { get; set; }
        public Control Parent { get; set; }
        
    }
}
