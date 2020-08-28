using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
	public interface IUnitOfWork<Tcontext>
	{
		public Tcontext _context { get; }
		Task SaveAsync(RequestContext requestContext = null);
		int Save(RequestContext requestContext = null);
		IDbContextTransaction BeginTransaction();
	}
}
