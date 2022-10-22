namespace HendInRentApi
{
    public interface HIRALogin<TLoginResult, TLoginInputData>
    {
        Task<TLoginResult> Login(TLoginInputData user);
    }
}
