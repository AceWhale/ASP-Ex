namespace ASP_Ex.Services.Kdf
{
	public interface IKdfService
	{
		string DerivedKey(String salt, String password);
	}
}
