﻿using AudioStreaming.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AudioStreaming.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>()
          where T : class;

       int Commit();
        void Dispose();

        Task<int> CommitAsync();
    }
}
