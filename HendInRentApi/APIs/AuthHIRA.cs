namespace HendInRentApi.APIs
{
    public interface AuthHIRA<TLoginResult, TLoginInputData>
    {
        Task<TLoginResult> Login(TLoginInputData user);
    }
}
