using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.DataAccess.Helpers
{
    public class ProjectManagerApiDbContextStub : ProjectManagerApiDbContext
    {
        public ProjectManagerApiDbContextStub(DbContextOptions options) : base(options)
        {

        }
        public void ModelCreation(ModelBuilder model)
        {
            OnModelCreating(model);
        }
    }
}
