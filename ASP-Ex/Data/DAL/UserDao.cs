using ASP_Ex.Data.Entities;

namespace ASP_Ex.Data.DAL
{
	public class UserDao
	{
		public readonly DataContext _dataContext;

		public UserDao(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
		public void Signup(User user)
		{
			if (user.Id == default)
			{
				user.Id = Guid.NewGuid();
			}
			_dataContext.Users.Add(user);
			_dataContext.SaveChanges();
		}
	}
}
