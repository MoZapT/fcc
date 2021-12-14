namespace WebAppFcc.Shared.Interfaces.DataServices
{
    public interface IBaseDataService 
    {
        event Action OnChange;

        void Init(Action action);
    }
}
