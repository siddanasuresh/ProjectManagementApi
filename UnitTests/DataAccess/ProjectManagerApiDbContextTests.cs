using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using UnitTests.DataAccess.Helpers;
using Moq;
using Xunit;
namespace UnitTests.DataAccess
{
    public class ProjectManagerApiDbContextTests
    {
        [Fact]
        public void ToVerifyModelCreationWorksOrNot()
        {
            var mockModel = new Mock<ModelBuilder>(new ConventionSet());
            try
            {
                var contextOptions = new DbContextOptions<ProjectManagerApiDbContextStub>();
                var projectManagerDbContextStub = new ProjectManagerApiDbContextStub(contextOptions);
                var modelBuilder = new ModelBuilder(new ConventionSet());
                var model = new Model();
                var configSource = new ConfigurationSource();

                var internalModelBuilder = new InternalModelBuilder(model);

                var taskdetail = new TaskDetail();

                var entity = new EntityType("TaskModel", model, configSource);
                var internalEntityTypeBuilder = new InternalEntityTypeBuilder(entity, internalModelBuilder);
                var entityTypeBuilder = new EntityTypeBuilder<TaskDetail>(internalEntityTypeBuilder);
                mockModel.Setup(m => m.Entity<TaskDetail>()).Returns(entityTypeBuilder);
                var property = new Property("Name", taskdetail.GetType(), taskdetail.GetType().GetProperty("Name"), taskdetail.GetType().GetField("Name"), entity, configSource, null);
                var internalPropertyBuilder = new InternalPropertyBuilder(property, internalModelBuilder);
                var propertyBuilder = new PropertyBuilder<string>(internalPropertyBuilder);

                var userDetails = new User();
                var userEntity = new EntityType("User", model, configSource);
                var userInternalEntityTypeBuilder = new InternalEntityTypeBuilder(userEntity, internalModelBuilder);
                var userEntityTypeBuilder = new EntityTypeBuilder<User>(userInternalEntityTypeBuilder);
                mockModel.Setup(m => m.Entity<User>()).Returns(userEntityTypeBuilder);
                var userProperty = new Property("FirstName", userDetails.GetType(), userDetails.GetType().GetProperty("FirstName"), userDetails.GetType().GetField("FirstName"), entity, configSource, null);
                var userInternalPropertyBuilder = new InternalPropertyBuilder(userProperty, internalModelBuilder);
                var userPropertyBuilder = new PropertyBuilder<string>(userInternalPropertyBuilder);

                var projectDetails = new Project();
                var projectEntity = new EntityType("Project", model, configSource);
                var projectInternalEntityTypeBuilder = new InternalEntityTypeBuilder(projectEntity, internalModelBuilder);
                var projectEntityTypeBuilder = new EntityTypeBuilder<Project>(projectInternalEntityTypeBuilder);
                mockModel.Setup(m => m.Entity<Project>()).Returns(projectEntityTypeBuilder);
                var projectProperty = new Property("FirstName", projectDetails.GetType(), projectDetails.GetType().GetProperty("FirstName"), projectDetails.GetType().GetField("FirstName"), entity, configSource, null);
                var projectInternalPropertyBuilder = new InternalPropertyBuilder(projectProperty, internalModelBuilder);
                var projectPropertyBuilder = new PropertyBuilder<string>(projectInternalPropertyBuilder);
                            
                projectManagerDbContextStub.ModelCreation(modelBuilder);
            }
            catch (Exception ex)
            {
                mockModel.Verify(m => m.Entity<TaskDetail>().HasKey("Id"), Times.Once);
                mockModel.Verify(m => m.Entity<User>().HasKey("UserId"), Times.Once);
                mockModel.Verify(m => m.Entity<Project>().HasKey("ProjectId"), Times.Once);
                Assert.NotNull(ex);
            }
        }
    }


}
