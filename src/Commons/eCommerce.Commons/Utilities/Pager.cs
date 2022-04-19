using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Commons.Utilities
{
    public class Pager<T>
    {
        public virtual int Page { get; set; }
        private int _PageSize;
        public virtual int PageSize
        {
            get => _PageSize; set
            {
                _PageSize = value;
                Page = 1;
            }
        }
        private IEnumerable<T> _Items;
        public virtual IEnumerable<T> Items
        {
            get => _Items;
            set
            {
                _Items = value;
                Page = 1;
            }
        }

        public int GetStartItems()
        {
            var result = PageSize * (Page - 1);
            return result <= 0 ? 1 : result;
        }

        public int GetEndItems()
        {
            var i = GetStartItems();
            var r = (i == 1 ? 0 : i);
            r += PageSize;
            return r > Items.Count() ? Items.Count() : r;
        }

        public int GetTotalPages()
        {
            return Convert.ToInt32(Math.Ceiling(((double)Items.Count()) / (PageSize)));
        }

        public IEnumerable<T> GetItemsByPage()
        {
            return _Items.Skip((Page -1) * PageSize).Take(PageSize);
        }
    }
}
