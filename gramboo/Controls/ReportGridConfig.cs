using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramboo.Controls
{
    class ReportGridSettings
    {

        public List<string> Columns { get; set; }
        public Dictionary<string,int> ColumnWidths { get; set; }
        public string ReportTitle { get; set; }
        public string ReportName { get; set; }

        ReportGridSettings()
        {

        }

        


    }
}
