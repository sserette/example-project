using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using ExampleProject.Command.Models;

namespace ExampleProject.Command
{
    public class ExampleProjectCommandContext : DbContext
    {
        private DbContextTransaction _currentTransaction;
        public DbSet<Employee> Employees { get; set; }
        public ExampleProjectCommandContext()
            :this("Example")
        {
        }
        public ExampleProjectCommandContext(string nameOrConnectionString)
            :base(nameOrConnectionString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ExampleProjectCommandContext>(null);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public virtual void BeginTransaction()
        {
            try
            {
                if(_currentTransaction != null)
                {
                    return;
                }
                _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public virtual void CloseTransaction()
        {
            CloseTransaction(exception: null);
        }
        public void CloseTransaction(Exception exception)
        {
            try
            {
                if(_currentTransaction != null && exception != null)
                {
                    _currentTransaction.Rollback();
                    return;
                }
                SaveChanges();
                _currentTransaction?.Commit();
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, "The validation errors are: ", fullErrorMessage);
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            catch (Exception)
            {
                if(_currentTransaction != null && _currentTransaction.UnderlyingTransaction.Connection != null)
                {
                    _currentTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                if(_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
