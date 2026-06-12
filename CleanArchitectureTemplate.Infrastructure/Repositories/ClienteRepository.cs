using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using CleanArchitectureTemplate.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Infrastructure.Repositories
{
    public class ClienteRepository : IClientesRepository
    {
        private readonly ApplicationDbContext _context;


        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
                throw new ArgumentException("El cliente no exite");

            _context.Clientes.Remove(cliente);

            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> GetByNameAsync(string name)
        {
            // 1. Validamos que el nombre no venga vacío o nulo
            if (string.IsNullOrWhiteSpace(name))
                return null;

            // 2. Buscamos el producto en la base de datos que coincida con el nombre
            // Usamos EF.Functions.Like si quieres que sea insensible a mayúsculas/minúsculas, 
            // o simplemente un Equals/== estándar de LINQ:
            return await _context.Clientes.FirstOrDefaultAsync(p => p.Nombre.ToLower() == name.ToLower());
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            // 1. Validamos que el ID sea un número válido para evitar consultas absurdas
            if (id <= 0)
                return null;

            // 2. Buscamos el producto por su clave primaria
            // FindAsync devuelve automáticamente null si no encuentra el registro
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }
    }
}
