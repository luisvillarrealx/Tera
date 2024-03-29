﻿using Microsoft.AspNetCore.Mvc;
using Tera_Web.Entities;
using Tera_Web.Filters;
using Tera_Web.Models;

namespace Tera_Web.Controllers
{
    [FilterSessionValidation]
    public class ConsolidatedController : Controller
    {
        //este se usa por un problema en las validaciones 
        ConsolidatedObj consolidatedList = new ConsolidatedObj();
        //esto es lo demas
        ConsolidatedObj consolidatedObj = new ConsolidatedObj();
        ConsolidatedModel consolidatedModel = new ConsolidatedModel();
        // GET: ConsolidatedController

        public ActionResult Consolidated()
        {
            List<ConsolidatedObj> _Consolidated = new List<ConsolidatedObj>();
            _Consolidated = consolidatedModel.GetConsolidatedList().ToList();
            return View(_Consolidated);
        }

        public ActionResult ConsolidatedFilter(DateTime min, DateTime max)
        {
            List<ConsolidatedObj> consolidatedList = new List<ConsolidatedObj>();
            consolidatedList =  consolidatedModel.GetConsolidatedListFilter(min, max);
            return View(consolidatedList);
        }


    }
}
