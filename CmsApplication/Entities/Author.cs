using System;
using System.Collections.Generic;
using System.Text;

namespace CmsApplication.Entities
{
	public class Author
	{
		public Guid id { get; set; }
		public string name { get; set; }
		public string email { get; set; }
		public bool isEmailVerified { get; set; }
	}
}
