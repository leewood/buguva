using System;
using System.Collections.Generic;
using System.Linq;

namespace mvc.Models
{
    partial class POADataModelsDataContext
    {
        public void get()
        {            
        }
        public List<Worker> GetWorkers()
        {
            return Workers.Where(c => c.deleted == null).ToList<Worker>();
        }

        public void AddWorker(Worker worker)
        {
            Workers.InsertOnSubmit(worker);
            this.SubmitChanges();
        }
    }
}
