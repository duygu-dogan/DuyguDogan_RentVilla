using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Services
{
    public interface IProductService
    {
        #region Generic
        List<Product> GetProducts();
        #endregion
    }
}
