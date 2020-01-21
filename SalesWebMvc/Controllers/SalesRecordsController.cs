﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // If minDate wasn't given, consider it to be the first day of current year
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now; // If maxDate wasn't given, consider it to be the current date
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // Dictionary ViewData value for key "minDate"
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); // Dictionary ViewData value for key "maxDate"

            List<SalesRecord> result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); // If minDate wasn't given, consider it to be the first day of current year
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now; // If maxDate wasn't given, consider it to be the current date
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd"); // Dictionary ViewData value for key "minDate"
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd"); // Dictionary ViewData value for key "maxDate"

            List<IGrouping<Department, SalesRecord>> result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(result);
        }
    }
}