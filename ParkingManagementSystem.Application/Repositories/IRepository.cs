using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagementSystem.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
    }
}
