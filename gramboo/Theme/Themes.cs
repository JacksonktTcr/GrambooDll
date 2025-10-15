using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing; 
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;

using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Drawing.Text;
using Gramboo.Controls;


namespace ThemeCreator
{
     
    [Serializable]
   public  class Themes
    {
        

        [Category("Form")]
        [Description("The background color of the Form")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color BackColor { get; set; }
        [Category("Form")]
        [Description("The foreground color of this component,which is used to display text")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color ForeColor { get; set; }
        [Category("Form")]
        [Description("Indicate the appearance and behavior of the border and title bar of the form")]
        public FormBorderStyle FormBorderStyle { get; set; }
        private string FontName;
        [Browsable(true)]
        [Category("Form")]
        [TypeConverter(typeof(FontNameConverter))]
        [DisplayName("FontName")]
        [Description("Represent text font name")]
        public string TestValues
        {
            get { return FontName; }
            set { FontName = value; }
        }
        private int FontSize { get; set; }
        [Category("Form")]
        [TypeConverter(typeof(FontSizeConverter))]
        [DisplayName("FontSize")]
        [Description("Represent text size")]
        public int TestValues1
        {
            get { return FontSize; }
            set { FontSize = value; }
        }

        [Category("Form")]
        [Description("Represent text Style")]
        public FontStyle FontStyle { get; set; }




        [Category("DataGridView")]
        [Description("The background color of the DataGridView")]
        [XmlElement(Type = typeof(XmlColor))]
        [DisplayName("BackgroundColor ")]
        public Color DataGridViewBackgroundColor { get; set; }


        [Category("DataGridView")]
        [Description("The foreground color of this DataGridView,which is used to display text")]
        [XmlElement(Type = typeof(XmlColor))]
        [DisplayName("ForeColor ")]
        public Color DataGridViewForeColor { get; set; }

        [Category("DataGridView")]
        [Description("The border style for the DataGridView  ")]
        [DisplayName("BorderStyle")]
        public BorderStyle DataGridViewBorderStyle { get; set; }

        [Category("DataGridView")]
        [Description("The back color of the selected cell in the datagridview")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color CellSelectionBackColor { get; set; }
       

        [Category("DataGridView")]
        [Description("Represent cell text style  ")]
        public FontStyle CellFontStyle { get; set; }

        [Category("DataGridView")]
        [Description("Represent grid color  ")]
        [DisplayName("GridColor")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color DataGridViewGridColor { get; set; }

        private string CellFontName { get; set; }
        [Browsable(true)]
        [Category("DataGridView")]
        [TypeConverter(typeof(FontNameConverter))]
        [DisplayName("CellFontName")]
        [Description("Represent cell text font name")]
       
        public string fontname1
        {
            get { return CellFontName; }
            set { CellFontName = value; }
        }

        private int CellFontSize { get; set; }
        [Category("DataGridView")]
        [TypeConverter(typeof(FontSizeConverter))]
        [DisplayName("CellFontSize")]
        [Description("Represent text size")]
      
        public int FontSize2
        {
            get { return CellFontSize; }
            set { CellFontSize = value; }
        }

        [Category("TextBox/ComboBox")]
        [DisplayName("BorderStyle")]
        [Description("The border style for the component")]
        public BorderStyle ControlBorderStyle { get; set; }
     





        [Category("TextBox/ComboBox")]
        [XmlElement(Type = typeof(XmlColor))]
        [Description("Gets or sets Border color of the control when it focus ")]
        public Color ActiveBorderColor { get; set; }


        [Category("TextBox/ComboBox")]
        [XmlElement(Type = typeof(XmlColor))]
        [Description("Gets or sets background color of the control when it focus ")]
        public Color ActiveBackColor { get; set; }




        [Category("TextBox/ComboBox")]
        [XmlElement(Type = typeof(XmlColor))]
        [DisplayName("BorderColor")]
        [Description(" set  BorderColor color of the component ")]
        public Color NormalBorderColor { get; set; }

        //[Category("TextBox/ComboBox")]
        //[XmlElement(Type = typeof(XmlColor))]
        //public Color NormalBackColor { get; set; }


        [Category("TextBox/ComboBox")]
        [DisplayName("BackColor")]
        [Description("The background color of the component")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color ControlBackColor { get; set; }

        [Category("TextBox/ComboBox")]
        [DisplayName("ForeColor")]
        [Description("The foreground color of the component,which is used to display text")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color ControlForeColor { get; set; }


        [Category("Group Box")]
        [Description("The background color of the component")]
        [XmlElement(Type = typeof(XmlColor))]
        public Color GroupBoxBackColor { get; set; }

        [Category("Group Box")]
        [XmlElement(Type = typeof(XmlColor))]
        [Description("The foreground color of the component,which is used to display text")]
        public Color GroupBoxForeColor { get; set; }

        [Category("Panel")]
        [DisplayName("BackColor")]
        [XmlElement(Type = typeof(XmlColor))]
        [Description("The background color of the component")]
        public Color PanelBackColor { get; set; }

        [Category("Panel")]
        [DisplayName("ForeColor")]
        [XmlElement(Type = typeof(XmlColor))]
        [Description("The foreground color of the component,which is used to display text")]
        public Color PanelForeColor { get; set; }

        [Category("Panel")]
        [DisplayName("BorderStyle")]
        [Description("The border style for the component")]
        public BorderStyle PanelBorderStyle { get; set; }

        [Category("Advanced")]
        [DisplayName("DisableTheme")]
        [Description("Disable Theme Settings and use Default Settings")]
        public bool DisableTheme { get; set; }
     



        public Themes()
        
        {

           

            BackColor = Color.PowderBlue;
            ForeColor = Color.DarkBlue;
            FormBorderStyle = FormBorderStyle.Sizable;
            
            ActiveBackColor = Color.LightSteelBlue;
            ActiveBorderColor = Color.Red   ;
          //  NormalBackColor = ControlBackColor;
            NormalBorderColor = Color.Gray;
            FontName = "Tahoma";
            FontSize = 8;
            FontStyle = FontStyle.Regular;
            Font NewFont = new Font(FontName, FontSize, FontStyle);
           
            DataGridViewBackgroundColor = Color.Lavender;
            DataGridViewForeColor = Color.DarkBlue;
            CellSelectionBackColor = Color.AntiqueWhite;
         
            CellFontName = "Century";
            CellFontSize = 8;
            CellFontStyle = FontStyle.Regular;
          
            DataGridViewBorderStyle = BorderStyle.Fixed3D;
            DataGridViewGridColor = Color.LightSlateGray;

            ControlBorderStyle = BorderStyle.FixedSingle;
            ControlForeColor = Color.DarkBlue;
            ControlBackColor = Color.Lavender;

            GroupBoxBackColor = SystemColors.Control;
            GroupBoxForeColor = Color.LightSkyBlue;

            PanelBackColor = Color.GreenYellow;
            PanelForeColor = Color.Red;
            PanelBorderStyle = BorderStyle.FixedSingle;
        }



        public void Save()
        {


            var path = System.Reflection.Assembly.GetEntryAssembly().Location;
            var name = System.IO.Path.GetFileName(path);

            XmlSerializer serializer = new XmlSerializer(typeof(Themes));
            TextWriter textWriter = new StreamWriter(Path.GetTempPath() + "Theme.xml");
            serializer.Serialize(textWriter, this);
            textWriter.Close();
        }




         public static Themes Open()
         {

             var path = System.Reflection.Assembly.GetEntryAssembly().Location;
             var name = System.IO.Path.GetFileName(path);

             if (!File.Exists(Path.GetTempPath() + "Theme.xml"))
                 return null;
             XmlSerializer deserializer = new XmlSerializer(typeof(Themes));
             TextReader textReader = new StreamReader(Path.GetTempPath() + "Theme.xml");
             Themes Theme;
             Theme = (Themes)deserializer.Deserialize(textReader);
             textReader.Close();

             return Theme;

         }



        public void ApplyTheme(Form frm)
        {
            if (DisableTheme)
                return;
            frm.BackColor = BackColor;
            frm.ForeColor = ForeColor;
            FormBorderStyle = FormBorderStyle;

            try
            {
                Font NewFont = new Font(FontName, FontSize, FontStyle);


            }
            catch (Exception ex)
            {
                FontName = "Tahoma";
                Font NewFont = new Font(FontName, FontSize, FontStyle);
            }
            ApplyThemeToChildren(frm);

            

        }

        private void ApplyThemeToChildren(Control parent)
        {
            foreach (Control Ctrl in parent.Controls)
            {
                if (Ctrl.HasChildren && Ctrl.GetType() != typeof(GrbDataGridView) && Ctrl.GetType() != typeof(DataGridView))
                {
                    ApplyThemeToChildren(Ctrl);
                }



                else
                {

                    if (Ctrl.GetType() == typeof(TextBox) || Ctrl.GetType().BaseType == typeof(TextBox))
                    {
                       
                        
                        
                       ((TextBox)Ctrl).BorderStyle = ControlBorderStyle;
                        ((TextBox)Ctrl).Font = new Font(FontName, FontSize, FontStyle);
                        ((TextBox)Ctrl).BackColor = ControlBackColor;
                        ((TextBox)Ctrl).ForeColor = ControlForeColor;
                    }
                    else if (Ctrl.GetType() == typeof(DataGridView) || Ctrl.GetType().BaseType == typeof(DataGridView))
                    {
                        ((DataGridView)Ctrl).BackgroundColor = DataGridViewBackgroundColor;
                        ((DataGridView)Ctrl).ForeColor = DataGridViewForeColor;
                        ((DataGridView)Ctrl).DefaultCellStyle.SelectionBackColor = CellSelectionBackColor;

                        try
                        {

                            ((DataGridView)Ctrl).Font = new Font(CellFontName, CellFontSize, CellFontStyle);
                        }
                        catch (Exception ex)
                        {
                            CellFontName = "Century";
                            ((DataGridView)Ctrl).Font = new Font(CellFontName, CellFontSize, CellFontStyle);
 
                        }
                   
                        ((DataGridView)Ctrl).BorderStyle = DataGridViewBorderStyle;
                        ((DataGridView)Ctrl).GridColor = DataGridViewGridColor;
                    }
                    else if (Ctrl.GetType() == typeof(GroupBox) || Ctrl.GetType().BaseType == typeof(GroupBox))
                    {
                        ((GroupBox)Ctrl).BackColor = GroupBoxBackColor;
                        ((GroupBox)Ctrl).ForeColor = GroupBoxForeColor;
                     



                    }

                    else if (Ctrl.GetType() == typeof(ComboBox) || Ctrl.GetType().BaseType == typeof(ComboBox))
                    {
                       // ((ComboBox)Ctrl).BorderStyle = ControlBorderStyle;
                        ((ComboBox)Ctrl).Font = new Font(FontName, FontSize, FontStyle);
                        ((ComboBox)Ctrl).BackColor = ControlBackColor;
                        ((ComboBox)Ctrl).ForeColor = ControlForeColor;
                        
                   
                         

                    }
                    else if (Ctrl.GetType() == typeof(Panel) || Ctrl.GetType().BaseType == typeof(Panel))
                    {

                        ((Panel)Ctrl).BackColor = PanelBackColor;
                        ((Panel)Ctrl).ForeColor = PanelForeColor;
                        ((Panel)Ctrl).BorderStyle = PanelBorderStyle;
                    }



                    else
                    {
                        Ctrl.Font = new Font(FontName, FontSize, FontStyle);
                        //Ctrl.BackColor = CtrlBackColor;

                    }

                    if (Ctrl.GetType().GetProperty("ActiveBackColor") != null)
                    {
                        Ctrl.GetType().GetProperty("ActiveBackColor").SetValue(Ctrl, ActiveBackColor,null );
                    }
                    if (Ctrl.GetType().GetProperty("NormalBackColor") != null)
                    {
                        Ctrl.GetType().GetProperty("NormalBackColor").SetValue(Ctrl, ControlBackColor, null);
                    }




                }
            }

        }


        
    }
   
  


   
    
}
