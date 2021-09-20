using GSL.Cadastro.SharedKernel.DomainObjects;
using System;
using System.Collections.Generic;

namespace GSL.Cadastro.Dominio.Models.Entidades
{
    public class Deposito : Entidade, IAggregateRoot
    {
        public Deposito(string tipo, EnderecoDeposito endereco, Guid id)
        {
            Id = id;
            Tipo = tipo;
            EnderecoDeposito = endereco;
        }

        public Deposito() { }

        public string Tipo { get; set; }
        public EnderecoDeposito EnderecoDeposito { get; private set; }
        public ICollection<MercadoriaDeposito> MercadoriaDepositos { get; set; }
        public void AtribuirEndereco(EnderecoDeposito endereco)
        {
            EnderecoDeposito = endereco;
        }
    }

}
