using System;
using System.Collections.Generic;
using System.Linq;

namespace Entith.AspNet.Domain
{
    /// <summary>
    /// A pseudo-service responsible for the creating and updating of
    /// the ITimestampable timestamps. In order to use this you will
    /// need to make sure that this gets instantiated and bound to the
    /// unit of work before SaveChanges() is called.
    /// </summary>
    public class TimestampableService : IDomainService
    {
        private IUnitOfWork _uow;

        public TimestampableService(IUnitOfWork uow)
        {
            uow.RegisterService(this);
            _uow = uow;
        }

        public SaveChangesResults SaveChanges()
        {
            return _uow.SaveChanges();
        }

        public ICollection<SaveChangesResult> OnSaveChanges()
        {
            DateTime now = DateTime.UtcNow;

            List<ICreateTimestampable> added = _uow.GetAdded<ICreateTimestampable>().ToList();
            List<IModifyTimestampable> modified = _uow.GetModified<IModifyTimestampable>().ToList();
            modified.AddRange(_uow.GetAdded<IModifyTimestampable>());

            foreach (ICreateTimestampable e in added)
                e.DateCreated = now;

            foreach (IModifyTimestampable e in modified)
                e.DateModified = now;

            return null;
        }

        public ICollection<SaveChangesResult>  PostSaveChanges()
        {
            return null;
        }
    }
}

