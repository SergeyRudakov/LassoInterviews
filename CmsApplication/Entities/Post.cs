using System;
using System.Collections.Generic;
using System.Text;

namespace CmsApplication.Entities
{
	public class Post
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string FullContent { get; set; }
		///When post is finished, then author sets it to be public visible
		public bool IsPublicVisible { get; set; }
		///When post shall become visible to public
		public DateTime PublishTime { get; set; }
		public int Category { get; set; }
		public Guid AuthorId { get; set; }
	}
}
