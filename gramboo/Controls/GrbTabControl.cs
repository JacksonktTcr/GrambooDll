using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeoTabControlLibrary;

namespace Gramboo.Controls
{
    public partial class GrbTabControl : NeoTabControlLibrary.NeoTabWindow
    {
        public enum RenderingStyles
        {
            Avast=0,
            CCleaner=1,
            FaceLinkBar=2,
            MarginBlue=3,
            MenuBar=4,
            NeoTabStrip=5,
            OrderedList=6,
            SizeDotNet=7,
            Telerik=8,
            VS2005=9,
            VS2008=10,
            VS2008Caption=11,
            VS2010=12,
            VS2012=13,
            WebSlider=14
        }

        public GrbTabControl()
        {
            InitializeComponent();
            RenderingStyle = RenderingStyles.Telerik;

        }

        [DefaultValue(8)]
        public RenderingStyles RenderingStyle
        {
            get
            {
                return RenderingStyle;
            }
            set
            {
                RenderingStyle = value;
                 //OnRenderStyleChanged();
            }

        }
        
        public virtual void  OnRenderStyleChanged()
        {
            try
            {
                switch (RenderingStyle)
                {
                    case RenderingStyles.Avast:
                        this.Renderer = new NeoTabControlLibrary.Renderer.Avast.AvastRendererVS3();
                        break;
                    case RenderingStyles.CCleaner:
                        this.Renderer = new NeoTabControlLibrary.Renderer.CCleaner.CCleanerRendererVS4();
                        break;
                    case RenderingStyles.FaceLinkBar:
                        this.Renderer = new NeoTabControlLibrary.Renderer.FaceLinkBar.FaceLinkBarRendererVS2();
                        break;
                    case RenderingStyles.MarginBlue:
                        this.Renderer = new NeoTabControlLibrary.Renderer.MarginBlue.MarginBlueRendererVS2();
                        break;
                    case RenderingStyles.MenuBar:
                        this.Renderer = new NeoTabControlLibrary.Renderer.MenuBar.MenuBarRendererVS2();
                        break;
                    case RenderingStyles.NeoTabStrip:
                        this.Renderer = new NeoTabControlLibrary.Renderer.NeoTabStrip.NeoTabStripRendererVS2();
                        break;
                    case RenderingStyles.OrderedList:
                        this.Renderer = new NeoTabControlLibrary.Renderer.OrderedList.OrderedListRenderer();
                        break;
                    case RenderingStyles.SizeDotNet:
                        this.Renderer = new NeoTabControlLibrary.Renderer.SizDotNET.SizDotNETRendererVS2();
                        break;
                    case RenderingStyles.Telerik:
                        this.Renderer = new NeoTabControlLibrary.Renderer.Telerik.TelerikRenderer();
                        break;
                    case RenderingStyles.VS2005:
                        this.Renderer = new NeoTabControlLibrary.Renderer.VS2005.VS2005LikeRenderer();
                        break;
                    case RenderingStyles.VS2008:
                        this.Renderer = new NeoTabControlLibrary.Renderer.VS2008.VS2008LikeRenderer();
                        break;

                    case RenderingStyles.VS2008Caption:
                        this.Renderer = new NeoTabControlLibrary.Renderer.VS2008Caption.VS2008CaptionRendererVS2();
                        break;

                    case RenderingStyles.VS2010:
                        this.Renderer = new NeoTabControlLibrary.Renderer.VS2010.VS2010LikeRenderer();
                        break;

                    case RenderingStyles.VS2012:
                        this.Renderer = new NeoTabControlLibrary.Renderer.VS2012.VS2012LikeRenderer();
                        break;

                    case RenderingStyles.WebSlider:
                        this.Renderer = new NeoTabControlLibrary.Renderer.WebSliders.WebSliderRendererVS3();
                        break;

                    default:

                        //this.Renderer = new DefaultRenderer();
                        break;

                }

                //this.BackColor = this.BackColor;
                //string typeName = this.Renderer.GetType().Name;
                //if (typeName.StartsWith("CCleanerRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("AvastRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("NeoTabStripRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("MarginGrayRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("VS2008LikeRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("VS2010LikeRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("VS2012LikeRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.White;
                //}
                //else if (typeName.StartsWith("WebSliderRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.Transparent;
                //}
                //else if (typeName.Equals("TelerikRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.Transparent;
                //}
                //else if (typeName.Equals("DefaultRenderer"))
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = Color.Transparent;
                //}
                //else
                //{
                //    foreach (NeoTabControlLibrary.NeoTabPage tp in this.Controls)
                //        tp.BackColor = this.BackColor;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
