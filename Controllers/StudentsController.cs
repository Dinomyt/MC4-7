using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCAPI.Data;
using MVCAPI.Models;

namespace MVCAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context){
            _context = context;
        }

       [HttpPost("CreateStudent")]
       public async Task<IActionResult> CreateStudent(Student newStudent){
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            await _context.AddAsync(newStudent);

            var result = await _context.SaveChangesAsync();

            if (result > 0){
                return Ok("Student Created Successfully");
            } else {
                return BadRequest("Could not create student");
            }
       }

        [HttpGet("/getAllStudents")]
        public async Task<IEnumerable<Student>> getAllStudents(){
            var ourStudents = await _context.Students.AsNoTracking().ToListAsync();
            return ourStudents;
        }

        [HttpGet("studentById")]
        public async Task<ActionResult<Student>> getStudent(int id){
            var ourStudent = await _context.Students.FindAsync(id);
            if (ourStudent == null){
                return NotFound();
            } else {
                return Ok(ourStudent);
            }
        }

        [HttpPut("updateStudentId")]
        public async Task<ActionResult<Student>> updateStudentId(int id, Student student){
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound("Unable to find student with ID: " + id);
            }

            UpdateStudentProperties(existingStudent, student);

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Student updated successfully");
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "An error occurred while updating the student");
            }
        }

        private void UpdateStudentProperties(Student existingStudent, Student newStudent){
            existingStudent.Name = newStudent.Name;
            existingStudent.Address = newStudent.Address;
            existingStudent.PhoneNumber = newStudent.PhoneNumber;
            existingStudent.Email = newStudent.Email;
        }

        [HttpDelete("deleteById")]
        public async Task<ActionResult<Student>> deleteById(int id){
            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound("Unable to find student with ID: " + id);
            }

            try
            {
                _context.Students.Remove(existingStudent); 
                await _context.SaveChangesAsync();
                return Ok("Student deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while trying to delete the student");
            }
        }

        [HttpGet("MadLib")]
        public string MadLib(string Name, string Location, string Verb, string Noun, string Adjective)
        {
            string madLibText = $"{Name} went to {Location} and {Verb} {Adjective} {Noun}.";
            return madLibText;
        }

        [HttpGet("ReverseIt")]
            public ActionResult<string> ReverseIt(string input)
            {
                if (string.IsNullOrEmpty(input))
                    return BadRequest("Input is required.");

                char[] charArray = input.ToCharArray();
                Array.Reverse(charArray);
                return $"Original String: {input} \nReversed: {new string(charArray)}";
            }

        [HttpGet("ReverseItNumbersOnly")]
        public ActionResult<string> ReverseItNumbersOnly(string input)
        {
            if (string.IsNullOrEmpty(input))
                return BadRequest("Input is required.");

            if (!input.All(char.IsDigit))
                return BadRequest("Input must contain only numbers.");

            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return $"Original Input: {input} \nReversed is {new string(charArray)}";
        }

        //  [HttpGet("comparison")]
        //  public string comparison(int x, int y)
        //  {
        //     if (x == y)
        //     {
        //         return $"{x} is equal to {y}";
        //     }
        //     else if (x > y)
        //     {
        //         return $"{x} is greater than {y}\n{y} is less than {x}";
        //     }
        //     else
        //     {
        //         return $"{x} is less than {y}\n{y} is greater than {x}";    
        //     }    
        //  }
    }
}