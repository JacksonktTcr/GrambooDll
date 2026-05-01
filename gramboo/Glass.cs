using System;
using System.Windows.Forms;

namespace Gramboo
{
    /// <summary>
    /// ? Simple MessageBox wrapper class
    /// Provides a clean interface for displaying message boxes.
    /// </summary>
    public static class Glass
    {
        /// <summary>
        /// Display a message box with the specified parameters.
        /// </summary>
        /// <param name="message">The message text to display</param>
        /// <param name="title">The title of the message box</param>
        /// <param name="icon">The icon to display</param>
        /// <param name="buttons">The buttons to display</param>
        /// <param name="defaultButton">The default button</param>
        /// <returns>The dialog result from the message box</returns>
        public static DialogResult Show(
            string message = "",
            string title = "",
            MessageBoxIcon icon = MessageBoxIcon.Information,
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1)
        {
            try
            {
                // Use standard Windows Forms MessageBox
                return MessageBox.Show(
                    message ?? "",
                    title ?? "",
                    buttons,
                    icon,
                    defaultButton);
            }
            catch (Exception ex)
            {
                // Fallback if something goes wrong
                return MessageBox.Show(
                    "Error displaying message: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
