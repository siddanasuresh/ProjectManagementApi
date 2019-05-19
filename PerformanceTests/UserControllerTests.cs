using DataAccess;
using DataAccess.Interfaces;
using Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NBench;
using Newtonsoft.Json;
using Pro.NBench.xUnit.XunitExtensions;
using ProjectManagerApi;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace PerformanceTests
{
    public class UserControllerTests
    {
        HttpClient httpClient;
        Counter counter;
        TestServer testServer;
        const string counterName = "Peformance Test";
        public UserControllerTests()
        {
            testServer = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<ApiStartup>().UseEnvironment("Development"));
            httpClient = testServer.CreateClient();
        }
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {

            counter = context.GetCounter(counterName);
        }

        [NBenchFact]
        [PerfBenchmark(Description = "To test GetAllUsers function", NumberOfIterations = 10, RunMode = RunMode.Throughput, RunTimeMilliseconds = 5000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [CounterThroughputAssertion(counterName, MustBe.GreaterThan, 30)]
        public void GetAllUsers()
        {
            var result = httpClient.GetAsync("/api/Users").Result;
            counter.Increment();
        }

        [NBenchFact]
        [PerfBenchmark(Description = "To test GetUser function", NumberOfIterations = 10, RunMode = RunMode.Throughput, RunTimeMilliseconds = 5000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [CounterThroughputAssertion(counterName, MustBe.GreaterThan, 30)]
        public void GetUser()
        {
            var result = httpClient.GetAsync("/api/Users/112").Result;
            counter.Increment();
        }

        [NBenchFact]
        [PerfBenchmark(Description = "To test post function", NumberOfIterations = 10, RunMode = RunMode.Throughput, RunTimeMilliseconds = 5000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [CounterThroughputAssertion(counterName, MustBe.GreaterThan, 30)]
        public void PostUser()
        {
            var result = httpClient.PostAsync("/api/Users", new StringContent(JsonConvert.SerializeObject(TestData.GetUsers().ToList<User>().Find(x => x.UserId == 112)), Encoding.UTF8, "application/json")).Result;
            counter.Increment();
        }

        [NBenchFact]
        [PerfBenchmark(Description = "To test put function", NumberOfIterations = 10, RunMode = RunMode.Throughput, RunTimeMilliseconds = 5000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [CounterThroughputAssertion(counterName, MustBe.GreaterThan, 30)]
        public void PutUser()
        {
            var result = httpClient.PostAsync("/api/Users/112", new StringContent(JsonConvert.SerializeObject(TestData.GetUsers().ToList<User>().Find(x => x.UserId == 112)), Encoding.UTF8, "application/json")).Result;
            counter.Increment();
        }
        [NBenchFact]
        [PerfBenchmark(Description = "To test delete function", NumberOfIterations = 10, RunMode = RunMode.Throughput, RunTimeMilliseconds = 5000, TestMode = TestMode.Measurement)]
        [CounterMeasurement(counterName)]
        [CounterThroughputAssertion(counterName, MustBe.GreaterThan, 30)]
        public void DeleteUser()
        {
            var result = httpClient.DeleteAsync("/api/Users/112").Result;
            counter.Increment();
        }

    }

    public class ApiStartup : Startup
    {
        public ApiStartup(IConfiguration configuration) : base(configuration)
        {

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigOtherServices(IServiceCollection services)
        {

            services.AddDbContext<ProjectManagerApiDbContext>(options => options.UseInMemoryDatabase("test-database"));

            services.AddTransient<IParentTaskDetails, ParentTaskDetail>();
            services.AddTransient<ITask, TaskRepository>();
            services.AddTransient<IProject, ProjectRepository>();
            services.AddTransient<IUser, UserRepository>();
        }
    }
}
