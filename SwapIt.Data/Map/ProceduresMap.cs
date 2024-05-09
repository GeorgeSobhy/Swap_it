
 
using SwapIt.Data.StoredProcedures.Models;
 
using Microsoft.EntityFrameworkCore;
namespace SwapIt.Data.Map
{
    public class ProceduresMap
    {
        public ProceduresMap(ModelBuilder modelBuilder)
        {

           

            modelBuilder.Entity<IntResult>(entity =>
            {
                entity.HasNoKey();
            });

             
        }

    }
}
