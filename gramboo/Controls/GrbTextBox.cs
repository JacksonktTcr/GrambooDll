using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using Gramboo.Controls.Enumeration;
using Gramboo.Database;

namespace Gramboo.Controls
{


     [Category("Gramboo Components")]
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(TextBox))]
	[Designer(typeof(GrbTextBoxControlDesigner))]

	public sealed  partial class GrbTextBox : TextBox, IDataBindinig, IVisualStyles
	{

		#region Declaration
		 private Color _BorderColor;
		private string _CueBanner = "";
		ToolTip tt = new ToolTip();
		private Mask m_mask;
		private string _format;		 
		private DataController dc = new DataController();
		private string _TableName="";


		public enum Formats
		{
			None = 0,
			Integer = 1

		};

		public enum Mask
		{
			[ValidationMessage("Enter Valid Input ")]
			[StringValue(@"^[a-z]*[A-Z]*\d*$")]
			None = 0,
			[ValidationMessage("Enter decimal values only ")]
            [StringValue(@"^(\+|-)?[0-9]\d*(.\d+)?$")]
			Decimal = 1,
			[ValidationMessage("Enter non-decimal number values only ")]
			[StringValue(@"^(\+|-)?\d+$")]
			Digit = 2,
			[ValidationMessage("Enter date/time  values only ")]
			[StringValue(@"^(3[0-1]|2[0-9]|1[0-9]|0[1-9])[\s{1}|\/|-](Jan|JAN|Feb|FEB|Mar|MAR|Apr|APR|May|MAY|Jun|JUN|Jul|JUL|Aug|AUG|Sep|SEP|Oct|OCT|Nov|NOV|Dec|DEC)[\s{1}|\/|-]\d{4}|([1-9]|1[0-2]|0[1-9]){1}(:[0-5][0-9][aApP][mM]){1}$")]
			DateTime = 3,
			[ValidationMessage("Enter valid pincode only ")]
			[StringValue(@"^[0-9]{1,6}$")]
			Pincode = 4,
			[ValidationMessage("Enter valid mobile number only ")]
			[StringValue(@"^\+[1-9]{1}[0-9]{10}$")]
			MobileNumber = 5,
			[ValidationMessage("Enter Land line number only ")]
			[StringValue(@"^[0-9]\d{2,4}-\d{6,8}$")]
			LandlineNumber = 6,
			[ValidationMessage("Enter valid email address ")]
			[StringValue(@"^[\w\.=-]+@[\w\.-]+\.[\w]{2,3}$")]
			Email = 7
		};

		public GrbTextBox()
		{
			InitializeComponent();
			base.BorderStyle = BorderStyle.FixedSingle;
			this.Size = new System.Drawing.Size(100, this.Font.Height + 7);
			ActiveBackColor = Color.White;
			NormalBackColor = Color.White;
			ActiveBorderColor = Color.FromArgb(77, 144, 254);
			NormalBorderColor = Color.Gray;;
			_BorderColor = NormalBorderColor;
			AcceptBlankValue = true;
			_format = "";
			TableName = "";
			DataField = "None";
            BindingProperty = "Text";
            Alias = "None";
            IsIDField = false;
            ShowComplusoryMark = true;
            BindToDataGridview = false;
            AcceptZero = true;
            EnlargeFont = true;

		}


		#endregion



		#region Properties

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
		//    //When first loaded set property with the first item in the rule list.
		//    get	{
				
		//        string C=""  ;
		//            if (_datafield != null)
		//            {
		//                C = _datafield;
		//            }
		//            else
		//            {
		//                if (HE_GlobalVars._ListofCols.Length > 0)
		//                {
		//                    //Sort the list before displaying it
		//                    Array.Sort(HE_GlobalVars._ListofCols);

		//                    C = HE_GlobalVars._ListofCols[0];
		//                }
		//            }

		//            return C;
		//        }
		//    set
		//    {
		//        _datafield = value;        
		//    }

		//}

		/// <summary>
		/// Gets or Sets Table field assosiated with the control
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets Table field assosiated with the control")]
		[DefaultValue("")]
		public string  TableName 
		{

			get
			{
				return _TableName;
			}
			set
			{
				_TableName = value;
				//UpdateListofColumns();
			}
		}

        /// <summary>
        /// Gets or Sets Database field assosiated with the control is an Id field
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Database field assosiated with the control is an Id field")]
        [DefaultValue(false)]
        public bool IsIDField { get; set; }

        /// <summary>
        /// Gets or Sets Whether Enlarge Font On Focus
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Whether Enlarge Font On Focus")]
        [DefaultValue(true)]
        public bool EnlargeFont { get; set; }
        /// <summary>
        /// Gets or Sets to which property data should be binded
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets to which property data should be binded")]
        [DefaultValue("Text")]
       public  string BindingProperty { get; set; }

		/// <summary>
		/// Gets or Sets whether control accepts blank values
		/// </summary>
		[DefaultValue(true)]
		[Browsable(true)]
		[Description("Gets or Sets whether control accepts blank values")]
		public bool AcceptBlankValue { get; set; }

        /// <summary>
        /// Gets or Sets whether control accepts zero
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("Gets or Sets whether control accepts zero")]
        public bool AcceptZero { get; set; }


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
       public  bool BindToDataGridview { get; set; }

		/// <summary>
		/// Gets or Sets Border color of the control when it is has focus 
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets Border color of the control when it is has focus ")]
		[DefaultValue(typeof(Color), "77, 144, 254")]
		public Color ActiveBorderColor { get; set; }
		/// <summary>
		/// Gets or Sets background color of the control when it is has focus 
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets background color of the control when it is has focus ")]
		[DefaultValue(typeof(Color), "White")]
		public Color ActiveBackColor { get; set; }
		/// <summary>
		/// Gets or Sets Border color of the control when it is has no focus 
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets Border color of the control when it is has no focus ")]
		[DefaultValue(typeof(Color), "Black")]
		public Color NormalBorderColor { get; set; }
		/// <summary>
		/// Gets or Sets background color of the control when it is has no focus 
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets background color of the control when it is has no focus ")]
		[DefaultValue(typeof(Color), "White")]
		public Color NormalBackColor { get; set; }

		/// <summary>
		/// Gets or Sets water mark text to the control when text is empty
		/// </summary>
		[Browsable(true)]
		[Description("Gets or Sets water mark text to the control when text is empty ")]
		[DefaultValue("")]
		public string CueBanner
		{
			get
			{
				return _CueBanner;
			}

			set
			{
				_CueBanner = value;
				if (_CueBanner != "" && !DesignMode )
				{
					CueProvider.SetCue(this, _CueBanner);
				}
			}
		}

		/// <summary>
		/// Gests or sets input mask for the control
		/// </summary>
		[Browsable(true )]
		[Description("Gests or sets input mask for the control")]
		[DefaultValue(Mask.None )]
		public Mask InputMask
		{
			get { return m_mask; }
			set
			{
				m_mask = value;
				if (ValidateMask() == false)
				{
					base.Text = "";
				}
			}
		}


		/// <summary>
		/// Gets or sets string format for the control
		/// </summary>
		[Browsable(false )]
		[Description("Gets or sets string format for the control")]
		[DefaultValue("")]
		public string Format
		{
			get
			{
				return _format;
			}
			set
			{
				_format = value;
				if (_format.Trim() != "")
				{
					Text = String.Format (  _format,Text  );
				}
			}
		}

		[Browsable(false)]
		[DefaultValue(BorderStyle.FixedSingle)]
		public new BorderStyle BorderStyle { get; set; }


        /// <summary>
        /// Gets or sets check whether the value is already exists in a collection
        /// </summary>
        [Browsable(true )]
        [Description(" Gets or sets check whether the value is already exists in a collection")]
        [DefaultValue(false)]
        public bool CheckDuplicates { get; set; }

		#endregion Properties


		#region Methods
 
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			this.BackColor = ActiveBackColor;
			_BorderColor = ActiveBorderColor;
            if (this.EnlargeFont == true)
            {
                this.Font = new Font(this.Font.FontFamily, this.Font.Size + 2,this.Font.Style);
            }
			this.Select(this.Text.Length, 0);
			
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

		
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			switch (m.Msg)
			{
				case 15: // this is the WM_PAINT message  
					// invalidate the TextBox so that it gets refreshed properly  
					this.Invalidate();
					// call the default win32 Paint method for the TextBox first  
					base.WndProc(ref m);
					// now use our code to draw extra stuff over the TextBox  
					Graphics g = Graphics.FromHwnd(this.Handle);
					Brush b = new SolidBrush(_BorderColor);
					Pen p = new Pen(b);
					g.DrawLine(p, new Point(0, 0), new Point(this.DisplayRectangle.Width, 0));
					g.DrawLine(p, new Point(0, this.DisplayRectangle.Height - 1), new Point(this.DisplayRectangle.Width, this.DisplayRectangle.Height - 1));
					g.DrawLine(p, new Point(0, 0), new Point(0, this.DisplayRectangle.Height));
					g.DrawLine(p, new Point(this.DisplayRectangle.Width - 1, 0), new Point(this.DisplayRectangle.Width - 1, this.DisplayRectangle.Height));

					if (!DesignMode && !this.AcceptBlankValue && ShowComplusoryMark)
					{
						 g = Graphics.FromHwnd(this.Parent.Handle );
						 b = new SolidBrush(Color .Red);


						 g.DrawString("*", new Font("Arial", 14, FontStyle.Regular), b, new PointF(this.Location.X + this.Width + 2, this.Location.Y ));
					}
					g.Dispose();
					b.Dispose();
					p.Dispose();
					break;
				default:
					base.WndProc(ref m);
					break;
			}
		}
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            this.SelectAll();
        }
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);



            //if (this.AcceptBlankValue == false && this.Text.Trim() == "")
            //{
            //    ShowBaloon("Blank values not allowed.","Warning",ToolTipIcon.Warning );
            //}
            if (!ValidateMask())
            {
                this.Focus();
                //ShowMessage(""
            }

			_BorderColor = NormalBorderColor;
			this.BackColor = NormalBackColor;
            if (this.EnlargeFont == true)
            {
                if (this.Font.Size > 6)
                {
                    this.Font = new Font(this.Font.FontFamily, this.Font.Size - 2, this.Font.Style);
                }
            }
			//Text = System.String.Format(_format,Text);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);

            if (m_mask == Mask.Decimal)
            {
                // allows 0-9, backspace, and decimal
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46 && e.KeyChar != 45))
                {
                    e.Handled = true;
                    return;
                }
                // checks to make sure only 1 decimal is allowed
                if (e.KeyChar == 46)
                {
                    if (this.Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                    if (this.Text.Trim().Length == 0)
                    {
                        e.Handled = true;
                        this.Text = "0.";
                        this.SelectionLength = 0;
                        this.SelectionStart = this.Text.Length ;
                    }
                }

                if (e.KeyChar == 45)
                {
                    if (this.Text.IndexOf(e.KeyChar) != -1)
                        e.Handled = true;
                }

            }
            if (m_mask == Mask.Digit || m_mask==Mask.Pincode )
            {
                // allows 0-9, backspace, and decimal
                if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 ))
                {
                    e.Handled = true;
                    return;
                }


            }

		}
	  

		public override string Text
		{
			get
			{
                if (InputMask == Mask.Digit || InputMask == Mask.Decimal)
                {
                    double i;

                    if (double.TryParse(base.Text, out i))
                    {
                        return i.ToString();
                    }
                    else
                    {
                        return "0";
                    }

                   


                }
                else
                {
                    return base.Text;
                }
            }
			set
            {
            //    base.Text = String.Format(_format, value);
                    if (ValidateMask() == false)
                {
                    base.Text = "";
                }
                else
                {
                    base.Text = value;
                }
			}
		}

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
		private void ShowBaloon(string Message, string Title = "Information", ToolTipIcon Icon = ToolTipIcon.Info )
		{
			if (DesignMode || !Visible ) 
				return;
			//GrambooToolTip.Show(this, Message, Title, GrambooToolTip.BalloonIcon.ShowInformation);
			tt.ToolTipTitle = Title;
			tt.ToolTipIcon = Icon;
			tt.ShowAlways = true;
			tt.IsBalloon = true;
			tt.SetToolTip(this, Message);
			tt.Show(Message, this, 5000);
		}


		/// <summary>
		/// Checks the regular expression
		/// </summary>
		/// <returns>bool</returns>
		private bool ValidateMask()
		{


			if (this.Text.Trim() == "")
				return true ;

            if (m_mask == Mask.None)
                return true;

			string TextToValidate;
			Regex expression;
			try
			{
				TextToValidate = this.Text;
				expression = new Regex(StringEnum.GetStringValue(m_mask) );
			}
			catch
			{
				return false;
			}
			// test text with expression
			if (expression.IsMatch(TextToValidate))
			{
				return true;
			}
			else
			{
				ShowBaloon(StringEnum.GetValidationMessage(m_mask), "Warning", ToolTipIcon.Warning)  ;
				  // no match
				return false;
			}

		}


		//private void UpdateListofColumns()
		//{
		//    if ( TableName.Trim().Length == 0)
		//        return;


		//    DataController dc = new DataController();
		//    DataTable dt = new DataTable();
		//    dt = dc.GetTableColumns( TableName,"dbo",  @"Data Source=.\JACKSON;Initial Catalog=GRB;Persist Security Info=True;User ID=sa;Password=P@SSW0RD;Integrated Security=True" );
		//    if (dt != null)
		//    {
		//        int _NumofRules = dt.Rows.Count;
		//        HE_GlobalVars._ListofCols = new string[_NumofRules];
		//        for (int i = 0; i <= _NumofRules - 1; i++)
		//        {
		//            HE_GlobalVars._ListofCols[i] = dt.Rows[i]["Column_Name"].ToString();
		//        }
		//    }
		//}
        protected override void OnTextChanged(EventArgs e)
        {
            
          


            if (this.InputMask == Mask.Decimal || this.InputMask == Mask.Digit)
            {
                if (this.Text.Trim().Length > 0)
                {
                    double i;

                    if (double.TryParse(this.Text, out i) == false)
                    {
                        return;
                    }
                }
            }

              base.OnTextChanged(e);
            //if (ValidateMask() == false)
            //{
            //     Text = "";
            //     this.ShowMessage("Invalid Characters ");
            //}
           
        }
		#endregion

	}

	#region Smart Tag Class

	[System.Security.Permissions.PermissionSet
	(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	public class GrbTextBoxControlDesigner : System.Windows.Forms.Design.ControlDesigner
	{
		private DesignerActionListCollection _actionLists;

		// Use pull model to populate smart tag menu.
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (null == _actionLists)
				{
					_actionLists = new DesignerActionListCollection();
					_actionLists.Add(new GrbTextBoxActionList(this.Component));
				}
				return _actionLists;
			}
		}
	}

	public class GrbTextBoxActionList : System.ComponentModel.Design.DesignerActionList
	{
		private GrbTextBox GrTextCtrl;

		private DesignerActionUIService designerActionUISvc = null;

		//The constructor associates the control with the smart tag list.
		public GrbTextBoxActionList(IComponent component)
			: base(component)
		{
			this.GrTextCtrl = component as GrbTextBox;

			// Cache a reference to DesignerActionUIService, so the DesigneractionList can be refreshed.
			this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
		}

		// Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
		private PropertyDescriptor GetPropertyByName(String propName)
		{
			PropertyDescriptor prop;
			prop = TypeDescriptor.GetProperties(GrTextCtrl)[propName];
			if (null == prop)
				throw new ArgumentException("Matching GrbTextBox property not found!", propName);
			else
				return prop;
		}

		// Properties that are targets of DesignerActionPropertyItem entries.
		public Color ActiveBorderColor
		{
			get
			{
				return GrTextCtrl.ActiveBorderColor;
			}
			set
			{
				GetPropertyByName("ActiveBorderColor").SetValue(GrTextCtrl, value);
			}
		}

		public Color ForeColor
		{
			get
			{
				return GrTextCtrl.ForeColor;
			}
			set
			{
				GetPropertyByName("ForeColor").SetValue(GrTextCtrl, value);
			}
		}
		public bool Multiline
		{
			get
			{
				return GrTextCtrl.Multiline;
			}
			set
			{
				GetPropertyByName("Multiline").SetValue(GrTextCtrl, value);
			}
		}

		// Implementation of this abstract method creates smart tag  items, associates their targets, and collects into list.
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();

			//Define static section header entries.
			items.Add(new DesignerActionHeaderItem("Appearance"));

			items.Add(new DesignerActionPropertyItem("ActiveBorderColor",
								 "Active BorderColor", "Appearance",
								 "Selects the  BorderColor when control gets focus."));
			items.Add(new DesignerActionPropertyItem("ForeColor",
								 "Fore Color", "Appearance",
								 "Selects the foreground color."));
			items.Add(new DesignerActionPropertyItem("Multiline",
								 "Multiline", "Appearance",
								 "Allow multiline text."));
			return items;
		}
	}
	#endregion

#region Column Dropdown class
	//public class ColumnList : StringConverter
	//{

	//    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	//    {
	//        //true means show a combobox
	//        return true;
	//    }

	//    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	//    {
	//        //true will limit to list. false will show the list, but allow free-form entry
	//        return true;
	//    }

	//    public override
	//        System.ComponentModel.TypeConverter.StandardValuesCollection
	//        GetStandardValues(ITypeDescriptorContext context)
	//    {
	//        return new StandardValuesCollection(HE_GlobalVars._ListofCols);
	//    }


	
	//}
	//internal class HE_GlobalVars
	//{
	//    internal static string[] _ListofCols ;

	//}
#endregion
}
