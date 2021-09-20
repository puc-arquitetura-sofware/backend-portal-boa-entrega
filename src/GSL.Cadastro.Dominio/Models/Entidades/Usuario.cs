using GSL.Cadastro.SharedKernel.DomainObjects;
using System;
using System.Collections.Generic;

namespace GSL.Cadastro.Dominio.Models.Entidades
{
    public class Usuario : Entidade
    {
        public Usuario(string nome, bool bloqueado, bool ativo, Email email, Endereco endereco, Guid perfilId, Guid id)
        {
            Id = id;
            PerfilId = perfilId;
            Nome = nome;
            Bloqueado = bloqueado;
            Ativo = ativo;
            Email = email;
            Endereco = endereco;
        }

        public Usuario(string nome, bool bloqueado, bool ativo, string password, Documento documento , Email email, Endereco endereco, Guid perfilId, Guid id)
        {
            Id = id;
            PerfilId = perfilId;
            Nome = nome;
            Bloqueado = bloqueado;
            Ativo = ativo;
            Email = email;
            Documento = documento;
            Endereco = endereco;
            Password = password;
        }

        public string Nome { get; private set; }
        public bool Bloqueado { get; private set; }
        public bool Ativo { get; private set; }
        public string Password { get; set; }
        public Documento Documento { get; set; }
        public Email Email { get; set; }
        public Endereco Endereco { get; private set; }
        public virtual Perfil Perfil { get; private set; }
        public Guid PerfilId {get; set;}

        //EF Constructor
        public Usuario() { }
        public ICollection<MercadoriaFornecedor> MercadoriaFornecedores { get; set; }
        public ICollection<MercadoriaCliente> MercadoriaClientes { get; set; }

        public void AtribuirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
        public void AtribuirPerfil(Perfil perfil)
        {
            //Perfil = perfil;
            PerfilId = perfil.Id;
        }

        public void Ativar()
        {
            Ativo = true;
        }
        public void Desativar()
        {
            Ativo = false;
        }
        public void Bloquear()
        {
            Bloqueado = true;
        }
        public void Desbloquear()
        {
            Bloqueado = false;
        }
    }
}
