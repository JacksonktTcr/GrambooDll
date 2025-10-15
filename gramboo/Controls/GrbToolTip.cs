	using System.Windows.Forms;
 
	using System.Runtime.InteropServices;
using System;

using System.Drawing;

namespace Gramboo.Controls
{
	class GrbToolTip
	{


		/// <summary>

		/// Class providing method for showing Balloon Tips

		/// </summary>






		#region   Enumerations: Global



		/// <summary>

		/// Balloon Icon Types

		/// </summary>

		public enum BalloonIcon
		{
			ShowNone = ToolTipIcon.None,

			ShowInformation = ToolTipIcon.Info,

			ShowWarning = ToolTipIcon.Warning,

			ShowError = ToolTipIcon.Error,
		};



		#endregion



		#region   private Declarations: Balloon Help for PictureBoxes



		private const uint EM_SHOWBALLOONTIP = 0x1503;



		/// <summary>

		/// Type EDITBALLOONTIP converted to a .NET Structure

		/// </summary>
		[StructLayout(LayoutKind.Sequential)]

		private struct EDITBALLOONTIP
		{


			public int cbStruct;
			[MarshalAs(UnmanagedType.LPWStr)]

			public string pszTitle;
			[MarshalAs(UnmanagedType.LPWStr)]

			public string pszText;

			public int ttiIcon;
		}


		/// <summary>

		/// Unicode version of SendMessage API needed for pszText + pszText in EDITBALLOONTIP as these are unicode parameters

		/// </summary>




		#endregion



		#region " private Declarations: Balloon Help / ToolTip for all Controls "
		[DllImport("User32.Dll")]
		public static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);


		/// <summary>

		/// Delay times for ToolTips

		/// </summary>
		private const int DELAY_AUTOPOPUP = 5000;

		private const int DELAY_INITIAL = 500;
		private const int DELAY_RESHOW = 500;



		#endregion



		#region " Subroutines: Balloon Help for PictureBoxes "



		/// <summary>

		/// Balloon Help for PictureBoxes

		/// </summary>

		/// <param name="ctlSource">The PictureBox you want the ToolTip to be displayed under</param>

		/// <param name="strToolTipText">The ToolTip text</param>

		/// <param name="strToolTipTitle">The ToolTip caption</param>

		/// <param name="enmToolTipIcon">The ToolTip icon - None, Info, Warning or Error</param>

		/// <remarks>Use this method for PictureBoxes as additional system balloon notifications are displayed and restrictions implemented. If the PictureBox is a password field, the user will be prompted if the capslock is on and will also not be able to cut text</remarks>

		public static void Show(Control ctlSource, string strToolTipText, string strToolTipTitle, BalloonIcon enmToolTipIcon = BalloonIcon.ShowInformation)
		{


			EDITBALLOONTIP EBT = new EDITBALLOONTIP();


			EBT.cbStruct = Marshal.SizeOf(EBT);

			EBT.ttiIcon = (int)enmToolTipIcon;

			EBT.pszText = strToolTipText;

			EBT.pszTitle = strToolTipTitle;




			IntPtr ptrEBT = Marshal.AllocHGlobal(EBT.cbStruct);



			Marshal.StructureToPtr(EBT, ptrEBT, false);

			//SendMessage( ctlSource.Handle, EM_SHOWBALLOONTIP, (IntPtr)IntPtr.Zero, (IntPtr)ptrEBT);


			PostMessage(ctlSource.Handle, (int)EM_SHOWBALLOONTIP, (int)IntPtr.Zero, (int)ptrEBT);



		}



		#endregion



		#region " Subroutines: Balloon Help / ToolTip for all Controls "



		/// <summary>

		/// Balloon Help / ToolTip for all Controls

		/// </summary>

		/// <param name="ctlSource">The control you want the ToolTip to be displayed under</param>

		/// <param name="strToolTipText">The ToolTip text</param>

		/// <param name="strToolTipTitle">The ToolTip caption</param>

		/// <param name="clrBackColor">The backcolor of the ToolTip (any System.Drawing.Color)</param>

		/// <param name="clrForeColor">The text colour of the ToolTip (any System.Drawing.Color)</param>

		/// <param name="enmToolTipIcon">The ToolTip icon - None, Info, Warning or Error</param>

		/// <param name="intAutoPopDelay">The period of time the ToolTip remains visible on control mousehover event</param>

		/// <param name="intInitialDelay">The period of time before the ToolTip appears with mouseover event</param>

		/// <param name="intReshowDelay">The period of time before subsequent ToolTip windows appear</param>

		/// <param name="blnIsBalloon">Show as a balloon tip (True) or a standard tooltip (False)</param>

		/// <param name="blnUseAnimation">Use animation - Only XP, Windows Server 2003, IE 5+</param>

		/// <param name="blnUseFading">Use fading - Only XP, Windows Server 2003, IE 5+</param>

		/// <param name="blnActive">Enable the tooltip</param>

		/// <param name="blnShowAlways">Force the ToolTip text to be displayed regardless if form has focus or not</param>

		/// <remarks>This method can be used with all form controls except ListViews, ListBoxs, TreeViews and RichPictureBoxes</remarks>

		public void Show(Control ctlSource, string strToolTipText, string strToolTipTitle, Color clrBackColor, Color clrForeColor, BalloonIcon enmToolTipIcon = BalloonIcon.ShowNone, int intAutoPopDelay = DELAY_AUTOPOPUP, int intInitialDelay = DELAY_INITIAL, int intReshowDelay = DELAY_RESHOW, bool blnIsBalloon = true,

bool blnUseAnimation = true, bool blnUseFading = true, bool blnActive = true, bool blnShowAlways = false)
		{


			ToolTip MyToolTip = new ToolTip();

			// Set up the delays for the ToolTip.



			MyToolTip.AutoPopDelay = intAutoPopDelay;

			MyToolTip.InitialDelay = intInitialDelay;

			MyToolTip.ReshowDelay = intReshowDelay;



			// Set up the appearance of the ToolTip



			MyToolTip.BackColor = clrBackColor;

			MyToolTip.ForeColor = clrForeColor;

			MyToolTip.IsBalloon = blnIsBalloon;

			MyToolTip.ToolTipIcon = (ToolTipIcon)enmToolTipIcon;

			MyToolTip.UseAnimation = blnUseAnimation;

			MyToolTip.UseFading = blnUseFading;



			// Set up the ToolTip display options



			MyToolTip.Active = blnActive;

			MyToolTip.ShowAlways = blnShowAlways;



			// Set ToolTip Caption and Text



			MyToolTip.ToolTipTitle = strToolTipTitle;

			MyToolTip.SetToolTip(ctlSource, strToolTipText);






		}


		#endregion


		#region " Properties: Balloon Help / ToolTip for all Controls "



		/// <summary>

		/// System defined ToolTip background colour

		/// </summary>

		/// <returns>System.Drawing.SystemColors.Info</returns>

		public Color TipBackColour
		{



			get { return SystemColors.Info; }
		}




		/// <summary>

		/// System defined ToolTip text colour

		/// </summary>

		/// <returns>System.Drawing.SystemColors.InfoText</returns>

		public Color TipTextColour
		{



			get { return SystemColors.InfoText; }
		}




		#endregion


	}
}
