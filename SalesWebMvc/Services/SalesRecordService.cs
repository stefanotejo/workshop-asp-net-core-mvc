using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Sync FindByDate
        public List<SalesRecord> FindByDate(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.CreatedAt >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.CreatedAt <= maxDate.Value);
            }

            return result
                .Include(x => x.Seller) // Join with table Seller
                .Include(x => x.Seller.Department) // Join with table Department
                .OrderByDescending(x => x.CreatedAt) // Ordering by date (decrescent)
                .ToList();
        }

        // Async FindByDate
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) // If minDate was given
            {
                result = result.Where(x => x.CreatedAt >= minDate.Value); // Apply minDate restriction
            }
            if (maxDate.HasValue) // If maxDate was given
            {
                result = result.Where(x => x.CreatedAt <= maxDate.Value); // Apply maxDate restriction
            }

            return await result
                .Include(x => x.Seller) // Join with table Seller
                .Include(x => x.Seller.Department) // Join with table Department
                .OrderByDescending(x => x.CreatedAt) // Ordering by date (decrescent)
                .ToListAsync(); // db access
        }

        // Async FindByDateGrouping
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            IQueryable<SalesRecord> result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue) // If minDate was given
            {
                result = result.Where(x => x.CreatedAt >= minDate.Value); // Apply minDate restriction
            }
            if (maxDate.HasValue) // If maxDate was given
            {
                result = result.Where(x => x.CreatedAt <= maxDate.Value); // Apply maxDate restriction
            }

            return await result
                .Include(x => x.Seller) // Join with table Seller
                .Include(x => x.Seller.Department) // Join with table Department
                .OrderByDescending(x => x.CreatedAt) // Ordering by date (decrescent)
                .GroupBy(x => x.Seller.Department) // Group by department. This return IGrouping
                .ToListAsync(); // db access
        }
    }
}
