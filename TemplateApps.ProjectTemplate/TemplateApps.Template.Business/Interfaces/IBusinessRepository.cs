using System;
using System.Collections;
using System.Collections.Generic;
using SharpRepository.Repository;
using SharpRepository.Repository.Traits;

namespace $safeprojectname$.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBusinessRepository<TDataObject>
        : IRepository<TDataObject>, IRepositoryQueryable<TDataObject>, ICanFind<TDataObject>, IEnumerable<TDataObject>, IEnumerable, IDisposable
        where TDataObject : class
    {

    }
}
