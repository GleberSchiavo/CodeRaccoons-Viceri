﻿using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;
using Fatec.Clinica.Dominio;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funcao de CRUD para a Clinica
    /// </summary>
    public class ClinicaRepositorio : IRepositorioBase<Clinicas>
    {

        /// <summary>
        /// Método que seleciona um paciente através do login e da senha.
        /// </summary>
        /// <param name="usuario">Objeto com os dados do usúario.</param>
        /// <returns>Paciente selecionado.</returns>
        public Clinicas Login(string email, string senha)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Clinicas>($"SELECT C.Id " +
                                                                   $"FROM [Clinica] C " +
                                                                   $"WHERE C.Email = '{email}' " +
                                                                   $"AND C.Senha = '{senha}'");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona todos as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Clinicas> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Clinicas>($"SELECT * FROM ViewClinicas");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona uma clinica do Database, através do Id.
        /// </summary>
        /// <returns>clinica seleciona.</returns>
        public Clinicas SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Clinicas>($"SELECT * FROM ViewClinicas " +
                                                                   $"WHERE ViewClinicas.Id = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma clinica do Database através do CNPJ.
        /// </summary>
        /// <returns>clinica seleciona.</returns>
        public Clinicas SelecionarPorCnpj(string cnpj)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Clinicas>($"SELECT * FROM Clinica " +
                                                                   $"WHERE Clinica.Cnpj = '{cnpj}' ");

                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma clinica do Database através do Email.
        /// </summary>
        /// <param name="email">Usado para buscar uma clínica no Database.</param>
        /// <returns>Clínica selecionada.</returns>
        public Clinicas SelecionarPorEmail(string email)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Clinicas>($"SELECT C.Id, C.Senha, C.Cnpj, C.StatusAtividade," +
                                                                   $"C.TelefoneCom, C.Nome, C.Estado, C.Cidade, " +
                                                                   $"C.Bairro, C.Logradouro, C.Numero, C.Complemento " +
                                                                   $"FROM [Clinica] C " +
                                                                   $"WHERE Email = '{email}'");
                return obj;
            }
        }

        /// <summary>
        /// Seleciona uma clinica do Database através do status de atividade, sendo TRUE para ativa e FALSE para inativa.
        /// </summary>
        /// <returns>Lista de clinicas.</returns>
        public IEnumerable<Clinicas> SelecionarPorStatusAtividade(bool status)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Clinicas>($"SELECT * FROM ViewClinicas " +
                                                       $"WHERE Clinica.StatusAtividade = {status}");

                return lista;
            }
        }

        /// <summary>
        /// Insere uma Clinica no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da Clinica.</param>
        /// <returns>ID da Clinica inserido no Database.</returns>
        public int Inserir(Clinicas entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Clinica] " +
                                                   $"(Email, Senha, Cnpj, StatusAtividade, TelefoneCom, Nome, Estado, Cidade, Bairro, Logradouro, Numero, Complemento) " +
                                                   $"VALUES ('{entity.Email}', " +
                                                   $"'{entity.Senha}' , " +
                                                   $"'{entity.Cnpj}'," +
                                                   $" '{entity.StatusAtividade}'," +
                                                   $" '{entity.TelefoneCom}', " +
                                                   $" '{entity.Nome}', " +
                                                   $"'{entity.Estado}', " +
                                                   $"'{entity.Cidade}', " +
                                                   $"'{entity.Bairro}', " +
                                                   $"'{entity.Logradouro}', " +
                                                   $"'{entity.Numero}', " +
                                                   $"'{entity.Complemento}') " +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados da Clinica no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        public void Alterar(Clinicas entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Clinica] " +
                                   $"SET Email = '{entity.Email}', " +
                                   $"Senha = '{entity.Senha}' , " +
                                   $"Cnpj = '{entity.Cnpj}', " +
                                   $"StatusAtividade = '{entity.StatusAtividade}', " +
                                   $"TelefoneCom = '{entity.TelefoneCom}', " +
                                   $"Nome = '{entity.Nome}', " +
                                   $"Estado = '{entity.Estado}' , " +
                                   $"Cidade = '{entity.Cidade}' , " +
                                   $"Bairro = '{entity.Bairro}', " +
                                   $"Logradouro = '{entity.Logradouro}', " +
                                   $"Numero = {entity.Numero}, " +
                                   $"Complemento = '{entity.Complemento}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }
        /// <summary>
        /// Deleta uma Clinica do Database
        /// </summary>
        /// <param name="id">Usado para Deletar uma Clinica no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Clinica] " +
                                   $"WHERE Id = {id}");
            }
        }

    }
}
