using Microsoft.Data.SqlClient;
using Repository.Pattern.Repositories;
using SwapIt.Data.Entities;
using System.Data;

namespace SwapIt.Repository.Repositories
{
    public class ProcedureRepository : IProcedureRepository
    {

        IRawSqlRepository _repository;
        public ProcedureRepository(IRawSqlRepository repository)
        {
            _repository = repository;
        }
        public List<Service>? GetServices(int serviceTypeId)
        {
            return _repository.QueryList<Service>("exec GetServices @ServiceTypeId",
                new SqlParameter("ServiceTypeId", SqlDbType.Int) { Value = serviceTypeId });
        }
    }


    public interface IProcedureRepository
    {

        List<Service>? GetServices(int serviceTypeId);
    }

}




