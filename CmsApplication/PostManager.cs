using CmsApplication.Entities;
using CmsApplication.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CmsApplication
{
	public class PostManager : InMemoryPostRepository
	{
		public async Task<List<Post>> SelectPosts()
		{
			var list = await this.SelectAll();

			var result = new List<Post>();
			for (var i = 0; i < list.Count; i++)
			{
				if (list[i].IsPublicVisible && list[i].PublishTime < DateTime.UtcNow)
				{
					result.Add(list[i]);
				}
			}

			return result;
		}

		public void PublishPost(Post post)
		{
			post.IsPublicVisible = true;
			post.PublishTime = DateTime.UtcNow;
		}
	}


	public class MarketingPostManager : PostManager
	{
		public async new Task<List<Post>> SelectPosts()
		{
			var list = await this.SelectAll();

			var result = new List<Post>();
			var authorIds = new List<Guid>();
			int marketingCategory = 10;

			for (var i = 0; i < list.Count; i++)
			{
				if (list[i].IsPublicVisible && list[i].PublishTime < DateTime.UtcNow
					&& list[i].Category == marketingCategory)
				{
					result.Add(list[i]);
					authorIds.Add(list[i].AuthorId);
				}
			}

			IAuthorRepository authorRepository = new InMemoryAuthorRepository();
			var authors = await authorRepository.GetAll();

			return result.Join(authors,
				(post) => post.AuthorId,
				(author) => author.id,
				(post, author) => new {
					Post = post,
					Author = author
				})
				.Where(postAuthor => postAuthor.Author.isEmailVerified)
				.Select(postAuthor => postAuthor.Post)
				.ToList();
		}

		public new void PublishPost(Post post)
		{
			base.PublishPost(post);
			post.Category = 10;
			post.Title = "Marketing: " + post.Title;
		}
	}
}
