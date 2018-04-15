﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Burger.Api.Data;
using Burger.Api.Models;

namespace Burger.Api.Controllers
{
    public class IngredientsController : ApiController
    {
        private readonly Context context;

        public IngredientsController(Context context)
        {
            this.context = context;
        }


        // GET ingredients
        public IEnumerable<Ingredient> Get()
        {
            return context.Ingredients;
        }

        // GET ingredients/5
        public Ingredient Get(short id)
        {
            return context.Ingredients.Find(id);
        }

        //// POST ingredients
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT ingredients/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE ingredients/5
        //public void Delete(int id)
        //{
        //}
    }
}