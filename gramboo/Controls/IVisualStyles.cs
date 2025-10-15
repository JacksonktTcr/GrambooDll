using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gramboo.Controls
{
    interface IVisualStyles
    {
          Color ActiveBorderColor { get; set; }
          Color ActiveBackColor { get; set; }
          Color NormalBorderColor { get; set; }
          Color NormalBackColor { get; set; }
    }
}
