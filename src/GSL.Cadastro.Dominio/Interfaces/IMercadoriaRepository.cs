﻿using GSL.Cadastro.Dominio.Models.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSL.Cadastro.Dominio.Interfaces
{
    public interface IMercadoriaRepository : IBaseRepository<Mercadoria>
    {
        Task<IEnumerable<MercadoriaDeposito>> ObterPorDepositoIdAsync(Guid depositoId);
        Task<IEnumerable<Mercadoria>> ObterPorClienteIdAsync(Guid clienteId);
        Task<IEnumerable<Mercadoria>> ObterTodasMercadoriasParaEntregasAsync(); 
    }
}
