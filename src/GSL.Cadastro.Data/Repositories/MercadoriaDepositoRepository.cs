using GSL.Cadastro.Dominio.Interfaces;
using GSL.Cadastro.Dominio.Models.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSL.Cadastro.Data.Repositories
{
    public class MercadoriaDepositoRepository : BaseRepository<MercadoriaDeposito>, IMercadoriaDepositoRepository
    {   
        private readonly CadastroDbContext _context;
        public MercadoriaDepositoRepository(CadastroDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Mercadoria>> ObterPorClienteIdAsync(Guid clienteId)
        {
            var result = await _context.MercadoriaClientes
            .Include(x => x.Cliente)
            .Include(x => x.Mercadoria)
            .AsNoTracking().ToListAsync();

            return result
               .Where(u => u.ClienteId == clienteId)
               .Select(m => m.Mercadoria);
        }

        public async Task<IEnumerable<Mercadoria>> ObterPorDepositoIdAsync(Guid depositoId)
        {
            var result = await _context.MercadoriaDepositos
            .Include(x => x.Deposito)
            .Include(x => x.Mercadoria)
            .AsNoTracking().ToListAsync();
            
            return result
               .Where(u => u.DepositoId == depositoId)
               .Select(m => m.Mercadoria);
        }
    }
}
