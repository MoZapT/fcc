namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IBaseDataService 
    {
        event Action OnChange;
        HttpClient Http { get; }

        void Init(Action action);
    }
}
