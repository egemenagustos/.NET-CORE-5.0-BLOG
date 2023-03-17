using BlogShared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogShared.Entities.Abstract
{
	public class DtoGetBase
	{
		public virtual ResultStates ResultStates { get; set; }

		public virtual string Message { get; set; }

        public virtual int CurrentPage { get; set; } = 1; /* Bizlere bir sayfa değeri verilmezse ilk sayfadan başlayacağız.*/

        public virtual int PageSize { get; set; } = 5; /* Bir sayfada kaç değer göstermek istediğimiz.*/

        public virtual int TotalCount { get; set; } /* Toplamda kaç makale olduğunu tutacağız ki buna göre sayfalama yapalım .*/

        public virtual int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

        public virtual bool ShowPrevious => CurrentPage > 1;

        public virtual bool ShowNext => CurrentPage < TotalPages;

        public virtual bool IsAscending { get; set; } = false;

        
    }
}
