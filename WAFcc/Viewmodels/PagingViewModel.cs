using WAFcc.Models;

namespace WAFcc.Viewmodels
{
    public class PagingViewModel : BaseViewModel
    {

        private readonly int _maximumPages = 10;
        private readonly int _offsetLeft = 2;

        #region PROPERTIES

        public int Amount { get; set; }
        new public int Skip { get; set; }
        new public int Take { get; set; }
        public int CurrentPage { get; set; }
        public int Pages { get; set; }
        public int MaximumPageBtns { get { return CurrentPage + _maximumPages; } }

        public int BeginPage 
        { 
            get 
            {
                int page = (CurrentPage - _offsetLeft);

                if (page <= 0)
                {
                    return 1;
                }

                return page;
            } 
        }

        #endregion

        public PagingViewModel(int take)
        {
            Take = take;
        }

        public PagingViewModel(int skip, int take, int amount)
        {
            Skip = skip;
            Take = take;
            Amount = amount;
            Pages = (Amount / Take) + ((Amount % Take) > 0 ? 1 : 0);
            CurrentPage = (Skip / Take) + 1;
        }

        public bool CanBeShown()
        {
            if (Pages > 1)
            {
                return true;
            }

            return false;
        }
    }
}