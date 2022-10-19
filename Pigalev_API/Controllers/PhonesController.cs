using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        // PUT: api/Phones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhones(int id, Phones phones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phones.id_phone)
            {
                return BadRequest();
            }

            db.Entry(phones).State = EntityState.Modified;

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