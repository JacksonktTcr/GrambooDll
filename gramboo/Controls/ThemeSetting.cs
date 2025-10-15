using Gramboo.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThemeCreator
{
    public partial class ThemeSetting : GrbForm
    {



         private static ThemeSetting instance;
        public static ThemeSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ThemeSetting();
                }
                else if (instance.IsDisposed)
                {
                    instance = new ThemeSetting();
                }
                return instance;
            }
        }
 




        Themes themes;

        public ThemeSetting()
        {
            InitializeComponent();

            themes = new Themes();

            themes = Themes.Open();

            if (themes != null)
                themes.ApplyTheme(this);
            else
                themes = new Themes();

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void ThemeSetting_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = themes;

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            themes.ApplyTheme(this);
            themes.Save();
        }



    }
}
