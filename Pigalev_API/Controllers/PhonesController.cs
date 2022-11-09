using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using Pigalev_API.Models;

namespace Pigalev_API.Controllers
{
    public class PhonesController : ApiController
    {
        private PhoneEntities db = new PhoneEntities();

        // GET: api/Phones
        [ResponseType(typeof(List<PhoneModel>))] 
        public IHttpActionResult GetPhones()
        {
            return Ok(db.Phones.ToList().ConvertAll(x => new PhoneModel(x)));
        }

        // GET: api/Phones/5
        [ResponseType(typeof(Phones))]
        public IHttpActionResult GetPhones(int id)
        {
            Phones phones = db.Phones.Find(id);
            if (phones == null)
            {
                return NotFound();
            }

            return Ok(phones);
        }

        // GETSearch: api/Phones
        [ResponseType(typeof(List<PhoneModel>))]
        public IHttpActionResult GetPhonesSearch(string fieldSearch, string textSearch, string fieldSort, string valueSort) // Метод для поиска и сортировки
        {
            List<Phones> phones = db.Phones.ToList();
            if (textSearch != null)
            {
                if (fieldSort != null)
                {
                    phones = Search(phones, fieldSearch, textSearch);
                    phones = Sorting(phones, fieldSort, valueSort);
                }
                else
                {
                    phones = Search(phones, fieldSearch, textSearch);
                }
            }
            else
            {
                if (fieldSort != null)
                {
                    phones = Sorting(phones, fieldSort, valueSort);
                }
            }
            return Ok(phones);
        }

        public List<Phones> Search(List<Phones> phones, string fieldSearch, string textSearch) // Метод для поиска
        {
            Regex regex = new Regex("^(" + textSearch + ")");
            switch (fieldSearch)
            {
                case ("manufacturer"):
                    phones = phones.Where(x => regex.IsMatch(x.manufacturer)).ToList();
                    break;
                case ("model"):
                    phones = phones.Where(x => regex.IsMatch(x.model)).ToList();
                    break;
                case ("colour"):
                    phones = phones.Where(x => regex.IsMatch(x.colour)).ToList();
                    break;
            }
            return phones;
        }

        public List<Phones> Sorting(List<Phones> phones, string fieldSort, string valueSort) // Метод для сортировки
        {
            switch (fieldSort)
            {
                case ("manufacturer"):
                    if (valueSort == "ascending")
                    {
                        phones = phones.OrderBy(x => x.manufacturer).ToList();

                    }
                    else
                    {
                        phones = phones.OrderByDescending(x => x.manufacturer).ToList();
                    }
                    break;
                case ("model"):
                    if (valueSort == "ascending")
                    {
                        phones = phones.OrderBy(x => x.model).ToList();

                    }
                    else
                    {
                        phones = phones.OrderByDescending(x => x.model).ToList();
                    }
                    break;
                case ("colour"):
                    if (valueSort == "ascending")
                    {
                        phones = phones.OrderBy(x => x.colour).ToList();

                    }
                    else
                    {
                        phones = phones.OrderByDescending(x => x.colour).ToList();
                    }
                    break;
                case ("price"):
                    if (valueSort == "ascending")
                    {
                        phones = phones.OrderBy(x => x.price).ToList();

                    }
                    else
                    {
                        phones = phones.OrderByDescending(x => x.price).ToList();
                    }
                    break;
            }
            return phones;
        }

        // PUT: api/Phones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhones(int id, Phones phones)
        {
            var dbPhones = db.Phones.FirstOrDefault(x => x.id_phone.Equals(id));

            dbPhones.manufacturer = phones.manufacturer;
            dbPhones.model = phones.model;
            dbPhones.colour = phones.colour;
            dbPhones.price = phones.price;
            dbPhones.image = phones.image;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhonesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Phones
        [ResponseType(typeof(Phones))]
        public IHttpActionResult PostPhones(Phones phones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Phones.Add(phones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = phones.id_phone }, phones);
        }

        // DELETE: api/Phones/5
        [ResponseType(typeof(Phones))]
        public IHttpActionResult DeletePhones(int id)
        {
            Phones phones = db.Phones.Find(id);
            if (phones == null)
            {
                return NotFound();
            }

            db.Phones.Remove(phones);
            db.SaveChanges();

            return Ok(phones);
        }

        // DELETE: api/Phones
        [ResponseType(typeof(Phones))]
        public IHttpActionResult DeletePhones()
        {
            foreach (Phones p in db.Phones.ToList())
            {
                db.Phones.Remove(p);
            }
            db.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhonesExists(int id)
        {
            return db.Phones.Count(e => e.id_phone == id) > 0;
        }
    }
}