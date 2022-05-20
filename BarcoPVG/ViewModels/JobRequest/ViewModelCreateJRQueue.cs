using System.Linq;

namespace BarcoPVG.ViewModels.JobRequest
{
    public class ViewModelCreateJRQueue : AbstractViewModelCollectionRQ
    {

        //Constructor
        public ViewModelCreateJRQueue() : base()
        {
            Load();
        }


        // Functie used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            var requestIds = _daoJR.GetAllJobRequests().Where(rq => (rq.Requester == _daoLogin.BarcoUser.Name)/* && (rq.JrStatus == "To approve")*/);
            IdRequestsOnly.Clear();



            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
