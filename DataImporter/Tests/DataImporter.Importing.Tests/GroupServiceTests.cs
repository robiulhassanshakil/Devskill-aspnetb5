using System;
using System.Diagnostics.CodeAnalysis;
using Autofac.Extras.Moq;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Entities;
using DataImporter.Importing.Repositories;
using DataImporter.Importing.Services;
using DataImporter.Importing.UniteOfWorks;
using Moq;
using NUnit.Framework;
using Shouldly;
using Group = DataImporter.Importing.BusinessObjects.Group;

namespace DataImporter.Importing.Tests
{
    [ExcludeFromCodeCoverage]
    public class GroupServiceTests
    {
        private AutoMock _mock;
        private Mock<IImportingUnitOfWork> _importingUniteOfWorkMock;
        private Mock<IGroupRepository> _groupRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IGroupService _groupService;

        [OneTimeSetUp]
        public void ClassSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            _mock?.Dispose();
        }

        [SetUp]
        public void TestSetup()
        {
            _importingUniteOfWorkMock = _mock.Mock<IImportingUnitOfWork>();
            _groupRepositoryMock = _mock.Mock<IGroupRepository>();
            _mapperMock = _mock.Mock<IMapper>();
            _groupService = _mock.Create<GroupService>();
        }

        [TearDown]
        public void TestCleanup()
        {
            _importingUniteOfWorkMock.Reset();
            _groupRepositoryMock.Reset();
            _mapperMock.Reset();
        }
        [Test]
        public void CreateGroup_GroupDoesNotExist_ThrowException()
        {
            //Arrange
            Group group = null;
            //Act and Assert
            Should.Throw<InvalidOperationException>(
                () => _groupService.CreateGroup(group)
            );
        }

        /* public void CreateGroup_GroupNameExist_ThrowException()
         {
             //Arrange
             var  group = new Group()
             {
                 Name = "DevSkill"
             };


             //Act


             //Assert
             Should.Throw<InvalidOperationException>(
                 () => _groupService.CreateGroup(group)
             );
         }*/
        [Test]
        public void CreateGroup_GroupExist_CreateGroup()
        {
            //Arrange
            var group =new Group()
            {
                Name = "DevSkill"
            } ;
            var gp = new Entities.Group()
            {
                Name = group.Name
            };
            
            _importingUniteOfWorkMock.Setup(x => x.Groups).Returns(_groupRepositoryMock.Object);
            _groupRepositoryMock.Object.Add(gp);
            _importingUniteOfWorkMock.Setup(x => x.Save()).Verifiable();
            //Act
            _groupService.CreateGroup(group);
            //Assert
            this.ShouldSatisfyAllConditions(
                () => _importingUniteOfWorkMock.VerifyAll(),
                ()=>_groupRepositoryMock.VerifyAll());
            
        }

    }
}