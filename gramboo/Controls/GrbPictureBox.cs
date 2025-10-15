using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
 
using Gramboo.Controls.PopupControl;
namespace Gramboo.Controls
{ 
 
    [Category("Gramboo Components")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(PictureBox))]
    public class GrbPictureBox : PictureBox, IDataBindinig
    {
        ToolTip tt = new ToolTip();
        PictureBox clearimg = new PictureBox();
        PictureBox tools = new PictureBox();
        Popup  complex;
        ImageProcessor complexPopup=new ImageProcessor();
        private bool _hasImage;

        public GrbPictureBox()
        {
            InitializeComponent();
            SaveToFieStream = false;

            clearimg.Name = "clearimg";
            clearimg.Text = "Clear";
            clearimg.Visible = false;
            clearimg.SizeMode = PictureBoxSizeMode.StretchImage;
            clearimg.Size = new System.Drawing.Size(16, 16);
            clearimg.Image = Gramboo.Properties.Resources.DeleteRed;



            tools.Name = "tools";
            tools.Text = "Tools";
            tools.Visible = false;
            tools.SizeMode = PictureBoxSizeMode.StretchImage;
            tools.Size = new System.Drawing.Size(16, 16);
            tools.Image = Gramboo.Properties.Resources.Setting ;
           
           

            this.Image = Gramboo.Properties.Resources.ImgNotFound;
            HasImage = false;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            DataField = "None";
            TableName = "";
            AcceptBlankValue = true;
            BindingProperty = "BinaryValue";
            Alias = "None";
            Browsable = false;
            ShowComplusoryMark = true;
            BindToDataGridview = false;

            clearimg.Click += delegate(object sender, EventArgs e)
            {
                this.Image = Gramboo.Properties.Resources.ImgNotFound;

                HasImage = false;
                clearimg.Visible = false;
                tools.Visible = false;
            };

            tools.Click += delegate(object sender, EventArgs e)
           {
               if (HasImage )
               {
                   try
                   {
                       complex = new Popup(complexPopup = new ImageProcessor());
                       ((ImageProcessor)complex.Content).ImageViewer.Image = (Bitmap)this.Image;
                       complex.Show(this);
                   }
                   catch (Exception ex)
                   {
                       
                   }

               }
           };

            complex = new Popup(complexPopup = new ImageProcessor());
            complex.Resizable = true;

        }

        #region Designer Generated Code
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GrbPictureBox
            // 
            this.Name = "GrbPictureBox";
            this.Size = new System.Drawing.Size(172, 28);
            this.ResumeLayout(false);

        }

        #endregion
        #endregion

        public new  Image Image
        {
            get
            {
                return base.Image;
            }

            set
            {

                base.Image = value;
                if (value != null)
                {
                   
                    HasImage = true;
                }
            }
        }

        private bool HasImage
        {
            get
            {
                return _hasImage ;

            }
            set
            {


                _hasImage = value;
                if (this.DesignMode == false && this.Parent != null)
                AddControls(value);
            }
        }


        private  Byte[] ImageToByte(Image ImageFile)
        {
            try
            {
                //----- Converts Image to Bye Array
                MemoryStream ms = new MemoryStream();
                ImageFile.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private Image ByteToImage(byte[] byteArrayIn)
        {
            try
            {
                //----- Converts Byte Array into image
                MemoryStream ms = new MemoryStream(byteArrayIn);
                Image returnImage = Image.FromStream(ms, true, true);
                return returnImage;


              


            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
// private static byte[] GetBytes(string str)
//{
//    try
//    {
//        //byte[] bytes = new byte[str.Length * sizeof(char)];
//        //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
//        //return Convert.to str ;
//    }
//    catch (Exception ex)
//    {
//        return new byte[0];
//    }

// }
// static string GetString(byte[] bytes)
// {
//     try
//     {

//         //char[] chars = new char[bytes.Length / sizeof(char)];
//         //System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
//         return bytes.ToString();
//     }
//     catch (Exception ex)
//     {
//         return "0x";
//     }
// }
        /// <summary>
        /// Gets or Sets Binary  Value of the image
        /// </summary>
        [Browsable(false)]
        [Description("Gets or Sets Binary  Value of the image")]
        [DefaultValue(null  )]
        public byte[]   BinaryValue 
        {
            get
            {
                //if (this.Image == null || Gramboo.Properties.Resources.ImgNotFound== this.Image )

                //    return GetString(GetBytes("0x"));
                //else
                    //return GetString( ImageToByte(this.Image)) ;
                if (HasImage )
                {
                    return ImageToByte(this.Image);
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value == null )
                {
                    this.Image = Gramboo.Properties.Resources.ImgNotFound;

                    HasImage = false;
                }
                else if (ByteToImage(  value) == null)
                {
                    this.Image = Gramboo.Properties.Resources.ImgNotFound;
                    HasImage = false;
                }
                else
                {
                    this.Image = ByteToImage(value);
                    HasImage = true;
                }
            }
        }
        /// <summary>
        /// Gets or Sets  Image can be added by the user
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Gets or Sets  Image can be added by the user")]
        public bool Browsable { get; set; }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!Browsable)
                return;

            OpenFileDialog opdg = new OpenFileDialog();

            opdg.Filter = "Bitmap Files (*.bmp)|*.bmp|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files |*.*";
            opdg.CheckPathExists = true;
            opdg.AddExtension = true;
            opdg.CheckFileExists = true;
            opdg.Multiselect = false;
            opdg.Title = "Select Picture";
            opdg.ValidateNames = true;

            if (opdg.ShowDialog() == DialogResult.OK)
            {
                this.Image = Image.FromFile(opdg.FileName);
                HasImage = true ;
             }



        }

        private void AddControls(bool flag)
        {
 
            clearimg.Visible = flag;
            tools.Visible = flag;
            if (flag)
            {
                clearimg.Location = new Point(this.Width - 16, this.Height - 16);
                clearimg.SizeMode = PictureBoxSizeMode.StretchImage;
                clearimg.BackColor = Color.Transparent;

                clearimg.Size = new Size(16, 16);
                this.Controls.Add(clearimg);
                clearimg.BringToFront();

                tools.Location = new Point(this.Width - 32, this.Height - 16);
                tools.SizeMode = PictureBoxSizeMode.StretchImage;
                tools.BackColor = Color.Transparent;

                tools.Size = new Size(16, 16);
                this.Controls.Add(tools);
                tools.BringToFront();
            }

           
           
        }
        /// <summary>
        /// Gets or Sets whether the Image should save to Filestream
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets whether the Image should save to Filestream")]
        [DefaultValue(false )]
        public bool SaveToFieStream { get; set; }

        /// <summary>
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Database field assosiated with the control")]
        [DefaultValue("None")]
        public string DataField { get; set; }

        /// <summary>
        /// Gets or Sets Alias for Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Alias for Database field assosiated with the control")]
        [DefaultValue("None")]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or Sets Table field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table field assosiated with the control")]
        [DefaultValue("")]
        public string TableName { get; set; }


        /// <summary>
        /// Gets or Sets to which property data should be binded
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets to which property data should be binded")]
        [DefaultValue("BinaryValue")]
        public string BindingProperty { get; set; }

        /// <summary>
        /// Gets or Sets whether control accepts blank values
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("Gets or Sets whether control accepts blank values")]
        public bool AcceptBlankValue { get; set; }


        /// <summary>
        /// Gets or Sets  Whether show a Red mark near control if it is a compulsory field
        /// </summary>
        [Browsable(true)]
        [Description("  Gets or Sets  Whether show a Red mark near control if it is a compulsory field")]
        [DefaultValue(true)]
        public bool ShowComplusoryMark { get; set; }
        /// <summary>
        /// Gets or Sets  Whether Control Bind to Datagridview
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets  Whether Control Bind to Datagridview")]
        [DefaultValue(false)]
        public bool BindToDataGridview { get; set; }
        /// <summary>
        /// Displays baloon style tooltip message 
        /// </summary>
        /// <param name="Message">Message to be displayed</param>
        /// <param name="Title">Title of the message</param>
        /// <param name="Icon">Icon assosiated with the control</param>
        public void ShowMessage(string Message, string Title = "Information", ToolTipIcon Icon = ToolTipIcon.Info)
        {
            if (DesignMode || !Visible)
                return;
            //GrambooToolTip.Show(this, Message, Title, GrambooToolTip.BalloonIcon.ShowInformation);
            tt.ToolTipTitle = Title;
            tt.ToolTipIcon = Icon;
            tt.ShowAlways = true;
            tt.IsBalloon = true;
            tt.SetToolTip(this, Message);
            tt.Show(Message, this, 5000);
        }

        public bool IsBlank()
        {
            if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null) == null)
            {
                ShowMessage("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null).ToString().Trim() == "")
            {
                ShowMessage("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
