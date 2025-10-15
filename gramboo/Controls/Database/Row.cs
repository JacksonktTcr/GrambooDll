using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Gramboo.Database
{
    public class Row : DataRow
    {

        internal Row(DataRowBuilder builder)
            : base(builder)
        {

        }

    }
}
