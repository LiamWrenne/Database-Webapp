using Database_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;

namespace Database_Web_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        //[Authorize]
        public IActionResult Database()
        {
                String connectionString = "Data Source=Liam;Initial Catalog=BigDatabase;Integrated Security=True";
                String sql = "SELECT id, region, sector_comb, sizea FROM [BigDatabase].[dbo].[Cyber_Security_Breaches_Survey_2016_raw_data_value_labels] WHERE sizea > 5000";

                var model = new List<Class>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = sql;
                    conn.Open();

                    using SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var Class = new Class();
                        Class.id = (string)rdr["id"];
                        Class.region = (string)rdr["region"];
                        Class.sector_comb = (string)rdr["sector_comb"];
                        Class.sizea = (string)rdr["sizea"];
                        model.Add(Class);
                    }

                }
                return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
