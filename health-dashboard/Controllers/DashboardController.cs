﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using health_dashboard.Models;
using Newtonsoft.Json;

namespace health_dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult Goals()
        {
            return View();
        }

        public IActionResult Index()
        {
            MyViewModel vm = new MyViewModel();

            string api_activities_json = System.IO.File.ReadAllText("./activity-find-1.json");
            List<HealthActivity> api_activities = (List<HealthActivity>)JsonConvert.DeserializeObject(api_activities_json, typeof(List<HealthActivity>));

            Dictionary<string, Dictionary<string, List<HealthActivity>>> activities_by_type = new Dictionary<string, Dictionary<string, List<HealthActivity>>>();
            /*
             *  activities_by_type = [
             *      [type] => [
             *          [date] => [
             *              HealthActivity,
             *          ],
             *      ],
             *  ]
             *  
             **/

            foreach (var a in api_activities)
            {
                if (!activities_by_type.ContainsKey(a.activity_type))
                {
                    activities_by_type.Add(a.activity_type, new Dictionary<string, List<HealthActivity>>());
                }

                DateTime startTime = DateTime.Parse(a.start_time);
                if (!activities_by_type[a.activity_type].ContainsKey(startTime.ToShortDateString()))
                {
                    activities_by_type[a.activity_type].Add(startTime.ToShortDateString(), new List<HealthActivity>());
                }

                activities_by_type[a.activity_type][startTime.ToShortDateString()].Add(a);
            }
            vm.Activities = activities_by_type;

            string challenge_json = System.IO.File.ReadAllText("./exampleChallengeData.json");
            List<object> challenge = (List<object>)JsonConvert.DeserializeObject(challenge_json, typeof(List<object>));
            vm.Challenges = challenge;

            return View(vm);
        }

        public IActionResult Input()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InputAjax()
        {
            // Ping off to HDR

            return Ok();
        }

        public IActionResult Rankings()
        {
            return View();
        }

        public IActionResult RemoveData()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // TEMPORARY BODGE (in this class, at least)
    public class MyViewModel
    {
        public Dictionary<string, Dictionary<string, List<HealthActivity>>> Activities { get; set; }
        public List<object> Challenges { get; set; }
    }
}
