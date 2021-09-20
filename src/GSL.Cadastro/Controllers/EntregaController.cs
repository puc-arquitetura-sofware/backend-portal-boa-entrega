using GSL.Cadastro.Dominio.Interfaces;
using GSL.Cadastro.Dominio.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GSL.Cadastro.Api.Controllers
{
    [Route("api/entrega")]
    [ApiController]
    public class EntregaController : MainController
    {
        private readonly IMercadoriaRepository _mercadoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EntregaController(
            IMercadoriaRepository mercadoriaRepository, 
            IUsuarioRepository usuarioRepository)
        {
            _mercadoriaRepository = mercadoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<EntregaViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> BuscarTodasEntregas()
        {
            var entregas = await BuscarEntregas();
            //retornar lista
            return CustomResponse(entregas);
        }


        private async Task<List<EntregaViewModel>> BuscarEntregas()
        {
            //Buscar Mercadorias dos Clientes nos Depositos
            var generator = new Random();
            var mercadorias = await _mercadoriaRepository.ObterTodasMercadoriasParaEntregasAsync();
            var usuarios = await _usuarioRepository.ObterTodosAsync();

            var entregas = new List<EntregaViewModel>();
            //MapearRetorno
            foreach (var usuario in usuarios)
            {
                var mercadoria = mercadorias.Where(m => m.MercadoriaClientes.Where(m => m.ClienteId == usuario.Id).Any());

                foreach (var m in mercadoria)
                {
                    var deposito = m.MercadoriaDepositos.FirstOrDefault().Deposito;
                    var entrega = new EntregaViewModel
                    {
                        EnderecoCliente = usuario.Endereco.ToString(),
                        EnderecoDeposito = deposito.EnderecoDeposito.ToString(),
                        NomeCliente = usuario.Nome,
                        NomeMercadoria = m.Nome,
                        PrevisaoEntrega = DateTime.UtcNow.AddDays(generator.Next(1, 10)).ToString("dd/MM/yyyy")
                    };

                    entregas.Add(entrega);
                }
            }

            return entregas;
        }

    }
}
