using DemoCRUDASPCore.Data;
using DemoCRUDASPCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace DemoCRUDASPCore.Controllers
{
    public class PersonController : Controller
    {
        private readonly PersonContext _context;
        private readonly IWebHostEnvironment _hostenvironment;
        public PersonController(PersonContext context, IWebHostEnvironment hotEnv)
        {
            _context = context;
            _hostenvironment = hotEnv;
        }
        public async Task<IActionResult> Index()
        {
            var persons = await _context.Person.ToListAsync();
            return View(persons);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Person person)
        {
            string fileName = UploadImgs(person);
            person.ImagName = fileName;
            _context.Add(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public string UploadImgs(Person person)
        {
            string fileName = null;
            string uploadDir = Path.Combine(_hostenvironment.WebRootPath, "images");
            fileName = Guid.NewGuid().ToString() + "-" + person.ImagFile.FileName;
            string filePath = Path.Combine(uploadDir, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                person.ImagFile.CopyTo(fileStream);
            }
            return fileName;
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }
            var person = await _context.Person.FirstOrDefaultAsync(c => c.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var person = await _context.Person.FirstOrDefaultAsync(c => c.PersonID == id);
            if (person == null) return NotFound();
            return View(person);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Person person)
        {
            if (person == null) return BadRequest();
            var p = await _context.Person.FirstOrDefaultAsync(c => c.PersonID == id);
            p.PersonName = person.PersonName;
            p.PersonAddress = person.PersonAddress;
            if (person.ImagName != null)
            {
                //Delete oId img
                string filepath = Path.Combine(_hostenvironment.WebRootPath, "images", person.ImagName);
                System.IO.File.Delete(filepath);
            }

            p.ImagName = UploadImgs(person);
            _context.Update(p);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
