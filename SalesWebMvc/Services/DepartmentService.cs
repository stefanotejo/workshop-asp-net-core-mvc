using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Sync FindAll
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(d => d.Name).ToList();
        }

        // Async FindAll
        public async Task<List<Department>> FindAllAsync()
        {
            /* Linq expressions are resolved on demand. That means that the d => d.Name expression will only be resolved
             * when some other thing provokes its execution. In this case, that thing is the ToList function, that is a
             * sync method. We then swap it for ToListAsync, which belongs to the namespace Microsoft.EntityFrameworkCore.
             * Also, the function that accesses the database is the ToList.
             */
            return await _context.Department.OrderBy(d => d.Name).ToListAsync();
            // await is needed for the compiler to know this instruction is async
        }
    }
}
