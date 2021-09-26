using DAL.Entities;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IResourcesRepository
    {
        IEnumerable<Resource> getAll();
        void bookResource(ResourceModel model);
    }
}
