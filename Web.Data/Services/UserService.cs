using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models;

namespace Web.Data.Services
{
	public interface IUserService
	{
		Task<List<User>> Get();
		Task<User> Get(string id);
		Task<User> Get(string email, string password);
	}
	public class UserService : IUserService
	{
		readonly DataContext context;
		public UserService(DataContext context) => this.context = context;

		public async Task<List<User>> Get()
		{
			return await context.User.AsNoTracking().ToListAsync();
		}

		public async Task<User> Get(string id)
		{
			var result = await context.User.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);

			return result;
		}

		public async Task<User> Get(string email, string password)
		{
			var result = await context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email && x.Password == password);

			return result;
		}

		public async Task<int> AddAsync(User TEntity)
		{
			int old_no = 0;
			var userid = "";

			using (var trans = context.Database.BeginTransaction())
			{
				try
				{
					var userdata = await context.User.AsNoTracking().OrderByDescending(x => x.CreateDate).ThenByDescending(t => t.UserId).FirstOrDefaultAsync();
					if(userdata != null)
					{
						old_no = int.Parse(userdata.UserId.Substring(2, 4)) + 1;

						userid = "M." + old_no.ToString().PadLeft(4,'0');
					}
					else
					{
						old_no = 1;
						userid = "M." + old_no.ToString().PadLeft(4, '0');
					}

					TEntity.UserId = userid;
					TEntity.RegisterDate = DateTime.Now;
					TEntity.CreateDate = DateTime.Now;
					context.Add(TEntity);

					trans.Commit();
					return await context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					trans.Rollback();
					return await context.SaveChangesAsync();
				}
			}

				
		}
	}
}
