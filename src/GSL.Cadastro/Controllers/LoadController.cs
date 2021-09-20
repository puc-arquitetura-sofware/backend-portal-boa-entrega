using GSL.Cadastro.Dominio.Interfaces;
using GSL.Cadastro.Dominio.Models.Entidades;
using GSL.Cadastro.SharedKernel.DomainObjects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GSL.Cadastro.Api.Controllers
{
    [Route("api/load")]
    [ApiController]
    public class LoadController : MainController
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPerfilRepository _perfilRepository;
        private readonly IMercadoriaRepository _mercadoriaRepository;
        private readonly IDepositoRepository _depositoRepository;
        private readonly IMercadoriaDepositoRepository _mercadoriaDepositoRepository;
        private readonly IMercadoriaClienteRepository _mercadoriaClienteRepository;

        public LoadController(
            IUsuarioRepository usuarioRepository,
            IPerfilRepository perfilRepository,
            IMercadoriaRepository mercadoriaRepository,
            IDepositoRepository depositoRepository,
            IMercadoriaDepositoRepository mercadoriaDepositoRepository, 
            IMercadoriaClienteRepository mercadoriaClienteRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
            _mercadoriaRepository = mercadoriaRepository;
            _depositoRepository = depositoRepository;
            _mercadoriaDepositoRepository = mercadoriaDepositoRepository;
            _mercadoriaClienteRepository = mercadoriaClienteRepository;
        }

        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Load()
        {
            await adicionarPerfis();
            await adicionarUsuarios();
            await adicionarMercadorias();
            await adicionarDepositos();
            await vincularDepositoMercadorias();
            await vincularClienteMercadoria();

            return Ok();
        }

        private async Task adicionarPerfis()
        {
            
            await _perfilRepository.AdicionarAsync(new Perfil
            {
                Id = Guid.Parse("5fa163ae-dc8a-481e-a829-3ecd0b096121"),
                Descricao = "Cliente",
                Ativo = true,
                AtualizadoEm = DateTime.UtcNow,
                CriadoEm = DateTime.UtcNow
            });
            await _perfilRepository.AdicionarAsync(new Perfil
            {
                Id = Guid.Parse("6fa163ae-dc8a-481e-a829-3ecd0b096122"),
                Descricao = "Fornecedor",
                Ativo = true,
                AtualizadoEm = DateTime.UtcNow,
                CriadoEm = DateTime.UtcNow
            });
        }

        private async Task adicionarUsuarios()
        {
            await _usuarioRepository.AdicionarAsync(new Usuario(
                        "Douglas Gomes Modesto",
                        false,
                        true,
                        "senha@123",
                        new Documento("82207743047"),
                        new Email("douglasgomesmodesto@gmail.com."),
                        new Endereco("Rua Barão Amaral de Cabo frio", "50", "Casa", "Jardim Itápolis", "03938172", "São Paulo", "São Paulo", Guid.Parse("327e4f13-d07d-4a6b-a299-b652367e4d32")),
                        Guid.Parse("5fa163ae-dc8a-481e-a829-3ecd0b096121"),
                        Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f24")
                    ));

            await _usuarioRepository.AdicionarAsync(new Usuario(
                        "Pablo Christian Pereira Nazareth",
                        false,
                        true,
                        "senha@123",
                        new Documento("88261494020"),
                        new Email("pablo.christian@gmail.com."),
                        new Endereco("Av. Brasil", "2.023", " Prédio 1 - Edifício Dom Cabral", "Funcionários", "30140002", "Belo Horizonte", "Minas Gerais", Guid.Parse("327e4f13-d07d-4a6b-a299-b652367e4d33")),
                        Guid.Parse("5fa163ae-dc8a-481e-a829-3ecd0b096121"),
                        Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f25")
                    ));
        }

        private async Task adicionarMercadorias()
        {
            await _mercadoriaRepository.AdicionarAsync(new Mercadoria("Monitor Wide Screen 25", "Monitor Wide Screen 25 LG", 950, true, Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf29")));
            await _mercadoriaRepository.AdicionarAsync(new Mercadoria("Teclado Mecanico", "Teclado Mecanico Dragon com Led", 420, true, Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf30")));
            await _mercadoriaRepository.AdicionarAsync(new Mercadoria("Mouser Gamer", "Mouser Gamer Dragon com Led", 260, true, Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf31")));
            await _mercadoriaRepository.AdicionarAsync(new Mercadoria("Notebook Avell A62", "Notebook de alta performance", 7850, true, Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf32")));
        }

        private async Task adicionarDepositos()
        {
            await _depositoRepository.AdicionarAsync(new Deposito(
                    "Armazén",
                    new EnderecoDeposito("Avenida Celso Garcia", "155", "", "Brás", "03015000", "São Paulo", "São Paulo", Guid.Parse("75880D3B-3016-491C-AAB9-443019125CD2")),
                    Guid.Parse("4DF14C90-1F4E-4984-B745-B6ADCCAB5F20")
                   ));

            await _depositoRepository.AdicionarAsync(new Deposito(
                    "Galpão",
                    new EnderecoDeposito("Av. Dom José Gaspar", "500", "Prédio 47", " Bairro Coração Eucarístico", "30535901", "Belo Horizonte", "Minas Gerais", Guid.Parse("34E96D53-46FF-455A-8B39-4D96092FE766")),
                    Guid.Parse("41B98488-F609-4AE7-8147-E598D0C1F4BC")
                   ));

        }

        private async Task vincularDepositoMercadorias()
        {
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf29"), Guid.Parse("4DF14C90-1F4E-4984-B745-B6ADCCAB5F20"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf30"), Guid.Parse("4DF14C90-1F4E-4984-B745-B6ADCCAB5F20"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf31"), Guid.Parse("4DF14C90-1F4E-4984-B745-B6ADCCAB5F20"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf32"), Guid.Parse("4DF14C90-1F4E-4984-B745-B6ADCCAB5F20"), 30));


            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf29"), Guid.Parse("41B98488-F609-4AE7-8147-E598D0C1F4BC"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf30"), Guid.Parse("41B98488-F609-4AE7-8147-E598D0C1F4BC"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf31"), Guid.Parse("41B98488-F609-4AE7-8147-E598D0C1F4BC"), 30));
            await _mercadoriaDepositoRepository.AdicionarAsync(new MercadoriaDeposito(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf32"), Guid.Parse("41B98488-F609-4AE7-8147-E598D0C1F4BC"), 30));

        }

        private async Task vincularClienteMercadoria()
        {
            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf29"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f24")));
            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf30"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f24")));
            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf31"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f24")));
            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf32"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f24")));

            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf29"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f25")));
            await _mercadoriaClienteRepository.AdicionarAsync(new MercadoriaCliente(Guid.Parse("9f86f90b-775e-425f-9f15-4d6f3985bf31"), Guid.Parse("84b3003d-a0f7-49bc-bb18-45a8d4269f25")));
        }
    }
}
