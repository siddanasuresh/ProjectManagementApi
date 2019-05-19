using Entities;
using System;
using System.Collections.Generic;
namespace UnitTests
{
    static class TestData
    {
        public static IList<Project> GetProjects()
        {
            return new List<Project>()
            {
               new Project() {ProjectId = 111,  ProjectName ="Mobile Application",StartDate=new DateTime(2018,11,1),EndDate=new DateTime(2019,12,1),ActiveStatus=true, Priority = 1},
               new Project() {ProjectId = 112,  ProjectName ="Project Management1",StartDate=new DateTime(2017,7,19),EndDate=new DateTime(2018,11,6),ActiveStatus=false, Priority = 2},
               new Project() {ProjectId = 113,  ProjectName ="Project Management2",StartDate=new DateTime(2018,12,18),EndDate=new DateTime(2019,11,1),ActiveStatus=true, Priority = 3},
               new Project() {ProjectId = 114,  ProjectName ="Project Management3",StartDate=new DateTime(2018,11,18),EndDate=new DateTime(2019,10,1),ActiveStatus=true, Priority = 10},
               new Project() {ProjectId = 115,  ProjectName ="Project Management4",StartDate=new DateTime(2018,3,1),EndDate=new DateTime(2018,11,1),ActiveStatus=false, Priority = 4},
               new Project() {ProjectId = 1116,  ProjectName ="Project Management5",StartDate=new DateTime(2018,9,1),EndDate=new DateTime(2019,9,1),ActiveStatus=true, Priority = 7}

            };

        }
        public static IList<User> GetUsers()
        {
            return new List<User>()
            {
                new User() {UserId = 4566, EmployeeId=45895, FirstName ="Suresh", LastName="Siddana"},
                new User() {UserId = 34456, EmployeeId=12356, FirstName ="Naresh", LastName="Gr"},
                new User() {UserId = 4566, EmployeeId=88, FirstName ="Suresh1", LastName="Siddana7"},
                new User() {UserId = 8990, EmployeeId=77, FirstName ="Naresh1", LastName="Gr6"},
                new User() {UserId = 12, EmployeeId=55, FirstName ="Suresh22", LastName="Siddana5"},
                new User() {UserId = 33, EmployeeId=66, FirstName ="Naresh44", LastName="Gr4"}
            };

        }
        public static IList<TaskDetail> GetTasks()
        {
            return new List<TaskDetail>()
            {
               new TaskDetail() {Id = 112,  Name ="Initial Design of Project Management API", ProjectId=111,StartDate=new DateTime(2018,11,1),EndDate=new DateTime(2019,12,1),ActiveStatus=false, Priority = 2},
               new TaskDetail() {Id = 113,  Name ="NUNIT TESTS",ProjectId=112,StartDate=new DateTime(2018,11,1),EndDate=new DateTime(2019,12,1),ActiveStatus=true, Priority = 3},
               new TaskDetail() {Id = 114,  Name ="DATABASE DESING",ProjectId=113,StartDate=new DateTime(2018,11,1),EndDate=new DateTime(2019,12,1),ActiveStatus=true, Priority = 10},
               new TaskDetail() {Id = 115,  Name ="TASK 99",ProjectId=1116,StartDate=new DateTime(2018,11,1),EndDate=new DateTime(2019,12,1),ActiveStatus=false, Priority = 4},
            };

        }
        public static IList<ParentTaskDetails> GetParentTaskDetails()
        {
            return new List<ParentTaskDetails>()
            {
                  new ParentTaskDetails() {ParentId = 112,   ParentTask ="Initial Design of Project Management API" },
                  new ParentTaskDetails() {ParentId = 112,   ParentTask ="DATABASE DESING" }
            };
        }
    }
}
