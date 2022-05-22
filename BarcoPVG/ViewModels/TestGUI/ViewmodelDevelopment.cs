namespace BarcoPVG.ViewModels.TestGUI
{
    public class ViewModelDevelopment : AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelDevelopment():base()
        {
            Load();
        }

        // Functie used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _daoJR.GetAllJobRequests();
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
