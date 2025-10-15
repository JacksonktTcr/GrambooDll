using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Gramboo.Database
{
	class Columns : ICollection<Column>, IListSource
	{
		private List<Column> _innerList;

		#region ICollection<Column> Members


		public Columns()
		{
			_innerList = new List<Column>();
		}

		public Columns(IEnumerable<Column > C)
			: this()
		{
			this.AddRange(C );
		}

		protected List<Cultist> InnerList
		{
			get { return _innerList; }
		}


		public void Add(Column item)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(Column item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(Column[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Remove(Column item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<Column> Members

		public IEnumerator<Column> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IListSource Members

		public bool ContainsListCollection
		{
			get { throw new NotImplementedException(); }
		}

		public System.Collections.IList GetList()
		{
			throw new NotImplementedException();
		}


		public void AddRange(IEnumerable<Column> C)
		{
			foreach (Column p in C )
			{
				this.Add(p);
			}
		}
		#endregion
	}
}
