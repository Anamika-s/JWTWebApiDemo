using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiWithEF.Models;

namespace WebApiWithEF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        List<Note> notes = null;
        

        public NotesController()
        {
            if (notes == null)
            {
                notes = new List<Note>()
                {

                    new Note(){ Id=1, Title="Note 1", Text="This is note 1"},
                    new Note(){ Id=2, Title="Note 2", Text="This is note 2"}
                };
            }

        }

        // GET: api/Employees
        [HttpGet]
        public  ActionResult<List<Note>> GetNotes()
        {
            return   notes.ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public  ActionResult<Note> GetNote(int id)
        {
            Note note = notes.FirstOrDefault(x=>x.Id==id);

            if (note == null)
            {
                return NotFound();
            }

            else
            return Ok(note);
        }

         [HttpPut("{id}")]
      
        public  ActionResult<Note> PutNote(int? id, Note note)
        {
            if (id == null)
            {
                return BadRequest();
            }

             Note temp = notes.FirstOrDefault(x => x.Id == id);

            if (note == null)
            {
                return NotFound();
            }

            else
            {
                foreach (Note obj in notes)
                {
                    if (obj.Id == id)
                    {
                        obj.Text = "Text changed";
                        obj.Title = "Title Changed";
                    }
                }
                return temp;
            }
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Note> PostNote(Note note)
        {
            notes.Add(note);

            return Created("Ok",note);
        }

        public ActionResult<Note> DeleteNote(int id)
        {
            Note temp = notes.FirstOrDefault(x => x.Id == id);

            if (temp == null)
            {

                return NotFound();
            }

            else
                notes.Remove(temp);
           return Ok(temp);
             
        }

        //private bool EmployeeExists(int id)
        //{
        //    return _context.Employees.Any(e => e.Id == id);
        //}
    }
}
