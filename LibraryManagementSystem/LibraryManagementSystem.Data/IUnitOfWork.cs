using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementSystem.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
