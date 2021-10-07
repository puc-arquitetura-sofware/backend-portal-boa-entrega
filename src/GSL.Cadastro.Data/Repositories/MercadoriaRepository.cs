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
    public class MercadoriaRepository : BaseRepository<Mercadoria>, IMercadoriaRepository
    {   
        private readonly CadastroDbContext _context;
        public MercadoriaRepository(CadastroDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Mercadoria>> ObterPorClienteIdAsync(Guid clienteId)
        {
            var result = await _context.MercadoriaClientes
                 .Include(m => m.Mercadoria)
                 .AsNoTracking()
                 .Where(x => x.ClienteId == clienteId)
                 .Select(m => m.Mercadoria)
                 .ToListAsync();


            return result;
        }

        public async Task<IEnumerable<MercadoriaDeposito>> ObterPorDepositoIdAsync(Guid depositoId)
        {

            var result = await _context.MercadoriaDepositos
                .Include(m => m.Mercadoria)
                .AsNoTracking()
                .Where(x => x.DepositoId == depositoId)
                .Select(m => m)
                .ToListAsync();


            return result;
        }

        public async Task<IEnumerable<Mercadoria>> ObterTodasMercadoriasParaEntregasAsync()
        {
            var result = await _context.Mercadorias
                  .Include(m => m.MercadoriaDepositos)
                  .ThenInclude(d => d.Deposito)
                    .ThenInclude(e => e.EnderecoDeposito)
                  .Include(m => m.MercadoriaClientes)
                  .ThenInclude(c => c.Cliente)
                    .ThenInclude(e => e.Endereco)
                  .AsNoTracking()
                  .ToListAsync();



            return result;
        }
    }
}
