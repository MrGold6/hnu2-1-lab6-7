using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabW4.Data;
using LabW4.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LabW4.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LabW4Context _context;

        public StudentsController(LabW4Context context)
        {
            _context = context;
        }

        // GET: Students
        //16. Продемонструвати сортування даних в таблицях
        //12. Продемонструвати роботу оператора join
        //18. Продемонструвати операції з множинами: перетин
        public async Task<IActionResult> Index()
        {
            var students = await _context.Student
               .Include(s => s.University)
               .Include(s => s.Voucher)
               .OrderBy(s => s.Name)
               .ToListAsync();

            return View(students);
        }

        // GET: Students/StudentLeftJoin
        //18. Продемонструвати операції з множинами: різниця.
        public IActionResult StudentLeftJoin()
        {
            var students = _context.Student;
            var vouchers = _context.Voucher;


            var query = students.GroupJoin(vouchers, student => student.VoucherId, voucher => voucher.VoucherId,
                (student, voucher) => new { student, subgroup = voucher.DefaultIfEmpty() })
                .Select(gj => new
                {
                    gj.student.Name,
                    gj.student.Lastname,
                    Voucher = gj.subgroup.FirstOrDefault().City
                });

            foreach (var v in query){
                Console.WriteLine($"{v.Name:-15} {v.Lastname:-15}: {v.Voucher}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Students/StudentFullJoin
        //17. Продемонструвати з'єднання таблиць.
        //18. Продемонструвати операції з множинами: об'єднання
        //13. При формуванні запитів використати анонімні типи.
        public IActionResult StudentFullJoin()
        {
            var students = _context.Student;
            var vouchers = _context.Voucher;

            var queryLeftJoin = students.GroupJoin(vouchers, student => student.VoucherId, voucher => voucher.VoucherId,
               (student, voucher) => new { student, subgroup = voucher.DefaultIfEmpty() })
               .Select(gj => new
               {
                   gj.student.Name,
                   gj.student.Lastname,
                   Voucher = gj.subgroup.FirstOrDefault().City
               });

            var queryRightJoin = vouchers.GroupJoin(students, voucher => voucher.VoucherId, student => student.VoucherId,
               (voucher, student) => new { voucher, subgroup =  student.DefaultIfEmpty() })
               .Select(gj => new
               {
                   gj.subgroup.FirstOrDefault().Name,
                   gj.subgroup.FirstOrDefault().Lastname,
                   Voucher =  gj.voucher.City
               });

            var FullOuterJoin = queryRightJoin.Union(queryLeftJoin);

            foreach (var v in FullOuterJoin)
            {
                Console.WriteLine($"{v.Name:-15} {v.Lastname:-15}: {v.Voucher}");
            }

            return RedirectToAction(nameof(Index));
        }

        //10. Використовуючи оператор where забезпечити відображення інформації, відібраної запевним критерієм.
        //15. Використати методи запитів при формуванні запитів до бази даних 
        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.University)
                .Include(s => s.Voucher)
                .Where(m => m.StudentId == id)
                .FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["UniversityId"] = new SelectList(_context.University, "UniversityId", "UniversityId");
            ViewData["VoucherId"] = new SelectList(_context.Voucher, "VoucherId", "VoucherId");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,Surname,Lastname,Age,Email,PhoneNumber,Address,Date_of_admission,IsGraduated,Atestat,NumbeOfRecordBook,Document,Group,Speciality,Course,VoucherId,UniversityId")] Student student)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["UniversityId"] = new SelectList(_context.University, "UniversityId", "UniversityId", student.UniversityId);
            ViewData["VoucherId"] = new SelectList(_context.Voucher, "VoucherId", "VoucherId", student.VoucherId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,Name,Surname,Lastname,Age,Email,PhoneNumber,Address,Date_of_admission,IsGraduated,Atestat,NumbeOfRecordBook,Document,Group,Speciality,Course,VoucherId,UniversityId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
     
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.University)
                .Include(s => s.Voucher)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
