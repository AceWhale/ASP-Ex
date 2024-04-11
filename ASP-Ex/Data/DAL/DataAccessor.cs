using ASP_Ex.Services.Kdf;

namespace ASP_Ex.Data.DAL
{
	public class DataAccessor
	{
		public readonly DataContext _dataContext;
        private readonly IKdfService _kdfService;

        public UserDao UserDao { get; private set; }
        public DataAccessor(DataContext dataContext, IKdfService kdfService)
        {
            _dataContext = dataContext;
            _kdfService = kdfService;
            UserDao = new UserDao(dataContext, kdfService);
        }
    }
}
