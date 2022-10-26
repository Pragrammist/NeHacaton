namespace HendInRentApi
{
    public interface HIRALogin<TLoginResult, TLoginInputData> // это интерфейс используется в WEB проекте
    {
        Task<TLoginResult> Login(TLoginInputData user);
    }
}
