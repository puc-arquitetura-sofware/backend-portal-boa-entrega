using GSL.Cadastro.Api.Configuration.Mappers;
using GSL.Cadastro.Dominio.Interfaces;
using GSL.Cadastro.Dominio.Models.Entidades;
using GSL.Cadastro.Dominio.Models.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GSL.Cadastro.Api.Controllers
{
    [Route("api/mercadoria")]
    [ApiController]
    public class MercadoriaController : MainController
    {
        private readonly IMercadoriaRepository _mercadoriaRepository;
        private readonly IDepositoRepository _depositoRepository;
        private readonly IMercadoriaDepositoRepository _mercadoriaDepositoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMercadoriaFornecedorRepository _mercadoriaFornecedorRepository;
        private readonly IMercadoriaClienteRepository _mercadoriaClienteRepository;
        public MercadoriaController(
            IMercadoriaRepository mercadoriaRepository,
            IDepositoRepository depositoRepository,
            IMercadoriaDepositoRepository mercadoriaDepositoRepository,
            IUsuarioRepository usuarioRepository,
            IMercadoriaFornecedorRepository mercadoriaFornecedorRepository, IMercadoriaClienteRepository mercadoriaClienteRepository)
        {
            _mercadoriaRepository = mercadoriaRepository;
            _depositoRepository = depositoRepository;
            _mercadoriaDepositoRepository = mercadoriaDepositoRepository;
            _usuarioRepository = usuarioRepository;
            _mercadoriaFornecedorRepository = mercadoriaFornecedorRepository;
            _mercadoriaClienteRepository = mercadoriaClienteRepository;
        }


        [HttpGet()]
        [ProducesResponseType(typeof(List<MercadoriaViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> BuscarTodasMercadorias()
        {

            var mercadorias = await _mercadoriaRepository.ObterTodosAsync();

            var listMercadorias = new List<MercadoriaViewModel>();

            //var mercadoriaMock = new MercadoriaViewModel("mouse", "mouse mecanico dragon", 250.00, true, Guid.NewGuid());
            //listMercadorias.Add(mercadoriaMock);

            foreach (var mercadoria in mercadorias)
            {
                listMercadorias.Add(MapperUtil.MapperMercadoriaToMercadoriaViewModel(mercadoria));
            }

            return CustomResponse(listMercadorias);

        }


        [HttpGet("id")]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> ObterMercadoriaPorId([FromQuery] Guid id)
        {
            var mercadoria = await _mercadoriaRepository.ObterPorIdAsync(id);
            return mercadoria == null ? NotFound() : CustomResponse(mercadoria);
        }

        [HttpGet("obter-por-deposito")]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> ObterMercadoriaPorDepositoId([FromQuery] Guid depositoId)
        {
            var mercadoriaDeposito = await _mercadoriaRepository.ObterPorDepositoIdAsync(depositoId);

            var mercadoriasViewModel = mercadoriaDeposito.Select(x => MapperUtil.MapperMercadoriaToMercadoriaViewModel(x.Mercadoria, x.QuantidadeEstoque ));

            return mercadoriaDeposito == null ? NotFound() : CustomResponse(mercadoriasViewModel);
        }

        [HttpGet("obter-por-cliente")]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> ObterMercadoriaPorClienteId([FromQuery] Guid clienteId)
        {
            var mercadoria = await _mercadoriaRepository.ObterPorClienteIdAsync(clienteId);
            return mercadoria == null ? NotFound() : CustomResponse(mercadoria);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Salvar([FromBody] MercadoriaViewModel mercadoriaViewModel)
        {
            var mercadoria = MapperUtil.MapperMercadoriaViewModelToMercadoria(mercadoriaViewModel);

            await _mercadoriaRepository.AdicionarAsync(mercadoria);

            return CustomResponse(MapperUtil.MapperMercadoriaToMercadoriaViewModel(mercadoria));
        }

        [HttpPut("id")]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Atualizar([FromQuery] Guid id, [FromBody] MercadoriaViewModel mercadoriaViewModel)
        {
            var depositoExist = await _mercadoriaRepository.ObterPorIdAsync(id);

            if (depositoExist == null)
                throw new NullReferenceException($"a propriedade { nameof(id) } deve ser informada");

            var deposito = MapperUtil.MapperMercadoriaViewModelToMercadoria(mercadoriaViewModel);
            await _mercadoriaRepository.AtualizarAsync(deposito);
            return CustomResponse(MapperUtil.MapperMercadoriaToMercadoriaViewModel(deposito));
        }

        [HttpDelete("id")]
        [ProducesResponseType(typeof(MercadoriaViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Deletar([FromQuery] Guid id)
        {
            var depositoExist = await _mercadoriaRepository.ObterPorIdAsync(id);

            if (depositoExist == null)
                throw new NullReferenceException($"a propriedade { nameof(id) } deve ser informada");

            await _mercadoriaRepository.RemoverAsync(depositoExist);
            return CustomResponse(MapperUtil.MapperMercadoriaToMercadoriaViewModel(depositoExist));
        }



        [HttpPost("mercadoriaId/fornecedorId")]
        [ProducesResponseType(typeof(MercadoriaFornecedorViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> VincularMercadoriaFornecedor([FromQuery] Guid mercadoriaId, [FromQuery] Guid fornecedorId)
        {
            var mercadoriaExist = await _mercadoriaRepository.ObterPorIdAsync(mercadoriaId);

            if (mercadoriaExist == null)
                throw new NullReferenceException($"a propriedade { nameof(mercadoriaId) } deve ser informada");

            var fornecedorExist = await _usuarioRepository.ObterPorIdAsync(fornecedorId);

            if (fornecedorExist == null)
                throw new NullReferenceException($"a propriedade { nameof(fornecedorId) } deve ser informada");


            //TODO: Inserir Relação entre Mercadoria e Deposito
            var mercadoriaFornecedor = new MercadoriaFornecedor(mercadoriaExist.Id, fornecedorExist.Id);
            await _mercadoriaFornecedorRepository.AdicionarAsync(mercadoriaFornecedor);


            var mercadoriViewModel = MapperUtil.MapperMercadoriaToMercadoriaViewModel(mercadoriaExist);
            var fornecedorViewModel = MapperUtil.MapperUsuarioToUsuarioViewModel(fornecedorExist);
            return CustomResponse(new MercadoriaFornecedorViewModel(mercadoriViewModel, fornecedorViewModel));
        }

        [HttpPost("mercadoriaId/clienteId")]
        [ProducesResponseType(typeof(MercadoriaClienteViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> VincularMercadoriaCliente([FromQuery] Guid mercadoriaId, [FromQuery] Guid clienteId)
        {
            var mercadoriaExist = await _mercadoriaRepository.ObterPorIdAsync(mercadoriaId);

            if (mercadoriaExist == null)
                throw new NullReferenceException($"a propriedade { nameof(mercadoriaId) } deve ser informada");

            var clienteExist = await _usuarioRepository.ObterPorIdAsync(clienteId);

            if (clienteExist == null)
                throw new NullReferenceException($"a propriedade { nameof(clienteId) } deve ser informada");


            //TODO: Inserir Relação entre Mercadoria e Deposito
            var mercadoriaCliente = new MercadoriaCliente(mercadoriaExist.Id, clienteExist.Id);
            await _mercadoriaClienteRepository.AdicionarAsync(mercadoriaCliente);


            var mercadoriViewModel = MapperUtil.MapperMercadoriaToMercadoriaViewModel(mercadoriaExist);
            var fornecedorViewModel = MapperUtil.MapperUsuarioToUsuarioViewModel(clienteExist);
            return CustomResponse(new MercadoriaClienteViewModel(mercadoriViewModel, fornecedorViewModel));
        }
    }
}
