using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc.Models
{
    partial class POADataModelsDataContext
    {
        public List<Worker> GetWorkers()
        {
            return Workers.ToList<Worker>();
        }

        public void AddWorker(Worker worker)
        {
            Workers.InsertOnSubmit(worker);
            this.SubmitChanges();
        }
    }
}
