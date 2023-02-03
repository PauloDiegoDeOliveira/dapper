﻿using Dapper;
using Empresa.Dapper.Domain.Core.Interfaces.Repositories;
using Empresa.Dapper.Domain.Entitys;
using Empresa.Dapper.Domain.Enums;
using Empresa.Dapper.Domain.Pagination;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.Base;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Base;
using Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper.ScriptsSql.Participante;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Empresa.Dapper.Infrastructure.Data.Repositorys.Dapper
{
    public class ParticipanteRepository : RepositoryDapperBase<Participante>, IParticipanteRepository
    {
        public ParticipanteRepository(IConfiguration configuration, IDapperScriptBase script) : base(configuration, script)
        {
            script = script as ParticipanteScript;
        }

        public override Task<IEnumerable<Participante>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public async Task<PagedList<Participante>> GetPaginationAsync(ParametersPalavraChave parametersPalavraChave)
        {
            try
            {
                await conexaoSql.OpenAsync();

                using (conexaoSql)
                {
                    IQueryable<Participante> participantes = (await conexaoSql.QueryAsync<Participante>(new ParticipanteScript().GetAll())).AsQueryable();

                    if (parametersPalavraChave.PalavraChave is null && parametersPalavraChave.Id is null && parametersPalavraChave.Status is 0)
                        participantes = participantes.Where(participante => participante.Status != EStatus.Excluido.ToString());
                    else if (parametersPalavraChave.Status != 0)
                        participantes = participantes.Where(participante => participante.Status == parametersPalavraChave.Status.ToString());

                    if (parametersPalavraChave.Id is not null)
                        participantes = participantes.Where(participante => parametersPalavraChave.Id.Contains(participante.Id));

                    // TODO: Verificar erro com like
                    if (!string.IsNullOrEmpty(parametersPalavraChave.PalavraChave))
                        participantes = participantes.Where(participante => EF.Functions.Like(participante.Nome, $"%{parametersPalavraChave.PalavraChave}%"));

                    if (parametersPalavraChave.Ordenar != 0)
                    {
                        switch (parametersPalavraChave.Ordenar.ToString())
                        {
                            case "Crescente":
                                participantes = participantes.OrderBy(participante => participante.Nome);
                                break;

                            case "Decrescente":
                                participantes = participantes.OrderByDescending(participante => participante.Nome);
                                break;

                            case "Novos":
                                participantes = participantes.OrderByDescending(participante => participante.CriadoEm);
                                break;

                            case "Antigos":
                                participantes = participantes.OrderBy(participante => participante.CriadoEm);
                                break;
                        }
                    }

                    return await Task.FromResult(PagedList<Participante>.ToPagedList(participantes, parametersPalavraChave.NumeroPagina, parametersPalavraChave.ResultadosExibidos));
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await conexaoSql.CloseAsync();
            }
        }
    }
}