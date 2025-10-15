using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gramboo.Database
{
	class DefaultValueEventArgs:EventArgs 
	{
		private Column _Column;

		public DefaultValueEventArgs(Column column)
			: base()
		{
			_Column = column;
		}

		public Column Column
		{
			get { return _Column; }
		}
	}
}
