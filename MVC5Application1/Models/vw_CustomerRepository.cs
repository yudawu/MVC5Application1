using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Application1.Models
{   
	public  class vw_CustomerRepository : EFRepository<vw_Customer>, Ivw_CustomerRepository
	{
        public vw_Customer Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface Ivw_CustomerRepository : IRepository<vw_Customer>
	{

	}
}