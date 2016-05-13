using System.Data.Entity;
using MooseMus.Models;
using MooseMus.Models.Entities;
using MooseMus.Tests;

namespace MooseMus.Tests
{
    class MockDataContext : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDataContext()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            course = new InMemoryDbSet<CourseModel>();
            courseUser = new InMemoryDbSet<CourseUsersModel>();
            project = new InMemoryDbSet<ProjectModel>();
            projectPart = new InMemoryDbSet<ProjectPartModel>();
            result = new InMemoryDbSet<ResultModel>();
            user = new InMemoryDbSet<UserModel>();
        }

        public IDbSet<CourseModel> course { get; set; }
        public IDbSet<CourseUsersModel> courseUser { get; set; }
        public IDbSet<ProjectModel> project { get; set; }
        public IDbSet<ProjectPartModel> projectPart { get; set; }
        public IDbSet<ResultModel> result { get; set; }
        public IDbSet<UserModel> user { get; set; }


        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}
