﻿using Fatec.Clinica.Dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Fatec.Clinica.Dado.Configuracao;
using Fatec.Clinica.Dado.Abstracao;

namespace Fatec.Clinica.Dado
{
    /// <summary>
    /// Funções de CRUD para a Consulta.
    /// </summary>
    public class ConsultaRepositorio : IRepositorioBase<Consulta>
    {

        /// <summary>
        /// Seleciona todos as consultas do Database.
        /// </summary>
        /// <returns>Lista de consultas.</returns>
        public IEnumerable<Consulta> Selecionar()
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var lista = connection.Query<Consulta>($"SELECT C.Id, C.Data_consulta, C.Historico," +
                                                       $"C.Id_Medico, C.Id_Paciente, M.Nome, P.Nome" +
                                                       $"FROM [Consulta] C" +
                                                       $"JOIN [Medico] M ON C.Id_Medico = M.Id" +
                                                       $"JOIN [Paciente] P ON C.Id_Paciente = P.Id");

                return lista;
            }
        }

        /// <summary>
        /// Seleciona uma a consulta do Database, através do Id.
        /// </summary>
        /// <returns>Consulta seleciona.</returns>
        public Consulta SelecionarPorId(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                var obj = connection.QueryFirstOrDefault<Consulta>($"SELECT C.Id, C.Data_consulta, C.Historico," +
                                                       $"C.Id_Medico, C.Id_Paciente, M.Nome, P.Nome" +
                                                       $"FROM [Consulta] C" +
                                                       $"JOIN [Medico] M ON C.Id_Medico = M.Id" +
                                                       $"JOIN [Paciente] P ON C.Id_Paciente = P.Id" +
                                                       $"WHERE C.Id = {id} ");

                return obj;
            }
        }

        /// <summary>
        /// Insere uma consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        /// <returns>ID da consulta inserido no Database.</returns>
        public int Inserir(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                return connection.QuerySingle<int>($"DECLARE @ID int;" +
                                                   $"INSERT INTO [Consulta] " +
                                                   $"(Historico, Data_Consulta, Id_Paciente, Id_Medico) " +
                                                   $"VALUES ('{entity.Historico}'," +
                                                   $" '{entity.Data}'," +
                                                   $" '{entity.Paciente.Id}'," +
                                                   $" '{entity.Medico.Id}'" +
                                                   $"SET @ID = SCOPE_IDENTITY();" +
                                                   $"SELECT @ID");
            }
        }

        /// <summary>
        /// Altera os dados da consulta no Database.
        /// </summary>
        /// <param name="entity">Objeto que contêm os dados da consulta.</param>
        public void Alterar(Consulta entity)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"UPDATE [Consulta] " +
                                   $"SET Historico = '{entity.Historico}'," +
                                   $"Data_Consulta = '{entity.Data}'," +
                                   $"Id_Paciente = '{entity.Paciente.Id}', " +
                                   $"Id_Medico = '{entity.Medico.Id}' " +
                                   $"WHERE Id = {entity.Id}");
            }
        }

        /// <summary>
        /// Deleta uma Consulta do Database
        /// </summary>
        /// <param name="id">Usado para selecionar a consulta no Database.</param>
        public void Deletar(int id)
        {
            using (var connection = new SqlConnection(DbConnectionFactory.SQLConnectionString))
            {
                connection.Execute($"DELETE " +
                                   $"FROM [Consulta] " +
                                   $"WHERE Id = {id}");
            }
        }
    }

}
