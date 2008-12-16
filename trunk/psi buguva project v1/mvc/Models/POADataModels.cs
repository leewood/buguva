using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Components.Validator;
using System.Web.Mvc;

namespace mvc.Models
{                        
    public partial class WorkerStatus
    {
        public ErrorSummary Validate()
        {
            ValidatorRunner vr = new ValidatorRunner(true, new CachedValidationRegistry());
            if (!vr.IsValid(this))
                return vr.GetErrorSummary(this);
            else
                return null;
        }
    }
                           
    partial class POADataModelsDataContext
    {
        public void get()
        {
        }
        public List<Worker> GetWorkers()
        {
            return Workers.Where(c => c.deleted == null).ToList<Worker>().Where(u => u.administationView()).ToList();
        }

        public void AddWorker(Worker worker)
        {
            Workers.InsertOnSubmit(worker);
            this.SubmitChanges();
        }

    }
}
