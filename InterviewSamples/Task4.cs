using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace InterviewSamples
{
	public class Post
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		///Should it be visible to public
		public bool IsPublicVisible { get; set; }
		///When it shall become visible to public
		public DateTime PublishTime { get; set; }
		public int Category { get; set; }
		public Guid AuthorId { get; set; }
	}

	public class Author
	{
		public Guid id { get; set; }
		public bool isEmailVerified { get; set; }
	}

	public class PostRepository
	{
		public async Task<List<Post>> SelectAll()
		{
			//will be a db request here in future
			return new List<Post>();
		}

		public Task Update(Post entity)
		{
			//will update entity in db here in future
			return Task.CompletedTask;
		}
	}

	public class AuthorRepository
	{
		public async Task<List<Author>> GetAll()
		{
			//will be a db request here in future
			return new List<Author>();
		}

		public Task Update(Author entity)
		{
			//will update entity in db here in future
			return Task.CompletedTask;
		}
	}


	public class PostManager : PostRepository
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

			AuthorRepository authorRepository = new AuthorRepository();
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
