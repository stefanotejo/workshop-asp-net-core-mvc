using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        // Sync FindAll
        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        // Async FindAll
        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
            // await is needed for the compiler to know this instruction is async
        }

        // Sync Insert
        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        // Async Insert
        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller); // This happens in the heap internal memory
            await _context.SaveChangesAsync(); // This is the operation that accesses the database, and therefore must be async
        }

        // Sync FindById
        public Seller FindById(int id)
        {
            // Include(seller => seller.Department) is for joining the Seller entry with his corresponding Department
            return _context.Seller.Include(seller => seller.Department).FirstOrDefault(seller => seller.Id == id);

            /* This is called eager loading, which is loading objects not only the sought object, but also the objects
             * associated with it */
        }

        // Async FindById
        public async Task<Seller> FindByIdAsync(int id)
        {
            // Include(seller => seller.Department) is for joining the Seller entry with his corresponding Department
            // FirstOrDefault is the operation that accesses the database
            return await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.Id == id);

            /* This is called eager loading, which is loading objects not only the sought object, but also the objects
             * associated with it */
        }

        // Sync Remove
        public void Remove(int id)
        {
            Seller seller = _context.Seller.Find(id);
            _context.Seller.Remove(seller);
            _context.SaveChanges();
        }

        // Async Remove
        public async Task RemoveAsync(int id)
        {
            // Find and SaveChanges operate on the database. Seller.Remove operates on the heap
            Seller seller = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(seller);
            await _context.SaveChangesAsync();
        }

        // Sync Update
        public void Update(Seller seller)
        {
            if(!_context.Seller.Any(x => x.Id == seller.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e) // Database exception
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        // Async Update
        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);
            // Had to do like this, don't exactly know why, only that Any accesses the database
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync(); // SaveChanges also operates on the database
            }
            catch (DbUpdateConcurrencyException e) // Database exception
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
